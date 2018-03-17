using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCampTajamar.Model
{
    public class ProductoTienda
    {
        public int IdProducto { get; set; }
        public int IdTienda { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Tienda Tienda { get; set; }
    }
}
