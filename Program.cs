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


        static void Comunicacion(ClienteCom cliente)
        {
            bool finalizar = false;

            while (!finalizar)
            {
                string mensaje = cliente.Leer();
                if (mensaje != null)
                {
                    Console.WriteLine("Cliente : "+ mensaje);
                    if (mensaje.ToLower() == "chao")
                    {
                        cliente.Escribir("Has sido desconectado");
                        finalizar = true;
                    }
                    else
                    {
                        Console.Write("Ingrese respuesta: ");
                        mensaje = Console.ReadLine().Trim();
                        cliente.Escribir(mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            cliente.Escribir("Has sido desconectado");
                            finalizar = true;
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
            Console.WriteLine("Iniciando el servidor en el puerto "+puerto);
            ServerSocket serverSocket = new ServerSocket(puerto);


            if (serverSocket.Iniciar())
            {

                while (true)
                {

                    Console.WriteLine("Esperando clientes...");
                    Socket socket = serverSocket.ObtenerCliente();
                    Console.WriteLine("Cliente conectado correctamente");
                    ClienteCom cliente = new ClienteCom(socket);
                    Comunicacion(cliente);

                }
            }
            else
            {

                Console.WriteLine("Error, no se puede acceder al puerto "+puerto);

            }

        }

    }
}
