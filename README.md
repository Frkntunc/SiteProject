# SiteProject
Patika.dev &amp; Innova .Net Bootcamp Bitirme Projesi
<br/>
<br/>
## Proje Özet
Asp.Net Core altyapısında mediator kullanılarak geliştirilen bir site yönetim paneli uygulamasıdır. 
Json web token ile rol bazlı kimlik yönetim sistemi oluşturuldu.
Admin aylık olarak fatura tutarlarını ve aidat bilgilerini girer. Bu tutarlar daire başına bölünür. Her kullanıcı kendi dairesine atanan faturaları görür ve bunları öder.
Kullanıcılar birbirlerine sistem üzerinden mesaj atabilir. Bu mesajlar duruma göre yeni veya okunmuş olarak işaretlenerek ilgili kullanıcıya gösterilir.
Veriler sql server'da tutuldu. Cache için redis kullanıldı. 
Ödeme için ayrı bir service oluşturularak rabbitmq ile birbirine bağlandı. 
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
