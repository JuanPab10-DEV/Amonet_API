# Frontend - Amonet

Interfaz web del sistema de gestión de citas, hecha con **HTML5, CSS3 y JavaScript (ES6+)**, sin frameworks.
Consume la API REST del backend (`Back/Amonet.Api`) con `fetch`.

## Estructura

```
Front/
└── web/
    ├── index.html   # Estructura de la interfaz
    ├── estilos.css  # Estilos
    └── app.js       # Lógica: clientes, citas, artistas, camillas y auditoría
```

## Ejecución

1. Levanta la API (`http://localhost:5131`), como se explica en el README principal.
2. Sirve el frontend en el puerto 5173:

```powershell
npx http-server Front/web -p 5173
```

3. Abre `http://localhost:5173`.

> No lo abras con doble clic: la API solo acepta peticiones de `localhost:3000` y `localhost:5173`
> (configurado en `Back/Amonet.Api/appsettings.json`).
