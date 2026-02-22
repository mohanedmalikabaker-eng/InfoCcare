using InfoCcare.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System.Text;

namespace InfoCcare.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<PrePaid_Data> PrePaid_Data { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<RetailShop> RetailShops { get; set; }
        public DbSet<PrepaidOffer> PrepaidOffers { get; set; }
        public DbSet<TaktikPrepaid> TaktikPrepaids { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<PostpaidOffer> PostpaidOffers { get; set; }
        public DbSet<Faqs> Faqs { get; set; }
        public DbSet<Tariff> Tariff { get; set; }
        public DbSet<Roaming> Roaming { get; set; }
        public DbSet<RoamingOp> RoamingOp { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<B2bPrepaidOffer> B2bPrepaidOffer { get; set; }
        public DbSet<TaktikB2BPrepaid> TaktikB2BPrepaid { get; set; }
        public DbSet<B2bPostpaidOffer> B2bPostpaidOffer { get; set; }
        public DbSet<TaktikB2BPostpaid> TaktikB2BPostpaid { get; set; }
        public DbSet<Mazaya> Mazaya { get; set; }
        public DbSet<MazayaCost> MazayaCost { get; set; }
        public DbSet<MazayaExtraUnits> MazayaExtraUnits { get; set; }
        public DbSet<Bade> Bade { get; set; }
        public DbSet<BadeFees> BadeFees { get; set; }
        public DbSet<BaseTranLimits> BaseTranLimits { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceDescPrice> DeviceDescPrice { get; set; }
        public DbSet<Tarifff> Tarifff { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Segment>()
                .HasOne(c => c.CreatedBy)
                .WithMany()
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Description>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Description>()
             .HasOne(c => c.Segment)
             .WithMany()
             .HasForeignKey(c => c.SegmentId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PrePaid_Data>()
                .HasOne(c => c.CreatedBy)
                .WithMany()
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Dealer>()
               .HasOne(c => c.CreatedBy)
               .WithMany()
               .HasForeignKey(c => c.CreatedById)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RetailShop>()
               .HasOne(c => c.CreatedBy)
               .WithMany()
               .HasForeignKey(c => c.CreatedById)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PrepaidOffer>()
               .HasOne(c => c.CreatedBy)
               .WithMany()
               .HasForeignKey(c => c.CreatedById)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TaktikPrepaid>()
              .HasOne(c => c.CreatedBy)
              .WithMany()
              .HasForeignKey(c => c.CreatedById)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Link>()
             .HasOne(c => c.CreatedBy)
             .WithMany()
             .HasForeignKey(c => c.CreatedById)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PostpaidOffer>()
           .HasOne(c => c.CreatedBy)
           .WithMany()
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Faqs>()
           .HasOne(c => c.CreatedBy)
           .WithMany()
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tariff>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Tariff>()
             .HasOne(c => c.Segment)
             .WithMany()
             .HasForeignKey(c => c.SegmentId)
             .OnDelete(DeleteBehavior.Restrict);

         builder.Entity<Roaming>()
        .HasOne(c => c.CreatedBy)
        .WithMany()
        .HasForeignKey(c => c.CreatedById)
        .OnDelete(DeleteBehavior.Restrict);

         builder.Entity<RoamingOp>()
        .HasOne(c => c.CreatedBy)
        .WithMany()
        .HasForeignKey(c => c.CreatedById)
        .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Zone>()
           .HasOne(c => c.CreatedBy)
           .WithMany()
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TaktikPostpaid>()
          .HasOne(c => c.CreatedBy)
          .WithMany()
          .HasForeignKey(c => c.CreatedById)
          .OnDelete(DeleteBehavior.Restrict);

          builder.Entity<B2bPrepaidOffer>()
         .HasOne(c => c.CreatedBy)
         .WithMany()
         .HasForeignKey(c => c.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

          builder.Entity<TaktikB2BPrepaid>()
         .HasOne(c => c.CreatedBy)
         .WithMany()
         .HasForeignKey(c => c.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<B2bPostpaidOffer>()
           .HasOne(c => c.CreatedBy)
           .WithMany()
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

         builder.Entity<TaktikB2BPostpaid>()
        .HasOne(c => c.CreatedBy)
        .WithMany()
        .HasForeignKey(c => c.CreatedById)
        .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Mazaya>()
           .HasOne(c => c.CreatedBy)
           .WithMany()
           .HasForeignKey(c => c.CreatedById)
           .OnDelete(DeleteBehavior.Restrict);

          builder.Entity<MazayaCost>()
         .HasOne(c => c.CreatedBy)
         .WithMany()
         .HasForeignKey(c => c.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

          builder.Entity<MazayaExtraUnits>()
         .HasOne(c => c.CreatedBy)
         .WithMany()
         .HasForeignKey(c => c.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bade>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BadeFees>()
          .HasOne(c => c.CreatedBy)
          .WithMany()
          .HasForeignKey(c => c.CreatedById)
          .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BaseTranLimits>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Device>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DeviceDescPrice>()
         .HasOne(c => c.CreatedBy)
         .WithMany()
         .HasForeignKey(c => c.CreatedById)
         .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tarifff>()
              .HasOne(c => c.CreatedBy)
              .WithMany()
              .HasForeignKey(c => c.CreatedById)
              .OnDelete(DeleteBehavior.Restrict);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted)
                .ToList();

            foreach (var modifiedEntitiey in modifiedEntities)
            {
                var auditLog = new AuditLog
                {
                    EntityName = modifiedEntitiey.Entity.GetType().Name,
                    UserEmail = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name),
                    Action = modifiedEntitiey.State.ToString(),
                    TimesTamp = DateTime.UtcNow,
                    Changes = GetChanges(modifiedEntitiey)
                };

                AuditLogs.Add(auditLog);
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private string GetChanges(EntityEntry modifiedEntitiey)
        {
            var changes = new StringBuilder();
            foreach (var property in modifiedEntitiey.OriginalValues.Properties)
            {
                var originalValue = modifiedEntitiey.OriginalValues[property];
                var currentValue = modifiedEntitiey.CurrentValues[property];

                if (!Equals(originalValue, currentValue))
                {
                    changes.AppendLine($"{property.Name}: From '{originalValue}' to '{currentValue}'");
                }
            }
            return changes.ToString();
        }
        public DbSet<InfoCcare.Models.TaktikPostpaid> TaktikPostpaid { get; set; } = default!;
    }
}
