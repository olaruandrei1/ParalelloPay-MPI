using System.IO.Pipes;
using System.Text;

namespace ParalelloPay_MPI.Application.Implementation;

public class MPIService
{
    private static readonly string _pipeName = "CustomPipe";

    private static readonly NamedPipeServerStream _pipeServer;

    static MPIService() => _pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.InOut,
        NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

    public static void InitMPI(string[] args) => _pipeServer.WaitForConnection();

    public static void FinishMPI()
    {
        _pipeServer.Close();
        _pipeServer.Dispose();
    }

    public static int GetRank() => System.Diagnostics.Process.GetCurrentProcess().SessionId;

    public static int GetSize() => System.Diagnostics.Process
        .GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length;

    public static void BroadcastMessage(string message)
    {
        foreach (var process in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process
                     .GetCurrentProcess().ProcessName))
            if (process.SessionId != System.Diagnostics.Process.GetCurrentProcess().SessionId)
                SendMessage(process, message);
    }

    public static void SendData(int targetRank, object data) => SendMessage(
        System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName)
            .FirstOrDefault(p => p.SessionId == targetRank), data.ToString());

    public static object ReceiveData(int sourceRank)
    {
        var buffer = new byte[1024];
        var bytesRead = _pipeServer.Read(buffer, 0, buffer.Length);

        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }

    private static void SendMessage(System.Diagnostics.Process process, string message)
    {
        using var pipeWriter = new StreamWriter($"\\\\.\\pipe\\{_pipeName}_{process.SessionId}");
        
        pipeWriter.WriteLine(message);
    }
}