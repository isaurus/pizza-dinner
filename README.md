# RESTful API con ASP.NET Core Web API

Repositorio base para un sistema escalable de pizzería, comenzando como RESTful API con arquitectura evolutiva.

## 📋 Descripción

Solución tecnológica para una pizzería hipotética diseñada para escalar desde un MVP hasta un sistema empresarial complejo. El proyecto inicia como una API RESTful con C# bajo el patrón MVC, con una evolución planificada hacia **Clean Architecture** y **Domain-Driven Design (DDD)**.

**Evolución arquitectónica:**


## 🚀 Funcionalidades Actuales (Fase Inicial)

### Backend (API RESTful)
- ✅ CRUD para gestión de:
  - Pizzas
  - Pedidos
- ✅ Documentación con Swagger/OpenAPI
- ✅ Base de datos SQL Server (Entity Framework Core)

### Próximas Fases
- 🔄 Migración a Clean Architecture
- 🔄 Implementación de DDD (Aggregates, Value Objects, Domain Events)
- 🔄 Sistema de pagos integrado
- 🔄 Notificaciones en tiempo real (WebSockets/SignalR)
- 🔄 Microservicios para procesamiento especializado

## 🛠 Stack Tecnológico

### Backend
| Tecnología | Uso Actual | Uso Futuro |
|------------|------------|------------|
| C# | ✅ Core del API | ✅ Mantenido |
| ASP.NET Core | ✅ MVC | ✅ Clean Architecture |
| Entity Framework | ✅ ORM Principal | ✅ Patrón Repository |
| SQL Server | ✅ DB Principal | ✅ + CQRS con Redis Cache |
| xUnit | ✅ Testing | ✅ + Integration Tests |
| **Futuras** | ❌ | **MediatR, AutoMapper, FluentValidation** |

### Frontend (Planeado)
- Angular/React (por definir)
- Mobile: Flutter/Xamarin (opcional)

## ⚙️ Instalación

1. Clonar repositorio:
   ```bash
   git clone https://github.com/tu-usuario/pizzeria-scalable-solution.git
