using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection.Emit;
using GaiaApplication.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public Microsoft.EntityFrameworkCore.DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
    }
}