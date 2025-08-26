namespace Vinder.IdentityProvider.Sdk.TestSuite.Fixtures;

public sealed class IdentityProviderFixture : IAsyncLifetime
{
    private IContainer _mongoContainer = default!;
    private IContainer _identityProviderContainer = default!;
    private INetwork _network = default!;

    public HttpClient HttpClient { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        _network = new NetworkBuilder()
            .WithName(Guid.NewGuid().ToString())
            .Build();

        await _network.CreateAsync();

        _mongoContainer = new ContainerBuilder()
            .WithImage("mongo:latest")
            .WithNetwork(_network)
            .WithNetworkAliases("mongo")
            .WithCleanUp(true)
            .WithExposedPort(27017)
            .WithPortBinding(0, 27017)
            .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "admin")
            .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "admin")
            .Build();

        await _mongoContainer.StartAsync();

        var connectionString = $"mongodb://admin:admin@mongo:27017";
        var secretKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        _identityProviderContainer = new ContainerBuilder()
            .WithImage("vinderio/identity.provider:latest")
            .WithCleanUp(true)
            .WithNetwork(_network)
            .WithExposedPort(8080)
            .WithPortBinding(0, 8080)
            .WithEnvironment("Settings__Database__ConnectionString", connectionString)
            .WithEnvironment("Settings__Security__SecretKey", secretKey)
            .WithEnvironment("Settings__Database__DatabaseName", "vinder-identity-provider")
            .WithEnvironment("Settings__Administration__Username", "admin")
            .WithEnvironment("Settings__Administration__Password", "admin")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Application started"))
            .Build();

        await _identityProviderContainer.StartAsync();

        var port = _identityProviderContainer.GetMappedPublicPort(8080);
        var host = $"http://localhost:{port}";

        HttpClient = new HttpClient { BaseAddress = new Uri(host) };
    }

    public async Task DisposeAsync()
    {
        await _identityProviderContainer.StopAsync();
        await _identityProviderContainer.DisposeAsync();

        await _mongoContainer.StopAsync();
        await _mongoContainer.DisposeAsync();

        await _network.DeleteAsync();
        await _network.DisposeAsync();
    }
}
