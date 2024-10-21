using AutoMapper;
using Azure.Storage.Blobs;
using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Mappers;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File(new RenderedCompactJsonFormatter(), "logs/log.txt")
            .CreateLogger();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        IMapper mapper = new Mapper(mapperConfig);
       

        try
        {
            Log.Information("Starting up");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }

        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection("Jwt");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
            };
        });

        // Add services to the container.
        builder.Services.AddControllers();
        // Configurar el BlobServiceClient con la cadena de conexión de Azure Blob Storage
        builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));

        builder.Services.AddSingleton(mapper);
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped(typeof(IQuestionRepository), typeof(QuestionRepository));
        builder.Services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
        builder.Services.AddScoped(typeof(IQuestionariesConfigRepository), typeof(QuestionariesConfigRepository));
        builder.Services.AddScoped(typeof(IQuestionaryRepository), typeof(QuestionaryRepository));
        builder.Services.AddScoped(typeof(IEvaluationRepository), typeof(EvaluationRepository));
        builder.Services.AddScoped(typeof(IRecordRepository), typeof(RecordRepository));
        builder.Services.AddScoped(typeof(IFileRepository), typeof(FileRepository));
        builder.Services.AddScoped(typeof(IFlagRepository), typeof(FlagRepository));
        builder.Services.AddScoped(typeof(IUsersProfilesRepository), typeof(UsersProfilesRepository));
        builder.Services.AddScoped<IQuestionTypeService, QuestionTypeService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IGroupService, GroupService>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IDivisionService, DivisionService>();
        builder.Services.AddScoped<IPositionService, PositionService>();
        builder.Services.AddScoped<IEscaleService, EscaleService>();
        builder.Services.AddScoped<IAreaService, AreaService>();
        builder.Services.AddScoped<IQuestionariesConfigService, QuestionariesConfigService>();
        builder.Services.AddScoped<IQuestionaryService, QuestionaryService>();
        builder.Services.AddScoped<IQuestionaryTypeService, QuestionaryTypeService>();
        builder.Services.AddScoped<IEvaluationService, EvaluationService>();
        builder.Services.AddScoped<IRecordService, RecordService>();
        builder.Services.AddScoped<IDynamicBlobService, DynamicBlobService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IFlagService, FlagService>();
        builder.Services.AddScoped<IColorService, ColorService>();
        builder.Services.AddScoped<IFlagTypeService, FlagTypeService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProfileService, ProfileService>();




        builder.Services.AddCors(options => {
            options.AddPolicy(MyAllowSpecificOrigins,
            builder => builder.WithOrigins("*")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                );
        });
        builder.Services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        var app = builder.Build();
        ApplyMigrations(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();
        app.UseAuthentication();    
        app.UseAuthorization();
        app.UseCors(MyAllowSpecificOrigins);

        app.MapControllers();

        app.Run();
    }

    private static void ApplyMigrations(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<ApplicationDbContext>();

            // Aplica las migraciones pendientes
            dbContext.Database.Migrate();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.ClearProviders();
                     logging.AddSerilog(dispose: true);
                 })
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                    
                 });

}
