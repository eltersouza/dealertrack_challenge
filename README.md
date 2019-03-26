## dealertrack_challenge
This code has been made for Dealertrack code challenge.

Originally it was to be developed in Angular 7, after I read all the information provided by the company, I started to plan all what I was going to do. Design a MVP project and extra features in case I have finished in time.

After I finish I was reading again the document provided (for double-check) when I realized I read Internet Explorer and in someway I associated with Edge. When I tested in Internet Explorer, not even the "loading" appeared. I had to code all the UI again.

In order to avoid any possibility of not being compliant with Internet Explorer I went old school and I made it in: .net core 2.1, C# MVC, jQuery and Bootstrap.

Prerequisite to use: .Net Core 2.1, SQL Server LocalDB.

How to use

- Just open Visual Studio, Rebuild solution this will download all NuGet packages used.

- This project is using EF core with migrations, for persistence of the csv parsing, you have to run the Update-Database command, instructions below:
  - This will create a database in your LocalDB for persist the csv parsing. 
  - Go in menu Tools > NuGet package manager > package manager console.
    - In the console, make sure the **Default project** is "_Presentation\Dealertrack.UI_"
    - type the following command: Update-Database

**IMPORTANT**: As I don't know if you want the application to persist, I coded some purposely fallbacks, so you can still import a csv and visualize it without persistence.

**NOTE**: If you decide to not use persistence, the loading at the beginning is going to take longer. Because of the EF Core timeout.



# Architecture

I created the solution to be a DDD Microservice-oriented solution. I didn't use Mediator and CQRS, though. It would be over-engineering, even for a experience evaluation code.

Thank you, I hope it could 