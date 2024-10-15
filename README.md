#  Брижак Андрей
#  Тестовое задание для С# разработчика;


# Venue Management API

This is an ASP.NET Core API project designed to manage entities such as **Venues**, **Tags**, and **Categories**. The API interacts with an MS SQL Server database and provides full CRUD operations for each entity, using **Entity Framework**, **Swagger** for API documentation, and **AutoMapper** for efficient mapping between models and DTOs.

## Project Overview

This project implements an API for managing venues (places such as restaurants, entertainment spots, and service providers) with related categories and tags. It uses ASP.NET Core for backend logic and MS SQL Server for data persistence.

### Entity Relationships

- **Category**:
  - Each category can have multiple venues.
  - Each venue can belong to only one category.
  
- **Venue**:
  - Represents a business or place.
  - Each venue belongs to one category.
  - A venue can be associated with multiple tags.

- **Tag**:
  - Represents labels or keywords associated with venues (e.g., "Outdoor Seating", "Live Music").
  - Each tag can be linked to multiple venues.
  - A venue can have multiple tags.

### Technologies Used

- **ASP.NET Core**: The API framework for handling HTTP requests and responses.
- **Entity Framework Core**: The Object-Relational Mapper (ORM) used for database interactions, with MS SQL Server as the database.
- **MS SQL Server**: The database where all entity data is stored.
- **AutoMapper**: Used for mapping between domain models and Data Transfer Objects (DTOs) to simplify data handling.
- **Swagger**: Provides a UI for exploring and testing the API endpoints.

### API Endpoints

The following entities have CRUD operations implemented:

- **Categories**:
  - Create, Read, Update, and Delete categories.
  - A category can contain multiple venues, but each venue belongs to only one category.

- **Venues**:
  - Create, Read, Update, and Delete venues.
  - Venues are linked to categories and can be associated with multiple tags.

- **Tags**:
  - Create, Read, Update, and Delete tags.
  - Tags can be linked to multiple venues.

