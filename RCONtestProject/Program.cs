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
            }

            Console.ReadLine();
        }
    }
}
