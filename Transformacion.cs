using System.Collections.Generic;
using System.Linq;

namespace ETLProyectoOpiniones
{
    public class Transformacion
    {
        public static (List<Cliente>, List<Producto>, List<Fuente>, List<Opinion>) TransformarDatos(
            List<ClienteCsv> clientesCsv,
            List<FuenteCsv> fuentesCsv,
            List<ProductoCsv> productosCsv,
            List<ComentarioSocialCsv> comentariosCsv,
            List<EncuestaCsv> encuestasCsv,
            List<ResenaWebCsv> resenasCsv)
        {
            var clientesLimpios = clientesCsv
                .Select(c => new Cliente { IdCliente = c.IdCliente, Nombre = c.Nombre, Email = c.Email })
                .ToList();

            var productosLimpios = productosCsv
                .Select(p => new Producto { IdProducto = p.IdProducto, Nombre = p.Nombre, Categoria = p.Categoria })
                .ToList();

            var fuentesLimpias = fuentesCsv
                .Select(f => new Fuente { IdFuente = f.IdFuente, TipoFuente = f.TipoFuente, FechaCarga = f.FechaCarga })
                .ToList();

            var opinionesUnificadas = new List<Opinion>();

            foreach (var c in comentariosCsv)
            {
                opinionesUnificadas.Add(new Opinion
                {
                    IdCliente = LimpiarIdCliente(c.IdCliente),
                    IdProducto = LimpiarIdProducto(c.IdProducto),
                    IdFuente = ObtenerIdFuente(c.Fuente, fuentesLimpias),
                    Fecha = c.Fecha,
                    Comentario = c.Comentario,
                    Calificacion = 0
                });
            }

            foreach (var e in encuestasCsv)
            {
                opinionesUnificadas.Add(new Opinion
                {
                    IdCliente = e.IdCliente,
                    IdProducto = e.IdProducto,
                    IdFuente = ObtenerIdFuente("Encuesta", fuentesLimpias),
                    Fecha = e.Fecha,
                    Comentario = e.Comentario,
                    Calificacion = e.PuntajeSatisfaccion
                });
            }

            foreach (var r in resenasCsv)
            {
                opinionesUnificadas.Add(new Opinion
                {
                    IdCliente = LimpiarIdCliente(r.IdCliente),
                    IdProducto = LimpiarIdProducto(r.IdProducto),
                    IdFuente = ObtenerIdFuente("Web", fuentesLimpias),
                    Fecha = r.Fecha,
                    Comentario = r.Comentario,
                    Calificacion = r.Rating
                });
            }

            return (clientesLimpios, productosLimpios, fuentesLimpias, opinionesUnificadas);
        }

        private static int LimpiarIdCliente(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var numero = new string(id.Where(char.IsDigit).ToArray());
            int.TryParse(numero, out int idNumerico);
            return idNumerico;
        }

        private static int LimpiarIdProducto(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var numero = new string(id.Where(char.IsDigit).ToArray());
            int.TryParse(numero, out int idNumerico);
            return idNumerico;
        }

        private static string ObtenerIdFuente(string tipo, List<Fuente> fuentes)
        {
            if (tipo.Contains("Instagram") || tipo.Contains("Twitter") || tipo.Contains("Facebook"))
            {
                tipo = "Red Social";
            }
            return fuentes.FirstOrDefault(f => f.TipoFuente.Equals(tipo, StringComparison.OrdinalIgnoreCase))?.IdFuente ?? "F000";
        }
    }
}