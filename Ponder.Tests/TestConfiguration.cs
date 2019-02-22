using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Ponder.Tests
{
    public class TestConfiguration : IConfiguration
    {
        public TestConfiguration()
        {
        }

        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
