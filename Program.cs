using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ETLProyectoOpiniones
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--- INICIO DEL PROCESO ETL ---");

            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = config.GetConnectionString("DefaultConnection");

                var (clientes, fuentes, productos, opiniones) = Extraccion.ExtraerDatos();
                Console.WriteLine("FASE 1: EXTRACCIÓN COMPLETADA.");

                var clientesLimpios = Transformacion.LimpiarClientes(clientes);
                Console.WriteLine("FASE 2: TRANSFORMACIÓN COMPLETADA.");

                Carga.CargarDatos(connectionString, clientesLimpios, fuentes, productos, opiniones);
                Console.WriteLine("FASE 3: CARGA COMPLETADA.");

                Console.WriteLine("\n--- PROCESO ETL FINALIZADO CON ÉXITO ---");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n--- ERROR EN EL PROCESO ---");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPresiona una tecla para salir.");
            Console.ReadKey();
        }
    }
}