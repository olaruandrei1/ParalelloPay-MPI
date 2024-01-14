using Microsoft.Extensions.DependencyInjection;
using ParalelloPay_MPI.Application.Contracts;
using ParalelloPay_MPI.Application.Implementation;

namespace ParalelloPay_MPI.Application;

public static class RegisterApplication
{
    public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection service)
    {
        service.AddSingleton<IProcess, Process>()
            .AddSingleton<IPaymentService, PaymentService>();

        return service;
    }
}