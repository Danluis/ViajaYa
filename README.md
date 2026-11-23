# ✈️ ViajaYa

### Sistema de gestión de viajes, vuelos y reservas

<p align="center">
  **ViajaYa** es una aplicación web desarrollada para facilitar la administración de una agencia de viajes, centralizando la gestión de clientes, agentes, destinos, vuelos, hoteles y reservas en una sola plataforma.
</p>

---

## 🌎 Acerca del proyecto

ViajaYa es un proyecto desarrollado con **ASP.NET Core y Blazor**, orientado a la gestión de operaciones de una agencia de viajes.

Su objetivo es ofrecer una plataforma organizada que permita administrar la información de los clientes y los servicios turísticos, facilitando el control de vuelos, hoteles y sus respectivas reservas.

## ✨ Funcionalidades

- 👥 **Gestión de clientes:** registro, consulta, modificación y eliminación de clientes.
- 🧑‍💼 **Gestión de agentes:** administración de los agentes de la agencia.
- 📍 **Gestión de destinos:** mantenimiento de los destinos disponibles.
- 🛫 **Gestión de aerolíneas:** administración de las aerolíneas.
- ✈️ **Gestión de vuelos:** control de la información de los vuelos.
- 🏨 **Gestión de hoteles:** administración de hoteles.
- 🎫 **Reservas de vuelos:** registro y administración de reservas aéreas.
- 🛎️ **Reservas de hoteles:** control de las reservas de alojamiento.

## 🛠️ Tecnologías utilizadas

| Tecnología | Uso |
|---|---|
| C# | Lenguaje de programación |
| .NET 11 | Plataforma de desarrollo |
| Blazor | Interfaz web interactiva |
| Entity Framework Core 11 | Acceso y gestión de datos |
| SQLite | Base de datos |
| Bootstrap | Diseño y estilos |
| QuickGrid | Visualización de registros |
| ASP.NET Core Identity | Gestión de autenticación |

## 🏗️ Estructura del proyecto

```text
ViajaYa/
│
├── Components/
│   ├── Layout/
│   └── Pages/
│       ├── AerolineaPages/
│       ├── AgentePages/
│       ├── ClientePages/
│       ├── DestinoPages/
│       ├── HotelPages/
│       ├── ReservaHotelPages/
│       ├── ReservaVueloPages/
│       └── VueloPages/
│
├── Data/
│
├── Models/
│
├── Services/
│
├── wwwroot/
│
├── Program.cs
└── ViajaYa.csproj
```

## ⚙️ Requisitos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- [.NET SDK 11](https://dotnet.microsoft.com/download/dotnet/11.0)
- [Visual Studio](https://visualstudio.microsoft.com/) o Visual Studio Code.
- [Git](https://git-scm.com/)

## 🚀 Instalación y ejecución

**1. Clonar el repositorio**

```bash
git clone https://github.com/Danluis/ViajaYa.git
```

**2. Entrar en la carpeta del proyecto**

```bash
cd ViajaYa
```

**3. Restaurar los paquetes**

```bash
dotnet restore
```

**4. Compilar el proyecto**

```bash
dotnet build
```

**5. Ejecutar la aplicación**

```bash
dotnet run
```

La dirección local se mostrará en la consola al iniciar la aplicación.

## 📌 Objetivo académico

Este proyecto forma parte del desarrollo de competencias en ingeniería de software, aplicando conceptos de programación orientada a objetos, desarrollo web, persistencia de datos y operaciones CRUD.

## 👨‍💻 Equipo de desarrollo

Proyecto colaborativo desarrollado por estudiantes de Ingeniería en Sistemas.

## 📄 Licencia

Proyecto desarrollado con fines académicos.
