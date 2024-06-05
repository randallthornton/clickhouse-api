using ClickHouse.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClickhouseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class ClickhouseContext: DbContext
{
    public DbSet<MyFirstTable> MyFirstTableSet { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseClickHouse("Host=localhost;Port=8123;Username=default;Password=;Database=helloworld;Compress=True;CheckCompressedHash=False;SocketTimeout=60000000;Compressor=lz4;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var x = modelBuilder
            .Entity<MyFirstTable>()
            .ToTable("my_first_table");

        x.HasNoKey();
        x.Property(e => e.UserId).HasColumnName("user_id");
        x.Property(e => e.Message).HasColumnName("message");
        x.Property(e => e.Timestamp).HasColumnName("timestamp");
        x.Property(e => e.Metric).HasColumnName("metric");
    }
}

public class MyFirstTable
{
    public uint UserId { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public float Metric { get; set; }
}