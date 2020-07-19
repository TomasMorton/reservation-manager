namespace ReservationManager

open System
open Giraffe
open Microsoft.AspNetCore.Http

module HttpHandlers =
    open EventStore.InMemory.Events

    let root =
        text "Welcome to the reservation manager"

    let getReservations =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            let result = Queries.getAllReservations getEvents
            json result next ctx

[<RequireQualifiedAccess>]
module WebApp =
    open HttpHandlers

    let apiAddress = sprintf "/api/%s"
    let apiRoute address = apiAddress address |> route

    let routes : HttpHandler =
        choose
            [ route "/" >=> root
              GET >=> apiRoute "reservations" >=> getReservations ]
