// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MitraSolusiTelematika.Repositories;

namespace MitraSolusiTelematika.Migrations
{
    [DbContext(typeof(MstDbContext))]
    [Migration("20211213135332_initialsetup")]
    partial class initialsetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MitraSolusiTelematika.Models.KodePos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Kabupaten")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kecamatan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kelurahan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoKodePos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Propinsi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KodePos");
                });
#pragma warning restore 612, 618
        }
    }
}
