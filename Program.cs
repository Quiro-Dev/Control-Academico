
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: Program.cs
// Descripción: El archivo principal, donde se encuentra el método principal a ejecutar
// ================================


using ControlAcademico.UI;

class Program
{
    //Menú principal donde mostramos las distintas entidades a gestionar
    static void Main()
    {
        //Creamos objetos de cada menú para llamar los métodos
        MenuAlumnos menuAlumnos = new MenuAlumnos();
        MenuCursos menuCursos = new MenuCursos();
        MenuProfesores menuProfesores = new MenuProfesores();
        MenuMatriculas menuMatriculas = new MenuMatriculas();

        while (true)
        {
            //Limpiamos la consola para un menú más legible
            Console.Clear();
            Console.WriteLine("\n===  SISTEMA CONTROL ACADÉMICO  ===");
            //Damos las opciones de las entidades que se pueden gestionar
            Console.WriteLine("1. Gestionar Alumnos");
            Console.WriteLine("2. Gestionar Cursos");
            Console.WriteLine("3. Gestionar Profesores");
            Console.WriteLine("4. Gestionar Matriculas");
            Console.WriteLine("5. Salir");
            Console.Write("\n Seleccione una opción: ");
            string opcion = Console.ReadLine();
            //Según la opción digitada, mostramos el caso
            switch (opcion)
            {
                //Para cada opción, mostramos el menú correspondiente
                case "1":
                    menuAlumnos.Mostrar();
                    break;
                case "2":
                    menuCursos.Mostrar();
                    break;
                case "3":
                    menuProfesores.Mostrar();
                    break;
                case "4":
                    menuMatriculas.Mostrar();
                    break;
                case "5":
                    Console.WriteLine("\nGracias por usar el sistema!");
                    return;
                default:
                    Console.WriteLine("\nOpción invalida.Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}