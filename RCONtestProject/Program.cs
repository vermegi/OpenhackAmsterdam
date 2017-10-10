using MinecraftServerRCON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCONtestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rcon = RCONClient.INSTANCE)
            {
                rcon.setupStream("openhackteam04.westeurope.cloudapp.azure.com", password: "cheesesteakjimmys");
                var answer = rcon.sendMessage(RCONMessageType.Command, "list");
                Console.WriteLine(answer.RemoveColorCodes());
                var thestring = answer.RemoveColorCodes();
                var parts = thestring.Split(new char[] { ' ', '/' });
                var playerdone = false;
                foreach (var part in parts)
                {
                    long result;
                    var succeeded = Int64.TryParse(part, out result);
                    if (succeeded)
                    {
                        if (!playerdone)
                        {
                            Console.WriteLine("players: " + result);
                            playerdone = true;
                        }
                        else
                            Console.WriteLine("capacity: " + result);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
