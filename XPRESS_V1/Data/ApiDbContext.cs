using Microsoft.EntityFrameworkCore;
using XPRESS_V1.Models;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<TestTable> TestTables { get; set; }
        public DbSet<Advait> Advait { get; set; }
        public DbSet<Test_Tables_George> TestTables_George { get; set; }
        public DbSet<Riona> Riona { get; set; }
        public DbSet<Mahesh> Maheshs { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<TravelType> TravelTypes { get; set; }
        public DbSet<TravelMode> TravelModes { get; set; }
        public DbSet<TripType> TripTypes { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TravelRequest> TravelRequests { get; set; }
        public DbSet<TicketOption> TicketOptions { get; set; }
        public DbSet<RequestApproval> RequestApprovals { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.ReportingManager)
                .WithMany(u => u.Subordinates)
                .HasForeignKey(u => u.ReportingManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.DuHead)
                .WithMany(u => u.DepartmentMembers)
                .HasForeignKey(u => u.DuHeadId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.Employee)
                .WithMany(u => u.TravelRequests)
                .HasForeignKey(tr => tr.EmployeeId);

            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.TravelType)
                .WithMany(tt => tt.TravelRequests)
                .HasForeignKey(tr => tr.TravelTypeId);

            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.TripType)
                .WithMany(tt => tt.TravelRequests)
                .HasForeignKey(tr => tr.TripTypeId);

            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.Project)
                .WithMany(p => p.TravelRequests)
                .HasForeignKey(tr => tr.ProjectCode); // Reverted to ProjectCode (string)

            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.TravelMode)
                .WithMany(tm => tm.TravelRequests)
                .HasForeignKey(tr => tr.TravelModeId);

            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.CurrentStatus)
                .WithMany(rs => rs.TravelRequests)
                .HasForeignKey(tr => tr.CurrentStatusId);

            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.SelectedTicketOption)
                .WithMany(to => to.SelectedByTravelRequests)
                .HasForeignKey(tr => tr.SelectedTicketOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelRequest>()
                .HasMany(tr => tr.TicketOptions)
                .WithOne(to => to.TravelRequest)
                .HasForeignKey(to => to.RequestId);

            modelBuilder.Entity<TravelRequest>()
                .HasMany(tr => tr.RequestApprovals)
                .WithOne(ra => ra.TravelRequest)
                .HasForeignKey(ra => ra.RequestId);

            modelBuilder.Entity<TravelRequest>()
                .HasMany(tr => tr.AuditLogs)
                .WithOne(al => al.TravelRequest)
                .HasForeignKey(al => al.RequestId);

            //modelBuilder.Entity<TicketOption>()
            //    .WithMany(u => u.CreatedTicketOptions)
            //    .HasForeignKey(to => to.CreatedBy);

            modelBuilder.Entity<RequestApproval>()
                .HasOne(ra => ra.Approver)
                .WithMany(u => u.Approvals)
                .HasForeignKey(ra => ra.ApproverId);

            modelBuilder.Entity<RequestApproval>()
                .HasOne(ra => ra.PreviousStatus)
                .WithMany(rs => rs.PreviousStatuses)
                .HasForeignKey(ra => ra.PreviousStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestApproval>()
                .HasOne(ra => ra.NewStatus)
                .WithMany(rs => rs.RequestApprovalsAsNewStatus)
                .HasForeignKey(ra => ra.NewStatusId);

            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(al => al.UserId);

            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.OldStatus)
                .WithMany(rs => rs.OldStatuses)
                .HasForeignKey(al => al.OldStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.NewStatus)
                .WithMany(rs => rs.AuditLogsAsNewStatus)
                .HasForeignKey(al => al.NewStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add indexes for frequently queried foreign keys
            modelBuilder.Entity<TravelRequest>()
                .HasIndex(tr => tr.EmployeeId);

            modelBuilder.Entity<TravelRequest>()
                .HasIndex(tr => tr.TravelTypeId);

            modelBuilder.Entity<TravelRequest>()
                .HasIndex(tr => tr.TripTypeId);

            modelBuilder.Entity<TravelRequest>()
                .HasIndex(tr => tr.ProjectCode); // Updated index to ProjectCode

            modelBuilder.Entity<TravelRequest>()
                .HasIndex(tr => tr.TravelModeId);

            modelBuilder.Entity<TravelRequest>()
                .HasIndex(tr => tr.CurrentStatusId);
            modelBuilder.Entity<Document>(entity =>
            {
                // Required fields (already enforced by attributes or design)
                entity.Property(d => d.EmployeeID).IsRequired();
                entity.Property(d => d.DocumentTypeID).IsRequired();
                entity.Property(d => d.DocumentNumber).IsRequired();

                // Optional fields — explicitly made nullable
                entity.Property(d => d.IssuingCountry).IsRequired(false);
                entity.Property(d => d.IssuingAuthority).IsRequired(false);
                entity.Property(d => d.DocumentLink).IsRequired(false);
                entity.Property(d => d.IsValid).IsRequired(false);
                entity.Property(d => d.Comments).IsRequired(false);
                entity.Property(d => d.CreatedBy).IsRequired(false);
                entity.Property(d => d.CreatedOn).IsRequired(false);
                entity.Property(d => d.ModifiedBy).IsRequired(false);
                entity.Property(d => d.ModifiedOn).IsRequired(false);
            });
        }
    }
}