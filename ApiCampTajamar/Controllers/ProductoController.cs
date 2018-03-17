using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCampTajamar.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCampTajamar.Controllers
{
    [Produces("application/json")]
    [Route("api/Producto")]
    public class ProductoController : Controller
    {
        public ProductoContext Context { get; private set; }
        public ProductoController(ProductoContext context)
        {
            Context = context;
        }

        public IEnumerable<Producto> Get()
        {
            var data = Context.Producto.ToList();
            return data;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)

        {
            var data = Context.Producto.FirstOrDefault(o => o.IdProducto == id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Producto value)
        {
            Context.Producto.Add(value);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created("", value);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Producto value)
        {
            var data = Context.Producto.FirstOrDefault(o => o.IdProducto == id);
            if (data == null)
                return NotFound();
            data.Existencias = value.Existencias;
            data.Nombre = value.Nombre;
            data.Precio = value.Precio;
            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = Context.Producto.Find(id);
            if (data == null)
                return NotFound();
            Context.Producto.Remove(data);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
}