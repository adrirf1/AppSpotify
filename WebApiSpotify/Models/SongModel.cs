using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSpotify.Models
{
    public class SongModel
    {
        string ConnectionString = "Server=tcp:sqlsongsserver.database.windows.net,1433;Initial Catalog=sqlnjbpsongs;Persist Security Info=False;User ID=sqlnjbpsongs;Password=sqlnjbpserver_01;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Propiedades
        public int ID { get; set; }
        public string SongName { get; set; }
        public string Singer { get; set; }
        public string Picture { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        // Metodos CRUD
        public ApiResponse GetAll()
        {
            List<SongModel> songsList = new List<SongModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Song ";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        using (SqlDataReader lector = cmd.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                songsList.Add(new SongModel
                                {
                                    ID = (int)lector["IDSong"],
                                    SongName = lector["SongName"].ToString(),
                                    Singer = lector["Singer"].ToString(),
                                    Picture = lector["Picture"].ToString(),
                                    Latitude = (double)lector["Latitude"],
                                    Longitude = (double)lector["Longitude"]
                                });
                            }
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Las canciones fueron obtenidas exitosamente",
                    Result = songsList
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se genero un error al obtener las canciones: {exc.Message}",
                };
            }
        }

        public ApiResponse Get(int id)
        {
            SongModel model = new SongModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Song WHERE IDSong = @ID";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataReader lector = cmd.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                model = new SongModel()
                                {
                                    ID = int.Parse(lector["IDSong"].ToString()),
                                    SongName = lector["SongName"].ToString(),
                                    Singer = lector["Singer"].ToString(),
                                    Picture = lector["Picture"].ToString(),
                                    Latitude = double.Parse(lector["Latitude"].ToString()),
                                    Longitude = double.Parse(lector["Longitude"].ToString())
                                };
                            }
                        }
                    }
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Se obtuvo la cancion exitosamente",
                    Result = model
                };

            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se genero un error al obtener la cancion: {exc.Message}"
                };
            }
        }

        public ApiResponse Add(SongModel model)
        {
            try
            {
                object newSong;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "INSERT INTO Song " +
                                  "(SongName, " +
                                  "Singer, " +
                                  "Picture, " +
                                  "Latitude, " +
                                  "Longitude) " +
                                  "VALUES " +
                                  "(@SongName, " +
                                  "@Singer, " +
                                  "@Picture, " +
                                  "@Latitude, " +
                                  "@Longitude);" +
                                  "SELECT SCOPE_IDENTITY();";
                    using(SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@SongName", model.SongName);
                        cmd.Parameters.AddWithValue("@Singer", model.Singer);
                        cmd.Parameters.AddWithValue("@Picture", model.Picture);
                        cmd.Parameters.AddWithValue("@Latitude", model.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", model.Longitude);
                        newSong = cmd.ExecuteScalar();
                        if (newSong !=null && newSong.ToString().Length > 0)
                        {
                            return new ApiResponse
                            {
                                IsSuccess = true,
                                Message = "La cancion fue añadida exitosamente",
                                Result = newSong
                            };
                            
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "No se",
                    Result = 0

                };
                
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se genero un error al añadir la cancion: {exc.Message}"
                };
            }
        }

        public ApiResponse Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "DELETE FROM Song WHERE IDSong = @IDSong";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@IDSong", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "La cancion se ha eliminado exitosamente",
                    Result = id
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se ha generado un error al eliminar la cancion: {exc.Message}"
                };
            }
        }

        public ApiResponse Update(SongModel model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "UPDATE Song SET SongName = @SongName, Singer = @Singer, Picture = @Picture, Latitude = @Latitude, Longitude = @Longitude " +
                                  "WHERE IDSong = @IDSong";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@SongName", model.SongName);
                        cmd.Parameters.AddWithValue("@Singer", model.Singer);
                        cmd.Parameters.AddWithValue("@Picture", model.Picture);
                        cmd.Parameters.AddWithValue("@Latitude", model.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", model.Longitude);
                        cmd.Parameters.AddWithValue("@IDSong", model.ID);
                        cmd.ExecuteNonQuery();
                    }
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "La cancion fue actualizada exitosamente",
                    Result = model.ID
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se genero un error al actualizar la cancion: {exc.Message}"
                };
            }
        }
    }
}
