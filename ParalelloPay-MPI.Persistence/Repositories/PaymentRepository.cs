using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using ParalelloPay_MPI.Application.Contracts;
using ParalelloPay_MPI.Domain;
using ParalelloPay_MPI.Domain.Responses;
namespace ParalelloPay_MPI.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private MySqlConnectionStringBuilder _sqlConnectionBuidler;

    private string CHECK_BALANCE = @"";
    
    public PaymentRepository()
    {
        _sqlConnectionBuidler = new MySqlConnectionStringBuilder
        {
            Server = "",
            Database = "",
            UserID = "",
            Password = "",
        };
    }
    
    public async Task<bool> CheckBalance(CheckBalanceRequest request)
    {
        using var conn = new MySqlConnection(_sqlConnectionBuidler.ConnectionString);

        DynamicParameters param = new();
        
        param.Add((""));
        
        await conn.OpenAsync();

        return await conn.QueryFirstOrDefaultAsync<bool>(sql: CHECK_BALANCE, param: param, commandType: CommandType.Text);
    }

    public async Task<bool> CheckCreditorValidation(CheckCreditorValidationRequest request)
    {
        using var conn = new MySqlConnection(_sqlConnectionBuidler.ConnectionString);

        DynamicParameters param = new();
        
        param.Add((""));
        
        await conn.OpenAsync();

        return await conn.QueryFirstOrDefaultAsync<bool>(sql: CHECK_BALANCE, param: param, commandType: CommandType.Text);
    }

    public async Task<InsertPaymentResponse> InsertPayment(InsertPaymentRequest request)
    {
        using var conn = new MySqlConnection(_sqlConnectionBuidler.ConnectionString);

        DynamicParameters param = new();
        
        param.Add((""));
        
        await conn.OpenAsync();

        var execute = await conn.QueryFirstOrDefaultAsync<bool>(sql: CHECK_BALANCE, param: param, commandType: CommandType.Text);

        return new() { };
    }

    public Task<bool> UpdatePayment(UpdatePaymentRequest request) => Task.FromResult(true);

    public Task<bool> LogMPIProcess(LogMPIProcessRequest request) => Task.FromResult(true);
}