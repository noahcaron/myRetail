The project myRetail.DataLayer uses Entity Framework. It uses the Products.mdf database in the Data folder. The associated test project has tests to see that all the CRUD operations are working correctly to the database. The myRetail.RestService project uses Web API and has a controller called ProductsController.cs that has operations for inserting, updated, and deleting products via the data layer. It also has a test project to test the controller methods.

======
MyRetail-SPA is an Angular.js single Page Application for managing the list of products via the REST service.
