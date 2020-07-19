module ReservationManager.HttpHandlers

open Giraffe
open EventStore.InMemory.Events
open Microsoft.AspNetCore.Http

let root : HttpHandler =
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
