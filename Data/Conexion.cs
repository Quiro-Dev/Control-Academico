
// ================================
// Realizado por: Santiago Quiroga
// GitHub: Quiro-Dev
// Clase: Conexion.cs 
// Descripción: Crea la conexión con la base de datos
// ================================

using Microsoft.Data.SqlClient; //Maneja la conexión a SQL Server
using System.Configuration; //Lee el App.Config

namespace ControlAcademico.Data
{
    public class Conexion
    {
        private readonly string cadenaConexion;

        public Conexion()
        {
            //Carga la cadena de conexión de App.config automáticamente
            cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        //Devuelve un SqlConnection listo para usar 
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
