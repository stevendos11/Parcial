using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1
{
    public class Cliente
    {
        public string Nombre { get; private set; }
        public string DocumentoIdentidad { get; private set; }

        public Cliente(string nombre, string documentoIdentidad)
        {
            Nombre = nombre;
            DocumentoIdentidad = documentoIdentidad;
        }
    }
}