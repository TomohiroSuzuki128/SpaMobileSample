using System;
using System.Threading.Tasks;
using JZipCodeSearchClient;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace JZipSearch.Core
{
    public static class JZipSearchClient
    {
        public static async Task<Address[]> ZipToAddress(string zipCode) => await ZipSearchClient.ZipToAddress(zipCode);
        public static async Task<Address[]> AddressToZip(string pref, string address) => await ZipSearchClient.AddressToZip(pref, address);
        public static async Task<Prefecture[]> Prefectures() => await ZipSearchClient.Prefectures();
        public static async Task<bool> RefreshSavedPrefectures()
        {
            var prefectures = await ZipSearchClient.Prefectures();
            var prefecturesJson = JsonConvert.SerializeObject(prefectures);
            var savedPrefecturesJson = SavedPrefecturesJson();
            if (prefecturesJson == savedPrefecturesJson) return false;
            SetSavedPrefecturesJson(prefecturesJson);
            return true;
        }
        public static Prefecture[] SavedPrefectures() => JsonConvert.DeserializeObject<Prefecture[]>(Preferences.Get("prefectures", ""));
        static string SavedPrefecturesJson() => Preferences.Get("prefectures", "");
        static void SetSavedPrefecturesJson(string json) => Preferences.Set("prefectures", json);
    }
}
