# 📚 Guía: Bootstrap y Ejecución del Proyecto

## 🎨 ¿Qué es Bootstrap?

**Bootstrap** es un framework de CSS (hojas de estilo) que proporciona:
- **Componentes pre-diseñados**: botones, formularios, tarjetas, tablas, modales, etc.
- **Sistema de grid**: para crear layouts responsivos fácilmente
- **Utilidades CSS**: clases rápidas para espaciado, colores, tipografía, etc.
- **JavaScript interactivo**: componentes como dropdowns, modales, tooltips

### Ejemplo de clases Bootstrap:
```html
<!-- Botón de Bootstrap -->
<button class="btn btn-primary">Click aquí</button>

<!-- Tarjeta de Bootstrap -->
<div class="card">
  <div class="card-header">Título</div>
  <div class="card-body">Contenido</div>
</div>

<!-- Formulario de Bootstrap -->
<input type="text" class="form-control" placeholder="Escribe aquí" />
```

---

## ✅ Cómo Verificar que Bootstrap Está Implementado

### 1. **Verificar en `package.json`**
```json
"dependencies": {
  "bootstrap": "^5.3.8",  // ✅ Bootstrap instalado
  "@popperjs/core": "^2.11.8"  // ✅ Dependencia de Bootstrap
}
```

**Ubicación:** `Front/nextjs/package.json` línea 25

### 2. **Verificar en `_app.tsx`**
```typescript
import 'bootstrap/dist/css/bootstrap.min.css'  // ✅ CSS de Bootstrap importado
require('bootstrap/dist/js/bootstrap.bundle.min.js')  // ✅ JS de Bootstrap cargado
```

**Ubicación:** `Front/nextjs/src/pages/_app.tsx` líneas 3 y 10

### 3. **Verificar uso en componentes**
Busca clases de Bootstrap como:
- `btn`, `btn-primary`, `btn-success`
- `card`, `card-header`, `card-body`
- `form-control`, `form-label`
- `container`, `row`, `col-md-6`
- `alert`, `alert-success`, `alert-danger`
- `table`, `table-dark`, `table-striped`

**Ejemplo en tu código:**
```typescript
// En ClientesSection.tsx
<button className="btn btn-primary">Buscar</button>  // ✅ Clase Bootstrap
<div className="card bg-dark text-white">  // ✅ Clase Bootstrap
  <div className="card-header">  // ✅ Clase Bootstrap
```

### 4. **Verificar visualmente**
Cuando ejecutes el proyecto, verás:
- Botones con estilo de Bootstrap (bordes redondeados, colores)
- Formularios con estilo de Bootstrap
- Tarjetas con sombras y bordes
- Tablas con estilo de Bootstrap

---

## 🚀 Cómo Ejecutar el Proyecto Completo

### **Paso 1: Verificar Base de Datos**

```powershell
# Verificar que SQL Server LocalDB esté corriendo
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "SELECT @@VERSION"
```

Si no está corriendo:
```powershell
sqllocaldb start MSSQLLocalDB
```

### **Paso 2: Ejecutar Migración de Base de Datos (si es necesario)**

Si ya tienes datos y necesitas agregar el campo `Cedula`:

```powershell
# Ejecutar migración
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\actualizar_cedulas_null.sql"
```

Si es la primera vez, ejecuta el script completo:
```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\amonet.sql"
```

### **Paso 3: Ejecutar Backend (API)**

Abre una terminal PowerShell:

```powershell
# Ir a la carpeta del backend
cd Back\Amonet.Api

# Ejecutar la API
dotnet run
```

**Deberías ver:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5131
```

**✅ La API estará corriendo en:** `http://localhost:5131`

### **Paso 4: Ejecutar Frontend Next.js**

Abre **OTRA** terminal PowerShell (deja la del backend corriendo):

```powershell
# Ir a la carpeta del frontend Next.js
cd Front\nextjs

# Instalar dependencias (solo la primera vez)
npm install

# Ejecutar el frontend
npm run dev
```

**Deberías ver:**
```
  ▲ Next.js 15.4.5
  - Local:        http://localhost:3000
```

**✅ El frontend estará corriendo en:** `http://localhost:3000`

### **Paso 5: Probar la Aplicación**

1. **Landing Page (Página Principal):**
   - Abre: `http://localhost:3000`
   - Deberías ver tu landing page de Amonet

2. **Panel de Administración:**
   - Abre: `http://localhost:3000/admin`
   - Deberías ver el panel con Bootstrap implementado

3. **Verificar Bootstrap:**
   - Los botones deben tener estilo de Bootstrap
   - Los formularios deben verse con estilo de Bootstrap
   - Las tarjetas (cards) deben tener sombras y bordes

---

## 🔍 Verificación Detallada de Bootstrap

### **Prueba Visual:**

1. **Abre:** `http://localhost:3000/admin`

2. **Busca estos elementos con estilo Bootstrap:**
   - ✅ Botones con bordes redondeados y colores (btn-primary, btn-success)
   - ✅ Formularios con inputs estilizados (form-control)
   - ✅ Tarjetas con sombras (card)
   - ✅ Tablas con estilo (table, table-dark)
   - ✅ Alertas con colores (alert, alert-success)

3. **Inspecciona el código en el navegador:**
   - Presiona `F12` para abrir DevTools
   - Ve a la pestaña "Network"
   - Recarga la página
   - Busca `bootstrap.min.css` - **debe estar cargado** ✅

4. **Inspecciona un elemento:**
   - Click derecho en un botón → "Inspeccionar"
   - Deberías ver clases como `btn`, `btn-primary` aplicadas

---

## 📋 Checklist de Verificación

### ✅ Backend
- [ ] SQL Server LocalDB corriendo
- [ ] Base de datos creada (`AmonetDb`)
- [ ] Tabla `Clientes` tiene campo `Cedula`
- [ ] API corriendo en `http://localhost:5131`
- [ ] Puedes acceder a `http://localhost:5131/api/clientes` (debe retornar JSON o error 404, pero no error de conexión)

### ✅ Frontend Next.js
- [ ] `npm install` ejecutado sin errores
- [ ] `npm run dev` ejecutado sin errores
- [ ] Frontend corriendo en `http://localhost:3000`
- [ ] Puedes ver la landing page
- [ ] Puedes acceder a `http://localhost:3000/admin`

### ✅ Bootstrap
- [ ] `bootstrap` en `package.json` ✅
- [ ] Bootstrap importado en `_app.tsx` ✅
- [ ] Clases Bootstrap usadas en componentes ✅
- [ ] Estilos de Bootstrap visibles en la página ✅

---

## 🐛 Solución de Problemas

### **Error: "Cannot find module 'bootstrap'"**
```powershell
cd Front\nextjs
npm install bootstrap @popperjs/core
```

### **Error: "Port 5131 already in use"**
```powershell
# Encontrar el proceso
netstat -ano | findstr :5131

# Matar el proceso (reemplaza PID con el número que aparezca)
taskkill /PID [PID] /F
```

### **Error: "Port 3000 already in use"**
```powershell
# Cambiar el puerto en Next.js
# Edita Front/nextjs/package.json y cambia:
"dev": "next dev -p 3001"
```

### **Error de conexión a la API**
- Verifica que el backend esté corriendo
- Verifica que CORS esté configurado en `Back/Amonet.Api/Program.cs`
- Verifica la URL en `Front/nextjs/src/lib/api.ts`

---

## 📝 Comandos Rápidos

```powershell
# Terminal 1: Backend
cd Back\Amonet.Api
dotnet run

# Terminal 2: Frontend
cd Front\nextjs
npm run dev

# Terminal 3: Base de datos (si necesitas)
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\actualizar_cedulas_null.sql"
```

---

## ✅ Resumen

**Bootstrap está implementado si:**
1. ✅ Aparece en `package.json`
2. ✅ Está importado en `_app.tsx`
3. ✅ Se usan clases Bootstrap en los componentes
4. ✅ Los estilos se ven en el navegador

**Para ejecutar el proyecto:**
1. ✅ Base de datos configurada
2. ✅ Backend corriendo (puerto 5131)
3. ✅ Frontend corriendo (puerto 3000)
4. ✅ Acceder a `http://localhost:3000/admin`

