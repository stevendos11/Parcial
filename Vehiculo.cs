using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1
{
    public abstract class Vehiculo
    {
        public string Marca { get; protected set; }
        public string Modelo { get; protected set; }
        public int AnioFabricacion { get; protected set; }
        public decimal PrecioPorDia { get; protected set; }
        public EstadoVehiculo Estado { get; set; }

        protected Vehiculo(string marca, string modelo, int anioFabricacion, decimal precioPorDia)
        {
            Marca = marca;
            Modelo = modelo;
            AnioFabricacion = anioFabricacion;
            PrecioPorDia = precioPorDia;
            Estado = EstadoVehiculo.Disponible;
        }

        public abstract void MostrarDetalles();
    }
}
