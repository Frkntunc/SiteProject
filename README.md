# SiteProject (tr)
Patika.dev &amp; Innova .Net Bootcamp Bitirme Projesi
<br/>
<br/>
## Proje Özet
Asp.Net Core altyapısında Mediator kullanılarak geliştirilen bir site yönetim paneli uygulamasıdır. 
Json web token ile rol bazlı kimlik yönetim sistemi oluşturuldu.
Admin aylık olarak fatura tutarlarını ve aidat bilgilerini girer. Bu tutarlar daire başına bölünür. Her kullanıcı kendi dairesine atanan faturaları görür ve bunları öder.
Kullanıcılar birbirlerine sistem üzerinden mesaj atabilir. Bu mesajlar duruma göre yeni veya okunmuş olarak işaretlenerek ilgili kullanıcıya gösterilir.
Veriler Sql server'da tutuldu. Cache için Redis kullanıldı. 
Ödeme için ayrı bir service oluşturularak Rabbitmq ile birbirine bağlandı. 
Daire sahiplerinin kredi kartı bilgileri bu service içinde MongoDb'de tutuldu. 
Client tarafı Asp.Net Mvc ile gelistirildi.
Test için Moq kütüphanesi kullanıldı.
<br/>
<br/>
## Kullanılan Teknolojiler ve Frameworkler
- Asp.Net Core
- Asp.Net Mvc
- Mssql Server
- MongoDb
- EntityFramework
- Automapper
- FluentValidation
- Mediator
- Jwt
- Redis
- RabbitMq
- Moq

<br/>
<br/>

# SiteProject (en)
Patika.dev & Innova .Net Bootcamp Graduation Project
<br/>
<br/>
## Summary
It is a site management panel application developed using Mediator in Asp.Net Core infrastructure.
A role-based identity management system was created with the Json web token.
Admin enters monthly bill amounts and dues information. These amounts are divided per flat. Each user sees and pays the bills assigned to his apartment.
Users can send messages to each other over the system. These messages are marked as new or read, depending on the situation, and shown to the relevant user.
Data is kept in Sql server. Redis is used for cache.
A separate service was created for payment and connected with Rabbitmq.
The credit card information of the flat owners was kept in MongoDb in this service.
Client side is developed with Asp.Net Mvc.
Moq library was used for testing.
<br/>
<br/>
## Technologies and Frameworks
- Asp.Net Core
- Asp.Net Mvc
- Mssql Server
- MongoDb
- EntityFramework
- Automapper
- FluentValidation
- Mediator
- Jwt
- Redis
- RabbitMq
- Moq
