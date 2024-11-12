using Registro_Escuela.Datos;
using System.Data.SqlClient;
using System.Data;

namespace Registro_Escuela.Models.Datos
{
    public class ActividadesDatos
    {
        // Codigo de Actividades 
        public List<Actividades> VerActividades()
        {
            var SelectAct = new List<Actividades>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Sp_Actividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SelectAct.Add(new Actividades
                        {
                            ID_Actividad = Convert.ToInt32(dr["ID_Actividad"]),
                            NombreActividad = dr["NombreActividad"].ToString(),
                            Fecha = dr["Fecha"].ToString(),
                            Conducta = dr["Conducta"].ToString(),
                            ID_Usuario = dr["ID_Usuario"].ToString(),

                        });
                    }
                }
            }

            return SelectAct;
        }


        public Actividades ObtenerAct(int ID_Actividad)
        {
            var nActividad = new Actividades();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarActividad", conexion);
                cmd.Parameters.AddWithValue("ID_Actividad", ID_Actividad);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        nActividad.ID_Actividad = Convert.ToInt32(dr["ID_Actividad"]);
                        nActividad.NombreActividad = dr["NombreActividad"].ToString();
                        nActividad.Fecha = dr["Fecha"].ToString();
                        nActividad.Conducta = dr["Conducta"].ToString();
                        nActividad.ID_Usuario = dr["ID_Usuario"].ToString();

                    }
                }
            }

            return nActividad;
        }
        public bool GuardarAct(Actividades nActividad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarActividad", conexion);
                    cmd.Parameters.AddWithValue("NombreActividad", nActividad.NombreActividad);
                    cmd.Parameters.AddWithValue("Fecha", nActividad.Fecha);
                    cmd.Parameters.AddWithValue("Conducta", nActividad.Conducta);
                    cmd.Parameters.AddWithValue("ID_Usuario", nActividad.ID_Usuario);
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
        public bool EliminarAct(int ID_Actividad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarActividad", conexion);
                    cmd.Parameters.AddWithValue("ID_Actividad", ID_Actividad);
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
        public bool EditarAct(Actividades nActividad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarActividad", conexion);
                    cmd.Parameters.AddWithValue("ID_Actividad", nActividad.ID_Actividad);
                    cmd.Parameters.AddWithValue("NombreActividad", nActividad.NombreActividad);
                    cmd.Parameters.AddWithValue("Fecha", nActividad.Fecha);
                    cmd.Parameters.AddWithValue("Conducta", nActividad.Conducta);
                    cmd.Parameters.AddWithValue("ID_Usuario", nActividad.ID_Usuario);

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
