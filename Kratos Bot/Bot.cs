using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using Leaf.xNet;

namespace Kratos
{
    internal class Bot
    {
        public static DiscordSocketClient _client;
       


        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.Ready += Activity;
            _client.Ready += Write;
            var token = "ODAwNTgyMjc4NzE1NDA4NDA0.YAUOWQ.EkFAJUo0Z6IJWRzVaZB2_2W9s-c";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
           
            

        }
        
        private static Task Write()
        {
            Console.Clear();
            Console.WriteLine("Bot ONLINE!");
            return Task.CompletedTask;
        }
        private static Task CommandHandler(SocketMessage messagee)
        {
            
            string command = "";
            int lengthOfCommand = -1;

            bool _isAllowed = false;

            var client = new WebClient();
            WebClient webClient = new WebClient();

          
         

            if (!messagee.Content.StartsWith(";"))
                return Task.CompletedTask;

            if (messagee.Author.IsBot)
                return Task.CompletedTask;

            if (messagee.Content.Contains(" "))
                lengthOfCommand = messagee.Content.IndexOf(' ');
            else
                lengthOfCommand = messagee.Content.Length;

            command = messagee.Content.Substring(1, lengthOfCommand - 1).ToLower();

            if (command.Equals("ping"))
            {
                string msg = messagee.Content;
                string ipp = Regex.Match(msg, ";ping '(.*?)'").Groups[1].Value;
                if (ipp == "")
                {
                    messagee.Channel.SendMessageAsync("Please input an IP! (Remember its between ' ')");
                }
                else
                {
                    Ping p = new Ping();
                    PingReply r;
                    r = p.Send(ipp);
                    if (r.Status == IPStatus.Success)
                    {
                        string response = "Ping to " + ipp.ToString() + " = Online!"
                           + " Response delay = " + r.RoundtripTime.ToString() + " ms" + "\n";

                        messagee.Channel.SendMessageAsync(response);
                        return Task.CompletedTask;
                    }
                    else
                    {
                        messagee.Channel.SendMessageAsync("Ping to " + ipp.ToString() + " = Timed Out");
                    }
                }
                
            }
            if (command.Equals("lookup"))
            {
                string msg = messagee.Content;
                string ipp = Regex.Match(msg, ";lookup '(.*?)'").Groups[1].Value;
                if (ipp == "")
                {
                    messagee.Channel.SendMessageAsync("Please input an IP! (Remember its between ' ')");
                }
                else
                {
                    Uri ipapi = new Uri($"http://ip-api.com/json/{ipp}");
                    using (HttpRequest httpRequest = new HttpRequest())
                    {
                        string uri = httpRequest.Get(ipapi).ToString();
                        string country = Regex.Match(uri, "\"country\":\"(.*?)\"").Groups[1].Value;
                        string regionname = Regex.Match(uri, "\"regionName\":\"(.*?)\"").Groups[1].Value;
                        string city = Regex.Match(uri, "\"city\":\"(.*?)\"").Groups[1].Value;
                        string lat = Regex.Match(uri, "\"lat\":(.*?),").Groups[1].Value;
                        string lon = Regex.Match(uri, "\"lon\":(.*?),").Groups[1].Value;
                        string isp = Regex.Match(uri, "\"isp\":\"(.*?)\"").Groups[1].Value;
                        EmbedBuilder Embed = new EmbedBuilder();
                        Embed.WithTitle($"{ipp} Data:");
                        Embed.AddField($"Country: ", $"{country}", true);
                        Embed.AddField($"Region: ", $"{regionname}", true);
                        Embed.AddField($"City: ", $"{city}", true);
                        Embed.AddField($"Coordinates: ", $"{lat} / {lon}", true);
                        Embed.AddField($"ISP: ", $"{isp}", true);
                        Embed.WithImageUrl("https://cdn.discordapp.com/attachments/797673359373631568/800598615612588042/unknown.png");
                        
                        Embed.WithColor(Color.Purple);
                        messagee.Channel.SendMessageAsync("", false, Embed.Build());
                        return Task.CompletedTask;

                    }
                  
                }
            }
            if (command.Equals("website"))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithTitle("Click Here");
                Embed.WithUrl("https://jurypastes.github.io/KratosBooter/");
                messagee.Channel.SendMessageAsync("", false, Embed.Build());
            }
            if (command.Equals("help"))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithTitle("Kratos Commands");
                Embed.AddField("**ping**", "Ping an ip");
                Embed.AddField("**lookup**", "Lookup an ip");
                Embed.AddField("**website**", "Display website");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/797673359373631568/800598615612588042/unknown.png");
                messagee.Channel.SendMessageAsync("", false, Embed.Build());
            }
           
            
           

            return Task.CompletedTask;
        }
        
        public static Task Activity()
        {
            _client.SetActivityAsync(new Game(";help", ActivityType.Playing));
            return Task.CompletedTask;
        }

        internal static void AttackSent()
        {
            throw new NotImplementedException();
        }
    }
}