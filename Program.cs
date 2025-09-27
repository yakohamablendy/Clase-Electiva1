using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ETLProyectoOpiniones
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--- INICIANDO EL DEL PROCESO ETL CON DATOS REALES ---");
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var connectionString = config.GetConnectionString("DefaultConnection");

                var (clientesCsv, fuentesCsv, productosCsv, comentariosCsv, encuestasCsv, resenasCsv) = Extraccion.ExtraerDatos();
                Console.WriteLine("FASE 1: EXTRACCIÓN COMPLETADA.");

                var (clientesLimpios, productosLimpios, fuentesLimpias, opinionesUnificadas) = Transformacion.TransformarDatos(
                    clientesCsv, fuentesCsv, productosCsv, comentariosCsv, encuestasCsv, resenasCsv);
                Console.WriteLine("FASE 2: TRANSFORMACIÓN COMPLETADA.");

                Carga.CargarDatos(connectionString, clientesLimpios, productosLimpios, fuentesLimpias, opinionesUnificadas);
                Console.WriteLine("FASE 3: CARGA COMPLETADA.");

                Console.WriteLine("\n--- PROCESO FINALIZADO CON ÉXITO ---");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n--- ERROR EN EL PROCESO ---");
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("\nPresiona una tecla para salir.");
            Console.ReadKey();
        }
    }
}