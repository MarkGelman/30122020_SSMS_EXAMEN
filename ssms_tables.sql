--Drop table Categories;
 /*CREATE TABLE Categories
	(
		id bigint identity,
		name varchar(20),
		Primary key (id)
	);

	CREATE TABLE Stores
	(
		id bigint identity,
		name varchar(20),
		flor int,
		category_id bigint,
		Primary key (id),
		Foreign key (category_id) REFERENCES Categories(id)
	);
	
	INSERT INTO Categories 
	VALUES	('Electronics'),
			('Tourism'),
			('Literature'),
			('Food'),
			('Entertainment'),
			('Jewerly'),
			('Computers');

	INSERT INTO Stores
	VALUES	('Big Shop',2,1),
			('Shekem Electric',1,1),
			('Mahsanei Hashmal',3,1),
			('Rikoshet',2,2),
			('All to tourism',1,2),
			('Stematsky',2,3),
			('Colliseum',3,3),
			('MacDonald`s',1,4),
			('Burgerunch',1,4),
			('Shvarma Ozen',1,4),
			('Chips and Cola',1,4),
			('Cinema City',3,5),
			('Accurate Shooter',2,5),
			('Golden Canyon',2,6),
			('Bugs',3,7),
			('Ivory',2,7);	*/ 

Exec sp_rename 'Stores.flor','floor';
go			