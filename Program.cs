using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParalelloPay_MPI.Application.Contracts;
using ParalelloPay_MPI.Application.Implementation;
using ParalelloPay_MPI.Persistence;
using ParalelloPay_MPI.Application;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .RegisterPersistenceDependencies()
            .RegisterApplicationDependencies()
            .BuildServiceProvider();

        var processService = serviceProvider.GetRequiredService<IProcess>();

        await processService.StartProcess(args);
    }
}