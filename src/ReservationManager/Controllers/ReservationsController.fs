namespace ReservationManager.Controllers

open System
open Microsoft.AspNetCore.Mvc
open ReservationManager.Types

type ReservationsController() =
    inherit BaseController()

    [<HttpGet>]
    member __.GetReservations() =
        [ { Date = new DateTime(2020, 01, 01)
            NumberOfAds = 3 }
          { Date = new DateTime(2020, 02, 01)
            NumberOfAds = 5 } ]
