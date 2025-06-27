
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: MatriculaBLL.cs
// Descripción: Contiene todas las validaciones de las operaciones SQL (CRUD) de la entidad Matricula
// ================================


using ControlAcademico.Data;
using ControlAcademico.Models;

namespace ControlAcademico.Bussines
{
    public class MatriculaBLL
    {
        private readonly MatriculaDAL _matriculaDAL = new();

        //Define la lógica para el método de obtener matriculas
        //En este caso no se requiere ninguna verificación, asi que solo devuelve el método
        public List<Matricula> ObtenerMatriculas()
        {
            return _matriculaDAL.ObtenerMatriculas();
        }

        //Define la lógica para insertar una matricula
        public bool InsertarMatricula(Matricula matricula)
        {
            //Verifica si la matricula ya está registrada con los mismos datos ingresados
            if (_matriculaDAL.MatriculaDuplicada(matricula))
            {
                Console.WriteLine("Ya existe una matricula igual registrada");
                return false;
            }
            //Valida que los ID no sean menores o iguales a 0, ya que no existen
            //Además de la valides de la fecha, para que no sea menor que 2020 ni mayor al año actual
            if(matricula.IdAlumno <= 0 ||
               matricula.IdCurso <= 0 ||
               matricula.Año < 2020 ||
               matricula.Año > DateTime.Now.Year || 
               matricula.IdProfesor <= 0)
            {
                Console.WriteLine("Datos inválidos");
                return false;
            }
            //Si aprueba las condiciones, retorna el método e inserta la matricula
            return _matriculaDAL.InsertarMatricula(matricula);
        }

        //Define la lógica para actualizar una matricula
        public bool ActualizarMatricula(Matricula matricula)
        {
            //Valida la existencia del ID ingresado
            if (!_matriculaDAL.MatriculaExistente(matricula.Id))
            {
                //En caso de no encontrarlo retorna el mensaje
                Console.WriteLine("Este ID no existe");
                return false;
            }
            //Valida que los ID no sean menores o iguales a 0, ya que no existen
            //Además de la valides de la fecha, para que no sea menor que 2020 ni mayor al año actual
            if (matricula.IdAlumno <= 0 ||
               matricula.IdCurso <= 0 ||
               matricula.Año < 2020 ||
               matricula.Año > DateTime.Now.Year ||
               matricula.IdProfesor <= 0)
            {
                Console.WriteLine("Datos inválidos para actualizar");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y actualiza la matricula
            return _matriculaDAL.ActualizarMatricula(matricula);
        }

        //Define la lógica para eliminar una matricula 
        public bool EliminarMatricula(int id)
        {
            //Verifica que el ID no sea menor o igual a 0, ya que no existe
            if(id <= 0)
            {
                Console.WriteLine("ID invalido para eliminar");
            }
            //Verifica la existencia de la matricula
            if (!_matriculaDAL.MatriculaExistente(id) )
            {
                Console.WriteLine("Este ID no existe");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y elimina la matricula
            return _matriculaDAL.EliminarMatricula(id);
        }
    }
}
