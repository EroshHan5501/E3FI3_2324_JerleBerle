
-- CREATE USER IF NOT EXISTS 'chef'@'localhost' IDENTIFIED BY 'cook123';
-- GRANT ALL PRIVILEGES ON recipeapp TO 'chef'@'localhost' WITH GRANT OPTION;
DROP DATABASE IF EXISTS recipeapp;
CREATE DATABASE recipeapp;
USE recipeapp;

CREATE TABLE User(
    userId INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password TEXT NOT NULL,
    role INT NOT NULL
);

CREATE TABLE Recipe(
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    userId INTEGER REFERENCES User(userId)
);

CREATE TABLE Ingredient(
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE UnitOfMeasurement(
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE RiuRel (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    RecipeId INTEGER REFERENCES Recipe(Id),
    IngredientId Integer REFERENCES Ingredient(Id),
    Quantity INTEGER,
    UnitOfMeasurementId INTEGER REFERENCES UnitOfMeasurement(Id)
);

-- Beispiel-Einfügungen für die 200 gängigsten Zutaten
INSERT INTO Ingredient (name) VALUES
('Salz'),
('Pfeffer'),
('Zucker'),
('Olivenöl'),
('Butter'),
('Knoblauch'),
('Zwiebeln'),
('Mehl'),
('Milch'),
('Eier'),
('Tomaten'),
('Huhn'),
('Rindfleisch'),
('Schweinefleisch'),
('Lachs'),
('Thunfisch'),
('Karotten'),
('Sellerie'),
('Brokkoli'),
('Kartoffeln'),
('Paprika'),
('Gurken'),
('Salat'),
('Spinat'),
('Champignons'),
('Käse'),
('Reis'),
('Pasta'),
('Brot'),
('Rosmarin'),
('Oregano'),
('Basilikum'),
('Zitrone'),
('Limette'),
('Orangen'),
('Äpfel'),
('Banane'),
('Erdbeeren'),
('Blaukäse'),
('Gouda'),
('Parmesan'),
('Schlagsahne'),
('Joghurt'),
('Honig'),
('Essig'),
('Senf'),
('Ketchup'),
('Sojasauce'),
('Kreuzkümmel'),
('Currypulver'),
('Chili'),
('Schwarzer Tee'),
('Kaffee'),
('Wein'),
('Bier'),
('Zimt'),
('Vanille'),
('Nelken'),
('Nussmus'),
('Mandel'),
('Walnuss'),
('Pistazien'),
('Schokolade'),
('Vanillezucker'),
('Kakaopulver'),
('Puderzucker'),
('Zitronenschale'),
('Erdnussbutter'),
('Marmelade'),
('Worchester-Sauce'),
('Rosenwasser'),
('Kokosmilch'),
('Kokosraspeln'),
('Avocado'),
('Rettich'),
('Koriander'),
('Lauch'),
('Petersilie'),
('Dill'),
('Dijon-Senf'),
('Tabasco'),
('Majonäse'),
('Sahnesauce'),
('Sauerrahm'),
('Apfelessig'),
('Rotwein'),
('Weißwein'),
('Rum'),
('Whiskey'),
('Bourbon'),
('Wodka'),
('Gin'),
('Likör'),
('Sambuca'),
('Tequila'),
('Sake'),
('Cointreau'),
('Triple Sec'),
('Cognac'),
('Kirschlikör'),
('Amaretto'),
('Curacao'),
('Pfirsichschnaps'),
('Melonenlikör'),
('Erdbeerlikör'),
('Kokosnusslikör'),
('Schwarzer Johannisbeerlikör'),
('Anislikör'),
('Limettensaft'),
('Kirschsaft'),
('Grenadine'),
('Zitronenlimonade'),
('Ginger Ale'),
('Tonic Water'),
('Club Soda'),
('Cola'),
('Orangensaft'),
('Cranberrysaft'),
('Tomatensaft'),
('Eistee'),
('Wasser'),
('Eiswürfel'),
('Salzlake'),
('Gemüsebrühe'),
('Hühnerbrühe'),
('Fischsauce'),
('Miso-Paste'),
('Panko-Paniermehl'),
('Zuckermais'),
('Schwarze Bohnen'),
('Kidneybohnen'),
('Erbsen'),
('Linsen'),
('Weiße Bohnen'),
('Kichererbsen'),
('Artischocken'),
('Herz von Palm'),
('Okra'),
('Bohnen'),
('Auberginen'),
('Sauerkraut'),
('Avocadoöl'),
('Rapsöl'),
('Distelöl'),
('Maisöl'),
('Sonnenblumenöl'),
('Sesamöl'),
('Palmöl'),
('Mandelöl'),
('Walnussöl'),
('Haselnussöl'),
('Rüböl'),
('Kartoffelöl'),
('Kokosnussöl'),
('Petersilienwurzel'),
('Kohlrabi'),
('Topinambur'),
('Rosenkohl'),
('Chicorée'),
('Endivie'),
('Mangold'),
('Fenchel'),
('Radieschen'),
('Schnittlauch'),
('Kresse'),
('Basilikum'),
('Rosmarin'),
('Thymian'),
('Majoran'),
('Petersilie'),
('Liebstöckel'),
('Zitronengras'),
('Lorbeerblatt'),
('Korianderblätter'),
('Minze'),
('Kardamom'),
('Sternanis'),
('Kreuzkümmelsamen'),
('Fenchelsamen'),
('Koriandersamen'),
('Senfsamen'),
('Kurkuma'),
('Piment'),
('Majoran'),
('Kümmel'),
('Basilikum'),
('Schnittlauch'),
('Dill'),
('Petersilie'),
('Kerbel'),
('Bohnenkraut'),
('Wacholderbeeren'),
('Estragon'),
('Kreuzkümmelpulver'),
('Paprikapulver'),
('Chilipulver'),
('Ingwerpulver'),
('Zimtpulver'),
('Muskatnuss'),
('Piment'),
('Nelkenpulver'),
('Currypulver'),
('Vanilleextrakt'),
('Zitronenextrakt'),
('Orangenextrakt'),
('Mandelaroma'),
('Zitronenzeste'),
('Orangenzeste');

INSERT INTO User(username, email, password, role) VALUE 
('user1', 'user1@gmail.com', '+5l9XAHrz5Ytggs7Dn+L/ut/S9M3zINoLyr5DSUsIMXYV0S3xruU9IE59pCmHkrTF9YQfkMQ78AW2ShyZrUXKw==', 1),
('user2', 'user2@gmail.com', '+5l9XAHrz5Ytggs7Dn+L/ut/S9M3zINoLyr5DSUsIMXYV0S3xruU9IE59pCmHkrTF9YQfkMQ78AW2ShyZrUXKw==', 0);

-- Beispiel-Einfügungen für 50 gängige Rezepte in Europa
INSERT INTO Recipe (name, userId) VALUES
('Paella', 1),
('Lasagne', 1),
('Fish and Chips', 1),
('Moussaka', 1),
('Wiener Schnitzel', 1),
('Ratatouille', 1),
('Gulaschsuppe', 1),
('Borschtsch',1),
('Couscous',1),
('Tzatziki',1),
('Pierogi',1),
('Haggis',1),
('Köttbullar',1),
('Tiramisu',1),
('Sauerbraten',1),
('Bouillabaisse',1),
('Moules-frites',1),
('Coq au Vin',1),
('Goulash',2),
("Shepherd's Pie",2),
('Carbonara',2),
('Sarma',2),
('Baklava',2),
('Rösti',2),
('Kebab',2),
('Sarma',2),
('Tarte Tatin',2),
('Plov',2),
('Irish Stew',2),
('Ceviche',2),
('Linsensuppe',2),
('Cordon Bleu',2),
('Raclette',2),
('Pilaf',2),
('Gazpacho',2),
('Baba Ganoush',2),
('Maultaschen',2),
('Bruschetta',2),
('Spanakopita',2),
('Wassail',2),
('Himmel und Erde',2),
('Paprikash',2),
('Colcannon',2),
('Karelian Pasties',2),
('Pisto',2),
('Kedgeree',2),
('Makowiec',2),
('Blini',2),
('Tortilla Española',2);

INSERT INTO UnitOfMeasurement (name) VALUES
('kg'),
('g'),
('L'),
('ml'),
('Prise');

INSERT INTO RiuRel (RecipeID, IngredientId, Quantity, UnitOfMeasurementId) VALUES
(1,5, 200, 2),
(2,5, 200, 2),
(2,5, 200, 2),
(2,5, 200, 2),
(1,6, 300, 5);
