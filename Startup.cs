using BarMenuBoardAPI.Configuration;
using BarMenuBoardAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarMenuBoardAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment,
                       IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            services.AddDbContext<BarMenuBoardContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("BarMenuBoardContext")));

            var pathIncludeXmlComments =
                $@"{Environment.ContentRootPath}\{Environment.ApplicationName}.xml";
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "BarMenuBoard API",
                    Description = "API for BarMenuBoard App"
                });

                if (System.IO.File.Exists(pathIncludeXmlComments))
                    c.IncludeXmlComments(pathIncludeXmlComments);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddApplicationInsightsTelemetry();

            services.Configure<RecipeImageConfiguration>(Configuration.GetSection("RecipeImageConfiguration"));
            services.Configure<BoardStyleImageConfiguration>(Configuration.GetSection("BoardStylesImageConfiguration"));

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCorsMiddleware();

            app.UseCors("ApiCorsPolicy");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarMenuBoard API");
                c.RoutePrefix = string.Empty;
            });
 
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
