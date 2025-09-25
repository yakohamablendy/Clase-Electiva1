using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLProyectoOpiniones
{
    public class Cliente
    {
        public int id_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string email { get; set; }
    }

    public class Fuente
    {
        public int id_fuente { get; set; }
        public string nombre_fuente { get; set; }
    }

    public class Producto
    {
        public int id_producto { get; set; }
        public string nombre_producto { get; set; }
        public string categoria { get; set; }
    }

    public class Opinion
    {
        public int id_opinion { get; set; }
        public int id_cliente { get; set; }
        public int id_producto { get; set; }
        public int id_fuente { get; set; }
        public DateTime fecha { get; set; }
        public string texto_opinion { get; set; }
        public int calificacion { get; set; }
    }
}
