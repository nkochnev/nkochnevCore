# nkochnevCore
Личный сайт

Используемые технологии и инструменты:
* .NET Standard 2.0
* .NET core 2.2
* Angular 7
* PostgreSQL
* Rider
* Microsoft Visual Studio Code
* Node JS
* Angular-cli
* JWT auth

Для запуска необходимо:
1. В файле NkochnevCore.WebApi/appsettings.json указать строку подключения для NkochnevDataContext 
Накатить миграции: 
dotnet ef database update --project "NkochnevCore.Infrastructure" --startup-project "NkochnevCore.WebApi"
Для добавления новый миграций:
dotnet ef migrations add addedIsDraftField --project "NkochnevCore.Infrastructure" --startup-project "NkochnevCore.WebApi"

2. Сбилдить frontend через angular-cli (рабочая папка NkochnevCore.Angular). Запустится через прокси, все запросы пойдут на NkochnevCore.WebApi без использования cors
	* npm start
3. запустить backend (startup project - NkochnevCore.WebApi)

Сейчас сайт хостится на ubuntu и postgresql
___

Personel site

Technologies and tools used:
* .NET Standard 2.0
* .NET core 2.2
* Angular 7
* PostgreSQL
* Rider
* Microsoft Visual Studio Code
* Node JS
* Angular-cli
* JWT auth

To start:
1. Set connection string for NkochnevDataContext
Apply migrations: sudo dotnet ef database update -s ../NkochnevCore.WebApi
2. Build frontend via angular-cli (working folder NkochnevCore.Angular). Proxy will start, all requests will go to NkochnevCore.WebApi without using cors
	* npm start
3. Run backend (startup project - NkochnevCore.WebApi)

Now site is hosted on Ubuntu and PostgreSQL