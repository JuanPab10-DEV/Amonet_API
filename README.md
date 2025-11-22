# 🎨 Amonet API - Sistema de Gestión de Citas para Estudio de Tatuajes

## 📋 Descripción del Proyecto

Sistema completo de gestión de citas para un estudio de tatuajes, desarrollado con arquitectura por capas, patrón CQRS y API REST. Permite gestionar clientes, artistas, camillas y citas, con un sistema de auditoría completo.

## 🏗️ Arquitectura

### Frontend
- **Next.js 15.4.5** (Framework React)
- **TypeScript 5.9.2**
- **Bootstrap 5.3.8**
- **Tailwind CSS 3.4.17**

### Backend
- **.NET 9.0**
- **C# 12**
- **Arquitectura por Capas** (Domain, Application, Infrastructure, Api)
- **Patrón CQRS** (Command Query Responsibility Segregation)
- **Dapper 2.1.66** (ORM)
- **FluentValidation** (Validación de datos)

### Base de Datos
- **SQL Server LocalDB**
- Scripts DDL en carpeta `BD/`

## 📁 Estructura del Proyecto

```
/
├── Front/              # Frontend
│   ├── nextjs/        # Proyecto Next.js (Framework React)
│   └── legacy/        # Frontend HTML/CSS/JS puro (backup)
│
├── Back/               # Backend
│   ├── Amonet.Api/            # Capa de presentación (API REST)
│   ├── Amonet.Application/     # Capa de aplicación (lógica de negocio)
│   ├── Amonet.Infrastructure/  # Capa de infraestructura (persistencia)
│   ├── Amonet.Domain/          # Capa de dominio
│   └── Amonet.sln              # Solución de Visual Studio
│
└── BD/                 # Base de Datos
    ├── amonet.sql              # Script DDL principal
    ├── migracion_cedula.sql    # Script de migración
    └── actualizar_cedulas_null.sql
```

## 🚀 Instalación y Ejecución

### Requisitos Previos
- .NET 9 SDK
- Node.js 18+
- SQL Server LocalDB

### Paso 1: Base de Datos
```powershell
# Iniciar LocalDB
sqllocaldb start MSSQLLocalDB

# Crear base de datos
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\amonet.sql"
```

### Paso 2: Backend
```powershell
cd Back\Amonet.Api
dotnet restore
dotnet build
dotnet run
```

El API estará disponible en: `http://localhost:5131`

### Paso 3: Frontend
```powershell
cd Front\nextjs
npm install
npm run dev
```

El frontend estará disponible en: `http://localhost:3000`


## 🎯 Funcionalidades

### Clientes
- ✅ Crear cliente
- ✅ Buscar cliente (por nombre, cédula, correo, teléfono)
- ✅ Obtener cliente por ID
- ✅ Actualizar cliente
- ✅ Validación de cédula única
- ✅ Capitalización automática según RAE

### Citas
- ✅ Crear cita
- ✅ Buscar citas (por cliente o artista)
- ✅ Cancelar cita
- ✅ Iniciar cita
- ✅ Terminar cita
- ✅ Validación de fechas y disponibilidad

### Artistas
- ✅ Listar artistas
- ✅ Buscar artistas
- ✅ Obtener artista por ID

### Camillas
- ✅ Listar camillas
- ✅ Buscar camillas
- ✅ Obtener camilla por ID

### Auditoría
- ✅ Registro automático de acciones
- ✅ Visualización de historial
- ✅ Información detallada (cliente, artista, cambios de estado)

## 🔗 Endpoints de la API

### Clientes
- `POST /api/clientes` - Crear cliente
- `GET /api/clientes/{id}` - Obtener cliente por ID
- `PUT /api/clientes/{id}` - Actualizar cliente
- `GET /api/busqueda/clientes` - Buscar clientes

### Citas
- `POST /api/citas` - Crear cita
- `GET /api/citas` - Listar citas
- `PUT /api/citas/{id}/cancelar` - Cancelar cita
- `PUT /api/citas/{id}/iniciar` - Iniciar cita
- `PUT /api/citas/{id}/terminar` - Terminar cita
- `GET /api/busqueda/citas` - Buscar citas

### Artistas
- `GET /api/artistas/{id}` - Obtener artista por ID
- `GET /api/busqueda/artistas` - Buscar artistas

### Camillas
- `GET /api/camillas/{id}` - Obtener camilla por ID
- `GET /api/busqueda/camillas` - Buscar camillas

### Auditoría
- `GET /api/auditorias` - Obtener auditorías

## ✅ Cumplimiento de Requisitos

### Requisitos Obligatorios
- ✅ HTML5, CSS3+, JavaScript ES6+
- ✅ .NET 9, Arquitectura por Capas, CQRS, Dapper
- ✅ API REST con JSON
- ✅ Script DDL de base de datos
- ✅ Estructura Front/Back/BD

### Tecnologías Opcionales (Puntos Adicionales)
- ✅ Bootstrap 5.3.8
- ✅ Next.js (Framework React)
- ⚠️ OJS Miniframework (pendiente)

## 👨‍💻 Autor

Proyecto desarrollado para el curso de Desarrollo de Software.

## 📄 Licencia

Este proyecto es de uso académico.

---

**Versión:** 1.0.0  
**Última actualización:** 2024
