using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Infrastructure.Configurations
    {
        public class UserSessionConfiguration
          : IEntityTypeConfiguration<UserSession>
        {
            public void Configure(EntityTypeBuilder<UserSession> builder)
            {
                builder.HasKey(x => new { x.UserId, x.SessionId });

                builder.HasOne(x => x.User)
                    .WithMany(u => u.LecturerersSessions)
                    .HasForeignKey(x => x.UserId) ;

                builder.HasOne(x => x.Session)
                    .WithMany(t => t.LecturerersSessions)
                    .HasForeignKey(x => x.SessionId).OnDelete(DeleteBehavior.Cascade); ;
            }
        }
    }

}
