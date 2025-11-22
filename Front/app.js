const BASE = "http://localhost:5131/api";

const formularioCrear = document.getElementById("form-cliente");
const textoResultadoCrear = document.getElementById("resultado-cliente");
const formularioConsulta = document.getElementById("form-consulta");
const textoResultadoConsulta = document.getElementById("resultado-consulta");

formularioCrear.addEventListener("submit", async evento => {
  evento.preventDefault();

  textoResultadoCrear.textContent = "Creando cliente…";

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
      textoResultadoCrear.textContent = `Cliente creado con id ${datos}`;
      document.getElementById("idCliente").value = datos;
    } else {
      textoResultadoCrear.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoCrear.textContent = `Error de red: ${error.message}`;
  }
});

formularioConsulta.addEventListener("submit", async evento => {
  evento.preventDefault();

  textoResultadoConsulta.textContent = "Consultando cliente…";

  const id = document.getElementById("idCliente").value.trim();
  if (!id) {
    textoResultadoConsulta.textContent = "Debes escribir un id de cliente";
    return;
  }

  try {
    const respuesta = await fetch(`${BASE}/clientes/${id}`);
    const datos = await respuesta.json();

    if (respuesta.ok) {
      textoResultadoConsulta.textContent = JSON.stringify(datos, null, 2);
    } else {
      textoResultadoConsulta.textContent =
        `Error ${respuesta.status}: ${JSON.stringify(datos)}`;
    }
  } catch (error) {
    textoResultadoConsulta.textContent = `Error de red: ${error.message}`;
  }
});
