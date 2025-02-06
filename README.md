# TechnicalTestCibergestionBack

## Descripción
Este repositorio contiene el backend del proyecto **TechnicalTestCibergestionBack**, desarrollado con **.NET 8.0** siguiendo los principios de **Clean Architecture**. La aplicación implementa la gestión de encuestas NPS y proporciona una API para el manejo de respuestas y cálculo del puntaje Net Promoter Score (NPS).

## Tecnologías Utilizadas
- **.NET 8.0**
- **Dapper** (Manejo eficiente de consultas SQL)
- **Fluent Validation** (Validación de datos de entrada)
- **AutoMapper** (Mapeo de objetos)
- **MediatR** (Implementación del patrón Mediator)
- **SQL Server** (Base de datos)

## Arquitectura y Estructura de Carpetas
El proyecto sigue la arquitectura limpia (Clean Architecture) y está dividido en las siguientes capas:

- **TechnicalTestCibergestionBack.Infrastructure**: Contiene la implementación de acceso a datos y configuraciones de infraestructura.
- **TechnicalTestCibergestionBack.Domain**: Define las entidades principales y reglas de negocio.
- **TechnicalTestCibergestionBack.Application**: Contiene la lógica de aplicación, como casos de uso y validaciones.
- **TechnicalTestCibergestionBack.Api**: Implementa los controladores y expone las API mediante endpoints REST.
- **Base de Datos**: Contiene la base de datos del proyecto, incluyendo el archivo `TechnicalTestDB.bak` para restaurar la base de datos.

## Base de Datos
Este proyecto utiliza **SQL Server** como base de datos principal. Se recomienda configurar la cadena de conexión en el archivo `appsettings.json` antes de ejecutar la aplicación.

### Configuración de la Base de Datos
1. Asegúrate de tener **SQL Server** instalado y en ejecución.
2. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=tu-servidor;Database=tu-base-de-datos;User Id=tu-usuario;Password=tu-password;"
   }
   ```
3. Restaurar la base de datos utilizando el archivo `TechnicalTestDB.bak` ubicado en la carpeta **Base de Datos**.
4. Ejecutar las migraciones o scripts necesarios para crear la estructura de la base de datos.

## Configuración y Ejecución
### Requisitos Previos
- **.NET 8.0 SDK**
- **SQL Server**
- **Postman o navegador para probar la API**

### Pasos para ejecutar el proyecto
1. Clonar el repositorio:
   ```sh
   git clone https://github.com/tu-usuario/TechnicalTestCibergestionBack.git
   ```
2. Acceder al directorio del proyecto:
   ```sh
   cd TechnicalTestCibergestionBack
   ```
3. Configurar la cadena de conexión a la base de datos en `appsettings.json`.
4. Restaurar paquetes NuGet:
   ```sh
   dotnet restore
   ```
5. Construir y ejecutar el proyecto:
   ```sh
   dotnet run --project TechnicalTestCibergestionBack.Api
   ```
6. Acceder a la documentación de la API en Swagger:
   ```
   https://localhost:7097/swagger/index.html
   ```

## Endpoints Principales

### Autenticación
- **POST** `/api/Auth/login`: Iniciar sesión.
- **POST** `/api/Auth/refresh`: Refrescar token de autenticación.

### Encuesta NPS
- **POST** `/api/Survey`: Enviar respuesta de la encuesta.
- **GET** `/api/Survey`: Obtener resultados de la encuesta.

## Contribución
Si deseas contribuir, sigue estos pasos:
1. Crea un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza los cambios y haz un commit (`git commit -m "Agrega nueva funcionalidad"`).
4. Envía tus cambios (`git push origin feature/nueva-funcionalidad`).
5. Crea un Pull Request.

