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
        #endregion

        #region AccountingMgr

        public DbSet<TradeAccount> TradeAccounts { get; set; }
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

            //modelBuilder.Entity<WarehouseVoucher>().Property(p => p.Destination).IsRequired();//.HasOne(p => p.Destination).WithRequiredPrincipal();
            //modelBuilder.Entity<WarehouseVoucher>().HasOne(p => p.Destination).WithOptional().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<WarehouseVoucher>().HasOne(p => p.Source).WithOptional().OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
            foreach (System.Reflection.PropertyInfo pi in typeof(AlpsContext).GetProperties().Where(p => p.Module.Name == "Alps.Domain.ERP.dll"))
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

            modelBuilder.Entity<StockInVoucher>().HasOne(p => p.Source).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StockInVoucher>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StockInVoucherItem>().HasKey(p => new { p.ID, p.StockInVoucherID });

            modelBuilder.Entity<SaleOrderItem>().OwnsOne(p => p.Quantity);
            modelBuilder.Entity<DistributionMgr.DistributionVoucherItem>().OwnsOne(p => p.Quantity);
            modelBuilder.Entity<DeliveryVoucherItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<MaterialRequisitionItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<MaterialReceiptItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<WarehouseVoucherItem>().OwnsOne(p => p.ProductSkuInfo);
            modelBuilder.Entity<PurchaseOrderItem>().OwnsOne(p => p.ProductSkuInfo);

            modelBuilder.Entity<StockInVoucher>().HasMany(p => p.Items).WithOne(p => p.StockInVoucher);

            modelBuilder.Entity<ProductStock>().HasKey(p => new { p.OwnerID, p.PositionID, p.ProductSkuID, p.SerialNumber });

            modelBuilder.Entity<StockOutVoucher>().HasOne(p => p.Destination).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StockOutVoucher>().HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<PurchaseOrderItem>().HasOne(p => p.Unit).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<WarehouseVoucherItem>().HasOne(p => p.Unit).WithMany().OnDelete(DeleteBehavior.Restrict);

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
            }
            void CommonMgrSeed(AlpsContext context)
            {
                #region 初始化部门
                Department d = Department.Create("供销科");
                context.Departments.Add(d);
                d = Department.Create("大槽车间");
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
                Customer c = Customer.Create("江水金");
                context.Customers.Add(c);
                c = Customer.Create("陈依寿");
                context.Customers.Add(c);
                c = Customer.Create("林光江");
                context.Customers.Add(c);
                context.SaveChanges();
                customerID = c.ID;
                #endregion

                #region 初始化供应商
                Supplier s = Supplier.Create("宏建");
                context.Suppliers.Add(s);
                s = Supplier.Create("恒辉");
                context.Suppliers.Add(s);
                s = Supplier.Create("富鑫");
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
                context.SaveChanges();
                positionID = position.ID;
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
                    .AddChildCatagory(Catagory.Create("5#")).AddChildCatagory(Catagory.Create("6#"))
                    );
                context.Catagories.Add(nCatagory);
                Catagory newCatagory = Catagory.Create("槽");
                context.Catagories.Add(newCatagory);
                caoCatagoryID = newCatagory.ID;
                newCatagory = Catagory.Create("角");
                context.Catagories.Add(newCatagory);
                jiaoCatagoryID = newCatagory.ID;
                newCatagory = Catagory.Create("连铸坯");
                context.Catagories.Add(newCatagory);
                newCatagory = Catagory.Create("电机");
                context.Catagories.Add(newCatagory);
                newCatagory = Catagory.Create("轧辊");
                context.Catagories.Add(newCatagory);
                newCatagory = Catagory.Create("镀锌板管");
                context.Catagories.Add(newCatagory);
                newCatagory.AddChildCatagory(Catagory.Create("方管"));
                newCatagory.AddChildCatagory(Catagory.Create("矩形管"));
                newCatagory.AddChildCatagory(Catagory.Create("圆管"));
                context.SaveChanges();
                #endregion

                #region 初始化产品
                Product product = null;
                Catagory associatedCatagory = context.Catagories.FirstOrDefault(p => p.Name == "5#");
                for(int i=13; i<30;i=i+2)
                {
                    product = Product.Create(associatedCatagory.Name+ i.ToString()+"-"+(i+1).ToString()+"Kg", 
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
                associatedCatagory = context.Catagories.FirstOrDefault(p => p.Name == "连铸坯");
                product = Product.Create("150连铸坯", "150*150连铸坯", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                product = Product.Create("120连铸坯", "120*120连铸坯", "系统创建", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                //Catagory associatedCatagory = context.Catagories.Find(caoCatagoryID);
                //product = Product.Create("5#槽钢", "5#槽钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                //product.SetCatagory(associatedCatagory);
                //context.Products.Add(product);
                //productID = product.ID;
                //product = Product.Create("6#槽钢", "6#槽钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                //product.SetCatagory(associatedCatagory);
                //context.Products.Add(product);
                //product = Product.Create("8#槽钢", "8#槽钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                //product.SetCatagory(associatedCatagory);
                //context.Products.Add(product);
                //product = Product.Create("10#槽钢", "10#槽钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                //product.SetCatagory(associatedCatagory);
                //context.Products.Add(product);
                //product = Product.Create("12#槽钢", "12#槽钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                //product.SetCatagory(associatedCatagory);
                //context.Products.Add(product);
                //product = Product.Create("14#槽钢", "14#槽钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                //product.SetCatagory(associatedCatagory);
                //context.Products.Add(product);
                associatedCatagory = context.Catagories.Find(jiaoCatagoryID);
                product = Product.Create("3#角钢", "3#角钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                product = Product.Create("4#角钢", "4#角钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                product = Product.Create("5#角钢", "5#角钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                product = Product.Create("6#角钢", "6#角钢", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                product.SetCatagory(associatedCatagory);
                context.Products.Add(product);
                //product = Product.Create("连铸坯", "连铸坯", "不想多说", PricingMethod.PricingByWeight, 2000, unitID);
                //context.Products.Add(product);
                //gpID = product.ID;
                //product = Product.Create("2极电机", "2极电机", "傻瓜都看的懂", PricingMethod.PricingByQuantity, 4000, unitID);
                //context.Products.Add(product);
                //product = Product.Create("4极电机", "4极电机", "傻瓜都看的懂", PricingMethod.PricingByQuantity, 4000, unitID);
                //context.Products.Add(product);
                //product = Product.Create("750轧辊", "750轧辊", "傻瓜都看的懂", PricingMethod.PricingByQuantity, 9000, unitID);
                //context.Products.Add(product);
                //product = Product.Create("700轧辊", "700轧辊", "傻瓜都看的懂", PricingMethod.PricingByQuantity, 9000, unitID);
                //context.Products.Add(product);
                context.SaveChanges();
                #endregion

                #region 初始化SKU
                ProductSku sku = null;
                foreach (Product p in context.Products.Where(p => p.Catagory.Name == "5#"))
                {
                        sku = ProductSku.Create(p.ID,p.Name + "*6米*144", 0, PricingMethod.PricingByQuantity, 2000);
                        context.ProductSkus.Add(sku);
                }
                gcSkuID = sku.ID;
                foreach (Product p in context.Products.Where(p => p.Catagory.Name == "6.3#"))
                {
                    sku = ProductSku.Create(p.ID, p.Name + "*6米*96条", 0, PricingMethod.PricingByQuantity, 2000);
                    context.ProductSkus.Add(sku);
                }
                foreach (Product p in context.Products.Where(p => p.Catagory.Name == "8#"))
                {
                    sku = ProductSku.Create(p.ID, p.Name + "*6米*84条", 0, PricingMethod.PricingByQuantity, 2000);
                    context.ProductSkus.Add(sku);
                    sku = ProductSku.Create(p.ID, p.Name + "*9米*64条", 0, PricingMethod.PricingByQuantity, 2000);
                    context.ProductSkus.Add(sku);
                }
                foreach (Product p in context.Products.Where(p => p.Catagory.Name == "连铸坯"))
                {
                    sku = ProductSku.Create(p.ID, p.Name + "*6米", 0, PricingMethod.PricingByQuantity, 2000);
                    context.ProductSkus.Add(sku);
                    sku = ProductSku.Create(p.ID, p.Name + "*12米", 0, PricingMethod.PricingByQuantity, 2000);
                    context.ProductSkus.Add(sku);
                }
                gpSkuID = sku.ID;
                //ProductSku sku = null;
                //int tempKg = 20;
                //foreach (Product p in context.Products.Where(p => p.Name.Contains("槽钢")))
                //{
                //    for (var i = 0; i < 10; i++)
                //    {
                //        sku = ProductSku.Create(p, (tempKg + i).ToString() + "公斤*6米*96条", 0, PricingMethod.PricingByQuantity, 2000);
                //        context.ProductSkus.Add(sku);
                //    }
                //    tempKg = tempKg + 2;
                //    //sku = ProductSku.Create(p, "200条包装", 0, PricingMethod.PricingByWeight, 2000);
                //    //context.ProductSkus.Add(sku);
                //}
                //gcSkuID = sku.ID;
                //foreach (Product p in context.Products.Where(p => p.Name.Contains("角钢")))
                //{
                //    sku = ProductSku.Create(p, "100条包装", 0, PricingMethod.PricingByQuantity, 2000);
                //    context.ProductSkus.Add(sku);
                //    sku = ProductSku.Create(p, "200条包装", 0, PricingMethod.PricingByWeight, 2000);
                //    context.ProductSkus.Add(sku);
                //}
                //foreach (Product p in context.Products.Where(p => p.Name.Contains("连铸坯")))
                //{
                //    sku = ProductSku.Create(p, "150方", 0, PricingMethod.PricingByWeight, 2000);
                //    context.ProductSkus.Add(sku);
                //    sku = ProductSku.Create(p, "120方", 0, PricingMethod.PricingByQuantity, 2000);
                //    context.ProductSkus.Add(sku);
                //}
                //gpSkuID = sku.ID;
                context.SaveChanges();

                #endregion
            }
            void ProductMgrSeed(AlpsContext context)
            {
                #region 无用


                //Catagory newCatagory = Catagory.Create("槽");
                //var childCatagory = Catagory.Create("8#");
                //newCatagory.AddChildCatagory(childCatagory);
                //newCatagory.AddChildCatagory(Catagory.Create("10#"));
                //newCatagory.AddChildCatagory(Catagory.Create("12#"));
                //context.Catagories.Add(newCatagory);
                //newCatagory = Catagory.Create("角");
                //context.Catagories.Add(newCatagory);
                //newCatagory.AddChildCatagory(Catagory.Create("3#"));
                //newCatagory.AddChildCatagory(Catagory.Create("4#"));
                //newCatagory.AddChildCatagory(Catagory.Create("5#"));
                //childCatagory = Catagory.Create("6#");
                //newCatagory.AddChildCatagory(childCatagory);
                //Product product = null;
                //childCatagory = context.Catagories.Local.FirstOrDefault(p => p.Name == "8#");
                //newCatagory = context.Catagories.Local.FirstOrDefault(p => p.Name == "5#");
                //for (int i = 0; i < 10; i++)
                //{
                //    product = Product.Create((i + 20).ToString() + "Kg", "8#槽钢" + (i + 20).ToString() + "Kg", "不想多说", PricingMethod.PricingByQuantity, 2000, unitID);
                //    context.Products.Add(product);
                //    product.AddAssociatedCatagory(childCatagory, 0);
                //    product = Product.Create((i + 5).ToString() + "Kg", "5#角钢" + (i + 5).ToString() + "Kg", "不想多说", PricingMethod.PricingByWeight, 1000, unitID);
                //    product.AddAssociatedCatagory(newCatagory, 0);
                //    context.Products.Add(product);
                //}

                //context.SaveChanges();
                //productID = product.ID;
                //product = Product.Create("连铸坯*120*6", "连铸坯*120*6", "连铸坯*120*6", PricingMethod.PricingByWeight, 2000, unitID);
                //context.Products.Add(product);
                //context.SaveChanges();
                //gpID = product.ID;
                #endregion

                //#region 初始化入库单

                //ProductSku sku = context.ProductSkus.Find(gcSkuID);
                //WarehouseVoucher voucher = WarehouseVoucher.Create(supplierID, departmentID, "系统初始化");
                //ProductSkuInfo psi = sku.GetProductSkuInfo();  
                //voucher.AddItem(psi, 2.3m, 1, 2100, "12345", positionID);
                //voucher.AddItem(psi, 2.5m, 1, 2100, "151515", positionID);
                //context.WarehouseVouchers.Add(voucher);         
                //context.SaveChanges();
                //voucher = WarehouseVoucher.Create(supplierID, departmentID, "系统初始化2");
                //voucher.AddItem(psi, 2.4m, 1, 2000, "600001", positionID);
                //voucher.AddItem(psi, 2.6m, 1, 2000, "600002", positionID);
                //context.WarehouseVouchers.Add(voucher);
                //psi = context.ProductSkus.Find(gpSkuID).GetProductSkuInfo();
                //voucher = WarehouseVoucher.Create(supplierID, departmentID, "系统初始化3");
                //voucher.AddItem(psi, 1000m, 1500, 1800, "", positionID);
                //voucher.AddItem(psi, 2000m, 3000, 1700, "", positionID);
                //context.WarehouseVouchers.Add(voucher);
                //voucher = WarehouseVoucher.Create(supplierID, departmentID, "系统初始化3");
                //voucher.AddItem(psi, 500m, 750, 1700, "", positionID);
                //context.WarehouseVouchers.Add(voucher);
                //context.SaveChanges();

                //#endregion

                //#region 初始化库存
                //context.ProductStocks.Add(ProductStock.Create(gcSkuID, 1, 2.5m, departmentID, positionID, "900001"));
                //context.ProductStocks.Add(ProductStock.Create(gpSkuID, 1, 2.52m, departmentID, positionID, "900002"));
                //context.SaveChanges();
                //#endregion

                //#region 初始化领料单
                //MaterialRequisition r = MaterialRequisition.Create(departmentID, productDepartmentID, "系统");
                //psi = context.ProductSkus.Find(gpSkuID).GetProductSkuInfo();
                //r.AddItem(psi, 1500, 1000, 1800, "", positionID);
                //context.MaterialRequisitions.Add(r);
                //context.SaveChanges();
                //#endregion

                //#region 初始化收料单
                //MaterialReceipt s = MaterialReceipt.Create(departmentID, productDepartmentID, "系统");
                //psi = context.ProductSkus.Find(gcSkuID).GetProductSkuInfo();
                //s.AddItem(psi, 1, 2.33m, 1800, "556699", positionID);
                //context.MaterialReceipts.Add(s);
                //context.SaveChanges();
                //#endregion

                //#region 初始化出库单
                //psi = context.ProductSkus.Find(gcSkuID).GetProductSkuInfo();
                //DeliveryVoucher dv = DeliveryVoucher.Create(departmentID, customerID, "系统初始化");
                //dv.AddItem(psi, 1, 2.5m, 2000, "900001", positionID, "");
                //context.DeliveryVouchers.Add(dv);
                //psi = context.ProductSkus.Find(gpSkuID).GetProductSkuInfo();
                //dv = DeliveryVoucher.Create(departmentID, customerID, "系统初始化");
                //dv.AddItem(psi, 2, 2.5m, 2000, "900003", positionID, "");
                //context.DeliveryVouchers.Add(dv);
                //context.SaveChanges();
                //#endregion

                var ysjsID = context.TradeAccounts.FirstOrDefault(p => p.Name == "永盛金属").ID;
                var cgID = context.ProductSkus.FirstOrDefault().ID;
                #region 初始化库存
                context.ProductStocks.Add(ProductStock.Create(ysjsID, positionID, cgID, "900001", 2.5m, 1));
                context.ProductStocks.Add(ProductStock.Create(ysjsID, positionID, cgID, "900002", 2.52m, 1));
                context.SaveChanges();
                #endregion

                #region 初始化入库单--StockInVoucher
                var skuID = context.ProductSkus.Find(gcSkuID).ID;
                var sID = context.TradeAccounts.FirstOrDefault().ID;
                var dID = context.TradeAccounts.LastOrDefault().ID;
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
                StockOutVoucher dv = StockOutVoucher.Create(dID, sID, "系统初始化");
                skuID = context.ProductSkus.Find(gcSkuID).ID;
                dv.AddItem(positionID, skuID, "900001", 2.5m, 1, 2000);
                context.StockOutVouchers.Add(dv);
                skuID = context.ProductSkus.Find(gpSkuID).ID;
                dv = StockOutVoucher.Create(dID, sID, "系统初始化");
                dv.AddItem(positionID, skuID, "900003", 2.5m, 2, 2000);
                context.StockOutVouchers.Add(dv);
                context.SaveChanges();
                #endregion
            }
            void SaleMgrSeed(AlpsContext context)
            {
                var goods = Commodity.Create(productID, "5#4Kg", "槽钢", 1700, 100);
                context.Commodities.Add(goods);
                var saleOrder = SaleOrder.Create(customerID);
                context.SaleOrders.Add(saleOrder);
                saleOrder = SaleOrder.Create(customerID, saleOrder);
                context.SaleOrders.Add(saleOrder);

                context.SaveChanges();
            }

            void PurchaseMgrSeed(AlpsContext context)
            {
                #region 采购订单初始化
                PurchaseOrder purchaseOrder = PurchaseOrder.Create(supplierID, "系统初始化");
                ProductSkuInfo gcpsi = context.ProductSkus.Find(gcSkuID).GetProductSkuInfo();
                purchaseOrder.AddItem(gcpsi, 10, 3.2m, 2000);
                purchaseOrder.AddItem(gcpsi, 20, 0, 2000);
                context.PurchaseOrders.Add(purchaseOrder);

                purchaseOrder = PurchaseOrder.Create(supplierID, "系统初始化");
                ProductSkuInfo gppsi = context.ProductSkus.Find(gpSkuID).GetProductSkuInfo();
                purchaseOrder.AddItem(gppsi, 1333, 1000m, 1800);
                context.PurchaseOrders.Add(purchaseOrder);
                context.SaveChanges();
                #endregion
            }
        }
        #endregion

    }

}
