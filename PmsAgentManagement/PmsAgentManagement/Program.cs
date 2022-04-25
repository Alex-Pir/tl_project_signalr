using System;
using System.Threading.Tasks;

namespace PmsAgentManagement
{
    public class Program
    {
        static void Main(string[] args) =>
            Console.WriteLine("tut");

        /*static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHostedService<Worker>()
                        .AddScoped<IMessageWriter, MessageWriter>());*/
    }
}