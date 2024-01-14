namespace ParalelloPay_MPI.Domain;

public class PaymentRequest
{
    public string DebitorAccount { get; set; } = default!;
    public string CreditorAccount { get; set; } = default!;
    public string DebitorName { get; set; } = default!;
    public string CreditorName { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string DateToPay { get; set; } = default!;
    public string Explanation { get; set; } = default!;
    public string Ccy { get; set; } = default!;
    public string TransferAnyway { get; set; } = default!;   
}