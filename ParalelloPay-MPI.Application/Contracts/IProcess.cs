namespace ParalelloPay_MPI.Application.Contracts;

public interface IProcess
{
   Task StartProcess(string[] requests);
}