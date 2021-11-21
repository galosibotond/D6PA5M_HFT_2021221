using System;
using D6PA5M_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;

namespace D6PA5M_HFT_2021221.Data
{
    public class AlbumStoreDbContext : DbContext
    {
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<RecordCompany> RecordCompanies { get; set; }


        public AlbumStoreDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AlbumStoreDatabase.mdf;Integrated Security=True";
                builder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity
                .HasOne(artist => artist.Genre)
                .WithMany(genre => genre.Artists)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity
                .HasOne(album => album.Artist)
                .WithMany(artist => artist.Albums)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity
                .HasMany(genre => genre.Artists)
                .WithOne(artist => artist.Genre)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<RecordCompany>(entity =>
            {
                entity
                .HasMany(recordCompany => recordCompany.Albums)
                .WithOne(album => album.RecordCompany)
                .HasForeignKey(x => x.RecordCompanyId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });

            Genre rockGenre = new Genre() { Id = 1001, Name = "Rock/metal" };
            Genre popGenre = new Genre() { Id = 1002, Name = "Pop" };
            Genre jazzGenre = new Genre() { Id = 1003, Name = "Jazz" };
            Genre classicalMusicGenre = new Genre { Id = 1004, Name = "Classical" };

            RecordCompany frontierRecords = new RecordCompany() { Id = 2001, Name = "Frontier Records" };
            RecordCompany islandRecords = new RecordCompany() { Id = 2002, Name = "Island Records" };
            RecordCompany columbiaRecords = new RecordCompany() { Id = 2003, Name = "Columbia Records" };
            RecordCompany deccaRecords = new RecordCompany() { Id = 2004, Name = "Decca Records" };

            Artist artist1 = new Artist() { Id = 3001, Name = "Royal Hunt", Country = "Denmark", FoundationDate = new DateTime(1989, 01, 01) };
            Artist artist2 = new Artist() { Id = 3002, Name = "Hammerfall", Country = "Norway", FoundationDate = new DateTime(1993, 05, 19) };
            Artist artist3 = new Artist() { Id = 3003, Name = "Shawn Mendes", Country = "USA" };
            Artist artist4 = new Artist() { Id = 3004, Name = "Johnny Cash", Country = "USA" };
            Artist artist5 = new Artist() { Id = 3005, Name = "J.S. Bach", Country = "Germany" };
            Artist artist6 = new Artist() { Id = 3006, Name = "Kamelot", Country = "USA", FoundationDate = new DateTime(1991, 10, 13) };
            Artist artist7 = new Artist() { Id = 3007, Name = "Silent Force", Country = "Germany", FoundationDate = new DateTime(1999, 03, 20) };

            Album album1 = new Album() { Id = 4001, Title = "Moving Target", ReleaseDate = new DateTime(1995, 04, 18), Stock = 1348, Price = 2990 };
            Album album2 = new Album() { Id = 4002, Title = "Eyewitness", ReleaseDate = new DateTime(2003, 05, 20), Stock = 150, Price = 3490 };
            Album album3 = new Album() { Id = 4003, Title = "Paper Blood", ReleaseDate = new DateTime(2006, 09, 05), Stock = 45, Price = 4990};
            Album album4 = new Album() { Id = 4004, Title = "Dystopia", ReleaseDate = new DateTime(2020, 11, 21), Stock = 3, Price = 2490 };
            Album album5 = new Album() { Id = 4005, Title = "Walk The Earth", ReleaseDate = new DateTime(2006, 03, 05), Stock = 95, Price = 1990 };
            Album album6 = new Album() { Id = 4006, Title = "Infatuator", ReleaseDate = new DateTime(2003, 01, 14), Stock = 0, Price = 1490};
            Album album7 = new Album() { Id = 4007, Title = "Infected", ReleaseDate = new DateTime(2013, 08, 07), Stock = 354, Price = 5490 };
            Album album8 = new Album() { Id = 4008, Title = "Dominion", ReleaseDate = new DateTime(2017, 05, 23), Stock = 178, Price = 2790 };
            Album album9 = new Album() { Id = 4009, Title = "Silverthorn", ReleaseDate = new DateTime(2011, 08, 13), Stock = 16, Price = 3990 };
            Album album10 = new Album() { Id = 4010, Title = "Haven", ReleaseDate = new DateTime(2014, 09, 03), Stock = 0, Price = 2990 };
            Album album11 = new Album() { Id = 4011, Title = "Sonatas", Stock = 120, Price = 2490 };
            Album album12 = new Album() { Id = 4012, Title = "Concertos", Stock = 19, Price = 2490 };
            Album album13 = new Album() { Id = 4013, Title = "Unearthed", Stock = 0, Price = 1990 };
            Album album14 = new Album() { Id = 4014, Title = "The Mystery of Life", Stock = 0, Price = 1490 };
            Album album15 = new Album() { Id = 4015, Title = "American III: Solitary Man", Stock = 32, Price = 1990 };
            Album album16 = new Album() { Id = 4016, Title = "American V: A Hundred Highways", Stock = 45, Price = 1990 };
            Album album17 = new Album() { Id = 4017, Title = "Handwritten", ReleaseDate = new DateTime(2015, 03, 05), Stock = 803, Price = 3490 };
            Album album18 = new Album() { Id = 4018, Title = "Illuminate", ReleaseDate = new DateTime(2016, 06, 13), Stock = 769, Price = 5990 };
            Album album19 = new Album() { Id = 4019, Title = "Shawn Mendes", ReleaseDate = new DateTime(2018, 05, 22), Stock = 413, Price = 6490 };
            Album album20 = new Album() { Id = 4020, Title = "Wonder", ReleaseDate = new DateTime(2020, 09, 20), Stock = 665, Price = 3990 };

            artist1.GenreId = rockGenre.Id;
            artist2.GenreId = rockGenre.Id;
            artist3.GenreId = popGenre.Id;
            artist4.GenreId = jazzGenre.Id;
            artist5.GenreId = classicalMusicGenre.Id;
            artist6.GenreId = rockGenre.Id;
            artist7.GenreId = rockGenre.Id;

            album1.RecordCompanyId = frontierRecords.Id;
            album2.RecordCompanyId = frontierRecords.Id;
            album3.RecordCompanyId = frontierRecords.Id;
            album4.RecordCompanyId = frontierRecords.Id;
            album5.RecordCompanyId = frontierRecords.Id;
            album6.RecordCompanyId = frontierRecords.Id;
            album7.RecordCompanyId = frontierRecords.Id;
            album8.RecordCompanyId = frontierRecords.Id;
            album9.RecordCompanyId = frontierRecords.Id;
            album10.RecordCompanyId = frontierRecords.Id;
            album11.RecordCompanyId = deccaRecords.Id;
            album12.RecordCompanyId = deccaRecords.Id;
            album13.RecordCompanyId = columbiaRecords.Id;
            album14.RecordCompanyId = columbiaRecords.Id;
            album15.RecordCompanyId = columbiaRecords.Id;
            album16.RecordCompanyId = columbiaRecords.Id;
            album17.RecordCompanyId = islandRecords.Id;
            album18.RecordCompanyId = islandRecords.Id;
            album19.RecordCompanyId = islandRecords.Id;
            album20.RecordCompanyId = islandRecords.Id;

            album1.ArtistId = artist1.Id;
            album2.ArtistId = artist1.Id;
            album3.ArtistId = artist1.Id;
            album4.ArtistId = artist1.Id;
            album5.ArtistId = artist7.Id;
            album6.ArtistId = artist7.Id;
            album7.ArtistId = artist2.Id;
            album8.ArtistId = artist2.Id;
            album9.ArtistId = artist6.Id;
            album10.ArtistId = artist6.Id;
            album11.ArtistId = artist5.Id;
            album12.ArtistId = artist5.Id;
            album13.ArtistId = artist4.Id;
            album14.ArtistId = artist4.Id;
            album15.ArtistId = artist4.Id;
            album16.ArtistId = artist4.Id;
            album17.ArtistId = artist3.Id;
            album18.ArtistId = artist3.Id;
            album19.ArtistId = artist3.Id;
            album20.ArtistId = artist3.Id;

            modelBuilder.Entity<Artist>().HasData(artist1, artist2, artist3, artist4, artist5, artist6, artist7);
            modelBuilder.Entity<Album>().HasData(album1, album2, album3, album4, album5, album6, album7, album8, album9, album10,
                                                 album11, album12, album13, album14, album15, album16, album17, album18, album19, album20);
            modelBuilder.Entity<Genre>().HasData(rockGenre, popGenre, jazzGenre, classicalMusicGenre);
            modelBuilder.Entity<RecordCompany>().HasData(frontierRecords, islandRecords, columbiaRecords, deccaRecords);
        }
    }
}
