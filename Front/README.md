# Frontend - Amonet API

Este directorio contiene el frontend de la aplicación Amonet.

## 📁 Estructura

```
Front/
├── nextjs/          # Proyecto Next.js (Framework React)
│   ├── app/         # Páginas y rutas
│   ├── components/  # Componentes React
│   ├── lib/         # Utilidades y servicios
│   └── package.json
├── legacy/          # Frontend original (HTML/CSS/JS puro)
│   ├── index.html
│   ├── app.js
│   └── estilos.css
└── README.md        # Este archivo
```

## 🚀 Tecnologías

### Requeridas ✅
- HTML5
- CSS3+
- JavaScript ES6+

### Opcionales (Puntos Adicionales) ✅
- **Next.js** (Framework React) - En `nextjs/`
- **Bootstrap 5** - Integrado en Next.js
- **OJS** - Por confirmar

## 📦 Instalación y Ejecución

### Next.js (Recomendado)

```bash
cd Front/nextjs
npm install
npm run dev
```

El frontend estará disponible en `http://localhost:3000`

### Frontend Legacy (Backup)

Simplemente abre `Front/legacy/index.html` en tu navegador.

## 🔗 Conexión con Backend

El frontend se conecta a la API REST en:
- **Desarrollo**: `http://localhost:5131/api`
- **Producción**: Configurar en variables de entorno

## 📝 Notas

- El frontend Next.js es la versión principal
- El frontend legacy se mantiene como referencia/backup
- Ambos frontends consumen la misma API REST

