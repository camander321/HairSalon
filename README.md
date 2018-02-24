
# Hair Salon

#### By Cameron Anderson

## Description
A website to keep track of a hair salon's employees and all  of their clients.
* 

## Specifications

- User clicks 'add a new stylist'
  - Example input: "user clicks link"
  - Example output: return Stylist creation form
  
- Gather form input and add details into stylist table in database
  - Example input: First:John, Last:Smith
  - Example output: INSERT INTO `stylists` (last, first) VALUES ('Smith', 'John');
  
- Show list of all stylists
  - Example input: user clicks 'show all stylists'
  - Example output: shows list of all stylists
 
- Show all of a stylist's clients
  - Example input: user clicks a stylist's name
  - Example output: shows list of all clients with that stylist
    
- User clicks 'add a new client'
  - Example input: "user clicks link"
  - Example output: return Client creation form
  
- Gather form input and add details into client table in database
  - Example input: First:Hugh, Last:Jackman
  - Example output: INSERT INTO `stylists` (last, first) VALUES ('Jackman', 'Hugh');
  
- Show list of all clients
  - Example input: user clicks 'show all clients'
  - Example output: shows list of all clients
 
- Showa clients details
  - Example input: user clicks a clients's name
  - Example output: shows the client's details, including their stylist
  

  
  
## Setup/Installation Requirements
* Start MAMP 
* Run MySQL with `mysql -uroot -proot`
* Use the following commands to create the database:
  * `CREATE DATABASE IF NOT EXISTS cameron_anderson DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;`
  * `USE cameron_anderson;`
  * `CREATE TABLE clients (id int(11), last_name varchar(255), first_name varchar(255), stylist int(11)) ENGINE=InnoDB DEFAULT CHARSET=utf8;`
  * `CREATE TABLE stylists (id int(11), last_name varchar(255), first_name varchar(255)) ENGINE=InnoDB DEFAULT CHARSET=utf8;`
  * `\q`
* Clone the git repository from 'https://github.com/camander321/HairSalon.git'.
* run the command 'dotnet restore' to download the necessary packages.
* run the command 'dotnet run' to build and run the server on localhost.
* use your preferred web browser to navigate to localhost:5000


## Support and contact details

* contact the author at chamburg321@gmail.com

## Technologies Used

* C#
* Asp .NET Core MVC
* Razor
* MySQL
* HTML
* CSS
* Bootstrap
* Javascript
* JQuery

### License

Copyright (c) 2018 Cameron Anderson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.