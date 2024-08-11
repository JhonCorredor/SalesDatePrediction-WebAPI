# Proyecto SalesDatePrediction-WebAPI

## Descripción

El proyecto `SalesDatePrediction` es una aplicación desarrollada con .NET Core 8 que predice la fecha de venta de productos basándose en datos históricos y otras variables. 
Esta solución sigue una arquitectura en capas, con una clara separación entre la lógica de negocio, acceso a datos, entidades y la API web.

## Estructura del Proyecto

La solución `SalesDatePrediction` está organizada de la siguiente manera:

- **Business/**: Contiene la lógica de negocio de la aplicación.
  - **Implements/**: Implementaciones concretas de las interfaces de negocio.
  - **Interfaces/**: Define las interfaces que estructuran la lógica de negocio, facilitando la inyección de dependencias y la prueba unitaria.

- **Data/**: Maneja el acceso a datos y las operaciones relacionadas con la persistencia de información.
  - **Implements/**: Implementaciones de las interfaces de acceso a datos, encargadas de la interacción con la base de datos.
  - **Interfaces/**: Define las interfaces para el acceso a datos, permitiendo un acoplamiento débil entre las capas.

- **Entity/**: Define las entidades de datos y el contexto de la base de datos.
  - **Context/**: Contiene la configuración del contexto de la base de datos (`DbContext`), gestionando la conexión y las operaciones sobre la base de datos.
  - **Dto/**: Define los Data Transfer Objects (DTOs) utilizados para transportar datos entre las capas de la aplicación, asegurando que solo se transfiera la información necesaria.
  - **Model/**: Contiene las clases de modelo que representan las entidades de la base de datos, las cuales son fundamentales para la persistencia de los datos.

- **Utilities/**: Proporciona clases auxiliares y utilidades comunes que son utilizadas en toda la solución, como validaciones, formatos, y otros métodos que no pertenecen directamente a las demás capas.

- **WebApi/**: Proyecto principal que expone la API RESTful.
  - **Controllers/**: Define los controladores de API que manejan las solicitudes HTTP, orquestando la interacción entre la lógica de negocio y la capa de datos.
  - **appsettings.json**: Archivo de configuración que contiene parámetros clave de la aplicación, como cadenas de conexión a la base de datos, configuración de logging, entre otros.
  - **Dockerfile**: Define cómo se debe construir la imagen Docker de la aplicación, facilitando el despliegue en entornos de contenedores.
  - **Program.cs**: Punto de entrada principal de la aplicación donde se inicializa la configuración y se arranca el servidor.
  - **ServiceExtensions.cs**: Contiene métodos de extensión para registrar servicios en el contenedor de inyección de dependencias, simplificando la configuración de la aplicación.

- **SalesDatePrediction.Tests/**: Contiene las pruebas unitarias para la solución. Estas pruebas aseguran la correcta funcionalidad del código, permitiendo verificar que la lógica de negocio y las operaciones de acceso a datos funcionan según lo esperado.

## Requisitos Previos

Antes de poner en marcha el proyecto, asegúrate de tener instalado:

- [.NET Core 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (si tu proyecto utiliza una base de datos SQL Server)
- [Docker](https://www.docker.com/) (opcional, si deseas ejecutar la aplicación en un contenedor)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (o [Visual Studio Code](https://code.visualstudio.com/))

## Instalación

Sigue estos pasos para instalar y ejecutar el proyecto localmente:

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/JhonCorredor/SalesDatePrediction-WebAPI.git
   cd SalesDatePrediction
2. **Restaurar las dependencias:**
   ```bash
   dotnet restore
3. **Ejecutar:**
	```bash
	dotnet run --project WebApi
4. **Pruebas Unitarias:**
	```bash
	cd SalesDatePrediction.Tests
	dotnet test

