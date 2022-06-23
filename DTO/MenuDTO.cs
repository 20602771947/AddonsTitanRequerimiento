using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MenuDTO
    {
        public int IdMenu { get; set; }
        public string Menu { get; set; }
        public bool Estado { get; set; }
        public int Orden { get; set; }
        public int Posicion { get; set; }
    }
}
