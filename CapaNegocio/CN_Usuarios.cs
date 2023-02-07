using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios(); 

        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {

            Mensaje = String.Empty;

            // Validando el Nombre
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres)) 
            {
                Mensaje = "El Nombre del usuario no puede ser vacio";
            }

            // Validando el Apellido
            if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellido del usuario no puede ser vacio";
            }

            // Validando el Correo
            if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede ser vacio";
            }


            // Si despues de todas las verificacioes no cambio el mensaje
            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "Bienvenido a la plataforma de Ventas";
                string mensaje_correo = "<h3>Bienvenido a la plataforma de Ventas El Grillo</h3><br><p>Su clave de acceso es:  !clave! </p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(clave); // Encriptar la Clave

                    return objCapaDato.Registrar(obj, out Mensaje);

                }
                else
                {
                    Mensaje = "No se pudo Enviar el Correo";
                    return 0;
                }

                
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = String.Empty;

            // Validando el Nombre
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El Nombre del usuario no puede ser vacio";
            }

            // Validando el Apellido
            if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellido del usuario no puede ser vacio";
            }

            // Validando el Correo
            if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede ser vacio";
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

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            return objCapaDato.CambiarClave(idusuario, nuevaclave,out Mensaje);
        }



        public bool ReestablecerClave(int idusuario, string correo, out string Mensaje)
        {

            Mensaje = String.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();

            bool resultado = objCapaDato.ReestablecerClave(idusuario, CN_Recursos.ConvertirSha256(nuevaclave), out Mensaje);

            if (resultado) {

                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente </h3><br><p>Su nueva clave de acceso es:  !clave! </p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo Enviar el Correo";
                    return false;
                }

            }
            else
            {
                Mensaje = "No se pudo Reestablecer la Contraseña";
                return false;
            }
        }

    }
}
