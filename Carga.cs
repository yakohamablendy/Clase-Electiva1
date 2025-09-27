using System.Collections.Generic; 
using Microsoft.Data.SqlClient;

namespace ETLProyectoOpiniones
{
    public class Carga
    {
        public static void CargarDatos(string connectionString, List<Cliente> clientes, List<Producto> productos, List<Fuente> fuentes, List<Opinion> opiniones)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                new SqlCommand("DELETE FROM Opiniones; DELETE FROM Clientes; DELETE FROM Productos; DELETE FROM Fuentes;", connection).ExecuteNonQuery();

                // Cargar Fuentes, Clientes y Productos
                foreach (var f in fuentes) { new SqlCommand($"INSERT INTO Fuentes (IdFuente, TipoFuente, FechaCarga) VALUES ('{f.IdFuente}', '{f.TipoFuente}', '{f.FechaCarga:yyyy-MM-dd}');", connection).ExecuteNonQuery(); }
                Console.WriteLine($"{fuentes.Count} Fuentes cargadas.");

                foreach (var c in clientes) { new SqlCommand($"INSERT INTO Clientes (IdCliente, Nombre, Email) VALUES ({c.IdCliente}, '{c.Nombre.Replace("'", "''")}', '{c.Email}');", connection).ExecuteNonQuery(); }
                Console.WriteLine($"{clientes.Count} Clientes cargados.");

                foreach (var p in productos) { new SqlCommand($"INSERT INTO Productos (IdProducto, Nombre, Categoria) VALUES ({p.IdProducto}, '{p.Nombre.Replace("'", "''")}', '{p.Categoria.Replace("'", "''")}');", connection).ExecuteNonQuery(); }
                Console.WriteLine($"{productos.Count} Productos cargados.");

                // --- LÓGICA DE CARGA DE OPINIONES CON DOBLE VALIDACIÓN ---

                // 1. Crear listas de IDs válidos para buscar rápido.
                var idsDeClientesValidos = new HashSet<int>(clientes.Select(c => c.IdCliente));
                var idsDeProductosValidos = new HashSet<int>(productos.Select(p => p.IdProducto));

                int opinionesCargadas = 0;
                int opinionesRechazadas = 0;

                // 2. Recorrer las opiniones y validar cada una.
                foreach (var o in opiniones)
                {
                    // VALIDACIÓN 1: El producto debe existir.
                    if (o.IdProducto == 0 || !idsDeProductosValidos.Contains(o.IdProducto))
                    {
                        opinionesRechazadas++;
                        continue; // Si el producto no es válido, salta a la siguiente opinión.
                    }

                    // VALIDACIÓN 2: El cliente puede ser desconocido (NULL).
                    string idClienteSql;
                    if (o.IdCliente == 0 || !idsDeClientesValidos.Contains(o.IdCliente))
                    {
                        idClienteSql = "NULL";
                    }
                    else
                    {
                        idClienteSql = o.IdCliente.ToString();
                    }

                    string comentarioSql = o.Comentario?.Replace("'", "''") ?? "";

                    new SqlCommand($"INSERT INTO Opiniones (IdCliente, IdProducto, IdFuente, Fecha, Comentario, Calificacion) VALUES ({idClienteSql}, {o.IdProducto}, '{o.IdFuente}', '{o.Fecha:yyyy-MM-dd}', '{comentarioSql}', {o.Calificacion});", connection).ExecuteNonQuery();
                    opinionesCargadas++;
                }

                Console.WriteLine($"{opinionesCargadas} Opiniones cargadas.");
                Console.WriteLine($"{opinionesRechazadas} Opiniones rechazadas por IdProducto inválido.");
            }
        }
    }
}