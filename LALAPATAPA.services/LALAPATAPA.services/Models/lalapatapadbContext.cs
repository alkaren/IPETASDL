using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LALAPATAPA.services.Models
{
    public partial class lalapatapadbContext : DbContext
    {
        public lalapatapadbContext()
        {
        }

        public lalapatapadbContext(DbContextOptions<lalapatapadbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<DetailTransaction> DetailTransaction { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<HeaderTransaction> HeaderTransaction { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<PaymentAttachment> PaymentAttachment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<TransactionLog> TransactionLog { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount)
                    .HasName("PK__Account__B7B00CE3B09E3DD8");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("AccountToCustomer");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("AccountToEmployee");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("AccountToUserGroup");
            });

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.HasKey(e => e.IdBankAccount)
                    .HasName("PK__BankAcco__B54B2CFCF1DB6690");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountOwner)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValidStart).HasColumnType("datetime");

                entity.Property(e => e.ValidUntil).HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PK__Customer__DB43864A17063489");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Customer__A9D1053440AC16E8")
                    .IsUnique();

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DetailTransaction>(entity =>
            {
                entity.HasKey(e => e.IdDetail)
                    .HasName("PK__DetailTr__BA1D96E4CB99901A");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProcurementPurpose).IsUnicode(false);

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.Property(e => e.TransactionDescription).IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdBankAccountNavigation)
                    .WithMany(p => p.DetailTransaction)
                    .HasForeignKey(d => d.IdBankAccount)
                    .HasConstraintName("DetailTransactionToBankAccount");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.DetailTransaction)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("DetailTransactionToProduct");

                entity.HasOne(d => d.IdTransactionNavigation)
                    .WithMany(p => p.DetailTransaction)
                    .HasForeignKey(d => d.IdTransaction)
                    .HasConstraintName("DetailTransactionToHeaderTransaction");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.IdDevice)
                    .HasName("PK__Device__E744484081D3C356");

                entity.Property(e => e.InstallationId).IsUnicode(false);

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.Device)
                    .HasForeignKey(d => d.IdAccount)
                    .HasConstraintName("DeviceToAccount");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PK__Employee__51C8DD7AE4C7A881");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.IdFeedback)
                    .HasName("PK__Feedback__408FF10361A2AF08");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.IdAccount)
                    .HasConstraintName("FeedbackToAccount");
            });

            modelBuilder.Entity<HeaderTransaction>(entity =>
            {
                entity.HasKey(e => e.IdTransaction)
                    .HasName("PK__HeaderTr__45542F459272C2CC");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerConfirmation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderAttachmentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderAttachmentUrl).IsUnicode(false);

                entity.Property(e => e.PaymentMethode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentTotal).HasColumnType("money");

                entity.Property(e => e.RequestDeliveryStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionNumber).IsUnicode(false);

                entity.Property(e => e.TransactionStatus).IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdAttachmentNavigation)
                    .WithMany(p => p.HeaderTransaction)
                    .HasForeignKey(d => d.IdAttachment)
                    .HasConstraintName("HeaderTransactionToPaymentAttachment");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.HeaderTransaction)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("HeaderTransactionToCustomer");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PK__Log__0C54DBC66BF15EF7");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.Source).IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<PaymentAttachment>(entity =>
            {
                entity.HasKey(e => e.IdAttachment)
                    .HasName("PK__PaymentA__DC8FA6141D41FB32");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Url).IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Product__2E8946D48B1ED41F");

                entity.Property(e => e.AvailableDescription).IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Commodity).IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileUrl).IsUnicode(false);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.IsAvailable)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MapType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Scale)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubDistrict)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TransactionLog>(entity =>
            {
                entity.HasKey(e => e.IdTransactioLog)
                    .HasName("PK__Transact__1E80FFC108C7C95C");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Target)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.IdTransactionNavigation)
                    .WithMany(p => p.TransactionLog)
                    .HasForeignKey(d => d.IdTransaction)
                    .HasConstraintName("TransactionLogToHeaderTransaction");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.IdGroup)
                    .HasName("PK__UserGrou__32DFFDB3E78EEDC2");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });
        }
    }
}
