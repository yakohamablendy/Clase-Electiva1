using System.Collections.Generic;
using System.Linq;

namespace ETLProyectoOpiniones
{
    public class Transformacion
    {
        public static List<Cliente> LimpiarClientes(List<Cliente> clientes)
        {
            var clientesLimpios = clientes
                .GroupBy(cliente => cliente.email)
                .Select(grupo => grupo.First())
                .ToList();

            return clientesLimpios;
        }
    }
}