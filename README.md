# Solution to Preqin

Hello reviewer, 
Thank you for taking  your time to look through this code.
It's not my best but i'll consider it a product that can be released.
I try to do what i can within 5/6 hours.
P.S: i referenced past projects and use template generators to move quickly.

The solution has 4 major projects.
## ETL 
This is the python project that extract the sample data and put it in our database
## frontend
This is the web application built using React, typescript, tailwindcss and vite.
It communicates with the Web Api
## Web API
An API built using C# .NET 9. it has an healthcheck endpoint as well as endpoints for the Investors and commitments data access.
## compose.yml
I'm a big fan of containerisation so i try to put the projects in containers and run them via that. I love the portability and "Works on My Machine" problem that it solved. It's also good for CI/CD, version control and dependency management

Sadly, i failed to containerise the frontend and python app within the time constraint. It's not impossible just ran out of time. Priority was on the API and unit tests.

## Architecture

Web App (frontend) --talks to--> API (Preqin.InvestorsApi) --> Database(compose.yml)


# Solution

To run:

Open your terminal, browse to the directory of this file and run these commands

> docker compose up -d

That should start the web api, postgres database and postgres db explorer

> cd ETL
> Follow the README instructions there

That should create the tables and insert data into the DB

> cd .. && cd frontend
> cp .env.example .env
> npm install
> npm start

That should start the web application where you should be able to see investors and their commitments

[http://localhost:5173/](http://localhost:5173/)