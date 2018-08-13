using System;
using System.Collections.Generic;
using System.Text;

namespace testPrinter
{
    public class item
    {
       public  string prd { get; set; }
       public double precio { get; set; }

        public item(string a, double b) {
            this.prd = a;
            this.precio = b;

        }
    }
}
