using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharmacyIMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicineCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LicenseNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RealName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MedicineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GenericName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DosageForm = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrescriptionType = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    StockAlertLevel = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_MedicineCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MedicineCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleReturnOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleReturnOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleReturnOrders_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SaleOrderId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetails_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleReturnOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SaleReturnOrderId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleReturnOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleReturnOrderDetails_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleReturnOrderDetails_SaleReturnOrders_SaleReturnOrderId",
                        column: x => x.SaleReturnOrderId,
                        principalTable: "SaleReturnOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineBatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineBatches_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineBatches_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOrders_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PurchaseReturnOrderId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOrderDetails_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnOrderDetails_PurchaseReturnOrders_PurchaseReturnOrderId",
                        column: x => x.PurchaseReturnOrderId,
                        principalTable: "PurchaseReturnOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MedicineCategories",
                columns: new[] { "Id", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "感冒用药", "治疗感冒、发烧、咳嗽等常见症状" },
                    { 2, "心脑血管", "高血压、心脏病等心脑血管相关药品" },
                    { 3, "消化系统", "胃肠用药、肝病用药等" },
                    { 4, "外用药品", "膏药、软膏、消毒液等外用制剂" },
                    { 5, "中药饮片", "中药材、中药配方颗粒等" },
                    { 6, "维生素类", "维生素、矿物质及营养补充剂" }
                });

            migrationBuilder.InsertData(
                table: "SaleOrders",
                columns: new[] { "Id", "CreateTime", "CustomerName", "CustomerPhone", "OperatorName", "OrderNo", "Remark", "SaleDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "张三", "13900001111", "admin", "XS20260702001", "零售", new DateTime(2026, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 43.00m },
                    { 2, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "李四", "13900002222", "admin", "XS20260706001", "零售", new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 128.00m },
                    { 3, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "王五", "13900003333", "孙销售", "XS20260707001", "零售", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 67.00m },
                    { 4, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "赵六", "13900004444", "孙销售", "XS20260707002", "客户取消订单", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 35.00m }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ContactPerson", "CreateTime", "IsActive", "LicenseNo", "Phone", "SupplierName" },
                values: new object[,]
                {
                    { 1, "北京市东城区xx路1号", "王经理", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "京AA0100001", "13800001111", "国药控股股份有限公司" },
                    { 2, "上海市浦东新区xx路2号", "李经理", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "沪AB0200002", "13800002222", "华润医药商业集团" },
                    { 3, "武汉市汉阳区xx路3号", "张经理", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "鄂AC0300003", "13800003333", "九州通医药集团" },
                    { 4, "广州市荔湾区xx路4号", "陈经理", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "粤AD0400004", "13800004444", "广州医药有限公司" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "IsActive", "PasswordHash", "Phone", "RealName", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "13800000000", "系统管理员", 0, "admin" },
                    { 2, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "13800000001", "王店长", 1, "dianzhang" },
                    { 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "13800000002", "赵采购", 2, "caigou" },
                    { 4, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "13800000003", "孙销售", 3, "xiaoshou" },
                    { 5, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "13800000004", "刘销售", 3, "liuxiaoshou" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "ApprovalNo", "CategoryId", "CreateTime", "DosageForm", "ExpiryDate", "GenericName", "Manufacturer", "MedicineCode", "MedicineName", "PrescriptionType", "PurchasePrice", "Remark", "SalePrice", "Specification", "StockAlertLevel", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "国药准字Z44022100", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "颗粒剂", new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "复方感冒灵颗粒", "华润三九医药", "YP-001", "感冒灵颗粒", 1, 8.50m, null, 15.00m, "10g*9袋", 20, 120 },
                    { 2, "国药准字H10900089", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "胶囊剂", new DateTime(2027, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "布洛芬", "中美史克制药", "YP-002", "布洛芬缓释胶囊", 1, 6.20m, null, 12.50m, "0.3g*24粒", 15, 85 },
                    { 3, "国药准字H13020726", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "胶囊剂", new DateTime(2027, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "阿莫西林", "华北制药集团", "YP-003", "阿莫西林胶囊", 0, 3.80m, null, 8.00m, "0.25g*24粒", 30, 200 },
                    { 4, "国药准字J20180025", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2027, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "硝苯地平", "拜耳医药保健", "YP-004", "硝苯地平控释片", 0, 18.50m, null, 35.00m, "30mg*7片", 10, 60 },
                    { 5, "国药准字J20171021", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2027, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "阿司匹林", "拜耳医药保健", "YP-005", "阿司匹林肠溶片", 0, 5.00m, null, 10.00m, "100mg*30片", 25, 150 },
                    { 6, "国药准字H20030413", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "胶囊剂", new DateTime(2027, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "奥美拉唑", "阿斯利康制药", "YP-006", "奥美拉唑肠溶胶囊", 0, 12.00m, null, 25.00m, "20mg*14粒", 10, 45 },
                    { 7, "国药准字H20000690", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "散剂", new DateTime(2027, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "蒙脱石", "博福-益普生制药", "YP-007", "蒙脱石散", 1, 7.50m, null, 16.00m, "3g*10袋", 15, 95 },
                    { 8, "国药准字Z53021104", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "气雾剂", new DateTime(2027, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "云南白药", "云南白药集团", "YP-008", "云南白药气雾剂", 1, 22.00m, null, 45.00m, "85g+30g", 8, 35 },
                    { 9, "国药准字Z44023754", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "搽剂", new DateTime(2027, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "红花油", "广东泰恩康制药", "YP-009", "红花油", 1, 4.50m, null, 9.80m, "20ml", 12, 70 },
                    { 10, "皖20160052", 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "饮片", new DateTime(2026, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "枸杞子", "亳州中药材市场", "YP-010", "枸杞子", 2, 25.00m, null, 48.00m, "500g/袋", 8, 40 },
                    { 11, "国药准字H31022339", 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "复合维生素B", "上海信谊药厂", "YP-011", "复合维生素B片", 1, 3.20m, null, 7.00m, "100片", 30, 180 },
                    { 12, "国药准字H10950029", 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2027, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "碳酸钙D3", "惠氏制药", "YP-012", "钙尔奇D片", 1, 28.00m, null, 58.00m, "60片", 10, 55 },
                    { 13, "国药准字H20020586", 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "头孢克洛", "礼来制药", "YP-013", "头孢克洛缓释片", 0, 15.00m, null, 32.00m, "0.375g*6片", 10, 30 },
                    { 14, "国药准字H10970210", 1, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "氯雷他定", "上海先灵葆雅", "YP-014", "氯雷他定片", 1, 5.50m, null, 12.00m, "10mg*6片", 10, 18 },
                    { 15, "国药准字H21020021", 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "维生素C", "东北制药", "YP-015", "维生素C片", 1, 1.50m, null, 4.00m, "100片", 20, 8 },
                    { 16, "国药准字H20120091", 4, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "喷雾剂", new DateTime(2028, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "糠酸莫米松鼻喷剂", "默沙东制药", "YP-016", "开瑞坦鼻喷剂", 1, 32.00m, null, 65.00m, "50μg*120喷", 10, 3 },
                    { 17, "国药准字H12020383", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "片剂", new DateTime(2028, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "对乙酰氨基酚", "中美天津史克", "YP-017", "对乙酰氨基酚片", 1, 2.00m, null, 5.00m, "0.5g*12片", 30, 0 },
                    { 18, "国药准字Z41022128", 5, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "丸剂", new DateTime(2027, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "六味地黄丸", "河南宛西制药", "YP-018", "六味地黄丸", 2, 18.00m, null, 38.00m, "360丸/瓶", 15, 5 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "CreateTime", "OperatorName", "OrderNo", "PurchaseDate", "Remark", "Status", "SupplierId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "CG20260701001", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "常规采购", 1, 1, 1850.00m },
                    { 2, new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "CG20260705001", new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "补货采购", 1, 2, 1200.00m }
                });

            migrationBuilder.InsertData(
                table: "SaleReturnOrders",
                columns: new[] { "Id", "CreateTime", "CustomerName", "OperatorName", "OrderNo", "Remark", "ReturnDate", "SaleOrderId", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "张三", "孙销售", "XSTH20260703001", "客户过敏退货", new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 15.00m },
                    { 2, new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "王五", "孙销售", "XSTH20260708001", "买错型号", new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 58.00m }
                });

            migrationBuilder.InsertData(
                table: "MedicineBatches",
                columns: new[] { "Id", "BatchNo", "CreateTime", "ExpiryDate", "MedicineId", "ProductionDate", "PurchaseOrderId", "Quantity", "Remark", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "B20260701001", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100, null, 8.50m },
                    { 2, "B20260701002", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 50, null, 6.20m },
                    { 3, "B20260701003", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 80, null, 3.80m },
                    { 4, "B20260705001", new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 40, null, 18.50m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDetails",
                columns: new[] { "Id", "ExpiryDate", "MedicineId", "PurchaseOrderId", "Quantity", "SubTotal", "UnitPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 100, 850.00m, 8.50m },
                    { 2, new DateTime(2027, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 50, 310.00m, 6.20m },
                    { 3, new DateTime(2027, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 80, 304.00m, 3.80m },
                    { 4, new DateTime(2027, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 60, 300.00m, 5.00m },
                    { 5, new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 1, 100, 320.00m, 3.20m },
                    { 6, new DateTime(2027, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, 40, 740.00m, 18.50m },
                    { 7, new DateTime(2027, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 30, 360.00m, 12.00m },
                    { 8, new DateTime(2027, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 20, 440.00m, 22.00m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseReturnOrders",
                columns: new[] { "Id", "CreateTime", "OperatorName", "OrderNo", "PurchaseOrderId", "Remark", "ReturnDate", "Status", "SupplierId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "赵采购", "CGTH20260703001", 1, "药品破损退货", new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 63.80m },
                    { 2, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "赵采购", "CGTH20260706001", 2, "临近效期退货", new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 370.00m }
                });

            migrationBuilder.InsertData(
                table: "SaleOrderDetails",
                columns: new[] { "Id", "MedicineId", "Quantity", "SaleOrderId", "SubTotal", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 1, 30.00m, 15.00m },
                    { 2, 2, 1, 1, 12.50m, 12.50m },
                    { 3, 4, 2, 2, 70.00m, 35.00m },
                    { 4, 6, 1, 2, 25.00m, 25.00m },
                    { 5, 9, 3, 2, 29.40m, 9.80m },
                    { 6, 12, 1, 3, 58.00m, 58.00m },
                    { 7, 7, 1, 3, 16.00m, 16.00m },
                    { 8, 8, 1, 4, 45.00m, 45.00m }
                });

            migrationBuilder.InsertData(
                table: "SaleReturnOrderDetails",
                columns: new[] { "Id", "MedicineId", "Quantity", "SaleReturnOrderId", "SubTotal", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 15.00m, 15.00m },
                    { 2, 12, 1, 2, 58.00m, 58.00m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseReturnOrderDetails",
                columns: new[] { "Id", "MedicineId", "PurchaseReturnOrderId", "Quantity", "SubTotal", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 3, 1, 5, 19.00m, 3.80m },
                    { 2, 5, 1, 2, 10.00m, 5.00m },
                    { 3, 11, 1, 5, 16.00m, 3.20m },
                    { 4, 2, 1, 3, 18.80m, 6.20m },
                    { 5, 4, 2, 20, 370.00m, 18.50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineBatches_MedicineId",
                table: "MedicineBatches",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineBatches_PurchaseOrderId",
                table: "MedicineBatches",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_CategoryId",
                table: "Medicines",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_MedicineId",
                table: "PurchaseOrderDetails",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrderId",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOrderDetails_MedicineId",
                table: "PurchaseReturnOrderDetails",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOrderDetails_PurchaseReturnOrderId",
                table: "PurchaseReturnOrderDetails",
                column: "PurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOrders_PurchaseOrderId",
                table: "PurchaseReturnOrders",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnOrders_SupplierId",
                table: "PurchaseReturnOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_MedicineId",
                table: "SaleOrderDetails",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_SaleOrderId",
                table: "SaleOrderDetails",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleReturnOrderDetails_MedicineId",
                table: "SaleReturnOrderDetails",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleReturnOrderDetails_SaleReturnOrderId",
                table: "SaleReturnOrderDetails",
                column: "SaleReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleReturnOrders_SaleOrderId",
                table: "SaleReturnOrders",
                column: "SaleOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineBatches");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "PurchaseReturnOrderDetails");

            migrationBuilder.DropTable(
                name: "SaleOrderDetails");

            migrationBuilder.DropTable(
                name: "SaleReturnOrderDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PurchaseReturnOrders");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "SaleReturnOrders");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "MedicineCategories");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
