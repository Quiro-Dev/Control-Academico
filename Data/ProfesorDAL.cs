
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: ProfesorDAL.cs
// Descripción: Contiene todas las operaciones SQL (CRUD) para la entidad Profesor
// ================================


using ControlAcademico.Models;
using Microsoft.Data.SqlClient;

namespace ControlAcademico.Data
{
    //Clase responsable de realizar todas las operaciones SQL relacionadas con la tabla Profesores
    public class ProfesorDAL
    {
        //Creamos una conexión privada proveniente de Conexion.cs
        private readonly Conexion _conexion;

        public ProfesorDAL()
        {
            _conexion = new Conexion(); 
        }

        //Método para obtener los profesores
        public List<Profesor> ObtenerProfesores()
        {
            //Lista de almacenamiento de los profesores
            List<Profesor> lista = new();
            //Obtiene la cadena de conexión de la base de datos
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consulta para obtener todos los profesores
                    string query = "SELECT * FROM Profesores;";
                    SqlCommand comando = new(query, conexion);
                    SqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        //Se crea un objeto profesor de la clase principal Profesor
                        //Donde se alamcenan los datos de cada profesor y se hace la respectiva conversión de datos
                        Profesor profesor = new Profesor
                        {
                            Id = Convert.ToInt32(lector["Id"]),
                            Nombre = lector["Nombre"].ToString(),
                            Especialidad = lector["Especialidad"].ToString(),
                            Email = lector["Email"].ToString()
                        };
                        //Agregamos a la lista cada profesor obtenido
                        lista.Add(profesor);
                    }
                }
                //En caso de errores atrapamos la excepción SQL y mostramos el mensaje del error
                catch(SqlException error)
                {
                    Console.WriteLine($"Error al obtener profesores: {error.Message}");
                }
            }
            return lista;
        }

        //Método para Insertar un Profesor
        public bool InsertarProfesor(Profesor profesor)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consulta SQL para insertar dentro de profesores
                    //Contiene los parámetros(@) para evitar Inyecciones SQL
                    string query = @"INSERT INTO Profesores (Nombre, Especialidad, Email)
                                     VALUES (@Nombre, @Especialidad, @Email)";
                    SqlCommand comando = new(query, conexion);
                    //Para cada parámetro se le asigna el valor correspondiente al objeto profesor
                    comando.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                    comando.Parameters.AddWithValue("@Especialidad", profesor.Especialidad);
                    comando.Parameters.AddWithValue("@Email", profesor.Email);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al insertar: {error.Message}");
                    return false;
                }
            }
        }

        //Método para actualizar un profesor
        public bool ActualizarProfesor(Profesor profesor)
        {
            //Se realiza el mismo procedimiento que en InsertarProfesor
            //Cambia la consulta SQL pidiendo el ID del profesor a actualizar con parámetros
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Profesores SET
                                     Nombre = @Nombre,
                                     Especialidad = @Especialidad,
                                     Email = @Email
                                     WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);

                    comando.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                    comando.Parameters.AddWithValue("@Especialidad", profesor.Especialidad);
                    comando.Parameters.AddWithValue("@Email", profesor.Email);
                    comando.Parameters.AddWithValue("@Id", profesor.Id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"{error.Message}");
                    return false;
                }
            }
        }

        //Método para eliminar un profesor
        public bool EliminarProfesor(int id)
        {
            //Se realiza el mismo procedimiento de los anteriores métodos
            //Cambiamos la consulta para pedir solo el ID del profesor a eliminar
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "DELETE FROM Profesores WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);
                    comando.Parameters.AddWithValue("@Id", id);

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

        //Método para verificar profesores existentes
        //Verifica si ya existe un profesor con ese ID
        public bool ProfesorExistente(int id)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Se consulta si hay un profesor con el ID ingresado por el usuario
                    string query = "SELECT COUNT(*) FROM Profesores WHERE Id = @Id;";
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

        //Método para verificar profesores duplicados
        //Verifica si ya existe un profesor con los mismos datos para evitar duplicaods
        public bool ProfesorDuplicado(Profesor profesor)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consultamos si hay un profesor con el mismo Nombre y Email
                    string query = @"SELECT COUNT(*) FROM Profesores WHERE
                                     Nombre = @Nombre AND
                                     Email = @Email;";
                    SqlCommand comando = new(query, conexion);

                    comando.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                    comando.Parameters.AddWithValue("@Email", profesor.Email);

                    int filasEncontradas = Convert.ToInt32(comando.ExecuteScalar());
                    return filasEncontradas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al verificar duplicados: {error.Message}");
                    return false;
                }
            }
        }
    }
}
