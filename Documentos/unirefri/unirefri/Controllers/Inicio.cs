using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using unirefri.Models;
using unirefri.Datos;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace unirefri.Controllers
{
    public class Inicio : Controller
    {
        static string union = "Server=DESKTOP-H70EI0M\\SQLEXPRESS;Database=Unirefri_BDa;Trusted_Connection=True;Integrated Security=True;";

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Registrar(Usuario ouser)
        {
            bool registrado;
            string mensaje;


            if (ouser.ContrasenaUsuario == ouser.confirmarClave)
            {
                ouser.ContrasenaUsuario = ConvertirSha256(ouser.ContrasenaUsuario);

            }
            else
            {
                ViewData["mensaje"] = "Las contraseñas no coinciden ";
                return View();

            }

            using (SqlConnection cnn = new SqlConnection(union))
            {
                SqlCommand cmr = new SqlCommand("sp_RegistrarUsuarios", cnn);
                cmr.Parameters.AddWithValue("NombreUsuario", ouser.NombreUsuario);
                cmr.Parameters.AddWithValue("ContrasenaUsuario", ouser.ContrasenaUsuario);
                cmr.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmr.Parameters.Add("mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmr.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                cmr.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmr.Parameters["registrado"].Value);
                mensaje = cmr.Parameters["mensaje"].Value.ToString();

            }


            ViewData["mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Inicio");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult Login(Usuario ouser)
        {

            ouser.ContrasenaUsuario = ConvertirSha256(ouser.ContrasenaUsuario);



            try
            {
                using (SqlConnection cnn = new SqlConnection(union))
                {
                    SqlCommand cmr = new SqlCommand("sp_validarUsuarios", cnn);
                    cmr.Parameters.AddWithValue("NombreUsuario", ouser.NombreUsuario);
                    cmr.Parameters.AddWithValue("ContrasenaUsuario", ouser.ContrasenaUsuario);
                    cmr.CommandType = CommandType.StoredProcedure;

                    cnn.Open();
                    ouser.IdUsuario = Convert.ToInt32(cmr.ExecuteScalar().ToString());

                }
            }
            catch (Exception e)
            {
                string error = e.Message;

            }

            if (ouser.IdUsuario != 0)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["mensaje"] = " El usuario / contraseña es incorrecto";
                return View();
            }

        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }
    }
}
