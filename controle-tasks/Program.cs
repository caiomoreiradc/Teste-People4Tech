using controle_tasks.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adicionar os serviços
builder.Services.AddControllers();
builder.Services.AddSingleton<ITaskRepository, TaskRepository>(); //Repositório Task
builder.Services.AddTransient<TaskService>(); //Serviço Task


// Adiciona o serviço de CORS, tava dando erro no front ao dar get
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

// Adicionar dados iniciais ao repositório para ajudar nos testes/visualização
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
