# ✈️ ViajaYa — Sistema de Gestión de Viajes

<p align="center">
  <strong>Una plataforma web para gestionar servicios y operaciones de una agencia de viajes.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-11.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 11">
  <img src="https://img.shields.io/badge/Blazor-Interactive%20Server-512BD4?style=for-the-badge&logo=blazor&logoColor=white" alt="Blazor">
  <img src="https://img.shields.io/badge/Entity%20Framework%20Core-ORM-6B3FA0?style=for-the-badge" alt="Entity Framework Core">
  <img src="https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C Sharp">
</p>

---

## 📌 Acerca del proyecto

**ViajaYa** es una aplicación web desarrollada como proyecto académico de Ingeniería en Sistemas. Su objetivo es facilitar la administración de una agencia de viajes mediante una plataforma que centraliza la información de clientes, agentes, aerolíneas, destinos, hoteles, vuelos y reservas.

El sistema permite gestionar las operaciones principales de una agencia de viajes desde una interfaz intuitiva, moderna y organizada.

## 🎯 Objetivos

- Centralizar la información de los servicios turísticos.
- Facilitar la administración de clientes y agentes.
- Organizar la información de vuelos, hoteles y destinos.
- Gestionar las reservas de vuelos y hoteles.
- Ofrecer una interfaz sencilla y adaptable a diferentes dispositivos.

## 🚀 Funcionalidades principales

| Módulo | Descripción |
|---|---|
| 👥 Clientes | Registro, consulta, edición y eliminación de clientes. |
| 🧑‍💼 Agentes | Administración de los agentes de viajes. |
| ✈️ Aerolíneas | Gestión de las aerolíneas disponibles. |
| 🌎 Destinos | Administración de destinos turísticos. |
| 🏨 Hoteles | Gestión de información de hoteles. |
| 🛫 Vuelos | Registro y administración de vuelos. |
| 🎫 Reservas de vuelos | Gestión de reservas relacionadas con vuelos. |
| 🛎️ Reservas de hoteles | Administración de reservas de alojamiento. |

## 🛠️ Tecnologías utilizadas

- **C#** — Lenguaje principal de programación.
- **.NET 11** — Plataforma de desarrollo.
- **Blazor Razor Components** — Desarrollo de la interfaz web.
- **Interactive Server** — Modelo de interactividad.
- **Entity Framework Core** — Acceso y gestión de datos.
- **Bootstrap 5** — Diseño y estructura visual.
- **Bootstrap Icons** — Iconografía.
- **QuickGrid** — Presentación de información en tablas.
- **CSS** — Personalización y diseño de la interfaz.
- **Git y GitHub** — Control de versiones.

## 🏗️ Arquitectura del proyecto

La solución está organizada en diferentes proyectos para separar las responsabilidades:

```text
ViajaYa/
│
├── ViajaYa/
│   ├── Components/
│   │   ├── Layout/
│   │   └── Pages/
│   │       ├── AgentePages/
│   │       ├── AerolineaPages/
│   │       ├── ClientePages/
│   │       ├── DestinoPages/
│   │       ├── HotelPages/
│   │       ├── ReservaHotelPages/
│   │       ├── ReservaVueloPages/
│   │       └── VueloPages/
│   │
│   └── wwwroot/
│
├── ViajaYa.Data/
│
├── ViajaYa.Services/
│
├── ViajaYa.Tests/
│
└── ViajaYa.sln
```

**Separación de responsabilidades:**

- `ViajaYa`: aplicación web y componentes de interfaz.
- `ViajaYa.Data`: acceso y persistencia de datos.
- `ViajaYa.Services`: lógica y operaciones del sistema.
- `ViajaYa.Tests`: proyecto destinado a las pruebas.

## 💻 Requisitos

Para ejecutar el proyecto necesitas:

- Windows, Linux o macOS.
- Visual Studio 2022 o Visual Studio Code.
- SDK de .NET 11.
- Git.

## ⚙️ Instalación y ejecución

**1. Clonar el repositorio**

```bash
git clone URL_DEL_REPOSITORIO
```

**2. Entrar a la carpeta del proyecto**

```bash
cd ViajaYa
```

**3. Restaurar los paquetes**

```bash
dotnet restore
```

**4. Compilar la solución**

```bash
dotnet build
```

**5. Ejecutar la aplicación**

```bash
dotnet run --project ViajaYa
```

La dirección local de acceso se mostrará en la consola al iniciar la aplicación.

## 🧪 Pruebas

El proyecto incluye un módulo independiente para pruebas.

Para ejecutar las pruebas disponibles:

```bash
dotnet test
```

## 🎨 Interfaz de usuario

ViajaYa cuenta con una interfaz enfocada en la experiencia del usuario:

- Diseño moderno con identidad visual azul.
- Navegación organizada entre módulos.
- Formularios para registrar y modificar información.
- Tablas para consultar registros.
- Tarjetas con bordes redondeados y sombras suaves.
- Diseño adaptable a diferentes tamaños de pantalla.
- Pantalla de bienvenida animada.

## 📚 Estado del proyecto

**En desarrollo**

El proyecto continúa evolucionando mediante mejoras en la interfaz, organización del código y funcionalidades de los diferentes módulos.

## 👨‍💻 Autores

Este proyecto fue desarrollado en colaboración por:

### Williams Rafael Sánchez Acosta

- GitHub: [@Williamsanchez056](https://github.com/Williamsanchez056)

### Dan Luis

- GitHub: [@Danluis](https://github.com/Danluis)

**Proyecto académico — Ingeniería en Sistemas**

---

<p align="center">
  ✈️ <strong>ViajaYa</strong>
  <br>
  <em>Tu próximo destino comienza aquí.</em>
</p>
