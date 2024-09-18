using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1
{
    public class Automovil : Vehiculo
    {
        public Automovil(string marca, string modelo, int anioFabricacion, decimal precioPorDia)
            : base(marca, modelo, anioFabricacion, precioPorDia) { }

        public override void MostrarDetalles()
        {
            Console.WriteLine($"Automóvil: Marca: {Marca}, Modelo: {Modelo}, Año: {AnioFabricacion}, Precio por día: {PrecioPorDia:C2}, Estado: {Estado}");
        }
    }
}