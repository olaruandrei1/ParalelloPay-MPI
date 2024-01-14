using System.Text.Json;
using ParalelloPay_MPI.Application.Contracts;
using ParalelloPay_MPI.Domain;

namespace ParalelloPay_MPI.Application.Implementation;

public class Process : IProcess
{
    private readonly IPaymentService _paymentService;

    public Process(IPaymentService paymentService) => _paymentService = paymentService;
    
    public async Task StartProcess(string[] requests)
    {
        try
        {
            var payments = new List<PaymentRequest>();

            foreach (var json in requests)
            {
                var payment = JsonSerializer.Deserialize<PaymentRequest>(json);
                payments.Add(payment);
            }

            await _paymentService.ProcessPayments(payments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}