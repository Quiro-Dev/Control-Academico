
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: Matriculas.cs
// Descripción: Contiene la clase principal Matricula
// ================================


namespace ControlAcademico.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdCurso{ get; set; }
        public int Año { get; set; }
        public int IdProfesor { get; set; }
    }
}