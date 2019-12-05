using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace GrowthTrigal.Common.Helpers
{

    public static class Settings
    {


        private const string SettingsKey = "settings_key";
        private const string _token = "token";
        private const string _farm = "farm";
        private const string _isRememberd = "IsRemembered";
        private static readonly bool _boolDefault = false;

        private static readonly string _stringDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;



        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);

            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Farm
        {
            get => AppSettings.GetValueOrDefault(_farm, _stringDefault);

            set => AppSettings.AddOrUpdateValue(_farm, value);
        }


        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRememberd, _boolDefault);

            set => AppSettings.AddOrUpdateValue(_isRememberd, value);
        }
    }

}
