using AutoPocoIO.Repository.Extensions;
using AutoPocoIO.Repository.Test.Services.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

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
            services.AddGenericMappingServices();

            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<DbContext>());
        }
    }
}