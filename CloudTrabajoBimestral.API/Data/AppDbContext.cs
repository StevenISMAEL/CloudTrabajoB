using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CloudTrabajoBimestral.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CloudTrabajoBimestral.Models.Asistencia> Asistencia { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Certificado> Certificado { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Espacio> Espacio { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Evento> Evento { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Inscripcion> Inscripcion { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Pago> Pago { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Participante> Participante { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Ponente> Ponente { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.Sesion> Sesion { get; set; } = default!;

public DbSet<CloudTrabajoBimestral.Models.SesionPonente> SesionPonente { get; set; } = default!;



    }
