# Rent A Car Application
This is simple ASP.NET Core MVC application for renting cars. In this application, you can add, edit and remove Cars and Clients. 
The relationship beetween tha Car and tha Client is one to many, which means one Car can be rented by one Client, and one Client can rent many Cars.

# Architecture, principles, patterns and libraries used
1. Clean (Onion) architecture
2. Dependency injection
3. Repository pattern
4. Entity framework
5. AutoMapper
6. Select2
7. X.PagedList

# ToDo
1. Authentication
2. Unit Testing
3. Logging
4. And much more...

# Installation
1. Download the code
2. Set Web.MVC as startup project
3. In Package Manager Console type update-database -context RentACarDbContext
4. Run the application
