using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Amonet.Application.Abstractions;
using Amonet.Application.Citas;
using Amonet.Application.Citas.Crear;
using Amonet.Application.Citas.Acciones;

namespace Amonet.Api.Controllers;

[ApiController]
[Route("api/citas")]
public sealed class CitasController : ControllerBase
{
    private readonly IManejadorComando<CrearCitaComando, Guid> _crearCita;
    private readonly IManejadorComando<ActualizarEstadoCitaComando, bool> _actualizarEstado;
    private readonly IValidator<CrearCitaComando> _validadorCrear;

    public CitasController(
        IManejadorComando<CrearCitaComando, Guid> crearCita,
        IManejadorComando<ActualizarEstadoCitaComando, bool> actualizarEstado,
        IValidator<CrearCitaComando> validadorCrear)
    {
        _crearCita = crearCita;
        _actualizarEstado = actualizarEstado;
        _validadorCrear = validadorCrear;
    }

    // POST api/citas
    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearCitaComando comando,
        CancellationToken ct)
    {
        var validacion = await _validadorCrear.ValidateAsync(comando, ct);
        if (!validacion.IsValid)
        {
            foreach (var error in validacion.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return ValidationProblem(ModelState);
        }

        var id = await _crearCita.ManejarAsync(comando, ct);
        return CreatedAtAction(nameof(Confirmar), new { id }, new { id });
    }

    // PUT api/citas/{id}/confirm
    [HttpPut("{id:guid}/confirm")]
    public async Task<IActionResult> Confirmar(Guid id, CancellationToken ct)
    {
        var comando = new ActualizarEstadoCitaComando
        {
            Id = id,
            NuevoEstado = "Confirmada",
            AccionAuditoria = "Cita confirmada"
        };

        await _actualizarEstado.ManejarAsync(comando, ct);
        return NoContent();
    }

    // PUT api/citas/{id}/cancel
    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancelar(Guid id, CancellationToken ct)
    {
        var comando = new ActualizarEstadoCitaComando
        {
            Id = id,
            NuevoEstado = "Cancelada",
            AccionAuditoria = "Cita cancelada"
        };

        await _actualizarEstado.ManejarAsync(comando, ct);
        return NoContent();
    }

    // PUT api/citas/{id}/checkin
    [HttpPut("{id:guid}/checkin")]
    public async Task<IActionResult> Checkin(Guid id, CancellationToken ct)
    {
        var comando = new ActualizarEstadoCitaComando
        {
            Id = id,
            NuevoEstado = "EnCurso",
            AccionAuditoria = "Cita checkin"
        };

        await _actualizarEstado.ManejarAsync(comando, ct);
        return NoContent();
    }

    // PUT api/citas/{id}/checkout
    [HttpPut("{id:guid}/checkout")]
    public async Task<IActionResult> Checkout(Guid id, CancellationToken ct)
    {
        var comando = new ActualizarEstadoCitaComando
        {
            Id = id,
            NuevoEstado = "Completada",
            AccionAuditoria = "Cita checkout"
        };

        await _actualizarEstado.ManejarAsync(comando, ct);
        return NoContent();
    }
}
