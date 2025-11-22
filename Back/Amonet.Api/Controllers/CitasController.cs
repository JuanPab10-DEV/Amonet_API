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
        return CreatedAtAction(nameof(Cancelar), new { id }, new { id });
    }

    // PUT api/citas/{id}/cancelar
    [HttpPut("{id:guid}/cancelar")]
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

    // PUT api/citas/{id}/iniciar
    [HttpPut("{id:guid}/iniciar")]
    public async Task<IActionResult> Iniciar(Guid id, CancellationToken ct)
    {
        var comando = new ActualizarEstadoCitaComando
        {
            Id = id,
            NuevoEstado = "EnCurso",
            AccionAuditoria = "Cita iniciada"
        };

        await _actualizarEstado.ManejarAsync(comando, ct);
        return NoContent();
    }

    // PUT api/citas/{id}/terminar
    [HttpPut("{id:guid}/terminar")]
    public async Task<IActionResult> Terminar(Guid id, CancellationToken ct)
    {
        var comando = new ActualizarEstadoCitaComando
        {
            Id = id,
            NuevoEstado = "Completada",
            AccionAuditoria = "Cita terminada"
        };

        await _actualizarEstado.ManejarAsync(comando, ct);
        return NoContent();
    }
}
