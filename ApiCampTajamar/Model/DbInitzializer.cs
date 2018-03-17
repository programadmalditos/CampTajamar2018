using System.Linq;
using ApiCampTajamar.Options;

namespace ApiCampTajamar.Model
{
    public class DbInitzializer
    {
        public static void Initialize(ProductoContext context)

        {
            if (!context.Usuario.Any())
            {
                context.Usuario.Add(new Usuario() {Login = "Luis",
                                Password = Sha1Utils.GetSha1("1234")});
                context.SaveChanges();
            }

            if (context.Producto.Any())

            {

                return;

            }



            var tienda = new Tienda()

            {

                Nombre = "La tienda de telefonos",



            };

            context.Tienda.Add(tienda);



            var cat = new Categoria()

            {

                Nombre = "Telefonia",



            };

            context.Categoria.Add(cat);



            var productos = new Producto[]{

                new Producto(){



                    Nombre="Iphone",

                    Existencias= 5,

                    Precio=1100,

                    Categoria= cat



                },

                new Producto(){



                    Nombre="Galaxy s9",

                    Existencias= 3,

                    Precio=900,

                    Categoria= cat

                },

            };

            foreach (var compra in productos)

            {

                context.Producto.Add(compra);

                var ti = new ProductoTienda()

                {

                    Producto = compra,

                    Tienda = tienda

                };

                context.ProductoTienda.Add(ti);

            }

            context.SaveChanges();



        }
    }
}