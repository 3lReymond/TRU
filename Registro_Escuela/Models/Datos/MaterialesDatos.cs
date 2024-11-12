using Registro_Escuela.Datos;
using System.Data.SqlClient;
using System.Data;

namespace Registro_Escuela.Models.Datos
{
    public class MaterialesDatos
    {
        public List<Materiales> VerMateriales()
        {
            var SelectMat = new List<Materiales>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarMateriales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SelectMat.Add(new Materiales
                        {
                            ID_Materiales = Convert.ToInt32(dr["ID_Materiales"]),
                            Nombre = dr["Nombre"].ToString(),
                            Stock = dr["Stock"] != DBNull.Value ? Convert.ToInt32(dr["Stock"]) : (int?)null,
                            Estado = dr["Estado"].ToString()
                        });
                    }
                }
            }

            return SelectMat;
        }

        public Materiales ObtenerMat(int ID_Materiales)
        {
            var nMateriales = new Materiales();
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerMateriales", conexion);
                    cmd.Parameters.AddWithValue("@ID_Materiales", ID_Materiales); 
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nMateriales.ID_Materiales = Convert.ToInt32(dr["ID_Materiales"]);
                            nMateriales.Nombre = dr["Nombre"].ToString();
                            nMateriales.Stock = dr["Stock"] != DBNull.Value ? Convert.ToInt32(dr["Stock"]) : (int?)null;
                            nMateriales.Estado = dr["Estado"].ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
               
                string error = $"Error SQL: {ex.Message}";
                
            }
            catch (Exception ex)
            {
                
                string error = $"Error: {ex.Message}";
                
            }

            return nMateriales;
        }

        public bool GuardarMat(Materiales nMateriales)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarMaterial", conexion);
                    cmd.Parameters.AddWithValue("@Nombre", nMateriales.Nombre); 
                    cmd.Parameters.AddWithValue("@Stock", nMateriales.Stock);
                    cmd.Parameters.AddWithValue("@Estado", nMateriales.Estado);
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

        public bool EliminarMat(int ID_Materiales)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarMaterial", conexion);
                    cmd.Parameters.AddWithValue("@ID_Materiales", ID_Materiales); 
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

        public bool EditarMat(Materiales nMateriales)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarMateriales", conexion);
                    cmd.Parameters.AddWithValue("@ID_Materiales", nMateriales.ID_Materiales); 
                    cmd.Parameters.AddWithValue("@Nombre", nMateriales.Nombre); 
                    cmd.Parameters.AddWithValue("@Stock", nMateriales.Stock);
                    cmd.Parameters.AddWithValue("@Estado", nMateriales.Estado);
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
