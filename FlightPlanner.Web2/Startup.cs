using AutoMapper;
using FlightPlanner.Core2.DTO;
using FlightPlanner.Core2.Models;
using FlightPlanner.Core2.Services;
using FlightPlanner.Data;
using FlightPlanner.Services;
using FlightPlanner.Services.Validators;
using FlightPlanner.Web2.Authentication;
using FlightPlanner.Web2.Mappings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FlightPlanner.Web2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightPlanner.Web2", Version = "v1" });
            });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions,
                    BasicAuthenticationHandler>(
                    "BasicAuthentication", null);

            services.AddDbContext<FlightPlannerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("flight-planner2")));

            services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IDbServiceExtended, DbServiceExtended>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IValidator<FlightRequest>, AirportCodeValidator>();
            services.AddScoped<IValidator<FlightRequest>, AirportCodesEqualityValidator>();
            services.AddScoped<IValidator<FlightRequest>, ArrivalTimeValidator>();
            services.AddScoped<IValidator<FlightRequest>, CarrierValidator>();
            services.AddScoped<IValidator<FlightRequest>, CityValidator>();
            services.AddScoped<IValidator<FlightRequest>, CountryValidator>();
            services.AddScoped<IValidator<FlightRequest>, DepartureTimeValidator>();
            services.AddScoped<IValidator<FlightRequest>, TimeFrameValidator>();
            services.AddScoped<IValidator<SearchFlightsRequest>, SearchRequestValidatior>();
            services.AddScoped<IAirportService, AirportService>();
            var cfg = AutoMapperConfiguration.GetConfig();
            services.AddSingleton(typeof(IMapper), cfg);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightPlanner.Web2 v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
