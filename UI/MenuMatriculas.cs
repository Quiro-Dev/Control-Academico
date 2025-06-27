
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: MenuMatriculas.cs 
// Descripción: Contiene el menú de consola para la gestión de las matriculas
// ================================


using ControlAcademico.Bussines;
using ControlAcademico.Models;

namespace ControlAcademico.UI
{
    public class MenuMatriculas
    {

        private readonly MatriculaBLL _matriculaBLL = new();

        public void Mostrar()
        {
            //Valida el estado del menú
            bool continuar = true;

            while (continuar)
            {
                //Limpiamos la consola para un menú más legible
                Console.Clear();
                //Damos las opciones para la gestión de matriculas
                Console.WriteLine("\n=== MENÚ MATRICULAS ===");
                Console.WriteLine("1. Ver todas las Matriculas");
                Console.WriteLine("2. Insertar Matricula");
                Console.WriteLine("3. Actualizar Matricula");
                Console.WriteLine("4. Eliminar Matricula");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                //Según la opción digitada mostramos el caso
                switch (opcion)
                {
                    //Para cada opción, ejecutamos el método correspondiente
                    case "1":
                        ObtenerMatriculas();
                        break;
                    case "2":
                        InsertarMatricula();
                        break;
                    case "3":
                        ActualizarMatricula();
                        break;
                    case "4":
                        EliminarMatricula();
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


        private void ObtenerMatriculas()
        {
            //Crea la lista con las matriculas obtenidas por el método
            var lista = _matriculaBLL.ObtenerMatriculas();
            Console.WriteLine("\n--- Lista de Matriculas ---");
            //Para cada matricula en la lista, mostraremos sus datos
            foreach (var matricula in lista)
            {
                Console.WriteLine($"ID: {matricula.Id} - ID Alumno: {matricula.IdAlumno} - ID Curso: {matricula.IdCurso} - Año Matricula: {matricula.Año} - ID Profesor: {matricula.IdProfesor} ");
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de datos para insertar una matricula
        private void InsertarMatricula()
        {
            Console.WriteLine("\n--- Insertar Matricula ---");
            //Le pedimos al usuario los datos de la matricula nueva
            int idAlumno = Utilidades.LeerEntero("ID Alumno: ");
            int idCurso = Utilidades.LeerEntero("ID Curso: ");
            int año = Utilidades.LeerEntero("Año Matricula: ");
            int idProfesor = Utilidades.LeerEntero("ID Profesor: ");
            //Creamos un objeto de la clase principal y agregamos los datos ingresados por el usuario
            Matricula matricula = new Matricula
            {
                IdAlumno = idAlumno,
                IdCurso = idCurso,
                Año = año,
                IdProfesor = idProfesor,
            };
            //Si en las validaciones (MatriculaBLL) no ocurrió ningún error, damos como True la variable
            bool exito = _matriculaBLL.InsertarMatricula(matricula);
            Console.WriteLine(exito ? "Matricula ingresada correctamente" : "No se pudo ingresar la matricula");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de datos para actualizar una matricula
        private void ActualizarMatricula()
        {
            Console.WriteLine("\n--- Actualizar Matricula ---");
            //Pedimos el ID de la matricula a eliminar
            int id = Utilidades.LeerEntero("ID Matricula a actualizar: ");
            //Pedimos los datos nuevos de la matricula
            int idAlumno = Utilidades.LeerEntero("ID Alumno: ");
            int idCurso = Utilidades.LeerEntero("ID Curso: ");
            int año = Utilidades.LeerEntero("Año Matricula: ");
            int idProfesor = Utilidades.LeerEntero("ID Profesor: ");
            //Creamos un objeto de la clase principal y agregamos los datos ingresados por el usuario
            Matricula matricula = new Matricula
            {
                Id = id,
                IdAlumno = idAlumno,
                IdCurso = idCurso,
                Año = año,
                IdProfesor = idProfesor,
            };
            //Si en las validaciones (MatriculaBLL no ocurrió ningún error, damos como True la variable)
            bool exito = _matriculaBLL.ActualizarMatricula(matricula);
            Console.WriteLine(exito ? "Matricula actualizada correctamente" : "No se pudo actualizar la matricula");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        //Petición de ID para eliminar matricula
        private void EliminarMatricula()
        {
            Console.WriteLine("\n--- Eliminar Matricula ---");
            int id = Utilidades.LeerEntero("ID de matricula a eliminar: ");
            //Si en las validaciones (MatriculaBLL no ocurrió ningún error, damos como True la variable)
            bool exito = _matriculaBLL.EliminarMatricula(id);
            Console.WriteLine(exito ? "Matricula eliminada correctamente" : "No se pudo eliminar la matricula ");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
