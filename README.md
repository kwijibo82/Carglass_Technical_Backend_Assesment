# Javier Mora - Carglass Technical Backend Assessment

## Descripción del Proyecto
Assesment para **Carglass** creado por Javier Mora **Hiberus**, con el objetivo de demostrar habilidades en el desarrollo backend utilizando
**.NET Core 6.0**. El proyecto se ha realizado en **Visual Studio 2022** siguiendo **Domain-Driven Design (DDD)** para garantizar un diseño modular y escalable.

---

## Características Principales
- **Arquitectura Modular**:
  - **Capa de Aplicación**: Controladores API para exponer servicios RESTful.
  - **Capa de Negocios (BL)**: Implementación de lógica empresarial.
  - **Capa de Datos (DL)**: Acceso a base de datos y repositorios.
  - **Entidades (Entities)**: Modelos de dominio que representan los datos fundamentales del sistema.
  - **DTOs (Data Transfer Objects)**: Para intercambio de datos entre capas de aplicación y lógica de negocios.
  - **Pruebas Unitarias (Test)**: Implementadas para validar funcionalidad crítica.

- **Patrones Implementados**:
  - **Repository Pattern** para el acceso a datos.

- **Buenas Prácticas**:
  - Uso de **RESTful APIs** con métodos estándar como por ejemplo `HttpGet` y `HttpPost`.
  - **Dependency Injection (DI)** para inyección de dependencias.

- **Servicios RESTful**:
  - Rutas organizadas y documentadas para endpoints críticos.
  - Respuesta uniforme con códigos HTTP.

- **Testing**:
  - Validación de lógica empresarial con pruebas unitarias.
  - Mocking de dependencias clave para pruebas aisladas.

---

## Paquetes NuGet Instalados
- **Microsoft.EntityFrameworkCore**: Framework ORM para acceso a base de datos.
- **Microsoft.EntityFrameworkCore.Tools**: Herramientas para migraciones y diseño de base de datos.
- **Microsoft.Extensions.DependencyInjection**: Soporte para inyección de dependencias.
- **Swashbuckle.AspNetCore**: Para generación de documentación Swagger/OpenAPI.
- **xUnit**: Framework para pruebas unitarias.
- **Moq**: Biblioteca para crear mocks en pruebas unitarias.
- **AutoMapper**: Para mapeo automático entre DTOs y entidades de dominio.

---

## TODOs y posibles ideas de mejora
- **Implementar un sistema de logging**: Se recomienda añadir una solución para la traza de errores mediante una biblioteca como `log4net` o `Serilog`. Esto permitirá registrar eventos, errores y métricas importantes del sistema, mejorando la capacidad de depuración y el monitoreo en entornos de producción.
- **Configurar integración continua (CI/CD)**: Implementar pipelines de automatización para pruebas, construcción y despliegue, usando herramientas como GitHub Actions, Azure DevOps o Jenkins.
- **Optimizar la capa de datos**: Evaluar el uso de índices en las tablas de la base de datos para mejorar el rendimiento de las consultas más frecuentes.
- **Documentación Swagger/OpenAPI**: Completar y refinar la documentación de los endpoints para asegurar claridad en su uso y propósito.
- **Validaciones adicionales**: Implementar validaciones más robustas tanto a nivel de DTO como en la lógica empresarial para evitar datos inconsistentes o incorrectos.

---

## Requisitos del Sistema
- **Visual Studio 2022** (Versión 17.7 o superior).
- **.NET SDK 6.0**.
- **SQL Server** para base de datos relacional.

---

## Configuración del Entorno
1. Clonar este repositorio:
   ```bash
   git clone https://github.com/kwijibo82/Carglass_Technical_Backend_Assesment.git
   ```
2. Navegar al directorio raíz:
   ```bash
   cd Carglass-Backend-Assesment
   ```
3. Restaurar los paquetes NuGet:
   ```bash
   dotnet restore
   ```
4. Crear la base de datos utilizando los scripts SQL incluidos:
   - Ejecutar `SCRIPT_BD.sql` para crear la estructura de la base de datos.
   - Ejecutar `INSERTS_PRODUCTOS.sql` para poblar la base de datos con datos de prueba.
5. Iniciar el proyecto:
   ```bash
   dotnet build
   dotnet run --project Carglass.TechnicalAssesment.Backend.Api
   ```

---

## Estructura del Repositorio
```
├── Carglass.TechnicalAssesment.Backend.Api
├── Carglass.TechnicalAssesment.Backend.BL
├── Carglass.TechnicalAssesment.Backend.Dtos
├── Carglass.TechnicalAssesment.Backend.Entities
├── Carglass.TechnicalAssesment.Backend.DL
├── Carglass.TechnicalAssesment.Backend.Test
├── Scripts
│   ├── SCRIPT_BD.sql
│   ├── INSERTS_PRODUCTOS.sql
```