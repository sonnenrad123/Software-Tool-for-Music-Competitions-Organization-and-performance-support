using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2
{
    class Program
    {
        private static ServiceHost host;

        static void Main(string[] args)
        {

            Console.WriteLine("Server is starting . . .");
            host = new ServiceHost(typeof(Server));
            host.Open();
            Console.WriteLine("Server started!");
            Console.ReadLine();
            Console.WriteLine("Server is closing . . .");
            host.Close();
            Console.WriteLine("Server closed!");
            Console.ReadLine();



            Console.Read();
        }
    }
}
