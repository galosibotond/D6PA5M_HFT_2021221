using D6PA5M_HFT_2021221.Data;
using D6PA5M_HFT_2021221.Endpoint.Services;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Repository;
using D6PA5M_HFT_2021221.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace D6PA5M_HFT_2021221.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IArtistLogic, ArtistLogic>();
            services.AddTransient<IAlbumLogic, AlbumLogic>();
            services.AddTransient<IGenreLogic, GenreLogic>();
            services.AddTransient<IRecordCompanyLogic, RecordCompanyLogic>();

            services.AddTransient<IArtistRepository, ArtistRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IRecordCompanyRepository, RecordCompanyRepository>();

            services.AddTransient<AlbumStoreDbContext, AlbumStoreDbContext>();

            services.AddSignalR();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:43173"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
