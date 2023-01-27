using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using ProyectIO;
using ProyectRepository;
using ProyectRepository.Conections;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;

namespace ProyectBack
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
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SecretKey")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

           

			#region servicios de la webAPI
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod();
					});
			});

			services.AddMvc(option => option.EnableEndpointRouting = false);

			IDbConnectionFactory dbConnectionFactory = new SqlConnectionFactory(Configuration.GetConnectionString("ProyectDB"));
			AppConfig.Instance.Set(dbConnectionFactory: dbConnectionFactory, appID: "", appName: "ProuectoDB");

			#endregion

			#region Swagger
			services.AddSwaggerGen(c =>
			{	
				c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "<strong>Seguridad de esta API utilizando JWT.</strong><br/>Copiar la palabra 'Bearer' con un espacio y a continuación el Token de acceso."
				});

				c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
				{
					{
						new Microsoft.OpenApi.Models.OpenApiSecurityScheme
						{
							Reference = new Microsoft.OpenApi.Models.OpenApiReference
							{
								Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});

				
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				

			});

			#endregion
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(a => a.Run(context => MyExceptionHandler(context)));
			}

			if (env.IsDevelopment() || env.IsStaging())
			{

				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
					c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "DeclaracionesAPI");
				});
			}
			var supportedCultures = new[]
			{
				new CultureInfo("es-AR"),
				new CultureInfo("pt"),
			};

			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("es-AR"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});

			app.UseRouting();

			app.UseCors();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static async Task MyExceptionHandler(HttpContext context)
		{

			var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
			var exception = feature.Error;

			string mensaje;
			int statusCode;

			if (exception is UnauthorizedAccessException)
			{
				mensaje = "";
				statusCode = StatusCodes.Status401Unauthorized;
			}


			else
			{
				mensaje = "Contacte soporte técnico"; 
				statusCode = StatusCodes.Status500InternalServerError;
			}
			context.Response.StatusCode = statusCode;
			await context.Response.WriteAsync(mensaje).ConfigureAwait(false);

		}
	}
}
