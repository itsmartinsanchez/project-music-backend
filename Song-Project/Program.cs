using Microsoft.EntityFrameworkCore;
using Song_Project.Services;
using Song_Project.Operations.Artists;
using Song_Project.Operations.Songs;
using Song_Project.Operations.Users;
using Song_Project.Operations.Comments;
using Song_Project.Data;
using Song_Project.Filters;
{
    
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArtistsService, EFArtistsService>();
builder.Services.AddScoped<ISongsService, EFSongsService>();
builder.Services.AddScoped<IUserService, EFUserService>();
builder.Services.AddScoped<ICommentsService, EFCommentsService>();
builder.Services.AddScoped<ValidateSaveComment, ValidateSaveComment>();
builder.Services.AddScoped<ValidateSaveArtists, ValidateSaveArtists>();
builder.Services.AddScoped<ValidateSaveSong, ValidateSaveSong>();
builder.Services.AddScoped<ValidateDeleteArtist, ValidateDeleteArtist>();
builder.Services.AddScoped<ValidateDeleteComment, ValidateDeleteComment>();
builder.Services.AddScoped<ValidateRegister, ValidateRegister>();
builder.Services.AddScoped<AuthenticationFilter, AuthenticationFilter>();
builder.Services.AddScoped<AuthenticationService, AuthenticationService>();

var  myCorsConfig = "_myAllowSpecificOrigins";

builder.Services.AddCors(options => {
    options.AddPolicy(
        name: myCorsConfig,
        policy => {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(myCorsConfig);

app.UseAuthorization();

app.MapControllers();

app.Run();
