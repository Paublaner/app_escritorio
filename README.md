# Finanzas — Gestor de finanzas personales

Aplicación de escritorio para gestionar finanzas personales: cuentas, ingresos y gastos por categoría, presupuestos mensuales y un **asistente con IA** que analiza tus gastos, te dice dónde puedes ahorrar y busca ofertas más baratas para tus gastos fijos.

El objetivo del proyecto es aplicar **arquitectura hexagonal (puertos y adaptadores)** e **inyección de dependencias** en una aplicación de escritorio con .NET, y conectarla con un **agente de IA** en Python.

> 🚧 **En desarrollo.** Este README refleja el estado actual del proyecto.

## Funcionalidades

- **Resumen** del mes: saldo total, ingresos, gastos y balance, gráfico de anillo de gastos por categoría, estado de los presupuestos y últimos movimientos.
- **Cuentas** de tipo Banco, Efectivo, Ahorro o Crédito.
- **Movimientos**: ingresos y gastos con filtro por mes y por cuenta. El saldo de la cuenta se actualiza solo.
- **Categorías** de ingreso o de gasto.
- **Presupuestos** mensuales por categoría, con una barra que muestra cuánto se ha usado.
- **Asistente**: chat con un agente de IA que consulta tus datos reales, recuerda la conversación y busca tarifas más baratas en internet.
- Interfaz moderna con **modo claro y oscuro**, que recuerda tu elección o sigue la de Windows.

## Tecnologías

**Aplicación de escritorio (C#)**
- **.NET 10** y **Windows Forms**, con controles dibujados a mano (tarjetas, botones, gráfico)
- **PostgreSQL** con **Entity Framework Core** (Npgsql)
- **FluentResults** para gestionar los errores sin excepciones
- **Microsoft.Extensions.DependencyInjection**, **Logging**, **Configuration** y **Http**

**Asistente de IA (Python)**
- **LangGraph** para el agente con herramientas, y **SqliteSaver** para la memoria de cada conversación
- **Ollama** con el modelo **qwen3**, que se ejecuta en local
- **Tavily** para buscar ofertas en internet
- **FastAPI** + **uvicorn** para exponer el agente a la aplicación

## Arquitectura

El núcleo de la aplicación (modelos y reglas de negocio) no conoce ni la interfaz ni la base de datos ni el asistente. Se comunica con el exterior únicamente a través de **puertos** (interfaces), que implementan los **adaptadores**.

```mermaid
flowchart LR
    UI["WinForms<br/>(adaptador driving)"] --> PD["Puertos driving<br/>ICuentaServicio, IAsistenteServicio..."]
    PD --> APP["Application<br/>CuentaServicio, AsistenteServicio..."]
    APP --> DOM["Domain<br/>Modelos"]
    APP --> PDN["Puertos driven<br/>ICuentaPersistencia, IAsistenteIA..."]
    DB["EF Core + PostgreSQL<br/>(adaptadores driven)"] -.implementa.-> PDN
    IA["AsistenteAdapter<br/>HttpClient"] -.implementa.-> PDN
    IA -- "POST /chat" --> API["FastAPI<br/>agente LangGraph"]
    API --> PG[("PostgreSQL")]
    API --> WEB["Tavily<br/>(internet)"]
```

- **Puertos driving (de entrada):** lo que la aplicación ofrece a la interfaz (crear una cuenta, registrar un movimiento, preguntar al asistente...).
- **Puertos driven (de salida):** lo que la aplicación necesita del exterior (guardar y consultar datos, hablar con el agente de IA).
- **Servicios:** implementan los puertos driving, aplican las reglas de negocio y reciben los puertos driven por **inyección de dependencias**.
- **Entidades EF separadas del dominio:** los adaptadores de base de datos convierten entre modelos de dominio y entidades con mappers, así el dominio no depende de EF Core.

Todas las operaciones devuelven `Result` / `Result<T>` de FluentResults. Así la interfaz recibe los errores de negocio (y los fallos del asistente, por ejemplo si la API está apagada) como resultados, en lugar de tener que capturar excepciones.

### El asistente

El agente es un grafo de LangGraph con dos nodos: el modelo y las herramientas. El modelo decide qué herramienta llamar, recibe el resultado y sigue hasta tener la respuesta.

| Herramienta | Qué hace |
|---|---|
| `obtener_cuentas` | Lista las cuentas con su tipo y saldo |
| `analizar_gastos` | Ingresos y gastos por mes, gastos por categoría, lista de gastos y presupuestos comparados con lo gastado |
| `buscar_ofertas` | Busca en internet tarifas más baratas para un gasto fijo (luz, fibra, móvil, gimnasio...) |

Las instrucciones del agente (`instrucciones.py`) le obligan a mirar los datos antes de aconsejar, a no inventar cifras, a citar el enlace de cada oferta y a no meter datos personales en las búsquedas.

Cada conversación tiene un `hilo_id`: con el mismo hilo, el agente recuerda lo hablado. El botón **Nueva conversación** de la aplicación empieza un hilo nuevo.

## Estructura del proyecto

```
App_Escritorio/                 ← aplicación de escritorio (C#)
├─ Domain/
│  ├─ Model/                    ← Cuenta, Categoria, Movimiento, Presupuesto
│  └─ Ports/
│     ├─ Driven/                ← ICuentaPersistencia, ..., IAsistenteIA
│     └─ Driving/               ← ICuentaServicio, ..., IAsistenteServicio
├─ Application/                 ← servicios con las reglas de negocio
├─ Infrastructure/Driven/Database/
│  ├─ Adapter/                  ← adaptadores EF Core y AsistenteAdapter (HTTP)
│  ├─ Context/PostgresContext.cs
│  ├─ Entity/                   ← entidades de EF Core
│  └─ Mapper/                   ← conversión entre dominio y entidades
├─ Migrations/                  ← migraciones de EF Core
├─ UI/                          ← formulario principal, vistas y controles propios
├─ appsettings.example.json
└─ Program.cs                   ← configuración e inyección de dependencias

Chatbot_Finanzas/               ← asistente de IA (Python)
├─ api.py                       ← endpoint POST /chat (FastAPI)
├─ main.py                      ← grafo de LangGraph y memoria
├─ herramientas.py              ← herramientas del agente
├─ instrucciones.py             ← instrucciones del agente
└─ requirements.txt
```

## Modelo de dominio

| Entidad | Descripción |
|---|---|
| `Cuenta` | Cuenta con nombre, saldo y tipo (Banco, Efectivo, Ahorro, Crédito) |
| `Categoria` | Categoría de movimiento, de tipo Ingreso o Gasto |
| `Movimiento` | Ingreso o gasto asociado a una cuenta y una categoría |
| `Presupuesto` | Límite de gasto para una categoría en un mes concreto |

## Reglas de negocio

Las reglas viven en los servicios de la capa Application. Todas se comprueban **antes** de escribir nada en la base de datos.

**Cuentas**
- El nombre es obligatorio y el saldo inicial no puede ser negativo.
- El saldo solo cambia a través de los movimientos.

**Categorías**
- El nombre es obligatorio.
- Cada categoría es de ingreso o de gasto.
- No puede haber dos categorías con el mismo nombre y tipo.

**Movimientos**
- El importe debe ser mayor que cero.
- La cuenta y la categoría deben existir.
- El tipo (ingreso o gasto) lo determina la categoría, no se indica a mano.
- Un gasto resta del saldo y un ingreso suma.
- Una cuenta no puede quedarse en negativo, **salvo que sea de tipo Crédito**.
- Al eliminar un movimiento, el saldo de la cuenta se revierte.

**Presupuestos**
- El límite debe ser mayor que cero y el mes, válido (1–12).
- Solo se pueden definir para categorías de gasto.
- Hay un único presupuesto por categoría y mes: si ya existe, se actualiza su límite.
- Lo gastado se calcula sumando los gastos de la categoría en ese mes.

**Asistente**
- La pregunta no puede estar vacía.

## Estado

- [x] Modelos de dominio
- [x] Puertos driving y driven con `Result`
- [x] Servicios de cuentas, categorías, movimientos y presupuestos
- [x] Revertir el saldo al eliminar un movimiento
- [x] Persistencia con PostgreSQL y EF Core
- [x] Configuración de la inyección de dependencias
- [x] Interfaz: resumen, cuentas, movimientos, categorías y presupuestos
- [x] Modo claro y oscuro
- [x] Asistente de IA con memoria, conectado a la aplicación por HTTP
- [x] Búsqueda de ofertas en internet
- [ ] Transacción al registrar un movimiento y actualizar el saldo
- [ ] Tests unitarios

## Ejecución

### Requisitos

- Windows y el [SDK de .NET 10](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- Para el asistente: [Python 3](https://www.python.org/downloads/), [Ollama](https://ollama.com/) y una API key gratuita de [Tavily](https://tavily.com/)

### 1. Base de datos y aplicación

```bash
git clone https://github.com/Paublaner/app_escritorio.git
cd app_escritorio/App_Escritorio
```

Copia `appsettings.example.json` como `appsettings.json` y pon tu contraseña de PostgreSQL. Después crea la base de datos con las migraciones y arranca la aplicación:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run
```

### 2. Asistente de IA (opcional)

La aplicación funciona sin el asistente; solo la vista **Asistente** lo necesita.

```bash
ollama pull qwen3

cd Chatbot_Finanzas
python -m venv .venv
.venv\Scripts\activate
pip install -r requirements.txt
```

Crea un archivo `.env` en `Chatbot_Finanzas` con la conexión a la misma base de datos y tu clave de Tavily:

```
FINANZAS_DSN=postgresql://postgres:TU_CONTRASEÑA@localhost:5432/finanzas
TAVILY_API_KEY=tvly-...
```

Arranca la API (con Ollama abierto):

```bash
uvicorn api:app
```

La aplicación la busca en `http://127.0.0.1:8000/`. Si la cambias de sitio, actualiza `Asistente:Url` en `appsettings.json`.
