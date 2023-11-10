USE recipeAppDb;

INSERT INTO User(username, email, password, role) VALUES('admin', 'admin.test@gmail.com', '+5l9XAHrz5Ytggs7Dn+L/ut/S9M3zINoLyr5DSUsIMXYV0S3xruU9IE59pCmHkrTF9YQfkMQ78AW2ShyZrUXKw==', 1);
INSERT INTO User(username, email, password, role) VALUES('test1', 'test1.test@gmail.com', '+5l9XAHrz5Ytggs7Dn+L/ut/S9M3zINoLyr5DSUsIMXYV0S3xruU9IE59pCmHkrTF9YQfkMQ78AW2ShyZrUXKw==', 0);
INSERT INTO User(username, email, password, role) VALUES('test2', 'test2.test@gmail.com', '+5l9XAHrz5Ytggs7Dn+L/ut/S9M3zINoLyr5DSUsIMXYV0S3xruU9IE59pCmHkrTF9YQfkMQ78AW2ShyZrUXKw==', 0);
INSERT INTO User(username, email, password, role) VALUES('test3', 'test3.test@gmail.com', '+5l9XAHrz5Ytggs7Dn+L/ut/S9M3zINoLyr5DSUsIMXYV0S3xruU9IE59pCmHkrTF9YQfkMQ78AW2ShyZrUXKw==', 0);

INSERT INTO Recipe(title, createdAt, description, imageUrl, userId) VALUES('Pasta', '2023-11-02T02:00:00', 'Nice try', '', 1);
INSERT INTO Recipe(title, createdAt, description, imageUrl, userId) VALUES('Pizza', '2023-11-02T02:00:00', 'Nice try', '', 2);
INSERT INTO Recipe(title, createdAt, description, imageUrl, userId) VALUES('Schnitzel', '2023-11-02T02:00:00', 'Nice try', '', 3);
INSERT INTO Recipe(title, createdAt, description, imageUrl, userId) VALUES('Burger', '2023-11-02T02:00:00', 'Nice try', '', 4);

INSERT INTO Unit(name) VALUES('ml');
INSERT INTO Unit(name) VALUES('kg');
INSERT INTO Unit(name) VALUES('g');
INSERT INTO Unit(name) VALUES('prise');
INSERT INTO Unit(name) VALUES('stueck');

INSERT INTO Amount(amountValue) VALUES(1);
INSERT INTO Amount(amountValue) VALUES(2);
INSERT INTO Amount(amountValue) VALUES(5);
INSERT INTO Amount(amountValue) VALUES(10);
INSERT INTO Amount(amountValue) VALUES(50);
INSERT INTO Amount(amountValue) VALUES(20);
INSERT INTO Amount(amountValue) VALUES(100);
INSERT INTO Amount(amountValue) VALUES(200);

INSERT INTO Ingredient(name, amountId, unitId) VALUES('Tomaten', 2, 5);
INSERT INTO Ingredient(name, amountId, unitId) VALUES('Salz', 1, 4);
INSERT INTO Ingredient(name, amountId, unitId) VALUES('Nudeln', 1, 2);

INSERT INTO IngredientRecipe(recipeId, ingredientId) VALUES(1, 1);
INSERT INTO IngredientRecipe(recipeId, ingredientId) VALUES(1, 2);
INSERT INTO IngredientRecipe(recipeId, ingredientId) VALUES(1, 3);
