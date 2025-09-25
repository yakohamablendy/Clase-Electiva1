using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ETLProyectoOpiniones
{
    public class Carga
    {
        public static void CargarDatos(string connectionString, List<Cliente> clientes, List<Fuente> fuentes, List<Producto> productos, List<Opinion> opiniones)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM Opiniones; DELETE FROM Clientes; DELETE FROM Fuentes; DELETE FROM Productos;";
                command.ExecuteNonQuery();

                foreach (var fuente in fuentes)
                {
                    command.CommandText = $"INSERT INTO Fuentes (id_fuente, nombre_fuente) VALUES ({fuente.id_fuente}, '{fuente.nombre_fuente}');";
                    command.ExecuteNonQuery();
                }

                foreach (var cliente in clientes)
                {
                    command.CommandText = $"INSERT INTO Clientes (id_cliente, nombre_cliente, email) VALUES ({cliente.id_cliente}, '{cliente.nombre_cliente}', '{cliente.email}');";
                    command.ExecuteNonQuery();
                }

                foreach (var producto in productos)
                {
                    command.CommandText = $"INSERT INTO Productos (id_producto, nombre_producto, categoria) VALUES ({producto.id_producto}, '{producto.nombre_producto}', '{producto.categoria}');";
                    command.ExecuteNonQuery();
                }

                foreach (var opinion in opiniones)
                {
                    command.CommandText = $"INSERT INTO Opiniones (id_opinion, id_cliente, id_producto, id_fuente, fecha, texto_opinion, calificacion) VALUES ({opinion.id_opinion}, {opinion.id_cliente}, {opinion.id_producto}, {opinion.id_fuente}, '{opinion.fecha:yyyy-MM-dd}', '{opinion.texto_opinion}', {opinion.calificacion});";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}