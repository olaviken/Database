# Database
This is an unfinished work project. The main goal of this project was to create a system that could track inventory and research infrastructure. It was cancelled because of similar system existed.
It provides a prototype for a inventory and infrastructure system with an underlying MySQL database. 

##About this project
These files gives a starting point for retrieving and storing data from an SQL-database using a C# program. This project is divided into several folders which include different classes. 
The main folder is "ListStructure" this is again divided into two folders, "Objects" and "Lists". "Objects" has classes for the different entities that was considered neccessary. While "Lists" has classes for retrieving, storing, updating, and deleting data from the database for the different entities. These classes uses a class called "SqlController" in the "Logic" folder. SqlController contains functions and variables for connecting to the database. Many of the variables was at this point hardcoded. In later version this should have been replaced with data possible stored at usersecrets or as a encrypted json file. The graphic user interface for this project is a test interface to control data coming to and from the database being correct. 
