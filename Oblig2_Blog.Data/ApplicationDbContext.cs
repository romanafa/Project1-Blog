using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2_Blog.Models.Entities;

namespace Oblig2_Blog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Blog>().ToTable("Blogs");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Tag>().ToTable("Tags");

            //Data seeding
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                    { 
                        Name = "Admin", 
                        NormalizedName = "ADMIN", 
                        Id = "65ecc8e1-4a98-4864-aa89-bc537f6e4618"
                    }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Blogger", 
                    NormalizedName = "BLOGGER", 
                    Id = "95a051e2-0a89-4429-9d3e-650a9dcbd296"
                }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Viewer", 
                    NormalizedName = "VIEWER", 
                    Id = "4af75c8c-29c0-4622-981b-edc13f4fdc56"
                }
            );

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                { 
                    Id = "f75ffe61-d2c7-44c2-9501-cf1f5d29d815",
                    Email = "admin@blog.no",
                    NormalizedEmail = "ADMIN@BLOG.NO",
                    UserName = "admin@blog.no",
                    NormalizedUserName = "ADMIN@BLOG.NO",
                    PasswordHash = hasher.HashPassword(null, "MyPassword123.")
                }
            );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "379bb5e6-6292-4e1e-8f84-47fec88eff93",
                    Email = "blogger@blog.no",
                    NormalizedEmail = "BLOGGER@BLOG.NO",
                    UserName = "blogger@blog.no",
                    NormalizedUserName = "BLOGGER@BLOG.NO",
                    PasswordHash = hasher.HashPassword(null, "MyPassword123.")
                }
            );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "607ee2ca-4326-43d8-b289-480a94013948",
                    Email = "foodBlogger@blog.no",
                    NormalizedEmail = "FOODBLOGGER@BLOG.NO",
                    UserName = "foodBlogger@blog.no",
                    NormalizedUserName = "FOODBLOGGER@BLOG.NO",
                    PasswordHash = hasher.HashPassword(null, "MyPassword123.")
                }
            );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "2a093558-1d1f-4c77-8422-aad80e5d168b",
                    Email = "travelBlogger@blog.no",
                    NormalizedEmail = "TRAVELBLOGGER@BLOG.NO",
                    UserName = "travelBlogger@blog.no",
                    NormalizedUserName = "TRAVELBLOGGER@BLOG.NO",
                    PasswordHash = hasher.HashPassword(null, "MyPassword123.")
                }
            );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "8aa46eaa-5727-42b7-8f3f-16a96d894658",
                    Email = "viewer@blog.no",
                    NormalizedEmail = "VIEWER@BLOG.NO",
                    UserName = "viewer@blog.no",
                    NormalizedUserName = "VIEWER@BLOG.NO",
                    PasswordHash = hasher.HashPassword(null, "MyPassword123.")
                }
            );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "fc7457a7-4715-4d7e-afe2-81f3357b73c5",
                    Email = "blogReader@blog.no",
                    NormalizedEmail = "BLOGREADER@BLOG.NO",
                    UserName = "blogReader@blog.no",
                    NormalizedUserName = "BLOGREADER@BLOG.NO",
                    PasswordHash = hasher.HashPassword(null, "MyPassword123.")
                }
            );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "11d43a25-64c8-45db-8208-d578dc9b03e1",
                    Email = "travel_addict@blog.no",
                    NormalizedEmail = "TRAVEL_ADDICT@BLOG.NO",
                    UserName = "travel_addict@blog.no",
                    NormalizedUserName = "TRAVEL_ADDICT@BLOG.NO",
                    PasswordHash = hasher.HashPassword(null, "MyPassword123.")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "65ecc8e1-4a98-4864-aa89-bc537f6e4618", 
                    UserId = "f75ffe61-d2c7-44c2-9501-cf1f5d29d815"
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "95a051e2-0a89-4429-9d3e-650a9dcbd296",
                    UserId = "379bb5e6-6292-4e1e-8f84-47fec88eff93"
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "95a051e2-0a89-4429-9d3e-650a9dcbd296",
                    UserId = "607ee2ca-4326-43d8-b289-480a94013948"
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "95a051e2-0a89-4429-9d3e-650a9dcbd296",
                    UserId = "2a093558-1d1f-4c77-8422-aad80e5d168b"
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4af75c8c-29c0-4622-981b-edc13f4fdc56",
                    UserId = "8aa46eaa-5727-42b7-8f3f-16a96d894658"
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4af75c8c-29c0-4622-981b-edc13f4fdc56",
                    UserId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5"
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4af75c8c-29c0-4622-981b-edc13f4fdc56",
                    UserId = "11d43a25-64c8-45db-8208-d578dc9b03e1"
                }
            );


            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    BlogTitle = "Blogg om katter", 
                    Description = "Velkommen til min blogg ", 
                    BlogId = 1, CanPost = true, 
                    Created = new DateTime(2022, 02, 13),
                    OwnerId = "379bb5e6-6292-4e1e-8f84-47fec88eff93"
                }
            );
            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    BlogTitle = "Matblogg",
                    Description = "Oppskrifter", 
                    BlogId = 2, CanPost = false, 
                    Created = new DateTime(2022, 05, 28),
                    OwnerId = "607ee2ca-4326-43d8-b289-480a94013948"
                }
            );
            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    BlogTitle = "Norgesferie", 
                    Description = "Alt om reise i Norge", 
                    BlogId = 3, CanPost = true, 
                    Created = new DateTime(2022, 06, 04),
                    OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"
                }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    PostTitle = "Roadtrip til Lofoten", 
                    PostText = "Lofoten byr på uforglemmelige naturopplevelser.", 
                    PostId = 2, 
                    BlogId = 3, 
                    Created = new DateTime(2022, 06, 04),
                    OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"
                }
            );
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    PostTitle = "Nordlys i Narvik", 
                    PostText = "Se nordlys fra Narvik!", 
                    PostId = 3, 
                    BlogId = 3, 
                    Created = new DateTime(2022, 06, 05),
                    OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"
                }
            );
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    PostTitle = "Bengal katter", 
                    PostText = "En bengal er en selvsikker, heller dominerende og aktiv katt.", 
                    PostId = 1, 
                    BlogId = 1, 
                    Created = new DateTime(2022, 02, 13),
                    OwnerId = "379bb5e6-6292-4e1e-8f84-47fec88eff93"
                }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    CommentText="Det er fint i Lofoten", 
                    CommentId = 1, 
                    PostId = 2,
                    Created = new DateTime(2022, 06, 05),
                    OwnerId = "8aa46eaa-5727-42b7-8f3f-16a96d894658"
                }
            );
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    CommentText = "Helt enig!!", 
                    CommentId = 2, 
                    PostId = 2, 
                    Created = new DateTime(2022, 06, 21),
                    OwnerId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5"
                }
            );
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    CommentText = "Jeg reiser til Lofoten neste år",
                    CommentId = 3,
                    PostId = 2,
                    Created = new DateTime(2022, 09, 14),
                    OwnerId = "11d43a25-64c8-45db-8208-d578dc9b03e1"
                }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "dyr",
                    TagId = 1
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "katt",
                    TagId = 2
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "reise",
                    TagId = 3
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "ferie",
                    TagId = 4
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "Norge",
                    TagId = 5
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "nordlys",
                    TagId = 6
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "natur",
                    TagId = 7
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "biltur",
                    TagId = 8
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "sommer",
                    TagId = 9
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "tips",
                    TagId = 10
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "narvik",
                    TagId = 11
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "bengal",
                    TagId = 12
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "bengalkatter",
                    TagId = 13
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "lofoten",
                    TagId = 14
                }
            );
        }

    }
}