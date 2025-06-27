
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: CursoBLL.cs 
// Descripción: Contiene todas las validaciones de las operaciones SQL (CRUD) de la entidad Curso
// ================================


using ControlAcademico.Data;
using ControlAcademico.Models;

namespace ControlAcademico.Bussines
{
    public class CursoBLL
    {
        private readonly CursoDAL _cursoDAL = new();

        //Define la lógica para el método de obtener cursos
        //En este caso no se requiere ninguna validación, asi que solo devuelve el método de obtenerCursos
        public List<Curso> ObtenerCursos()
        {
            return _cursoDAL.ObtenerCursos();
        }

        //Define la lógica para insertar un curso
        public bool InsertarCurso(Curso curso)
        {
            //Verifica si un curso ya está registrado con los mismos datos ingresados
            if (_cursoDAL.CursoDuplicado(curso))
            {
                Console.WriteLine("Este Curso ya existe");
                return false;
            }
            //Valida ell campo de NombreCurso para que no sea nulo,
            //el campo DuracionMeses para que no sea menor a 0 y
            //el campo Nivel, para que tampoco sea nulo
            if(string.IsNullOrWhiteSpace(curso.NombreCurso) || curso.DuracionMeses <= 0 || string.IsNullOrWhiteSpace(curso.Nivel))
            {
                Console.WriteLine("No se pudo Insertar el curso");
                return false;
            }
            //Si aprueba las condiciones, retorna el metodo para insertar el curso
            return _cursoDAL.InsertarCurso(curso);
        }

        //Define la lógica para actualizar un curso
        public bool ActualizarCurso(Curso curso)
        {
            //Verifica la existencia del ID ingresado
            if (!_cursoDAL.CursoExistente(curso.Id))
            {
                //En caso de no ser encontrado el ID retorna el mensaje
                Console.WriteLine("No existe un curso con ese ID");
                return false;
            }
            //Valida que el ID, NombreCurso,DuracionMeses y Nivel sean datos validos y lógicos
            if (curso.Id <= 0 || string.IsNullOrWhiteSpace(curso.NombreCurso) || curso.DuracionMeses <= 0 || string.IsNullOrWhiteSpace(curso.Nivel))
            {
                Console.WriteLine("No se pudo Actualizar el curso");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y actualiza el curso
            return _cursoDAL.ActualizarCurso(curso);
        }

        //Define la lógica para eliminar un curso
        public bool EliminarCurso(int id)
        {
            //Valida que el ID no sea menor a 0, ya que no existe
            if (id <= 0)
            {
                Console.WriteLine("ID Invalido");
                return false;
            }
            //Valida la existencia del ID ingresado
            if (!_cursoDAL.CursoExistente(id))
            {
                Console.WriteLine("No existe un curso con ese ID");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y elimina el alumno
            return _cursoDAL.EliminarCurso(id);
        }
    }
}
