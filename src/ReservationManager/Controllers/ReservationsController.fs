namespace ReservationManager.Controllers

open Microsoft.AspNetCore.Mvc

type ReservationsController() =
    inherit BaseController()

    [<HttpGet>]
    member __.GetReservations() = [ "reservation 1"; "reservation 2" ]
