DROP DATABASE IF EXISTS recipeAppDb;
CREATE DATABASE recipeAppDb;
USE recipeAppDb;

CREATE TABLE User(
    userId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password TEXT NOT NULL,
    role INT NOT NULL
);

CREATE TABLE Recipe(
    recipeId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(100) NOT NULL, 
    createdAt DATETIME NOT NULL,
    description TEXT NOT NULL,
    imageUrl TEXT NOT NULL,
    userId INTEGER REFERENCES User(userId)
);

CREATE TABLE Amount(
    amountId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    amountValue INTEGER NOT NULL
);

CREATE TABLE Unit(
    unitId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100)
);

CREATE TABLE Ingredient (
    ingredientId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    amountId INTEGER REFERENCES Amount(amountId),
    unitId INTEGER REFERENCES Unit(unitId)
);

CREATE TABLE ingredientrecipe(
    recipeIngredientId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    recipeId INTEGER REFERENCES Recipe(recipeId),
    ingredientId INTEGER REFERENCES Ingredient(ingredientId)
);