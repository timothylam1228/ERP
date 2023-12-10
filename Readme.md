**Prepare for database data**

1. Startup Microsoft sql server

2. Create database named "Qbuild"
   Open SQL Server Management Studio (SSMS).
   Connect to your SQL Server instance.
   Execute the following SQL command to create a new database named Qbuild:

```sql
CREATE DATABASE Qbuild;
```

3. Create two table named "Bom" & "Part"

```sql
CREATE TABLE Bom (
    /* Define Bom table schema here */
);

CREATE TABLE Part (
    /* Define Part table schema here */
);

```

4. Import data from csv to according table
   Use the SQL Server import wizard or appropriate SQL commands to import data into these tables from your CSV files.

**Start Back End**

Install .NET SDK

navigate to backend/ERP

```bash
cd backend/ERP
```

# Installation

```bash
dotnet restore
```

# Running the Application

```bash
dotnet run
```

# Build the Application

```
dotnet build
```

API Endpoints
The backend will expose the following endpoints:

'/boms'
'/parts'

**Start Front End**

Prerequisites
Node.js (version 12 or higher)
npm (usually comes with Node.js)

# Installation

navigation to frontend folder and install the package

```bash
cd frontend
npm install
```

# Running the Application

```bash
npm run dev
```

# Building for Production

```bash
npm run build
```
