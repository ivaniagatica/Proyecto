using System.Data;
using System.Data.SqlClient;
using unirefri.Models;

namespace unirefri.Datos
{
    public class EmpleadoDatos
    {
        public List<Empleado> listarEmpleado()
        {

            var olistaEmpleado = new List<Empleado>();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmd = new SqlCommand(" sp_Empleado", conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olistaEmpleado.Add(new Empleado()
                        {
                            IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                            NombreEmpleados = dr["nombreEmpleado"].ToString(),
                            ApellidoEmpleados = dr["apellidoEmpleado"].ToString(),
                            PuestoEmpleados = dr["puestoEmpleado"].ToString(),
                        });
                    }
                }


            }

            return olistaEmpleado;
        }


        public Empleado ObtenerEmpleado(int IdEmpleado)
        {

            var oempleado = new Empleado();

            var cn = new conectar();

            using (var conectar = new SqlConnection(cn.getCadenaSQL()))
            {
                conectar.Open();
                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", conectar);
                cmd.Parameters.AddWithValue("IdEmpleado", IdEmpleado);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oempleado.IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]);
                        oempleado.NombreEmpleados = dr["nombreEmpleado"].ToString();
                        oempleado.ApellidoEmpleados = dr["apellidoEmpleado"].ToString();
                        oempleado.PuestoEmpleados = dr["puestoEmpleado"].ToString();

                    }
                }
            }

            return oempleado;
        }

        public bool guardarEmpleado(Empleado oempleado)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmd = new SqlCommand("sp_guardarEmpleado", conectar);
                    cmd.Parameters.AddWithValue("nombreEmpleado", oempleado.NombreEmpleados);
                    cmd.Parameters.AddWithValue("apellidoEmpleado", oempleado.ApellidoEmpleados);
                    cmd.Parameters.AddWithValue("puestoEmpleado", oempleado.PuestoEmpleados);
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

        public bool editarEmpleado(Empleado oempleado)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmd = new SqlCommand("sp_editarEmpleado", conectar);
                    cmd.Parameters.AddWithValue("IdEmpleado", oempleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("nombreEmpleado", oempleado.NombreEmpleados);
                    cmd.Parameters.AddWithValue("apellidoEmpleado", oempleado.ApellidoEmpleados);
                    cmd.Parameters.AddWithValue("puestoEmpleado", oempleado.PuestoEmpleados);
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

        public bool eliminarEmpleado(int IdEmpleado)
        {
            bool rpta;

            try
            {
                var cn = new conectar();

                using (var conectar = new SqlConnection(cn.getCadenaSQL()))
                {
                    conectar.Open();
                    SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", conectar);
                    cmd.Parameters.AddWithValue("IdEmpleado", IdEmpleado);
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
