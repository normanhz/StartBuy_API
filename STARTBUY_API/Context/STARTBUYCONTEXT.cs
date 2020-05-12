using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace STARTBUY_API
{
    public partial class STARTBUYCONTEXT : DbContext
    {
        public STARTBUYCONTEXT()
        {
        }

        public STARTBUYCONTEXT(DbContextOptions<STARTBUYCONTEXT> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCategoriasProductos> TblCategoriasProductos { get; set; }
        public virtual DbSet<TblCiudades> TblCiudades { get; set; }
        public virtual DbSet<TblDepartamentos> TblDepartamentos { get; set; }
        public virtual DbSet<TblEmpresas> TblEmpresas { get; set; }
        public virtual DbSet<TblEstadosVentas> TblEstadosVentas { get; set; }
        public virtual DbSet<TblGanancias> TblGanancias { get; set; }
        public virtual DbSet<TblGeneros> TblGeneros { get; set; }
        public virtual DbSet<TblPaises> TblPaises { get; set; }
        public virtual DbSet<TblProductos> TblProductos { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblUsuarios> TblUsuarios { get; set; }
        public virtual DbSet<TblUsuariosPersonas> TblUsuariosPersonas { get; set; }
        public virtual DbSet<TblVentasProductos> TblVentasProductos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=LOCALHOST\\SQLEXPRESS;database=DBSTARTBUY;user=;password=;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCategoriasProductos>(entity =>
            {
                entity.HasKey(e => e.CategoriaProductoId);

                entity.ToTable("TBL_CATEGORIAS_PRODUCTOS");

                entity.Property(e => e.CategoriaProductoId).HasColumnName("Categoria_ProductoID");

                entity.Property(e => e.CategoriaProducto)
                    .HasColumnName("Categoria_Producto")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            });

            modelBuilder.Entity<TblCiudades>(entity =>
            {
                entity.HasKey(e => e.CiudadId);

                entity.ToTable("TBL_CIUDADES");

                entity.Property(e => e.CiudadId).HasColumnName("CiudadID");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDepartamentos>(entity =>
            {
                entity.HasKey(e => e.DepartamentoId);

                entity.ToTable("TBL_DEPARTAMENTOS");

                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.Departamento)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEmpresas>(entity =>
            {
                entity.HasKey(e => e.EmpresaId);

                entity.ToTable("TBL_EMPRESAS");

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

                entity.Property(e => e.CiudadId).HasColumnName("CiudadID");

                entity.Property(e => e.DireccionCompleta)
                    .HasColumnName("Direccion_Completa")
                    .IsUnicode(false);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.NombreContacto)
                    .HasColumnName("Nombre_Contacto")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroContacto).HasColumnName("Numero_Contacto");

                entity.Property(e => e.PaisId).HasColumnName("PaisID");
            });

            modelBuilder.Entity<TblEstadosVentas>(entity =>
            {
                entity.HasKey(e => e.EstadoVentaId)
                    .HasName("PK_TBL_ESTADOS_VENTAA");

                entity.ToTable("TBL_ESTADOS_VENTAS");

                entity.Property(e => e.EstadoVentaId).HasColumnName("Estado_VentaID");

                entity.Property(e => e.EstadoVenta)
                    .HasColumnName("Estado_Venta")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblGanancias>(entity =>
            {
                entity.HasKey(e => e.GananciaId);

                entity.ToTable("TBL_GANANCIAS");

                entity.Property(e => e.GananciaId).HasColumnName("Ganancia_ID");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VentaProductoId).HasColumnName("Venta_ProductoID");
            });

            modelBuilder.Entity<TblGeneros>(entity =>
            {
                entity.HasKey(e => e.GeneroId);

                entity.ToTable("TBL_GENEROS");

                entity.Property(e => e.GeneroId).HasColumnName("GeneroID");

                entity.Property(e => e.Genero)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPaises>(entity =>
            {
                entity.HasKey(e => e.PaisId);

                entity.ToTable("TBL_PAISES");

                entity.Property(e => e.PaisId).HasColumnName("PaisID");

                entity.Property(e => e.CodePais)
                    .HasColumnName("Code_Pais")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProductos>(entity =>
            {
                entity.HasKey(e => e.ProductoId);

                entity.ToTable("TBL_PRODUCTOS");

                entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

                entity.Property(e => e.CantidadEnStock).HasColumnName("Cantidad_En_Stock");

                entity.Property(e => e.CategoriaProductoId).HasColumnName("Categoria_ProductoID");

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("Fecha_Ingreso")
                    .HasColumnType("date");

                entity.Property(e => e.FechaModifico)
                    .HasColumnName("Fecha_Modifico")
                    .HasColumnType("date");

                entity.Property(e => e.Precio).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Producto)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioIngreso).HasColumnName("Usuario_Ingreso");

                entity.Property(e => e.UsuarioModifico).HasColumnName("Usuario_Modifico");
            });

            modelBuilder.Entity<TblRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("TBL_ROLES");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUsuarios>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.ToTable("TBL_USUARIOS");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CiudadId).HasColumnName("CiudadID");

                entity.Property(e => e.DireccionCompleta)
                    .HasColumnName("Direccion_Completa")
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

                entity.Property(e => e.GeneroId).HasColumnName("GeneroID");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PaisId).HasColumnName("PaisID");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUsuariosPersonas>(entity =>
            {
                entity.HasKey(e => e.UsuarioPersonaId);

                entity.ToTable("TBL_USUARIOS_PERSONAS");

                entity.Property(e => e.UsuarioPersonaId).HasColumnName("Usuario_PersonaID");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CiudadId).HasColumnName("CiudadID");

                entity.Property(e => e.DireccionCompleta)
                    .HasColumnName("Direccion_Completa")
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GeneroId).HasColumnName("GeneroID");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PaisId).HasColumnName("PaisID");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblVentasProductos>(entity =>
            {
                entity.HasKey(e => e.VentaProductoId);

                entity.ToTable("TBL_VENTAS_PRODUCTOS");

                entity.Property(e => e.VentaProductoId).HasColumnName("Venta_ProductoID");

                entity.Property(e => e.CategoriaProductoId).HasColumnName("Categoria_ProductoID");

                entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

                entity.Property(e => e.EstadoVentaId).HasColumnName("Estado_VentaID");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Precio).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UsuarioComprador).HasColumnName("Usuario_Comprador");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
