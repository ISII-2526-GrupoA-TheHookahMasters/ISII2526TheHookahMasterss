using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {

    public DbSet<Fabricante> Fabricante { get; set; }
    public DbSet<CompraItem> CompraItem { get; set; }
    public DbSet<AlquilarItem> AlquilarItem { get; set; }
    public DbSet<Compra> Compra { get; set; }
}
