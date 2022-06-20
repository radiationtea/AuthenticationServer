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
                optionsBuilder.UseMySql(System.IO.File.ReadAllText("db.txt"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.10-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("int(10) unsigned")
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
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("max_score");
            });

            modelBuilder.Entity<Depart>(entity =>
            {
                entity.HasKey(e => e.Depid)
                    .HasName("PRIMARY");

                entity.ToTable("departs");

                entity.Property(e => e.Depid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("depid");

                entity.Property(e => e.Desc)
                    .HasMaxLength(200)
                    .HasColumnName("desc");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("files");

                entity.Property(e => e.Fileid)
                    .HasColumnName("fileid")
                    .HasDefaultValueSql("uuid()");

                entity.Property(e => e.Postid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("postid");

                entity.Property(e => e.Url)
                    .HasColumnType("mediumtext")
                    .HasColumnName("url");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.Hisid)
                    .HasName("PRIMARY");

                entity.ToTable("history");

                entity.Property(e => e.Hisid)
                    .HasColumnType("int(11)")
                    .HasColumnName("hisid");

                entity.Property(e => e.Createdat)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("current_timestamp()");

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
                entity.ToTable("legends");

                entity.Property(e => e.Legendid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("legendid");

                entity.Property(e => e.Cardinal)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("cardinal");

                entity.Property(e => e.Name)
                    .HasMaxLength(4)
                    .HasColumnName("name");

                entity.Property(e => e.Score)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("score");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Msgid)
                    .HasName("PRIMARY");

                entity.ToTable("messages");

                entity.Property(e => e.Msgid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("msgid");

                entity.Property(e => e.Content)
                    .HasColumnType("mediumtext")
                    .HasColumnName("content");

                entity.Property(e => e.Errors)
                    .HasColumnType("mediumtext")
                    .HasColumnName("errors");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .HasColumnName("phone");

                entity.Property(e => e.Requestedat)
                    .HasColumnType("timestamp")
                    .HasColumnName("requestedat")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Resolvedat)
                    .HasColumnType("timestamp")
                    .HasColumnName("resolvedat");

                entity.Property(e => e.Type)
                    .HasMaxLength(9)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => new { e.Permid, e.Roleid })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("permissions");

                entity.Property(e => e.Permid)
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("permid");

                entity.Property(e => e.Roleid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("roleid");

                entity.Property(e => e.Label)
                    .HasMaxLength(30)
                    .HasColumnName("label");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.HasIndex(e => e.Subid, "FK_posts_subcate");

                entity.Property(e => e.Postid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("postid");

                entity.Property(e => e.Closed).HasColumnName("closed");

                entity.Property(e => e.Content)
                    .HasColumnType("mediumtext")
                    .HasColumnName("content");

                entity.Property(e => e.Createdat)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Subid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("subid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(18)
                    .HasColumnName("userid");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.Subid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_posts_subcate");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => new { e.Roleid, e.Userid })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("roles");

                entity.Property(e => e.Roleid)
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("roleid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(18)
                    .HasColumnName("userid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Label)
                    .HasMaxLength(30)
                    .HasColumnName("label");
            });

            modelBuilder.Entity<Subcate>(entity =>
            {
                entity.HasKey(e => e.Subid)
                    .HasName("PRIMARY");

                entity.ToTable("subcate");

                entity.Property(e => e.Subid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("subid");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("categoryid");

                entity.Property(e => e.Label)
                    .HasMaxLength(30)
                    .HasColumnName("label");

                entity.Property(e => e.Score)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("score");
            });

            modelBuilder.Entity<User>(entity =>
            {
                // entity.HasNoKey();

                entity.ToTable("users");

                entity.HasIndex(e => e.Depid, "FK_departs_TO_users_1");

                entity.Property(e => e.Cardinal)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("cardinal");

                entity.Property(e => e.Depid)
                    .HasColumnType("int(10) unsigned")
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

                entity.Property(e => e.Salt)
                    .HasMaxLength(5)
                    .HasColumnName("salt")
                    .IsFixedLength();

                entity.Property(e => e.Userid)
                    .HasMaxLength(18)
                    .HasColumnName("userid");

                entity.HasOne(d => d.Dep)
                    .WithMany()
                    .HasForeignKey(d => d.Depid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_departs_TO_users_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
