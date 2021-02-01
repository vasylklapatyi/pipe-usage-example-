using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace CSProject
{

    public static class Program
    {
        private static NamedPipeServerStream _server;

        static void Main(string[] args)
        {
            Console.WriteLine("C# SIDE");
            var namedPipeServer = new NamedPipeServerStream("my-very-cool-pipe-example", PipeDirection.InOut, 1, PipeTransmissionMode.Byte);
            var streamReader = new StreamReader(namedPipeServer);
            namedPipeServer.WaitForConnection();

            var writer = new StreamWriter(namedPipeServer);
            writer.Write("Hello from c#");
            writer.Write((char)0);
            writer.Flush();
            namedPipeServer.WaitForPipeDrain();

            Console.WriteLine($"read from pipe client: {streamReader.ReadLine()}");
            namedPipeServer.Dispose();
        }
    }
}