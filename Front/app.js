const BASE = "http://localhost:5131/api";

// Referencias de formularios y elementos de texto
const formularioCrearCliente = document.getElementById("formCliente");
const textoResultadoCrearCliente = document.getElementById("resultadoCliente");
const formularioConsultaCliente = document.getElementById("formConsultaCliente");
const textoResultadoConsultaCliente = document.getElementById("resultadoConsultaCliente");

const formularioCrearCita = document.getElementById("formCrearCita");
const textoResultadoCrearCita = document.getElementById("resultadoCrearCita");

const formularioAccionesCita = document.getElementById("formAccionesCita");
const textoResultadoAccionCita = document.getElementById("resultadoAccionCita");

const botonCargarAuditorias = document.getElementById("botonCargarAuditorias");
const campoMaximoAuditorias = document.getElementById("maximoAuditorias");
const cuerpoTablaAuditorias = document.getElementById("tablaAuditorias");

/*
  Clientes
*/

formularioCrearCliente.addEventListener("submit", async evento => {
  evento.preventDefault();

  textoResultadoCrearCliente.textContent = "Creando cliente…";

  const cuerpo = {
    nombreCompleto: document.getElementById("nombreCompleto").value,
    correo: document.getElementById("correo").value || null,
    telefono: document.getElementById("telefono").value || null
  };

  try {
    const respuesta = await fetch(`${BASE}/clientes`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(cuerpo)
    });

    const datos = await respuesta.json();

    if (respuesta.ok) {
      textoResultadoCrearCliente.textContent = `Cliente creado con id ${datos}`;
      document.getElementById("idCliente").value = datos;
    } else {
      textoResultadoCrearCliente.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoCrearCliente.textContent = `Error de red: ${error.message}`;
  }
});

formularioConsultaCliente.addEventListener("submit", async evento => {
  evento.preventDefault();

  textoResultadoConsultaCliente.textContent = "Consultando cliente…";

  const id = document.getElementById("idCliente").value.trim();
  if (!id) {
    textoResultadoConsultaCliente.textContent = "Debes escribir un id de cliente";
    return;
  }

  try {
    const respuesta = await fetch(`${BASE}/clientes/${id}`);
    const datos = await respuesta.json();

    if (respuesta.ok) {
      textoResultadoConsultaCliente.textContent = JSON.stringify(datos, null, 2);
    } else {
      textoResultadoConsultaCliente.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoConsultaCliente.textContent = `Error de red: ${error.message}`;
  }
});

/*
  Citas
*/

formularioCrearCita.addEventListener("submit", async evento => {
  evento.preventDefault();

  textoResultadoCrearCita.textContent = "Creando cita…";

  const fechaInicio = document.getElementById("citaFechaInicio").value;
  const fechaFin = document.getElementById("citaFechaFin").value;

  const cuerpo = {
    clienteId: document.getElementById("citaClienteId").value.trim(),
    artistaId: document.getElementById("citaArtistaId").value.trim(),
    camillaId: document.getElementById("citaCamillaId").value.trim(),
    fechaInicio: fechaInicio ? new Date(fechaInicio).toISOString() : null,
    fechaFin: fechaFin ? new Date(fechaFin).toISOString() : null
  };

  try {
    const respuesta = await fetch(`${BASE}/citas`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(cuerpo)
    });

    const datos = await respuesta.json();

    if (respuesta.ok) {
      textoResultadoCrearCita.textContent = `Cita creada con id ${datos}`;
      document.getElementById("citaIdAccion").value = datos;
    } else {
      textoResultadoCrearCita.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoCrearCita.textContent = `Error de red: ${error.message}`;
  }
});

formularioAccionesCita.addEventListener("click", async evento => {
  if (evento.target.tagName !== "BUTTON") {
    return;
  }

  const accion = evento.target.getAttribute("data-accion");
  const id = document.getElementById("citaIdAccion").value.trim();

  if (!id) {
    textoResultadoAccionCita.textContent = "Debes escribir un id de cita";
    return;
  }

  textoResultadoAccionCita.textContent = `Aplicando acción ${accion}…`;

  let rutaAccion = "";
  if (accion === "confirmar") rutaAccion = "confirm";
  if (accion === "cancelar") rutaAccion = "cancel";
  if (accion === "checkin") rutaAccion = "checkin";
  if (accion === "checkout") rutaAccion = "checkout";

  try {
    const respuesta = await fetch(`${BASE}/citas/${id}/${rutaAccion}`, {
      method: "PUT"
    });

    if (respuesta.ok) {
      textoResultadoAccionCita.textContent =
        `Acción ${accion} aplicada correctamente`;
    } else {
      const datos = await respuesta.json().catch(() => ({}));
      textoResultadoAccionCita.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoAccionCita.textContent = `Error de red: ${error.message}`;
  }
});

/*
  Auditorías
*/

botonCargarAuditorias.addEventListener("click", async () => {
  const maximo = Number(campoMaximoAuditorias.value) || 50;

  cuerpoTablaAuditorias.innerHTML = "<tr><td colspan=\"4\">Cargando…</td></tr>";

  try {
    const respuesta = await fetch(
      `${BASE}/auditorias?maximoRegistros=${encodeURIComponent(maximo)}`
    );
    const lista = await respuesta.json();

    if (!respuesta.ok) {
      cuerpoTablaAuditorias.innerHTML =
        `<tr><td colspan="4">Error ${respuesta.status}: ${JSON.stringify(lista)}</td></tr>`;
      return;
    }

    if (!Array.isArray(lista) || lista.length === 0) {
      cuerpoTablaAuditorias.innerHTML =
        "<tr><td colspan=\"4\">No hay registros de auditoría</td></tr>";
      return;
    }

    cuerpoTablaAuditorias.innerHTML = "";
    for (const item of lista) {
      const fila = document.createElement("tr");

      const celdaId = document.createElement("td");
      celdaId.textContent = item.id;
      fila.appendChild(celdaId);

      const celdaAccion = document.createElement("td");
      celdaAccion.textContent = item.accion;
      fila.appendChild(celdaAccion);

      const celdaFecha = document.createElement("td");
      celdaFecha.textContent = new Date(item.fecha).toLocaleString();
      fila.appendChild(celdaFecha);

      const celdaDatos = document.createElement("td");
      celdaDatos.textContent = item.datos ?? "";
      fila.appendChild(celdaDatos);

      cuerpoTablaAuditorias.appendChild(fila);
    }
  } catch (error) {
    cuerpoTablaAuditorias.innerHTML =
      `<tr><td colspan="4">Error de red: ${error.message}</td></tr>`;
  }
});
