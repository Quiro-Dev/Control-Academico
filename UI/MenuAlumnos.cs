
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: MenuAlumnos.cs
// Descripción: Contiene el menú de consola para la gestión de los alumnos
// ================================

using ControlAcademico.Bussines;
using ControlAcademico.Models;

namespace ControlAcademico.UI
{
    public class MenuAlumnos
    {
        private readonly AlumnoBLL _alumnoBLL = new();

        public void Mostrar()
        {
            //Valida el estado del menú 
            bool continuar = true;

            while (continuar)
            {
                //Limpiamos la consola para un menú más legible 
                Console.Clear();
                //Damos las opciones para la gestión de los alumnos
                Console.WriteLine("\n === MENÚ DE ALUMNOS ===");
                Console.WriteLine("1. Ver todos los alumnos");
                Console.WriteLine("2. Insertar nuevo alumno");
                Console.WriteLine("3. Actualizar Alumno ");
                Console.WriteLine("4. Eliminar Alumno");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();
                //Según la opción digitada mostraremos el caso
                switch (opcion)
                {
                    //Para cada opción, ejecutamos el método correspondiente
                    case "1":
                        ObtenerAlumnos();
                        break;
                    case "2":
                        InsertarAlumno();
                        break;
                    case "3":
                        ActualizarAlumno();
                        break;
                    case "4":
                        EliminarAlumno();
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

        private void ObtenerAlumnos()
        {
            //Crea una lista con los alumnos obtenidos del método
            var lista = _alumnoBLL.ObtenerAlumnos();
            Console.WriteLine("\n --- Lista De Alumnos ---");
            //Para cada alumno en la lista, mostraremos sus datos
            foreach(var alumno in lista)
            {
                Console.WriteLine($"ID: {alumno.Id} - Nombre: {alumno.Nombre} - Nacimiento: {alumno.FechaNacimiento.ToShortDateString()} - Email: {alumno.Email} - Telefono: {alumno.Telefono}");
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de datos para insertar un alumno
        private void InsertarAlumno()
        {
            Console.WriteLine("\n --- Insertar Alumno ---");
            //Le pedimos al usuario los datos del alumno nuevo
            string nombre = Utilidades.LeerTexto("Nombre: ");
            DateTime fechaNacimiento = Utilidades.LeerFecha("Fecha Nacimiento (YYYY-MM-DD): ");
            string email = Utilidades.LeerTexto("Email: ");
            string telefono = Utilidades.LeerTexto("Teléfono: ");
            //Creamos un objeto de la clase principal y agregamos los datos ingresados por el usuario
            Alumno alumno = new Alumno
            {
                Nombre = nombre,
                FechaNacimiento = fechaNacimiento,
                Email = email,
                Telefono = telefono
            };
            //Si en las validaciones (AlumnoBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _alumnoBLL.InsertarAlumno( alumno );
            Console.WriteLine(exito ? "Alumno ingresado correctamente" : "No se pudo insertar el alumno");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de datos para Actualizar un alumno
        private void ActualizarAlumno()
        {
            Console.WriteLine("\n --- Actualizar Alumno ---");
            //Pedimos el ID del alumno a actualizar
            int id = Utilidades.LeerEntero("ID del alumno que desea actualizar: ");
            //Pedimos los datos nuevos para el alumno seleccionado
            string nombre = Utilidades.LeerTexto("Nombre nuevo: ");
            DateTime fechaNacimiento = Utilidades.LeerFecha("Fecha Nacimiento nueva (YYYY-MM-DD): ");
            string email = Utilidades.LeerTexto("Email nuevo: ");
            string telefono = Utilidades.LeerTexto("Teléfono nuevo: ");

            //Creamos un objeto de la clase principal y agregamos los datos ingresados por el usuario
            Alumno alumno = new Alumno
            {
                Id = id,
                Nombre = nombre,
                FechaNacimiento = fechaNacimiento,
                Email = email,
                Telefono = telefono
            };
            //Si en las validaciones (AlumnoBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _alumnoBLL.ActualizarAlumno(alumno);
            Console.WriteLine(exito ? "Alumno actualizado correctamente" : "No se pudo actualizar el alumno");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición del ID del alumno a eliminar
        private void EliminarAlumno()
        {
            Console.WriteLine("\n --- Eliminar Alumno ---");
            int id = Utilidades.LeerEntero("ID del alumno a eliminar: ");
            //Si en las validaciones (AlumnoBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _alumnoBLL.EliminarAlumno(id);
            Console.WriteLine(exito ? "Alumno eliminado correctamente" : "No se pudo eliminar el alumno");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
