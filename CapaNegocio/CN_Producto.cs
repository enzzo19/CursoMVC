using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto
    {

        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        
        public int Registrar(Producto obj, out string Mensaje)
        {

            Mensaje = String.Empty;

            // Validando el Nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El Nombre del Producto no puede estar vacio";
            }


            // Validando la Descripcion
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descricion del Producto no puede estar vacio";
            }

            // Validando la Marca
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe Seleccionar una Marca";
            }

            // Validando la Categoria
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Seleccionar una Categoria";
            }

            // Validando el Precio
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el Precio del Producto";
            }

            // Validando el Stock
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el Stock del Producto";
            }


            // Si despues de todas las verificacioes no cambio el mensaje
            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Registrar(obj, out Mensaje);

            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = String.Empty;

            // Validando el Nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El Nombre del Producto no puede estar vacio";
            }


            // Validando la Descripcion
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descricion del Producto no puede estar vacio";
            }

            // Validando la Marca
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe Seleccionar una Marca";
            }

            // Validando la Categoria
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Seleccionar una Categoria";
            }

            // Validando el Precio
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el Precio del Producto";
            }

            // Validando el Stock
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el Stock del Producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }


        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }


    }
}
