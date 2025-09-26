using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLProyectoOpiniones
{
    // --- Clases para leer los archivos CSV originales ---

    public class ClienteCsv
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }

    public class FuenteCsv
    {
        public string IdFuente { get; set; }
        public string TipoFuente { get; set; }
        public DateTime FechaCarga { get; set; }
    }

    public class ProductoCsv
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
    }

    public class ComentarioSocialCsv
    {
        public string IdComment { get; set; }
        public string IdCliente { get; set; }
        public string IdProducto { get; set; }
        public string Fuente { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
    }

    public class EncuestaCsv
    {
        public int IdOpinion { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int PuntajeSatisfaccion { get; set; }
    }

    public class ResenaWebCsv
    {
        public string IdReview { get; set; }
        public string IdCliente { get; set; }
        public string IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int Rating { get; set; }
    }

    // --- Clases para las tablas finales de la Base de Datos ---

    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }

    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
    }

    public class Fuente
    {
        public string IdFuente { get; set; }
        public string TipoFuente { get; set; }
        public DateTime FechaCarga { get; set; }
    }

    public class Opinion
    {
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public string IdFuente { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
    }
}