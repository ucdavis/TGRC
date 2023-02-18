using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TGRC.Models
{
    public partial class TGRCContext : DbContext
    {
        public TGRCContext()
        {
        }

        public TGRCContext(DbContextOptions<TGRCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accession> Accessions { get; set; }
        public virtual DbSet<AccessionCategoriesInAccession> AccessionCategoriesInAccessions { get; set; }
        public virtual DbSet<AccessionCategories> AccessionCategories { get; set; }
        public virtual DbSet<AccessionCluster> AccessionClusters { get; set; }
        public virtual DbSet<AccessionStatus> AccessionStatuses { get; set; }
        public virtual DbSet<AccessionsInImage> AccessionsInImages { get; set; }
        public virtual DbSet<CategoriesInImage> CategoriesInImages { get; set; }
        public virtual DbSet<Colleague> Colleagues { get; set; }
        public virtual DbSet<ColleaguesInImage> ColleaguesInImages { get; set; }
        public virtual DbSet<CulturalRecommendation> CulturalRecommendations { get; set; }
        public virtual DbSet<CultureInAccession> CultureInAccessions { get; set; }
        public virtual DbSet<DonorsInAccession> DonorsInAccessions { get; set; }
        public virtual DbSet<Gene> Genes { get; set; }
        public virtual DbSet<GenesAndAllele> GenesAndAlleles { get; set; }
        public virtual DbSet<GenesAndAllelesInAccession> GenesAndAllelesInAccessions { get; set; }
        public virtual DbSet<GenesAndAllelesInImage> GenesAndAllelesInImages { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<IsogenicityType> IsogenicityTypes { get; set; }
        public virtual DbSet<MatingSystem> MatingSystems { get; set; }
        public virtual DbSet<MutantType> MutantTypes { get; set; }
        public virtual DbSet<PhenoInGene> PhenoInGenes { get; set; }
        public virtual DbSet<PhenotypicCategory> PhenotypicCategories { get; set; }


         public static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accession>(entity =>
            {
                entity.HasKey(e =>e.AccessionNum);

                entity.Property(e => e.AccessionNum).HasMaxLength(10);

                entity.Property(e => e.AssociatedBiota).HasMaxLength(150);

                entity.Property(e => e.ChromosomeNum).HasMaxLength(50);

                entity.Property(e => e.CollectionDate).HasMaxLength(2);

                entity.Property(e => e.CollectionMonth).HasMaxLength(2);

                entity.Property(e => e.CollectionNum).HasMaxLength(10);

                entity.Property(e => e.CollectionSite).HasMaxLength(150);

                entity.Property(e => e.CollectionYear).HasMaxLength(4);

                entity.Property(e => e.Comments).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.CulRecId).HasColumnName("CulRecID");

                entity.Property(e => e.CultivarName).HasMaxLength(30);

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ElevSource).HasMaxLength(50);

                entity.Property(e => e.Elevation).HasMaxLength(11);

                entity.Property(e => e.Habitat).HasMaxLength(100);

                entity.Property(e => e.IlsNumber)
                    .HasMaxLength(30)
                    .HasColumnName("ILs_number");

                entity.Property(e => e.KaryotypicalVariations).HasMaxLength(50);

                entity.Property(e => e.LatDir).HasMaxLength(1);

                entity.Property(e => e.LatLonSource).HasMaxLength(50);

                entity.Property(e => e.LonDir).HasMaxLength(1);

                entity.Property(e => e.MatingSystem).HasMaxLength(20);

                entity.Property(e => e.Mta)
                    .HasMaxLength(50)
                    .HasColumnName("MTA");

                entity.Property(e => e.OtherId)
                    .HasMaxLength(30)
                    .HasColumnName("OtherID");

                entity.Property(e => e.PopulationSize).HasMaxLength(20);

                entity.Property(e => e.ProvinceOrDepartment).HasMaxLength(20);

                entity.Property(e => e.PstvdStatus).HasColumnName("pstvd_status");

                entity.Property(e => e.Reference).HasMaxLength(255);

                entity.Property(e => e.ReferenceOld)
                    .HasMaxLength(255)
                    .HasColumnName("Reference_old");

                entity.Property(e => e.RelevantChromosome).HasMaxLength(50);

                entity.Property(e => e.SampleSize).HasMaxLength(20);

                entity.Property(e => e.SiteDetails).HasMaxLength(150);

                entity.Property(e => e.SourceOfAccession).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(12);

                entity.Property(e => e.StockIncreaseNotes).HasMaxLength(250);

                entity.Property(e => e.Taxon).HasMaxLength(30);

                entity.Property(e => e.Taxon2).HasMaxLength(40);

                entity.Property(e => e.Traits).HasMaxLength(200);

                entity.Property(e => e.VegetationType).HasMaxLength(20);

                entity.HasMany(e => e.Donors);
            });

            modelBuilder.Entity<AccessionCategoriesInAccession>(entity =>
            {
                entity.HasKey(e => new {e.AccessionNum, e.AccessionCategory});

                entity.Property(e => e.AccessionCategory).HasMaxLength(40);

                entity.Property(e => e.AccessionNum).HasMaxLength(50);
            });

            modelBuilder.Entity<AccessionCategories>(entity =>
            {
                entity.HasKey(e => e.AccessionCategory);

                entity.Property(e => e.AccessionCategory)
                    .HasMaxLength(40)
                    .HasColumnName("AccessionCategory");

                entity.Property(e => e.Definition).HasMaxLength(250);
            });

            modelBuilder.Entity<AccessionCluster>(entity =>
            {
                entity.HasKey(e => new {e.AccessionNum, e.ClusterName});

                entity.ToTable("AccessionCluster");

                entity.Property(e => e.AccessionNum).HasMaxLength(10);

                entity.Property(e => e.ClusterName).HasMaxLength(60);
            });

            modelBuilder.Entity<AccessionStatus>(entity =>
            {
                entity.HasKey(e => e.Status);

                entity.ToTable("AccessionStatus");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("AccessionStatus");

                entity.Property(e => e.Definition).HasMaxLength(255);
            });

            modelBuilder.Entity<AccessionsInImage>(entity =>
            {
                entity.HasKey(e => new {e.AccessionNum, e.ImageNum});

                entity.Property(e => e.AccessionNum).HasMaxLength(50);

                entity.Property(e => e.DateModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<CategoriesInImage>(entity =>
            {
                entity.HasKey(e => new {e.ImageNum, e.Category });

                entity.Property(e => e.Category).HasMaxLength(50);
            });

            modelBuilder.Entity<Colleague>(entity =>
            {               

                entity.ToTable("COLLEAGUES");

                entity.Property(e => e.Address1).HasMaxLength(100);

                entity.Property(e => e.Address2).HasMaxLength(75);

                entity.Property(e => e.Associate).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.CoName)
                    .HasMaxLength(100)
                    .HasColumnName("coName");

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CountryGroup)
                    .HasMaxLength(50)
                    .HasColumnName("Country_group");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(48);

                entity.Property(e => e.Fax).HasMaxLength(16);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Institute).HasMaxLength(75);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.MiddleInitial).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PostCode).HasMaxLength(13);

                entity.Property(e => e.SAddress1)
                    .HasMaxLength(100)
                    .HasColumnName("sAddress1");

                entity.Property(e => e.SAddress2)
                    .HasMaxLength(100)
                    .HasColumnName("sAddress2");

                entity.Property(e => e.SCity)
                    .HasMaxLength(50)
                    .HasColumnName("sCity");

                entity.Property(e => e.SCountry)
                    .HasMaxLength(50)
                    .HasColumnName("sCountry");

                entity.Property(e => e.SInstitute)
                    .HasMaxLength(75)
                    .HasColumnName("sInstitute");

                entity.Property(e => e.SPostalCode)
                    .HasMaxLength(13)
                    .HasColumnName("sPostalCode");

                entity.Property(e => e.SState)
                    .HasMaxLength(50)
                    .HasColumnName("sState");

                entity.Property(e => e.Salutation)
                    .HasMaxLength(10)
                    .HasColumnName("salutation");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(12);

                entity.Property(e => e.UsdaOrganizationalCategory)
                    .HasMaxLength(4)
                    .HasColumnName("USDA_OrganizationalCategory");

                entity.Property(e => e.User)
                    .HasMaxLength(30)
                    .HasColumnName("user");

                entity.Property(e => e.WebAddress).HasColumnName("Web address");
            });

            modelBuilder.Entity<ColleaguesInImage>(entity =>
            {
                entity.HasKey(e => new {e.ColleagueNum, e.ImageNum});

                entity.Property(e => e.DateModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<CulturalRecommendation>(entity =>
            {
                entity.HasKey(e => e.RecommendationNum);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Details).HasMaxLength(255);

                entity.Property(e => e.Tgrcnote)
                    .HasMaxLength(255)
                    .HasColumnName("TGRCNote");
            });

            modelBuilder.Entity<CultureInAccession>(entity =>
            {
                entity.HasKey(e => new {e.AccessionNum, e.RecommendationNum});

                entity.ToTable("CultureInAccession");

                entity.Property(e => e.AccessionNum).HasMaxLength(50);
            });

            modelBuilder.Entity<DonorsInAccession>(entity =>
            {
                entity.HasKey(e => new {e.ColleagueNum, e.AccessionNum});

                entity.Property(e => e.AccessionNum).HasMaxLength(50);

                entity.Property(e => e.Collector).HasColumnName("collector");

                entity.Property(e => e.Donor).HasColumnName("donor");
            });

            modelBuilder.Entity<Gene>(entity =>
            {
                entity.HasKey(e => e.Gene1);

                entity.ToTable("GENES");

                entity.Property(e => e.Arm).HasMaxLength(1);

                entity.Property(e => e.Chromosome).HasMaxLength(50);

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Gene1)
                    .HasMaxLength(8)
                    .HasColumnName("Gene");

                entity.Property(e => e.LocusName).HasMaxLength(40);

                entity.Property(e => e.MarkerType).HasMaxLength(50);

                entity.Property(e => e.Notes).HasMaxLength(100);
            });

            modelBuilder.Entity<GenesAndAllele>(entity =>
            {
                entity.HasKey(e => new {e.Gene, e.Allele});

                entity.Property(e => e.Allele).HasMaxLength(8);

                entity.Property(e => e.AlleleName).HasMaxLength(30);

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Gene).HasMaxLength(8);

                entity.Property(e => e.MutantType).HasMaxLength(12);

                entity.Property(e => e.PhenotypeCombo).HasMaxLength(50);

                entity.Property(e => e.SynonymsOfAllele).HasMaxLength(50);
            });

            modelBuilder.Entity<GenesAndAllelesInAccession>(entity =>
            {
                entity.HasKey(e => new {e.AccessionNum, e.Gene, e.Allele});
                entity.Property(e => e.AccessionNum).HasMaxLength(10);

                entity.Property(e => e.Allele).HasMaxLength(8);

                entity.Property(e => e.Background).HasMaxLength(50);

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Gene).HasMaxLength(8);

                entity.Property(e => e.Isogenicity).HasMaxLength(20);
            });

            modelBuilder.Entity<GenesAndAllelesInImage>(entity =>
            {
                entity.HasKey(e => new {e.ImageNum, e.Gene, e.Allele});

                entity.Property(e => e.Allele).HasMaxLength(50);

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Gene).HasMaxLength(50);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ImageNum);

                entity.ToTable("IMAGES");

                entity.Property(e => e.A)
                    .HasMaxLength(200)
                    .HasColumnName("a");

                entity.Property(e => e.B)
                    .HasMaxLength(200)
                    .HasColumnName("b");

                entity.Property(e => e.Caption).HasMaxLength(255);

                entity.Property(e => e.Chk).HasColumnName("chk");

                entity.Property(e => e.Chkthumb).HasColumnName("chkthumb");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.File).HasMaxLength(150);                

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Pedigree).HasMaxLength(255);

                entity.Property(e => e.SlideNum).HasMaxLength(16);
            });

            modelBuilder.Entity<IsogenicityType>(entity =>
            {
                entity.HasKey(e => e.Isogenicity);

                entity.ToTable("IsogenicityType");

                entity.Property(e => e.IsogMeaning)
                    .HasMaxLength(50)
                    .HasColumnName("Isog-meaning");

                entity.Property(e => e.Isogenicity).HasMaxLength(50);
            });

            modelBuilder.Entity<MatingSystem>(entity =>
            {
                entity.HasKey(e => e.System);
                entity.Property(e => e.System)
                    .HasMaxLength(20)
                    .HasColumnName("MatingSystem");
            });

            modelBuilder.Entity<MutantType>(entity =>
            {
                entity.HasKey(e => e.Type);

                entity.ToTable("MutantType");

                entity.Property(e => e.MutMeaning).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(12)
                    .HasColumnName("MutantType");
            });

            modelBuilder.Entity<PhenoInGene>(entity =>
            {
                entity.HasKey(e => new {e.Gene, e.Allele});

                entity.Property(e => e.Allele).HasMaxLength(50);

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Gene).HasMaxLength(50);

                entity.Property(e => e.PhenotypicalCategory)
                    .HasMaxLength(50)
                    .HasColumnName("Phenotypical category");

                entity.Property(e => e.PrimaryPhenotype).HasMaxLength(50);
            });

            modelBuilder.Entity<PhenotypicCategory>(entity =>
            {
                entity.HasKey(e => e.PhenotypicCategory1);

                entity.Property(e => e.AbbrevForSolgenes).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.PhenotypicCategory1)
                    .HasMaxLength(50)
                    .HasColumnName("PhenotypicCategory");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
