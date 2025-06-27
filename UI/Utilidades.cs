
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: Utilidades.cs 
// Descripción: Contiene algunas validaciones para peticiones muy comunes
// ================================


namespace ControlAcademico.UI
{
    public static class Utilidades
    {
        //Método para pedir un numero entero
        public static int LeerEntero(string mensaje)
        {
            int valor;
            bool valido;

            do
            {
                //Mostramos el mensaje de petición
                Console.Write(mensaje);
                //Es valido (True) cuando la conversión de Valor es exitosa
                valido = int.TryParse(Console.ReadLine(), out valor);
                //Verifica que sea un numero válido
                if (!valido || valor < 0)
                {
                    Console.WriteLine("Este valor no es valido");
                }
                //Realizará la petición mientras el valor no sea valido o sea menor a 0
            } while (!valido || valor < 0);

            return valor;
        }

        //Método para pedir un texto
        public static string LeerTexto(string mensaje)
        {
            string entrada;

            do
            {
                //Mostramos el mensaje de petición
                Console.Write(mensaje);
                //Pedimos el valor
                entrada = Console.ReadLine();
                //Valida que el valor no sea nulo
                if (string.IsNullOrWhiteSpace(entrada))
                    Console.WriteLine("Este campo no puede estar vacío");
                //Realizará la petición mientras que el valor sea nulo
            } while (string.IsNullOrWhiteSpace(entrada));

            return entrada;
        }

        //Método para pedir una fecha
        public static DateTime LeerFecha(string mensaje)
        {
            DateTime fecha;
            bool valido;

            do
            {
                //Mostramos el mensaje de petición
                Console.Write(mensaje);
                //Es valido (True) cuando la conversión de Fecha es exitosa
                valido = DateTime.TryParse(Console.ReadLine(), out fecha);
                //Verifica que el valor ingresado sea valido
                if (!valido)
                {
                    Console.WriteLine("Ingrese una fecha valida. Ej: 2006-03-18");
                }
                //Realizara la petición mientras que el valor no sea valido
            } while (!valido);
            return fecha;
        }

    }
}
