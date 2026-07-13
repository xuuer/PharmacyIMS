# PharmacyIMS - 药店信息管理系统

> 基于 WPF + Entity Framework Core 的桌面端药店进销存管理应用。

## 技术栈

| 技术 | 版本 |
|------|------|
| .NET | 8.0 |
| WPF | .NET 8.0-windows |
| Entity Framework Core | 8.0.27 |
| SQL Server | 2019+ / LocalDB |

## 功能模块

- **用户管理**：支持管理员、店长、采购员、销售员多角色权限控制
- **药品管理**：药品信息维护、分类管理、批号追踪、效期管理
- **供应商管理**：供应商信息、资质证照、合作状态
- **采购管理**：采购入库单、采购退货单、批次入库
- **销售管理**：销售出库单、销售退货单、零售记录
- **库存管理**：实时库存查询、库存预警、临期预警
- **数据看板**：Dashboard 统计概览

## 项目结构

```
PharmacyIMS/
├── Data/              # DbContext 与数据库配置
├── Models/            # 实体模型
├── Enums/             # 枚举定义
├── ViewModels/        # MVVM ViewModel
├── Views/             # WPF 视图 (.xaml)
├── Helpers/           # 工具类、转换器、命令基类
└── Migrations/        # EF Core 迁移文件
```

## 快速开始

### 1. 环境要求

- Windows 10/11
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server 或 SQL Server LocalDB
- Visual Studio 2022 (推荐)

### 2. 克隆项目

```bash
git clone https://github.com/yourusername/PharmacyIMS.git
cd PharmacyIMS
```

### 3. 配置数据库

默认连接字符串使用本地 SQL Server：

```csharp
Server=.;Database=PharmacyIMS;Trusted_Connection=True;TrustServerCertificate=True;
```

如需修改，请编辑 `PharmacyIMS/Data/AppDbContext.cs` 中的 `OnConfiguring` 方法。

### 4. 运行迁移（首次启动）

```bash
cd PharmacyIMS
dotnet ef database update
```

> 或直接运行程序，EF Core 会自动创建数据库并填充种子数据。

### 5. 启动应用

```bash
dotnet run
```

## 默认账号

| 用户名 | 密码 | 角色 |
|--------|------|------|
| admin | 123456 | 系统管理员 |
| dianzhang | 123456 | 店长 |
| caigou | 123456 | 采购员 |
| xiaoshou | 123456 | 销售员 |

## 截图
<img width="500" height="350" alt="image" src="https://github.com/user-attachments/assets/aa24513c-8880-45be-970b-eba7ed7a402b" />
<img width="500" height="350" alt="image" src="https://github.com/user-attachments/assets/5526e5e9-3375-4e26-9c35-03cb8a30fc88" />
<img width="600" height="350" alt="image" src="https://github.com/user-attachments/assets/03e6b22d-9db6-4de4-965f-ecbc8bc1f14d" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/13929802-20b0-4376-8da6-c58015a3a18b" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/c2d7e313-0001-41ad-97a4-a3f735300334" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/7f2f2c62-d2eb-4b2f-ae49-43c5cf0e2a30" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/f669b236-c014-4200-8a78-86ac0b892fb0" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/2293add5-8190-4438-8419-c153e376fd4b" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/a3b5e7ae-8d76-475c-9c90-52c8096eda92" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/f756c00a-ff70-4f8f-9b14-a4e0e4d62275" />
<img width="700" height="350" alt="image" src="https://github.com/user-attachments/assets/d8533635-c0bf-4b5c-bf55-17a80e1aae43" />
<img width="700" height="350" alt="image" src="https://github.com/user-attachments/assets/ad1cc57a-f4e1-491b-834a-534af07f5a18" />
<img width="700" height="350" alt="image" src="https://github.com/user-attachments/assets/2faa80d0-bb65-49ee-9efe-86bc075d298f" />
<img width="700" height="350" alt="image" src="https://github.com/user-attachments/assets/2929e55b-1825-4003-9cba-8b52aeb25138" />
<img width="700" height="350" alt="image" src="https://github.com/user-attachments/assets/76d4734b-b0b1-4362-a67f-5197ebbe77af" />
<img width="700" height="350" alt="image" src="https://github.com/user-attachments/assets/43f82d09-7e91-450d-ac10-f6a5cc908edd" />
<img width="700" height="350" alt="image" src="https://github.com/user-attachments/assets/6ca8da41-dada-41e6-a180-ac22f78b886c" />



## 贡献

欢迎提交 Issue 和 Pull Request。

## License

[MIT](LICENSE)
