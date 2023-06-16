using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

public partial class ChatContext : DbContext
{
    public ChatContext()
    {
    }

    public ChatContext(DbContextOptions<ChatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<ChatRoomUserMapping> ChatRoomUserMappings { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ImageSet> ImageSets { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NGYNMINHVU\\SQLEXPRESS;Initial Catalog=Chat;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.Password).HasMaxLength(400);
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.ToTable("ChatRoom");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.Admin).HasMaxLength(400);
            entity.Property(e => e.Creator).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.Secret).HasMaxLength(400);
        });

        modelBuilder.Entity<ChatRoomUserMapping>(entity =>
        {
            entity.ToTable("ChatRoomUserMapping");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.AccId).HasMaxLength(400);
            entity.Property(e => e.ChatRoomId).HasMaxLength(400);

            entity.HasOne(d => d.Acc).WithMany(p => p.ChatRoomUserMappings)
                .HasForeignKey(d => d.AccId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatRoomUserMapping_Account");

            entity.HasOne(d => d.ChatRoom).WithMany(p => p.ChatRoomUserMappings)
                .HasForeignKey(d => d.ChatRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatRoomUserMapping_ChatRoom");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.ImageSetId).HasMaxLength(400);
            entity.Property(e => e.ImageUrl).HasMaxLength(400);

            entity.HasOne(d => d.ImageSet).WithMany(p => p.Images)
                .HasForeignKey(d => d.ImageSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Image_ImageSet");
        });

        modelBuilder.Entity<ImageSet>(entity =>
        {
            entity.ToTable("ImageSet");

            entity.Property(e => e.Id).HasMaxLength(400);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.ChatRoomId).HasMaxLength(400);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ImageSetId).HasMaxLength(400);
            entity.Property(e => e.Message1)
                .HasMaxLength(700)
                .HasColumnName("Message");

            entity.HasOne(d => d.ChatRoom).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_ChatRoom");

            entity.HasOne(d => d.ImageSet).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ImageSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_ImageSet");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.AccId).HasMaxLength(400);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Fullname).HasMaxLength(400);
            entity.Property(e => e.ImageSetId).HasMaxLength(400);

            entity.HasOne(d => d.Acc).WithMany(p => p.Users)
                .HasForeignKey(d => d.AccId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Account");

            entity.HasOne(d => d.ImageSet).WithMany(p => p.Users)
                .HasForeignKey(d => d.ImageSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_ImageSet");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
