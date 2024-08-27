using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using App.Mappper;
using AutoMapper;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private async Task InitializeMongoDbAsync(IServiceProvider serviceProvider)
        {
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var mongoConfig = Configuration.GetSection("MongoDB");
            var connectionString = mongoConfig.GetValue<string>("ConnectionString");
            var databaseName = mongoConfig.GetValue<string>("DatabaseName");

            services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
            services.AddSingleton<IMongoDatabase>(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(databaseName));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapppingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IServiceDemo, ServiceDemo>();
            services.AddControllers(); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; 
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Or other endpoints
            });
        }
    }
}
