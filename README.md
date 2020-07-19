# Reservation Manager

## Troubleshooting
### 404 on new route
Ensure that your request handler is a class member, not a function:
```
[<HttpGet>]
member __.GetReservations() = []
```