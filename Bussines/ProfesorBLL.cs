
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: ProfesorBLL.cs
// Descripción: Contiene todas las validaciones de las operaciones SQL (CRUD) de la entidad Profesor
// ================================


using ControlAcademico.Data;
using ControlAcademico.Models;

namespace ControlAcademico.Bussines
{
    public class ProfesorBLL
    {
        private readonly ProfesorDAL _profesorDAL = new();

        //Define la lógica para el método de obtener profesores
        //En este caso no se requiere ninguna validación, asi que solo devuelve el método
        public List<Profesor> ObtenerProfesores()
        {
            return _profesorDAL.ObtenerProfesores();
        }

        //Define al lógica para insertar un profesor
        public bool InsertarProfesor(Profesor profesor)
        {
            //Verifica si un profesor ya está registrado con los mismos datos ingresados
            if (_profesorDAL.ProfesorDuplicado(profesor))
            {
                Console.WriteLine("Este Profesor ya existe");
                return false;
            }
            //Valida los campos Nombre,Especialidad y Email para que ninguno sea nulo
            if (string.IsNullOrWhiteSpace(profesor.Nombre) ||
                string.IsNullOrWhiteSpace(profesor.Especialidad) ||
                string.IsNullOrWhiteSpace(profesor.Email))
            {
                Console.WriteLine("No se pudo insertar el profesor");
                return false;
            }
            //Si aprueba las condiciones, retorna el método e inserta el profesor
            return _profesorDAL.InsertarProfesor(profesor);
        }

        //Define la lógica para actualizar un profesor
        public bool ActualizarProfesor(Profesor profesor)
        {
            //Valida que el ID sea valido
            if (profesor.Id <= 0 || !_profesorDAL.ProfesorExistente(profesor.Id) )

            {
                Console.WriteLine("Este ID no existe o es invalido");
                return false;
            }
            //Verifica que los campos Nombre,Especialidad y Email no sean nulos
            if (string.IsNullOrWhiteSpace(profesor.Nombre) ||
                string.IsNullOrWhiteSpace(profesor.Especialidad) ||
                string.IsNullOrWhiteSpace(profesor.Email))
            {
                Console.WriteLine("No se pudo Actualizar el profesor");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y actualiza el profesor
            return _profesorDAL.ActualizarProfesor(profesor);
        }

        public bool EliminarProfesor(int id)
        {
            //Valida la existencia del ID ingresado
            if (id <= 0 || !_profesorDAL.ProfesorExistente(id) )
            {
                Console.WriteLine("Este ID no existe o es invalido");
                return false;
            }
            //Si aprueba las condiciones, retorna el método y elimina el profesor
            return _profesorDAL.EliminarProfesor(id);
        }
    }
}
