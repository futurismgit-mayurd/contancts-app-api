# contancts-app-api

Clone URL "https://github.com/pavanfuturism/contancts-app-api.git"

Version : .Net Core 6.0
VS Studio: VS 2022 community edition

Json File Path: ContactsApp.API/Output/contacts.json

Installation
1. Download the VS 2022 Community Edition from "https://visualstudio.microsoft.com/free-developer-offers/"
2. Install VS 2022
3. After installation go to the project directory.
4. Open the "ContactsApp.sln" file and press Ctrl+ALT+B to build the solution
5. After successful build, hit F5 to run the API.
6. Copy the URL from the browser and replace it in Enviornment/environment.ts file in baseurl key.

        # Design Architecture
i. ContactsApp.API – this is used to write all controllers, startup file (for dependency injections, add database connection etc.) & helper files. I have used exception middleware inside Helper folder for handling global level exceptions.
ii. ContactsApp.Common – this project is used to have all common methods,extension methods, string constants etc.
iii. ContactsApp.Entity – this project is used to define all the DTO objects required for the API’s
iv. ContactsApp.Repository – this project is used to define generic repositories when working with Database
v. ContactsApp.Service – this project is used to define all interfaces  used by controllers for Add/Update/Delete/Get methods. This layer communicates with the Database for CRUD operations.

