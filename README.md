# My WEMA Recruitment Assessment: Onboarding API

This solution is built using .NET6, employing a clean architecture and repository pattern.

When run locally, the API opens the Swagger UI, allowing for simple testing of functionality.

FluentValidation was utilised to validate incoming payloads.

A local Redis Server was used for caching, (please modify the redis url on 'appsettings.Development.json' to your valid redis url before running).

To mock the OTP sending feature, I utilised RabbitMQ event publisher and a consumer console that simulates a cell phone and displays the generated OTP. (please modify RabbitMQ credentials on 'appsettings.Development.json' to your valid credentials before running)

The database utilised is Microsoft SQL Server.
