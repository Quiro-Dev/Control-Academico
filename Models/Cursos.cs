
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: Cursos.cs
// Descripción: Contiene la clase principal Curso
// ================================

namespace ControlAcademico.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string NombreCurso { get; set; }
        public int DuracionMeses { get; set; }
        public string Nivel {  get; set; }

    }
}