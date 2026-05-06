using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Reader> Readers => Set<Reader>();
        public DbSet<Loan> Loans => Set<Loan>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasIndex(x => x.Isbn).IsUnique();
            modelBuilder.Entity<Reader>().HasIndex(x => x.LibraryCardNumber).IsUnique();

            modelBuilder.Entity<Loan>()
                .HasOne(x => x.Book).WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Loan>()
                .HasOne(x => x.Reader).WithMany(x => x.Loans)
                .HasForeignKey(x => x.ReaderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
