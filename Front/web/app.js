const BASE = "http://localhost:5131/api";

// Referencias de formularios y elementos de texto
const formularioCrearCliente = document.getElementById("formCliente");
const textoResultadoCrearCliente = document.getElementById("resultadoCliente");
const botonConsultarCliente = document.getElementById("botonConsultarCliente");
const textoResultadoConsultaCliente = document.getElementById("resultadoConsultaCliente");

const formularioCrearCita = document.getElementById("formCrearCita");
const textoResultadoCrearCita = document.getElementById("resultadoCrearCita");

const textoResultadoAccionCita = document.getElementById("resultadoAccionCita");
const botonCancelarCita = document.getElementById("botonCancelarCita");
const botonIniciarCita = document.getElementById("botonIniciarCita");
const botonTerminarCita = document.getElementById("botonTerminarCita");

const botonCargarAuditorias = document.getElementById("botonCargarAuditorias");
const campoMaximoAuditorias = document.getElementById("maximoAuditorias");
const cuerpoTablaAuditorias = document.getElementById("tablaAuditorias");

/*
  Utilidades
*/

// Capitalización según RAE (primera letra mayúscula, resto minúscula)
function capitalizarRAE(texto) {
  if (!texto) return texto;
  return texto
    .split(/\s+/)
    .map(palabra => {
      if (palabra.length === 0) return palabra;
      return palabra[0].toUpperCase() + palabra.slice(1).toLowerCase();
    })
    .join(" ");
}

// Formatear fecha según zona horaria de Medellín (America/Bogota, UTC-5)
function formatearFechaMedellin(fecha) {
  if (!fecha) return "-";
  
  try {
    const fechaObj = new Date(fecha);
    
    // Usar Intl.DateTimeFormat con zona horaria de Bogotá (misma que Medellín)
    const formateador = new Intl.DateTimeFormat('es-CO', {
      timeZone: 'America/Bogota',
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false
    });
    
    // Formatear y ajustar el formato a DD/MM/YYYY, HH:MM:SS
    const partes = formateador.formatToParts(fechaObj);
    const dia = partes.find(p => p.type === 'day').value;
    const mes = partes.find(p => p.type === 'month').value;
    const año = partes.find(p => p.type === 'year').value;
    const hora = partes.find(p => p.type === 'hour').value;
    const minuto = partes.find(p => p.type === 'minute').value;
    const segundo = partes.find(p => p.type === 'second').value;
    
    return `${dia}/${mes}/${año}, ${hora}:${minuto}:${segundo}`;
  } catch (error) {
    console.error("Error al formatear fecha:", error);
    return "-";
  }
}

// Aplicar capitalización automática a inputs de texto
function aplicarCapitalizacion(inputId) {
  const input = document.getElementById(inputId);
  if (!input) return;
  
  input.addEventListener("blur", (e) => {
    if (e.target.value) {
      e.target.value = capitalizarRAE(e.target.value);
    }
  });
}

// Aplicar capitalización a campos de texto
aplicarCapitalizacion("nombreCompleto");
aplicarCapitalizacion("clienteNombreEditar");

/*
  Funcionalidad de Autocompletado Mejorada
*/

let timeoutBusqueda = null;
let indiceSeleccionado = -1;

// Función genérica para crear autocompletado con carga de datos completos
function crearAutocompletado(inputId, hiddenId, dropdownId, endpoint, formatearItem, obtenerTexto, callbackSeleccion) {
  const input = document.getElementById(inputId);
  const hidden = document.getElementById(hiddenId);
  const dropdown = document.getElementById(dropdownId);

  if (!input || !hidden || !dropdown) return;

  input.addEventListener("input", async (e) => {
    const busqueda = e.target.value.trim();
    
    clearTimeout(timeoutBusqueda);
    indiceSeleccionado = -1;

    if (busqueda.length < 2) {
      dropdown.classList.remove("show");
      hidden.value = "";
      return;
    }

    timeoutBusqueda = setTimeout(async () => {
      try {
        const url = `${BASE}/busqueda/${endpoint}?busqueda=${encodeURIComponent(busqueda)}&maximoRegistros=10`;
        const respuesta = await fetch(url);
        const items = await respuesta.json();

        if (!respuesta.ok || !Array.isArray(items)) {
          dropdown.classList.remove("show");
          return;
        }

        if (items.length === 0) {
          dropdown.innerHTML = '<div class="autocomplete-item">No se encontraron resultados</div>';
          dropdown.classList.add("show");
          return;
        }

        dropdown.innerHTML = "";
        items.forEach((item, index) => {
          const div = document.createElement("div");
          div.className = "autocomplete-item";
          div.innerHTML = formatearItem(item);
          div.addEventListener("click", async () => {
            input.value = obtenerTexto(item);
            hidden.value = item.id;
            dropdown.classList.remove("show");
            
            // Cargar datos completos si hay callback
            if (callbackSeleccion) {
              await callbackSeleccion(item.id);
            }
          });
          dropdown.appendChild(div);
        });

        dropdown.classList.add("show");
      } catch (error) {
        console.error("Error en búsqueda:", error);
        dropdown.classList.remove("show");
      }
    }, 300);
  });

  // Cerrar dropdown al hacer clic fuera
  const container = input.closest(".autocomplete-container");
  document.addEventListener("click", (e) => {
    if (container && !container.contains(e.target)) {
      dropdown.classList.remove("show");
    }
  });

  // Navegación con teclado
  input.addEventListener("keydown", async (e) => {
    const items = dropdown.querySelectorAll(".autocomplete-item");
    
    if (e.key === "ArrowDown") {
      e.preventDefault();
      indiceSeleccionado = Math.min(indiceSeleccionado + 1, items.length - 1);
      items.forEach((item, i) => {
        item.classList.toggle("selected", i === indiceSeleccionado);
      });
    } else if (e.key === "ArrowUp") {
      e.preventDefault();
      indiceSeleccionado = Math.max(indiceSeleccionado - 1, -1);
      items.forEach((item, i) => {
        item.classList.toggle("selected", i === indiceSeleccionado);
      });
    } else if (e.key === "Enter" && indiceSeleccionado >= 0) {
      e.preventDefault();
      const item = items[indiceSeleccionado];
      if (item) {
        item.click();
      }
    } else if (e.key === "Escape") {
      dropdown.classList.remove("show");
    }
  });
}

// Función para cargar datos completos del cliente
async function cargarDatosCompletosCliente(id) {
  try {
    const respuesta = await fetch(`${BASE}/clientes/${id}`);
    if (!respuesta.ok) return;
    
    const cliente = await respuesta.json();
    
    // Mostrar datos en sección de solo lectura
    document.getElementById("clienteCedulaMostrar").textContent = cliente.cedula || "-";
    document.getElementById("clienteNombreMostrar").textContent = cliente.nombreCompleto || "-";
    document.getElementById("clienteCorreoMostrar").textContent = cliente.correo || "-";
    document.getElementById("clienteTelefonoMostrar").textContent = cliente.telefono || "-";
    document.getElementById("datosCliente").style.display = "block";
    
    // Llenar formulario de edición
    document.getElementById("clienteIdEditar").value = cliente.id;
    document.getElementById("clienteCedulaEditar").value = cliente.cedula || "";
    document.getElementById("clienteNombreEditar").value = cliente.nombreCompleto || "";
    document.getElementById("clienteCorreoEditar").value = cliente.correo || "";
    document.getElementById("clienteTelefonoEditar").value = cliente.telefono || "";
    
  } catch (error) {
    console.error("Error al cargar datos del cliente:", error);
  }
}

// Función para cargar datos completos del artista
async function cargarDatosCompletosArtista(id) {
  try {
    const respuesta = await fetch(`${BASE}/artistas/${id}`);
    if (!respuesta.ok) return;
    
    const artista = await respuesta.json();
    
    // Mostrar datos en sección de solo lectura
    document.getElementById("citaArtistaNombre").textContent = artista.nombreArtistico || "-";
    document.getElementById("citaArtistaEstilos").textContent = artista.estilos || "-";
    document.getElementById("datosArtistaCita").style.display = "block";
    
  } catch (error) {
    console.error("Error al cargar datos del artista:", error);
  }
}

// Función para cargar datos completos de la camilla
async function cargarDatosCompletosCamilla(id) {
  try {
    const respuesta = await fetch(`${BASE}/camillas/${id}`);
    if (!respuesta.ok) return;
    
    const camilla = await respuesta.json();
    
    // Mostrar datos en sección de solo lectura
    document.getElementById("citaCamillaCodigo").textContent = camilla.codigo || "-";
    document.getElementById("datosCamillaCita").style.display = "block";
    
  } catch (error) {
    console.error("Error al cargar datos de la camilla:", error);
  }
}

// Función para cargar datos completos del cliente en la cita
async function cargarDatosCompletosClienteCita(id) {
  try {
    const respuesta = await fetch(`${BASE}/clientes/${id}`);
    if (!respuesta.ok) return;
    
    const cliente = await respuesta.json();
    
    // Mostrar datos en sección de solo lectura
    document.getElementById("citaClienteCedula").textContent = cliente.cedula || "-";
    document.getElementById("citaClienteNombre").textContent = cliente.nombreCompleto || "-";
    document.getElementById("citaClienteCorreo").textContent = cliente.correo || "-";
    document.getElementById("citaClienteTelefono").textContent = cliente.telefono || "-";
    document.getElementById("datosClienteCita").style.display = "block";
    
  } catch (error) {
    console.error("Error al cargar datos del cliente:", error);
  }
}

// Inicializar autocompletados
crearAutocompletado(
  "citaClienteId",
  "citaClienteIdHidden",
  "dropdownCliente",
  "clientes",
  (item) => `<strong>${item.nombreCompleto}</strong><div class="subtitulo">Cédula: ${item.cedula || ""} ${item.correo ? "• " + item.correo : ""}</div>`,
  (item) => item.nombreCompleto,
  cargarDatosCompletosClienteCita
);

crearAutocompletado(
  "citaArtistaId",
  "citaArtistaIdHidden",
  "dropdownArtista",
  "artistas",
  (item) => `<strong>${item.nombreArtistico}</strong><div class="subtitulo">${item.estilos || ""}</div>`,
  (item) => item.nombreArtistico,
  cargarDatosCompletosArtista
);

crearAutocompletado(
  "citaCamillaId",
  "citaCamillaIdHidden",
  "dropdownCamilla",
  "camillas",
  (item) => `<strong>${item.codigo}</strong>`,
  (item) => item.codigo,
  cargarDatosCompletosCamilla
);

crearAutocompletado(
  "idCliente",
  "idClienteHidden",
  "dropdownConsultaCliente",
  "clientes",
  (item) => `<strong>${item.nombreCompleto}</strong><div class="subtitulo">Cédula: ${item.cedula || ""} ${item.correo ? "• " + item.correo : ""}</div>`,
  (item) => item.nombreCompleto,
  cargarDatosCompletosCliente
);

// Función para cargar datos completos de la cita seleccionada
async function cargarDatosCompletosCita(id) {
  try {
    const respuesta = await fetch(`${BASE}/busqueda/citas?busqueda=&maximoRegistros=100`);
    if (!respuesta.ok) return;
    
    const citas = await respuesta.json();
    const cita = citas.find(c => c.id === id);
    
    if (!cita) return;
    
    // Mostrar datos en sección de solo lectura
    document.getElementById("citaInfoCliente").textContent = `${cita.clienteNombre} (${cita.clienteCedula})`;
    document.getElementById("citaInfoArtista").textContent = cita.artistaNombre;
    document.getElementById("citaInfoFechaInicio").textContent = formatearFechaMedellin(cita.fechaInicio);
    document.getElementById("citaInfoFechaFin").textContent = formatearFechaMedellin(cita.fechaFin);
    document.getElementById("citaInfoEstado").textContent = cita.estado;
    document.getElementById("datosCitaSeleccionada").style.display = "block";
    
  } catch (error) {
    console.error("Error al cargar datos de la cita:", error);
  }
}

// Autocompletado para buscar citas
crearAutocompletado(
  "citaBuscar",
  "citaIdAccion",
  "dropdownCita",
  "citas",
  (item) => `<strong>${item.clienteNombre}</strong> con <strong>${item.artistaNombre}</strong><div class="subtitulo">${formatearFechaMedellin(item.fechaInicio)} - Estado: ${item.estado}</div>`,
  (item) => `${item.clienteNombre} - ${item.artistaNombre}`,
  cargarDatosCompletosCita
);

/*
  Clientes
*/

formularioCrearCliente.addEventListener("submit", async evento => {
  evento.preventDefault();

  textoResultadoCrearCliente.textContent = "Creando cliente…";

  const cuerpo = {
    cedula: document.getElementById("cedula").value.trim(),
    nombreCompleto: capitalizarRAE(document.getElementById("nombreCompleto").value),
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
      formularioCrearCliente.reset();
    } else {
      textoResultadoCrearCliente.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoCrearCliente.textContent = `Error de red: ${error.message}`;
  }
});

// Consultar cliente
botonConsultarCliente.addEventListener("click", async () => {
  const id = document.getElementById("idClienteHidden").value;
  if (!id) {
    textoResultadoConsultaCliente.textContent = "Debes seleccionar un cliente";
    return;
  }

  await cargarDatosCompletosCliente(id);
});

// Editar cliente
const formActualizarCliente = document.getElementById("formActualizarCliente");
const resultadoEditarCliente = document.getElementById("resultadoEditarCliente");
const botonCancelarEdicion = document.getElementById("botonCancelarEdicion");

formActualizarCliente.addEventListener("submit", async evento => {
  evento.preventDefault();

  resultadoEditarCliente.textContent = "Guardando cambios…";

  const id = document.getElementById("clienteIdEditar").value;
  const cuerpo = {
    id: id,
    cedula: document.getElementById("clienteCedulaEditar").value.trim(),
    nombreCompleto: capitalizarRAE(document.getElementById("clienteNombreEditar").value),
    correo: document.getElementById("clienteCorreoEditar").value || null,
    telefono: document.getElementById("clienteTelefonoEditar").value || null
  };

  try {
    const respuesta = await fetch(`${BASE}/clientes/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(cuerpo)
    });

    if (respuesta.ok) {
      resultadoEditarCliente.textContent = "Cliente actualizado correctamente";
      document.getElementById("formEditarCliente").style.display = "none";
      // Recargar datos
      await cargarDatosCompletosCliente(id);
    } else {
      const datos = await respuesta.json().catch(() => ({}));
      resultadoEditarCliente.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    resultadoEditarCliente.textContent = `Error de red: ${error.message}`;
  }
});

botonCancelarEdicion.addEventListener("click", () => {
  document.getElementById("formEditarCliente").style.display = "none";
  resultadoEditarCliente.textContent = "";
});

// Botón para mostrar formulario de edición
document.addEventListener("click", (e) => {
  if (e.target.id === "botonEditarCliente" || e.target.closest("#botonEditarCliente")) {
    document.getElementById("formEditarCliente").style.display = "block";
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

  const clienteId = document.getElementById("citaClienteIdHidden").value;
  const artistaId = document.getElementById("citaArtistaIdHidden").value;
  const camillaId = document.getElementById("citaCamillaIdHidden").value;

  if (!clienteId || !artistaId || !camillaId) {
    textoResultadoCrearCita.textContent = "Debes seleccionar cliente, artista y camilla. Todos deben estar creados previamente.";
    return;
  }

  // Validar que todos existan
  try {
    const [clienteRes, artistaRes, camillaRes] = await Promise.all([
      fetch(`${BASE}/clientes/${clienteId}`),
      fetch(`${BASE}/artistas/${artistaId}`),
      fetch(`${BASE}/camillas/${camillaId}`)
    ]);

    if (!clienteRes.ok || !artistaRes.ok || !camillaRes.ok) {
      textoResultadoCrearCita.textContent = "Error: Uno o más elementos seleccionados no existen. Por favor, selecciona elementos válidos.";
      return;
    }

    const cuerpo = {
      clienteId: clienteId,
      artistaId: artistaId,
      camillaId: camillaId,
      fechaInicio: fechaInicio ? new Date(fechaInicio).toISOString() : null,
      fechaFin: fechaFin ? new Date(fechaFin).toISOString() : null
    };

    const respuesta = await fetch(`${BASE}/citas`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(cuerpo)
    });

    const datos = await respuesta.json();

    if (respuesta.ok) {
      textoResultadoCrearCita.textContent = `Cita creada con id ${datos.id || datos}`;
      document.getElementById("citaIdAccion").value = datos.id || datos;
    } else {
      textoResultadoCrearCita.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoCrearCita.textContent = `Error de red: ${error.message}`;
  }
});

// Acciones sobre citas
async function ejecutarAccionCita(accion) {
  const id = document.getElementById("citaIdAccion").value;

  if (!id) {
    textoResultadoAccionCita.textContent = "Debes buscar y seleccionar una cita primero";
    return;
  }

  textoResultadoAccionCita.textContent = `Aplicando acción ${accion}…`;

  let rutaAccion = "";
  if (accion === "cancelar") rutaAccion = "cancelar";
  if (accion === "iniciar") rutaAccion = "iniciar";
  if (accion === "terminar") rutaAccion = "terminar";

  try {
    const respuesta = await fetch(`${BASE}/citas/${id}/${rutaAccion}`, {
      method: "PUT"
    });

    if (respuesta.ok) {
      textoResultadoAccionCita.textContent = `Cita ${accion} correctamente`;
      // Recargar datos de la cita
      await cargarDatosCompletosCita(id);
      // Recargar auditorías si están visibles
      if (cuerpoTablaAuditorias.innerHTML) {
        botonCargarAuditorias.click();
      }
    } else {
      const datos = await respuesta.json().catch(() => ({}));
      textoResultadoAccionCita.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoAccionCita.textContent = `Error de red: ${error.message}`;
  }
}

botonCancelarCita.addEventListener("click", () => ejecutarAccionCita("cancelar"));
botonIniciarCita.addEventListener("click", () => ejecutarAccionCita("iniciar"));
botonTerminarCita.addEventListener("click", () => ejecutarAccionCita("terminar"));

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
      celdaFecha.textContent = formatearFechaMedellin(item.fecha);
      fila.appendChild(celdaFecha);

      const celdaDatos = document.createElement("td");
      // Intentar parsear JSON y mostrar información más legible
      let datosTexto = item.datos ?? "";
      try {
        const datosJson = JSON.parse(datosTexto);
        let textoLegible = "";
        
        if (datosJson.ClienteNombre && datosJson.ArtistaNombre) {
          textoLegible = `Cliente: ${datosJson.ClienteNombre} (${datosJson.ClienteCedula || ""}) | Artista: ${datosJson.ArtistaNombre}`;
          if (datosJson.EstadoAnterior && datosJson.NuevoEstado) {
            textoLegible += ` | Estado: ${datosJson.EstadoAnterior} → ${datosJson.NuevoEstado}`;
          }
        } else if (datosJson.CitaId) {
          textoLegible = `Cita ID: ${datosJson.CitaId}`;
          if (datosJson.NuevoEstado) {
            textoLegible += ` | Estado: ${datosJson.NuevoEstado}`;
          }
        } else {
          textoLegible = datosTexto;
        }
        
        celdaDatos.textContent = textoLegible || datosTexto;
      } catch {
        celdaDatos.textContent = datosTexto;
      }
      fila.appendChild(celdaDatos);

      cuerpoTablaAuditorias.appendChild(fila);
    }
  } catch (error) {
    cuerpoTablaAuditorias.innerHTML =
      `<tr><td colspan="4">Error de red: ${error.message}</td></tr>`;
  }
});
