using AIMY.Db.Prototype.Api.Infrastructure;
using AIMY.Db.Prototype.Infrastructure.Context;
using AIMY.Db.Prototype.Infrastructure.Entities;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Amazon.S3.Util.S3EventNotification;

namespace AIMY.Db.Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController(MyDbContext context) : ControllerBase
    {
        [HttpPost("GenerateUsers")]
        public async Task<IActionResult> GenerateUsers([FromBody] int numberOfUsers)
        {
            try
            {

                var mohamed = context.Users.ExecuteDelete();

                var mohamedId = UUIDv7Generator.NewUuid();
                var userData = new List<User>()
                {
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.mahfouz@flairstech.com", Name = "Ahmed Mahfouz", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.youssef@flairstech.com", Name = "Ahmed Hashem Abdelghani Youssef", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "salma.ehab@flairstech.com", Name = "Salma Ehab", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "marina.tawfik@flairstech.com", Name = "Marina Tawfik", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "abduallah.samir@flairstech.com", Name = "Abduallah Samir Hamad Allah Abdelrahman", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "shaimaa.sayed@flairstech.com", Name = "Shaimaa Fawzy Sayed", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "abdo.nasser@flairstech.com", Name = "Abdo Nasser Abdo Ahmed", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "mohamed.abdelaty@flairstech.com", Name = "Mohamed Tarek Abdelaty", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.mah66fouz@flairstech.com", Name = "Ahmed Mahfouz", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "mennatalla.osman@flairstech.com", Name = "Mennatalla Mostafa Hussein Osman", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "mahmoud.khalifa@flairstech.com", Name = "Mahmoud Mekawy Khalifa", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "mohamed.shokry@flairstech.com", Name = "Mohamed Hani Shokry", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "mohamed.fadl@flairstech.com", Name = "Mohamed Nabil Fadl", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "passant.azkalani@flairstech.com", Name = "Passant Waleed Azkalani", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "aya.abozeid@flairstech.com", Name = "Aya Abozeid", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "omar.nawar@flairstech.com", Name = "Omar Nawar", CreatedBy = mohamedId },
                    new() { Id = mohamedId, Email = "mohamed.abdella@flairstech.com", Name = "Mohamed Hassan Ahmed Abdella", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "clara.wagdy@flairstech.com", Name = "Clara Wagdy Samuel", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "engy.yasser@flairstech.com", Name = "Engy Yasser Elsayed Elsayed", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "ahmed.elassi@flairstech.com", Name = "Ahmed Elassi", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "habeba.kamel@flairstech.com", Name = "Habeba Kamel", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "mostafa.hassan@flairstech.com", Name = "Mostafa Hassan", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "nadine.elsayed@flairstech.com", Name = "Nadine El Sayed", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "samaa.magdy@flairstech.com", Name = "Samaa Mohamed Magdy", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "samatorab@gmail.com", Name = "Samaa Torab", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "abdelrahman.fathy@flairstech.com", Name = "Abdelrahman Mohamed Fathy Tawfik", CreatedBy = mohamedId },
                    new() { Id = UUIDv7Generator.NewUuid(), Email = "esraa.behery@flairstech.com", Name = "Esraa Behery Gafar Ahmed Shaheen", CreatedBy = mohamedId }
                };

                // First, drop the unique constraint on email
                var result1 = context.Database.ExecuteSql($"ALTER TABLE \"auth\".users DROP CONSTRAINT IF EXISTS users_email_key;");

                // Now drop the index on email (which should work since the constraint has been removed)
                var result2 = context.Database.ExecuteSql($"DROP INDEX IF EXISTS \"auth\".idx_users_email;");


                // Insert the user data
                context.Users.AddRange(userData);
                await context.SaveChangesAsync();

                // Recreate the indexes
                // First, recreate the regular index
                var result3 = context.Database.ExecuteSql($"CREATE INDEX idx_users_email ON \"auth\".users (email);");

                // Then recreate the unique constraint (which will automatically create a unique index)
                var result4 = context.Database.ExecuteSql($"ALTER TABLE \"auth\".users ADD CONSTRAINT users_email_key UNIQUE (email);");

                return Ok(new
                {
                    Message = $"{userData.Count} users generated successfully.",
                    Users = userData.Select(u => new { u.Id, u.Email, u.Name }).ToList()
                });
            }
            catch (Exception ex)
            {
                // Return error details for troubleshooting
                return StatusCode(500, new
                {
                    Error = "An error occurred while generating users",
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }


        [HttpPost("CreateSuperAdminRole")]
        public async Task<IActionResult> CreateSuperAdminRole(Guid appId)
        {
            try
            {
                var allRulesDeleted = context.Roles.ExecuteDelete();

                var superAdminRole = new Role
                {
                    Id = UUIDv7Generator.NewUuid(),
                    Name = "Super Admin",
                    Key = "super_admin",
                    AppId = appId, // Assuming a system app ID for the role
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
                    UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update
                };

                context.Roles.Add(superAdminRole);

                await context.SaveChangesAsync();


                return Ok(new { Message = "Super Admin role created successfully", RoleId = superAdminRole.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while creating the Super Admin role", Message = ex.Message });
            }
        }

        [HttpPost("CreateAIMYQAApp")]
        public async Task<IActionResult> CreateAIMYQAApp()
        {
            try
            {
                var allRolesDeleted = context.Roles.ExecuteDelete();
                var allAppsDeleted = context.Apps.ExecuteDelete();

                var app = new App
                {
                    Id = UUIDv7Generator.NewUuid(), // Assuming a system app ID
                    Name = "AIMY QA",
                    Key = "aimy_qa",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
                    UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update
                };


                // Drop the unique constraint on app key if it exists
                var dropConstraintResult = context.Database.ExecuteSql($"ALTER TABLE \"app\".apps DROP CONSTRAINT IF EXISTS apps_key_key;");
                // Drop the unique constraint on app key if it exists
                var dropConstraintNameResult = context.Database.ExecuteSql($"ALTER TABLE \"app\".apps DROP CONSTRAINT IF EXISTS apps_name_key;");

                // Add the app to the context and save changes
                context.Apps.Add(app);
                await context.SaveChangesAsync();

                // Recreate the unique index on app key
                var createIndexResult = context.Database.ExecuteSql($"CREATE UNIQUE INDEX apps_key_key ON \"app\".apps (key);");
                // Recreate the unique index on app name
                var createIndexNameResult = context.Database.ExecuteSql($"CREATE UNIQUE INDEX apps_name_key ON \"app\".apps (name);");

                return Ok(new { Message = "AIMY QA app created successfully", AppId = app.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while creating the AIMY QA app", Message = ex.Message });
            }
        }


        [HttpPost("AssginSuperAdminRoleToUsers")]
        public async Task<IActionResult> AssginSuperAdminRoleToUsers(Guid appId)
        {
            try
            {
                // Get the Super Admin role
                var superAdminRole = await context.Roles.FirstOrDefaultAsync(r => r.Key == "super_admin" && r.AppId == appId);
                if (superAdminRole == null)
                {
                    return NotFound(new { Message = "Super Admin role not found" });
                }
                // Get all users in the system
                var users = await context.Users.ToListAsync();
                // Create UserRole entries for each user
                var userRoles = users.Select(user => new UserRole
                {
                    Id = UUIDv7Generator.NewUuid(),
                    UserId = user.Id,
                    AppRoleId = superAdminRole.Id,
                    AppId = appId, // Assuming a system app ID for the role
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12"), // Assuming a system user ID for creation
                    UpdatedBy = new Guid("01985511-5e90-7358-8978-ec052554aa12") // Assuming a system user ID for update
                }).ToList();
                context.UserRoles.AddRange(userRoles);
                await context.SaveChangesAsync();
                return Ok(new { Message = "Super Admin role assigned to all users successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while assigning the Super Admin role to users", Message = ex.Message });
            }
        }
    }
}
