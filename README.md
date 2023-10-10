# 🌆CondominiumApp

## ⚠️Situation:
According to data from elmundo.cr, condominiums in Costa Rica have increased almost 600% since 1990.
https://elmundo.cr/costa-rica/desde-1990-construccion-de-condominios-y-apartamentos-en-la-gam-aumento-casi-un-600/
This has produced an immense need for applications and services that help the companies that manage these housing projects.

## 💬Introduction:
The idea is to develop an application that streamlines the entry and exit of visitors to the condominiums.

## 📑Description
Each condominium owner (house or apartment owner) will be registered in the system (through the root user) in which he/she can be assigned a housing project and also a house or apartment number:
Example. 

Sebastián Campos with ID 115450523 was assigned house 154 in the Natura Condominium located in San Rafael de Escazú.

Each condominium owner once registered will have the possibility in the system to create, edit and delete favorite visits.
Example.
```
1. Sebastián Campos, ID 115450523 registered a favorite with the following details.
  a.	Mariela Salas, sister, ID 113550249 vehicle Nissan Versa White model, license plate GMS-106.
```

## 📝Requirements for the CondominiumApp
```
Therefore, as mentioned above, the root user can:
1.  Create, edit and delete hosting projects (Logo, Code, Name, Name, Address, Office Phone) 2.
2.	Create, edit and delete condominiums (ID, Name, Phone numbers (1 or more), Email, Photo).
3.	Assign condominium owners to housing projects.
4.  Create, edit and delete security guards.
5.	Send registration information to each condominium owner by email.
6.  Listing of housing projects and condominium owners with basic filters.
```
```
The other level of user of the system will be the same condominium owner who will be able to:
1.  Create, edit and delete visits (ID, Name, Vehicle {Make, Model and Color}).
2.	Create, edit and delete favorite visits (ID, Name, Vehicle {Make, Model and Color}).
3.	You will be able to create 'Delivery' type quick visits (UberEats, Didi, Rappi, PedidosYa, etc.).
4.	Create one or more vehicles of your property that will have free access (License Plate, Make, Model and Color).
5.	In case you have created favorite visits, the system will allow you to choose among them to make a normal visit registration. You will only have to indicate time and date of the visit.
```
```
The other level of user will be the security officers who will be able to:
1.	Officers will be able to choose the housing project they are working on that day.
2.	To be able to filter by license plate number vehicles of the condominium owners that have free entry.
3.	Review a visitation record per condominium owner to grant access.
4.	Example:
     - Visitor Karla Guzman arrives at the housing project in her black Toyota Rav4 vehicle and indicates that she is going to house 120 where Julian is staying.
     - The officer must check the system and filter by house, license plate or name and verify that there is already a visitation record made to proceed with the opening of the gate.
     - Otherwise, he will have to call the condominium owner directly to proceed with the opening of the gate.
```
## 🚨Additional requirement
EasyPass
```
The system will give the condominium owner the opportunity to make an EasyPass. 
The EasyPass is a QR Code (based on a 4-digit number) that can be shared with the visitor. This code is valid for 12 hours and the visitor can, instead of talking to the security officer, scan their QR code or type in their EasyPass on a digital keypad. If the code is correct, it will give the visitor free passage or it will indicate that the code is incorrect.
```
## 💻Software Available:
- C#
  - ASP MVC
- SQL Server
- HTML5
- CSS3
- Java Script
