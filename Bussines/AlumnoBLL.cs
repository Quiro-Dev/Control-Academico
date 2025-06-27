
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: AlumnoBLL.cs
// Descripción: Contiene todas las validaciones de las operaciones SQL (CRUD) de la entidad alumno
// ================================


using ControlAcademico.Data;
using ControlAcademico.Models;

namespace ControlAcademico.Bussines
{
    public class AlumnoBLL
    {
        private readonly AlumnoDAL _alumnoDAL = new();

        //Define la lógica para el método de obtener alumnos
        //En este caso no se requiere ninguna validación, asi que solo devuelve el método de obtenerAlumnos
        public List<Alumno> ObtenerAlumnos()
        {
            return _alumnoDAL.ObtenerAlumnos();
        }

        //Define la lógica para insertar un alumno
        public bool InsertarAlumno(Alumno alumno)
        {
            //Verifica si un alumno ya está registrado con los mismos datos ingresados
            if (_alumnoDAL.AlumnoDuplicado(alumno))
            {
                Console.WriteLine("El Nombre, Email o Teléfono ya están registrados");
                return false;
            }
            //Valida el campo nombre, para que no sea nulo y el campo fechaNacimiento para una fecha valida
            if (string.IsNullOrWhiteSpace(alumno.Nombre) ||
                alumno.FechaNacimiento == DateTime.MinValue)
            {
                Console.WriteLine("Datos inválidos. No se puede insertar");
                return false;
            }
            //Si aprueba las condiciones, retorna el método e inserta el alumno
            return _alumnoDAL.InsertarAlumno(alumno);
        }

        //Define la lógica para actualizar un alumno
        public bool ActualizarAlumno(Alumno alumno)
        {
            //Verifica la existencia del ID ingresado
            if (!_alumnoDAL.AlumnoExistente(alumno.Id))
            {
                //En caso de no ser encontrado el ID retorna el mensaje
                Console.WriteLine("Este ID no existe");
                return false;
            }

            //Valida que el ID, Nombre y fechaNacimiento sean datos validos y lógicos
            if (alumno.Id <= 0 ||
                string.IsNullOrWhiteSpace(alumno.Nombre) ||
                alumno.FechaNacimiento == DateTime.MinValue)
            {
                Console.WriteLine("Datos inválidos para actualizar");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y actualiza el alumno
            return _alumnoDAL.ActualizarAlumno(alumno);
        }

        //Define la lógica para eliminar un alumno
        public bool EliminarAlumno(int id)
        {
            //Verifica que el ID no sea menor a 0, ya que no existe
            if(id <= 0)
            {
                Console.WriteLine("ID Invalido para eliminar");
                return false ;
            }

            //Verifica la existencia del ID ingresado
            if (!_alumnoDAL.AlumnoExistente(id))
            {
                Console.WriteLine("Este ID no existe");
                return false;
            }

            //Verifica si el alumno tiene matriculas registradas
            if (_alumnoDAL.TieneMatriculas(id))
            {
                //En caso de tener una o más matrículas, no permitirá su eliminación  
                Console.WriteLine("No se puede eliminar porque el alumno tiene matrículas registradas");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y elimina el alumno
            return _alumnoDAL.EliminarAlumno(id);
        }
    }
}
