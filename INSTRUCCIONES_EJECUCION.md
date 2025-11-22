# 🚀 Instrucciones de Ejecución - Proyecto Amonet

## 📋 Requisitos Previos

- ✅ .NET 9 SDK instalado
- ✅ Node.js y npm instalados
- ✅ SQL Server LocalDB instalado
- ✅ Git (opcional, para clonar)

---

## 🗄️ PASO 1: Configurar Base de Datos

### Opción A: Primera vez (Base de datos nueva)

```powershell
# Ejecutar script completo
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\amonet.sql"
```

### Opción B: Ya tienes datos (Agregar campo Cedula)

```powershell
# Ejecutar migración
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\actualizar_cedulas_null.sql"
```

### Verificar que la base de datos existe:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "USE AmonetDb; SELECT COUNT(*) FROM Clientes;"
```

---

## 🔧 PASO 2: Ejecutar Backend (API)

### Abre una terminal PowerShell:

```powershell
# Navegar a la carpeta del backend
cd C:\Users\pablo\OneDrive\Documents\GitHub\Amonet_API\Back\Amonet.Api

# Compilar y ejecutar
dotnet run
```

**✅ Deberías ver:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5131
```

**⚠️ IMPORTANTE:** Deja esta terminal abierta y corriendo.

---

## 🎨 PASO 3: Ejecutar Frontend Next.js

### Abre OTRA terminal PowerShell (nueva):

```powershell
# Navegar a la carpeta del frontend
cd C:\Users\pablo\OneDrive\Documents\GitHub\Amonet_API\Front\nextjs

# Instalar dependencias (solo la primera vez)
npm install

# Ejecutar el frontend
npm run dev
```

**✅ Deberías ver:**
```
  ▲ Next.js 15.4.5
  - Local:        http://localhost:3000
```

**⚠️ IMPORTANTE:** Deja esta terminal también abierta y corriendo.

---

## 🌐 PASO 4: Acceder a la Aplicación

### Opción 1: Landing Page (Página Principal)
```
http://localhost:3000
```

### Opción 2: Panel de Administración
```
http://localhost:3000/admin
```

---

## ✅ Verificar que Todo Funciona

### 1. **Verificar Backend:**
Abre en el navegador:
```
http://localhost:5131/api/clientes
```
**Debería retornar:** JSON (vacío `[]` o lista de clientes) o error 404, pero NO error de conexión.

### 2. **Verificar Frontend:**
- Abre: `http://localhost:3000/admin`
- Deberías ver el panel de administración con Bootstrap
- Los botones deben tener estilo de Bootstrap

### 3. **Probar Funcionalidad:**
1. Crear un cliente
2. Buscar un cliente
3. Crear una cita
4. Ver auditorías

---

## 🔍 Cómo Identificar Bootstrap

### En el Código:
1. Abre: `Front/nextjs/src/pages/_app.tsx`
2. Busca: `import 'bootstrap/dist/css/bootstrap.min.css'` ✅
3. Busca: `require('bootstrap/dist/js/bootstrap.bundle.min.js')` ✅

### En el Navegador:
1. Abre: `http://localhost:3000/admin`
2. Presiona `F12` (DevTools)
3. Ve a la pestaña "Network"
4. Recarga la página (`F5`)
5. Busca: `bootstrap.min.css` - **debe estar cargado** ✅

### Visualmente:
- Botones con bordes redondeados y colores
- Formularios con inputs estilizados
- Tarjetas (cards) con sombras
- Tablas con estilo profesional

---

## 🐛 Problemas Comunes

### Error: "Port 5131 already in use"
```powershell
# Encontrar proceso
netstat -ano | findstr :5131

# Matar proceso (reemplaza PID)
taskkill /PID [NÚMERO] /F
```

### Error: "Port 3000 already in use"
```powershell
# Cambiar puerto en package.json
# Edita: Front/nextjs/package.json
# Cambia: "dev": "next dev -p 3001"
```

### Error: "Cannot find module 'bootstrap'"
```powershell
cd Front\nextjs
npm install bootstrap @popperjs/core
```

### Error: "No se puede conectar a la API"
- Verifica que el backend esté corriendo
- Verifica la URL en `Front/nextjs/src/lib/api.ts`
- Verifica CORS en `Back/Amonet.Api/Program.cs`

---

## 📊 Estructura de Puertos

| Servicio | Puerto | URL |
|----------|--------|-----|
| Backend API | 5131 | http://localhost:5131 |
| Frontend Next.js | 3000 | http://localhost:3000 |
| Base de Datos | - | (localdb)\MSSQLLocalDB |

---

## ✅ Checklist Final

- [ ] Base de datos configurada y migrada
- [ ] Backend corriendo en puerto 5131
- [ ] Frontend corriendo en puerto 3000
- [ ] Puedo acceder a `http://localhost:3000`
- [ ] Puedo acceder a `http://localhost:3000/admin`
- [ ] Veo estilos de Bootstrap en la página
- [ ] Puedo crear un cliente
- [ ] Puedo buscar clientes
- [ ] Puedo crear citas
- [ ] Puedo ver auditorías

---

## 🎯 Siguiente Paso

Una vez que todo esté funcionando, podemos trabajar en integrar "OJS miniframework" si me puedes aclarar qué es exactamente.

