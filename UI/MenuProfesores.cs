
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: MenuProfesores.cs 
// Descripción: Contiene el menú de consola para la gestión de los profesores
// ================================


using ControlAcademico.Bussines;
using ControlAcademico.Models;

namespace ControlAcademico.UI
{
    public class MenuProfesores
    {

        private readonly ProfesorBLL _profesorBLL = new();

        public void Mostrar()
        {
            //Valida el estado del menú
            bool continuar = true;

            while (continuar)
            {
                //Limpiamos la consola para un menú más legible
                Console.Clear();
                //Damos las opciones para la gestión de los profesores
                Console.WriteLine("\n === MENÚ PROFESORES ===");
                Console.WriteLine("1. Ver todos los Profesores");
                Console.WriteLine("2. Insertar nuevo Profesor");
                Console.WriteLine("3. Actualizar Profesor");
                Console.WriteLine("4. Eliminar Profesor");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();
                //Según la opción digitada mostraremos el caso
                switch (opcion)
                {
                    //Para cada opción, ejecutamos el método correspondiente
                    case "1":
                        ObtenerProfesores();
                        break;
                    case "2":
                        InsertarProfesor();
                        break;
                    case "3":
                        ActualizarProfesor();
                        break;
                    case "4":
                        EliminarProfesor();
                        break;
                    case "5":
                        continuar = false; 
                        break;
                    default:
                        Console.WriteLine("Opción invalida. Intente de nuevo");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\n Presione una tecla para continuar");
                }
            }
        }


        private void ObtenerProfesores()
        {
            //Crea una lista con los profesores obtenidos del método
            var lista = _profesorBLL.ObtenerProfesores();
            Console.WriteLine("\n === Lista de Profesores ===");
            //Para cada profesor en la lista, mostraremos sus datos
            foreach (var profesor in lista)
            {
                Console.WriteLine($"ID: {profesor.Id} - Nombre: {profesor.Nombre} - Especialidad: {profesor.Especialidad} - Email: {profesor.Email}");
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
        
        //Petición de datos para insertar un profesor
        private void InsertarProfesor()
        {
            Console.WriteLine("\n--- Insertar Profesor ---");
            //Le pedimos al usuario los datos del profesor nuevo
            string nombre = Utilidades.LeerTexto("Nombre:");
            string especialidad = Utilidades.LeerTexto("Especialidad: ");
            string email = Utilidades.LeerTexto("Email: ");
            //Creamos un objeto de la clase principal y agregamos los datos ingresados por el usuario
            Profesor profesor = new Profesor
            {
                Nombre = nombre,
                Especialidad = especialidad,
                Email = email,
            };
            //Si en las validaciones (ProfesorBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _profesorBLL.InsertarProfesor(profesor);
            Console.WriteLine(exito ? "Profesor ingresado correctamente" : "No se pudo insertar el profesor");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de datos para Actualizar un profesor
        private void ActualizarProfesor()
        {
            Console.WriteLine("\n--- Actualizar Profesor ---");
            //Pedimos el ID del profesor a actualizar
            int id = Utilidades.LeerEntero("ID del profesor a actualizar: ");
            //Pedimos los datos nuevos para el profesor seleccionado
            string nombre = Utilidades.LeerTexto("Nombre nuevo:");
            string especialidad = Utilidades.LeerTexto("Especialidad nueva: ");
            string email = Utilidades.LeerTexto("Email nuevo: ");
            //Creamos un objeto de la clase principal y agregamos los datos ingresados por el usuario
            Profesor profesor = new Profesor
            {
                Id = id,
                Nombre = nombre,
                Especialidad = especialidad,
                Email = email,
            };
            //Si en las validaciones (ProfesorBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _profesorBLL.ActualizarProfesor(profesor);
            Console.WriteLine(exito ? "Profesor actualizado correctamente" : "No se pudo actualizar el profesor");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición del ID del profesor a eliminar
        private void EliminarProfesor()
        {
            Console.WriteLine("\n--- Eliminar Profesor ---");
            int id = Utilidades.LeerEntero("ID del profesor a eliminar: ");
            //Si en las validaciones (ProfesorBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _profesorBLL.EliminarProfesor(id);
            Console.WriteLine(exito ? "Profesor eliminado correctamente" : "No se pudo eliminar el profesor");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}


