
## Example Requests
- http://localhost:8080/api/investors
- http://localhost:8080/api/investors/4/commitments?assetClass=Infrastructure



##  Key Features
Clean architecture with separation of concerns

Entity Framework Core for ORM

Docker containerization for PostgreSQL

DTO pattern for API responses

Query parameter filtering

Proper entity relationships with navigation properties

Async/await pattern for database operations

## TODO

Use Result type rather than Exception for flow control

Pagination support (could be added via [FromQuery] parameters)

Error handling middleware

Proper structural logging. Use Serilog for example

// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Information()
//     .Enrich.FromLogContext()
// .Enrich.WithCorrelationId()
// .Enrich.WithClientIp()
// .Enrich.WithMachineName()
//     .WriteTo.Console()
//     .CreateLogger();
//
// builder.Host.UseSerilog();

Proper status codes (200 OK, 404 Not Found, etc.)