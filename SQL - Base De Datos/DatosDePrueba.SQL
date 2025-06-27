-- ========================
-- Script de Datos de Prueba - ControlAcademico
-- ========================

-- Primero insertamos en las tablas independientes

-- Alumnos
INSERT INTO Alumnos (Nombre, FechaNacimiento, Email, Telefono) VALUES
('Laura Gómez', '2002-03-15', 'laura.gomez@email.com', '3001234567'),
('Carlos Martínez', '2001-09-25', 'carlos.martinez@email.com', '3012345678'),
('Andrea López', '2000-06-10', 'andrea.lopez@email.com', '3023456789');

-- Profesores
INSERT INTO Profesores (Nombre, Especialidad, Email) VALUES
('Juan Pérez', 'Matemáticas', 'juan.perez@email.com'),
('Lucía Torres', 'Física', 'lucia.torres@email.com'),
('Diego Ramírez', 'Programación', 'diego.ramirez@email.com');

-- Cursos
INSERT INTO Cursos (NombreCurso, DuracionMeses, Nivel) VALUES
('Matemáticas Básicas', 4, 'Básico'),
('Física General', 6, 'Intermedio'),
('Introducción a la Programación', 5, 'Básico');

-- Matriculas (relaciones entre alumnos, cursos y profesores)
-- Supone que los IDs van en orden 1, 2, 3...
INSERT INTO Matriculas (IdAlumno, IdCurso, Año, IdProfesor) VALUES
(1, 1, 2024, 1), -- Laura Gómez en Matemáticas Básicas con Juan Pérez
(2, 3, 2024, 3), -- Carlos en Programación con Diego
(3, 2, 2025, 2); -- Andrea en Física con Lucía
