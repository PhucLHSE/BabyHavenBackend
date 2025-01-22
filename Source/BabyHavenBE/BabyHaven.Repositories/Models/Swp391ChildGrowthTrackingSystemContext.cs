using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BabyHaven.Repositories.Models;

public partial class Swp391ChildGrowthTrackingSystemContext : DbContext
{
    public Swp391ChildGrowthTrackingSystemContext()
    {
    }

    public Swp391ChildGrowthTrackingSystemContext(DbContextOptions<Swp391ChildGrowthTrackingSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<ChildMilestone> ChildMilestones { get; set; }

    public virtual DbSet<ConsultationRequest> ConsultationRequests { get; set; }

    public virtual DbSet<ConsultationResponse> ConsultationResponses { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<GrowthRecord> GrowthRecords { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberMembership> MemberMemberships { get; set; }

    public virtual DbSet<MembershipPackage> MembershipPackages { get; set; }

    public virtual DbSet<Milestone> Milestones { get; set; }

    public virtual DbSet<PackageFeature> PackageFeatures { get; set; }

    public virtual DbSet<PackagePromotion> PackagePromotions { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<RatingFeedback> RatingFeedbacks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=metmoix100;Initial Catalog=SWP391_ChildGrowthTrackingSystem;Persist Security Info=True;User ID=sa;Password=12345;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.AlertId).HasName("PK__Alerts__EBB16AED697D5361");

            entity.Property(e => e.AlertId).HasColumnName("AlertID");
            entity.Property(e => e.AlertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiseaseId).HasColumnName("DiseaseID");
            entity.Property(e => e.GrowthRecordId).HasColumnName("GrowthRecordID");
            entity.Property(e => e.Message).HasMaxLength(2000);
            entity.Property(e => e.SeverityLevel).HasMaxLength(50);

            entity.HasOne(d => d.Disease).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.DiseaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alerts__DiseaseI__3D2915A8");

            entity.HasOne(d => d.GrowthRecord).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.GrowthRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alerts__GrowthRe__3C34F16F");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blogs__54379E50E02419AB");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageBlog).HasMaxLength(2000);
            entity.Property(e => e.ReferenceSources).HasMaxLength(2000);
            entity.Property(e => e.RejectionReason).HasMaxLength(2000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Tags)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blogs__AuthorID__55F4C372");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blogs__CategoryI__56E8E7AB");
        });

        modelBuilder.Entity<BlogCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__BlogCate__19093A2BC23A1806");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK__BlogCateg__Paren__503BEA1C");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.ChildId).HasName("PK__Children__BEFA073691E46C40");

            entity.Property(e => e.ChildId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ChildID");
            entity.Property(e => e.Allergies).HasMaxLength(2000);
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.RelationshipToMember)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.Children)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Children__Member__5070F446");
        });

        modelBuilder.Entity<ChildMilestone>(entity =>
        {
            entity.HasKey(e => new { e.ChildId, e.MilestoneId }).HasName("PK__ChildMil__2E664F3326AAA7EA");

            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.MilestoneId).HasColumnName("MilestoneID");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Guidelines).HasMaxLength(2000);
            entity.Property(e => e.Importance)
                .HasMaxLength(50)
                .HasDefaultValue("Medium");
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Not Achieved");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Child).WithMany(p => p.ChildMilestones)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChildMile__Child__498EEC8D");

            entity.HasOne(d => d.Milestone).WithMany(p => p.ChildMilestones)
                .HasForeignKey(d => d.MilestoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChildMile__Miles__4A8310C6");
        });

        modelBuilder.Entity<ConsultationRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Consulta__33A8519A5EEC5465");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Attachments)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Urgency).HasMaxLength(50);

            entity.HasOne(d => d.Child).WithMany(p => p.ConsultationRequests)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__Child__208CD6FA");

            entity.HasOne(d => d.Member).WithMany(p => p.ConsultationRequests)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__Membe__1F98B2C1");
        });

        modelBuilder.Entity<ConsultationResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK__Consulta__1AAA640C21F60539");

            entity.Property(e => e.ResponseId).HasColumnName("ResponseID");
            entity.Property(e => e.Attachments)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.ResponseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ConsultationResponses)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__Docto__282DF8C2");

            entity.HasOne(d => d.Request).WithMany(p => p.ConsultationResponses)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__Reque__2739D489");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("PK__Diseases__69B533A9AADC2888");

            entity.Property(e => e.DiseaseId).HasColumnName("DiseaseID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.DiseaseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DiseaseType).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.Prevention).HasMaxLength(2000);
            entity.Property(e => e.Severity).HasMaxLength(50);
            entity.Property(e => e.Symptoms).HasMaxLength(2000);
            entity.Property(e => e.Treatment).HasMaxLength(2000);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EDFB4107A61");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Doctors__85FB4E383846BE9D").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Doctors__A9D105340700DFA0").IsUnique();

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Biography).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Degree)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HospitalAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HospitalName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctors__UserID__0D7A0286");
        });

        modelBuilder.Entity<DoctorSpecialization>(entity =>
        {
            entity.HasKey(e => e.DoctorSpecializationId).HasName("PK__DoctorSp__14F6ED6DB4262255");

            entity.Property(e => e.DoctorSpecializationId).HasColumnName("DoctorSpecializationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorSpe__Docto__17F790F9");

            entity.HasOne(d => d.Specialization).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorSpe__Speci__18EBB532");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("PK__Features__82230A295C9929B5");

            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.FeatureName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<GrowthRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__GrowthRe__FBDF78C9F1AE59BA");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.AttentionSpan).HasMaxLength(50);
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DevelopmentalMilestones).HasMaxLength(255);
            entity.Property(e => e.Hearing).HasMaxLength(50);
            entity.Property(e => e.ImmunizationStatus).HasMaxLength(2000);
            entity.Property(e => e.MentalHealthStatus).HasMaxLength(50);
            entity.Property(e => e.NeurologicalReflexes).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.NutritionalStatus).HasMaxLength(50);
            entity.Property(e => e.PhysicalActivityLevel).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Verified).HasDefaultValue(false);
            entity.Property(e => e.Vision).HasMaxLength(50);

            entity.HasOne(d => d.Child).WithMany(p => p.GrowthRecords)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GrowthRec__Child__571DF1D5");

            entity.HasOne(d => d.RecordedByNavigation).WithMany(p => p.GrowthRecords)
                .HasForeignKey(d => d.RecordedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GrowthRec__Recor__5812160E");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B388A20041C");

            entity.HasIndex(e => e.UserId, "UQ__Members__1788CCAD8F1121FC").IsUnique();

            entity.Property(e => e.MemberId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("MemberID");
            entity.Property(e => e.EmergencyContact)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.LeaveDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.Member)
                .HasForeignKey<Member>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Members__UserID__49C3F6B7");
        });

        modelBuilder.Entity<MemberMembership>(entity =>
        {
            entity.HasKey(e => e.MemberMembershipId).HasName("PK__MemberMe__5EAB967F3E9F2C3B");

            entity.Property(e => e.MemberMembershipId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("MemberMembershipID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberMemberships)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberMem__Membe__7E37BEF6");

            entity.HasOne(d => d.Package).WithMany(p => p.MemberMemberships)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberMem__Packa__7F2BE32F");
        });

        modelBuilder.Entity<MembershipPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Membersh__322035EC0C4C996B");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasDefaultValue("VND");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.PackageName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.SupportLevel).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Milestone>(entity =>
        {
            entity.HasKey(e => e.MilestoneId).HasName("PK__Mileston__09C4805836A966FA");

            entity.Property(e => e.MilestoneId).HasColumnName("MilestoneID");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Importance)
                .HasMaxLength(50)
                .HasDefaultValue("Medium");
            entity.Property(e => e.MilestoneName).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<PackageFeature>(entity =>
        {
            entity.HasKey(e => new { e.PackageId, e.FeatureId }).HasName("PK__PackageF__BA02054E37A9444E");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.FeatureId).HasColumnName("FeatureID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Feature).WithMany(p => p.PackageFeatures)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PackageFe__Featu__76969D2E");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageFeatures)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PackageFe__Packa__75A278F5");
        });

        modelBuilder.Entity<PackagePromotion>(entity =>
        {
            entity.HasKey(e => new { e.PackageId, e.PromotionId }).HasName("PK__PackageP__D70C771ED06DF4C0");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Package).WithMany(p => p.PackagePromotions)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PackagePr__Packa__6B24EA82");

            entity.HasOne(d => d.Promotion).WithMany(p => p.PackagePromotions)
                .HasForeignKey(d => d.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PackagePr__Promo__6C190EBB");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42F2F48759ABA");

            entity.Property(e => e.PromotionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PromotionID");
            entity.Property(e => e.ApplicablePackageIds)
                .HasMaxLength(2000)
                .HasColumnName("ApplicablePackageIDs");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.MaxDiscountAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MinPurchaseAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PromotionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RedemptionCount).HasDefaultValue(0);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.TargetAudience)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PromotionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__Creat__5FB337D6");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PromotionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__Modif__60A75C0F");
        });

        modelBuilder.Entity<RatingFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__RatingFe__6A4BEDD6179D8515");

            entity.Property(e => e.Comment).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FeedbackType)
                .HasMaxLength(50)
                .HasDefaultValue("general");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Response).WithMany(p => p.RatingFeedbacks)
                .HasForeignKey(d => d.ResponseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RatingFee__Respo__31B762FC");

            entity.HasOne(d => d.User).WithMany(p => p.RatingFeedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RatingFee__UserI__30C33EC3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A066A2C93");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__Speciali__5809D84F35DA61C7");

            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.SpecializationName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B99EAD2AA");

            entity.Property(e => e.TransactionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GatewayTransactionId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("GatewayTransactionID");
            entity.Property(e => e.MemberMembershipId).HasColumnName("MemberMembershipID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.MemberMembership).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.MemberMembershipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Membe__05D8E0BE");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__UserI__04E4BC85");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CCAC20C7D758");

            entity.HasIndex(e => e.Username, "UQ__UserAcco__536C85E49ADA28B5").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__UserAcco__85FB4E38A28D7DDE").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__UserAcco__A9D105348AF32F8A").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture).HasMaxLength(2000);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.VerificationCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAccou__RoleI__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
