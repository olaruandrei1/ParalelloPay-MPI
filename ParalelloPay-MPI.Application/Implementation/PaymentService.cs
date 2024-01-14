using ParalelloPay_MPI.Application.Contracts;
using ParalelloPay_MPI.Domain;

namespace ParalelloPay_MPI.Application.Implementation;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repo;

    public PaymentService(IPaymentRepository repo) => _repo = repo;
    
    public async Task ProcessPayments(List<PaymentRequest> request)
    {
        var rank = MPIService.GetRank();
        var size = MPIService.GetSize();

        var totalPayments = request.Count;
        var paymentsPerProcess = totalPayments / size;
        var startIndex = rank * paymentsPerProcess;
        var endIndex = startIndex + paymentsPerProcess;

        for (var i = startIndex; i < endIndex; i++)
        {
            if (!await _repo.CheckBalance(new() { }))
            {
                MPIService.BroadcastMessage($"ERR_IBL_01 for : {i}");
                MPIService.FinishMPI();
            }

            if (!await _repo.CheckCreditorValidation(new() { }))
            {
                MPIService.BroadcastMessage($"ERR_ICR_01 for : {i}");
                MPIService.FinishMPI(); 
            }

            var makePayment = await _repo.InsertPayment(new() { });

            if (makePayment is not null)
            {
                MPIService.BroadcastMessage($"ERR_IPY_01 for : {i}");
                MPIService.FinishMPI();  
            }

            var logMpiAction = await _repo.LogMPIProcess(new() { });
            
            if (!logMpiAction)
            {
                MPIService.BroadcastMessage($"ERR_MPI_01 for : {i}");
                MPIService.FinishMPI(); 
            } 
            
            MPIService.SendData(1, makePayment.CoreReference);
        }
    }
}