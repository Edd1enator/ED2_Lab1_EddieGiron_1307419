using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_EddieGiron_1307419
{
    public class Persona: IComparable<Persona>
    {
        public Persona() {
            name = "";
            dpi = "";
            birthday = DateTime.MinValue;
            address = "";
        }
        public string name { get; set; }
        public string dpi { get; set; }
        public DateTime birthday { get; set; }
        public string address { get; set; }

        /*public static Comparison<Persona> CompararNombre(Persona persona1, Persona persona2)
        {
            return persona1.name.CompareTo(persona2.name);
        }*/
        public int CompareTo(Persona other)
        {
            if (other == null) return 0;
            int result = this.dpi.CompareTo(other.dpi);
            if (result == 0) 
            {
                result = this.name.CompareTo(other.name);
            }
            return result;
        }
    }
   
}
