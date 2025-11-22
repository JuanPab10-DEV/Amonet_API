# Script de Verificación de Bootstrap y Estructura del Proyecto

Write-Host "=== VERIFICACIÓN DE BOOTSTRAP Y ESTRUCTURA ===" -ForegroundColor Cyan
Write-Host ""

# 1. Verificar estructura
Write-Host "1. ESTRUCTURA DE CARPETAS:" -ForegroundColor Yellow
$estructura = @(
    @{Path="Front\nextjs"; Nombre="Frontend Next.js"},
    @{Path="Front\legacy"; Nombre="Frontend Legacy"},
    @{Path="Back\Amonet.Api"; Nombre="Backend API"},
    @{Path="BD\amonet.sql"; Nombre="Script BD"}
)

foreach ($item in $estructura) {
    if (Test-Path $item.Path) {
        Write-Host "   [OK] $($item.Nombre)" -ForegroundColor Green
    } else {
        Write-Host "   [X]  $($item.Nombre) - NO ENCONTRADO" -ForegroundColor Red
    }
}

Write-Host ""

# 2. Verificar Bootstrap en package.json
Write-Host "2. BOOTSTRAP EN PACKAGE.JSON:" -ForegroundColor Yellow
$packagePath = "Front\nextjs\package.json"
if (Test-Path $packagePath) {
    $packageContent = Get-Content $packagePath -Raw
    if ($packageContent -match '"bootstrap"') {
        Write-Host "   [OK] Bootstrap encontrado en package.json" -ForegroundColor Green
        # Extraer versión
        if ($packageContent -match '"bootstrap":\s*"([^"]+)"') {
            $version = $matches[1]
            Write-Host "        Versión: $version" -ForegroundColor Cyan
        }
    } else {
        Write-Host "   [X]  Bootstrap NO encontrado en package.json" -ForegroundColor Red
    }
} else {
    Write-Host "   [X]  package.json no encontrado" -ForegroundColor Red
}

Write-Host ""

# 3. Verificar importación en _app.tsx
Write-Host "3. IMPORTACIÓN DE BOOTSTRAP:" -ForegroundColor Yellow
$appPath = "Front\nextjs\src\pages\_app.tsx"
if (Test-Path $appPath) {
    $appContent = Get-Content $appPath -Raw
    
    if ($appContent -match "bootstrap/dist/css/bootstrap.min.css") {
        Write-Host "   [OK] Bootstrap CSS importado" -ForegroundColor Green
    } else {
        Write-Host "   [X]  Bootstrap CSS NO importado" -ForegroundColor Red
    }
    
    if ($appContent -match "bootstrap/dist/js/bootstrap.bundle.min.js") {
        Write-Host "   [OK] Bootstrap JS importado" -ForegroundColor Green
    } else {
        Write-Host "   [X]  Bootstrap JS NO importado" -ForegroundColor Red
    }
} else {
    Write-Host "   [X]  _app.tsx no encontrado" -ForegroundColor Red
}

Write-Host ""

# 4. Verificar uso de clases Bootstrap
Write-Host "4. USO DE CLASES BOOTSTRAP:" -ForegroundColor Yellow
$componentes = @(
    "Front\nextjs\src\sections\admin\ClientesSection.tsx",
    "Front\nextjs\src\sections\admin\CitasSection.tsx"
)

$clasesBootstrap = @("btn", "card", "form-control", "alert", "table")
$encontradas = 0

foreach ($comp in $componentes) {
    if (Test-Path $comp) {
        $content = Get-Content $comp -Raw
        foreach ($clase in $clasesBootstrap) {
            if ($content -match "className=.*$clase") {
                $encontradas++
                break
            }
        }
    }
}

if ($encontradas -gt 0) {
    Write-Host "   [OK] Clases Bootstrap encontradas en componentes ($encontradas archivos)" -ForegroundColor Green
} else {
    Write-Host "   [X]  No se encontraron clases Bootstrap" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== RESUMEN ===" -ForegroundColor Cyan
Write-Host "Para verificar visualmente Bootstrap:" -ForegroundColor Yellow
Write-Host "1. Ejecuta: cd Front\nextjs && npm install && npm run dev" -ForegroundColor White
Write-Host "2. Abre: http://localhost:3000/admin" -ForegroundColor White
Write-Host "3. Presiona F12 y busca 'bootstrap.min.css' en Network" -ForegroundColor White
Write-Host ""

