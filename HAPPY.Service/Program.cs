using HAPPYWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;

namespace HAPPY.Service
{
    static class Program
    {
        static void Main(string[] args)
        {

            ServiceHost hostDPManager = new ServiceHost(typeof(DepartamentoPessoal));
            hostDPManager.Open();

            ServiceHost hostPubSubManager = new ServiceHost(typeof(PubSubManager));
            hostPubSubManager.Open();

            PrintServiceInfo(hostDPManager);
            PrintServiceInfo(hostPubSubManager);

            Console.WriteLine("Serviço Iniciado. Press [Enter] para finalizar.");
            Console.ReadLine();

            hostDPManager.Close();
            hostPubSubManager.Close();
        
        }

        static void PrintServiceInfo(ServiceHost host)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("{0}\n Servidor conectado com sucesso!\n",
                host.Description.ServiceType);

            foreach (ServiceEndpoint se in host.Description.Endpoints)
                Console.WriteLine("Endereço: " + se.Address + "\n\n===================================================\n");

            Console.ResetColor();
        }
    }
}
