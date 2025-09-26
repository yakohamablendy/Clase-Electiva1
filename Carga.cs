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

                foreach (var f in fuentes)
                {
                    new SqlCommand($"INSERT INTO Fuentes (IdFuente, TipoFuente, FechaCarga) VALUES ('{f.IdFuente}', '{f.TipoFuente}', '{f.FechaCarga:yyyy-MM-dd}');", connection).ExecuteNonQuery();
                }
                Console.WriteLine($"{fuentes.Count} Fuentes cargadas.");

                foreach (var c in clientes)
                {
                    new SqlCommand($"INSERT INTO Clientes (IdCliente, Nombre, Email) VALUES ({c.IdCliente}, '{c.Nombre.Replace("'", "''")}', '{c.Email}');", connection).ExecuteNonQuery();
                }
                Console.WriteLine($"{clientes.Count} Clientes cargados.");

                foreach (var p in productos)
                {
                    new SqlCommand($"INSERT INTO Productos (IdProducto, Nombre, Categoria) VALUES ({p.IdProducto}, '{p.Nombre.Replace("'", "''")}', '{p.Categoria.Replace("'", "''")}');", connection).ExecuteNonQuery();
                }
                Console.WriteLine($"{productos.Count} Productos cargados.");

                foreach (var o in opiniones)
                {
                    string idClienteSql = (o.IdCliente == 0) ? "NULL" : o.IdCliente.ToString();
                    string comentarioSql = o.Comentario?.Replace("'", "''") ?? "";

                    new SqlCommand($"INSERT INTO Opiniones (IdCliente, IdProducto, IdFuente, Fecha, Comentario, Calificacion) VALUES ({idClienteSql}, {o.IdProducto}, '{o.IdFuente}', '{o.Fecha:yyyy-MM-dd}', '{comentarioSql}', {o.Calificacion});", connection).ExecuteNonQuery();
                }
                Console.WriteLine($"{opiniones.Count} Opiniones cargadas.");
            }
        }
    }
}