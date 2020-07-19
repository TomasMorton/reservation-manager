namespace ReservationManager

open System
open Giraffe

module HttpHandlers =
    let root =
        text "Welcome to the reservation manager"

    let getReservations =
        let query () = Queries.getAllReservations ()

        query () |> json

[<RequireQualifiedAccess>]
module WebApp =
    open HttpHandlers

    let apiAddress = sprintf "/api/%s"
    let apiRoute address = apiAddress address |> route

    let routes : HttpHandler =
        choose
            [ route "/" >=> root
              GET >=> apiRoute "reservations" >=> getReservations ]
