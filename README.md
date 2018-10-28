# nkochnevCore
Личный сайт

Используемые технологии и инструменты:
* .NET Standard 2.0
* .NET core 2.1
* Angular 5
* MS SQL
* PostgreSQL
* Microsoft Visual Studio 2017
* Microsoft Visual Studio Code
* Node JS
* Angular-cli
* JWT auth

Для запуска необходимо:
1. Выбрать БД (MSSQL или Postgresql), поменять строку подключения NkochnevDataContext и раскомментировать нужный блок в файлах:
		- NkochnevCore.WebApi/appsettings.json
		- NkochnevCore.WebApi/Startup.cs	
		- NkochnevCore.WebApi/DesignTimeDbContextFactory.cs
Накатить миграции: sudo dotnet ef database update -s ../NkochnevCore.WebApi
2. Сбилдить frontend через angular-cli (рабочая папка NkochnevCore.Angular). Запустится через прокси, все запросы пойдут на NkochnevCore.WebApi без использования cors
	* npm start
3. запустить backend (startup project - NkochnevCore.WebApi)

Сейчас сайт хостится на ubuntu и postgresql
___

Personel site

Technologies and tools used:
* .NET Standard 2.0
* .NET core 2.1
* Angular 5
* MS SQL
* PostgreSQL
* Microsoft Visual Studio 2017
* Microsoft Visual Studio Code
* Node JS
* Angular-cli
* JWT auth

To start:
1. Select DB (MSSQL или Postgresql), change connection sting NkochnevDataContext and uncomment target code block in files:
		- NkochnevCore.WebApi/appsettings.json
		- NkochnevCore.WebApi/Startup.cs	
		- NkochnevCore.WebApi/DesignTimeDbContextFactory.cs
Apply migrations: sudo dotnet ef database update -s ../NkochnevCore.WebApi
2. Build frontend via angular-cli (working folder NkochnevCore.Angular). Proxy will start, all requests will go to NkochnevCore.WebApi without using cors
	* npm start
3. Run backend (startup project - NkochnevCore.WebApi)

Now site is hosted on Ubuntu and PostgreSQL
