using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiCampTajamar.Model
{
    public class Producto
    {
        [Key] public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Existencias { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<ProductoTienda> ProductoTienda { get; set; }
    }
}