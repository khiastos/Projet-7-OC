# Projet 7 : Rendez votre back-end .NET plus flexible avec une API REST

Ce projet est la création d'une **API**, avec des **JWT** pour l'authentification, **Identity** pour la connexion et de faire des **tests unitaires** sur les controlleurs. 
L'objectif était de créer pour la première fois une API, pour une **application de trading**, Findexium.

---
## Outils et technologies utilisés

- **Visual Studio 2022**
- **C# / ASP.NET Core**
- **Swagger**
- **Entity Framework**
- **JWT**
- **Identity**
- **xUnit, Moq, FluentAssertions**
- **ILogger**
  
---
## L'API

- **Base de données** : SQL Server via EF Core (approche Code-First)
- **Repository pattern (générique)** : Création d'un GenericRepository et d'une interface IGenericRepository, pour éviter plusieurs fichiers repository qui sont similaires
- **Endpoint protégé (écriture + lecture)** : Chaque endpoint de modification est protégé par [Authorize(Roles="Admin")]
- **Identity** : Gestion des droits, connexion et création de compte (seeding pour la création de rôles)
- **JSON Web Tokens (JWT)** : Endpoint d’authentification (login) qui valide l’utilisateur puis émet un JWT signé après s'être connecté via Identity
- **ILogger** : Des logs sont mis en place à chaque appel sur les endpoints, avec la date, l'heure, la route et l'utilisateur concerné (IN/EX/OUT)
- **DTO** : Mise en place de DTOs pour ne faire transiter que les informations essentielles, ainsi que des mappers correspondant (ToDto, ToEntity, ApplyTo)
- **CRUD** : Chaque controlleur pour chacune de mes entités est basé sous le pattern CRUD, en rajoutant un GetAll() pour chaque

---
## Les tests unitaires

- Les tests sont effectués sur les controlleurs qui valident les méthodes CRUD en fonction de la réponse HTTP, soit 10 tests chacun (50 au total) :
 
1. GetAll() → 200 OK
2. GetById() → 200 OK / 404 NotFound
3. Create() → 201 Created / 400 BadRequest
4. Update() → 200 OK / 404 NotFound / 400 BadRequest
5. Delete() → 204 NoContent / 404 NotFound

---
## Installation  

1. `git clone https://github.com/khiastos/Projet-7-OC.git`  
2. Ouvrir la solution dans Visual Studio  
3. Modifier la chaîne de connexion dans `appsettings.json` en mettant celle de votre base en local et le nom de votre choix
4. Lancer `Update-Database` dans la Console du Gestionnaire de Package
5. Exécuter le projet

Si besoin, vous pouvez supprimer les migrations déjà existantes pour éviter les potentiels conflits, il faudra cependant exécuter `Add-Migration (+ Nom de votre choix)` pour créer une nouvelle migration, puis effectuer `Update-Database`.

---
## Lancement

L'adresse mail du compte admin (créé via seeding) est `admin@gmail.com`
Vous aurez juste à vous faire un compte avec cette adresse mail et à relancer l'application pour pouvoir tester l'API côté admin

