# Staff Management Web

This repository implements a Staff Management system using ASP.NET Core Web API and ASP.NET Core MVC. It allows users to manage staff data, including searching and exporting staff information.

**Features**
* **Staff API**: Provides endpoints for CRUD (Create, Read, Update, Delete) operations on staff data.
* **Search Functionality**: Enables searching staff by ID, Gender, and Birthday range.
* **Export Functionality**: Offers options to export staff data to PDF and Excel formats.
* **MVC Client**: Presents a user-friendly interface for searching and potentially viewing staff details (depending on chosen implementation).

**Prerequisites**
* .NET Core SDK (https://dotnet.microsoft.com/en-us/download)
* Visual Studio 2022(recommended) or a compatible code editor

**Installation**
1. Clone this repository:
```Bash
git clone https://github.com/kimchimysr/StaffManagementWeb.git
```
2. Open the solution file (.sln) in Visual Studio.
3. Ensure you have the necessary dependencies installed. You can restore them using the Package Manager Console:
```Bash
dotnet restore
```

**Usage**
1. Change the Connection String in the following projects <br/>
* **Web API**: StaffManagementWebAPI/appsetting.json
```Json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\MSSQLServer1;Database=DBStaff;Trusted_Connection=True;MultipleActiveResultSets=true;"
}
```
* **Web API Test**: StaffManagementWebAPI.Test/Fixture/WebApplicationFactoryFixture.cs
```C#
private const string _connectionString
    = @$"Server=.\MSSQLServer1;Database=UserIntegration;Trusted_Connection=True";
```
2. Change Server Name in MVC
* **Server Name**: StaffManagementMVC/Helper/HttpHelper
```C#
private static readonly string _serverName = "http://localhost:8081";
```
3. Run Web API and MVC

**Technologies Used**
* ASP.NET Core Web API (.NET 6)
* ASP.NET Core MVC (.NET 6)
* Entity Framework Core 6
* ClosedXML v0.102.2 (Export to Excel)
* itext7 v7.2.6 (Export to PDF)
