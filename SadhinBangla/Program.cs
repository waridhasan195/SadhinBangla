using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;
using SadhinBangla.Rapositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SadhinBanglaDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("SadhinBanglaDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SadhinBanglaAuthDbConnectionString")));
                                                                                                                                                                                                                                                                                                                                                                        

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


builder.Services.AddScoped<ITagRapository, TagRapository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRapository, CloudinaryImageRepository>();
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
