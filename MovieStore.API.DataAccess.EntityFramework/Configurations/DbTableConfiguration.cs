using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.DataAccess.EntityFramework.Configurations
{
    public class DbTableConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable($"{nameof(T)}s");
            builder.HasKey(e => e.Id);
        }
    }
}