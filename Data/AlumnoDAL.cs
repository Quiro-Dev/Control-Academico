
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: AlumnoDAL.cs
// Descripción: Contiene todas las operaciones SQL (CRUD) para la entidad Alumno
// ================================


using ControlAcademico.Models;
using Microsoft.Data.SqlClient;


namespace ControlAcademico.Data
{
    //Clase responsable de realizar todas las operaciones SQL relacionadas con la tabla Alumnos
    public class AlumnoDAL
    {
        //Creamos una conexión privada proveniente de Conexion.cs
        private readonly Conexion _conexion;

        public AlumnoDAL()
        {
            _conexion = new Conexion();
        }
        //Método para obtener los alumnos
        public List<Alumno> ObtenerAlumnos()
        {
            //Lista para almacenamiento de los alumnos
            List<Alumno> lista = new();
            //Obtiene la cadena de conexión de la base de datos
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            { 
                try
                {
                    conexion.Open();
                    //Consulta para obtener todos los alumnos
                    string query = "SELECT * FROM Alumnos"; 
                    SqlCommand comando = new(query, conexion);
                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        //Se crea un objeto alumno de la Clase principal Alumno
                        //Donde se almacenan los datos de cada alumno y se hace la respectiva conversión de datos
                        Alumno alumno = new Alumno
                        {
                            Id = Convert.ToInt32(lector["Id"]),
                            Nombre = lector["Nombre"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(lector["FechaNacimiento"]),
                            Email = lector["Email"].ToString(),
                            Telefono = lector["Telefono"].ToString()
                        };
                        //Agregamos a la lista cada alumno obtenido
                        lista.Add(alumno);
                    }
                }
                //En caso de errores atrapamos la excepción SQL y mostramos el mensaje del error
                catch (SqlException error)
                {
                    Console.WriteLine("Error al conectar: " + error.Message);
                }
            }
            return lista;
        }

        //Método para Insertar un Alumno
        public bool InsertarAlumno(Alumno alumno)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consulta SQL para insertar dentro de alumnos
                    //Contiene los parámetros(@) para evitar Inyecciones SQL
                    string query = @"INSERT INTO Alumnos (Nombre, FechaNacimiento, Email, Telefono) 
                                     VALUES(@Nombre, @FechaNacimiento, @Email, @Telefono)";

                    SqlCommand comando = new(query, conexion);
                    //Para cada parámetro se le asigna el valor correspondiente al objeto alumno
                    comando.Parameters.AddWithValue("@Nombre",alumno.Nombre);
                    comando.Parameters.AddWithValue("@FechaNacimiento",alumno.FechaNacimiento);
                    comando.Parameters.AddWithValue("@Email",alumno.Email);
                    comando.Parameters.AddWithValue("@Telefono",alumno.Telefono);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0;

                }
                catch(SqlException error)
                {
                    Console.WriteLine($"Error al insertar alumno: {error.Message}");
                    return false;
                }
            }
        }

        //Método para actualizar un alumno
        public bool ActualizarAlumno(Alumno alumno)
        {
            //Se realiza el mismo procedimiento que en InsertarAlumno
            //Cambia la consulta SQL pidiendo el ID del alumno a actualizar con parámetros
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Alumnos SET 
                                     Nombre = @Nombre,
                                     FechaNacimiento = @FechaNacimiento,
                                     Email = @Email, 
                                     Telefono = @Telefono
                                     WHERE Id = @Id";

                    SqlCommand comando = new(query, conexion);
                    comando.Parameters.AddWithValue("@Nombre",alumno.Nombre);
                    comando.Parameters.AddWithValue("@FechaNacimiento",alumno.FechaNacimiento);
                    comando.Parameters.AddWithValue("@Email",alumno.Email);
                    comando.Parameters.AddWithValue("@Telefono",alumno.Telefono);
                    comando.Parameters.AddWithValue("@Id",alumno.Id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al actualizar alumno: {error.Message}");
                    return false;
                }
            }
        }

        //Método para eliminar alumno
        public bool EliminarAlumno(int id)
        {
            //Se realiza el mismo procedimiento de los anteriores métodos
            //Cambiamos la consulta para pedir solo el ID del alumno a eliminar
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "DELETE FROM Alumnos WHERE Id= @Id";
                    SqlCommand comando = new(query, conexion);

                    comando.Parameters.AddWithValue("Id", id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch(SqlException error)
                {
                    Console.WriteLine($"Error al eliminar: {error.Message}");
                    return false;
                }
            }
        }

        //Método para verificar alumnos existentes
        //Verifica si ya existe un alumno con el ID ingresado
        public bool AlumnoExistente(int id)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Se consulta si hay un alumno con el ID ingresado por el usuario
                    string query = "SELECT COUNT(*) FROM Alumnos WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);
                    comando.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = Convert.ToInt32(comando.ExecuteScalar());
                    return filasAfectadas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al verificar existencia: {error.Message}");
                    return false;
                }
            }
        }

        //Método para verificar alumnos duplicados 
        //Verifica si ya existe un alumno con los mismos datos para evitar duplicados al insertar
        public bool AlumnoDuplicado(Alumno alumno)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consultamos si hay un alumno con el mismo Nombre, Email y Teléfono 
                    string query = @"SELECT COUNT(*) FROM Alumnos WHERE
                                     Nombre = @Nombre AND
                                     Email = @Email AND
                                     Telefono = @Telefono";
                    SqlCommand comando = new(query, conexion);
                    
                    comando.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                    comando.Parameters.AddWithValue("@Email", alumno.Email);
                    comando.Parameters.AddWithValue("@Telefono", alumno.Telefono);

                    int filasEncontradas = Convert.ToInt32(comando.ExecuteScalar());
                    return filasEncontradas > 0;

                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al verificar duplicado: {error.Message}");
                    return false;
                }
            }
        }

        //Método para verificar si un alumno tiene Matriculas
        //Verifica si un alumno cuenta con matriculas registradas
        public bool TieneMatriculas(int idAlumno)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consultamos la cuenta de matriculas registradas por el alumno según su ID
                    string query = "SELECT COUNT(*) FROM Matriculas WHERE IdAlumno = @IdAlumno;";
                    SqlCommand comando = new(query, conexion);
                    comando.Parameters.AddWithValue("@IdAlumno", idAlumno);

                    int filasEncontradas = Convert.ToInt32(comando.ExecuteScalar());
                    return filasEncontradas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al verificar matriculas del alumno: {error.Message}");
                    return false;
                }
            }
        }
    }
}