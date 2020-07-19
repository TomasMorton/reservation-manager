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

  
## Dependencies
### Giraffe
While the basic ASP.NET Core api libraries can be used with F#, they're not very functional. This is particularly an issue when trying to compose the app (dependency management).\
Giraffe is a slim wrapper around ASP.NET Core that allows us to build a more functionally designed app. 