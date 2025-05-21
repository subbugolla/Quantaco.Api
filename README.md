# Quantaco Education Portal

Education Portal built using .Net 8 backend APIs and React frontend

## Steps on how to run the application

### Api

   - Open the solution in visual studio
   - Select IIS Express and Run
   - It should load https://localhost:44371/swagger/index.html
   - In memory Db Schema gets created when the first call hits the DbContext
   - Data refreshes as and when the api is restarted

### Client App

Using Visual Studio Code Terminal or any Command Prompt
   - Navigate to the folder where you see \Quantaco.Client\package.json file
   - npm install
   - npm start
   - This loads up the Client application on a browser

## Design Patterns Used

1. **Backend - Repository Pattern**
   - Implemented through Entity Framework Core
   - Provides consistent interface for data operations
   - Abstracts data access logic

2. **Frontend - Observer Pattern**
   - Implemented through Redux for state management
   - Components subscribe to state changes
   - Efficient UI updates

3. **Models for data transfer**
   - Clean separation of concerns
   - Separate models for read and write operations

4. **Dependency Injection**
   - Better testability
   - Loose coupling between components

### Assumptions in real-time
 - In house Identity System available for authentication and authorization
 - RDBMS or similar data storage environment is available
 - Caching technology like Redis can be used for data caching


### Due to time constraints

# Backend
 - Couldn't implement Unit tests for all layers. Added a Test project with a simple test case to demonstrate the capability
 - Couldn't implement Clean Architecture into possible layers

# Frontend
 - Learning and applying the concepts due to lac of commercial experience using React
 - Could use reactQuery with axios for caching the data that improves performance
 - Could improve the look and feel with CSS
 - Couldn't implement Unit Tests
 - Couldn't test the UI for different screen sizes

## Technology Stack

### Backend (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core (In-Memory Database)
- JWT Token Authentication
- Serilog for Logging
- Swagger UI for API Documentation

### Frontend (React)
- TypeScript
- Material-UI for Components
- Formik & Yup for Form Validation
- Redux Toolkit for State Management
- React Router for Navigation
- Axios for API Calls