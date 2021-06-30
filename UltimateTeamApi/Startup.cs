using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UltimateTeamApi.Domain.Persistance.Contexts;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Exceptions;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.Persistance.Repositories;
using UltimateTeamApi.Services;
using UltimateTeamApi.Settings;

namespace UltimateTeamApi
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
            //CORS Support
            services.AddCors();

            services.AddControllers();

            //App Settings Section Reference
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //JSON Web Token Authentication Configuration
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //Authentication Service Configuration
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //Database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Dependency Injection Configuration
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IFunctionalityRepository, FunctionalityRepository>();
            services.AddScoped<ISessionStadisticRepository, SessionStadisticRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<ISubscriptionBillRepository, SubscriptionBillRepository>();
            services.AddScoped<ISubscriptionTypeRepository, SubscriptionTypeRepository>();
            services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionParticipantRepository, SessionParticipantRepository>();
            services.AddScoped<ISessionTypeRepository, SessionTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFunctionalityService, FunctionalityService>();
            services.AddScoped<ISessionStadisticService, SessionStadisticService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupMemberService, GroupMemberService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<ISubscriptionBillService, SubscriptionBillService>();
            services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionParticipantService, SessionParticipantService>();
            services.AddScoped<ISessionTypeService, SessionTypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDriveService, ExternalTools.Services.DriveService>();
            services.AddScoped<ITrelloService, ExternalTools.Services.TrelloService>(); 
            services.AddScoped<ICalendarService, ExternalTools.Services.CalendarService>();

            services.AddScoped<IMiroService, ExternalTools.Services.MiroService>();

            //Apply Endpoints Naming Convention
            services.AddRouting(options => options.LowercaseUrls = true);

            //AutoMapper Setup
            services.AddAutoMapper(typeof(Startup));

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UltimateTeamApi", Version = "v1" });
                c.EnableAnnotations();
            });

            //Google Configuration
            services
                .AddAuthentication(o =>
                {
                    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogleOpenIdConnect(options =>
                {
                    options.ClientId = "85775309915-j9lv6vi2jd747hplr59h36d26e305l86.apps.googleusercontent.com";
                    options.ClientSecret = "uEpe7MDtye3dDzIkLJw232Cm";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UltimateTeamApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //CORS Configuration
            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            //Authentication Support
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
