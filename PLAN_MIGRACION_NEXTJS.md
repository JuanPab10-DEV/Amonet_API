# 📋 Plan de Migración a Next.js + Bootstrap + OJS

## 🎯 Objetivo
Integrar Next.js, Bootstrap y OJS en el proyecto actual para cumplir con los requisitos opcionales y obtener puntos adicionales.

## 📁 Estructura Propuesta

```
/
├── Front/
│   ├── nextjs/              # Nuevo proyecto Next.js
│   │   ├── app/
│   │   ├── components/
│   │   ├── lib/
│   │   ├── package.json
│   │   └── ...
│   ├── legacy/              # Frontend actual (backup)
│   │   ├── index.html
│   │   ├── app.js
│   │   └── estilos.css
│   └── README.md            # Instrucciones
├── Back/
└── BD/
```

## ✅ Opciones de Implementación

### Opción A: Integrar Next.js aquí (RECOMENDADA)
**Ventajas:**
- ✅ Cumple estructura de entrega
- ✅ Todo en un solo proyecto
- ✅ Fácil de entregar
- ✅ Mantiene frontend actual como backup

**Pasos:**
1. Crear carpeta `Front/nextjs/`
2. Inicializar proyecto Next.js
3. Migrar funcionalidades del frontend actual
4. Agregar Bootstrap
5. Agregar OJS (si aplica)

### Opción B: Usar proyecto Next.js existente
**Desventajas:**
- ❌ No cumple estructura de entrega requerida
- ❌ Proyectos separados
- ❌ Más difícil de evaluar

## 🚀 Plan de Ejecución

### Fase 1: Setup Next.js
- [ ] Crear proyecto Next.js en `Front/nextjs/`
- [ ] Configurar `package.json`
- [ ] Configurar `next.config.js`
- [ ] Configurar conexión con API (CORS)

### Fase 2: Agregar Bootstrap
- [ ] Instalar Bootstrap via npm
- [ ] Configurar Bootstrap en Next.js
- [ ] Migrar estilos actuales a clases Bootstrap
- [ ] Mantener tema dark personalizado

### Fase 3: Agregar OJS
- [ ] Investigar qué es OJS
- [ ] Instalar/configurar OJS
- [ ] Integrar con Next.js

### Fase 4: Migración de Funcionalidades
- [ ] Componente: Crear Cliente
- [ ] Componente: Buscar/Editar Cliente
- [ ] Componente: Crear Cita
- [ ] Componente: Buscar/Gestionar Citas
- [ ] Componente: Auditoría
- [ ] Autocompletado
- [ ] Validaciones

### Fase 5: Testing y Documentación
- [ ] Probar todas las funcionalidades
- [ ] Actualizar README
- [ ] Documentar estructura

## 📝 Notas

**Sobre OJS:**
- Necesito confirmar qué es "OJS" que mencionas
- Podría ser:
  - OJS (Open Journal Systems) - pero no aplica para frontend
  - Algún framework JavaScript específico
  - Error de escritura

**Sobre Bootstrap:**
- Usaremos Bootstrap 5
- Integraremos con Next.js
- Mantendremos el tema dark actual

## ⚠️ Consideraciones

1. **Backend no cambia**: La API REST sigue igual
2. **Base de datos no cambia**: SQL Server sigue igual
3. **Frontend actual**: Se mantiene en `Front/legacy/` como backup
4. **Estructura de entrega**: Se mantiene correcta

