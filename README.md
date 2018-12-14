# AuctionZ

Simple auction built with asp net core 2.1, n-layered architecture
and SignalR 


Auction supports following actions:
-  Authorization
-  CRUD on Lots
-  Search of lots by critirea: title, category (depends on user roles)
-  Bids
-  Lot, user roles and lot categories management.
-  Real time bids (notifications sends when someone make bid)
-  Real time auction (notifications sends to winner when lot ends.(provide secret in EmailService))

## Built with 
- **ASP.NET Core 2.1**  
- **Entity Framework Core**
- **AutoMapper**
- **SignalR** � Library allows server code to send async notifications to client-side web apps
 
## Todo
- A bit of fixes related to url routes.
