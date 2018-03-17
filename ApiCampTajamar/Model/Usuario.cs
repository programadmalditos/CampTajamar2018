using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCampTajamar.Model
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
