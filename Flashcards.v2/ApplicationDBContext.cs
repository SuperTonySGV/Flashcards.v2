using Microsoft.EntityFrameworkCore;
using Flashcards.v2.Models;

namespace Flashcards.v2;

internal class ApplicationDBContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source=MOTHERSHIP-XR7\\SQLEXPRESS;Initial Catalog=flashcards;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<Stack> Stacks { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }
}
