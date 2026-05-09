🚧 This project is currently under development (Database design completed, backend and frontend in progress)

# 🎓 University Management System (UMS)

A full-stack University Management System built using Clean Architecture principles.

---

## 🚀 Technologies Used

### Backend (Planned / In Progress)
- ASP.NET Core Web API
- C#
- Entity Framework Core
- ADO.NET

### Frontend (Planned)
- HTML / CSS / JS / Angular

### Database
- Microsoft SQL Server
- Stored Procedures
- Views
- Triggers

---

## 🏗️ Architecture

This project follows **Clean Architecture**:

- API Layer (Presentation)
- Application Layer (Business Logic)
- Domain Layer (Entities)
- Infrastructure Layer (Data Access)

---

## 🗄️ Database Structure

## 🗄️ Database Structure

The database is designed using a modular and maintainable approach, where each database component is separated into its own script file for better version control and scalability.

### 📁 Folder Structure

- **Tables/**
  Contains all table creation scripts. Each table is defined in a separate `.sql` file.

- **StoredProcedures/**
  Contains all stored procedure scripts used for business logic and database operations.

- **Views/**
  Contains SQL views used for data abstraction and reporting.

- **Functions/**
  Contains scalar and table-valued functions used in queries and calculations.

- **Indexes/**
  Contains performance optimization scripts (indexes) for tables.

- **Constraints/**
  Contains database constraints such as:
  - Primary Keys
  - Foreign Keys
  - Unique Constraints
  - Check Constraints

- **Schema/**
  Contains the main database initialization script (`UMS_Database_Schema.sql`) which creates the database and basic configuration.

Database/
│
├── Schema/
├── Tables/
├── StoredProcedures/
├── Views/
├── Functions/
├── Indexes/
└── Constraints/


---

## ⚙️ How to Run Project

### 1. Backend Setup
- Open the solution in Visual Studio
- Restore NuGet packages
- Set database connection string in `appsettings.json`
- Run the API project

### 2. Database
- Run `UMS_Database_Schema.sql`
- Then run tables scripts
- Then stored procedures

---

## 📌 Features

- User Management
- Student Management
- Doctor Management
- Fees System
- Academic Records
- Role-based Authorization

## 🎯 Project Goal

The goal of this project is to build a scalable University Management System using Clean Architecture principles, focusing on:

- Separation of concerns
- Maintainable database design
- Scalable backend structure
- Real-world enterprise-level structure

---

## 👨‍💻 Developer

Laith Darwish  
Management Information Systems Graduate  
.NET & Full Stack Developer

---

## 📈 Future Improvements

- Frontend Angular UI upgrade
- JWT Authentication
- Docker support
- CI/CD pipeline

---
