using System.Data;
using System.Data.SqlClient;
using unirefri.Models;



namespace unirefri.Datos
{
    public class PrecioDetalleDatos
    {

        public List<PrecioDetalle> listarPrecioDetalle()
        {

            var opreciodetalle = new List<PrecioDetalle>();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdpd = new SqlCommand("sp_PrecioDetalle", conectar);
                cmdpd.CommandType = CommandType.StoredProcedure;

                using (var drpd = cmdpd.ExecuteReader())
                {
                    while (drpd.Read())
                    {
                        opreciodetalle.Add(new PrecioDetalle()
                        {
                            IdPrecioDetalle = Convert.ToInt32(drpd["IdPrecioDetalle"]),
                            NumeroArticulo = drpd["NumeroArticulo"].ToString(),
                            Articulos = drpd["Articulos"].ToString(),
                            PrecioLista = drpd["PrecioLista"].ToString(),

                        });
                    }
                }


            }

            return opreciodetalle;
        }


        public PrecioDetalle obtenerPrecioDetalle(int IdPrecioDetalle)
        {

            var opreciod = new PrecioDetalle();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdpd = new SqlCommand("sp_obtenerPrecioDetalle", conectar);
                cmdpd.Parameters.AddWithValue("IdprecioDetalle", IdPrecioDetalle);
                cmdpd.CommandType = CommandType.StoredProcedure;

                using (var drpd = cmdpd.ExecuteReader())
                {
                    while (drpd.Read())
                    {

                        opreciod.IdPrecioDetalle = Convert.ToInt32(drpd["IdprecioDetalle"]);
                        opreciod.NumeroArticulo = drpd["NumeroArticulo"].ToString();
                        opreciod.Articulos = drpd["Articulos"].ToString();
                        opreciod.PrecioLista = drpd["PrecioLista"].ToString();

                    }
                }


            }

            return opreciod;
        }
        public bool guardarPrecioDetalle(PrecioDetalle opreciod)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdpd = new SqlCommand(" sp_guardarPrecioDetalle", conectar);
                    cmdpd.Parameters.AddWithValue("NumeroArticulo", opreciod.NumeroArticulo);
                    cmdpd.Parameters.AddWithValue("Articulos", opreciod.Articulos);
                    cmdpd.Parameters.AddWithValue("PrecioLista", opreciod.PrecioLista);
                    cmdpd.CommandType = CommandType.StoredProcedure;
                    cmdpd.ExecuteNonQuery();

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

        public bool editarPrecioDetalle(PrecioDetalle opreciod)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdpd = new SqlCommand("sp_editarPrecioDetalle", conectar);
                    cmdpd.Parameters.AddWithValue("IdprecioDetalle", opreciod.IdPrecioDetalle);
                    cmdpd.Parameters.AddWithValue("NumeroArticulo", opreciod.NumeroArticulo);
                    cmdpd.Parameters.AddWithValue("Articulos", opreciod.Articulos);
                    cmdpd.Parameters.AddWithValue("PrecioLista", opreciod.PrecioLista);
                    cmdpd.CommandType = CommandType.StoredProcedure;
                    cmdpd.ExecuteNonQuery();

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

        public bool eliminarPrecioDetalle(int IdPrecioDetalle)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdpd = new SqlCommand("sp_eliminarPrecioDetalle", conectar);
                    cmdpd.Parameters.AddWithValue("IdprecioDetalle", IdPrecioDetalle);
                    cmdpd.CommandType = CommandType.StoredProcedure;
                    cmdpd.ExecuteNonQuery();

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





