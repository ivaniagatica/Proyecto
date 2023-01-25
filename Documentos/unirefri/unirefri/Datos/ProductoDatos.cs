using System.Data;
using System.Data.SqlClient;
using unirefri.Models;

namespace unirefri.Datos
{
    public class ProductoDatos
    {
        public List<Producto> listarProducto()
        {

            var oproducto = new List<Producto>();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdp = new SqlCommand("sp_Producto", conectar);
                cmdp.CommandType = CommandType.StoredProcedure;

                using (var drp = cmdp.ExecuteReader())
                {
                    while (drp.Read())
                    {
                        oproducto.Add(new Producto()
                        {
                            IdProducto = Convert.ToInt32(drp["IdProducto"]),
                            DescripcionProducto = drp["descripcionProducto"].ToString(),
                            FechaCreacionProducto = drp["fechaCreacionProducto"].ToString(),
                            UsuarioCreacionProducto = drp["usuarioCreacionProducto"].ToString(),
                            FechaActualizacionProducto = drp["fechaActualizacionProducto"].ToString(),
                            UsuarioActualizacionProducto = drp["usuarioActualizacionProducto"].ToString(),
                        });
                    }
                }


            }

            return oproducto;
        }


        public Producto ObtenerProducto(int IdProducto)
        {

            var oproduto = new Producto();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmdp = new SqlCommand("sp_obtenerProducto", conectar);
                cmdp.Parameters.AddWithValue("IdProducto", IdProducto);
                cmdp.CommandType = CommandType.StoredProcedure;

                using (var drp = cmdp.ExecuteReader())
                {
                    while (drp.Read())
                    {

                        oproduto.IdProducto = Convert.ToInt32(drp["IdProducto"]);
                        oproduto.DescripcionProducto = drp["descripcionProducto"].ToString();
                        oproduto.FechaCreacionProducto = drp["fechaCreacionProducto"].ToString();
                        oproduto.UsuarioCreacionProducto = drp["usuarioCreacionProducto"].ToString();
                        oproduto.FechaActualizacionProducto = drp["fechaActualizacionProducto"].ToString();
                        oproduto.UsuarioActualizacionProducto = drp["usuarioActualizacionProducto"].ToString();

                    }
                }


            }

            return oproduto;
        }

        public bool guardarProducto(Producto oproduto)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdp = new SqlCommand("sp_guardarProducto", conectar);
                    cmdp.Parameters.AddWithValue("descripcionProducto", oproduto.DescripcionProducto);
                    cmdp.Parameters.AddWithValue("fechaCreacionProducto", oproduto.FechaCreacionProducto);
                    cmdp.Parameters.AddWithValue("usuarioCreacionProducto", oproduto.UsuarioCreacionProducto);
                    cmdp.Parameters.AddWithValue("fechaActualizacionProducto", oproduto.FechaActualizacionProducto);
                    cmdp.Parameters.AddWithValue("usuarioActualizacionProducto", oproduto.UsuarioActualizacionProducto);
                    cmdp.CommandType = CommandType.StoredProcedure;
                    cmdp.ExecuteNonQuery();

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

        public bool editarProducto(Producto oproduto)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdp = new SqlCommand("sp_editarProducto", conectar);
                    cmdp.Parameters.AddWithValue("IdProducto", oproduto.IdProducto);
                    cmdp.Parameters.AddWithValue("descripcionProducto", oproduto.DescripcionProducto);
                    cmdp.Parameters.AddWithValue("fechaCreacionProducto", oproduto.FechaCreacionProducto);
                    cmdp.Parameters.AddWithValue("usuarioCreacionProducto", oproduto.UsuarioCreacionProducto);
                    cmdp.Parameters.AddWithValue("fechaActualizacionProducto", oproduto.FechaActualizacionProducto);
                    cmdp.Parameters.AddWithValue("usuarioActualizacionProducto", oproduto.UsuarioActualizacionProducto);
                    cmdp.CommandType = CommandType.StoredProcedure;
                    cmdp.ExecuteNonQuery();

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
        public bool eliminarProducto(int IdProducto)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmdp = new SqlCommand("sp_eliminarProducto", conectar);
                    cmdp.Parameters.AddWithValue("IdProducto", IdProducto);
                    cmdp.CommandType = CommandType.StoredProcedure;
                    cmdp.ExecuteNonQuery();

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
