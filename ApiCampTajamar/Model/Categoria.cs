using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApiCampTajamar.Model;

namespace ApiCampTajamar.Model
{
public class Categoria{
    [Key]
    public int IdCategoria { get; set; }
    public string Nombre { get; set; }
    public virtual ICollection<Producto> Producto { get; set; }

}
}
