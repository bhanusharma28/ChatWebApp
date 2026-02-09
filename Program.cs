using Microsoft.EntityFrameworkCore;
using SignalRChatWebAPI.Data;
using SignalRChatWebAPI.Hubs;
using SignalRChatWebAPI.Interfaces.IRepository;
using SignalRChatWebAPI.Interfaces.IService;
using SignalRChatWebAPI.Repository;
using SignalRChatWebAPI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ChatAppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ChatAppSignalrDbCon")));

builder.Services.AddScoped<IChatRepository, ChatRepository>();

builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowRenderUI",
		policy => {
			policy.WithOrigins(
				"https://chatwebappui.onrender.com"
			)
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials();
		});
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseWebSockets();      // ? VERY IMPORTANT for SignalR

app.UseCors("AllowRenderUI");  // ? Allows your HTML file to connect

app.MapHub<ChatHub>("/chathub");

app.Run();
