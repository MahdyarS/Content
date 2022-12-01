using Content.Core.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Infrastructure.Database.Configurations
{
    public class MediaTagConfigureation : IEntityTypeConfiguration<MediaTag>
    {
        public void Configure(EntityTypeBuilder<MediaTag> builder)
        {
            builder.HasOne(p => p.Media).WithMany(p => p.MediaTags).HasForeignKey(p => p.MediaId);
            builder.HasOne(p => p.Tag).WithMany(p => p.MediaTags).HasForeignKey(p => p.TagId);
        }
    }
}
