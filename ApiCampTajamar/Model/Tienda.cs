using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCampTajamar.Model
{
    public class Tienda
    {
        [Key]
        public int IdTienda { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ProductoTienda> ProductoTienda { get; set; }

    }
}
