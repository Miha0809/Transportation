// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transportation.Services;

#nullable disable

namespace Transportation.Services.Migrations
{
    [DbContext(typeof(TransportationDbContext))]
    [Migration("20220925122750_V2")]
    partial class V2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Transportation.Models.CargoCompany.CargoCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("CargoCompanies");
                });

            modelBuilder.Entity("Transportation.Models.Customer.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("From")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Transportation.Models.Customer.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("HistoryCargoId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<int?>("SizeCargoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("HistoryCargoId");

                    b.HasIndex("SizeCargoId");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("Transportation.Models.Customer.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("RoleId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Transportation.Models.Customer.SizeCargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Height")
                        .HasColumnType("bigint");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<long>("Weight")
                        .HasColumnType("bigint");

                    b.Property<long>("Width")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("SizeCargo");
                });

            modelBuilder.Entity("Transportation.Models.Driver.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CargoCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("HistoryCargoId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CargoCompanyId");

                    b.HasIndex("CargoId");

                    b.HasIndex("HistoryCargoId");

                    b.HasIndex("RoleId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Transportation.Models.Driver.HistoryCargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("HistoryCargo");
                });

            modelBuilder.Entity("Transportation.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Transportation.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Transportation.Models.CargoCompany.CargoCompany", b =>
                {
                    b.HasOne("Transportation.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Transportation.Models.Customer.Cargo", b =>
                {
                    b.HasOne("Transportation.Models.Customer.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Transportation.Models.Driver.HistoryCargo", null)
                        .WithMany("Cargoes")
                        .HasForeignKey("HistoryCargoId");

                    b.HasOne("Transportation.Models.Customer.SizeCargo", "SizeCargo")
                        .WithMany()
                        .HasForeignKey("SizeCargoId");

                    b.Navigation("Address");

                    b.Navigation("SizeCargo");
                });

            modelBuilder.Entity("Transportation.Models.Customer.Customer", b =>
                {
                    b.HasOne("Transportation.Models.Customer.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId");

                    b.HasOne("Transportation.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Cargo");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Transportation.Models.Driver.Driver", b =>
                {
                    b.HasOne("Transportation.Models.CargoCompany.CargoCompany", "CargoCompany")
                        .WithMany()
                        .HasForeignKey("CargoCompanyId");

                    b.HasOne("Transportation.Models.Customer.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Transportation.Models.Driver.HistoryCargo", "HistoryCargo")
                        .WithMany()
                        .HasForeignKey("HistoryCargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Transportation.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Cargo");

                    b.Navigation("CargoCompany");

                    b.Navigation("HistoryCargo");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Transportation.Models.Driver.HistoryCargo", b =>
                {
                    b.Navigation("Cargoes");
                });
#pragma warning restore 612, 618
        }
    }
}
