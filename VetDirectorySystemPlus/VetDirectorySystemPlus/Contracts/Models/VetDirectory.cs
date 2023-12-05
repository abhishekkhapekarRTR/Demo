using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VetDirectorySystemPlus.Contracts.Models
{
    /// <summary>
    /// This contract is used to store the details of vet
    /// </summary>
    public class VetDirectory
    {
        public string? StoreName { get; set; }
        public string? PrimaryAddress { get; set; } 
        public string? StoreHours { get; set; } 
        public string? StoreLocatorPhone { get; set; }
    }
}
