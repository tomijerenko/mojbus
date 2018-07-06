using Microsoft.EntityFrameworkCore;
using MojBus.Data.Entities;

namespace MojBus.Data
{
    public partial class MojBusContext : DbContext
    {
        public virtual DbSet<Gtfsagencies> Gtfsagencies { get; set; }
        public virtual DbSet<Gtfscalendar> Gtfscalendar { get; set; }
        public virtual DbSet<GtfscalendarDates> GtfscalendarDates { get; set; }
        public virtual DbSet<Gtfsroutes> Gtfsroutes { get; set; }
        public virtual DbSet<Gtfsstops> Gtfsstops { get; set; }
        public virtual DbSet<GtfsstopTimes> GtfsstopTimes { get; set; }
        public virtual DbSet<Gtfstrips> Gtfstrips { get; set; }
        public virtual DbSet<StopDataEntity> StopData { get; set; }
        public virtual DbSet<RouteStopsEntity> RouteData { get; set; }
        public virtual DbSet<FavouriteStopRoutes> FavouriteStopRoutes { get; set; }

        public MojBusContext(DbContextOptions<MojBusContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gtfsagencies>(entity =>
            {
                entity.HasKey(e => e.AgencyId);

                entity.ToTable("GTFSAgencies");

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");

                entity.Property(e => e.AgencyEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyFareUrl)
                    .HasColumnName("AgencyFareURL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyLang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyTimeZone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyUrl)
                    .HasColumnName("AgencyURL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalAgencyId)
                    .HasColumnName("ExternalAgencyID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gtfscalendar>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("GTFSCalendar");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ExternalServiceId).HasColumnName("ExternalServiceID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GtfscalendarDates>(entity =>
            {
                entity.HasKey(e => e.CalendarDateId);

                entity.ToTable("GTFSCalendarDates");

                entity.Property(e => e.CalendarDateId).HasColumnName("CalendarDateID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            });

            modelBuilder.Entity<Gtfsroutes>(entity =>
            {
                entity.HasKey(e => e.RouteId);

                entity.ToTable("GTFSRoutes");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");

                entity.Property(e => e.ExternalRouteId)
                    .HasColumnName("ExternalRouteID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RouteColor)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RouteDesc).IsUnicode(false);

                entity.Property(e => e.RouteLongName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RouteShortName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RouteTextColor)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RouteUrl)
                    .HasColumnName("RouteURL")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gtfsstops>(entity =>
            {
                entity.HasKey(e => e.StopId);

                entity.ToTable("GTFSStops");

                entity.Property(e => e.StopId).HasColumnName("StopID");

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");

                entity.Property(e => e.ExternalStopId)
                    .HasColumnName("ExternalStopID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ParentStationId).HasColumnName("ParentStationID");

                entity.Property(e => e.StopCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StopDesc).IsUnicode(false);

                entity.Property(e => e.StopName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StopUrl)
                    .HasColumnName("StopURL")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GtfsstopTimes>(entity =>
            {
                entity.HasKey(e => e.StopTimeId);

                entity.ToTable("GTFSStopTimes");

                entity.Property(e => e.StopTimeId).HasColumnName("StopTimeID");

                entity.Property(e => e.StopId).HasColumnName("StopID");

                entity.Property(e => e.TripId).HasColumnName("TripID");
            });

            modelBuilder.Entity<Gtfstrips>(entity =>
            {
                entity.HasKey(e => e.TripId);

                entity.ToTable("GTFSTrips");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");

                entity.Property(e => e.DirectionId).HasColumnName("DirectionID");

                entity.Property(e => e.ExternalTripId)
                    .HasColumnName("ExternalTripID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.TripHeadsign)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TripShortName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FavouriteStopRoutes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RouteShortName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StopName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
