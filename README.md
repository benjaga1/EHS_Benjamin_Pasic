# EHS_Benjamin_Pasic NewsApp

Napravljena je web aplikacija za pregled, spremanje i praćenje vijesti. Projekat je napravljen koristeći **ASP.NET Core Razor Pages**, **SQLite** i moderni **Bootstrap 5** dizajn.

---

## Tehnologije

- ASP.NET Core Razor Pages  
- C#  
- SQLite  
- Bootstrap 5  
- jQuery  
- NewsAPI (za dohvat novosti)  

---

## Upute za pokretanje projekta

### Zahtjevi

- Visual Studio 2026 Community Edition (ASP.NET workload)  
- .NET 10
- Git  
- Opcionalno: DB Browser for SQLite (za pregled baze)  

---

### Korak 1: Kloniranje repozitorija

```bash
git clone https://github.com/benjaga1/EHS_Benjamin_Pasic.git
```
### Korak 2: Otvori projekat u Visual Studio

1. Pokreni **Visual Studio 2026**.  
2. Klikni **Open a project or solution**.  
3. Otvori `.sln` fajl iz root foldera projekta.  

---

### Korak 3: Restore NuGet paketa

Ako Visual Studio ne restore-a automatski:  

1. Idi na **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**  
2. Klikni **Restore**  

---

### Korak 4: Konfiguracija baze i API ključa

1. Napravi fajl `.env` u root folderu:
NEWS_API_KEY=news-api-key
> Zamijeni `news-api-key` sa tvojim stvarnim News API ključem.

2. SQLite baza se nalazi u projektu (`news.db`).  
   - Ako želiš pregledati bazu, otvori fajl `news.db` u **DB Browser for SQLite**.  

---

### Korak 5: Pokreni projekat

1. Odaberi **IIS Express** ili drugi browser target u Visual Studio.  
2. Pritisni **F5** ili klikni **Start Debugging**.  
3. Projekat će se otvoriti u pretraživaču.  

---

### Korak 6: Git update

Ako želiš preuzeti najnovije promjene sa repozitorija:  

```bash
git pull origin main
```
---
### Napomene

- Projekat koristi Razor Pages arhitekturu.
- Styling je definisan u wwwroot/css/site.css.
- Obavezno unesi svoj NEWS_API_KEY u .env fajl da bi API funkcionalnosti radile.
