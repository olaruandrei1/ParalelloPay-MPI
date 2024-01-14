using ParalelloPay_MPI.Domain;
using ParalelloPay_MPI.Domain.Responses;

namespace ParalelloPay_MPI.Application.Contracts;

public interface IPaymentRepository
{
   Task<bool> CheckBalance(CheckBalanceRequest request);
   Task<bool> CheckCreditorValidation(CheckCreditorValidationRequest request);
   Task<InsertPaymentResponse> InsertPayment(InsertPaymentRequest request);
   Task<bool> UpdatePayment(UpdatePaymentRequest request);
   Task<bool> LogMPIProcess(LogMPIProcessRequest request);
}