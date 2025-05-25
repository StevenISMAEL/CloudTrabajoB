using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;

namespace CloudTrabajo.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var BaseUrl = "https://ejemploblazorapp1202504-aneufacscghfaneq.westus-01.azurewebsites.net/api";
            //var BaseUrl = "https://localhost:7287/api";
            Crud<Asistencia>.EndPoint = $"{BaseUrl}/Asistencias";
            Crud<Certificado>.EndPoint = $"{BaseUrl}/Certificados";
            Crud<Espacio>.EndPoint = $"{BaseUrl}/Espacios";
            Crud<Evento>.EndPoint = $"{BaseUrl}/Eventos";
            Crud<Inscripcion>.EndPoint = $"{BaseUrl}/Inscripciones";
            Crud<Pago>.EndPoint = $"{BaseUrl}/Pagos";
            Crud<Participante>.EndPoint = $"{BaseUrl}/Participantes";
            Crud<Ponente>.EndPoint = $"{BaseUrl}/Ponentes";
            Crud<Sesion>.EndPoint = $"{BaseUrl}/Sesiones";
            Crud<SesionPonente>.EndPoint = $"{BaseUrl}/SesionPonentes";







            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
