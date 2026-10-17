using System;
using System.Collections.Generic;
using Ams_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ams_Api.Data;

public partial class AmsDbContext : DbContext
{
    public AmsDbContext(DbContextOptions<AmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccntAttachment> AccntAttachments { get; set; }

    public virtual DbSet<AccntBalancetype> AccntBalancetypes { get; set; }

    public virtual DbSet<AccntChartofaccount> AccntChartofaccounts { get; set; }

    public virtual DbSet<AccntPaymode> AccntPaymodes { get; set; }

    public virtual DbSet<AccntRecon> AccntRecons { get; set; }

    public virtual DbSet<AccntReconconso> AccntReconconsos { get; set; }

    public virtual DbSet<AccntSrcdoc> AccntSrcdocs { get; set; }

    public virtual DbSet<AccntSuppdoc> AccntSuppdocs { get; set; }

    public virtual DbSet<AccntTag> AccntTags { get; set; }

    public virtual DbSet<AccntTaxregister> AccntTaxregisters { get; set; }

    public virtual DbSet<AccntTranspomode> AccntTranspomodes { get; set; }

    public virtual DbSet<AccntType> AccntTypes { get; set; }

    public virtual DbSet<BirAtc> BirAtcs { get; set; }

    public virtual DbSet<BirAtcCorp> BirAtcCorps { get; set; }

    public virtual DbSet<BodAllowance> BodAllowances { get; set; }

    public virtual DbSet<BodOfficer> BodOfficers { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierType> SupplierTypes { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantsVatoutput> TenantsVatoutputs { get; set; }

    public virtual DbSet<UserAccess> UserAccesses { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserModule> UserModules { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccntAttachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_attachment");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<AccntBalancetype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_balancetype");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<AccntChartofaccount>(entity =>
        {
            entity.HasKey(e => e.CoaId).HasName("PRIMARY");

            entity.ToTable("accnt_chartofaccount");

            entity.Property(e => e.AddedDate).HasColumnType("date");
            entity.Property(e => e.CoaDesc).HasMaxLength(150);
            entity.Property(e => e.EditDate).HasColumnType("date");
            entity.Property(e => e.Tag).HasMaxLength(5);
        });

        modelBuilder.Entity<AccntPaymode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_paymode");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<AccntRecon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_recon");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Atcid).HasColumnName("ATCId");
            entity.Property(e => e.AttachId).HasColumnName("Attach_Id");
            entity.Property(e => e.BodOffcrPcf)
                .HasComment("1 = BoD 2 = Officer 3= PCF ")
                .HasColumnName("BodOffcrPCF");
            entity.Property(e => e.BodoffcrId).HasColumnName("Bodoffcr_Id");
            entity.Property(e => e.BranchShareAmt).HasPrecision(12);
            entity.Property(e => e.CoaId).HasColumnName("CoaID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.ExemptAmt).HasPrecision(12);
            entity.Property(e => e.GoodService).HasMaxLength(5);
            entity.Property(e => e.GrossAmt).HasPrecision(12);
            entity.Property(e => e.InvoiceDate).HasColumnType("date");
            entity.Property(e => e.PassOnAmt).HasPrecision(12);
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.SrcNum).HasMaxLength(50);
            entity.Property(e => e.SupprtNum).HasMaxLength(50);
            entity.Property(e => e.TaxHeld).HasPrecision(12);
            entity.Property(e => e.TaxRate).HasPrecision(12);
            entity.Property(e => e.TaxableAmt).HasPrecision(12);
            entity.Property(e => e.Tin)
                .HasMaxLength(50)
                .HasColumnName("TIN");
            entity.Property(e => e.Total).HasPrecision(12);
            entity.Property(e => e.TranspoMode).HasMaxLength(20);
            entity.Property(e => e.UpdateDate).HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserUpdtIdby).HasColumnName("UserUpdtIDBy");
            entity.Property(e => e.VatAmt).HasPrecision(12);
            entity.Property(e => e.VatClaim).HasPrecision(12);
            entity.Property(e => e.VatOthersAmt).HasPrecision(12);
        });

        modelBuilder.Entity<AccntReconconso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_reconconso");

            entity.Property(e => e.Atc)
                .HasMaxLength(45)
                .HasColumnName("ATC");
            entity.Property(e => e.AttachmentId).HasColumnName("Attachment_Id");
            entity.Property(e => e.BranchCode).HasMaxLength(10);
            entity.Property(e => e.BranchDesc).HasMaxLength(50);
            entity.Property(e => e.CapsGoods).HasPrecision(10);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Exempt).HasPrecision(10);
            entity.Property(e => e.OtherCapsGoods).HasPrecision(10);
            entity.Property(e => e.Services).HasPrecision(10);
            entity.Property(e => e.Tin).HasColumnName("TIN");
            entity.Property(e => e.Vat).HasPrecision(10);
            entity.Property(e => e.WithTax).HasPrecision(10);
            entity.Property(e => e.WithTaxBase).HasPrecision(10);
            entity.Property(e => e.ZeroRated).HasPrecision(10);
        });

        modelBuilder.Entity<AccntSrcdoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_srcdoc");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<AccntSuppdoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_suppdoc");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<AccntTag>(entity =>
        {
            entity.HasKey(e => e.AccntTagId).HasName("PRIMARY");

            entity.ToTable("accnt_tag");

            entity.Property(e => e.AccntTagId).HasMaxLength(3);
            entity.Property(e => e.AccntTagdesc).HasMaxLength(50);
        });

        modelBuilder.Entity<AccntTaxregister>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_taxregister");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<AccntTranspomode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accnt_transpomode");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<AccntType>(entity =>
        {
            entity.HasKey(e => e.AccntTypeId).HasName("PRIMARY");

            entity.ToTable("accnt_type");

            entity.Property(e => e.AccnttypeDesc).HasMaxLength(50);
        });

        modelBuilder.Entity<BirAtc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bir_atc");

            entity.Property(e => e.Code).HasMaxLength(45);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Rate).HasPrecision(10);
        });

        modelBuilder.Entity<BirAtcCorp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bir_atc_corp");

            entity.Property(e => e.Code).HasMaxLength(45);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Rate).HasPrecision(5);
        });

        modelBuilder.Entity<BodAllowance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bod_allowance");

            entity.Property(e => e.Amount).HasPrecision(10);
            entity.Property(e => e.Date).HasColumnType("date");
        });

        modelBuilder.Entity<BodOfficer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bod_officer");

            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.TypeId).HasMaxLength(45);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Branchid).HasName("PRIMARY");

            entity.ToTable("branch");

            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Contact1).HasMaxLength(45);
            entity.Property(e => e.Contact2).HasMaxLength(45);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DateUpdate).HasColumnType("date");
            entity.Property(e => e.Email1).HasMaxLength(45);
            entity.Property(e => e.Email2).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Ocn)
                .HasMaxLength(45)
                .HasColumnName("OCN");
            entity.Property(e => e.Rdo)
                .HasMaxLength(45)
                .HasColumnName("RDO");
            entity.Property(e => e.Tin).HasMaxLength(45);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("company");

            entity.Property(e => e.Accronym).HasMaxLength(45);
            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Atcextension)
                .HasMaxLength(10)
                .HasColumnName("ATCExtension");
            entity.Property(e => e.BranchCode).HasMaxLength(10);
            entity.Property(e => e.Corporation).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.EditDate).HasColumnType("date");
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.MiddleInitial).HasMaxLength(20);
            entity.Property(e => e.Suffix).HasMaxLength(45);
            entity.Property(e => e.Tin).HasMaxLength(45);
            entity.Property(e => e.Tradename).HasMaxLength(100);
        });

        modelBuilder.Entity<SupplierType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supplier_type");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tenants");

            entity.Property(e => e.AddressDetails).HasMaxLength(150);
            entity.Property(e => e.City).HasMaxLength(150);
            entity.Property(e => e.Corporation).HasMaxLength(250);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Firstname).HasMaxLength(45);
            entity.Property(e => e.Lastname).HasMaxLength(45);
            entity.Property(e => e.Middlename).HasMaxLength(45);
            entity.Property(e => e.Tin)
                .HasMaxLength(45)
                .HasColumnName("TIN");
            entity.Property(e => e.TinCode).HasMaxLength(45);
        });

        modelBuilder.Entity<TenantsVatoutput>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tenants_vatoutput");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.EditDate).HasColumnType("date");
            entity.Property(e => e.ExemptAmount).HasPrecision(10);
            entity.Property(e => e.GrossAmount).HasPrecision(10);
            entity.Property(e => e.TaxableAmount).HasPrecision(10);
            entity.Property(e => e.VatInput).HasPrecision(10);
        });

        modelBuilder.Entity<UserAccess>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ModuleId }).HasName("PRIMARY");

            entity.ToTable("user_access");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_account");

            entity.Property(e => e.CompanyId).HasMaxLength(45);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.EditDate).HasColumnType("date");
            entity.Property(e => e.Firstname).HasMaxLength(45);
            entity.Property(e => e.Lastname).HasMaxLength(45);
            entity.Property(e => e.Middlename).HasMaxLength(45);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasDefaultValueSql("'hash password'");
            entity.Property(e => e.Suffix).HasMaxLength(45);
            entity.Property(e => e.Username).HasMaxLength(45);
        });

        modelBuilder.Entity<UserModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_module");

            entity.Property(e => e.Module).HasMaxLength(100);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_role");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
