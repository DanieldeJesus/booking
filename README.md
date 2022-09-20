# Booking Alten

#Overview
Implementation a booking API for the very last hotel in Cancun assuming the scenario of Post-Covid situation. People are now free to travel everywhere but because of the pandemic, a lot of hotels went bankrupt. Some former famous travel places are left with only one hotel. Taking in count the following requirements:

API will be maintained by the hotel’s IT department.
As it’s the very last hotel, the quality of service must be 99.99 to 100% => no downtime
For the purpose of the test, we assume the hotel has only one room available
To give a chance to everyone to book the room, the stay can’t be longer than 3 days and can’t be reserved more than 30 days in advance.
All reservations start at least the next day of booking,
To simplify the use case, a “DAY’ in the hotel room starts from 00:00 to 23:59:59.
Every end-user can check the room availability, place a reservation, cancel it or modify it.
To simplify the API is insecure.


#Documentation
 - The APIPostman project, is a backend application for use with Postman;
 - The MVCIncomplet project is an MVC application using .NetCore3.1 and Razor. But it's not finished;
 - In the create_tables file are the scripts to create the database in MySql;

To use with postman:
GET - http://localhost:5582/api/reservation/available-room/2022-09-28/2022-09-30

POST -http://localhost:5582/api/reservation/insert
{
    "name": "Daniel de Jesus",
    "email": "daniel@email.com",
    "phone": "+5548999999999",
    "reservationroom":{
        "bookedfrom": "2022-09-20",
        "bookedto": "2022-09-30",
        "room": {
            "id":1
        }
    }
}

PUT - http://localhost:5582/api/reservation/update
{
    "id":1,
    "name": "Daniel",
    "email": "daniel@email.com.br",
    "phone": "+5548999999999",
    "reservationroom":{
        "id":1,
        "bookedfrom": "2022-09-21",
        "bookedto": "2022-09-30",
        "room": {
            "id":1
        }
    }
}

PUT - http://localhost:5582/api/reservation/cancel/1
