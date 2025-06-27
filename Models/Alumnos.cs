
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: Alumnos.cs
// Descripción: Contiene la clase principal Alumno
// ================================

namespace ControlAcademico.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}