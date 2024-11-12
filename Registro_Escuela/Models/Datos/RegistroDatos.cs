using Registro_Escuela.Models;
using System.Data.SqlClient;
using System.Data;

namespace Registro_Escuela.Datos
{
    public class RegistroDatos
    {
        

        public List<Registro> Listar()
        {
            var selectList = new List<Registro>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Sp_Select1", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        selectList.Add(new Registro
                        {
                            ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]),
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
							NomTutor = dr["NomTutor"].ToString(),
							Telefono = dr["Telefono"].ToString(),
                            Contraseña = dr["Contraseña"].ToString(),
                        });
                    }
                }
            }

            return selectList;
        }

        public Registro Obtener(int ID_Usuario)
        {
            var nRegistro = new Registro();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarUsuario", conexion);
                cmd.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        nRegistro.ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]);
                        nRegistro.Nombres = dr["Nombres"].ToString();
                        nRegistro.Apellidos = dr["Apellidos"].ToString();
						nRegistro.NomTutor = dr["NomTutor"].ToString();
						nRegistro.Telefono = dr["Telefono"].ToString();
                        nRegistro.Contraseña = dr["Contraseña"].ToString();
                    }
                }
            }

            return nRegistro;
        }

        public bool Guardar(Registro nRegistro)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Sp_Incertar", conexion);
                    cmd.Parameters.AddWithValue("Nombres", nRegistro.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", nRegistro.Apellidos);
                    cmd.Parameters.AddWithValue("NomTutor", nRegistro.NomTutor);
                    cmd.Parameters.AddWithValue("Telefono", nRegistro.Telefono);
                    cmd.Parameters.AddWithValue("Contraseña", nRegistro.Contraseña);
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

        public bool Eliminar(int ID_Usuario)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EliminarUsuario", conexion);
                    cmd.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
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
        public bool Editar(Registro nRegistro)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EditarUsuario", conexion);
                    cmd.Parameters.AddWithValue("ID_Usuario", nRegistro.ID_Usuario);
                    cmd.Parameters.AddWithValue("Nombres", nRegistro.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", nRegistro.Apellidos);
                    cmd.Parameters.AddWithValue("NomTutor", nRegistro.NomTutor);
                    cmd.Parameters.AddWithValue("Telefono", nRegistro.Telefono);
                    cmd.Parameters.AddWithValue("Contraseña", nRegistro.Contraseña);
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
