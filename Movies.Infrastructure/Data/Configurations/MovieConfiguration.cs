using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entity;

namespace Movies.Infrastructure.Data.Configurations;

internal class MovieConfiguration: IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(movie => movie.Id);
        builder.Property(movie => movie.Title).HasMaxLength(255);
        builder.Property(movie => movie.Description).HasMaxLength(4_000);
    }
}