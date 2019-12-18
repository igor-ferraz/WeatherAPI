# :cloud: WeatherAPI
It's an Web API (using ASP.NET Core) that tracks temperatures of registered cities. <br>
I built this application to demonstrate how to use Domain-Driven Design architecture with a practical approach. <br>
The Swagger package is used to make the API visualization easier as well as provide an optional way to test it without another software.

## :office: Architecture
This project is using the __Repository__ concept as the only way to manipulate the database.<br>
Assuming that you already know what is DDD and what each layer have to do, there is a short description for each project:

- __Weather.Infrastructure__: here you can find the Repositories and the service to handle HTTP requests.<br>
- __Weather.API__: this project manage the dependency injection and contains our endpoints (controllers).<br>
- __Weather.Application__: all the business logic is here, inside our three services.<br>
- __Weather.Domain__: an usual domain, including models, data contracts and interfaces (for services and repositories).<br>
- __Weather.Common__: this project only exists to share the RemoveDiacritics string extension to the others projects.

#### If you don't know much about DDD, here is a super simplified explanation (for this project):

- The __Presentation Layer__ is responsible to manage the dependency injection. Here you can easily change the modules that the others projects will use.
- The __Infrastructure Layer__ doesn't know anything about the business or about the others projects: it'll only manipulate the database based on Domain models.
- The __Application Layer__ contains all the business logic and has no idea how to connect to the database or where it is. In other words, it'll use the Repositories from Infrastructure Layer to retrieve, update or save data.
- The __Domain__ contains all the definitions that other projects needs to talk each other. Technically, the models, data contracts and the interfaces live here.

## :books: Requirements
To run this project properly it's necessary to create the database as below.<br>

<table>
  <thead>
    <tr>
      <th>Cities</th>
      <th>Temperatures</th>
    </tr>
  </thead>
  <tbody>
    <tr><td>

|Column|Type|
|------|----|
|id|int|
|name|varchar|
|state|char(2)|

</td>
<td>

|Column|Type|
|------|----|
|id|int|
|degrees|decimal|
|created|datetime|
|city_id|int|

</td></tr>
</tbody>
</table>

<details>
<summary>SQL for database creation</summary>

```sql
-- SQL Server
CREATE DATABASE Weather;

-- Run the CREATE DATABASE command separated
-- After that you can run all the code bellow
CREATE TABLE cities (
   id INT PRIMARY KEY IDENTITY(1,1),
   name VARCHAR(100) NOT NULL,
   state CHAR(2) NOT NULL
);

CREATE TABLE temperatures (
   id INT PRIMARY KEY IDENTITY(1,1),
   degrees DECIMAL(5,2) NOT NULL,
   created DATETIME NOT NULL,
   city_id INT NOT NULL,
   FOREIGN KEY(city_id) REFERENCES cities(id)
);
```
</details>

__Important__: it's following the _brazilian pattern_:
- each city is localized in only one state.
- each state has exactly two characters as unique identification code.

#
Do you have any suggestion? Feel free to improve this sample! <br>
Hope this project can help someone! Enjoy it! :smile:
