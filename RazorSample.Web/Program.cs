using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RazorSample.Web
{
    public sealed class Program
    {
        private readonly string[] _args;

        public Program(string[] args)
        {
            _args = args;
        }

        public static void Main(string[] args)
        {
            new Program(args).RunAsync()
                             .GetAwaiter()
                             .GetResult();
        }

        public async Task RunAsync()
        {
            await WebHost.CreateDefaultBuilder(_args)
                         .UseStartup<Startup>()
                         .Build()
                         .RunAsync();
        }
    }
}
