using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1
{
    public class Reserva
    {
        public Cliente Cliente { get; private set; }
        public Vehiculo Vehiculo { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public int Duracion { get; private set; } 

        public Reserva(Cliente cliente, Vehiculo vehiculo, DateTime fechaInicio, int duracion)
        {
            Cliente = cliente;
            Vehiculo = vehiculo;
            FechaInicio = fechaInicio;
            Duracion = duracion;
            vehiculo.Estado = EstadoVehiculo.Alquilado; 
        }
    }
}