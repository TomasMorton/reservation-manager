namespace ReservationManager

open System
open Giraffe

module HttpHandlers =
    open ReservationManager.Types

    let root =
        text "Welcome to the reservation manager"

    let getReservations =
        [ { Date = DateTime(2020, 01, 01)
            NumberOfAds = 3 }
          { Date = DateTime(2020, 02, 01)
            NumberOfAds = 5 } ]
        |> json

[<RequireQualifiedAccess>]
module WebApp =
    open HttpHandlers

    let apiAddress = sprintf "/api/%s"
    let apiRoute address = apiAddress address |> route

    let routes : HttpHandler =
        choose
            [ route "/" >=> root
              GET >=> apiRoute "reservations" >=> getReservations ]
