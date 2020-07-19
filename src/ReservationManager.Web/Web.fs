namespace ReservationManager

open Giraffe
open Microsoft.AspNetCore.Http
open ReservationManager.CommandData

module HttpHandlers =
    open EventStore.InMemory.Events

    let root =
        text "Welcome to the reservation manager"

    let getReservations =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            let store = getEvents
            let result = Queries.getAllReservations store
            negotiate result next ctx

    let createCommandHandler () =
        let state = []
        CommandHandler.execute SystemClock.clock state

    let createResponse result (next : HttpFunc) (ctx : HttpContext) =
        match result with
        | Result.Ok event ->
            ctx.SetStatusCode 201
            negotiate event next ctx
        | Result.Error error ->
            ctx.SetStatusCode 400
            negotiate error next ctx

    let createReservation cmdData =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            let handler = createCommandHandler ()
            let result = CreateReservation cmdData |> handler
            createResponse result next ctx

[<RequireQualifiedAccess>]
module WebApp =
    open HttpHandlers

    let apiAddress = sprintf "/api/%s"
    let apiRoute address = apiAddress address |> route

    let routes : HttpHandler =
        choose
            [ route "/" >=> root
              GET >=> apiRoute "reservations" >=> getReservations
              POST >=> apiRoute "reservations" >=> bindJson<CreateReservationData> createReservation ]
