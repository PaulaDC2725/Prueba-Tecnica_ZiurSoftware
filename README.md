# Prueba Técnica: Consumo de API REST y Grilla de Inventario - Ziur Software

## Título y Descripción
Solución desarrollada en Blazor Web App (.NET 8) utilizando C# para el consumo seguro de una API REST protegida mediante autenticación Bearer Token y la presentación estructurada de datos de inventario en una grilla interactiva.

## Arquitectura y Buenas Prácticas
El proyecto fue diseñado aplicando principios SOLID y patrones de Clean Code para garantizar un código mantenible, escalable y desacoplado:

- **Separación de Responsabilidades**: División clara entre las capas de presentación (UI Blazor Components), lógica de integración (Servicios HTTP) y transferencia de datos (Modelos/DTOs).
- **Inyección de Dependencias**: Registro de servicios e interfaces (`IInventarioService`, `InventarioService`, `ILogger<T>`) mediante el contenedor IoC nativo de .NET 8.
- **Manejo Eficiente de Recursos HTTP**: Implementación de `IHttpClientFactory` a través de clientes tipados (`AddHttpClient<IInventarioService, InventarioService>()`) para prevenir el agotamiento de sockets y optimizar el ciclo de vida de las conexiones HTTP.
- **Registro de Eventos (Logging)**: Sustitución de salidas a consola por `ILogger<InventarioService>` para el rastreo estructurado y auditable de excepciones durante la comunicación con la API externa.

## Componentes UI
- **Renderizado de Grilla**: Uso del componente `QuickGrid` (`Microsoft.AspNetCore.Components.QuickGrid`) para el renderizado de alto rendimiento y ordenamiento de columnas en la interfaz.
- **Gestión de Estados de la UI**: Manejo explícito y controlado de los estados del componente (`Cargando`, `Error` y `Éxito/Datos vacíos`), asegurando una experiencia de usuario clara ante respuestas de red.

### Uso de la inteligencia artificial en el desarrollo

Durante el ciclo de desarrollo se utilizó la herramienta antigravity CLI impulsada por el motor de Google Gemini como soporte técnico especializado desde la línea de comandos, enfocando su integración en:

- **Generación y Mapeo de DTOs**: Análisis de la respuesta JSON retornada por el endpoint externo para inferir la estructura envolvente (`ApiResponseDto`) y definir el DTO de los ítems (`InventarioItemDto`).
- **Refactorización de Servicios HTTP**: Optimización del manejo de excepciones, implementación de buenas prácticas de inyecciones de dependencias y estandarización del encabezado de autorización Bearer.
- **Auditoría de Código y Arquitectura**: Validación del cumplimiento de principios SOLID, verificación de tipos anulables (Nullable Reference Types) y eliminación de advertencias de compilación.

## Instrucciones de Ejecución

### Prerrequisitos
- .NET 8.0 SDK o superior instalado.
- Git.

### Pasos de Instalación y Ejecución

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/PaulaDC2725/Prueba-Tecnica_ZiurSoftware
   cd Prueba-Tecnica_ZiurSoftware
   ```

2. Restaurar dependencias del proyecto:
   ```bash
   dotnet restore ZiurTest.Blazor/ZiurTest.Blazor.csproj
   ```

3. Compilar la solución:
   ```bash
   dotnet build ZiurTest.Blazor/ZiurTest.Blazor.csproj
   ```

4. Ejecutar la aplicación:
   ```bash
   dotnet run --project ZiurTest.Blazor/ZiurTest.Blazor.csproj
   ```

5. Acceder a la aplicación desde el navegador en la URL HTTPS indicada en la consola.
