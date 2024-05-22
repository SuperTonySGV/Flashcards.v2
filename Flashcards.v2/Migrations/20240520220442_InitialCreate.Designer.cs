﻿// <auto-generated />
using System;
using Flashcards.v2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flashcards.v2.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240520220442_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Flashcards.v2.Models.Flashcard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Question")
                        .IsUnique();

                    b.HasIndex("StackId");

                    b.ToTable("Flashcards");
                });

            modelBuilder.Entity("Flashcards.v2.Models.Stack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Stacks");
                });

            modelBuilder.Entity("Flashcards.v2.Models.StudySession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("StackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StackId");

                    b.ToTable("StudySessions");
                });

            modelBuilder.Entity("Flashcards.v2.Models.Flashcard", b =>
                {
                    b.HasOne("Flashcards.v2.Models.Stack", "Stack")
                        .WithMany("Flashcards")
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stack");
                });

            modelBuilder.Entity("Flashcards.v2.Models.StudySession", b =>
                {
                    b.HasOne("Flashcards.v2.Models.Stack", "Stack")
                        .WithMany()
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stack");
                });

            modelBuilder.Entity("Flashcards.v2.Models.Stack", b =>
                {
                    b.Navigation("Flashcards");
                });
#pragma warning restore 612, 618
        }
    }
}
