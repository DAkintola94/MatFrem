# Private project personally created by me

Matfrem is a fullstack application created with MVC .NET.

## Setting up the application

To run the application

1. install Docker, and have it running.
2. Open a terminal or CLI and navigate to the root folder of the cloned repository. Make sure you are in the same directory as the docker-compose.yml file.
3. Once you are in the correct directory, run the following command to build and start the containers:
   **docker-compose up --build**

### Fuctions in the application

The application let you register user and log-in afterward.

### Logging/Authentication

The application also let you log-in with email and password that are hard seeded into the database with code first approach.

Seeded system_administrator:
Email: sysadmin@test.com
Password: Testingtesting1234

Seeded driver:
Email: driver@test.com
Password: Drivertesting1234

Seeded customer:
Email: customer@test.com
Password: Customertesting1234

### # Private project personally created by me

Matfrem is a fullstack application created with MVC .NET.

## Setting up the application

To run the application

1. install or have Docker running.
2. Clone the repository and simply run it (this can be done by clicking on the sln file).
3. Open Visual studio or Rider.
4. Change the application setting to docker compose, then run. You can also run it without using docker compose.

### Fuctions in the application

The application let you register user and log-in afterward.

### Logging/Authentication

The application also let you log-in with email and password that are hard seeded into the database with code first approach.

Seeded system_administrator:
Email: sysadmin@test.com
Password: Testingtesting1234

Seeded driver:
Email: driver@test.com
Password: Drivertesting1234

Seeded customer:
Email: customer@test.com
Password: Customertesting1234

### Systemadministrator

Systemadministrator can do mostly everything, such as creating a product, creating and deleting user, etc.

### MVC

- MVC (short for Model, View and Controller) is a framework for software development that separates data, logic and presentation.

* Model: Represent the appication's data using classes.
* Controller: Handles user requests by communicating with the model and the view.
* View: Presents the data to the users.

### Entity framework

- Entity framework is an ORB(Object-Relational Mapper) that allows you to use .NET objects when working the the databases.
- In the application, there is a class called AppDbContext that contains code to setup entity and communicate with the database.
- The application uses ORM because its much simple and easy than going to the database for every changes made

### Domain Models

- domain models define the application's data structure. These are the entities represented in the database.

### View Model

- A view model is a model designed specifically to be used in views, combining data from multiple models into a single object.
- In the application, View Models are used to format and structure data before presenting it to the user.

### Database

- The database run in its own Docker container and its initialized using Docker Compose.
