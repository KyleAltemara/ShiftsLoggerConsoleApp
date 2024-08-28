# ShiftsLogger

This is a console application that allows users to log and manage work shifts. The application uses a SQLite database to store and retrieve shift data. Users can insert, delete, update, and view their shifts.
<https://www.thecsharpacademy.com/project/17/shifts-logger>

## Features

### ShiftsLoggerAPI
- Connects to a local SQL database specified in the `app.config` file.
- Contains models for the shifts, and a controller to manage the shifts.
- Uses Entity Framework Core to interact with the database, and created the Migrations.

### ShiftsLoggerConsole
- Displays a menu with options to manage shifts or exit the application.
- Allows users to add, delete, update, and view shifts.
- Uses Spectre.Console to create a user-friendly console interface.
- The ShiftController class contains methods to interact with the database using HttpClientHandler to connect to the API.

## Getting Started

To run the application, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Configure the `app.config` file with the appropriate connection string for SQLite.
4. Build the solution to restore NuGet packages and compile the code.
5. Run the ShiftsLoggerAPI project to start the API.
6. Run the ShiftsLoggerConsole project to start the console application.

## Dependencies

- Microsoft.EntityFrameworkCore: The application uses this package to manage the database context and entity relationships.
- Spectre.Console: The application uses this package to create a user-friendly console interface.
- System.Configuration.ConfigurationManager: The application uses this package to read the connection string from the `app.config` file.

## Usage

1. The application will display a menu with options to manage shifts or exit the application.
2. Select an option by using the arrow keys and press Enter.
3. Follow the prompts to perform the desired action.
4. The application will continue to run until you choose the "Exit" option.

## License

This project is licensed under the MIT License.

## Resources Used

- [The C# Academy](https://www.thecsharpacademy.com/)
- GitHub Copilot to generate code snippets.
