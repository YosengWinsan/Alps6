using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alps.Web.Service.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlpsResources",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlpsResources", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AlpsUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IDName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    MobilePhoneNumber = table.Column<string>(nullable: true),
                    IdentityNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlpsUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Catagories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ParentID = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    IsParentCatagory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Catagories_Catagories_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Catagories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DispatchRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    RecordNumber = table.Column<long>(nullable: false),
                    CarNumber = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    ModifyTime = table.Column<DateTimeOffset>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    GrossWeight = table.Column<decimal>(nullable: false),
                    TareWeight = table.Column<decimal>(nullable: false),
                    GrossWeightTime = table.Column<DateTimeOffset>(nullable: false),
                    GrossWeightOperator = table.Column<string>(nullable: true),
                    TareWeightOperator = table.Column<string>(nullable: true),
                    TareWeightTime = table.Column<DateTimeOffset>(nullable: false),
                    WeightConfirmedTime = table.Column<DateTimeOffset>(nullable: false),
                    WeightConfirmedOperator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispatchRecords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lenders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IDNumber = table.Column<string>(nullable: true),
                    MobilePhoneNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lenders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Warehouse = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCatagorySettings",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ProductID = table.Column<Guid>(nullable: false),
                    CatagoryID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatagorySettings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupplierClasses",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierClasses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TradeAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BankAccount = table.Column<string>(nullable: true),
                    CellPhoneNumber = table.Column<string>(nullable: true),
                    InventoryManagement = table.Column<bool>(nullable: false),
                    Types = table.Column<int>(nullable: false),
                    Test = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Group = table.Column<int>(nullable: false),
                    IsBaseUnit = table.Column<bool>(nullable: false),
                    RateOfExchange = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AlpsRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AlpsUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlpsRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlpsRoles_AlpsUsers_AlpsUserID",
                        column: x => x.AlpsUserID,
                        principalTable: "AlpsUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CountryID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialReceipts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SourceDepartmentID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    SubmitUser = table.Column<string>(nullable: true),
                    Handler = table.Column<string>(nullable: true),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialReceipts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialReceipts_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialReceipts_Departments_SourceDepartmentID",
                        column: x => x.SourceDepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialRequisitions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    RequisitionDepartmentID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    SubmitUser = table.Column<string>(nullable: true),
                    Handler = table.Column<string>(nullable: true),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequisitions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialRequisitions_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialRequisitions_Departments_RequisitionDepartmentID",
                        column: x => x.RequisitionDepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DepositDate = table.Column<DateTimeOffset>(nullable: false),
                    LenderID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    OriginAmount = table.Column<decimal>(nullable: false),
                    InterestSettlementDate = table.Column<DateTimeOffset>(nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    VoucherNumber = table.Column<string>(nullable: true),
                    HashCode = table.Column<string>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    ModifyDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoanVouchers_Lenders_LenderID",
                        column: x => x.LenderID,
                        principalTable: "Lenders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    FullDescription = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    BaseUnitID = table.Column<Guid>(nullable: false),
                    EnableAuxiliaryUnit = table.Column<bool>(nullable: false),
                    AuxiliaryUnitID = table.Column<Guid>(nullable: true),
                    PricingMethod = table.Column<int>(nullable: false),
                    ListPrice = table.Column<decimal>(nullable: false),
                    CatagoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Units_BaseUnitID",
                        column: x => x.BaseUnitID,
                        principalTable: "Units",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Catagories_CatagoryID",
                        column: x => x.CatagoryID,
                        principalTable: "Catagories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    RoleID = table.Column<Guid>(nullable: false),
                    ResourceID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permissions_AlpsResources_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "AlpsResources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_AlpsRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "AlpsRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ProvinceID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialReceiptItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    MaterialReceiptID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialReceiptItems", x => new { x.ID, x.MaterialReceiptID });
                    table.ForeignKey(
                        name: "FK_MaterialReceiptItems_MaterialReceipts_MaterialReceiptID",
                        column: x => x.MaterialReceiptID,
                        principalTable: "MaterialReceipts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialRequisitionItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    MaterialRequisitionID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequisitionItems", x => new { x.ID, x.MaterialRequisitionID });
                    table.ForeignKey(
                        name: "FK_MaterialRequisitionItems_MaterialRequisitions_MaterialRequisitionID",
                        column: x => x.MaterialRequisitionID,
                        principalTable: "MaterialRequisitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LoanVoucherID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DepositDate = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Interest = table.Column<decimal>(nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    ModifyDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawRecords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WithdrawRecords_LoanVouchers_LoanVoucherID",
                        column: x => x.LoanVoucherID,
                        principalTable: "LoanVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSkus",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ProductID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Vendable = table.Column<bool>(nullable: false),
                    CommodityName = table.Column<string>(nullable: true),
                    ListPrice = table.Column<decimal>(nullable: false),
                    QuantityRate = table.Column<decimal>(nullable: false),
                    StockQuantity = table.Column<decimal>(nullable: false),
                    PreSellQuantity = table.Column<decimal>(nullable: false),
                    OrderedQuantity = table.Column<decimal>(nullable: false),
                    StockAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    PreSellAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    OrderedAuxiliaryQuantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSkus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSkus_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CityID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Counties_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commodities",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    ListPrice = table.Column<decimal>(nullable: false),
                    StockQuantity = table.Column<decimal>(nullable: false),
                    PreSellQuantity = table.Column<decimal>(nullable: false),
                    OrderedQuantity = table.Column<decimal>(nullable: false),
                    StockAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    PreSellAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    OrderedAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    IsFutures = table.Column<bool>(nullable: false),
                    DateOfDelivery = table.Column<DateTime>(nullable: true),
                    IsVirtualCommodity = table.Column<bool>(nullable: false),
                    QuantityRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commodities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Commodities_ProductSkus_ID",
                        column: x => x.ID,
                        principalTable: "ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    PositionID = table.Column<Guid>(nullable: false),
                    OwnerID = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => new { x.OwnerID, x.PositionID, x.ProductSkuID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_ProductStocks_Departments_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStocks_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStocks_ProductSkus_ProductSkuID",
                        column: x => x.ProductSkuID,
                        principalTable: "ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Address_CountyID = table.Column<Guid>(nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_Counties_Address_CountyID",
                        column: x => x.Address_CountyID,
                        principalTable: "Counties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address_CountyID = table.Column<Guid>(nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    SupplierClassID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suppliers_SupplierClasses_SupplierClassID",
                        column: x => x.SupplierClassID,
                        principalTable: "SupplierClasses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suppliers_Counties_Address_CountyID",
                        column: x => x.Address_CountyID,
                        principalTable: "Counties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    CustomerID = table.Column<Guid>(nullable: false),
                    Handler = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    SubmitUser = table.Column<string>(nullable: true),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeliveryVouchers_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryVouchers_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CustomerID = table.Column<Guid>(nullable: false),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    ParentSaleOrderID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrders_SaleOrders_ParentSaleOrderID",
                        column: x => x.ParentSaleOrderID,
                        principalTable: "SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockOutVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CustomerID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    Confirmer = table.Column<string>(nullable: true),
                    ConfirmTime = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOutVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockOutVouchers_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockOutVouchers_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    SupplierID = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockInVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SupplierID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    Confirmer = table.Column<string>(nullable: true),
                    ConfirmTime = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    DispatchRecordID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockInVouchers_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockInVouchers_DispatchRecords_DispatchRecordID",
                        column: x => x.DispatchRecordID,
                        principalTable: "DispatchRecords",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockInVouchers_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    SupplierID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    SubmitUser = table.Column<string>(nullable: true),
                    Handler = table.Column<string>(nullable: true),
                    TotalQuantity = table.Column<decimal>(nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WarehouseVouchers_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseVouchers_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryVoucherItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DeliveryVoucherID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    PurchaseOrderItemID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryVoucherItem", x => new { x.ID, x.DeliveryVoucherID });
                    table.ForeignKey(
                        name: "FK_DeliveryVoucherItem_DeliveryVouchers_DeliveryVoucherID",
                        column: x => x.DeliveryVoucherID,
                        principalTable: "DeliveryVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryVoucherItem_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributionVoucher",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SaleOrderID = table.Column<Guid>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    DistributionAddress_CountyID = table.Column<Guid>(nullable: false),
                    DistributionAddress_Street = table.Column<string>(nullable: true),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionVoucher", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DistributionVoucher_SaleOrders_SaleOrderID",
                        column: x => x.SaleOrderID,
                        principalTable: "SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionVoucher_Counties_DistributionAddress_CountyID",
                        column: x => x.DistributionAddress_CountyID,
                        principalTable: "Counties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SaleOrderID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    CommodityName = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_ProductSkus_ProductSkuID",
                        column: x => x.ProductSkuID,
                        principalTable: "ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_SaleOrders_SaleOrderID",
                        column: x => x.SaleOrderID,
                        principalTable: "SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockOutVoucherItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StockOutVoucherID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PositionID = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOutVoucherItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockOutVoucherItems_StockOutVouchers_StockOutVoucherID",
                        column: x => x.StockOutVoucherID,
                        principalTable: "StockOutVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    PurchaseOrderID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderID",
                        column: x => x.PurchaseOrderID,
                        principalTable: "PurchaseOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockInVoucherItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StockInVoucherID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PositionID = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInVoucherItems", x => new { x.ID, x.StockInVoucherID });
                    table.ForeignKey(
                        name: "FK_StockInVoucherItems_StockInVouchers_StockInVoucherID",
                        column: x => x.StockInVoucherID,
                        principalTable: "StockInVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseVoucherItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    WarehouseVoucherID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    PurchaseOrderItemID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Freight = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseVoucherItems", x => new { x.ID, x.WarehouseVoucherID });
                    table.ForeignKey(
                        name: "FK_WarehouseVoucherItems_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseVoucherItems_WarehouseVouchers_WarehouseVoucherID",
                        column: x => x.WarehouseVoucherID,
                        principalTable: "WarehouseVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributionVoucherItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SaleOrderItemID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    DistributionVoucherID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionVoucherItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DistributionVoucherItem_DistributionVoucher_DistributionVoucherID",
                        column: x => x.DistributionVoucherID,
                        principalTable: "DistributionVoucher",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistributionVoucherItem_ProductSkus_ProductSkuID",
                        column: x => x.ProductSkuID,
                        principalTable: "ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlpsRoles_AlpsUserID",
                table: "AlpsRoles",
                column: "AlpsUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Catagories_ParentID",
                table: "Catagories",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceID",
                table: "Cities",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Counties_CityID",
                table: "Counties",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Address_CountyID",
                table: "Customers",
                column: "Address_CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVoucherItem_DeliveryVoucherID",
                table: "DeliveryVoucherItem",
                column: "DeliveryVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVoucherItem_PositionID",
                table: "DeliveryVoucherItem",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVouchers_CustomerID",
                table: "DeliveryVouchers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVouchers_DepartmentID",
                table: "DeliveryVouchers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionVoucher_SaleOrderID",
                table: "DistributionVoucher",
                column: "SaleOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionVoucher_DistributionAddress_CountyID",
                table: "DistributionVoucher",
                column: "DistributionAddress_CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionVoucherItem_DistributionVoucherID",
                table: "DistributionVoucherItem",
                column: "DistributionVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionVoucherItem_ProductSkuID",
                table: "DistributionVoucherItem",
                column: "ProductSkuID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanVouchers_LenderID",
                table: "LoanVouchers",
                column: "LenderID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReceiptItems_MaterialReceiptID",
                table: "MaterialReceiptItems",
                column: "MaterialReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReceipts_DepartmentID",
                table: "MaterialReceipts",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReceipts_SourceDepartmentID",
                table: "MaterialReceipts",
                column: "SourceDepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequisitionItems_MaterialRequisitionID",
                table: "MaterialRequisitionItems",
                column: "MaterialRequisitionID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequisitions_DepartmentID",
                table: "MaterialRequisitions",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequisitions_RequisitionDepartmentID",
                table: "MaterialRequisitions",
                column: "RequisitionDepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ResourceID",
                table: "Permissions",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleID",
                table: "Permissions",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BaseUnitID",
                table: "Products",
                column: "BaseUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatagoryID",
                table: "Products",
                column: "CatagoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSkus_ProductID",
                table: "ProductSkus",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_PositionID",
                table: "ProductStocks",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductSkuID",
                table: "ProductStocks",
                column: "ProductSkuID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryID",
                table: "Provinces",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_ProductID",
                table: "PurchaseOrderItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderID",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierID",
                table: "PurchaseOrders",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_ProductSkuID",
                table: "SaleOrderItems",
                column: "ProductSkuID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderItems_SaleOrderID",
                table: "SaleOrderItems",
                column: "SaleOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_CustomerID",
                table: "SaleOrders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_ParentSaleOrderID",
                table: "SaleOrders",
                column: "ParentSaleOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_StockInVoucherItems_StockInVoucherID",
                table: "StockInVoucherItems",
                column: "StockInVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_StockInVouchers_DepartmentID",
                table: "StockInVouchers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_StockInVouchers_DispatchRecordID",
                table: "StockInVouchers",
                column: "DispatchRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_StockInVouchers_SupplierID",
                table: "StockInVouchers",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutVoucherItems_StockOutVoucherID",
                table: "StockOutVoucherItems",
                column: "StockOutVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutVouchers_CustomerID",
                table: "StockOutVouchers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOutVouchers_DepartmentID",
                table: "StockOutVouchers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierClassID",
                table: "Suppliers",
                column: "SupplierClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Address_CountyID",
                table: "Suppliers",
                column: "Address_CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseVoucherItems_PositionID",
                table: "WarehouseVoucherItems",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseVoucherItems_WarehouseVoucherID",
                table: "WarehouseVoucherItems",
                column: "WarehouseVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseVouchers_DepartmentID",
                table: "WarehouseVouchers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseVouchers_SupplierID",
                table: "WarehouseVouchers",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawRecords_LoanVoucherID",
                table: "WithdrawRecords",
                column: "LoanVoucherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commodities");

            migrationBuilder.DropTable(
                name: "DeliveryVoucherItem");

            migrationBuilder.DropTable(
                name: "DistributionVoucherItem");

            migrationBuilder.DropTable(
                name: "MaterialReceiptItems");

            migrationBuilder.DropTable(
                name: "MaterialRequisitionItems");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ProductCatagorySettings");

            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "SaleOrderItems");

            migrationBuilder.DropTable(
                name: "StockInVoucherItems");

            migrationBuilder.DropTable(
                name: "StockOutVoucherItems");

            migrationBuilder.DropTable(
                name: "TradeAccounts");

            migrationBuilder.DropTable(
                name: "WarehouseVoucherItems");

            migrationBuilder.DropTable(
                name: "WithdrawRecords");

            migrationBuilder.DropTable(
                name: "DeliveryVouchers");

            migrationBuilder.DropTable(
                name: "DistributionVoucher");

            migrationBuilder.DropTable(
                name: "MaterialReceipts");

            migrationBuilder.DropTable(
                name: "MaterialRequisitions");

            migrationBuilder.DropTable(
                name: "AlpsResources");

            migrationBuilder.DropTable(
                name: "AlpsRoles");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "ProductSkus");

            migrationBuilder.DropTable(
                name: "StockInVouchers");

            migrationBuilder.DropTable(
                name: "StockOutVouchers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "WarehouseVouchers");

            migrationBuilder.DropTable(
                name: "LoanVouchers");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DropTable(
                name: "AlpsUsers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "DispatchRecords");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Lenders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Catagories");

            migrationBuilder.DropTable(
                name: "SupplierClasses");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
