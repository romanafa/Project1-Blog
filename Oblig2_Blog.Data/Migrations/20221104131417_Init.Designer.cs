﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oblig2_Blog.Data;

#nullable disable

namespace Oblig2_Blog.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221104131417_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "65ecc8e1-4a98-4864-aa89-bc537f6e4618",
                            ConcurrencyStamp = "4017a978-e150-4283-b152-95f92ae1ac2c",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "95a051e2-0a89-4429-9d3e-650a9dcbd296",
                            ConcurrencyStamp = "5f2dbaeb-82b6-4a6c-b46e-89e758a18dbe",
                            Name = "Blogger",
                            NormalizedName = "BLOGGER"
                        },
                        new
                        {
                            Id = "4af75c8c-29c0-4622-981b-edc13f4fdc56",
                            ConcurrencyStamp = "e1b430cf-a141-4421-b847-3be82f667d11",
                            Name = "Viewer",
                            NormalizedName = "VIEWER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f75ffe61-d2c7-44c2-9501-cf1f5d29d815",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "19e8ffa9-6935-4a82-9445-538b0149fe91",
                            Email = "admin@blog.no",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@BLOG.NO",
                            NormalizedUserName = "ADMIN@BLOG.NO",
                            PasswordHash = "AQAAAAEAACcQAAAAELM/YWJfnz4U7nMzTSuyNrzVpam1y6p5U1G9VMF4tfNdhKiNp/8jV58QGLETOzQR3g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e3a98919-a521-4089-9172-bb2b7069a949",
                            TwoFactorEnabled = false,
                            UserName = "admin@blog.no"
                        },
                        new
                        {
                            Id = "379bb5e6-6292-4e1e-8f84-47fec88eff93",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b862cb5d-927a-40a9-88b1-6fff9c5790ba",
                            Email = "blogger@blog.no",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "BLOGGER@BLOG.NO",
                            NormalizedUserName = "BLOGGER@BLOG.NO",
                            PasswordHash = "AQAAAAEAACcQAAAAEBtzkHwyiLLAU3cvN/Xb5Hrto3bVdRm1dnp1qDwek4O3nqNuvrvfI3ol3MV10fEX2w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c2447846-e71d-47ac-8030-6868313e57fe",
                            TwoFactorEnabled = false,
                            UserName = "blogger@blog.no"
                        },
                        new
                        {
                            Id = "607ee2ca-4326-43d8-b289-480a94013948",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "715bc308-4459-48a2-b2c5-2644fa392d3c",
                            Email = "foodBlogger@blog.no",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "FOODBLOGGER@BLOG.NO",
                            NormalizedUserName = "FOODBLOGGER@BLOG.NO",
                            PasswordHash = "AQAAAAEAACcQAAAAEGh5eWUKTGBSJFJPoqng70F3c7JB+QL7p6nWYUPSgxkZ1D1VEEJB+MQkiqwPl0t1Ug==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d097cf7e-ba05-44fa-94e1-baed19cf700e",
                            TwoFactorEnabled = false,
                            UserName = "foodBlogger@blog.no"
                        },
                        new
                        {
                            Id = "2a093558-1d1f-4c77-8422-aad80e5d168b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "8707c39b-23fe-47e9-a7fc-e76cf045b88a",
                            Email = "travelBlogger@blog.no",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "TRAVELBLOGGER@BLOG.NO",
                            NormalizedUserName = "TRAVELBLOGGER@BLOG.NO",
                            PasswordHash = "AQAAAAEAACcQAAAAEIz7k4AP/ASUt/3I5HIoCtAKBH0oV2UWqaqyRDoiUPK0zD+0380RDq+DVqC6TR+BLA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c90b6ea4-3a5e-46ed-b8b4-1a1620874bd7",
                            TwoFactorEnabled = false,
                            UserName = "travelBlogger@blog.no"
                        },
                        new
                        {
                            Id = "8aa46eaa-5727-42b7-8f3f-16a96d894658",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1b175b12-2984-4535-98b0-e1bf13cf2b77",
                            Email = "viewer@blog.no",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "VIEWER@BLOG.NO",
                            NormalizedUserName = "VIEWER@BLOG.NO",
                            PasswordHash = "AQAAAAEAACcQAAAAEK4EUdCdf7uRCAsFufDTSbm6QlGYoO2RnhCT+fgicDvBOQnkY77+fQBn8Xz05AwWWA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8e6583fd-4fba-44f1-851d-64fb8a5e73a6",
                            TwoFactorEnabled = false,
                            UserName = "viewer@blog.no"
                        },
                        new
                        {
                            Id = "fc7457a7-4715-4d7e-afe2-81f3357b73c5",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "447e13a0-bf64-43ea-b9f3-2455057f5fdd",
                            Email = "blogReader@blog.no",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "BLOGREADER@BLOG.NO",
                            NormalizedUserName = "BLOGREADER@BLOG.NO",
                            PasswordHash = "AQAAAAEAACcQAAAAEIcbXg7fQ/vN6k3oqgUJ3gbSFq/LR/7/H9URyO6afKDPjysCfqvARCgImHIAO2EG2Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "023f88ad-5e87-45ee-b022-b3b7b93ca054",
                            TwoFactorEnabled = false,
                            UserName = "blogReader@blog.no"
                        },
                        new
                        {
                            Id = "11d43a25-64c8-45db-8208-d578dc9b03e1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "212ffc21-e9be-4192-807f-1fc5ed27e39f",
                            Email = "travel_addict@blog.no",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "TRAVEL_ADDICT@BLOG.NO",
                            NormalizedUserName = "TRAVEL_ADDICT@BLOG.NO",
                            PasswordHash = "AQAAAAEAACcQAAAAEL2Z+au+7fPjbhoqNGnghoamZrxUgJWEr+pM8K73MRksH5E7N+Wun8mNqhJ+f0JpXA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2a18d30e-b575-4574-aa93-1bef745402ab",
                            TwoFactorEnabled = false,
                            UserName = "travel_addict@blog.no"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "f75ffe61-d2c7-44c2-9501-cf1f5d29d815",
                            RoleId = "65ecc8e1-4a98-4864-aa89-bc537f6e4618"
                        },
                        new
                        {
                            UserId = "379bb5e6-6292-4e1e-8f84-47fec88eff93",
                            RoleId = "95a051e2-0a89-4429-9d3e-650a9dcbd296"
                        },
                        new
                        {
                            UserId = "607ee2ca-4326-43d8-b289-480a94013948",
                            RoleId = "95a051e2-0a89-4429-9d3e-650a9dcbd296"
                        },
                        new
                        {
                            UserId = "2a093558-1d1f-4c77-8422-aad80e5d168b",
                            RoleId = "95a051e2-0a89-4429-9d3e-650a9dcbd296"
                        },
                        new
                        {
                            UserId = "8aa46eaa-5727-42b7-8f3f-16a96d894658",
                            RoleId = "4af75c8c-29c0-4622-981b-edc13f4fdc56"
                        },
                        new
                        {
                            UserId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5",
                            RoleId = "4af75c8c-29c0-4622-981b-edc13f4fdc56"
                        },
                        new
                        {
                            UserId = "11d43a25-64c8-45db-8208-d578dc9b03e1",
                            RoleId = "4af75c8c-29c0-4622-981b-edc13f4fdc56"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"), 1L, 1);

                    b.Property<string>("BlogTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CanPost")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BlogId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Blogs", (string)null);

                    b.HasData(
                        new
                        {
                            BlogId = 1,
                            BlogTitle = "Blogg om katter",
                            CanPost = true,
                            Created = new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Velkommen til min blogg ",
                            OwnerId = "379bb5e6-6292-4e1e-8f84-47fec88eff93"
                        },
                        new
                        {
                            BlogId = 2,
                            BlogTitle = "Matblogg",
                            CanPost = false,
                            Created = new DateTime(2022, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Oppskrifter",
                            OwnerId = "607ee2ca-4326-43d8-b289-480a94013948"
                        },
                        new
                        {
                            BlogId = 3,
                            BlogTitle = "Norgesferie",
                            CanPost = true,
                            Created = new DateTime(2022, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Alt om reise i Norge",
                            OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"
                        });
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<int?>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("BlogId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PostId");

                    b.ToTable("Comment", (string)null);

                    b.HasData(
                        new
                        {
                            CommentId = 1,
                            CommentText = "Det er fint i Lofoten",
                            Created = new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OwnerId = "8aa46eaa-5727-42b7-8f3f-16a96d894658",
                            PostId = 2
                        },
                        new
                        {
                            CommentId = 2,
                            CommentText = "Helt enig!!",
                            Created = new DateTime(2022, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OwnerId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5",
                            PostId = 2
                        },
                        new
                        {
                            CommentId = 3,
                            CommentText = "Jeg reiser til Lofoten neste år",
                            Created = new DateTime(2022, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OwnerId = "11d43a25-64c8-45db-8208-d578dc9b03e1",
                            PostId = 2
                        });
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<int?>("BlogId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Posts", (string)null);

                    b.HasData(
                        new
                        {
                            PostId = 2,
                            BlogId = 3,
                            Created = new DateTime(2022, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b",
                            PostText = "Lofoten byr på uforglemmelige naturopplevelser.",
                            PostTitle = "Roadtrip til Lofoten"
                        },
                        new
                        {
                            PostId = 3,
                            BlogId = 3,
                            Created = new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b",
                            PostText = "Se nordlys fra Narvik!",
                            PostTitle = "Nordlys i Narvik"
                        },
                        new
                        {
                            PostId = 1,
                            BlogId = 1,
                            Created = new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OwnerId = "379bb5e6-6292-4e1e-8f84-47fec88eff93",
                            PostText = "En bengal er en selvsikker, heller dominerende og aktiv katt.",
                            PostTitle = "Bengal katter"
                        });
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags", (string)null);

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            TagName = "dyr"
                        },
                        new
                        {
                            TagId = 2,
                            TagName = "katt"
                        },
                        new
                        {
                            TagId = 3,
                            TagName = "reise"
                        },
                        new
                        {
                            TagId = 4,
                            TagName = "ferie"
                        },
                        new
                        {
                            TagId = 5,
                            TagName = "Norge"
                        },
                        new
                        {
                            TagId = 6,
                            TagName = "nordlys"
                        },
                        new
                        {
                            TagId = 7,
                            TagName = "natur"
                        },
                        new
                        {
                            TagId = 8,
                            TagName = "biltur"
                        },
                        new
                        {
                            TagId = 9,
                            TagName = "sommer"
                        },
                        new
                        {
                            TagId = 10,
                            TagName = "tips"
                        },
                        new
                        {
                            TagId = 11,
                            TagName = "narvik"
                        },
                        new
                        {
                            TagId = 12,
                            TagName = "bengal"
                        },
                        new
                        {
                            TagId = 13,
                            TagName = "bengalkatter"
                        },
                        new
                        {
                            TagId = 14,
                            TagName = "lofoten"
                        });
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<int>("PostsPostId")
                        .HasColumnType("int");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.HasKey("PostsPostId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Blog", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Comment", b =>
                {
                    b.HasOne("Oblig2_Blog.Models.Entities.Blog", null)
                        .WithMany("Comments")
                        .HasForeignKey("BlogId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("Oblig2_Blog.Models.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Post", b =>
                {
                    b.HasOne("Oblig2_Blog.Models.Entities.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Blog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("Oblig2_Blog.Models.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Oblig2_Blog.Models.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Blog", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Oblig2_Blog.Models.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
