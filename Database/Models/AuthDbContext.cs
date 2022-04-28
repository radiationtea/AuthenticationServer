using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Auth.Database.Models
{
    public partial class AuthDbContext : DbContext
    {
        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Depart> Departs { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Legend> Legends { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Subcate> Subcates { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(System.IO.File.ReadAllText("db.txt"), ServerVersion.Parse("5.5.68-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("categories");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("int(11)")
                    .HasColumnName("categoryid");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.EvalDateStart)
                    .HasMaxLength(4)
                    .HasColumnName("eval_date_start")
                    .IsFixedLength();

                entity.Property(e => e.EvalDateStop)
                    .HasMaxLength(4)
                    .HasColumnName("eval_date_stop")
                    .IsFixedLength();

                entity.Property(e => e.Label)
                    .HasMaxLength(30)
                    .HasColumnName("label");

                entity.Property(e => e.Manager)
                    .HasMaxLength(18)
                    .HasColumnName("manager");

                entity.Property(e => e.MaxScore)
                    .HasColumnType("int(11)")
                    .HasColumnName("max_score");
            });

            modelBuilder.Entity<Depart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("departs");

                entity.Property(e => e.Depid)
                    .HasColumnType("int(11)")
                    .HasColumnName("depid");

                entity.Property(e => e.Desc)
                    .HasMaxLength(200)
                    .HasColumnName("desc");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("files");

                entity.Property(e => e.Fileid).HasColumnName("fileid");

                entity.Property(e => e.Postid)
                    .HasColumnType("int(11)")
                    .HasColumnName("postid");

                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasColumnName("url");

                entity.Property(e => e.Userid)
                    .HasMaxLength(18)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("history");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("int(11)")
                    .HasColumnName("categoryid");

                entity.Property(e => e.Createdat)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdat");

                entity.Property(e => e.Hisid)
                    .HasColumnType("int(11)")
                    .HasColumnName("hisid");

                entity.Property(e => e.Subid)
                    .HasColumnType("int(11)")
                    .HasColumnName("subid");

                entity.Property(e => e.Teacherid)
                    .HasMaxLength(18)
                    .HasColumnName("teacherid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(18)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Legend>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("legends");

                entity.Property(e => e.Cardinal)
                    .HasColumnType("int(11)")
                    .HasColumnName("cardinal");

                entity.Property(e => e.Legendid)
                    .HasColumnType("int(11)")
                    .HasColumnName("legendid");

                entity.Property(e => e.Name)
                    .HasMaxLength(4)
                    .HasColumnName("name");

                entity.Property(e => e.Score)
                    .HasColumnType("int(11)")
                    .HasColumnName("score");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Notiid)
                    .HasName("PRIMARY");

                entity.ToTable("messages");

                entity.Property(e => e.Notiid)
                    .HasColumnType("int(11)")
                    .HasColumnName("notiid");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.Errors)
                    .HasColumnType("text")
                    .HasColumnName("errors");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .HasColumnName("phone");

                entity.Property(e => e.Requestedat)
                    .HasColumnType("timestamp")
                    .HasColumnName("requestedat")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Resolvedat)
                    .HasColumnType("timestamp")
                    .HasColumnName("resolvedat");

                entity.Property(e => e.Type)
                    .HasMaxLength(9)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Permid)
                    .HasName("PRIMARY");

                entity.ToTable("permissions");

                entity.Property(e => e.Permid)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("permid");

                entity.Property(e => e.Label)
                    .HasMaxLength(30)
                    .HasColumnName("label");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("posts");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("int(11)")
                    .HasColumnName("categoryid");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.Createdat)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdat");

                entity.Property(e => e.Postid)
                    .HasColumnType("int(11)")
                    .HasColumnName("postid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(18)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Roleid)
                    .HasColumnType("int(11)")
                    .HasColumnName("roleid");

                entity.Property(e => e.Label)
                    .HasMaxLength(30)
                    .HasColumnName("label");

                entity.Property(e => e.Perms)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("perms");
            });

            modelBuilder.Entity<Subcate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("subcate");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("int(11)")
                    .HasColumnName("categoryid");

                entity.Property(e => e.Label)
                    .HasMaxLength(150)
                    .HasColumnName("label");

                entity.Property(e => e.Score)
                    .HasColumnType("int(11)")
                    .HasColumnName("score");

                entity.Property(e => e.Subid)
                    .HasColumnType("int(11)")
                    .HasColumnName("subid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users");

                entity.Property(e => e.Cardinal)
                    .HasColumnType("int(11)")
                    .HasColumnName("cardinal");

                entity.Property(e => e.Depid)
                    .HasColumnType("int(11)")
                    .HasColumnName("depid");

                entity.Property(e => e.Name)
                    .HasMaxLength(4)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .HasColumnName("phone");

                entity.Property(e => e.Roles)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("roles");

                entity.Property(e => e.Salt)
                    .HasMaxLength(5)
                    .HasColumnName("salt")
                    .IsFixedLength();

                entity.Property(e => e.Userid)
                    .HasMaxLength(18)
                    .HasColumnName("userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
