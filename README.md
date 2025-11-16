# 🧮 Premium Calculator (Angular + .NET Backend)

## 📌 Project Overview
This project is a **Premium Calculator UI** built with Angular.  
It allows a member to input personal details and calculate their **monthly premium** based on occupation rating factors.

The calculation formula is:

Death Premium = (Death Cover Amount * Occupation Rating Factor * Age) / 1000 * 12

---

## 🎯 Features
- Angular UI form with mandatory fields:
  - Name
  - Age Next Birthday
  - Date of Birth (mm/YYYY)
  - Occupation (dropdown)
  - Death – Sum Insured
- Occupation dropdown triggers **auto-calculation** of premium.
- Occupation ratings and factors are pre-defined.
- Premium result displayed dynamically on the screen.
- Clean architecture with **Reactive Forms** and **Service Layer** for calculation logic.

---

## 🛠 Tech Stack
- **Frontend**: Angular 18+, SCSS
- **Backend (optional)**: ASP.NET Core Web API (for persistence)
- **Database (design only)**: SQL Server / any RDBMS

---

## 📂 Project Structure


---

## ⚙️ Setup Instructions

### 1. Prerequisites
- Node.js (>= 18.19)
- Angular CLI (latest)
- npm

### 2. Install Dependencies
```bash
npm install

Assumptions & Clarifications
All input fields are mandatory.

Occupation dropdown values are predefined and mapped to rating factors.

Premium calculation is client-side; backend integration is optional.

Date of Birth is captured in mm/YYYY format.

No authentication or user management is included in this version.

Database schema is provided for reference; scripts are not required.

