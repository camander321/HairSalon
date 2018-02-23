
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
  - Example input: First:John, Last:Smith, StartDate:02/23/2018
  - Example output: INSERT INTO `stylists` (last, first, start_date) VALUES ('Smith', 'John', 2018_23_02);
    
- User clicks 'add a new client'
  - Example input: "user clicks link"
  - Example output: return Client creation form
  
- Gather form input and add details into client table in database
  - Example input: First:Hugh, Last:Jackman, NextAppt:05/01/2018
  - Example output: INSERT INTO `stylists` (last, first, appt) VALUES ('Jackman', 'Hugh', 2018_05_01);
  

    


## Setup/Installation Requirements

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