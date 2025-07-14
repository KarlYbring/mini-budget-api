using Microsoft.EntityFrameworkCore;
using MiniBudgetApp.Models;

namespace MiniBudgetApp.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)

{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<User> Users { get; set; }
}
