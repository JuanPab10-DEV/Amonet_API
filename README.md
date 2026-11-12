# 🎨 Amonet API - Sistema de Gestión de Citas para Estudio de Tatuajes

## 📋 Descripción del Proyecto

Sistema completo de gestión de citas para un estudio de tatuajes, desarrollado con arquitectura por capas, patrón CQRS y API REST. Permite gestionar clientes, artistas, camillas y citas, con un sistema de auditoría completo.

## 🏗️ Arquitectura

### Frontend
- **HTML5, CSS3 y JavaScript (ES6+)**, sin frameworks
- Consume la API REST con `fetch`

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
│   └── web/           # Interfaz en HTML, CSS y JavaScript
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
- SQL Server LocalDB
- Node.js 18+ (solo para servir el frontend en local)

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
npx http-server Front/web -p 5173
```

El frontend estará disponible en: `http://localhost:5173`

> Sírvelo desde ese puerto en lugar de abrir `index.html` con doble clic: la API solo acepta
> peticiones de los orígenes configurados en `appsettings.json` (`localhost:3000` y `localhost:5173`).


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

## 👨‍💻 Autor

Desarrollado por **Juan Pablo Saldarriaga** como proyecto académico, a partir del caso de
[Amonet Tattoo Studio](https://github.com/JuanPab10-DEV/Amonet), un estudio de tatuajes de Medellín.
