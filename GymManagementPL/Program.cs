namespace GymManagementPL
{
    public class Program()
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // AddMember services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(
                option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("GymDbContextConectionString")
                //another way to get to the appsettings
                //option.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["GymDbContextConectionString"]
                ));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IMemberServices, MemberService>();
            builder.Services.AddScoped<ITrainerServices, TrainerService>();
            builder.Services.AddScoped<IPlanServices, PLanService>();
            builder.Services.AddScoped<ISessionServices, SessionService>();
            builder.Services.AddScoped<IAttachmentService, AttatchmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Config =>
                {
                    Config.User.RequireUniqueEmail = true;

                }).AddEntityFrameworkStores<GymDbContext>();
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/Login";
                config.AccessDeniedPath = "/Account/AccessDenied";
            });


            builder.Services.AddAutoMapper(A => A.AddProfile(new SessionMappingProfile()));
            builder.Services.AddAutoMapper(A => A.AddProfile(new MemberMappingProfile()));
            builder.Services.AddAutoMapper(A => A.AddProfile(new HealthRecordMappingProfile()));
            builder.Services.AddAutoMapper(A => A.AddProfile(new PlanMappingProfile()));
            var app = builder.Build();
            #region MigrateDataBase--->DataSeeding
            using var Scope = app.Services.CreateScope();
            var gymDbContext = Scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var PendingMigrations = gymDbContext.Database.GetPendingMigrations();
            if (PendingMigrations?.Any() ?? false)
                gymDbContext.Database.Migrate();
            GymDbSeeding.SeedData(gymDbContext);
            IdentityDbContextSeeding.SeedData(roleManager, UserManager);
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
