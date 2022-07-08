Microservice solution
08.07.2022

My note:
While I was doing it I found out that using several tables is kinda hard so View Table was created.
I have ID's because at first it was Azure database and it had some problems with strings (later I found out that it was just some wierd option).
Migrations worked flawlessly until I had to switch to local SQL Server. Then they broke - I wrote information how to fix them inside this file below.
There are some things that I would love to make work differently. Unfortunately some changes had to be made after I thought I was done
with Microservice while I was doing the Xamarin Project part.
Also I had to use CONVEYOR in order to make my local database accessible over the internet.

Brief overview over the files used within this solution:

Classes:
CarTechExam.cs:
This class was made as my model that contains information pulled from or into the Database.
It resembles Columns from my View table that combines resutls from table Cars and TechExam into one, easy to use view table.

Database:
Entity:
Entities are just models that resemble tables in my Database.
Cars.cs:
Same as before but contains infromation 1:1 as my Cars Table.

TechExam.cs:
Same as before but contains infromation 1:1 as my Exams Table.
      
Views:
There is only one View class ( CarDetailsView.cs ) that contain information 1:1 as my view table.
  
DatabaseContext.cs:
This class contains database context that resembles tables that exist in my Database and some configs.
DbSet<T> is used to declare a table inside it (Cars, Exams, CarDetailsView)
Function OnConfiguring is used to set up a connection between Microservice and The Database, line 15 (optionsBuilder.UseSqlServer()) should
be changed in order to connect to different database.
  
Migartions:
 While I was using migrations to automatically create tables and view from my database, such tables (if migration won't work) could be created
using those SQL commands:
CREATE TABLE Cars (Car_ID int IDENTITY(1,1) PRIMARY KEY, RegNum varchar(255), Brand varchar(255), Model varchar(255));
CREATE TABLE Exams (TechId int IDENTITY(1,1) PRIMARY KEY, Car_ID int, IsWorking bit, ActualDate datetime2, NextDate datetime2, FOREIGN KEY (Car_ID) REFERENCES dbo.Cars(Car_ID));
create view CarDetailsView as select RegNum, Brand, Model, ActualDate, NextDate, IsWorking from [dbo].[Cars] as C inner join [dbo].[Exams] as E on C.CarID = E.CarID 
    
I found out that migrations (after swtiching to local database from Azure database) can break up - createcardetailsview migration sometimes get set up before the rest of the 
tables - it won't work. If that occurs then it is advised to exclude CreateCarDetailsView Migration, migrate the database, retrieve previously excluded migration and migrate it again.
Migration can be made with this command:
update-database
All migration files contain informations to set up database from my entites and view, they were set up automatically.

Controllers:
CarsController.cs:
This is the main controller that takes care of all GET / POST functions of my microservice. Using DatabaseContext datatype operations
on the database can be performed.
  
There are several options to use:
GetCarByBrand() - didn't change the name - it gets a string that contains car specific information and an integer that will specify what type
of infromation it recieved in order to search for car or cars if multiple car were found. It uses localhost:<port>/api/cars path.
  
Post() - It is used to insert new data into the database. I create new Car, then insert it into the database within Cars Table - since ID are automatic, I search for the same car in the database and then using its new found ID I can create new TechExam object that can be placed into Exams Table.
    
Update() - I couldn't really find out how to properly use Update here so it's still HttpPost (The fact that Update part was specified in Xamarin part of the task)
Whole CarTechExam entity is provided, then it is found in the database and saved as variables. Then the information gets changed and saved into the database.
