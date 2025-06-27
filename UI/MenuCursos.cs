
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: MenuCursos.cs 
// Descripción: Contiene el menú de consola para la gestión de los cursos
// ================================

using ControlAcademico.Bussines;
using ControlAcademico.Models;

namespace ControlAcademico.UI
{
    public class MenuCursos
    {
        private readonly CursoBLL _cursoBLL = new();

        public void Mostrar()
        {
            //Valida el estado del menú
            bool continuar = true;

            while (continuar)
            {
                //Limpiamos la consola para un menú más legible
                Console.Clear();
                //Damos las opciones para la gestión de cursos
                Console.WriteLine("\n === MENÚ CURSOS ===");
                Console.WriteLine("1. Ver todos los cursos");
                Console.WriteLine("2. Insertar nuevo curso");
                Console.WriteLine("3. Actualizar curso");
                Console.WriteLine("4. Eliminar curso");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();
                //Según la opción digitada mostraremos el caso
                switch (opcion)
                {
                    //Para cada opción, ejecutamos el método correspondiente
                    case "1":
                        ObtenerCursos();
                        break;
                    case "2":
                        InsertarCurso();
                        break;
                    case "3":
                        ActualizarCurso();
                        break;
                    case "4":
                        EliminarCurso();
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


        private void ObtenerCursos()
        {
            //Crea una lista con los cursos obtenidos del método
            var lista = _cursoBLL.ObtenerCursos();
            Console.WriteLine("\n--- Lista de Cursos ---");
            //Para cada curso en la lista, mostraremos sus datos
            foreach(var curso in lista)
            {
                Console.WriteLine($"ID: {curso.Id} - Nombre Curso: {curso.NombreCurso} - Nivel {curso.Nivel} - Duración: {curso.DuracionMeses} meses");
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de datos para insertar un curso
        private void InsertarCurso()
        {
            Console.WriteLine("\n --- Insertar Curso ---");
            //Le pedimos al usuario los datos del curso nuevo
            string nombreCurso = Utilidades.LeerTexto("Nombre del Curso: ");
            string nivel = Utilidades.LeerTexto("Nivel del Curso: ");
            int duracionMeses = Utilidades.LeerEntero("Duración del Curso en meses: ");
            //Creamos un objeto de la clase principal y le agregamos los datos ingresados por el usuario
            Curso curso = new Curso
            {
                NombreCurso = nombreCurso,
                Nivel = nivel,
                DuracionMeses = duracionMeses,
            };
            //Si en las validaciones (CursoBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _cursoBLL.InsertarCurso(curso);
            Console.WriteLine(exito ? "Curso ingresado correctamente" : "No se pudo ingresar el Curso");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de datos para actualizar un curso
        private void ActualizarCurso()
        {
            Console.WriteLine("\n --- Actualizar Curso ---");
            //Pedimos el ID del curso a actualizar
            int id = Utilidades.LeerEntero("ID del curso a actualizar: ");
            //Pedimos los datos nuevos para el curso seleccionado
            string nombreCurso = Utilidades.LeerTexto("Nombre nuevo del Curso: ");
            string nivel = Utilidades.LeerTexto("Nivel nuevo del Curso: ");
            int duracionMeses = Utilidades.LeerEntero("Duración nueva del Curso en meses: ");

            //Creamos un objeto de la clase principal y agregamos los datos ingresados por el usuario
            Curso curso = new Curso
            {
                Id = id,
                NombreCurso = nombreCurso,
                Nivel = nivel,
                DuracionMeses = duracionMeses,
            };
            //Si en las validaciones (CursoBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _cursoBLL.ActualizarCurso(curso);
            Console.WriteLine(exito ? "Curso actualizado correctamente" : "No se pudo actualizar el curso");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición del ID del curso a eliminar
        private void EliminarCurso()
        {
            Console.WriteLine("\n --- Eliminar Curso ---");
            int id = Utilidades.LeerEntero("ID del curso a eliminar: ");
            //Si en las validaciones (CursoBLL) no ocurrió ningún error, damos como true la variable
            bool exito = _cursoBLL.EliminarCurso(id);
            Console.WriteLine(exito ? "Curso eliminado correctamente" : "No se pudo eliminar el curso");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
