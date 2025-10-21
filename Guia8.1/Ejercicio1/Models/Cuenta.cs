using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1.Models
{
    internal class Cuenta: IComparable
    {
        public int Dni { get; set; }
        public double Importe { get; set; }
        public string Nombre { get; set; }

        public Cuenta (int dni, string nom, double imp)
        {
            this.Dni = dni;
            this.Importe = imp;
            this.Nombre = nom;
        }

        public int CompareTo(Object otherObject)
        {
            Cuenta other = otherObject as Cuenta;
            if (other != null)
            {
                return this.Dni.CompareTo(other.Dni);
            }
            return 0;
        }

        public override string ToString()
        {
            return $"{Nombre}-{Dni}-{Importe}";
        }
    }
}
