using controle_tasks.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adicionar os servi�os
builder.Services.AddControllers();
builder.Services.AddSingleton<ITaskRepository, TaskRepository>(); //Reposit�rio Task
builder.Services.AddTransient<TaskService>(); //Servi�o Task


// Adiciona o servi�o de CORS, tava dando erro no front ao dar get
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Adicionar dados iniciais ao reposit�rio para ajudar nos testes/visualiza��o
TaskSeed.InicializarTasks(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa o middleware de CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();


app.Run();
