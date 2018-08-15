using System;
using System.Collections.Generic;
using System.Text;

namespace testPrinter
{
    public class item
    {
        public Int64 id { get; set; }
       public  string prd { get; set; }
       public double precio { get; set; }

        public item( Int64 id,  string a, double b) {
            this.id = id;
            this.prd = a;
            this.precio = b;

        }
    }
}
