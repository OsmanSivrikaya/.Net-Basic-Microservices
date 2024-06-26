## Geliştirme aşamasında ❗❗

# Online Kurs Satış Platformu Microservice Mimarisi ile .NET 8

Bu proje, .NET 8 kullanarak geliştirdiğim bir online kurs satış platformudur. Bu projede, farklı microservice'leri dockerize edip, çeşitli veritabanları ve mesaj kuyruk sistemleri ile entegre ederek modern bir microservice mimarisi oluşturulmuştur.

## Microservices

### Catalog Microservice
- **Görev:** Kurs bilgilerini yönetmek ve sunmak
- **Veritabanı:** MongoDB

### Basket Microservice
- **Görev:** Sepet işlemlerini yönetmek
- **Veritabanı:** RedisDB

### Discount Microservice
- **Görev:** İndirim kuponlarını yönetmek
- **Veritabanı:** PostgreSQL

### Order Microservice
- **Görev:** Sipariş işlemlerini yönetmek
- **Teknolojiler:** Domain Driven Design, CQRS (MediatR Library)
- **Veritabanı:** SQL Server

### FakePayment Microservice
- **Görev:** Ödeme işlemlerini yönetmek

### IdentityServer Microservice
- **Görev:** Kullanıcı datalarını yönetmek, token ve refresh token üretmek
- **Veritabanı:** SQL Server

### PhotoStock Microservice
- **Görev:** Kurs fotoğraflarını yönetmek ve sunmak

### API Gateway
- **Kütüphane:** Ocelot Library

### Message Broker
- **Mesaj Kuyruğu:** RabbitMQ
- **Kütüphane:** MassTransit

### Asp.Net Core MVC Microservice
- **Görev:** Microservices'den gelen dataları kullanıcıya göstermek ve etkileşime geçmek

## Teknolojiler
- .NET 8 ile Microservice Architecture
- Asynchronous ve Synchronous Microservice iletişimi
- API Gateway (Ocelot Library)
- RabbitMQ
- Docker & Docker Compose
- IdentityServer4
- AccessToken / RefreshToken
- Domain Driven Design
- CQRS Pattern
- PostgreSQL
- MongoDB
- SQL Server