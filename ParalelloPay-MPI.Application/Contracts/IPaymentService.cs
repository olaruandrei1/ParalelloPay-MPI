using ParalelloPay_MPI.Domain;

namespace ParalelloPay_MPI.Application.Contracts;

public interface IPaymentService
{
    Task ProcessPayments(List<PaymentRequest> requests);
}