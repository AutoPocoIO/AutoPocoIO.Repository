using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AutoPocoIO.Repository.Extensions;
using AutoPocoIO.Repository.Test.Services.TestUtilities;
using Microsoft.EntityFrameworkCore;

namespace AutoPocoIO.Repository.Test.Extensions
{
    public class DataServiceCollectoinExtensionsTests
    {
        [Fact]

        public void AddGenericMappingServices_Throws_WhenNoContextsRegistered()
        {
            ServiceCollection services = new();

            Assert.Throws<InvalidOperationException>(() => services.AddGenericMappingServices());
        }

        [Fact]
        public void AddGenericMappingServices_AddDbContext_WhenContextsRegistered()
        {
            ServiceCollection services = new();
            services.AddDbContext<TestDbContext>();

            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<DbContext>());
        }
    }
}

