using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WebhookChat
{
    internal class Program
    {
        static async Task Chat()
        {
            var httpClient = new HttpClient();
            string wU = ""; // Set your webhook here
            while (true)
            {
                try
                {
                    Console.Write("$> ");
                    string message = Console.ReadLine();
                    if (message.ToString().ToLower() == "close")
                    {
                        break;
                    }
                    else if (message.ToLower() == "kill")
                    {
                        await httpClient.DeleteAsync(wU);
                        break;
                    }
                    var content = new StringContent("{\"content\": \"" + message + "\"}", Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(wU, content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            Environment.Exit(0);
        }

        static async Task Main(string[] args)
        {
            Console.Title = "Webhook live Chat";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(new string('-', 15) + " Webhook live Chat " + new string('-', 15));
            Console.ResetColor();
            await Chat();
        }
    }
}
