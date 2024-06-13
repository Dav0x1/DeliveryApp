using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeliveryApp.Helpers
{
    public static class LanguageResources
    {
        private static readonly ResourceDictionary LanguageDictionary = (ResourceDictionary)Application.Current.Resources["LanguageDictionary"];

        public static string GetStringValue(string key)
        {
            if(LanguageDictionary is null)
                return string.Empty;

            return LanguageDictionary.Contains(key) ? LanguageDictionary[key] as string : string.Empty;
        }
    }
}
