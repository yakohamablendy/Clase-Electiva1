using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace ETLProyectoOpiniones
{
    public class Extraccion
    {
        public static (List<Cliente>, List<Fuente>, List<Producto>, List<Opinion>) ExtraerDatos()
        {
            List<Cliente> clientes;
            List<Fuente> fuentes;
            List<Producto> productos;
            List<Opinion> opiniones;

            using (var reader = new StreamReader("datos_csv/clientes.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                clientes = csv.GetRecords<Cliente>().ToList();
            }

            using (var reader = new StreamReader("datos_csv/fuentes.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                fuentes = csv.GetRecords<Fuente>().ToList();
            }

            using (var reader = new StreamReader("datos_csv/productos.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                productos = csv.GetRecords<Producto>().ToList();
            }

            using (var reader = new StreamReader("datos_csv/opiniones.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                opiniones = csv.GetRecords<Opinion>().ToList();
            }

            return (clientes, fuentes, productos, opiniones);
        }
    }
}