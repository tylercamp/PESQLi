using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PESQLi
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    [Table("cms_entries")]
    public class CmsEntry
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Content { get; set; }
    }

    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<CmsEntry> CmsEntries { get; set; }


        public static class Seed
        {
            private static User[] DefaultUsers { get; } = new User[]
            {
                new User { Id = 1, Name = "Alyssa", Password="password" },
                new User { Id = 2, Name = "Bryan", Password="bpass" },
                new User { Id = 3, Name = "Christine", Password = "anactualpassword12345" }
            };

            private static CmsEntry[] DefaultCmsEntries { get; } = new CmsEntry[]
            {
                new CmsEntry { Id = 1, Tag = "foo", Content = "CMS Entry for `foo`" },
                new CmsEntry { Id = 2, Tag = "bar", Content = "CMS Entry for `bar`" }
            };

            public static void Apply(ModelContext context)
            {
                var existingUserIds = context.Users.Select(u => u.Id).ToList();
                var existingCmsEntryIds = context.CmsEntries.Select(e => e.Id).ToList();

                var newUsers = DefaultUsers.Where(u => !existingUserIds.Contains(u.Id));
                var newCmsEntries = DefaultCmsEntries.Where(e => !existingCmsEntryIds.Contains(e.Id));

                context.AddRange(newUsers);
                context.AddRange(newCmsEntries);

                context.SaveChanges();
            }
        }
    }
}
