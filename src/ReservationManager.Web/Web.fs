namespace ReservationManager

open Giraffe
open Microsoft.AspNetCore.Http

module HttpHandlers =
    open EventStore.InMemory.Events

    let root =
        text "Welcome to the reservation manager"

    let getReservations =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            let store = getEvents
            let result = Queries.getAllReservations store
            json result next ctx

    let createReservation =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            setStatusCode 201 next ctx

[<RequireQualifiedAccess>]
module WebApp =
    open HttpHandlers

    let apiAddress = sprintf "/api/%s"
    let apiRoute address = apiAddress address |> route

    let routes : HttpHandler =
        choose
            [ route "/" >=> root
              GET >=> apiRoute "reservations" >=> getReservations
              POST >=> apiRoute "reservations" >=> createReservation ]
