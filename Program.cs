using Parcial1;
using System;
using System.Linq;
namespace Parcial1

{
    class Program
    {
        static void Main(string[] args)
        {
            // Inicialización del sistema de alquiler y algunos vehículos
            SistemaAlquiler sistema = new SistemaAlquiler();
            sistema.AgregarVehiculo(new Automovil("Toyota", "Corolla", 2020, 150m));
            sistema.AgregarVehiculo(new Motocicleta("Harley", "Davidson", 2019, 200m));
            sistema.AgregarVehiculo(new Camion("Ford", "F150", 2018, 300m));

            bool continuar = true;

            // Menú principal
            while (continuar)
            {
                MostrarMenu();
                int opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        sistema.MostrarVehiculos();
                        break;
                    case 2:
                        AgregarNuevoVehiculo(sistema);
                        break;
                    case 3:
                        ReservarVehiculoConAntelacion(sistema);
                        break;
                    case 4:
                        DevolverVehiculo(sistema);
                        break;
                    case 5:
                        EliminarVehiculo(sistema);
                        break;
                    case 6:
                        sistema.MostrarHistorialReservas();
                        break;
                    case 7:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }

            Console.WriteLine("Gracias por usar el sistema. Presione cualquier tecla para salir.");
            Console.ReadKey();
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\nSeleccione una opción:");
            Console.WriteLine("1. Mostrar todos los vehículos");
            Console.WriteLine("2. Agregar un nuevo vehículo");
            Console.WriteLine("3. Reservar un vehículo para una fecha futura");
            Console.WriteLine("4. Devolver un vehículo");
            Console.WriteLine("5. Eliminar un vehículo");
            Console.WriteLine("6. Mostrar historial de reservas");
            Console.WriteLine("7. Salir");
            Console.Write("Opción: ");
        }

        static int LeerOpcion()
        {
            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                Console.WriteLine("Por favor, introduzca un número válido.");
                return -1;
            }
            return opcion;
        }

        static void AgregarNuevoVehiculo(SistemaAlquiler sistema)
        {
            Console.WriteLine("Ingrese el tipo de vehículo (Automovil, Motocicleta, Camion):");
            string tipo = Console.ReadLine().ToLower();
            Console.WriteLine("Ingrese la marca del vehículo:");
            string marca = Console.ReadLine();
            Console.WriteLine("Ingrese el modelo del vehículo:");
            string modelo = Console.ReadLine();
            Console.WriteLine("Ingrese el año de fabricación:");

            if (!int.TryParse(Console.ReadLine(), out int anioFabricacion))
            {
                Console.WriteLine("Año de fabricación no válido.");
                return;
            }

            Console.WriteLine("Ingrese el precio por día:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal precioPorDia))
            {
                Console.WriteLine("Precio por día no válido.");
                return;
            }

            Vehiculo vehiculo = null;

            switch (tipo)
            {
                case "automovil":
                    vehiculo = new Automovil(marca, modelo, anioFabricacion, precioPorDia);
                    break;
                case "motocicleta":
                    vehiculo = new Motocicleta(marca, modelo, anioFabricacion, precioPorDia);
                    break;
                case "camion":
                    vehiculo = new Camion(marca, modelo, anioFabricacion, precioPorDia);
                    break;
                default:
                    Console.WriteLine("Tipo de vehículo no reconocido.");
                    return;
            }

            sistema.AgregarVehiculo(vehiculo);
            Console.WriteLine("Vehículo agregado exitosamente.");
        }

        static void ReservarVehiculoConAntelacion(SistemaAlquiler sistema)
        {
            Console.WriteLine("Seleccione el número del vehículo a reservar:");
            sistema.MostrarVehiculos();

            if (!int.TryParse(Console.ReadLine(), out int vehiculoIndex) || vehiculoIndex < 1 || vehiculoIndex > sistema.Vehiculos.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            Vehiculo vehiculo = sistema.Vehiculos[vehiculoIndex - 1];

            if (vehiculo.Estado != EstadoVehiculo.Disponible)
            {
                Console.WriteLine("Vehículo no disponible.");
                return;
            }

            Console.WriteLine("Ingrese el nombre del cliente:");
            string nombreCliente = Console.ReadLine();
            Console.WriteLine("Ingrese el documento de identidad del cliente:");
            string documentoIdentidad = Console.ReadLine();

            Console.WriteLine("Ingrese la fecha de inicio de la reserva (AAAA-MM-DD):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime fechaInicio))
            {
                Console.WriteLine("Formato de fecha inválido.");
                return;
            }

            Console.WriteLine("Ingrese el número de días de la reserva:");
            if (!int.TryParse(Console.ReadLine(), out int duracion) || duracion <= 0)
            {
                Console.WriteLine("Número de días no válido.");
                return;
            }

            Cliente cliente = new Cliente(nombreCliente, documentoIdentidad);
            sistema.ReservarVehiculo(cliente, vehiculo, fechaInicio, duracion);
            Console.WriteLine("Reserva realizada exitosamente.");
        }

        static void DevolverVehiculo(SistemaAlquiler sistema)
        {
            var alquilados = sistema.Vehiculos.Where(v => v.Estado == EstadoVehiculo.Alquilado).ToList();
            if (!alquilados.Any())
            {
                Console.WriteLine("No hay vehículos alquilados en este momento.");
                return;
            }

            Console.WriteLine("Seleccione el número del vehículo a devolver:");
            for (int i = 0; i < alquilados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {alquilados[i].Marca} {alquilados[i].Modelo}");
            }

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > alquilados.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            sistema.DevolverVehiculo(alquilados[index - 1]);
            Console.WriteLine("Vehículo devuelto correctamente.");
        }

        static void EliminarVehiculo(SistemaAlquiler sistema)
        {
            var disponibles = sistema.Vehiculos.Where(v => v.Estado == EstadoVehiculo.Disponible).ToList();
            if (!disponibles.Any())
            {
                Console.WriteLine("No hay vehículos disponibles para eliminar.");
                return;
            }

            Console.WriteLine("Seleccione el número del vehículo a eliminar:");
            for (int i = 0; i < disponibles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {disponibles[i].Marca} {disponibles[i].Modelo}");
            }

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > disponibles.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            sistema.EliminarVehiculo(disponibles[index - 1]);
            Console.WriteLine("Vehículo eliminado correctamente.");
        }
    }
}
