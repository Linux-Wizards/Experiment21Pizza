# Experiment21Pizza

Pizza management web application written in Blazor Server.

# Repository structure

Go to [Source](https://github.com/Linux-Wizards/Experiment21Pizza/tree/main/Source) to see the source code of Experiment21Pizza. Go to [Prototypes](https://github.com/Linux-Wizards/Experiment21Pizza/tree/main/Prototypes) to see the previous attempts at creating a web application in C#.

# What does the application offer?

It simplifies management of pizza orders.

Orders go through three stages:
- placing new orders
- preparing orders
- delivering orders

The application offers authentication and authorization. Pages can be restristed to users with a certain role.

There is an "Admin" page to manage users and their roles.

# Administration

Default admin credentials:\
Login: admin@experiment21.com\
Password: SuperStrongAdminPassword

# Potentially asked questions

## How are orders refreshed?

They are refreshed every 10 seconds (using a timer).

This does not work perfectly - it adds new orders to the list, but it doesn't update the status of existing orders.

## How do I restrict placing orders to users with a certain role?

There is a line commented out at the beginning of "PlaceOrder.razor":
```
@* @attribute [Authorize(Roles = "Administrator, PlaceOrders")] *@
```

Remove the comment ("@\*" and "\*@") to permit only users with "Administrator" or "PlaceOrders" role to place orders.

It works similarly for other pages.

## Where does the name "Experiment21" come from.

It's related to age of one of the programmers behind the application, [DarkoGNU](https://github.com/DarkoGNU). It expresed his hope to finish the application before his 21st birthday in November 2023 (he was successful).

## How do I update the database after an application update?

Open a terminal and make sure that you're in the folder that contains the source code ([Experiment21Pizza/Source
/Experiment21](https://github.com/Linux-Wizards/Experiment21Pizza/tree/main/Source/Experiment21)).

Run the following command:

```
dotnet ef database update
```

## How do I create a new database?

The same code suggested in the question above should do the job.

## How do I create a migration to update the database after changing the models?

Open a terminal and make sure that you're in the folder that contains the source code ([Experiment21Pizza/Source
/Experiment21](https://github.com/Linux-Wizards/Experiment21Pizza/tree/main/Source/Experiment21)).

Run the following command:

```
dotnet ef migrations add DescriptiveMigrationName
```
