using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NCARB.EesaService.Infrastructure;

namespace src.Migrations
{
    [DbContext(typeof(ApplicantContext))]
    [Migration("20161019171101_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("NCARB.EesaService.Entities.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("NCARB.EesaService.Entities.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("NCARB.EesaService.Entities.Catagory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AreaId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Catagory");
                });

            modelBuilder.Entity("NCARB.EesaService.Entities.Deficiency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicantId");

                    b.Property<int?>("CategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Deficiency");
                });

            modelBuilder.Entity("NCARB.EesaService.Entities.Catagory", b =>
                {
                    b.HasOne("NCARB.EesaService.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");
                });

            modelBuilder.Entity("NCARB.EesaService.Entities.Deficiency", b =>
                {
                    b.HasOne("NCARB.EesaService.Entities.Applicant")
                        .WithMany("Deficiencies")
                        .HasForeignKey("ApplicantId");

                    b.HasOne("NCARB.EesaService.Entities.Catagory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });
        }
    }
}
