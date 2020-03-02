using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alps.Web.Service.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounting_TradeAccounts",
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
                    table.PrimaryKey("PK_Accounting_TradeAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Common_Departments",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Common_Units",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Group = table.Column<int>(nullable: false),
                    IsBaseUnit = table.Column<bool>(nullable: false),
                    RateOfExchange = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Units", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Country_Countries",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Loan_Lenders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IDNumber = table.Column<string>(nullable: true),
                    MobilePhoneNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    Invalid = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(nullable: false),
                    InvalidDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_Lenders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Logistics_DispatchRecords",
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
                    GrossWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TareWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossWeightTime = table.Column<DateTimeOffset>(nullable: false),
                    GrossWeightOperator = table.Column<string>(nullable: true),
                    TareWeightOperator = table.Column<string>(nullable: true),
                    TareWeightTime = table.Column<DateTimeOffset>(nullable: false),
                    WeightConfirmedTime = table.Column<DateTimeOffset>(nullable: false),
                    WeightConfirmedOperator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logistics_DispatchRecords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_Catagories",
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
                    table.PrimaryKey("PK_Product_Catagories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_Catagories_Product_Catagories_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Product_Catagories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_Positions",
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
                    table.PrimaryKey("PK_Product_Positions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_ProductCatagorySettings",
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
                    table.PrimaryKey("PK_Product_ProductCatagorySettings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_SupplierClasses",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_SupplierClasses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Security_AlpsResources",
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
                    table.PrimaryKey("PK_Security_AlpsResources", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Security_AlpsRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_AlpsRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Security_AlpsUsers",
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
                    table.PrimaryKey("PK_Security_AlpsUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_MaterialReceipts",
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
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_MaterialReceipts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_MaterialReceipts_Common_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_MaterialReceipts_Common_Departments_SourceDepartmentID",
                        column: x => x.SourceDepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_MaterialRequisitions",
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
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_MaterialRequisitions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_MaterialRequisitions_Common_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_MaterialRequisitions_Common_Departments_RequisitionDepartmentID",
                        column: x => x.RequisitionDepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Province_Provinces",
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
                    table.PrimaryKey("PK_Province_Provinces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Province_Provinces_Country_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country_Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan_LoanVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DepositDate = table.Column<DateTimeOffset>(nullable: false),
                    LenderID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OriginAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestSettlementDate = table.Column<DateTimeOffset>(nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    VoucherNumber = table.Column<string>(nullable: true),
                    HashCode = table.Column<string>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    ModifyDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_LoanVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_LoanVouchers_Loan_Lenders_LenderID",
                        column: x => x.LenderID,
                        principalTable: "Loan_Lenders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Products",
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
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CatagoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_Products_Common_Units_BaseUnitID",
                        column: x => x.BaseUnitID,
                        principalTable: "Common_Units",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Products_Product_Catagories_CatagoryID",
                        column: x => x.CatagoryID,
                        principalTable: "Product_Catagories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security_Permission",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(nullable: false),
                    ResourceID = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_Permission", x => new { x.ResourceID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_Security_Permission_Security_AlpsResources_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "Security_AlpsResources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Security_Permission_Security_AlpsRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Security_AlpsRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Security_AlpsRoleUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    RoleID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_AlpsRoleUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Security_AlpsRoleUser_Security_AlpsRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Security_AlpsRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Security_AlpsRoleUser_Security_AlpsUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "Security_AlpsUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_MaterialReceiptItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MaterialReceiptID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: true),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_MaterialReceiptItems", x => new { x.ID, x.MaterialReceiptID });
                    table.ForeignKey(
                        name: "FK_Product_MaterialReceiptItems_Product_MaterialReceipts_MaterialReceiptID",
                        column: x => x.MaterialReceiptID,
                        principalTable: "Product_MaterialReceipts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_MaterialRequisitionItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MaterialRequisitionID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: true),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_MaterialRequisitionItems", x => new { x.ID, x.MaterialRequisitionID });
                    table.ForeignKey(
                        name: "FK_Product_MaterialRequisitionItems_Product_MaterialRequisitions_MaterialRequisitionID",
                        column: x => x.MaterialRequisitionID,
                        principalTable: "Product_MaterialRequisitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City_Cities",
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
                    table.PrimaryKey("PK_City_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_City_Cities_Province_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Province_Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan_WithdrawRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LoanVoucherID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DepositDate = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    ModifyDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_WithdrawRecords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_WithdrawRecords_Loan_LoanVouchers_LoanVoucherID",
                        column: x => x.LoanVoucherID,
                        principalTable: "Loan_LoanVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_ProductSkus",
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
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreSellQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreSellAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderedAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductSkus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_ProductSkus_Product_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product_Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "County_Counties",
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
                    table.PrimaryKey("PK_County_Counties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_County_Counties_City_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "City_Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_ProductStocks",
                columns: table => new
                {
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    PositionID = table.Column<Guid>(nullable: false),
                    OwnerID = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    AuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductStocks", x => new { x.OwnerID, x.PositionID, x.ProductSkuID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_Product_ProductStocks_Common_Departments_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductStocks_Product_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Product_Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductStocks_Product_ProductSkus_ProductSkuID",
                        column: x => x.ProductSkuID,
                        principalTable: "Product_ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale_Commodities",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreSellQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreSellAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderedAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFutures = table.Column<bool>(nullable: false),
                    DateOfDelivery = table.Column<DateTime>(nullable: true),
                    IsVirtualCommodity = table.Column<bool>(nullable: false),
                    QuantityRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_Commodities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sale_Commodities_Product_ProductSkus_ID",
                        column: x => x.ID,
                        principalTable: "Product_ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Common_Customers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Address_CountyID = table.Column<Guid>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Common_Customers_County_Counties_Address_CountyID",
                        column: x => x.Address_CountyID,
                        principalTable: "County_Counties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Common_Suppliers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address_CountyID = table.Column<Guid>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    SupplierClassID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Suppliers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Common_Suppliers_Purchase_SupplierClasses_SupplierClassID",
                        column: x => x.SupplierClassID,
                        principalTable: "Purchase_SupplierClasses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Common_Suppliers_County_Counties_Address_CountyID",
                        column: x => x.Address_CountyID,
                        principalTable: "County_Counties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_DeliveryVouchers",
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
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_DeliveryVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_DeliveryVouchers_Common_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Common_Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_DeliveryVouchers_Common_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sale_SaleOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CustomerID = table.Column<Guid>(nullable: false),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    ParentSaleOrderID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_SaleOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sale_SaleOrders_Common_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Common_Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_SaleOrders_Sale_SaleOrders_ParentSaleOrderID",
                        column: x => x.ParentSaleOrderID,
                        principalTable: "Sale_SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stock_StockOutVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CustomerID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Confirmer = table.Column<string>(nullable: true),
                    ConfirmTime = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_StockOutVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stock_StockOutVouchers_Common_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Common_Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_StockOutVouchers_Common_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_WarehouseVouchers",
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
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_WarehouseVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Product_WarehouseVouchers_Common_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_WarehouseVouchers_Common_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Common_Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_PurchaseOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    SupplierID = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_PurchaseOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchase_PurchaseOrders_Common_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Common_Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock_StockInVouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SupplierID = table.Column<Guid>(nullable: false),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    TotalAuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Confirmer = table.Column<string>(nullable: true),
                    ConfirmTime = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    DispatchRecordID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_StockInVouchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stock_StockInVouchers_Common_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Common_Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_StockInVouchers_Logistics_DispatchRecords_DispatchRecordID",
                        column: x => x.DispatchRecordID,
                        principalTable: "Logistics_DispatchRecords",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_StockInVouchers_Common_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Common_Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_DeliveryVoucherItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DeliveryVoucherID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    PurchaseOrderItemID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_DeliveryVoucherItem", x => new { x.ID, x.DeliveryVoucherID });
                    table.ForeignKey(
                        name: "FK_Product_DeliveryVoucherItem_Product_DeliveryVouchers_DeliveryVoucherID",
                        column: x => x.DeliveryVoucherID,
                        principalTable: "Product_DeliveryVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_DeliveryVoucherItem_Product_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Product_Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logistics_DistributionVoucher",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SaleOrderID = table.Column<Guid>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logistics_DistributionVoucher", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Logistics_DistributionVoucher_Sale_SaleOrders_SaleOrderID",
                        column: x => x.SaleOrderID,
                        principalTable: "Sale_SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale_SaleOrderItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SaleOrderID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    CommodityName = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_SaleOrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sale_SaleOrderItems_Product_ProductSkus_ProductSkuID",
                        column: x => x.ProductSkuID,
                        principalTable: "Product_ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_SaleOrderItems_Sale_SaleOrders_SaleOrderID",
                        column: x => x.SaleOrderID,
                        principalTable: "Sale_SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock_StockOutVoucherItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StockOutVoucherID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionID = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_StockOutVoucherItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stock_StockOutVoucherItems_Stock_StockOutVouchers_StockOutVoucherID",
                        column: x => x.StockOutVoucherID,
                        principalTable: "Stock_StockOutVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_WarehouseVoucherItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WarehouseVoucherID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: true),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductNumber = table.Column<string>(nullable: true),
                    PositionID = table.Column<Guid>(nullable: false),
                    PurchaseOrderItemID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Freight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_WarehouseVoucherItems", x => new { x.ID, x.WarehouseVoucherID });
                    table.ForeignKey(
                        name: "FK_Product_WarehouseVoucherItems_Product_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Product_Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_WarehouseVoucherItems_Product_WarehouseVouchers_WarehouseVoucherID",
                        column: x => x.WarehouseVoucherID,
                        principalTable: "Product_WarehouseVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_PurchaseOrderItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    PurchaseOrderID = table.Column<Guid>(nullable: false),
                    ProductSkuInfo_SkuID = table.Column<Guid>(nullable: true),
                    ProductSkuInfo_Name = table.Column<string>(nullable: true),
                    ProductSkuInfo_PricingMethod = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_PurchaseOrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchase_PurchaseOrderItems_Product_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product_Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_PurchaseOrderItems_Purchase_PurchaseOrders_PurchaseOrderID",
                        column: x => x.PurchaseOrderID,
                        principalTable: "Purchase_PurchaseOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock_StockInVoucherItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StockInVoucherID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionID = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_StockInVoucherItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stock_StockInVoucherItems_Stock_StockInVouchers_StockInVoucherID",
                        column: x => x.StockInVoucherID,
                        principalTable: "Stock_StockInVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_ProductSkuInfo",
                columns: table => new
                {
                    DeliveryVoucherItemID = table.Column<Guid>(nullable: false),
                    DeliveryVoucherItemDeliveryVoucherID = table.Column<Guid>(nullable: false),
                    SkuID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PricingMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductSkuInfo", x => new { x.DeliveryVoucherItemID, x.DeliveryVoucherItemDeliveryVoucherID });
                    table.ForeignKey(
                        name: "FK_Product_ProductSkuInfo_Product_DeliveryVoucherItem_DeliveryVoucherItemID_DeliveryVoucherItemDeliveryVoucherID",
                        columns: x => new { x.DeliveryVoucherItemID, x.DeliveryVoucherItemDeliveryVoucherID },
                        principalTable: "Product_DeliveryVoucherItem",
                        principalColumns: new[] { "ID", "DeliveryVoucherID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address_Address",
                columns: table => new
                {
                    DistributionVoucherID = table.Column<Guid>(nullable: false),
                    CountyID = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address_Address", x => x.DistributionVoucherID);
                    table.ForeignKey(
                        name: "FK_Address_Address_County_Counties_CountyID",
                        column: x => x.CountyID,
                        principalTable: "County_Counties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Address_Logistics_DistributionVoucher_DistributionVoucherID",
                        column: x => x.DistributionVoucherID,
                        principalTable: "Logistics_DistributionVoucher",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logistics_DistributionVoucherItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SaleOrderItemID = table.Column<Guid>(nullable: false),
                    ProductSkuID = table.Column<Guid>(nullable: false),
                    AuxiliaryQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DistributionVoucherID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logistics_DistributionVoucherItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Logistics_DistributionVoucherItem_Logistics_DistributionVoucher_DistributionVoucherID",
                        column: x => x.DistributionVoucherID,
                        principalTable: "Logistics_DistributionVoucher",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logistics_DistributionVoucherItem_Product_ProductSkus_ProductSkuID",
                        column: x => x.ProductSkuID,
                        principalTable: "Product_ProductSkus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_Address_CountyID",
                table: "Address_Address",
                column: "CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_City_Cities_ProvinceID",
                table: "City_Cities",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Customers_Address_CountyID",
                table: "Common_Customers",
                column: "Address_CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Suppliers_SupplierClassID",
                table: "Common_Suppliers",
                column: "SupplierClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Suppliers_Address_CountyID",
                table: "Common_Suppliers",
                column: "Address_CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_County_Counties_CityID",
                table: "County_Counties",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanVouchers_LenderID",
                table: "Loan_LoanVouchers",
                column: "LenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_WithdrawRecords_LoanVoucherID",
                table: "Loan_WithdrawRecords",
                column: "LoanVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_DistributionVoucher_SaleOrderID",
                table: "Logistics_DistributionVoucher",
                column: "SaleOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_DistributionVoucherItem_DistributionVoucherID",
                table: "Logistics_DistributionVoucherItem",
                column: "DistributionVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Logistics_DistributionVoucherItem_ProductSkuID",
                table: "Logistics_DistributionVoucherItem",
                column: "ProductSkuID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Catagories_ParentID",
                table: "Product_Catagories",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeliveryVoucherItem_DeliveryVoucherID",
                table: "Product_DeliveryVoucherItem",
                column: "DeliveryVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeliveryVoucherItem_PositionID",
                table: "Product_DeliveryVoucherItem",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeliveryVouchers_CustomerID",
                table: "Product_DeliveryVouchers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeliveryVouchers_DepartmentID",
                table: "Product_DeliveryVouchers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MaterialReceiptItems_MaterialReceiptID",
                table: "Product_MaterialReceiptItems",
                column: "MaterialReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MaterialReceipts_DepartmentID",
                table: "Product_MaterialReceipts",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MaterialReceipts_SourceDepartmentID",
                table: "Product_MaterialReceipts",
                column: "SourceDepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MaterialRequisitionItems_MaterialRequisitionID",
                table: "Product_MaterialRequisitionItems",
                column: "MaterialRequisitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MaterialRequisitions_DepartmentID",
                table: "Product_MaterialRequisitions",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MaterialRequisitions_RequisitionDepartmentID",
                table: "Product_MaterialRequisitions",
                column: "RequisitionDepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Products_BaseUnitID",
                table: "Product_Products",
                column: "BaseUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Products_CatagoryID",
                table: "Product_Products",
                column: "CatagoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSkus_ProductID",
                table: "Product_ProductSkus",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductStocks_PositionID",
                table: "Product_ProductStocks",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductStocks_ProductSkuID",
                table: "Product_ProductStocks",
                column: "ProductSkuID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseVoucherItems_PositionID",
                table: "Product_WarehouseVoucherItems",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseVoucherItems_WarehouseVoucherID",
                table: "Product_WarehouseVoucherItems",
                column: "WarehouseVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseVouchers_DepartmentID",
                table: "Product_WarehouseVouchers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseVouchers_SupplierID",
                table: "Product_WarehouseVouchers",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Province_Provinces_CountryID",
                table: "Province_Provinces",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PurchaseOrderItems_ProductID",
                table: "Purchase_PurchaseOrderItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PurchaseOrderItems_PurchaseOrderID",
                table: "Purchase_PurchaseOrderItems",
                column: "PurchaseOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PurchaseOrders_SupplierID",
                table: "Purchase_PurchaseOrders",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_SaleOrderItems_ProductSkuID",
                table: "Sale_SaleOrderItems",
                column: "ProductSkuID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_SaleOrderItems_SaleOrderID",
                table: "Sale_SaleOrderItems",
                column: "SaleOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_SaleOrders_CustomerID",
                table: "Sale_SaleOrders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_SaleOrders_ParentSaleOrderID",
                table: "Sale_SaleOrders",
                column: "ParentSaleOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Security_AlpsRoleUser_RoleID",
                table: "Security_AlpsRoleUser",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Security_AlpsRoleUser_UserID",
                table: "Security_AlpsRoleUser",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Security_Permission_RoleID",
                table: "Security_Permission",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockInVoucherItems_StockInVoucherID",
                table: "Stock_StockInVoucherItems",
                column: "StockInVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockInVouchers_DepartmentID",
                table: "Stock_StockInVouchers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockInVouchers_DispatchRecordID",
                table: "Stock_StockInVouchers",
                column: "DispatchRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockInVouchers_SupplierID",
                table: "Stock_StockInVouchers",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockOutVoucherItems_StockOutVoucherID",
                table: "Stock_StockOutVoucherItems",
                column: "StockOutVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockOutVouchers_CustomerID",
                table: "Stock_StockOutVouchers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockOutVouchers_DepartmentID",
                table: "Stock_StockOutVouchers",
                column: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounting_TradeAccounts");

            migrationBuilder.DropTable(
                name: "Address_Address");

            migrationBuilder.DropTable(
                name: "Loan_WithdrawRecords");

            migrationBuilder.DropTable(
                name: "Logistics_DistributionVoucherItem");

            migrationBuilder.DropTable(
                name: "Product_MaterialReceiptItems");

            migrationBuilder.DropTable(
                name: "Product_MaterialRequisitionItems");

            migrationBuilder.DropTable(
                name: "Product_ProductCatagorySettings");

            migrationBuilder.DropTable(
                name: "Product_ProductSkuInfo");

            migrationBuilder.DropTable(
                name: "Product_ProductStocks");

            migrationBuilder.DropTable(
                name: "Product_WarehouseVoucherItems");

            migrationBuilder.DropTable(
                name: "Purchase_PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "Sale_Commodities");

            migrationBuilder.DropTable(
                name: "Sale_SaleOrderItems");

            migrationBuilder.DropTable(
                name: "Security_AlpsRoleUser");

            migrationBuilder.DropTable(
                name: "Security_Permission");

            migrationBuilder.DropTable(
                name: "Stock_StockInVoucherItems");

            migrationBuilder.DropTable(
                name: "Stock_StockOutVoucherItems");

            migrationBuilder.DropTable(
                name: "Loan_LoanVouchers");

            migrationBuilder.DropTable(
                name: "Logistics_DistributionVoucher");

            migrationBuilder.DropTable(
                name: "Product_MaterialReceipts");

            migrationBuilder.DropTable(
                name: "Product_MaterialRequisitions");

            migrationBuilder.DropTable(
                name: "Product_DeliveryVoucherItem");

            migrationBuilder.DropTable(
                name: "Product_WarehouseVouchers");

            migrationBuilder.DropTable(
                name: "Purchase_PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Product_ProductSkus");

            migrationBuilder.DropTable(
                name: "Security_AlpsUsers");

            migrationBuilder.DropTable(
                name: "Security_AlpsResources");

            migrationBuilder.DropTable(
                name: "Security_AlpsRoles");

            migrationBuilder.DropTable(
                name: "Stock_StockInVouchers");

            migrationBuilder.DropTable(
                name: "Stock_StockOutVouchers");

            migrationBuilder.DropTable(
                name: "Loan_Lenders");

            migrationBuilder.DropTable(
                name: "Sale_SaleOrders");

            migrationBuilder.DropTable(
                name: "Product_DeliveryVouchers");

            migrationBuilder.DropTable(
                name: "Product_Positions");

            migrationBuilder.DropTable(
                name: "Product_Products");

            migrationBuilder.DropTable(
                name: "Logistics_DispatchRecords");

            migrationBuilder.DropTable(
                name: "Common_Suppliers");

            migrationBuilder.DropTable(
                name: "Common_Customers");

            migrationBuilder.DropTable(
                name: "Common_Departments");

            migrationBuilder.DropTable(
                name: "Common_Units");

            migrationBuilder.DropTable(
                name: "Product_Catagories");

            migrationBuilder.DropTable(
                name: "Purchase_SupplierClasses");

            migrationBuilder.DropTable(
                name: "County_Counties");

            migrationBuilder.DropTable(
                name: "City_Cities");

            migrationBuilder.DropTable(
                name: "Province_Provinces");

            migrationBuilder.DropTable(
                name: "Country_Countries");
        }
    }
}
