# Yolk_Pokemons
REST Api for the best Pokemon game ever

Setup instructions:
1. Read the database setup instructions in ".\Yolk Studo Backend Case Study" folder.
2. Change the connection string in ".\Yolk_Pokemon.Api\appsettings.json" to be in sync with your local postgres server.
3. Build the solution "Yolk_Pokemon.sln" to Release.
4. Run the ".exe" application in the release folder.
5. Allow the certificate.
6. Freely use the application with CRUD commands on your HTTPS(5001) server.

Working endpoints:
Create trainer - https://localhost:5001/api/trainers
Get trainer - https://localhost:5001/api/trainers/{trainerId}
Get all trainers - https://localhost:5001/api/trainers
Update trainer - https://localhost:5001/api/trainers/{trainerId}
Delete trainer - https://localhost:5001/api/trainers/{trainerId}
Create pokemon - https://localhost:5001/api/pokemon
Get pokemon - https://localhost:5001/api/pokemon/{pokemonId}
Get all pokemons - https://localhost:5001/api/pokemon
Add pokemon to trainer - https://localhost:5001/api/trainers/{trainerId}/pokemon

The application does not validate the bodies of the commands, co read the request obejcts to get know the structure.
Get all pokemons is able to be:
- filtered by type and region: /api/pokemon?type=fire
- sorted by name, level and catchAt: /api/pokemon?sortBy=level:desc



