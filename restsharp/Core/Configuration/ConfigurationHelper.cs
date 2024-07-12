using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Core.Configuration
{
    public static class ConfigurationHelper
    {
        private static IConfigurationRoot _configuration = null;

        public static IConfiguration ReadConfiguration(string path)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path)
                .Build();
            _configuration = builder;
            return _configuration;
        }

        public static IConfigurationRoot GetConfiguration()
        {
            return _configuration;
        }

        public static string GetValueByKey(string key)
        {
            if (_configuration == null)
            {
                throw new Exception("Configuration not found");
            }
            return _configuration[key];
        }

    }
}