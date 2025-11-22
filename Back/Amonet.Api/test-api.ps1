# Script para probar la API de Clientes

$baseUrl = "http://localhost:5131"

Write-Host "=== Probando API de Clientes ===" -ForegroundColor Green

# 1. Crear un cliente
Write-Host "`n1. Creando un cliente..." -ForegroundColor Yellow

$body = @{
    nombreCompleto = "Juan Pérez"
    correo = "juan.perez@example.com"
    telefono = "+34 600 123 456"
} | ConvertTo-Json

try {
    $response = Invoke-RestMethod -Uri "$baseUrl/api/clientes" `
        -Method POST `
        -ContentType "application/json" `
        -Body $body
    
    $clienteId = $response
    Write-Host "✓ Cliente creado exitosamente!" -ForegroundColor Green
    Write-Host "  ID del cliente: $clienteId" -ForegroundColor Cyan
    
    # 2. Obtener el cliente recién creado
    Write-Host "`n2. Obteniendo el cliente por ID..." -ForegroundColor Yellow
    
    $cliente = Invoke-RestMethod -Uri "$baseUrl/api/clientes/$clienteId" `
        -Method GET `
        -ContentType "application/json"
    
    Write-Host "✓ Cliente obtenido exitosamente!" -ForegroundColor Green
    Write-Host "  Datos del cliente:" -ForegroundColor Cyan
    $cliente | ConvertTo-Json -Depth 3
    
} catch {
    Write-Host "✗ Error: $($_.Exception.Message)" -ForegroundColor Red
    if ($_.Exception.Response) {
        $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
        $responseBody = $reader.ReadToEnd()
        Write-Host "  Respuesta del servidor: $responseBody" -ForegroundColor Red
    }
}

Write-Host "`n=== Prueba completada ===" -ForegroundColor Green

