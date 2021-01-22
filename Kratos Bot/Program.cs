using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kratos;
using Kratos_Bot;


namespace Kratos_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Bot...");
            Thread.Sleep(150);
            Start();
            Thread.Sleep(200);
            Console.Clear();
            Console.WriteLine("Bot is ONLINE!");
                

        }
        static void Start()
        {
            Bot prog = new Bot();
            prog.MainAsync().GetAwaiter().GetResult();
        }
       
    }
}
