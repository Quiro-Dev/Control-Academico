
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: MatriculaDAL.cs 
// Descripción: Contiene todas las operaciones SQL (CRUD) para la entidad Matricula
// ================================


using ControlAcademico.Models;
using Microsoft.Data.SqlClient;

namespace ControlAcademico.Data
{
    //Clase responsable de realizar todas las operaciones SQL relacionadas con la tabla Matriculas
    public class MatriculaDAL
    {
        //Creamos una conexión privada proveniente de Conexion.cs
        private readonly Conexion _conexion;

        public MatriculaDAL()
        {
            _conexion = new Conexion();
        }

        //Método para obtener las Matriculas
        public List<Matricula> ObtenerMatriculas()
        {
            //Lista para almacenamiento de las matriculas
            List<Matricula> lista = new();
            //Obtiene la cadena de conexión de la base de datos
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consulta para obtener todas las matriculas
                    string query = "SELECT * FROM Matriculas;";
                    SqlCommand comando = new(query, conexion);
                    SqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        //Se crea un objeto matricula de la clase principal Matricula
                        //Donde se almacenan los datos de cada matricula y se hace la respectiva conversión de datos
                        Matricula matricula = new Matricula
                        {
                            Id = Convert.ToInt32(lector["Id"]),
                            IdAlumno = Convert.ToInt32(lector["IdAlumno"]),
                            IdCurso = Convert.ToInt32(lector["IdCurso"]),
                            Año = Convert.ToInt32(lector["Año"]),
                            IdProfesor = Convert.ToInt32(lector["IdProfesor"]),
                        };
                        //Agregamos a la lista cada matricula obtenida
                        lista.Add(matricula);
                    }
                }
                //En caso de errores atrapamos la excepción SQL y mostramos el mensaje de error
                catch(SqlException error)
                {
                    Console.WriteLine($"Error al obtener matriculas: {error.Message}");
                }
            }
            return lista;
        }

        //Método para insertar una matricula
        public bool InsertarMatricula(Matricula matricula)
        {
            using(SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consulta SQL para insertar dentro de Matriculas
                    //Contiene los parámetros (@) para evitar Inyecciones SQL
                    string query = @"INSERT INTO Matriculas (IdAlumno, IdCurso, Año, IdProfesor)
                                     VALUES (@IdAlumno, @IdCurso, @Año, @IdProfesor);";
                    SqlCommand comando = new(query, conexion);
                    //Para cada parámetro se le asigna el valor correspondiente al objeto
                    comando.Parameters.AddWithValue("@IdAlumno", matricula.IdAlumno);
                    comando.Parameters.AddWithValue("@IdCurso", matricula.IdCurso);
                    comando.Parameters.AddWithValue("@Año", matricula.Año);
                    comando.Parameters.AddWithValue("@IdProfesor", matricula.IdProfesor);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (SqlException error)
                { 
                    Console.WriteLine($"Error al Insertar: {error.Message}");
                    return false;
                }
            }
        }

        //Método para actualizar una matricula
        public bool ActualizarMatricula(Matricula matricula)
        {
            //Se realiza el mismo procedimiento que en InsertarMatricula
            //Cambia la consulta SQL pidiendo el ID de la matricula a actualizar con parámetros
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = @"UPDATE Matriculas SET 
                                     IdAlumno = @IdAlumno,
                                     IdCurso = @IdCurso,
                                     Año = @Año,
                                     IdProfesor = @IdProfesor
                                     WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);
                    comando.Parameters.AddWithValue("@IdAlumno", matricula.IdAlumno);
                    comando.Parameters.AddWithValue("@IdCurso",matricula.IdCurso);
                    comando.Parameters.AddWithValue("@Año",matricula.Año);
                    comando.Parameters.AddWithValue("@IdProfesor",matricula.IdProfesor);
                    comando.Parameters.AddWithValue("@Id",matricula.Id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch(SqlException error)
                {
                    Console.WriteLine($"Error al Actualizar: {error.Message}");
                    return false;
                }
            }
        }

        //Método para eliminar una matricula
        public bool EliminarMatricula(int id)
        {
            //Se realiza el mismo procedimiento de los anteriores métodos
            //Cambiamos la consulta para pedir solo el ID de la matricula a eliminar
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "DELETE FROM Matriculas WHERE Id = @Id;";
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

        //Método para verificar matriculas existentes
        //Verifica si ya existe una matricula con el ID ingresado
        public bool MatriculaExistente(int id)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Se consulta si hay una matricula con el ID ingresado por el usuario
                    string query = "SELECT COUNT(*) FROM Matriculas WHERE Id = @Id;";
                    SqlCommand comando = new(query, conexion);

                    comando.Parameters.AddWithValue("@Id", id);
                    int filasEncontradas = Convert.ToInt32(comando.ExecuteScalar());
                    return filasEncontradas > 0;
                }
                catch (SqlException error)
                {
                    Console.WriteLine($"Error al verificar existencia: {error.Message}");
                    return false;
                }
            }
        }

        //Método para verificar matriculas duplicadas
        //Verifica si ya existe una matricula con los mismos datos para evitar duplicados al insertar
        public bool MatriculaDuplicada(Matricula matricula)
        {
            using (SqlConnection conexion = _conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    //Consultamos si hay una matricula con el mismo alumno, curso y año de matricula
                    string query = @"SELECT COUNT(*) FROM Matriculas WHERE 
                                     IdAlumno = @IdAlumno AND
                                     IdCurso = @IdCurso AND
                                     Año = @Año;";
                    SqlCommand comando = new(query, conexion);

                    comando.Parameters.AddWithValue("@Id", matricula.Id);
                    comando.Parameters.AddWithValue("@IdAlumno", matricula.IdAlumno);
                    comando.Parameters.AddWithValue("@IdCurso", matricula.IdCurso);
                    comando.Parameters.AddWithValue("@Año", matricula.Año);
                    comando.Parameters.AddWithValue("@IdProfesor", matricula.IdProfesor);

                    int filasEncontradas = Convert.ToInt32(comando.ExecuteScalar());
                    return filasEncontradas > 0;
                }
                catch(SqlException error)
                {
                    Console.WriteLine($"Error al verificar duplicado: {error.Message}");
                    return false;
                }
            }
        }

    }
}

