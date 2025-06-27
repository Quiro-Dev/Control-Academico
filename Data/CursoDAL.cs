
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: CursoDAL.cs
// Descripción: Contiene todas las operaciones SQL (CRUD) para la entidad Curso
// ================================

using ControlAcademico.Models;
using Microsoft.Data.SqlClient;


namespace ControlAcademico.Data
{
    //Clase responsable de realizar todas las operaciones SQL relacionadas con la tabla Cursos
    public class CursoDAL
    {
        //Creamos una conexión privada proveniente de Conexion.cs
        private readonly Conexion _conexion;

        public CursoDAL()
        {
            _conexion = new Conexion();
        }
        //Método para obtener los Cursos
        public List<Curso> ObtenerCursos()
        {
            //Lista para almacenamiento de los cursos
            List<Curso> lista = new();
            //Obtiene la cadena de conexión de la base de datos
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consulta para obtener todos los cursos
                    string query = "SELECT * FROM Cursos;";
                    SqlCommand comando = new(query, conexion);
                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        //Se crea un objeto curso de la clase principal Curso
                        //Donde se almacenan los datos de cada curso y se hace la respectiva conversión de datos
                        Curso curso = new Curso
                        {
                            Id = Convert.ToInt32(lector["Id"]),
                            NombreCurso = lector["NombreCurso"].ToString(),
                            DuracionMeses = Convert.ToInt32(lector["DuracionMeses"]),
                            Nivel = lector["Nivel"].ToString(),
                        };
                        //Agregamos a lista cada curso obtenido
                        lista.Add(curso);
                    }

                }
                //En caso de errores, atrapamos la excepción SQL y mostramos el mensaje del error
                catch(SqlException error)
                {
                    Console.WriteLine($"Error en la BD{error.Message}");
                }
            }
            return lista;
        }
        //Método para Insertar un Curso
        public bool InsertarCurso(Curso curso)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consulta SQL para insertar dentro de Cursos
                    //Contiene los parámetros(@) para evitar Inyecciones SQL
                    string query = @"INSERT INTO Cursos (NombreCurso, DuracionMeses, Nivel) 
                                     VALUES (@NombreCurso, @DuracionMeses, @Nivel) ";
                    SqlCommand comando = new(query, conexion);
                    //Para cada parámetro se le asigna el valor correspondiente al objeto curso
                    comando.Parameters.AddWithValue ("@NombreCurso", curso.NombreCurso);
                    comando.Parameters.AddWithValue ("@DuracionMeses", curso.DuracionMeses);
                    comando.Parameters.AddWithValue ("@Nivel", curso.Nivel);

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

        //Método para Actualizar un curso
        public bool ActualizarCurso(Curso curso)
        {
            //Se realiza el mismo procedimiento que en InsertarCurso
            //Cambia la consulta SQL pidiendo el ID del curso a actualizar con parámetros
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Cursos SET
                                    NombreCurso = @NombreCurso, 
                                    DuracionMeses = @DuracionMeses, 
                                    Nivel = @Nivel
                                    WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);
                    comando.Parameters.AddWithValue("@NombreCurso", curso.NombreCurso);
                    comando.Parameters.AddWithValue("@DuracionMeses", curso.DuracionMeses);
                    comando.Parameters.AddWithValue("@Nivel", curso.Nivel);
                    comando.Parameters.AddWithValue("@Id", curso.Id);

                    int filasAfectadas = comando.ExecuteNonQuery ();
                    return filasAfectadas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al actualizar: {error.Message}");
                    return false;
                }
            }
        }

        //Método para eliminar curso
        public bool EliminarCurso(int id)
        {
            //Se realiza el mismo procedimiento de los anteriores métodos
            //Cambiamos la consulta para pedir solo el ID del curso a eliminar
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "DELETE FROM Cursos WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);

                    comando.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al eliminar: {error.Message}");
                    return false;
                }
            }
        }

        //Método para verificar cursos existentes
        //Verifica si ya existe un curso con el ID ingresado
        public bool CursoExistente(int id)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Se consulta si hay un curso con el ID ingresado por el usuario
                    string query = "SELECT COUNT(*) FROM Cursos WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);
                    comando.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = Convert.ToInt32(comando.ExecuteScalar());
                    return filasAfectadas > 0;
                }
                catch(SqlException error)
                {
                    Console.WriteLine($"Error al verificar existencia: {error.Message}");
                    return false;
                }
            }
        }

        //Método para verificar cursos duplicados 
        //Verifica si ya existe un curso con los mismos datos para evitar duplicados al insertar
        public bool CursoDuplicado(Curso curso)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consultamos si hay un curso con el mismo Nombre y Nivel
                    string query = @"SELECT COUNT(*) FROM Cursos WHERE
                                    NombreCurso = @NombreCurso AND
                                    Nivel = @Nivel;";
                    SqlCommand comando = new(query, conexion);

                    comando.Parameters.AddWithValue("@NombreCurso", curso.NombreCurso);
                    comando.Parameters.AddWithValue("@Nivel", curso.Nivel);

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
