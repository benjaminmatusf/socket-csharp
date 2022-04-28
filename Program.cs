using Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketBM
{
    internal class Program
    {
        //PROBANDO 
        //Inicia una conversacion con un cliente hasta que algun extremo
        //de la comunicacion se despida, con un chaooo

        //en mi caso hare un metodo generar comunicacion, osea toda comunicacion del cliente pasara por el metodo.
        static void GenerarComunicacion(ClienteCom cliente)
        {
            bool terminar = false;
            while (!terminar)
            {
                string mensaje = cliente.Leer();
                if (mensaje != null)
                {
                    Console.WriteLine("Cliente : {0}", mensaje);
                    if (mensaje.ToLower() == "chao")
                    {
                        cliente.Escribir("Has sido desconectado");
                        terminar = true;
                    }
                    else
                    {
                        Console.Write("Ingrese respuesta: ");
                        mensaje = Console.ReadLine().Trim();
                        cliente.Escribir(mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            cliente.Escribir("Has sido desconectado");
                            terminar = true;
                        }
                    }
                }
                else
                {
                    cliente.Escribir("Has sido desconectado");
                    cliente.Desconectar();
                }
            }
        }
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Levantando servidor en puerto {0}", puerto);
            ServerSocket serverSocket = new ServerSocket(puerto);

            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    //luego de iniciar el servidor queda esperando....
                    Console.WriteLine("Esperando clientes...");
                    Socket socket = serverSocket.ObtenerCliente();
                    Console.WriteLine("Cliente recibido");
                    // comunicarse con el cliente
                    ClienteCom cliente = new ClienteCom(socket);
                    GenerarComunicacion(cliente);
                }
            }
            else
            {
                Console.WriteLine("Error al tomar posesion del puerto{0}", puerto);
            }

        }

    }
}
