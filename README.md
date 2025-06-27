# 📘 Control Académico

Este es un sistema de consola desarrollado en C# para la **gestión académica de una institución educativa**, que permite administrar alumnos, cursos, profesores y matrículas de forma sencilla y eficiente.

## 🧠 ¿Qué funcionalidades incluye?

- Ver todos los alumnos, profesores, cursos y matrículas.
- Insertar nuevos registros en cada entidad.
- Actualizar registros existentes.
- Eliminar registros (con validaciones que impiden, por ejemplo, eliminar un alumno con matrículas).
- Validaciones lógicas y estructurales implementadas en capa de negocio.
- Interfaz de consola simple y clara para navegar por menús.

## ⚙️ Tecnologías utilizadas

- **Lenguaje:** C#
- **IDE:** Visual Studio 2022
- **Base de datos:** SQL Server
- **Arquitectura:** En capas (UI, BLL, DAL, Models)
- **ADO.NET:** Para conexión directa a la base de datos
- **App.config:** Manejo de cadena de conexión de forma profesional
- **Control de versiones:** Git + GitHub

## 🗂️ Estructura del proyecto

| Carpeta / Archivo           | Descripción                                                   |
|----------------------------|---------------------------------------------------------------|
| `ControlAcademico.sln`     | Archivo de solución de Visual Studio                          |
| `ControlAcademico.csproj`  | Archivo de configuración del proyecto C#                      |
| `App.config`               | Contiene la cadena de conexión a SQL Server                   |
| `Program.cs`               | Punto de entrada principal del programa                       |
| `README.md`                | Documentación del proyecto                                    |
| `Models/`                  | Clases de entidades: `Alumno`, `Profesor`, `Curso`, etc.      |
| `Data/`                    | Lógica de acceso a datos (DAL)                                |
| `Bussines/`                | Lógica de negocio (BLL) con validaciones                      |
| `UI/`                      | Menús de consola para gestionar alumnos, cursos, etc.         |
| `ScriptsSQL/`              | Scripts `.sql` para crear la base de datos y poblarla         |



## 🧩 Base de datos

El proyecto se conecta a una base de datos SQL Server llamada **ControlAcademico**, que contiene las siguientes tablas:

- `Alumnos`
- `Profesores`
- `Cursos`
- `Matriculas` (con claves foráneas)

🔧 En la carpeta `ScriptsSQL` encontrarás 2 archivos `.sql` que:

1. **Crea la base de datos** y las tablas con sus claves primarias y foráneas.
2. **Agrega datos de prueba** (alumnos, profesores, cursos y algunas matrículas).

Esto permite que cualquier usuario pueda replicar rápidamente el entorno de pruebas.

## 🚀 ¿Cómo ejecutar el proyecto?

1. Clona este repositorio:
   ```bash
   git clone https://github.com/Quiro-Dev/ControlAcademico.git
2. **Abre el archivo** `ControlAcademico.sln` en Visual Studio.

3. **Ejecuta el script SQL** desde SQL Server Management Studio (SSMS) para crear la base de datos `ControlAcademico` con sus tablas y datos de prueba.

4. Verifica la cadena de conexión en el archivo `App.config`. Si es necesario, ajusta el nombre del servidor o la autenticación según tu equipo.

5. **Ejecuta** el programa desde Visual Studio (Ctrl + F5 o botón "Iniciar").

## 👨🏻‍💻Autor
- **Nombre:** Santiago Quiroga
- **GitHub:** Quiro-Dev
- 💻 Futuro FreeLancer y creador de contenido en desarrollo de software

## 😁¡Gracias por visitar el repositorio!
- Si te gusto el proyecto deja una ⭐ o compártelo con otros estudiantes
