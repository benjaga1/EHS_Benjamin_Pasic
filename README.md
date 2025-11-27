# EHS_Benjamin_Pasic NewsApp

A web application has been developed for browsing, saving, and tracking news articles. The project is built using **ASP.NET Core Razor Pages**, **SQLite** and a modern **Bootstrap 5** design.

---

## Technologies

- ASP.NET Core Razor Pages  
- C#  
- SQLite  
- Bootstrap 5  
- jQuery  
- NewsData.io API (main data source)

---

## Project Setup Instructions

### Requirements

- Visual Studio 2026 Community Edition (ASP.NET workload)  
- .NET 10
- Git  
- Optional: DB Browser for SQLite (for database inspection)

---

### Step 1: Clone the repository

```bash
git clone https://github.com/benjaga1/EHS_Benjamin_Pasic.git
```
### Step 2: Open the project in Visual Studio

1. Launch **Visual Studio 2026**
2. Click **Open a project or solution**
3. Open the `.sln` file located in the project root folder

---

### Step 3: Restore NuGet packages

If Visual Studio does not restore them automatically:

1. Go to **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**  
2. Click **Restore**  

---

### Step 4: Configure the database and API key

1. Create an `.env` file in the root folder and add:
NEWS_API_KEY=news-api-key
> Replace `news-api-key` with your actual NewsAPI key

2. The SQLite database is included in the project (`news.db`) 
   - If you want to inspect the database, open `news.db` using **DB Browser for SQLite**

---

### Step 5: Run the project

1. Select **IIS Express** or another browser target in Visual Studio
2. Press **F5** or click **Start Debugging**
3. The project will launch in your browser

---

### Step 6: Update using Git

To pull the latest changes from the repository: 

```bash
git pull origin main
```
---
### Notes

- The project uses a Razor Pages architecture
- Styling is defined in `wwwroot/css/site.css`
- Make sure to set your `NEWS_API_KEY` in the `.env` file for the API functionality to work
