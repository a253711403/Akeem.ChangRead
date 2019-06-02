using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    public class ChangReadContext : DbContext
    {
        public ChangReadContext(DbContextOptions<ChangReadContext> options) : base(options)
        {
        }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<LoginRecord> LoginRecord { get; set; }
        public DbSet<Chapter> Chapter { get; set; }
        public DbSet<ChapterContent> ChapterContent { get; set; }
        public DbSet<KeywordReplace> KeywordReplace { get; set; }
        public DbSet<NovelDemand> NovelDemand { get; set; }
        public DbSet<NovelInfo> NovelInfo { get; set; }
        public DbSet<NovelInSort> NovelInSort { get; set; }
        public DbSet<NovelSort> NovelSort { get; set; }
        public DbSet<NovelSource> NovelSource { get; set; }
        public DbSet<SystemConfig> SystemConfig { get; set; }
    }
}
