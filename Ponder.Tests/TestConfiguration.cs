using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Ponder.Tests
{
    /// <summary>
    /// Stub IConfiguration implementation for testing
    /// </summary>
    public class TestConfiguration : IConfiguration
    {
        private Dictionary<string, string> _config;
        public TestConfiguration()
        {
            _config = new Dictionary<string, string>();
        }

        public string this[string key]{ get => _config[key]; set => _config[key] = value; }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public Microsoft.Extensions.Primitives.IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
