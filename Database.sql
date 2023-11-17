-- CREATE USER IF NOT EXISTS 'chef'@'localhost' IDENTIFIED BY 'cook123';
-- GRANT ALL PRIVILEGES ON recipeapp TO 'chef'@'localhost' WITH GRANT OPTION;

DROP DATABASE IF EXISTS recipeapp;
CREATE DATABASE recipeapp;
USE recipeapp;


CREATE TABLE Recipe(
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100)
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
    UOMId INTEGER REFERENCES UnitOfMeasurement(Id)
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


-- Beispiel-Einfügungen für 50 gängige Rezepte in Europa
INSERT INTO Recipe (name) VALUES
('Paella'),
('Lasagne'),
('Fish and Chips'),
('Moussaka'),
('Wiener Schnitzel'),
('Ratatouille'),
('Gulaschsuppe'),
('Borschtsch'),
('Couscous'),
('Tzatziki'),
('Pierogi'),
('Haggis'),
('Köttbullar'),
('Tiramisu'),
('Sauerbraten'),
('Bouillabaisse'),
('Moules-frites'),
('Coq au Vin'),
('Goulash'),
("Shepherd's Pie"),
('Carbonara'),
('Sarma'),
('Baklava'),
('Rösti'),
('Kebab'),
('Sarma'),
('Tarte Tatin'),
('Plov'),
('Irish Stew'),
('Ceviche'),
('Linsensuppe'),
('Cordon Bleu'),
('Raclette'),
('Pilaf'),
('Gazpacho'),
('Baba Ganoush'),
('Maultaschen'),
('Bruschetta'),
('Spanakopita'),
('Wassail'),
('Himmel und Erde'),
('Paprikash'),
('Colcannon'),
('Karelian Pasties'),
('Pisto'),
('Kedgeree'),
('Makowiec'),
('Blini'),
('Tortilla Española');

INSERT INTO RiuRel (RecipeID, IngredientId) VALUES
(1,5),
(1,6);