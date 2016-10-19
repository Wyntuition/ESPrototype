# Service prototype

## Create the database 

1. Run: 

    `dotnet ef database update`

1. Add an item to the database. You can either use the following curl command, or your favorite tool like Postman:

    `
    curl -H "Content-Type: application/json" -X POST -d '{"title":"I Was Posted"}' http://localhost:5000/api/articles
    `