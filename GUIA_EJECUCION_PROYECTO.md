# 🚀 Guía de Ejecución del Proyecto Amonet

## 📚 ¿Qué es Bootstrap?

**Bootstrap** es un framework de CSS (hojas de estilo) que proporciona:
- ✅ Componentes pre-diseñados (botones, formularios, tablas, cards, etc.)
- ✅ Sistema de grid responsivo (para layouts adaptativos)
- ✅ Utilidades CSS rápidas (márgenes, padding, colores, etc.)
- ✅ JavaScript para componentes interactivos (modales, dropdowns, etc.)

### 🔍 Cómo Identificar que Bootstrap está Implementado

#### 1. **En el código (package.json)**
```json
"dependencies": {
  "bootstrap": "^5.x.x"  // ✅ Bootstrap instalado
}
```

#### 2. **En el código (_app.tsx)**
```typescript
import 'bootstrap/dist/css/bootstrap.min.css'  // ✅ CSS de Bootstrap importado
require('bootstrap/dist/js/bootstrap.bundle.min.js')  // ✅ JS de Bootstrap cargado
```

#### 3. **En los componentes (ejemplo)**
```tsx
// Clases de Bootstrap se ven así:
<button className="btn btn-primary">  // ✅ "btn" y "btn-primary" son clases de Bootstrap
<div className="card bg-dark">        // ✅ "card" es componente de Bootstrap
<div className="row g-2">            // ✅ "row" es sistema de grid de Bootstrap
<input className="form-control">     // ✅ "form-control" es estilo de Bootstrap
```

#### 4. **Visualmente en el navegador**
- Botones con estilos consistentes
- Formularios con bordes y padding uniformes
- Cards con sombras y bordes redondeados
- Grid responsivo que se adapta a diferentes tamaños de pantalla

---

## 🏃 Cómo Ejecutar el Proyecto Completo

### 📋 Requisitos Previos

1. **.NET 9 SDK** instalado
2. **Node.js** (v18 o superior) instalado
3. **SQL Server LocalDB** ejecutándose
4. **Base de datos** creada y migrada

---

## 🔧 Paso 1: Verificar Base de Datos

### 1.1 Verificar que LocalDB esté ejecutándose
```powershell
sqllocaldb info MSSQLLocalDB
```

### 1.2 Verificar que la base de datos existe
```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "SELECT name FROM sys.databases WHERE name = 'AmonetDb'"
```

### 1.3 Si la base de datos no existe, crearla
```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\amonet.sql"
```

### 1.4 Verificar que el campo Cedula existe
```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "USE AmonetDb; SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Clientes' AND COLUMN_NAME = 'Cedula'"
```

Si no existe, ejecutar la migración:
```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\actualizar_cedulas_null.sql"
```

---

## 🔧 Paso 2: Ejecutar el Backend (API)

### 2.1 Abrir terminal en la raíz del proyecto
```powershell
cd C:\Users\pablo\OneDrive\Documents\GitHub\Amonet_API
```

### 2.2 Navegar a la carpeta del API
```powershell
cd Back\Amonet.Api
```

### 2.3 Compilar el proyecto
```powershell
dotnet build
```

**✅ Debe mostrar:** `Compilación correcta. 0 Errores`

### 2.4 Ejecutar el API
```powershell
dotnet run
```

**✅ Debe mostrar algo como:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5131
```

**⚠️ IMPORTANTE:** Deja esta terminal abierta. El API debe seguir ejecutándose.

---

## 🔧 Paso 3: Ejecutar el Frontend (Next.js)

### 3.1 Abrir una NUEVA terminal (deja el backend ejecutándose)

### 3.2 Navegar a la carpeta del frontend
```powershell
cd C:\Users\pablo\OneDrive\Documents\GitHub\Amonet_API\Front\nextjs
```

### 3.3 Instalar dependencias (solo la primera vez)
```powershell
npm install
```

**✅ Debe instalar:** Bootstrap, React, Next.js, TypeScript, etc.

### 3.4 Ejecutar el frontend
```powershell
npm run dev
```

**✅ Debe mostrar:**
```
  ▲ Next.js 15.x.x
  - Local:        http://localhost:3000
  - ready started server on 0.0.0.0:3000
```

---

## 🌐 Paso 4: Abrir en el Navegador

### 4.1 Landing Page (Página Principal)
Abre: **http://localhost:3000**

**✅ Debe mostrar:** Tu landing page de Amonet con diseño profesional

### 4.2 Panel de Administración
Abre: **http://localhost:3000/admin**

**✅ Debe mostrar:** Panel de administración con:
- Sección de Clientes (con Bootstrap)
- Sección de Citas (con Bootstrap)
- Sección de Auditoría (con Bootstrap)

---

## ✅ Verificación de Funcionalidades

### Verificar Bootstrap está Funcionando

1. **Abre http://localhost:3000/admin**
2. **Inspecciona los elementos:**
   - Botones deben tener estilo Bootstrap (bordes redondeados, colores)
   - Formularios deben tener estilo Bootstrap (inputs con bordes)
   - Cards deben tener sombras y bordes redondeados
   - Grid debe ser responsivo

3. **Abre las herramientas de desarrollador (F12)**
4. **Ve a la pestaña "Network"**
5. **Recarga la página**
6. **Busca:** `bootstrap.min.css` - ✅ Debe aparecer cargado

### Verificar Conexión Backend-Frontend

1. **En el panel de administración, intenta crear un cliente:**
   - Llena el formulario
   - Haz clic en "Crear Cliente"
   - ✅ Debe mostrar mensaje de éxito

2. **Intenta buscar un cliente:**
   - Escribe un nombre en el campo de búsqueda
   - ✅ Debe mostrar resultados

3. **Verifica la consola del navegador (F12 > Console):**
   - ✅ No debe haber errores de conexión
   - ✅ Las peticiones a `http://localhost:5131/api` deben ser exitosas

---

## 🐛 Solución de Problemas

### Error: "No se puede conectar a la base de datos"
**Solución:**
```powershell
# Verificar que LocalDB está ejecutándose
sqllocaldb start MSSQLLocalDB

# Verificar la cadena de conexión en:
# Back\Amonet.Api\appsettings.json
```

### Error: "CORS policy"
**Solución:**
Verificar que en `Back\Amonet.Api\appsettings.json` esté:
```json
"Cors": {
  "AllowedOrigins": [ "http://localhost:3000", "http://localhost:5173" ]
}
```

### Error: "Bootstrap no se ve"
**Solución:**
1. Verificar que `npm install` se ejecutó correctamente
2. Verificar que `_app.tsx` tiene los imports de Bootstrap
3. Limpiar caché: `npm run build` y luego `npm run dev`

### Error: "Puerto 5131 ya en uso"
**Solución:**
```powershell
# Encontrar el proceso
netstat -ano | findstr :5131

# Matar el proceso (reemplaza PID con el número)
taskkill /PID <PID> /F
```

---

## 📊 Estructura de Puertos

| Servicio | Puerto | URL |
|----------|--------|-----|
| Backend API | 5131 | http://localhost:5131 |
| Frontend Next.js | 3000 | http://localhost:3000 |
| SQL Server LocalDB | 1433 | (localdb)\MSSQLLocalDB |

---

## ✅ Checklist de Verificación

- [ ] Base de datos creada y migrada
- [ ] Backend compila sin errores
- [ ] Backend ejecutándose en puerto 5131
- [ ] Frontend instalado (npm install)
- [ ] Frontend ejecutándose en puerto 3000
- [ ] Bootstrap visible en la interfaz
- [ ] Puedo crear un cliente
- [ ] Puedo buscar clientes
- [ ] Puedo crear una cita
- [ ] Puedo ver auditorías
- [ ] No hay errores en la consola del navegador

---

## 🎯 URLs Importantes

- **Landing Page:** http://localhost:3000
- **Panel Admin:** http://localhost:3000/admin
- **API Base:** http://localhost:5131/api
- **API Clientes:** http://localhost:5131/api/clientes
- **API Citas:** http://localhost:5131/api/citas
- **API Auditorías:** http://localhost:5131/api/auditorias

---

## 📝 Notas

- El backend debe ejecutarse ANTES que el frontend
- Mantén ambas terminales abiertas mientras trabajas
- Si cambias código del backend, reinicia el servidor
- Si cambias código del frontend, Next.js recarga automáticamente (hot reload)

