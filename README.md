# RunSqlScriptProgram
### This is an Application Created by `dotnet MVC 6.0` this app used the concept `code first `  This has a CRUD Application for managing the `mssql connection string `  and has a small function for taking an `mssql script` as a `file` or more and running on an mssql database instance  based on choose entry `mssql connection string `
---
### install `sql express 2019`
---
### create `login` with name `test` and password `test` or change it and follow changed into `appsettings.json` 
---
### restore `~/Docs/ManualScriptDB.bak` as `ManualScriptDB` database
---
### install `dotnet SDK 6`
---
### from downloaded site folder run powershell 
```powershell
dotnet run
```
---
### for create a new  Go to `ConnectionString` from the header and  `create new` 

![Screenshot%202022-11-20%20220045.png](/attachment/Screenshot%202022-11-20%20220045.png)
---
### `Connection String Name` this name will be used when choose a `Connection String` as a distention for run some `mssql script`
---
### `Connection String Data Source` this `Data Source` or `Server` like `localhost` or `COMPUTERNAME\MSSQLSERVER` will be used when choose a `Connection String` as a `Data Source` into selected `Connection String`
---
### `Connection String UserID` this `UserID`  like `Username` of `mssql` will be used when Access selected a `Connection String` as a `UserID` into selected `Connection String`
---
### `Connection String Password` this `Password`  like `Password of UserID` of `mssql` will be used when Access selected a `Connection String` as a `Password` into selected `Connection String`
---
### `Connection String Initial Catalog` this `Initial Catalog`  like `Database Name` or `Schema` of `mssql` will be used when Access selected a `Connection String` as a `Initial Catalog` into selected `Connection String`
---
### a `ConnectionsId` this `ConnectionsId` as a `Department` or `Category` like `Live` or `Prelive` based on `Staging` or `Environment` will be used when Access selected a `Connection String` as a `identity` based on `Role` or `Privilege` or `Authorization` for `Members` ASAP `not working`
---
![Screenshot%202022-11-20%20220124.png](/attachment/Screenshot%202022-11-20%20220124.png)

### select a `Connection String` from list that's is Connection String using to apply some query
---
### upload one or more than files with any extensions to run content into server based on selected `Connection String`
---
### when `Run` button fire will be create folder `~/wwwroot/Script's` and if file uploaded run success will be copied file into `~/wwwroot/Script's` with file content based on `execution result` and change `file name` with name convention generated with `Date` + `Time` + `Filename` in general if file uploaded run failure will be append `Error` to name of copied file

![Screenshot 2022-11-27 130608.png](/attachment/Screenshot%202022-11-27%20130608.png)

