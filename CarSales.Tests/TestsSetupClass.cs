using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace CarSales.Tests
{
    [SetUpFixture]

    public class TestsSetupClass
    {
        private IContainer container;
        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            container = new ContainerBuilder()
              // Set the image for the container
              .WithImage("redis:latest")
              // Bind port 6379 of the container to a port 6379 on the host.
              .WithPortBinding(6379, 6379)
            // Wait until the HTTP endpoint of the container is available.
              .WithCleanUp(true)
              .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
            // Build the container configuration.
              .Build();

            await container.StartAsync();
        }

        [OneTimeTearDown]
        public async Task GlobalTeardown()
        {
            await container.StopAsync();
            
        }
    }
}
