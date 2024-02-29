using Bot.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.UserId);
        builder.HasIndex(user => user.ChatId);
        builder.Property(user=>user.FirstName).HasMaxLength(255);
        builder.Property(user => user.LastName).HasMaxLength(255);
        builder.Property(user=>user.LanguageCode).HasMaxLength(3).IsRequired();
    }
}

