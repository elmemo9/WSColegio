using System.Data.SqlClient;
using WSColegio.Common;

namespace WSColegio.Models
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        //Begins Alumno's Methods
        public int AddAlumno(Alumno alumno)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "INSERT INTO alumno(Nombre, Apellidos, Genero, FechaNacimiento) values (@nombre, @apellidos, @genero, @fechanacimiento); SELECT CAST(scope_identity() AS int);";
                        command.Parameters.AddWithValue("@nombre", alumno.Nombre);
                        command.Parameters.AddWithValue("@apellidos", alumno.Apellidos);
                        command.Parameters.AddWithValue("@genero", alumno.Genero);
                        command.Parameters.AddWithValue("@fechanacimiento", alumno.FechaNacimiento);
                        int result = (Int32)command.ExecuteScalar();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;

            }

        }

        public bool AlumnoExists(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT Id FROM alumno WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reader.Close();
                                sqlTransaction.Commit();
                                return true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
            }
            return false;
        }

        public int DeleteAlumno(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE alumno SET Deleted = 1 WHERE Id=@id; " +
                            "UPDATE gradoalumno SET Deleted = 1 WHERE AlumnoId=@id;";
                        command.Parameters.AddWithValue("@id", id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }

        public List<Alumno>? GetAlumnos(List<Filter>? filters)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            List<Alumno> alumnos = new();
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT Id, Nombre, Apellidos, Genero, FechaNacimiento FROM alumno WHERE Deleted = 0";
                        if (filters != null && filters.Count > 0)
                        {
                            int i = 0;
                            foreach (Filter filter in filters)
                            {
                                command.CommandText += string.Format(" and {0} = @filter{1}", filter.Column, i.ToString());
                                command.Parameters.AddWithValue("@filter" + i, filter.Value);
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var alumno = new Alumno()
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellidos = reader.GetString(2),
                                    Genero = reader.GetString(3),
                                    FechaNacimiento = reader.GetDateTime(4)
                                };
                                alumnos.Add(alumno);
                            }
                            reader.Close();
                            sqlTransaction.Commit();
                            connection.Close();
                            return alumnos;
                        }
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                    sqlTransaction.Rollback();
                    connection.Close();
                    return null;
                }
            }

        }

        public int UpdateAlumno(Alumno alumno)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE alumno SET Nombre=@nombre, Apellidos=@apellidos, Genero=@genero, FechaNacimiento=@fechanacimiento WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@nombre", alumno.Nombre);
                        command.Parameters.AddWithValue("@apellidos", alumno.Apellidos);
                        command.Parameters.AddWithValue("@genero", alumno.Genero);
                        command.Parameters.AddWithValue("@fechanacimiento", alumno.FechaNacimiento);
                        command.Parameters.AddWithValue("@id", alumno.Id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }
        //Ends Alumno's Methods


        //Begins GradoAlumno's Methods
        public int AddGradoAlumno(GradoAlumno gradoAlumno)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "INSERT INTO gradoalumno(alumnoid, gradoid, seccion) values (@alumnoid, @gradoid, @seccion); SELECT CAST(scope_identity() AS int);";
                        command.Parameters.AddWithValue("@alumnoid", gradoAlumno.AlumnoId);
                        command.Parameters.AddWithValue("@gradoid", gradoAlumno.GradoId);
                        command.Parameters.AddWithValue("@seccion", gradoAlumno.Seccion);
                        int result = (Int32)command.ExecuteScalar();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;

            }

        }

        public bool GradoAlumnoExists(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT Id FROM gradoalumno WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reader.Close();
                                sqlTransaction.Commit();
                                return true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
            }
            return false;
        }

        public int DeleteGradoAlumno(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE gradoalumno SET Deleted = 1 WHERE Id=@id";
                        command.Parameters.AddWithValue("@id", id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }

        public List<GradoAlumno>? GetGradoAlumnos(List<Filter>? filters)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            List<GradoAlumno> gradoAlumnos = new();
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT gradoalumno.Id, AlumnoId, CONCAT(alumno.Nombre, ' ', alumno.Apellidos) as AlumnoNombre,  GradoId, grado.Nombre, Seccion " +
                            "FROM gradoalumno JOIN alumno on gradoalumno.AlumnoId = alumno.Id JOIN grado on gradoalumno.GradoId= grado.Id WHERE gradoalumno.Deleted = 0";
                        if (filters != null && filters.Count > 0)
                        {
                            int i = 0;
                            foreach (Filter filter in filters)
                            {
                                command.CommandText += string.Format(" and {0} = @filter{1}", filter.Column, i.ToString());
                                command.Parameters.AddWithValue("@filter" + i, filter.Value);
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var gradoAlumno = new GradoAlumno()
                                {
                                    Id = reader.GetInt32(0),
                                    AlumnoId = reader.GetInt32(1),
                                    GradoId = reader.GetInt32(2),
                                    Seccion = reader.GetString(3),
                                    AlumnoNombre = reader.GetString(4),
                                    GradoNombre = reader.GetString(5)
                                };
                                gradoAlumnos.Add(gradoAlumno);
                            }
                            reader.Close();
                            sqlTransaction.Commit();
                            connection.Close();
                            return gradoAlumnos;
                        }
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                    sqlTransaction.Rollback();
                    connection.Close();
                    return null;
                }
            }

        }

        public int UpdateGradoAlumno(GradoAlumno gradoAlumno)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE gradoalumno SET AlumnoId=@alumnoid, GradoId=@gradoid, Seccion=@seccion WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@alumnoid", gradoAlumno.AlumnoId);
                        command.Parameters.AddWithValue("@gradoid", gradoAlumno.GradoId);
                        command.Parameters.AddWithValue("@seccion", gradoAlumno.Seccion);
                        command.Parameters.AddWithValue("@id", gradoAlumno.Id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }
        //Ends GradoAlumno's Methods



        //Begins Grado's Methods
        public int AddGrado(Grado grado)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "INSERT INTO grado(Nombre, ProfesorId) values (@nombre, @profesorid); SELECT CAST(scope_identity() AS int);";
                        command.Parameters.AddWithValue("@nombre", grado.Nombre);
                        command.Parameters.AddWithValue("@profesorid", grado.ProfesorId);

                        int result = (Int32)command.ExecuteScalar();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;

            }

        }

        public bool GradoExists(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT Id FROM grado WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reader.Close();
                                sqlTransaction.Commit();
                                return true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
            }
            return false;
        }

        public int DeleteGrado(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE grado SET Deleted = 1 WHERE Id=@id; " +
                            "UPDATE gradoalumno SET Deleted = 1 WHERE GradoId=@id;";
                        command.Parameters.AddWithValue("@id", id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }

        public List<Grado>? GetGrados(List<Filter>? filters)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            List<Grado> grados = new();
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT grado.Id, grado.Nombre, ProfesorId, CONCAT(profesor.nombre, ' ', profesor.apellidos) as NombreProfesor " +
                            "FROM grado JOIN profesor ON grado.ProfesorId = profesor.Id WHERE grado.Deleted = 0";
                        if (filters != null && filters.Count > 0)
                        {
                            int i = 0;
                            foreach (Filter filter in filters)
                            {
                                command.CommandText += string.Format(" and {0} = @filter{1}", filter.Column, i.ToString());
                                command.Parameters.AddWithValue("@filter" + i, filter.Value);
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var grado = new Grado()
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    ProfesorId = reader.GetInt32(2),
                                    NombreProfesor = reader.GetString(3)
                                };
                                grados.Add(grado);
                            }
                            reader.Close();
                            sqlTransaction.Commit();
                            connection.Close();
                            return grados;
                        }
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                    sqlTransaction.Rollback();
                    connection.Close();
                    return null;
                }
            }

        }

        public int UpdateGrado(Grado grado)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE grado SET Nombre=@nombre, ProfesorId=@profesorid WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@nombre", grado.Nombre);
                        command.Parameters.AddWithValue("@profesorid", grado.ProfesorId);

                        command.Parameters.AddWithValue("@id", grado.Id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }
        //Ends Grados's Methods



        //Begins Profesor's Methods
        public int AddProfesor(Profesor profesor)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "INSERT INTO profesor(Nombre, Apellidos, Genero) values (@nombre, @apellidos, @genero); SELECT CAST(scope_identity() AS int);";
                        command.Parameters.AddWithValue("@nombre", profesor.Nombre);
                        command.Parameters.AddWithValue("@apellidos", profesor.Apellidos);
                        command.Parameters.AddWithValue("@genero", profesor.Genero);
                        int result = (Int32)command.ExecuteScalar();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;

            }

        }

        public bool ProfesorExists(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT Id FROM profesor WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reader.Close();
                                sqlTransaction.Commit();
                                return true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
            }
            return false;
        }

        public int DeleteProfesor(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE profesor SET Deleted = 1 WHERE Id=@id;" +
                            "UPDATE grado SET Deleted = 1 WHERE ProfesorId=@id;" +
                            "UPDATE profesor SET profesor.Deleted = 1 " +
                            "FROM profesor JOIN grado ON profesor.Id = grado.ProfesorId  " +
                            "WHERE grado.ProfesorId =@id";
                        command.Parameters.AddWithValue("@id", id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }

        public List<Profesor>? GetProfesores(List<Filter>? filters)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            List<Profesor> profesores = new();
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "SELECT Id, Nombre, Apellidos, Genero FROM profesor WHERE Deleted = 0";
                        if (filters != null && filters.Count > 0)
                        {
                            int i = 0;
                            foreach (Filter filter in filters)
                            {
                                command.CommandText += string.Format(" and {0} = @filter{1}", filter.Column, i.ToString());
                                command.Parameters.AddWithValue("@filter" + i, filter.Value);
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var profesor = new Profesor()
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellidos = reader.GetString(2),
                                    Genero = reader.GetString(3)
                                };
                                profesores.Add(profesor);
                            }
                            reader.Close();
                            sqlTransaction.Commit();
                            connection.Close();
                            return profesores;
                        }
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                    sqlTransaction.Rollback();
                    connection.Close();
                    return null;
                }
            }

        }

        public int UpdateProfesor(Profesor profesor)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTransaction;
                        command.CommandText = "UPDATE profesor SET Nombre=@nombre, Apellidos=@apellidos, Genero=@genero WHERE Id=@id and Deleted = 0";
                        command.Parameters.AddWithValue("@nombre", profesor.Nombre);
                        command.Parameters.AddWithValue("@apellidos", profesor.Apellidos);
                        command.Parameters.AddWithValue("@genero", profesor.Genero);
                        command.Parameters.AddWithValue("@id", profesor.Id);
                        int result = command.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return result;
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                }
                return 0;
            }
        }
        //Ends Profesor's Methods

    }
}
