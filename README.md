# 🎓 Blazor Teacher

An interactive .NET 10 tutorial that demonstrates building a full REST API with CRUD operations (using EF Core + SQLite) and a Blazor Dashboard. This comprehensive tutorial covers the complete stack from backend API development to frontend Blazor components.

## 📚 Tutorial Contents

### API Development
- ✅ Environment setup and .NET 10 configuration
- ✅ Solution structure with clean architecture
- ✅ Domain modeling with Entity Framework Core
- ✅ SQLite database configuration
- ✅ EF Core migrations
- ✅ Input validation with FluentValidation
- ✅ Global error handling middleware
- ✅ Structured logging with Serilog

### Blazor Components
- ✅ Components overview and structure
- ✅ Blazor directives (`@page`, `@inject`, `@implements`, etc.)
- ✅ Event handling and binding
- ✅ Component lifecycle methods
- ✅ Rendering modes (Interactive Server, WebAssembly, Auto)
- ✅ Reusable component patterns

### Communication & State
- ✅ Component parameters
- ✅ Cascading parameters
- ✅ Event callbacks for child-to-parent communication
- ✅ State management patterns
- ✅ CSS isolation and styling

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Your favorite IDE (Visual Studio 2022, VS Code, Rider)

### Running the API

```bash
cd src/BlazorTeacher.Api
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001

### Running the Blazor Dashboard

```bash
cd src/BlazorTeacher.Dashboard
dotnet run
```

The Dashboard will be available at:
- HTTP: http://localhost:5002
- HTTPS: https://localhost:5003

### Running Both Simultaneously

```bash
# Terminal 1 - API
cd src/BlazorTeacher.Api
dotnet run --urls "http://localhost:5000"

# Terminal 2 - Dashboard
cd src/BlazorTeacher.Dashboard
dotnet run --urls "http://localhost:5002"
```

## 📁 Project Structure

```
BlazorTeacher/
├── src/
│   ├── BlazorTeacher.Api/           # REST API project
│   │   ├── Controllers/             # API endpoints
│   │   ├── Data/                    # DbContext & configurations
│   │   ├── Middleware/              # Error handling, logging
│   │   ├── Validators/              # FluentValidation validators
│   │   └── Extensions/              # Mapping extensions
│   │
│   ├── BlazorTeacher.Dashboard/     # Blazor Web App
│   │   ├── Components/
│   │   │   ├── Course/              # Course-related components
│   │   │   ├── Shared/              # Reusable components
│   │   │   ├── Tutorial/            # Tutorial pages
│   │   │   ├── Layout/              # Layout components
│   │   │   └── Pages/               # Page components
│   │   └── Services/                # HTTP clients, state management
│   │
│   └── BlazorTeacher.Shared/        # Shared library
│       ├── Models/                  # Domain entities
│       └── DTOs/                    # Data Transfer Objects
│
└── README.md
```

## 🔌 API Endpoints

### Courses

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/courses` | Get all courses (with filtering) |
| GET | `/api/courses/{id}` | Get course by ID |
| GET | `/api/courses/{id}/lessons` | Get lessons for a course |
| GET | `/api/courses/stats` | Get course statistics |
| POST | `/api/courses` | Create a new course |
| PUT | `/api/courses/{id}` | Update a course |
| DELETE | `/api/courses/{id}` | Delete a course |

### Lessons

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/lessons` | Get all lessons |
| GET | `/api/lessons/{id}` | Get lesson by ID |
| POST | `/api/lessons` | Create a new lesson |
| PUT | `/api/lessons/{id}` | Update a lesson |
| DELETE | `/api/lessons/{id}` | Delete a lesson |

## 🧩 Key Components

### Reusable Components

- **LoadingSpinner** - Customizable loading indicator
- **AlertMessage** - Dismissible notification alerts
- **ConfirmDialog** - Modal confirmation dialogs
- **CourseCard** - Course display card with actions
- **CourseForm** - Form for creating/editing courses
- **CourseList** - Grid of course cards

### Interactive Demos

- **Counter** - Basic counter demonstrating state
- **Lifecycle Demo** - Visual component lifecycle explorer

## 📖 Tutorial Pages

Navigate to these pages in the Dashboard to learn:

1. **Home** (`/`) - Tutorial overview and quick links
2. **API Tutorial** (`/tutorial/api`) - REST API development guide
3. **Components Tutorial** (`/tutorial/components`) - Blazor components deep dive
4. **State Tutorial** (`/tutorial/state`) - State management patterns
5. **Course Dashboard** (`/courses`) - Full CRUD example
6. **Lifecycle Demo** (`/lifecycle`) - Interactive lifecycle explorer

## 🛠️ Technologies Used

- **.NET 10** - Latest .NET platform
- **ASP.NET Core Web API** - REST API framework
- **Blazor** - Interactive web UI framework
- **Entity Framework Core** - ORM for data access
- **SQLite** - Lightweight database
- **FluentValidation** - Input validation library
- **Serilog** - Structured logging

## 📝 License

This project is open source and available for educational purposes.

## 🤝 Contributing

Contributions are welcome! Feel free to submit issues and pull requests.
