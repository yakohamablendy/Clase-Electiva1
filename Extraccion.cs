using System.Globalization;
using CsvHelper;

namespace ETLProyectoOpiniones
{
    public class Extraccion
    {
        public static (List<ClienteCsv>, List<FuenteCsv>, List<ProductoCsv>, List<ComentarioSocialCsv>, List<EncuestaCsv>, List<ResenaWebCsv>) ExtraerDatos()
        {
            var clientes = LeerCsv<ClienteCsv>("datos_csv/clients.csv");
            var fuentes = LeerCsv<FuenteCsv>("datos_csv/fuente_datos.csv");
            var productos = LeerCsv<ProductoCsv>("datos_csv/products.csv");
            var comentarios = LeerCsv<ComentarioSocialCsv>("datos_csv/social_comments.csv");
            var encuestas = LeerCsv<EncuestaCsv>("datos_csv/surveys_part1.csv");
            var resenas = LeerCsv<ResenaWebCsv>("datos_csv/web_reviews.csv");

            return (clientes, fuentes, productos, comentarios, encuestas, resenas);
        }

        private static List<T> LeerCsv<T>(string rutaArchivo)
        {
            using (var reader = new StreamReader(rutaArchivo))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
            }
        }
    }
}