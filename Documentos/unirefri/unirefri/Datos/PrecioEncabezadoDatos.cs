using System.Data;
using System.Data.SqlClient;
using unirefri.Models;

namespace unirefri.Datos
{
    public class PrecioEncabezadoDatos
    {
        public List<PrecioEncabezado> listarPrecioEncabezado()
        {

            var oprecioencabezado = new List<PrecioEncabezado>();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdpe = new SqlCommand("sp_PrecioEncabezado", conectar);
                cmdpe.CommandType = CommandType.StoredProcedure;

                using (var drpe = cmdpe.ExecuteReader())
                {
                    while (drpe.Read())
                    {
                        oprecioencabezado.Add(new PrecioEncabezado()
                        {
                            IdCorrelativo = Convert.ToInt32(drpe["IdCorrelativo"]),
                            FechaPrecioEncabezado = drpe["fechaPrecioEncabezado"].ToString(),
                            horaPrecioEncabezado = drpe["horaPrecioEncabezado"].ToString(),
                        });
                    }
                }


            }

            return oprecioencabezado;
        }
        public PrecioEncabezado obtenerPrecioEncabezado(int IdCorrelativo)
        {

            var oprecioenca = new PrecioEncabezado();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdpe = new SqlCommand("sp_obtener", conectar);
                cmdpe.Parameters.AddWithValue("IdCorrelativo", IdCorrelativo);
                cmdpe.CommandType = CommandType.StoredProcedure;

                using (var drpe = cmdpe.ExecuteReader())
                {
                    while (drpe.Read())
                    {

                        oprecioenca.IdCorrelativo = Convert.ToInt32(drpe["IdCorrelativo"]);
                        oprecioenca.FechaPrecioEncabezado = drpe["fechaPrecioEncabezado"].ToString();
                        oprecioenca.horaPrecioEncabezado = drpe["horaPrecioEncabezado"].ToString();
                        //  oprecioenca.IdUsuario = drpe["IdUsuario"].ToString();
                    }
                }


            }

            return oprecioenca;
        }

        public bool guardarPrecioEncabezado(PrecioEncabezado oprecioenca)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdpe = new SqlCommand("sp_guardarPrecioEncabezado", conectar);
                    cmdpe.Parameters.AddWithValue("fechaPrecioEncabezado", oprecioenca.FechaPrecioEncabezado);
                    cmdpe.Parameters.AddWithValue("horaPrecioEncabezado", oprecioenca.horaPrecioEncabezado);
                    // cmdpe.Parameters.AddWithValue("idUsuario", oprecioenca.IdUsuario);
                    cmdpe.CommandType = CommandType.StoredProcedure;
                    cmdpe.ExecuteNonQuery();

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

        public bool editarPrecioEncabezado(PrecioEncabezado oprecioenca)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdpe = new SqlCommand("sp_editarPrecioEncabezado", conectar);
                    cmdpe.Parameters.AddWithValue("IdCorrelativo", oprecioenca.IdCorrelativo);
                    cmdpe.Parameters.AddWithValue("fechaPrecioEncabezado", oprecioenca.FechaPrecioEncabezado);
                    cmdpe.Parameters.AddWithValue("horaPrecioEncabezado", oprecioenca.horaPrecioEncabezado);
                    // cmdpe.Parameters.AddWithValue("idUsuario", oprecioenca.IdUsuario);
                    cmdpe.CommandType = CommandType.StoredProcedure;
                    cmdpe.ExecuteNonQuery();

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
        public bool eliminarPrecioEncabezado(int IdCorrelativo)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdpe = new SqlCommand("sp_eliminarPrecioEncabezado", conectar);
                    cmdpe.Parameters.AddWithValue("IdCorrelativo", IdCorrelativo);
                    cmdpe.CommandType = CommandType.StoredProcedure;
                    cmdpe.ExecuteNonQuery();

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
