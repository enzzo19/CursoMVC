using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.Web.Security;

namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        // GET: Acceso
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o Contraseña Incorrecta"; // Para compartir info en la misma vista
                return View();
            }
            else
            {
                if (oUsuario.Reestablecer)
                {
                    TempData["IdUsuario"] = oUsuario.IdUsuario; // Para compartir info en distintas vista con un mismocontrolador
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false); // Crea una cookie de autenticacion

                ViewBag.Error = null;
                
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {

            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.IdUsuario == int.Parse(idusuario)).FirstOrDefault();

            if (oUsuario.Clave != CN_Recursos.ConvertirSha256(claveactual))
            {   
                TempData["IdUsuario"] = idusuario;  // Para que no se pierda al actualizar
                ViewData["vclave"] = "";
                ViewBag.Error = "La Contraseña Actual no es correcta";
                return View();

            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["IdUsuario"] = idusuario;  // Para que no se pierda al actualizar
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las Nuevas Contraseñas NO Coinciden";
                return View();

            }
            ViewData["vclave"] = "";

            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);
            string mensaje;


            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdUsuario"] = idusuario;  // Para que no se pierda al actualizar
                // ViewData["vclave"] = claveactual;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(item => item.Correo == correo).FirstOrDefault();

            if(oUsuario == null)
            {
                ViewBag.Error = "No se encontro un Usuario con ese correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().ReestablecerClave(oUsuario.IdUsuario, correo, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");

            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }


        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }


    }
}