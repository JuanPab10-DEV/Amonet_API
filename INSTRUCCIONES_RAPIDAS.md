# ⚡ Instrucciones Rápidas - Ejecutar Proyecto

## 🚀 Ejecución en 3 Pasos

### Paso 1: Base de Datos (Solo una vez)

```powershell
# Verificar que LocalDB está ejecutándose
sqllocaldb start MSSQLLocalDB

# Crear base de datos (si no existe)
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\amonet.sql"

# Ejecutar migración de cédula (si es necesario)
sqlcmd -S "(localdb)\MSSQLLocalDB" -i "BD\actualizar_cedulas_null.sql"
```

---

### Paso 2: Backend (Terminal 1)

```powershell
# Navegar al API
cd Back\Amonet.Api

# Compilar
dotnet build

# Ejecutar (dejar esta terminal abierta)
dotnet run
```

**✅ Debe mostrar:** `Now listening on: http://localhost:5131`

**⚠️ IMPORTANTE:** Deja esta terminal abierta

---

### Paso 3: Frontend (Terminal 2 - NUEVA)

```powershell
# Navegar al frontend
cd Front\nextjs

# Instalar dependencias (solo la primera vez)
npm install

# Ejecutar (dejar esta terminal abierta)
npm run dev
```

**✅ Debe mostrar:** `ready started server on 0.0.0.0:3000`

---

## 🌐 Abrir en el Navegador

- **Landing Page:** http://localhost:3000
- **Panel Admin:** http://localhost:3000/admin

---

## ✅ Verificar que Todo Funciona

1. **Abre:** http://localhost:3000/admin
2. **Intenta crear un cliente:**
   - Llena el formulario
   - Haz clic en "Crear Cliente"
   - ✅ Debe mostrar mensaje de éxito
3. **Intenta buscar:**
   - Escribe en el campo de búsqueda
   - ✅ Debe mostrar resultados

---

## 🐛 Problemas Comunes

### "No se puede conectar a la base de datos"
```powershell
sqllocaldb start MSSQLLocalDB
```

### "Puerto 5131 en uso"
```powershell
# Encontrar proceso
netstat -ano | findstr :5131
# Matar proceso (reemplaza PID)
taskkill /PID <PID> /F
```

### "Bootstrap no se ve"
```powershell
cd Front\nextjs
npm install
npm run dev
```

---

## 📝 Notas

- ✅ Backend debe ejecutarse ANTES que frontend
- ✅ Mantén ambas terminales abiertas
- ✅ Backend en puerto 5131
- ✅ Frontend en puerto 3000

