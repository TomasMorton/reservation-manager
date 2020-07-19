# Reservation Manager

## Plan
create a reservation for a campaign
  needs to have a date range and the number of faces

input validation:
  date range is valid
  number of faces is positive

business validation:
  start date respects lead time
  number of faces are available

system data required:
  number of faces available per day (capacity)

  
  

## Troubleshooting
### 404 on new route
Ensure that your request handler is a class member, not a function:
```
[<HttpGet>]
member __.GetReservations() = []
```