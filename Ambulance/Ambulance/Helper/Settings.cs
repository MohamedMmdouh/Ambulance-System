using Plugin.Settings;
using Plugin.Settings.Abstractions;

using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Helper
{
    public static class Settings
    { 
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;
        private const string SettingsKey2 = "permentdata";
        private static readonly string SettingsDefault2 = string.Empty;
        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        public static string permantdata
        {

            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey2, SettingsDefault2);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey2, value);
            }
        }
        public static void clearAllData()
        {
            AppSettings.Clear();
        }

        public const string AzureUrl = "https://ambulancesystem.azurewebsites.net/api/";

        public const string AzureHub = "https://ambulancesystem.azurewebsites.net/RequestHub/";

        public const string Ngrok = "https://ambulancesystem.azurewebsites.net/api/";

        public const string NgrokHub = "https://ambulancesystem.azurewebsites.net/RequestHub/";
        
    }
}