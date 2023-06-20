﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.EfCore;

#nullable disable

namespace ToDoList.Migrations
{
    [DbContext(typeof(ToDoContext))]
    [Migration("20230604142412_Messages")]
    partial class Messages
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ToDoList.Models.ActivityLogger", b =>
                {
                    b.Property<int>("ActivityLoggerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityLoggerId"));

                    b.Property<string>("ActivityPerformed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ActivityLoggerId");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityLogger");
                });

            modelBuilder.Entity("ToDoList.Models.LoggerTable", b =>
                {
                    b.Property<int>("LoggerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoggerId"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoggerId");

                    b.ToTable("LoggerTable");
                });

            modelBuilder.Entity("ToDoList.Models.Messaging", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FKSenderId")
                        .HasColumnType("int");

                    b.Property<Guid>("MsgUniqueId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("FKSenderId");

                    b.ToTable("Messaging");
                });

            modelBuilder.Entity("ToDoList.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ToDoList.Models.TaskHistory", b =>
                {
                    b.Property<int>("TaskHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskHistoryId"));

                    b.Property<int>("FKTaskAssignedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("FKTaskId")
                        .HasColumnType("int");

                    b.Property<int>("TaskAssignedToUserId")
                        .HasColumnType("int");

                    b.Property<int?>("TaskTableTaskId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TaskHistoryId");

                    b.HasIndex("TaskTableTaskId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskHistory");
                });

            modelBuilder.Entity("ToDoList.Models.TaskTable", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<int?>("AssignedToUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FKCreatedByUserId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskId");

                    b.HasIndex("FKCreatedByUserId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("ToDoList.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("FKRoleId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("FKRoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ToDoList.Models.ActivityLogger", b =>
                {
                    b.HasOne("ToDoList.Models.User", "User")
                        .WithMany("ActivityLogger")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Models.Messaging", b =>
                {
                    b.HasOne("ToDoList.Models.User", "User")
                        .WithMany("Message")
                        .HasForeignKey("FKSenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Models.TaskHistory", b =>
                {
                    b.HasOne("ToDoList.Models.TaskTable", "TaskTable")
                        .WithMany("TaskHistorys")
                        .HasForeignKey("TaskTableTaskId");

                    b.HasOne("ToDoList.Models.User", "User")
                        .WithMany("TaskHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("TaskTable");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Models.TaskTable", b =>
                {
                    b.HasOne("ToDoList.Models.User", "User")
                        .WithMany("TaskTable")
                        .HasForeignKey("FKCreatedByUserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Models.User", b =>
                {
                    b.HasOne("ToDoList.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("FKRoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ToDoList.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ToDoList.Models.TaskTable", b =>
                {
                    b.Navigation("TaskHistorys");
                });

            modelBuilder.Entity("ToDoList.Models.User", b =>
                {
                    b.Navigation("ActivityLogger");

                    b.Navigation("Message");

                    b.Navigation("TaskHistory");

                    b.Navigation("TaskTable");
                });
#pragma warning restore 612, 618
        }
    }
}
