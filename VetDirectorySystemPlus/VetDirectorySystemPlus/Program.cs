using HtmlAgilityPack;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection.Metadata;
using VetDirectorySystemPlus.Contracts.Models;
using CsvHelper;

public class VatDirectory
{

    public static void Main(string[] args)
    {
        string url = "https://www.1800petmeds.com/vetdirectory?horizontalView=true";
        var response = CallUrl(url);

        //Getting the response as vetDictionary list
        var vetResponse = InitializeVetList(response);

        //Preparing csv file
        PrepareCSVFile(vetResponse);

        Console.ReadLine();
    }

    private static HtmlDocument CallUrl(string fullUrl)
    {
        var web = new HtmlWeb();

        // visiting the target web page 
        var response =  web.Load(fullUrl);
        return response;
    }

    private static List<VetDirectory> InitializeVetList(HtmlDocument response)
    {
        //// creating the list that will keep the scraped data of vet
        var VetDirectoryLists = new List<VetDirectory>();

        // getting the list of HTML markupt vet nodes
        var vetDirectoryHTMLElements = response.DocumentNode.QuerySelectorAll("div.store-details");

        foreach (var item in vetDirectoryHTMLElements)
        {
            var storeName = HtmlEntity.DeEntitize(item.QuerySelector("div.store-name")?.InnerText);
            var address = HtmlEntity.DeEntitize(item.QuerySelector("address")?.InnerText);
            var storeHours = HtmlEntity.DeEntitize(item.QuerySelector("div.store-hours").InnerText);
            var storeLocatorPhone = HtmlEntity.DeEntitize(item.QuerySelector("a.storelocator-phone").InnerText);

            var VetDirectoryList = new VetDirectory() { StoreName = storeName, PrimaryAddress = address, StoreHours = storeHours, StoreLocatorPhone = storeLocatorPhone };

            VetDirectoryLists.Add(VetDirectoryList);
        }
        return VetDirectoryLists;
    }

    private static void PrepareCSVFile(List<VetDirectory> VetDirectoryLists)
    {
        // crating the CSV output file 
        using (var writer = new StreamWriter("VetDictionary.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(VetDirectoryLists);
        }
    }
}
