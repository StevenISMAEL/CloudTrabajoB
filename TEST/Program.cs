using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using System;

namespace TEST
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configurar los endpoints para cada entidad
            Crud<Asistencia>.EndPoint = "https://localhost:7287/api/Asistencias";
            Crud<Certificado>.EndPoint = "https://localhost:7287/api/Certificados";
            Crud<Espacio>.EndPoint = "https://localhost:7287/api/Espacios";
            Crud<Evento>.EndPoint = "https://localhost:7287/api/Eventos";
            Crud<Inscripcion>.EndPoint = "https://localhost:7287/api/Inscripciones";
            Crud<Pago>.EndPoint = "https://localhost:7287/api/Pagos";
            Crud<Participante>.EndPoint = "https://localhost:7287/api/Participantes";
            Crud<Ponente>.EndPoint = "https://localhost:7287/api/Ponentes";
            Crud<Sesion>.EndPoint = "https://localhost:7287/api/Sesiones";

            try
            {
                //1.Participante
                var participante = Crud<Participante>.Create(new Participante
                {
                    Cedula = "123456789",
                    Name = "Ana",
                    Lastname = "Gómez",
                    Email = "ana.gomez@email.com",
                    Phone = "555-1234"
                }).Result;
                Console.WriteLine(participante.Cedula + " - " + participante.Name + " " + participante.Lastname + " " + participante.Email + " " + participante.Phone);

                // 2. Espacio
                var espacio = Crud<Espacio>.Create(new Espacio
                {
                    Id = 1,
                    Name = "Sala Principal",
                    Type = "Auditorio",
                    Description = "Sala principal para conferencias",
                    Capacity = 100,
                    Location = "Edificio A, Piso 2"
                }).Result;
                Console.WriteLine(espacio.Id + " - " + espacio.Name + " " + espacio.Type + " " + espacio.Description + " " + espacio.Capacity + " " + espacio.Location);

                // 3. Evento
                var evento = Crud<Evento>.Create(new Evento
                {
                    Id = 1,
                    Name = "Conferencia de Tecnología 2025",
                    fechaInicio = new DateTime(2025, 6, 15, 9, 0, 0),
                    fechaFin = new DateTime(2025, 6, 15, 17, 0, 0),
                    type = "Conferencia",
                    location = "Centro de Convenciones",
                    maxCapacity = 200
                }).Result;
                Console.WriteLine(evento.Id + " - " + evento.Name + " " + evento.fechaInicio + " " + evento.fechaFin + " " + evento.type + " " + evento.location + " " + evento.maxCapacity);

                // 4. Ponente
                var ponente = Crud<Ponente>.Create(new Ponente
                {
                    Id = 1,
                    Name = "Juan",
                    Lastname = "Pérez",
                    Email = "juan.perez@email.com",
                    Phone = "555-5678",
                    Especialidad = "Inteligencia Artificial"
                }).Result;
                Console.WriteLine(ponente.Id + " - " + ponente.Name + " " + ponente.Lastname + " " + ponente.Email + " " + ponente.Phone + " " + ponente.Especialidad);

                // 5. Sesión
                var sesion = Crud<Sesion>.Create(new Sesion
                {
                    Id = 1,
                    Name = "Introducción a la IA",
                    horaInicio = new DateTime(2025, 6, 15, 10, 0, 0),
                    horaFin = new DateTime(2025, 6, 15, 12, 0, 0),
                    EspacioID = 1,
                    EventoID = 1
                }).Result;
                Console.WriteLine(sesion.Id + " - " + sesion.Name + " " + sesion.horaInicio + " " + sesion.horaFin + " " + sesion.EspacioID + " " + sesion.EventoID);

                // 6. Inscripción
                var inscripcion = Crud<Inscripcion>.Create(new Inscripcion
                {
                    Id = 1,
                    fechaInscripcion = new DateTime(2025, 5, 24),
                    estado = true,
                    EventoId = 1,
                    Cedula = "123456789"
                }).Result;
                Console.WriteLine(inscripcion.Id + " - " + inscripcion.fechaInscripcion + " " + inscripcion.estado + " " + inscripcion.EventoId + " " + inscripcion.Cedula);

                // 7. Asistencia
                var asistencia = Crud<Asistencia>.Create(new Asistencia
                {
                    Id = 1,
                    fechaAsistencia = new DateTime(2025, 6, 15, 10, 0, 0),
                    estado = true,
                    sesionId = 1,
                    inscripcionId = 1
                }).Result;
                Console.WriteLine(asistencia.Id + " - " + asistencia.fechaAsistencia + " " + asistencia.estado + " " + asistencia.sesionId + " " + asistencia.inscripcionId);

                // 8. Pago
                var pago = Crud<Pago>.Create(new Pago
                {
                    Id = 1,
                    monto = 150.00,
                    fechaPago = new DateTime(2025, 5, 24),
                    medioPago = "Tarjeta de crédito",
                    estado = true,
                    InscripcionID = 1
                }).Result;
                Console.WriteLine(pago.Id + " - " + pago.monto + " " + pago.fechaPago + " " + pago.medioPago + " " + pago.estado + " " + pago.InscripcionID);

                // 9. Certificado
                var certificado = Crud<Certificado>.Create(new Certificado
                {
                    Id = 1,
                    fechaEmision = new DateTime(2025, 6, 16),
                    UrlDescarga = "https://example.com/certificado1.pdf",
                    InscripcionID = 1
                }).Result;
                Console.WriteLine(certificado.Id + " - " + certificado.fechaEmision + " " + certificado.UrlDescarga + " " + certificado.InscripcionID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar datos: {ex.Message}");
            }

            Console.WriteLine("\nDatos insertados correctamente!");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}