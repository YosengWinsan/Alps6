using Alps.Domain.AccountingMgr;
using Alps.Domain.Common;
using Alps.Domain.ProductMgr;
using Alps.Domain.PurchaseMgr;
using Alps.Domain.SaleMgr;
using Alps.Domain.StockMgr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Alps.Domain.LoanMgr;
using Alps.Domain.LogisticsMgr;
using Alps.Domain.SecurityMgr;
namespace Alps.Domain
{
    public class AlpsContext : //IdentityDbContext<AlpsUser>
        DbContext
    {
        public AlpsContext(DbContextOptions options)
            : base(options)
        {

        }
        #region DbSet属性
        #region StockMgr
        public DbSet<StockInVoucher> StockInVouchers { get; set; }
        public DbSet<StockInVoucherItem> StockInVoucherItems { get; set; }
        public DbSet<StockOutVoucher> StockOutVouchers { get; set; }
        public DbSet<StockOutVoucherItem> StockOutVoucherItems { get; set; }
        #endregion

        #region Common
        public DbSet<Unit> Units { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<County> Counties { get; set; }
        #endregion

        #region ProductMgr
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSku> ProductSkus { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        //public DbSet<Product_back_2> Products { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ProductCatagorySetting> ProductCatagorySettings { get; set; }
        public DbSet<WarehouseVoucher> WarehouseVouchers { get; set; }
        public DbSet<WarehouseVoucherItem> WarehouseVoucherItems { get; set; }
        public DbSet<DeliveryVoucher> DeliveryVouchers { get; set; }

        public DbSet<MaterialReceipt> MaterialReceipts { get; set; }
        public DbSet<MaterialReceiptItem> MaterialReceiptItems { get; set; }
        public DbSet<MaterialRequisition> MaterialRequisitions { get; set; }
        public DbSet<MaterialRequisitionItem> MaterialRequisitionItems { get; set; }
        #endregion

        #region SaleMgr
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderItem> SaleOrderItems { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        #endregion

        #region PurchaseMgr
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<SupplierClass> SupplierClasses { get; set; }
        #endregion

        #region AccountingMgr

        public DbSet<TradeAccount> TradeAccounts { get; set; }



        #endregion

        #region SecurityMgr

        public DbSet<AlpsUser> AlpsUsers { get; set; }
        public DbSet<AlpsRole> AlpsRoles { get; set; }
        public DbSet<AlpsResource> AlpsResources { get; set; }
        //public DbSet<Permission> Permissions { get; set; }

        #endregion

        #region LoanMgr

        public DbSet<Lender> Lenders { get; set; }
        public DbSet<LoanVoucher> LoanVouchers { get; set; }
        public DbSet<WithdrawRecord> WithdrawRecords { get; set; }

        #endregion
        #region LogisticsMgr
        public DbSet<DispatchRecord> DispatchRecords { get; set; }

        #endregion



        #endregion

        #region 初始化相关
        public enum InitialMode
        {
            DropAlways,
            DropIfModelChanges
        }

        public static void Initial(AlpsContext context)
        {
            //if (mode == InitialMode.DropAlways)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                new AlpsContextInitializer().Seed(context);
            }
            //else if (mode == InitialMode.DropIfModelChanges)
            //{
            //    context.Database.
            //}
            //    Database.SetInitializer<AlpsContext>(new AlpsContextInitializerDropIfModelChanges());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<DispatchRecord>().ToTable("Load_")
            foreach (var e in modelBuilder.Model.GetEntityTypes())
            {
                if (e.GetTableName().Length > 0)
                {
                    var newName = e.Name.Replace("Alps.Domain.", "").Replace("." + e.ShortName(), "").Replace("Mgr", "") + "_" + e.GetTableName();
                    e.SetTableName(newName);
                }
            }
            foreach (System.Reflection.PropertyInfo pi in typeof(AlpsContext).GetProperties().Where(p => p.Module.Name == "Alps.Domain.dll"))
            {

                if (pi.PropertyType.IsGenericType)
                {
                    //modelBuilder.ApplyConfiguration<>(new AbstractEntityTypeConfiguration());
                    Type t = typeof(AbstractEntityTypeConfiguration<>);
                    t = t.MakeGenericType(pi.PropertyType.GetGenericArguments()[0]);
                    dynamic o = Activator.CreateInstance(t);
                    modelBuilder.ApplyConfiguration(o);

                    //o.Configure(modelBuilder.Entity(pi.PropertyType.GetGenericArguments()[0]));
                    //modelBuilder.Configurations.Add(o);
                }
            }
            //modelBuilder.Entity<AlpsUser>().HasKey(p => p.Id);

            modelBuilder.Entity<WarehouseVoucher>().HasKey(p => p.ID);
            //modelBuilder.Entity<WarehouseVoucher>().Property(p => p.SourceID).IsRequired();
            modelBuilder.Entity<WarehouseVoucher>().HasOne(p => p.Supplier).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<WarehouseVoucher>().Property(p => p.DestinationID).IsRequired();
            modelBuilder.Entity<WarehouseVoucher>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WarehouseVoucherItem>().HasKey(p => new { p.ID, p.WarehouseVoucherID });

            modelBuilder.Entity<DeliveryVoucher>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DeliveryVoucher>().HasOne(p => p.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DeliveryVoucherItem>().HasKey(p => new { p.ID, p.DeliveryVoucherID });

            modelBuilder.Entity<MaterialReceipt>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaterialReceipt>().HasOne(p => p.SourceDepartment).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaterialReceiptItem>().HasKey(p => new { p.ID, p.MaterialReceiptID });

            modelBuilder.Entity<MaterialRequisition>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaterialRequisition>().HasOne(p => p.RequisitionDepartment).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaterialRequisitionItem>().HasKey(p => new { p.ID, p.MaterialRequisitionID });

            modelBuilder.Entity<StockInVoucher>().HasOne(p => p.Supplier).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StockInVoucher>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StockInVoucherItem>().HasKey(p => new { p.ID, p.StockInVoucherID });

            //modelBuilder.Entity<SaleOrderItem>().OwnsOne(p => p.Quantity);
            //modelBuilder.Entity<DistributionMgr.DistributionVoucherItem>().OwnsOne(p => p.Quantity);
            modelBuilder.Entity<DistributionVoucher>().OwnsOne(p => p.DistributionAddress).HasOne(p => p.County).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<DistributionVoucher>().HasOne(p=>p.SaleOrder).WithMany().OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<DeliveryVoucherItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<MaterialRequisitionItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<MaterialReceiptItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<WarehouseVoucherItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<PurchaseOrderItem>().OwnsOne(p => p.ProductSkuInfo);

            modelBuilder.Entity<StockInVoucher>().HasMany(p => p.Items).WithOne(p => p.StockInVoucher);
            modelBuilder.Entity<SaleOrder>().HasMany(p => p.Items).WithOne(p => p.SaleOrder);

            modelBuilder.Entity<ProductStock>().HasKey(p => new { p.OwnerID, p.PositionID, p.ProductSkuID, p.SerialNumber });

            modelBuilder.Entity<StockOutVoucher>().HasOne(p => p.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StockOutVoucher>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>().OwnsOne(p => p.Address);
            modelBuilder.Entity<Supplier>().OwnsOne(p => p.Address);

            modelBuilder.Entity<Commodity>().HasOne(p => p.Sku).WithMany().HasForeignKey(f => f.ID);

            modelBuilder.Entity<LoanVoucher>().Property(p => p.InterestRate).HasColumnType("decimal(7,4)");
            modelBuilder.Entity<WithdrawRecord>().Property(p => p.InterestRate).HasColumnType("decimal(7,4)");
            //modelBuilder.Entity<Commodity>().HasKey(p => new { p.OwnerID, p.ProductSkuID });
            // modelBuilder.Entity<Commodity>().HasOne(p=>p.ProductSku).WithOne().HasForeignKey("");
            //modelBuilder.Entity<PurchaseOrderItem>().HasOne(p => p.Unit).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<WarehouseVoucherItem>().HasOne(p => p.Unit).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Permission>().HasKey(p=>new {p.ResourceID,p.RoleID});
            modelBuilder.Entity<Permission>().HasOne(p=>p.Role).WithMany(p=>p.Permissions).HasForeignKey(p=>p.RoleID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Permission>().HasOne(p=>p.Resource).WithMany(p=>p.Permissions).HasForeignKey(p=>p.ResourceID).OnDelete(DeleteBehavior.Restrict);

        }
        public class AbstractEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
        {

            //public AbstractEntityTypeConfiguration()
            //{
            //    Property(p => p.Timestamp).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
            //        .HasColumnType("timestamp").IsConcurrencyToken();

            //}

            public void Configure(EntityTypeBuilder<T> builder)
            {
                builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsRowVersion()
                    .IsConcurrencyToken();
            }
        }
        //public class AlpsContextInitializerDropAlways : DropCreateDatabaseAlways<AlpsContext>
        //{
        //    protected override void Seed(AlpsContext context)
        //    {
        //        base.Seed(context);
        //            new AlpsContextInitializer().Seed(context);
        //    }
        //}
        //public class AlpsContextInitializerDropIfModelChanges : DropCreateDatabaseIfModelChanges<AlpsContext>
        //{
        //    protected override void Seed(AlpsContext context)
        //    {
        //        base.Seed(context);
        //        new AlpsContextInitializer().Seed(context);
        //    }
        //}
        public class AlpsContextInitializer //: DropCreateDatabaseAlways<AlpsContext>
        //DropCreateDatabaseIfModelChanges<AlpsContext>
        {
            Guid supplierID = Guid.Empty;
            Guid customerID = Guid.Empty;
            Guid departmentID = Guid.Empty;
            Guid productDepartmentID = Guid.Empty;
            Guid productID = Guid.Empty;
            Guid positionID = Guid.Empty;
            Guid unitID = Guid.Empty;
            Guid gpID = Guid.Empty;
            Guid gcSkuID = Guid.Empty;
            Guid gpSkuID = Guid.Empty;
            Guid caoCatagoryID = Guid.Empty;
            Guid jiaoCatagoryID = Guid.Empty;
            public void Seed(AlpsContext context)
            {

                //base.Seed(context);
                CommonMgrSeed(context);
                ProductMgrSeed(context);
                PurchaseMgrSeed(context);
                SaleMgrSeed(context);
                LoanMgrSeed(context);
                LogisticsMgrSeed(context);
            }
            void LogisticsMgrSeed(AlpsContext context)
            {
                context.DispatchRecords.Add(DispatchRecord.Create("闽AB8119", "系统"));
                context.DispatchRecords.Add(DispatchRecord.Create("闽AM5095", "系统"));
                context.DispatchRecords.Add(DispatchRecord.Create("闽AM5186", "系统"));
                context.SaveChanges();
            }
            void LoanMgrSeed(AlpsContext context)
            {

                Lender lender = Lender.Create("张三", "350182123", "13905911234");
                context.Lenders.Add(lender);
                lender = Lender.Create("李四", "35018212121213", "13905911231");
                context.Lenders.Add(lender);
                lender = Lender.Create("王五", "3501821234121212", "13905911232");
                context.Lenders.Add(lender);

                LoanVoucher loanvoucher = LoanVoucher.Create(lender.ID, 1000000, 0.006m, "456123", new DateTimeOffset(DateTime.Now.AddMonths(-4)));
                context.LoanVouchers.Add(loanvoucher);
                loanvoucher = LoanVoucher.Create(lender.ID, 2000000, 0.006m, "456124");
                context.LoanVouchers.Add(loanvoucher);
                context.SaveChanges();
            }
            void CommonMgrSeed(AlpsContext context)
            {

                #region 初始化管理员
                AlpsRole role = AlpsRole.Create("Admin", "管理员");
                context.AlpsRoles.Add(role);
                AlpsUser user = AlpsUser.Create("a", "a", "李", "123456", "223344");

                user.AddRole(role);
                role = AlpsRole.Create("User", "用户");
                context.AlpsRoles.Add(role);
                user.AddRole(role);
                context.AlpsUsers.Add(user);

                user = AlpsUser.Create("b", "b", "张三", "112233", "789789");
                user.AddRole(role);
                context.AlpsUsers.Add(user);

                role=AlpsRole.Create("Cashier", "出纳");
                context.AlpsRoles.Add(role);
                user=AlpsUser.Create("cw","cw","财务","11","22");
                user.AddRole(role);
                context.AlpsUsers.Add(user);



                #endregion
                #region 初始化地址

                context.Countries.Add(Country.Create("菲律宾"));
                Country country = Country.Create("中国");
                context.Countries.Add(country);
                context.Provinces.Add(Province.Create("浙江省", country.ID));
                context.Provinces.Add(Province.Create("广东省", country.ID));
                context.Provinces.Add(Province.Create("江西省", country.ID));
                context.Provinces.Add(Province.Create("湖南省", country.ID));
                Province province = Province.Create("福建省", country.ID);
                context.Provinces.Add(province);
                context.Cities.Add(City.Create("泉州市", province.ID));
                context.Cities.Add(City.Create("莆田市", province.ID));
                context.Cities.Add(City.Create("宁德市", province.ID));
                City city = City.Create("福州市", province.ID);
                context.Cities.Add(city);
                context.Counties.Add(County.Create("福清市", city.ID));
                context.Counties.Add(County.Create("闽候县", city.ID));
                context.Counties.Add(County.Create("鼓楼区", city.ID));
                County county = County.Create("长乐区", city.ID);
                context.Counties.Add(county);

                context.SaveChanges();

                #endregion

                #region 初始化部门
                Department d = Department.Create("供销科");
                context.Departments.Add(d);
                d = Department.Create("轧钢车间");
                productDepartmentID = d.ID;
                context.Departments.Add(d);
                d = Department.Create("焊管车间");
                context.Departments.Add(d);
                d = Department.Create("采购部");
                context.Departments.Add(d);
                context.SaveChanges();
                departmentID = d.ID;
                #endregion

                #region 初始化客户
                Address address = Address.Create(county, "青口钢材市场");
                Customer c = Customer.Create("江水金", address);
                context.Customers.Add(c);
                address = Address.Create(county, "青口钢材市场");
                c = Customer.Create("陈依寿", address);
                context.Customers.Add(c);
                address = Address.Create(county, "青口钢材市场");
                c = Customer.Create("林光江", address);
                context.Customers.Add(c);
                context.SaveChanges();
                customerID = c.ID;
                #endregion

                #region 初始化供应商

                SupplierClass sc = SupplierClass.Create("配件供应商");
                context.SupplierClasses.Add(sc);
                sc = SupplierClass.Create("煤碳供应商");
                context.SupplierClasses.Add(sc);
                SupplierClass gpsc = SupplierClass.Create("坯料供应商");
                context.SupplierClasses.Add(gpsc);

                address = Address.Create(county, "漳州");
                Supplier s = Supplier.Create("三宝", gpsc.ID, address);
                context.Suppliers.Add(s);
                address = Address.Create(county, "罗源");
                s = Supplier.Create("亿鑫", gpsc.ID, address);
                context.Suppliers.Add(s);
                address = Address.Create(county, "松下镇");
                s = Supplier.Create("大东海", gpsc.ID, address);
                context.Suppliers.Add(s);
                address = Address.Create(county, "吴航镇");
                s = Supplier.Create("锦强", sc.ID, address);
                context.Suppliers.Add(s);
                context.SaveChanges();
                supplierID = s.ID;
                #endregion

                #region 初始化交易账户

                TradeAccount ta = TradeAccount.Create("宏建", TradeAccountType.SupplierAndCustomer, "13900000000", "");
                context.TradeAccounts.Add(ta);
                ta = TradeAccount.Create("富鑫", TradeAccountType.SupplierAndCustomer, "13900000001", "");
                context.TradeAccounts.Add(ta);
                ta = TradeAccount.Create("江水金", TradeAccountType.Customer, "13900000003", "");
                context.TradeAccounts.Add(ta);
                ta = TradeAccount.Create("陈依寿", TradeAccountType.Customer, "13900000005", "");
                context.TradeAccounts.Add(ta);
                ta = TradeAccount.Create("永盛金属", TradeAccountType.Customer, "13900000006", "");
                context.TradeAccounts.Add(ta);
                ta = TradeAccount.Create("永盛钢贸", TradeAccountType.Customer, "13900000006", "");
                context.TradeAccounts.Add(ta);
                context.SaveChanges();
                #endregion

                #region 初始化单位
                Unit unit = Unit.Create("吨", 1, false, 1000);
                context.Units.Add(unit);
                unitID = unit.ID;
                context.Units.Add(Unit.Create("公斤", 1, true));
                context.Units.Add(Unit.Create("件", 2, true));
                context.SaveChanges();
                #endregion

                #region 初始化仓位
                context.Positions.Add(new Position() { Name = "新建616", Number = "616", Warehouse = "新建仓库" });
                Position position = new Position() { Name = "小槽315", Number = "315", Warehouse = "小槽仓库" };
                context.Positions.Add(position);


                positionID = position.ID;
                position = new Position() { Name = "坯场-1", Number = "901", Warehouse = "坯场" };
                context.Positions.Add(position);
                position = new Position() { Name = "坯场-2", Number = "902", Warehouse = "坯场" };
                context.Positions.Add(position);
                context.SaveChanges();
                #endregion

                #region 初始化类别

                Catagory nCatagory = Catagory.Create("钢材");
                nCatagory.AddChildCatagory(Catagory.Create("槽钢")
                    .AddChildCatagory(Catagory.Create("5#")).AddChildCatagory(Catagory.Create("6.3#")).
                    AddChildCatagory(Catagory.Create("8#")).AddChildCatagory(Catagory.Create("10#"))
                    .AddChildCatagory(Catagory.Create("12#")).AddChildCatagory(Catagory.Create("14#"))
                    .AddChildCatagory(Catagory.Create("16#")).AddChildCatagory(Catagory.Create("18#"))
                    .AddChildCatagory(Catagory.Create("20#")))
                    .AddChildCatagory(Catagory.Create("角钢")
                    .AddChildCatagory(Catagory.Create("3#")).AddChildCatagory(Catagory.Create("4#"))
                    .AddChildCatagory(Catagory.Create("5#")).AddChildCatagory(Catagory.Create("6#")))
                     .AddChildCatagory(Catagory.Create("工字钢")
                    .AddChildCatagory(Catagory.Create("10#")).AddChildCatagory(Catagory.Create("12#"))
                    .AddChildCatagory(Catagory.Create("14#")).AddChildCatagory(Catagory.Create("16#"))
                    .AddChildCatagory(Catagory.Create("18#")).AddChildCatagory(Catagory.Create("20a#")));
                context.Catagories.Add(nCatagory);
                nCatagory = Catagory.Create("坯料").AddChildCatagory(Catagory.Create("连铸坯"));
                context.Catagories.Add(nCatagory);
                nCatagory = Catagory.Create("镀锌板管")
                .AddChildCatagory(Catagory.Create("方管").AddChildCatagory(Catagory.Create("16方")).AddChildCatagory(Catagory.Create("20方")).AddChildCatagory(Catagory.Create("32方")))
                .AddChildCatagory(Catagory.Create("矩形管").AddChildCatagory(Catagory.Create("20*40")).AddChildCatagory(Catagory.Create("30*50")).AddChildCatagory(Catagory.Create("40*60")))
                .AddChildCatagory(Catagory.Create("圆管").AddChildCatagory(Catagory.Create("2寸")).AddChildCatagory(Catagory.Create("3寸")).AddChildCatagory(Catagory.Create("4寸")));
                context.Catagories.Add(nCatagory);
                nCatagory = Catagory.Create("轧辊");
                context.Catagories.Add(nCatagory);
                context.SaveChanges();
                #endregion

                #region 初始化产品
                Product product = null;
                Catagory associatedCatagory = context.Catagories.FirstOrDefault(p => p.Name == "5#");
                for (int i = 13; i < 30; i = i + 2)
                {
                    product = Product.Create(associatedCatagory.Name + i.ToString() + "-" + (i + 1).ToString() + "Kg",
                        associatedCatagory.Name + i.ToString() + "-" + (i + 1).ToString() + "Kg", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                    product.SetCatagory(associatedCatagory);
                    context.Products.Add(product);
                }
                associatedCatagory = context.Catagories.FirstOrDefault(p => p.Name == "6.3#");
                for (int i = 15; i < 38; i = i + 2)
                {
                    product = Product.Create(associatedCatagory.Name + i.ToString() + "-" + (i + 1).ToString() + "Kg",
                        associatedCatagory.Name + i.ToString() + "-" + (i + 1).ToString() + "Kg", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                    product.SetCatagory(associatedCatagory);
                    context.Products.Add(product);
                }
                associatedCatagory = context.Catagories.FirstOrDefault(p => p.Name == "8#");
                for (int i = 17; i < 48; i = i + 2)
                {
                    product = Product.Create(associatedCatagory.Name + i.ToString() + "-" + (i + 1).ToString() + "Kg",
                        associatedCatagory.Name + i.ToString() + "-" + (i + 1).ToString() + "Kg", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                    product.SetCatagory(associatedCatagory);
                    context.Products.Add(product);
                }
                foreach (Catagory childCatagory in context.Catagories.FirstOrDefault(p => p.Name == "工字钢").Children)
                {
                    product = Product.Create(childCatagory.Name + "下偏3-5%", childCatagory.Name + "下偏3-5%", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                    product.SetCatagory(childCatagory);
                    context.Products.Add(product);
                    product = Product.Create(childCatagory.Name + "下偏8-10%", childCatagory.Name + "下偏8-10%", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                    product.SetCatagory(childCatagory);
                    context.Products.Add(product);
                    product = Product.Create(childCatagory.Name + "下偏18-20%", childCatagory.Name + "下偏18-20%", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                    product.SetCatagory(childCatagory);
                    context.Products.Add(product);
                }

                associatedCatagory = context.Catagories.FirstOrDefault(p => p.Name == "连铸坯");
                product = Product.Create("150*150", "150*150连铸坯", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                product = Product.Create("120*120", "120*120连铸坯", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                context.SaveChanges();

                #endregion

                #region 初始化SKU
                ProductSku sku = null;
                foreach (Product p in context.Products.Where(p => p.Catagory.Name == "5#"))
                {
                    //sku = ProductSku.Create(p, "6米*144条", "系统初始化");
                    sku = ProductSku.Create(p.ID, p.Name + " " + "6米*144条", "系统初始化", "", true, "", 3800, 2.3m, 0, 0);
                    context.ProductSkus.Add(sku);
                }
                gcSkuID = sku.ID;
                foreach (Product p in context.Products.Where(p => p.Catagory.Name == "6.3#"))
                {
                    sku = ProductSku.Create(p.ID, p.Name + " " + "6米*96条", "系统初始化", "", true, "", 3800, 2.3m, 0, 0);
                    //sku = ProductSku.Create(p, "6米*96条", "系统初始化");
                    context.ProductSkus.Add(sku);
                }
                foreach (Product p in context.Products.Where(p => p.Catagory.Name == "8#"))
                {
                    sku = ProductSku.Create(p.ID, p.Name + " " + "6米*84条", "系统初始化", "", true, "", 3800, 3.6m, 0, 0);
                    //sku = ProductSku.Create(p, "6米*84条", "系统初始化");
                    context.ProductSkus.Add(sku);
                    sku = ProductSku.Create(p.ID, p.Name + " " + "6米*64条", "系统初始化", "", true, "", 3800, 3.6m, 0, 0);
                    //sku = ProductSku.Create(p, "9米*64条", "系统初始化");
                    context.ProductSkus.Add(sku);
                }
                // foreach (Product p in context.Products.Where(p => p.Catagory.Name == "连铸坯"))
                // {
                //     sku = ProductSku.Create(p.ID, p.Name + " " + "6米", "系统初始化", "", false);
                //     //sku = ProductSku.Create(p, "6米", "系统初始化");
                //     context.ProductSkus.Add(sku);
                //     sku = ProductSku.Create(p.ID, p.Name + " " + "12米", "系统初始化", "", false);
                //     //sku = ProductSku.Create(p, "12米", "系统初始化");
                //     context.ProductSkus.Add(sku);
                // }
                var gp = context.Products.FirstOrDefault(p => p.Name == "150*150");
                var newSku = ProductSku.Create(gp.ID, "亿鑫150*150*12M", "", "9001", false);
                context.ProductSkus.Add(newSku);
                gpSkuID = newSku.ID;
                newSku = ProductSku.Create(gp.ID, "大东海150*150*12M", "", "9002", false);
                context.ProductSkus.Add(newSku);
                newSku = ProductSku.Create(gp.ID, "春兴150*150*12M", "", "9003", false);
                context.ProductSkus.Add(newSku);
                newSku = ProductSku.Create(gp.ID, "东华150*150*12M", "", "9003", false);
                context.ProductSkus.Add(newSku);

                context.SaveChanges();

                #endregion
            }
            void ProductMgrSeed(AlpsContext context)
            {

                var ysjsID = context.Departments.FirstOrDefault(p => p.Name == "轧钢车间").ID;
                var cgID = context.ProductSkus.FirstOrDefault().ID;
                #region 初始化库存
                context.ProductStocks.Add(ProductStock.Create(ysjsID, positionID, cgID, "900001", 2.5m, 1));
                context.ProductStocks.Add(ProductStock.Create(ysjsID, positionID, cgID, "900002", 2.52m, 1));
                context.SaveChanges();
                #endregion

                #region 初始化入库单--StockInVoucher
                var skuID = context.ProductSkus.Find(gcSkuID).ID;
                var sID = context.Suppliers.FirstOrDefault().ID;
                var dID = context.Departments.OrderByDescending(p => p.ID).FirstOrDefault().ID;
                StockInVoucher voucher = StockInVoucher.Create(sID, dID, "系统初始化");
                voucher.AddItem(positionID, skuID, "12345", 2.3m, 1, 2100);
                voucher.AddItem(positionID, skuID, "151515", 2.5m, 1, 2100);
                context.StockInVouchers.Add(voucher);
                context.SaveChanges();
                voucher = StockInVoucher.Create(sID, dID, "系统初始化2");
                voucher.AddItem(positionID, skuID, "600001", 2.4m, 1, 2000);
                voucher.AddItem(positionID, skuID, "600002", 2.6m, 1, 2000);
                context.StockInVouchers.Add(voucher);
                skuID = context.ProductSkus.Find(gpSkuID).ID;
                voucher = StockInVoucher.Create(sID, dID, "系统初始化3");
                voucher.AddItem(positionID, skuID, "", 1000m, 1500, 1800);
                voucher.AddItem(positionID, skuID, "", 2000m, 3000, 1700);
                context.StockInVouchers.Add(voucher);
                voucher = StockInVoucher.Create(sID, dID, "系统初始化3");
                voucher.AddItem(positionID, skuID, "", 500m, 750, 1700);
                context.StockInVouchers.Add(voucher);
                context.SaveChanges();
                #endregion

                #region 初始化出库单--StockOutVoucher
                var cID = context.Customers.FirstOrDefault().ID;
                StockOutVoucher dv = StockOutVoucher.Create(dID, cID, "系统初始化");
                skuID = context.ProductSkus.Find(gcSkuID).ID;
                dv.AddItem(positionID, skuID, "900001", 2.5m, 1, 2000);
                context.StockOutVouchers.Add(dv);
                skuID = context.ProductSkus.Find(gpSkuID).ID;
                dv = StockOutVoucher.Create(dID, cID, "系统初始化");
                dv.AddItem(positionID, skuID, "900003", 2.5m, 2, 2000);
                context.StockOutVouchers.Add(dv);
                context.SaveChanges();
                #endregion
            }
            void SaleMgrSeed(AlpsContext context)
            {
                var quantity = 100;
                foreach (var sku in context.ProductSkus)
                {
                    context.Commodities.Add(Commodity.Create(sku.ID, sku.FullName, sku.Description, 3800, quantity * 3, quantity++));

                }
                context.SaveChanges();
                var saleOrder = SaleOrder.Create(customerID);
                // foreach (var commodity in context.Commodities.Take(5))
                // {
                //     saleOrder.AddItem(commodity.ID, 12, 2000, 5, "#");
                // }
                foreach (var productSku in context.ProductSkus.Take(5))
                {
                    saleOrder.AddItem(productSku.ID, productSku.Name, 12, 2000, 5, "#");
                }
                context.SaleOrders.Add(saleOrder);
                saleOrder = SaleOrder.Create(customerID);
                foreach (var productSku in context.ProductSkus.OrderByDescending(p => p.ID).Take(5))
                {
                    saleOrder.AddItem(productSku.ID, productSku.Name, 1, 3000, 1, "#");
                }
                // foreach (var commodity in context.Commodities.OrderByDescending(p => p.ID).Take(5))
                // {
                //     saleOrder.AddItem(commodity.ID, 1, 3000, 1, "#");
                // }
                context.SaleOrders.Add(saleOrder);

                context.SaveChanges();
            }

            void PurchaseMgrSeed(AlpsContext context)
            {
                #region 采购订单初始化
                // PurchaseOrder purchaseOrder = PurchaseOrder.Create(supplierID, "系统初始化");
                // ProductSkuInfo gcpsi = context.ProductSkus.Find(gcSkuID).GetProductSkuInfo();
                // purchaseOrder.AddItem(gcpsi, 10, 3.2m, 2000);
                // purchaseOrder.AddItem(gcpsi, 20, 0, 2000);
                // context.PurchaseOrders.Add(purchaseOrder);

                // purchaseOrder = PurchaseOrder.Create(supplierID, "系统初始化");
                // ProductSkuInfo gppsi = context.ProductSkus.Find(gpSkuID).GetProductSkuInfo();
                // purchaseOrder.AddItem(gppsi, 1333, 1000m, 1800);
                // context.PurchaseOrders.Add(purchaseOrder);
                // context.SaveChanges();
                #endregion
            }
        }
        #endregion

    }

}
