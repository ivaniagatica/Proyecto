using System.Data;
using System.Data.SqlClient;
using unirefri.Models;

namespace unirefri.Datos
{
    public class ListaDatos
    {
        public List<Lista> lista()
        {

            var olistainformacion = new List<Lista>();

            var cnl = new conectar();

            using (var conectar = new SqlConnection(cnl.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdl = new SqlCommand("sp_Lista", conectar);
                cmdl.CommandType = CommandType.StoredProcedure;

                using (var drl = cmdl.ExecuteReader())
                {
                    while (drl.Read())
                    {
                        olistainformacion.Add(new Lista()
                        {
                            IdLista = Convert.ToInt32(drl["IdLista"]),
                            NombreLista = drl["nombreLista"].ToString(),
                            FechaCreacion = drl["fechaCreacionLista"].ToString(),
                            UsuarioCreacion = drl["usuarioCreacionLista"].ToString(),
                            FechaActualizacion = drl["fechaActualizacionLista"].ToString(),
                            UsuarioActualizacion = drl["usuarioActualizacionLista"].ToString(),

                        });
                    }

                }

            }

            return olistainformacion;
        }

        public Lista ObtenerLista(int IdLista)
        {

            var olista = new Lista();

            var cnl = new conectar();

            using (var conectar = new SqlConnection(cnl.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdl = new SqlCommand("sp_obtenerLista", conectar);
                cmdl.Parameters.AddWithValue("IdLista", IdLista);
                cmdl.CommandType = CommandType.StoredProcedure;

                using (var drl = cmdl.ExecuteReader())
                {
                    while (drl.Read())
                    {

                        olista.IdLista = Convert.ToInt32(drl["IdLista"]);
                        olista.NombreLista = drl["nombreLista"].ToString();
                        olista.FechaCreacion = drl["fechaCreacionLista"].ToString();
                        olista.UsuarioCreacion = drl["usuarioCreacionLista"].ToString();
                        olista.FechaActualizacion = drl["fechaActualizacionLista"].ToString();
                        olista.UsuarioActualizacion = drl["usuarioActualizacionLista"].ToString();


                    }
                }


            }

            return olista;
        }

        public bool guardarLista(Lista olista)
        {
            bool rpta;

            try
            {
                var cnl = new conectar();

                using (var conectar = new SqlConnection(cnl.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdl = new SqlCommand("sp_guardarLista", conectar);
                    cmdl.Parameters.AddWithValue("nombreLista", olista.NombreLista);
                    cmdl.Parameters.AddWithValue("fechaCreacionLista", olista.FechaCreacion);
                    cmdl.Parameters.AddWithValue("usuarioCreacionLista", olista.UsuarioCreacion);
                    cmdl.Parameters.AddWithValue("fechaActualizacionLista", olista.FechaActualizacion);
                    cmdl.Parameters.AddWithValue("usuarioActualizacionLista", olista.UsuarioActualizacion);
                    cmdl.CommandType = CommandType.StoredProcedure;
                    cmdl.ExecuteNonQuery();

                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool editarLista(Lista olista)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdl = new SqlCommand("sp_editarLista", conectar);
                    cmdl.Parameters.AddWithValue("IdLista", olista.IdLista);
                    cmdl.Parameters.AddWithValue("nombreLista", olista.NombreLista);
                    cmdl.Parameters.AddWithValue("fechaCreacionLista", olista.FechaCreacion);
                    cmdl.Parameters.AddWithValue("usuarioCreacionLista", olista.UsuarioCreacion);
                    cmdl.Parameters.AddWithValue("fechaActualizacionLista", olista.FechaActualizacion);
                    cmdl.Parameters.AddWithValue("usuarioActualizacionLista", olista.UsuarioActualizacion);
                    cmdl.CommandType = CommandType.StoredProcedure;
                    cmdl.ExecuteNonQuery();

                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool eliminarLista(int IdLista)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmd = new SqlCommand("sp_eliminarLista", conectar);
                    cmd.Parameters.AddWithValue("IdLista", IdLista);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }
    }
}

