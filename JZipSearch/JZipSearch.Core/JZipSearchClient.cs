using System;
using System.Threading.Tasks;
using JZipCodeSearchClient;

namespace JZipSearch.Core
{
    public static class JZipSearchClient
    {
        public static async Task<Address[]> ZipToAddress(string zipCode) => await ZipSearchClient.ZipToAddress(zipCode);
        public static async Task<Address[]> AddressToZip(string pref, string address) => await ZipSearchClient.AddressToZip(pref, address);
    }
}
