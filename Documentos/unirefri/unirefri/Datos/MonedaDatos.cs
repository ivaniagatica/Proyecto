using System.Data;
using System.Data.SqlClient;
using unirefri.Models;

namespace unirefri.Datos
{
    public class MonedaDatos
    {
        public List<Moneda> listarMoneda()
        {

            var oMoneda = new List<Moneda>();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdm = new SqlCommand("sp_Moneda", conectar);
                cmdm.CommandType = CommandType.StoredProcedure;

                using (var drm = cmdm.ExecuteReader())
                {
                    while (drm.Read())
                    {
                        oMoneda.Add(new Moneda()
                        {
                            IdMoneda = Convert.ToInt32(drm["IdMoneda"]),
                            Descripcion = drm["descripcionMoneda"].ToString(),
                        });
                    }
                }


            }

            return oMoneda;
        }

        public Moneda ObtenerMoneda(int IdMoneda)
        {

            var omoneda = new Moneda();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdm = new SqlCommand("sp_obtenerMoneda", conectar);
                cmdm.Parameters.AddWithValue("IdMoneda", IdMoneda);
                cmdm.CommandType = CommandType.StoredProcedure;

                using (var drm = cmdm.ExecuteReader())
                {
                    while (drm.Read())
                    {
                        omoneda.IdMoneda = Convert.ToInt32(drm["IdMoneda"]);
                        omoneda.Descripcion = drm["descripcionMoneda"].ToString();
                    }
                }

            }

            return omoneda;
        }


        public bool guardarmoneda(Moneda omoneda)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdm = new SqlCommand("sp_guardarMoneda", conectar);
                    cmdm.Parameters.AddWithValue("descripcionMoneda", omoneda.Descripcion);
                    cmdm.CommandType = CommandType.StoredProcedure;
                    cmdm.ExecuteNonQuery();

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
        public bool editarMoneda(Moneda omoneda)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdm = new SqlCommand("sp_editarMoneda", conectar);
                    cmdm.Parameters.AddWithValue("Idcitas", omoneda.IdMoneda);
                    cmdm.Parameters.AddWithValue("descripcionMoneda", omoneda.Descripcion);
                    cmdm.CommandType = CommandType.StoredProcedure;
                    cmdm.ExecuteNonQuery();

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

        public bool eliminarMoneda(int IdMoneda)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdm = new SqlCommand("sp_eliminarMoneda", conectar);
                    cmdm.Parameters.AddWithValue("IdMoneda", IdMoneda);
                    cmdm.CommandType = CommandType.StoredProcedure;
                    cmdm.ExecuteNonQuery();

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
