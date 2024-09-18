using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1
{
    public class SistemaAlquiler
    {
        public List<Vehiculo> Vehiculos { get; private set; } = new List<Vehiculo>();
        public List<Reserva> Reservas { get; private set; } = new List<Reserva>();

        public void AgregarVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo == null)
            {
                throw new ArgumentNullException(nameof(vehiculo), "El vehículo no puede ser nulo.");
            }
            Vehiculos.Add(vehiculo);
            Console.WriteLine("Vehículo agregado correctamente.");
        }

        public void EliminarVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo.Estado != EstadoVehiculo.Disponible)
            {
                throw new InvalidOperationException("No se puede eliminar un vehículo que no está disponible.");
            }
            if (!Vehiculos.Remove(vehiculo))
            {
                throw new InvalidOperationException("El vehículo especificado no se encontró.");
            }
            Console.WriteLine("Vehículo eliminado correctamente.");
        }

        public void ReservarVehiculo(Cliente cliente, Vehiculo vehiculo, DateTime fechaInicio, int duracion)
        {
            if (vehiculo.Estado != EstadoVehiculo.Disponible)
            {
                throw new InvalidOperationException("El vehículo no está disponible para reservas.");
            }
            if (fechaInicio < DateTime.Now)
            {
                throw new ArgumentException("La fecha de inicio de la reserva debe ser en el futuro.");
            }
            vehiculo.Estado = EstadoVehiculo.Alquilado;
            Reservas.Add(new Reserva(cliente, vehiculo, fechaInicio, duracion));
            Console.WriteLine("Reserva realizada exitosamente.");
        }

        public void DevolverVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo.Estado != EstadoVehiculo.Alquilado)
            {
                throw new InvalidOperationException("El vehículo no está actualmente alquilado.");
            }
            vehiculo.Estado = EstadoVehiculo.Disponible;
            Console.WriteLine("Vehículo devuelto correctamente.");
        }

        public void MostrarHistorialReservas()
        {
            foreach (var reserva in Reservas)
            {
                Console.WriteLine($"Cliente: {reserva.Cliente.Nombre}, Vehículo: {reserva.Vehiculo.Marca} {reserva.Vehiculo.Modelo}, Fecha de inicio: {reserva.FechaInicio.ToShortDateString()}, Duración: {reserva.Duracion} días");
            }
        }
        public void MostrarVehiculos()
        {
            if (Vehiculos.Count == 0)
            {
                Console.WriteLine("No hay vehículos registrados en el sistema.");
                return;
            }

            Console.WriteLine("Listado de todos los vehículos:");
            for (int i = 0; i < Vehiculos.Count; i++)
            {
                Vehiculo vehiculo = Vehiculos[i];
                Console.WriteLine($"{i + 1}. {vehiculo.Marca} {vehiculo.Modelo}, Año: {vehiculo.AnioFabricacion}, Precio por día: {vehiculo.PrecioPorDia:C}, Estado: {vehiculo.Estado}");
            }
        }
    }
}