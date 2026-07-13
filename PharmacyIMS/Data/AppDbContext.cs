using Microsoft.EntityFrameworkCore;
using PharmacyIMS.Enums;
using PharmacyIMS.Models;

namespace PharmacyIMS.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MedicineCategory> MedicineCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }
        public DbSet<PurchaseReturnOrder> PurchaseReturnOrders { get; set; }
        public DbSet<PurchaseReturnOrderDetail> PurchaseReturnOrderDetails { get; set; }
        public DbSet<SaleReturnOrder> SaleReturnOrders { get; set; }
        public DbSet<SaleReturnOrderDetail> SaleReturnOrderDetails { get; set; }
        public DbSet<MedicineBatch> MedicineBatches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PharmacyIMS;" + "Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ──── 药品分类种子数据 ────
            modelBuilder.Entity<MedicineCategory>().HasData(
                new MedicineCategory { Id = 1, CategoryName = "感冒用药", Description = "治疗感冒、发烧、咳嗽等常见症状" },
                new MedicineCategory { Id = 2, CategoryName = "心脑血管", Description = "高血压、心脏病等心脑血管相关药品" },
                new MedicineCategory { Id = 3, CategoryName = "消化系统", Description = "胃肠用药、肝病用药等" },
                new MedicineCategory { Id = 4, CategoryName = "外用药品", Description = "膏药、软膏、消毒液等外用制剂" },
                new MedicineCategory { Id = 5, CategoryName = "中药饮片", Description = "中药材、中药配方颗粒等" },
                new MedicineCategory { Id = 6, CategoryName = "维生素类", Description = "维生素、矿物质及营养补充剂" }
            );

            // ──── 供应商种子数据 ────
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, SupplierName = "国药控股股份有限公司", ContactPerson = "王经理", Phone = "13800001111", Address = "北京市东城区xx路1号", LicenseNo = "京AA0100001", IsActive = true, CreateTime = new DateTime(2024, 1, 1) },
                new Supplier { Id = 2, SupplierName = "华润医药商业集团", ContactPerson = "李经理", Phone = "13800002222", Address = "上海市浦东新区xx路2号", LicenseNo = "沪AB0200002", IsActive = true, CreateTime = new DateTime(2024, 1, 1) },
                new Supplier { Id = 3, SupplierName = "九州通医药集团", ContactPerson = "张经理", Phone = "13800003333", Address = "武汉市汉阳区xx路3号", LicenseNo = "鄂AC0300003", IsActive = true, CreateTime = new DateTime(2024, 1, 1) },
                new Supplier { Id = 4, SupplierName = "广州医药有限公司", ContactPerson = "陈经理", Phone = "13800004444", Address = "广州市荔湾区xx路4号", LicenseNo = "粤AD0400004", IsActive = true, CreateTime = new DateTime(2024, 1, 1) }
            );

            // ──── 药品种子数据（12种药品） ────
            modelBuilder.Entity<Medicine>().HasData(
                new Medicine
                {
                    Id = 1,
                    MedicineCode = "YP-001",
                    MedicineName = "感冒灵颗粒",
                    GenericName = "复方感冒灵颗粒",
                    Specification = "10g*9袋",
                    DosageForm = "颗粒剂",
                    Manufacturer = "华润三九医药",
                    ApprovalNo = "国药准字Z44022100",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 8.50m,
                    SalePrice = 15.00m,
                    StockQuantity = 120,
                    StockAlertLevel = 20,
                    ExpiryDate = new DateTime(2027, 6, 1),
                    CategoryId = 1,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 2,
                    MedicineCode = "YP-002",
                    MedicineName = "布洛芬缓释胶囊",
                    GenericName = "布洛芬",
                    Specification = "0.3g*24粒",
                    DosageForm = "胶囊剂",
                    Manufacturer = "中美史克制药",
                    ApprovalNo = "国药准字H10900089",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 6.20m,
                    SalePrice = 12.50m,
                    StockQuantity = 85,
                    StockAlertLevel = 15,
                    ExpiryDate = new DateTime(2027, 8, 15),
                    CategoryId = 1,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 3,
                    MedicineCode = "YP-003",
                    MedicineName = "阿莫西林胶囊",
                    GenericName = "阿莫西林",
                    Specification = "0.25g*24粒",
                    DosageForm = "胶囊剂",
                    Manufacturer = "华北制药集团",
                    ApprovalNo = "国药准字H13020726",
                    PrescriptionType = PrescriptionType.Prescription,
                    PurchasePrice = 3.80m,
                    SalePrice = 8.00m,
                    StockQuantity = 200,
                    StockAlertLevel = 30,
                    ExpiryDate = new DateTime(2027, 3, 20),
                    CategoryId = 1,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 4,
                    MedicineCode = "YP-004",
                    MedicineName = "硝苯地平控释片",
                    GenericName = "硝苯地平",
                    Specification = "30mg*7片",
                    DosageForm = "片剂",
                    Manufacturer = "拜耳医药保健",
                    ApprovalNo = "国药准字J20180025",
                    PrescriptionType = PrescriptionType.Prescription,
                    PurchasePrice = 18.50m,
                    SalePrice = 35.00m,
                    StockQuantity = 60,
                    StockAlertLevel = 10,
                    ExpiryDate = new DateTime(2027, 5, 10),
                    CategoryId = 2,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 5,
                    MedicineCode = "YP-005",
                    MedicineName = "阿司匹林肠溶片",
                    GenericName = "阿司匹林",
                    Specification = "100mg*30片",
                    DosageForm = "片剂",
                    Manufacturer = "拜耳医药保健",
                    ApprovalNo = "国药准字J20171021",
                    PrescriptionType = PrescriptionType.Prescription,
                    PurchasePrice = 5.00m,
                    SalePrice = 10.00m,
                    StockQuantity = 150,
                    StockAlertLevel = 25,
                    ExpiryDate = new DateTime(2027, 9, 1),
                    CategoryId = 2,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 6,
                    MedicineCode = "YP-006",
                    MedicineName = "奥美拉唑肠溶胶囊",
                    GenericName = "奥美拉唑",
                    Specification = "20mg*14粒",
                    DosageForm = "胶囊剂",
                    Manufacturer = "阿斯利康制药",
                    ApprovalNo = "国药准字H20030413",
                    PrescriptionType = PrescriptionType.Prescription,
                    PurchasePrice = 12.00m,
                    SalePrice = 25.00m,
                    StockQuantity = 45,
                    StockAlertLevel = 10,
                    ExpiryDate = new DateTime(2027, 4, 18),
                    CategoryId = 3,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 7,
                    MedicineCode = "YP-007",
                    MedicineName = "蒙脱石散",
                    GenericName = "蒙脱石",
                    Specification = "3g*10袋",
                    DosageForm = "散剂",
                    Manufacturer = "博福-益普生制药",
                    ApprovalNo = "国药准字H20000690",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 7.50m,
                    SalePrice = 16.00m,
                    StockQuantity = 95,
                    StockAlertLevel = 15,
                    ExpiryDate = new DateTime(2027, 7, 22),
                    CategoryId = 3,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 8,
                    MedicineCode = "YP-008",
                    MedicineName = "云南白药气雾剂",
                    GenericName = "云南白药",
                    Specification = "85g+30g",
                    DosageForm = "气雾剂",
                    Manufacturer = "云南白药集团",
                    ApprovalNo = "国药准字Z53021104",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 22.00m,
                    SalePrice = 45.00m,
                    StockQuantity = 35,
                    StockAlertLevel = 8,
                    ExpiryDate = new DateTime(2027, 10, 5),
                    CategoryId = 4,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 9,
                    MedicineCode = "YP-009",
                    MedicineName = "红花油",
                    GenericName = "红花油",
                    Specification = "20ml",
                    DosageForm = "搽剂",
                    Manufacturer = "广东泰恩康制药",
                    ApprovalNo = "国药准字Z44023754",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 4.50m,
                    SalePrice = 9.80m,
                    StockQuantity = 70,
                    StockAlertLevel = 12,
                    ExpiryDate = new DateTime(2027, 11, 12),
                    CategoryId = 4,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 10,
                    MedicineCode = "YP-010",
                    MedicineName = "枸杞子",
                    GenericName = "枸杞子",
                    Specification = "500g/袋",
                    DosageForm = "饮片",
                    Manufacturer = "亳州中药材市场",
                    ApprovalNo = "皖20160052",
                    PrescriptionType = PrescriptionType.TraditionalChinese,
                    PurchasePrice = 25.00m,
                    SalePrice = 48.00m,
                    StockQuantity = 40,
                    StockAlertLevel = 8,
                    ExpiryDate = new DateTime(2026, 12, 30),
                    CategoryId = 5,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 11,
                    MedicineCode = "YP-011",
                    MedicineName = "复合维生素B片",
                    GenericName = "复合维生素B",
                    Specification = "100片",
                    DosageForm = "片剂",
                    Manufacturer = "上海信谊药厂",
                    ApprovalNo = "国药准字H31022339",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 3.20m,
                    SalePrice = 7.00m,
                    StockQuantity = 180,
                    StockAlertLevel = 30,
                    ExpiryDate = new DateTime(2027, 2, 28),
                    CategoryId = 6,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 12,
                    MedicineCode = "YP-012",
                    MedicineName = "钙尔奇D片",
                    GenericName = "碳酸钙D3",
                    Specification = "60片",
                    DosageForm = "片剂",
                    Manufacturer = "惠氏制药",
                    ApprovalNo = "国药准字H10950029",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 28.00m,
                    SalePrice = 58.00m,
                    StockQuantity = 55,
                    StockAlertLevel = 10,
                    ExpiryDate = new DateTime(2027, 1, 15),
                    CategoryId = 6,
                    CreateTime = new DateTime(2024, 1, 1)
                },

                // ──── 临期药品（有效期 <= 2026-10-08） ────
                new Medicine
                {
                    Id = 13,
                    MedicineCode = "YP-013",
                    MedicineName = "头孢克洛缓释片",
                    GenericName = "头孢克洛",
                    Specification = "0.375g*6片",
                    DosageForm = "片剂",
                    Manufacturer = "礼来制药",
                    ApprovalNo = "国药准字H20020586",
                    PrescriptionType = PrescriptionType.Prescription,
                    PurchasePrice = 15.00m,
                    SalePrice = 32.00m,
                    StockQuantity = 30,
                    StockAlertLevel = 10,
                    ExpiryDate = new DateTime(2026, 7, 25),
                    CategoryId = 1,
                    CreateTime = new DateTime(2024, 6, 1)
                },
                new Medicine
                {
                    Id = 14,
                    MedicineName = "氯雷他定片",
                    MedicineCode = "YP-014",
                    GenericName = "氯雷他定",
                    Specification = "10mg*6片",
                    DosageForm = "片剂",
                    Manufacturer = "上海先灵葆雅",
                    ApprovalNo = "国药准字H10970210",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 5.50m,
                    SalePrice = 12.00m,
                    StockQuantity = 18,
                    StockAlertLevel = 10,
                    ExpiryDate = new DateTime(2026, 8, 30),
                    CategoryId = 1,
                    CreateTime = new DateTime(2024, 5, 1)
                },
                new Medicine
                {
                    Id = 15,
                    MedicineName = "维生素C片",
                    MedicineCode = "YP-015",
                    GenericName = "维生素C",
                    Specification = "100片",
                    DosageForm = "片剂",
                    Manufacturer = "东北制药",
                    ApprovalNo = "国药准字H21020021",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 1.50m,
                    SalePrice = 4.00m,
                    StockQuantity = 8,
                    StockAlertLevel = 20,
                    ExpiryDate = new DateTime(2026, 9, 15),
                    CategoryId = 6,
                    CreateTime = new DateTime(2024, 1, 1)
                },

                // ──── 库存预警药品（库存 <= 预警下限） ────
                new Medicine
                {
                    Id = 16,
                    MedicineName = "开瑞坦鼻喷剂",
                    MedicineCode = "YP-016",
                    GenericName = "糠酸莫米松鼻喷剂",
                    Specification = "50μg*120喷",
                    DosageForm = "喷雾剂",
                    Manufacturer = "默沙东制药",
                    ApprovalNo = "国药准字H20120091",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 32.00m,
                    SalePrice = 65.00m,
                    StockQuantity = 3,
                    StockAlertLevel = 10,
                    ExpiryDate = new DateTime(2028, 3, 1),
                    CategoryId = 4,
                    CreateTime = new DateTime(2024, 6, 1)
                },
                new Medicine
                {
                    Id = 17,
                    MedicineName = "对乙酰氨基酚片",
                    MedicineCode = "YP-017",
                    GenericName = "对乙酰氨基酚",
                    Specification = "0.5g*12片",
                    DosageForm = "片剂",
                    Manufacturer = "中美天津史克",
                    ApprovalNo = "国药准字H12020383",
                    PrescriptionType = PrescriptionType.OTC,
                    PurchasePrice = 2.00m,
                    SalePrice = 5.00m,
                    StockQuantity = 0,
                    StockAlertLevel = 30,
                    ExpiryDate = new DateTime(2028, 6, 15),
                    CategoryId = 1,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new Medicine
                {
                    Id = 18,
                    MedicineName = "六味地黄丸",
                    MedicineCode = "YP-018",
                    GenericName = "六味地黄丸",
                    Specification = "360丸/瓶",
                    DosageForm = "丸剂",
                    Manufacturer = "河南宛西制药",
                    ApprovalNo = "国药准字Z41022128",
                    PrescriptionType = PrescriptionType.TraditionalChinese,
                    PurchasePrice = 18.00m,
                    SalePrice = 38.00m,
                    StockQuantity = 5,
                    StockAlertLevel = 15,
                    ExpiryDate = new DateTime(2027, 12, 1),
                    CategoryId = 5,
                    CreateTime = new DateTime(2024, 4, 1)
                }
            );

            // ──── 管理员账号种子数据 ────
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                    RealName = "系统管理员",
                    Role = UserRole.Admin,
                    Phone = "13800000000",
                    IsActive = true,
                    CreateTime = new DateTime(2024, 1, 1)
                },
                new User
                {
                    Id = 2,
                    Username = "dianzhang",
                    PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                    RealName = "王店长",
                    Role = UserRole.Manager,
                    Phone = "13800000001",
                    IsActive = true,
                    CreateTime = new DateTime(2024, 3, 1)
                },
                new User
                {
                    Id = 3,
                    Username = "caigou",
                    PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                    RealName = "赵采购",
                    Role = UserRole.Purchaser,
                    Phone = "13800000002",
                    IsActive = true,
                    CreateTime = new DateTime(2024, 3, 1)
                },
                new User
                {
                    Id = 4,
                    Username = "xiaoshou",
                    PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                    RealName = "孙销售",
                    Role = UserRole.Salesperson,
                    Phone = "13800000003",
                    IsActive = true,
                    CreateTime = new DateTime(2024, 3, 1)
                },
                new User
                {
                    Id = 5,
                    Username = "liuxiaoshou",
                    PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                    RealName = "刘销售",
                    Role = UserRole.Salesperson,
                    Phone = "13800000004",
                    IsActive = false,
                    CreateTime = new DateTime(2024, 3, 1)
                }
            );

            // ──── 采购入库单种子数据 ────
            modelBuilder.Entity<PurchaseOrder>().HasData(
                new PurchaseOrder
                {
                    Id = 1,
                    OrderNo = "CG20260701001",
                    PurchaseDate = new DateTime(2026, 7, 1),
                    TotalAmount = 1850.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "admin",
                    SupplierId = 1,
                    Remark = "常规采购",
                    CreateTime = new DateTime(2026, 7, 1)
                },
                new PurchaseOrder
                {
                    Id = 2,
                    OrderNo = "CG20260705001",
                    PurchaseDate = new DateTime(2026, 7, 5),
                    TotalAmount = 1200.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "admin",
                    SupplierId = 2,
                    Remark = "补货采购",
                    CreateTime = new DateTime(2026, 7, 5)
                }
            );

            // ──── 采购明细种子数据 ────
            modelBuilder.Entity<PurchaseOrderDetail>().HasData(
                new PurchaseOrderDetail { Id = 1, PurchaseOrderId = 1, MedicineId = 1, Quantity = 100, UnitPrice = 8.50m, SubTotal = 850.00m, ExpiryDate = new DateTime(2027, 6, 1) },
                new PurchaseOrderDetail { Id = 2, PurchaseOrderId = 1, MedicineId = 2, Quantity = 50, UnitPrice = 6.20m, SubTotal = 310.00m, ExpiryDate = new DateTime(2027, 8, 15) },
                new PurchaseOrderDetail { Id = 3, PurchaseOrderId = 1, MedicineId = 3, Quantity = 80, UnitPrice = 3.80m, SubTotal = 304.00m, ExpiryDate = new DateTime(2027, 3, 20) },
                new PurchaseOrderDetail { Id = 4, PurchaseOrderId = 1, MedicineId = 5, Quantity = 60, UnitPrice = 5.00m, SubTotal = 300.00m, ExpiryDate = new DateTime(2027, 9, 1) },
                new PurchaseOrderDetail { Id = 5, PurchaseOrderId = 1, MedicineId = 11, Quantity = 100, UnitPrice = 3.20m, SubTotal = 320.00m, ExpiryDate = new DateTime(2027, 2, 28) },
                new PurchaseOrderDetail { Id = 6, PurchaseOrderId = 2, MedicineId = 4, Quantity = 40, UnitPrice = 18.50m, SubTotal = 740.00m, ExpiryDate = new DateTime(2027, 5, 10) },
                new PurchaseOrderDetail { Id = 7, PurchaseOrderId = 2, MedicineId = 6, Quantity = 30, UnitPrice = 12.00m, SubTotal = 360.00m, ExpiryDate = new DateTime(2027, 4, 18) },
                new PurchaseOrderDetail { Id = 8, PurchaseOrderId = 2, MedicineId = 8, Quantity = 20, UnitPrice = 22.00m, SubTotal = 440.00m, ExpiryDate = new DateTime(2027, 10, 5) }
            );

            // ──── 销售出库单种子数据 ────
            modelBuilder.Entity<SaleOrder>().HasData(
                new SaleOrder
                {
                    Id = 1,
                    OrderNo = "XS20260702001",
                    SaleDate = new DateTime(2026, 7, 2),
                    CustomerName = "张三",
                    CustomerPhone = "13900001111",
                    TotalAmount = 43.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "admin",
                    Remark = "零售",
                    CreateTime = new DateTime(2026, 7, 2)
                },
                new SaleOrder
                {
                    Id = 2,
                    OrderNo = "XS20260706001",
                    SaleDate = new DateTime(2026, 7, 6),
                    CustomerName = "李四",
                    CustomerPhone = "13900002222",
                    TotalAmount = 128.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "admin",
                    Remark = "零售",
                    CreateTime = new DateTime(2026, 7, 6)
                },
                new SaleOrder
                {
                    Id = 3,
                    OrderNo = "XS20260707001",
                    SaleDate = new DateTime(2026, 7, 7),
                    CustomerName = "王五",
                    CustomerPhone = "13900003333",
                    TotalAmount = 67.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "孙销售",
                    Remark = "零售",
                    CreateTime = new DateTime(2026, 7, 7)
                },
                new SaleOrder
                {
                    Id = 4,
                    OrderNo = "XS20260707002",
                    SaleDate = new DateTime(2026, 7, 7),
                    CustomerName = "赵六",
                    CustomerPhone = "13900004444",
                    TotalAmount = 35.00m,
                    Status = OrderStatus.Cancelled,
                    OperatorName = "孙销售",
                    Remark = "客户取消订单",
                    CreateTime = new DateTime(2026, 7, 7)
                }
            );

            // ──── 销售明细种子数据 ────
            modelBuilder.Entity<SaleOrderDetail>().HasData(
                new SaleOrderDetail { Id = 1, SaleOrderId = 1, MedicineId = 1, Quantity = 2, UnitPrice = 15.00m, SubTotal = 30.00m },
                new SaleOrderDetail { Id = 2, SaleOrderId = 1, MedicineId = 2, Quantity = 1, UnitPrice = 12.50m, SubTotal = 12.50m },
                new SaleOrderDetail { Id = 3, SaleOrderId = 2, MedicineId = 4, Quantity = 2, UnitPrice = 35.00m, SubTotal = 70.00m },
                new SaleOrderDetail { Id = 4, SaleOrderId = 2, MedicineId = 6, Quantity = 1, UnitPrice = 25.00m, SubTotal = 25.00m },
                new SaleOrderDetail { Id = 5, SaleOrderId = 2, MedicineId = 9, Quantity = 3, UnitPrice = 9.80m, SubTotal = 29.40m },
                new SaleOrderDetail { Id = 6, SaleOrderId = 3, MedicineId = 12, Quantity = 1, UnitPrice = 58.00m, SubTotal = 58.00m },
                new SaleOrderDetail { Id = 7, SaleOrderId = 3, MedicineId = 7, Quantity = 1, UnitPrice = 16.00m, SubTotal = 16.00m },
                new SaleOrderDetail { Id = 8, SaleOrderId = 4, MedicineId = 8, Quantity = 1, UnitPrice = 45.00m, SubTotal = 45.00m }
            );

            // ──── 药品批次种子数据 ────
            modelBuilder.Entity<MedicineBatch>().HasData(
                new MedicineBatch { Id = 1, BatchNo = "B20260701001", MedicineId = 1, Quantity = 100, UnitPrice = 8.50m, ExpiryDate = new DateTime(2027, 6, 1), ProductionDate = new DateTime(2024, 6, 1), PurchaseOrderId = 1, CreateTime = new DateTime(2026, 7, 1) },
                new MedicineBatch { Id = 2, BatchNo = "B20260701002", MedicineId = 2, Quantity = 50, UnitPrice = 6.20m, ExpiryDate = new DateTime(2027, 8, 15), ProductionDate = new DateTime(2024, 8, 15), PurchaseOrderId = 1, CreateTime = new DateTime(2026, 7, 1) },
                new MedicineBatch { Id = 3, BatchNo = "B20260701003", MedicineId = 3, Quantity = 80, UnitPrice = 3.80m, ExpiryDate = new DateTime(2027, 3, 20), ProductionDate = new DateTime(2024, 3, 20), PurchaseOrderId = 1, CreateTime = new DateTime(2026, 7, 1) },
                new MedicineBatch { Id = 4, BatchNo = "B20260705001", MedicineId = 4, Quantity = 40, UnitPrice = 18.50m, ExpiryDate = new DateTime(2027, 5, 10), ProductionDate = new DateTime(2024, 5, 10), PurchaseOrderId = 2, CreateTime = new DateTime(2026, 7, 5) }
            );

            // ──── 采购退货单种子数据 ────
            modelBuilder.Entity<PurchaseReturnOrder>().HasData(
                new PurchaseReturnOrder
                {
                    Id = 1,
                    OrderNo = "CGTH20260703001",
                    ReturnDate = new DateTime(2026, 7, 3),
                    TotalAmount = 63.80m,
                    Status = OrderStatus.Completed,
                    OperatorName = "赵采购",
                    SupplierId = 1,
                    PurchaseOrderId = 1,
                    Remark = "药品破损退货",
                    CreateTime = new DateTime(2026, 7, 3)
                },
                new PurchaseReturnOrder
                {
                    Id = 2,
                    OrderNo = "CGTH20260706001",
                    ReturnDate = new DateTime(2026, 7, 6),
                    TotalAmount = 370.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "赵采购",
                    SupplierId = 2,
                    PurchaseOrderId = 2,
                    Remark = "临近效期退货",
                    CreateTime = new DateTime(2026, 7, 6)
                }
            );

            // ──── 采购退货明细种子数据 ────
            modelBuilder.Entity<PurchaseReturnOrderDetail>().HasData(
                new PurchaseReturnOrderDetail { Id = 1, PurchaseReturnOrderId = 1, MedicineId = 3, Quantity = 5, UnitPrice = 3.80m, SubTotal = 19.00m },
                new PurchaseReturnOrderDetail { Id = 2, PurchaseReturnOrderId = 1, MedicineId = 5, Quantity = 2, UnitPrice = 5.00m, SubTotal = 10.00m },
                new PurchaseReturnOrderDetail { Id = 3, PurchaseReturnOrderId = 1, MedicineId = 11, Quantity = 5, UnitPrice = 3.20m, SubTotal = 16.00m },
                new PurchaseReturnOrderDetail { Id = 4, PurchaseReturnOrderId = 1, MedicineId = 2, Quantity = 3, UnitPrice = 6.20m, SubTotal = 18.80m },
                new PurchaseReturnOrderDetail { Id = 5, PurchaseReturnOrderId = 2, MedicineId = 4, Quantity = 20, UnitPrice = 18.50m, SubTotal = 370.00m }
            );

            // ──── 销售退货单种子数据 ────
            modelBuilder.Entity<SaleReturnOrder>().HasData(
                new SaleReturnOrder
                {
                    Id = 1,
                    OrderNo = "XSTH20260703001",
                    ReturnDate = new DateTime(2026, 7, 3),
                    CustomerName = "张三",
                    TotalAmount = 15.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "孙销售",
                    SaleOrderId = 1,
                    Remark = "客户过敏退货",
                    CreateTime = new DateTime(2026, 7, 3)
                },
                new SaleReturnOrder
                {
                    Id = 2,
                    OrderNo = "XSTH20260708001",
                    ReturnDate = new DateTime(2026, 7, 8),
                    CustomerName = "王五",
                    TotalAmount = 58.00m,
                    Status = OrderStatus.Completed,
                    OperatorName = "孙销售",
                    SaleOrderId = 3,
                    Remark = "买错型号",
                    CreateTime = new DateTime(2026, 7, 8)
                }
            );

            // ──── 销售退货明细种子数据 ────
            modelBuilder.Entity<SaleReturnOrderDetail>().HasData(
                new SaleReturnOrderDetail { Id = 1, SaleReturnOrderId = 1, MedicineId = 1, Quantity = 1, UnitPrice = 15.00m, SubTotal = 15.00m },
                new SaleReturnOrderDetail { Id = 2, SaleReturnOrderId = 2, MedicineId = 12, Quantity = 1, UnitPrice = 58.00m, SubTotal = 58.00m }
            );
        }
    }
}
