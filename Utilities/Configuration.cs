
using Microsoft.Extensions.Configuration;

namespace APITesting.Utilities
{
    public static class Configuration
    {
        public static IConfigurationRoot? ConfigurationSettings { get; set; }
       
        public static string ApiUrl => ConfigurationSettings.GetConnectionString($"apiUrl");
        public static string ApiToken => ConfigurationSettings.GetConnectionString($"apiToken");
       

        /// <summary>
        /// Setup the configuration to  get the app settings from appsettings.json
        /// </summary>
        public static void SetupConfigFile()
        {

            // Build and combine the configuration file
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);
            ConfigurationSettings = builder.Build();
        }
    }
}
