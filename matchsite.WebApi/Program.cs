using matchsite.WebApi.Context; // Context klasörünün adý neyse onu yazmalýsýn
using matchsite.WebApi.Services; // Service klasörünün adý neyse onu yazmalýsýn

var builder = WebApplication.CreateBuilder(args);

// --- SERVÝS KAYIT ALANI (builder.Build'den önce olmalý) ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>();

// 2. Kendi yazdýðýmýz servislerin sisteme tanýtýlmasý (Dependency Injection)
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<MatchEventService>();
builder.Services.AddScoped<MatchEventTypeService>();
builder.Services.AddScoped<StandingService>();
// ---------------------------------------------------------

var app = builder.Build();

// --- HTTP PIPELINE (Uygulama Yapýlandýrma Alaný) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();