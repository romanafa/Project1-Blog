using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oblig2_Blog.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlogTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanPost = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId");
                    table.ForeignKey(
                        name: "FK_Comment_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId");
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsPostId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsPostId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4af75c8c-29c0-4622-981b-edc13f4fdc56", "e1b430cf-a141-4421-b847-3be82f667d11", "Viewer", "VIEWER" },
                    { "65ecc8e1-4a98-4864-aa89-bc537f6e4618", "4017a978-e150-4283-b152-95f92ae1ac2c", "Admin", "ADMIN" },
                    { "95a051e2-0a89-4429-9d3e-650a9dcbd296", "5f2dbaeb-82b6-4a6c-b46e-89e758a18dbe", "Blogger", "BLOGGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "11d43a25-64c8-45db-8208-d578dc9b03e1", 0, "212ffc21-e9be-4192-807f-1fc5ed27e39f", "travel_addict@blog.no", false, false, null, "TRAVEL_ADDICT@BLOG.NO", "TRAVEL_ADDICT@BLOG.NO", "AQAAAAEAACcQAAAAEL2Z+au+7fPjbhoqNGnghoamZrxUgJWEr+pM8K73MRksH5E7N+Wun8mNqhJ+f0JpXA==", null, false, "2a18d30e-b575-4574-aa93-1bef745402ab", false, "travel_addict@blog.no" },
                    { "2a093558-1d1f-4c77-8422-aad80e5d168b", 0, "8707c39b-23fe-47e9-a7fc-e76cf045b88a", "travelBlogger@blog.no", false, false, null, "TRAVELBLOGGER@BLOG.NO", "TRAVELBLOGGER@BLOG.NO", "AQAAAAEAACcQAAAAEIz7k4AP/ASUt/3I5HIoCtAKBH0oV2UWqaqyRDoiUPK0zD+0380RDq+DVqC6TR+BLA==", null, false, "c90b6ea4-3a5e-46ed-b8b4-1a1620874bd7", false, "travelBlogger@blog.no" },
                    { "379bb5e6-6292-4e1e-8f84-47fec88eff93", 0, "b862cb5d-927a-40a9-88b1-6fff9c5790ba", "blogger@blog.no", false, false, null, "BLOGGER@BLOG.NO", "BLOGGER@BLOG.NO", "AQAAAAEAACcQAAAAEBtzkHwyiLLAU3cvN/Xb5Hrto3bVdRm1dnp1qDwek4O3nqNuvrvfI3ol3MV10fEX2w==", null, false, "c2447846-e71d-47ac-8030-6868313e57fe", false, "blogger@blog.no" },
                    { "607ee2ca-4326-43d8-b289-480a94013948", 0, "715bc308-4459-48a2-b2c5-2644fa392d3c", "foodBlogger@blog.no", false, false, null, "FOODBLOGGER@BLOG.NO", "FOODBLOGGER@BLOG.NO", "AQAAAAEAACcQAAAAEGh5eWUKTGBSJFJPoqng70F3c7JB+QL7p6nWYUPSgxkZ1D1VEEJB+MQkiqwPl0t1Ug==", null, false, "d097cf7e-ba05-44fa-94e1-baed19cf700e", false, "foodBlogger@blog.no" },
                    { "8aa46eaa-5727-42b7-8f3f-16a96d894658", 0, "1b175b12-2984-4535-98b0-e1bf13cf2b77", "viewer@blog.no", false, false, null, "VIEWER@BLOG.NO", "VIEWER@BLOG.NO", "AQAAAAEAACcQAAAAEK4EUdCdf7uRCAsFufDTSbm6QlGYoO2RnhCT+fgicDvBOQnkY77+fQBn8Xz05AwWWA==", null, false, "8e6583fd-4fba-44f1-851d-64fb8a5e73a6", false, "viewer@blog.no" },
                    { "f75ffe61-d2c7-44c2-9501-cf1f5d29d815", 0, "19e8ffa9-6935-4a82-9445-538b0149fe91", "admin@blog.no", false, false, null, "ADMIN@BLOG.NO", "ADMIN@BLOG.NO", "AQAAAAEAACcQAAAAELM/YWJfnz4U7nMzTSuyNrzVpam1y6p5U1G9VMF4tfNdhKiNp/8jV58QGLETOzQR3g==", null, false, "e3a98919-a521-4089-9172-bb2b7069a949", false, "admin@blog.no" },
                    { "fc7457a7-4715-4d7e-afe2-81f3357b73c5", 0, "447e13a0-bf64-43ea-b9f3-2455057f5fdd", "blogReader@blog.no", false, false, null, "BLOGREADER@BLOG.NO", "BLOGREADER@BLOG.NO", "AQAAAAEAACcQAAAAEIcbXg7fQ/vN6k3oqgUJ3gbSFq/LR/7/H9URyO6afKDPjysCfqvARCgImHIAO2EG2Q==", null, false, "023f88ad-5e87-45ee-b022-b3b7b93ca054", false, "blogReader@blog.no" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "TagName" },
                values: new object[,]
                {
                    { 1, "dyr" },
                    { 2, "katt" },
                    { 3, "reise" },
                    { 4, "ferie" },
                    { 5, "Norge" },
                    { 6, "nordlys" },
                    { 7, "natur" },
                    { 8, "biltur" },
                    { 9, "sommer" },
                    { 10, "tips" },
                    { 11, "narvik" },
                    { 12, "bengal" },
                    { 13, "bengalkatter" },
                    { 14, "lofoten" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4af75c8c-29c0-4622-981b-edc13f4fdc56", "11d43a25-64c8-45db-8208-d578dc9b03e1" },
                    { "95a051e2-0a89-4429-9d3e-650a9dcbd296", "2a093558-1d1f-4c77-8422-aad80e5d168b" },
                    { "95a051e2-0a89-4429-9d3e-650a9dcbd296", "379bb5e6-6292-4e1e-8f84-47fec88eff93" },
                    { "95a051e2-0a89-4429-9d3e-650a9dcbd296", "607ee2ca-4326-43d8-b289-480a94013948" },
                    { "4af75c8c-29c0-4622-981b-edc13f4fdc56", "8aa46eaa-5727-42b7-8f3f-16a96d894658" },
                    { "65ecc8e1-4a98-4864-aa89-bc537f6e4618", "f75ffe61-d2c7-44c2-9501-cf1f5d29d815" },
                    { "4af75c8c-29c0-4622-981b-edc13f4fdc56", "fc7457a7-4715-4d7e-afe2-81f3357b73c5" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "BlogTitle", "CanPost", "Created", "Description", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Blogg om katter", true, new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Velkommen til min blogg ", "379bb5e6-6292-4e1e-8f84-47fec88eff93" },
                    { 2, "Matblogg", false, new DateTime(2022, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oppskrifter", "607ee2ca-4326-43d8-b289-480a94013948" },
                    { 3, "Norgesferie", true, new DateTime(2022, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alt om reise i Norge", "2a093558-1d1f-4c77-8422-aad80e5d168b" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "BlogId", "Created", "OwnerId", "PostText", "PostTitle" },
                values: new object[] { 1, 1, new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "379bb5e6-6292-4e1e-8f84-47fec88eff93", "En bengal er en selvsikker, heller dominerende og aktiv katt.", "Bengal katter" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "BlogId", "Created", "OwnerId", "PostText", "PostTitle" },
                values: new object[] { 2, 3, new DateTime(2022, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "2a093558-1d1f-4c77-8422-aad80e5d168b", "Lofoten byr på uforglemmelige naturopplevelser.", "Roadtrip til Lofoten" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "BlogId", "Created", "OwnerId", "PostText", "PostTitle" },
                values: new object[] { 3, 3, new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2a093558-1d1f-4c77-8422-aad80e5d168b", "Se nordlys fra Narvik!", "Nordlys i Narvik" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "BlogId", "CommentText", "Created", "OwnerId", "PostId" },
                values: new object[] { 1, null, "Det er fint i Lofoten", new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "8aa46eaa-5727-42b7-8f3f-16a96d894658", 2 });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "BlogId", "CommentText", "Created", "OwnerId", "PostId" },
                values: new object[] { 2, null, "Helt enig!!", new DateTime(2022, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "fc7457a7-4715-4d7e-afe2-81f3357b73c5", 2 });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "BlogId", "CommentText", "Created", "OwnerId", "PostId" },
                values: new object[] { 3, null, "Jeg reiser til Lofoten neste år", new DateTime(2022, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "11d43a25-64c8-45db-8208-d578dc9b03e1", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_OwnerId",
                table: "Blogs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BlogId",
                table: "Comment",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OwnerId",
                table: "Posts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsTagId",
                table: "PostTag",
                column: "TagsTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
