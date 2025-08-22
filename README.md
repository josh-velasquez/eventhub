# eventhub
An event platform

## System Design Scenario

## Prerequisites

Before running this project, make sure you have the following installed:

- **Node.js** (v18 or higher) - for the React frontend
- **.NET 8 SDK** - for the ASP.NET Core backend


## Project Structure

This project consists of two main components:
- **Frontend**: React + TypeScript + Vite application (`eventhub/` directory)
- **Backend**: ASP.NET Core Web API (`api/api/` directory)

## Running the Application

### Backend (.NET API)

1. **Navigate to the API directory:**
   ```bash
   cd api/api
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Run the API:**
   ```bash
   dotnet run
   ```

   The API will start on `https://localhost:7287`

4. **Access Swagger UI:**
   - Open your browser and go to `https://localhost:7287/swagger`
   - This provides interactive API documentation

### Frontend (React App)

1. **Navigate to the frontend directory:**
   ```bash
   cd eventhub
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Start the development server:**
   ```bash
   npm run dev
   ```

   The React app will start on `http://localhost:5173`

## Database

The application uses SQLite as the database, which is automatically created when you first run the API. The database file (`event.db`) is preloaded.

## API Endpoints

The API provides the following endpoints (all prefixed with `/api/v0/`):

### Events Endpoints

- **`GET /api/v0/events`** - Retrieves a paginated list of events with filtering, sorting, and search capabilities

### Sales Endpoints

- **`GET /api/v0/sales`** - Retrieves a paginated list of ticket sales with filtering and sorting options
- **`GET /api/v0/sales/event/{eventId}`** - Gets ticket sales for a specific event
- **`GET /api/v0/sales/analytics/sales-count`** - Returns top events ranked by number of tickets sold
- **`GET /api/v0/sales/analytics/total-amount`** - Returns top events ranked by total revenue generated
