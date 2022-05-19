DROP DATABASE IF EXISTS TextAdventure;
CREATE DATABASE TextAdventure;
USE TextAdventure;


CREATE TABLE Room (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(50),
  Preposition VARCHAR(10),
  Description VARCHAR(200)
);

CREATE TABLE Door (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR (50),
  OpenText VARCHAR (200),
  Open INT,
  Secret INT,
  Room1Id_FK INT,
  FOREIGN KEY (Room1Id_FK) REFERENCES Room (Id),
  Room2Id_FK INT,
  FOREIGN KEY (Room2Id_FK) REFERENCES Room (Id)
);

CREATE TABLE ItemTyp (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR (20)
);

CREATE TABLE LootItem (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR (50),
  Description VARCHAR(200),
  InteractText VARCHAR(200),
  PackText VARCHAR(200),
  Open INT,
  Secret INT,
  Fix INT,
  RoomId_FK INT,
  ItemTyp_FK INT,
  FOREIGN KEY (RoomId_FK) REFERENCES Room (Id),
  FOREIGN KEY (ItemTyp_FK) REFERENCES ItemTyp (Id)
  
);

CREATE TABLE DecoItem (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR (50),
  Description VARCHAR(200),
  InteractText VARCHAR(200),
  PackText VARCHAR(200),
  MaxNumber INT
  );


INSERT INTO Room
  (Name, Preposition, Description)
VALUES
  ("Eingangshalle", "in der", "Es handelt sich um eine große Halle mit Steinfliesen und vielen Säulen."),
  ("Küche", "in der", "Groß, hell, und es riecht nach Essen und Rauch."),
  ("Folterkammer", "in der", "Ein unheimlicher Ort mit verdächtigen Flecken auf dem Boden und der Wand."),
  ("Geheimversteck", "im", "Eigentlich garnicht hier."),
  ("Schlafzimmer", "im", "Eine Festung der Träume."),
  ("Thronsaal", "im", "Ein roter Teppich führt zum Thron. Die Wände sind mit Teppichen geschmückt."),
  ("Turmzimmer", "im", "Die Jungfer in Nöten wartet auf euch."),
  ("Abstellkammer", "in der", "Klein und verstaubt."),
  ("Teesalon", "im", "Ein kleines emütliches Zimmer mit vielen exotischen Pflanzen."),
  ("Speisesaal", "im", "Ein großer festlich geschmückter Saal."),
  ("Speisekammer", "in der", "Klein, aber voll mit gut richenden Speisen."),
  ("Gang", "im", "Er ist spärlich beleuchtet mit vielen Türen. Das Ende kannst du nicht sehen."),
  ("Freien", "im", "Endlich hast du es ins Freie geschaft.")
  ;


INSERT INTO Door
  (Name, OpenText, Open, Secret, Room1Id_FK, Room2Id_FK)
VALUES
  ("eine einfache Holztür", "Die Tür öffnet sich quietschend.", 1, 0, 1, 12),
  ("eine viel benutzte Holztür", "Die Tür öffnet sich erstaunlich leicht.", 1, 0, 1, 2),
  ("eine unscheinbare Holztür", "Die Tür öffnet sich knarrend.", 1, 0, 2, 10),
  ("eine alte, dreckige Holztür", "Die Tür öffnet sich knarrend und quietschend.", 1, 0, 2, 8),
  ("eine alte Holztür", "Die Tür öffnet sich quietschend. Hinter ihr befindet sich eine schmale Wendeltreppe.", 1, 0, 12, 7),
  ("eine alte Holztür", "Die Tür öffnet sich quietschend.", 1, 0, 1, 2),
  ("ein prächtiges Tor", "Das Tor öffnet sich nur sehr schwer.", 1, 0, 1, 6),
  ("eine Stahltür", "Die Tür öffnet sich quietschend.", 1, 0, 12, 3),
  ("eine mit Blumen verzierte Holztür", "Die Tür öffnet sich erstaunlich leicht.", 1, 0, 12, 9),
  ("eine verzierte Doppeltür aus Holz", "Die Tür öffnet sich nur schwer.", 1, 0, 12, 10),
  ("eine verzierte Holztür", "Die Tür öffnet sich erstaunlich leicht.", 1, 0, NULL, NULL),
  ("eine Gittertür", "Die Tür öffnet sich quietschend. Dahinter befindt sich ein langer Gang mit einem kleinen Lichtfleck am Ende.", 0, 1, 3, 13),
  ("ein Gemälde von einem Ritter auf einem prächtigen Pferd", "Das gemälde lässt sich zur Seite drehen. Dahinter befindet sich ein dunkler nierieger Gang.", 1, 1, 4, 6),
  ("ein alter Kleiderschrank", "Die Hinterwand des Schrankes fehlt. Dort befindet sich ein dunkler langer Gang.", 1, 1, 4, 5),
  ("eine Falltür", "Unter der Falltür siehst du ein schwarzes Loch. An der Seite kannst du eine schmale Leiter entdecken.", 1, 1, NULL, NULL)
  ;

INSERT INTO ItemTyp
	(Name)
VALUES
	("key"),
	("weapon"),
	("money"),
	("book")
    ;
 
INSERT INTO LootItem
	(Name, Description, InteractText, PackText, Open, Secret, Fix, ItemTyp_FK, RoomId_FK)
VALUES
	("ein großer Schlüssel", "Er ist groß, alt und goldend.", "Du brachst erst ein Schloss, um ihn zu benutzen.", "Bestimmt brauchst du den Schlüssel noch. Du packst ihn ein.", 1, 1, 0, 1, 5),
	("ein kleiner Schlüssel", "Er ist klein und goldend.", "Du brachst erst ein Schloss, um ihn zu benutzen.", "Bestimmt brauchst du den Schlüssel noch. Du packst ihn ein.", 1, 1, 0, 1, 5),
	("ein kleiner Schlüssel", "Er ist klein und silbern.", "Du brachst erst ein Schloss, um ihn zu benutzen.", "Bestimmt brauchst du den Schlüssel noch. Du packst ihn ein.", 1, 1, 0, 1, 8),
	("ein alter Schlüssel", "Er ist alt und silbern.", "Du brachst erst ein Schloss, um ihn zu benutzen.", "Bestimmt brauchst du den Schlüssel noch. Du packst ihn ein.", 1, 1, 0, 1, 9),
	("ein altes Schwert", "Es ist kaputt.", "Mit einem kaputten Schwert kann man nichts machen.", "Was willst du mit dem kapputem Schwert. Nach kurzem überlegen, legst du es zurück.", 1, 1, 1, 2, 8),
	("ein altes Schwert", "Es ist ein reines Dekoschwert mit stumpfer Klinge.", "Du schwingst das Schwert etwas ungeschickt durch die Luft.", "Du packst es ein. Vielleicht kannst du es verkaufen, wenn du es jemals hier heraus kommst.", 1, 1, 0, 2, 6),
	("ein altes Schwert", "Es ist alt, aber man kann es benutzen.", "Du schwingst das Schwert etwas ungeschickt durch die Luft.", "Du packst es ein. Vielleicht kannst du dich damit verteidigen.", 1, 1, 0, 2, 1),
	("eine alte Tasche", "Sie sieht ziemlich alt und runtergekommen aus.", "Du schaust in die Tasche. Sie ist leer.", "Du packst sie ein, obwohl du keine Idee hast, wozu du sie brachen könntest.", 1, 1, 0, NULL, 2),
    ("eine kleine Münze", "Sie ist klein und goldend.", "Du schaust, wie es mit deinem Glück steht. Bei Zahl kommst du hier heraus, bei Kopf nicht. Du wirfst die Münze ... Kopf", "Du packst sie ein. Man kann nie genug Geld haben.", 1, 1, 0, 3, 5),
    ("eine große Truhe", "Sie sieht ziemlich groß und schwer aus.", "Du öffnest sie und schaust hinein.", "Du versuchst sie hoch zu heben. Dein Rücken merkt an, dass das eine deiner schlechteren Ideen war.", 0, 1, 1, NULL, 4),
    ("eine große Truhe", "Sie sieht ziemlich groß und schwer aus.", "Du öffnest sie und schaust hinein.", "Du versuchst sie hoch zu heben. Dein Rücken merkt an, dass das eine deiner schlechteren Ideen war.", 0, 1, 1, NULL, 5),
    ("eine große Truhe", "Sie sieht ziemlich groß und schwer aus.", "Du öffnest sie und schaust hinein.", "Du versuchst sie hoch zu heben. Dein Rücken merkt an, dass das eine deiner schlechteren Ideen war.", 0, 1, 1, NULL, 7),
    ("eine große Truhe", "Sie sieht ziemlich groß und schwer aus.", "Du öffnest sie und schaust hinein.", "Du versuchst sie hoch zu heben. Dein Rücken merkt an, dass das eine deiner schlechteren Ideen war.", 0, 1, 1, NULL, 11),
    ("ein großer Kleiderschrank", "Er ist groß und alt.",  "Du öffnest sie und schaust hinein.", "Sie ist zu groß für dein Rucksack.", 0, 1, 1, NULL, 5),
    ("ein alter Schrank", "Ein normaler Schrank.",  "Du öffnest sie und schaust hinein.", "Da du kein Möbelpacker bist, läßt du ihn stehen.", 0, 1, 1, NULL, 2),
    ("ein alter Schrank", "Seine Tür ist kaputt.",  "Du öffnest sie und schaust hinein.", "Da du kein Möbelpacker bist, läßt du ihn stehen.", 1, 1, 1, NULL, 12),
    ("ein großer Schrank", "Er ist groß und alt.",  "Du öffnest sie und schaust hinein.", "Da du kein Möbelpacker bist, läßt du ihn stehen.", 0, 1, 1, NULL, 7),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1, 1, 1, NULL, 1),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 5),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 5),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 1),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 10),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 10),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 10),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 10),
    ("ein Stuhl", "Ein normaler Stuhl.", "Du setzt dich kurz auf den Stuhl und ruhst dich aus.", "Was willst du mit einem Stuhl? Er ist zu groß für deinen Rucksack.", 1,1, 1, NULL, 10),
    ("ein Stuhl", "Ein kaputter Stuhl.", "Du setzt dich auf den Stuhl und fällst mit dem Stuhl auf dem Boden. Der Stuhl war KAPUTT, Genie.", "Was willst du mit einem Stuhl, der außerdem kaputt ist?", 1, 1, 1, NULL, 10),
    ("ein Bett", "Er ist ein großes Himmelbett.", "Du legst dich kurz hin und springst gleich wieder auf. Aus der Matratze kommen lauter Ratten, die soford wieder im Dunklen verschwinden.", "Nein.", 1, 1, 1, NULL, 5),
    ("ein altes Buch", "Es handelt sich um einen Liebesroman.", "Du ließt ein paar Seiten, bis dir einfällt, dass du hier nicht zum Lesen bist.", "Du packst es ein. Vielleicht hast du später Zeit zum Lesen." , 1, 1, 1, 4, 5)
    ;
    
INSERT INTO DecoItem
	(Name, Description, InteractText, PackText, MaxNumber)
VALUES
	("altes Gemälde", "Ein Gemälde von einer schönen Dame", "Du schaust dir das Bild genauer an. Kannst aber keine Hinweise erkennen, die dir helfen, hier heraus zukommen.", "Das Bild ist fest an der Wand. Du bekommst es nicht ab.", 3),
	("altes Gemälde", "Ein Gemälde von einer Ritter", "Du schaust dir das Bild genauer an. Kannst aber keine Hinweise erkennen, die dir helfen, hier heraus zukommen.", "Das Bild ist fest an der Wand. Du bekommst es nicht ab.", 3),
	("altes Gemälde", "Ein unkenntliches Gemälde", "Du schaust dir das Bild genauer an, kannst aber nichts erkennen.", "Das Bild ist fest an der Wand. Du bekommst es nicht ab.", 5),
	("alter Tisch", "Ein alter Tisch mit wackelden Beinen.", "Die legst deinen Rucksack kurz auf dem Tisch ab.", "Was willst du mit einem Tisch? Er ist zu groß für deinen Rucksack.", 1),
	("Scherben", "viele kleine und große Scherben", "Du kannst nicht erkennen, was es einmal war.", "Da du nicht zum Putzen hier bist, lässt du die Scherben liegen.", 5),
	("alte Vase", "Eine alte Vase mit vielen Rissen.", "Du hast keine Blumen und die Vase hat auch ein Loch und ist damit unbrauchbar.", "Sie ist unbrauchbar. Du lässt sie stehen.",5),
	("alter Tepich", "Er ist alt und häßlich.", "Du hebst ihn hoch, aber außer Staub ist nichts darunter.", "Du brauchst ihn nicht und lässt ihn liegen.", 1),
	("dunkle Flecken", "Man kann nicht erkennen, von was die Flecken ursprünglich stammen.", "Da du nicht zum Putzen hier bist, lässt du die Flecken Flecken sein.", "Das ist unmöglich.", 5),
    ("ein altes Buch", "Man kann den Titel nicht entziffern.", "Du schaust ins Buch, doch die Seiten sind unleserlich geworden.", "Du brauchst kein Müll einpacken.", 5)
    ;
