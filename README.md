# RFC7807ProblemDetails

`RFC7807ProblemDetails` is a .NET library designed to enhance error handling in web APIs by implementing the RFC 7807 standard for HTTP API problem details. This library provides a standardized way to return error responses, making them more informative and helpful for client applications. It supports internationalization for error messages and allows for the integration of custom additional fields and logging.

## Features

- **Standardized Error Responses**: Implements RFC 7807 to provide structured error responses across your API.
- **Internationalization Support**: Offers the ability to return error messages in multiple languages.
- **Custom Additional Fields**: Easily add custom fields to your problem details for more descriptive errors.
- **Integrated Logging**: Built-in support for logging error details for better error tracking and analysis.
- **Easy Integration**: Designed to be easily integrated with existing .NET web APIs.

## Installation

To install the `RFC7807ProblemDetails` library, use the following NuGet command:
```bash
Install-Package RFC7807ProblemDetails
```

Or via the .NET CLI:
```bash
dotnet add package RFC7807ProblemDetails
```

## Usage
### Configuring the Middleware
To use RFC7807ProblemDetails, add the middleware to your application's pipeline in the Startup.cs file:
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseMiddleware<RFC7807ProblemDetailsMiddleware>();
}
```

## Contributing
Contributions are welcome! Please fork the repository and submit pull requests with any enhancements, bug fixes, or improvements. For major changes, please open an issue first to discuss what you would like to change.