# Service prototype

## Create the database 

1. Run: 

    `dotnet ef database update`

1. Add an item to the database. You can either use the following curl command, or your favorite tool like Postman:

    `
    curl -H "Content-Type: application/json" -X POST -d '{"lastname":"Posted"}' http://localhost:5000/api/applicant 
    `

## Run the application in ASP.NET Core container and database container

1. If you haven't built yet, run: 

    `docker-compose build`

1. To start the containers, run: 

    `docker-compose up -d`

## NOTE: To run a web container only

1. If you haven't built it, run:

    `docker build -t ncarb:eesaRun . `

1. Then run: 

    `docker run -it -p 5000:5000 -v $(pwd):/eesa -t ncarb:eesa`