# CarSales

Website which simulates users importing, selling and reviewing vehicles. Uses credits as a currency to simulate transactions.

INSTALLATION GUIDE

Option 1: The development environment uses local SQL server and a redis docker container on port 5002.
The migration will be applied on startup.

Option 2: The staging environment is set up by running "docker-compose up --build" in the project's main directory.
The migration will be applied on startup.
The app will be accessible at localhost:80
Use "docker-compose down --volumes" when removing.

There are 5 types of users - Admin, Importer, Owner, Reviewer, Salesman.

- Admin:
 + Approves or denies role requests sent by other users
- Importer:
 + Imports vehicles
- Owner:
 + Buys and creates offers to buy for vehicles on sale
 + Default role granted on registration
- Reviewer:
 + Reviews vehicles for sale and gets credits for doing so
 + Can only review if vehicle salesman orders review
 + Affects vehicle rating with his review
- Salesman:
 + Buys imported vehicles and can sell them
 + Can sell his own vehicles
 + Can request reviews for his vehicles on sale
 + Accepts or declines offers from other users trying to buy his vehicles


