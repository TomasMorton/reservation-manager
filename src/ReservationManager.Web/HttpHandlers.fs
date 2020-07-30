module ReservationManager.HttpHandlers

open System.Collections.Concurrent
open Giraffe
//open EventStore.InMemory
open EventStore.Sql
open Microsoft.AspNetCore.Http

// App composition
//let store = ConcurrentDictionary<string, Event list>()

let createCommandHandler () =
    let state = []
    CommandHandler.execute SystemClock.clock state

let appendToStore events =
//    Store.appendEvents store events
    Store.appendEvents events

let getAllReservations () =
//    Store.getEvents store |> Queries.getAllReservations
    Store.getEvents |> Queries.getAllReservations

// Helpers
let appendIfSuccessful result =
    match result with
    | Ok events -> appendToStore events
    | Error _ -> ()

let createResponse result (next : HttpFunc) (ctx : HttpContext) =
    match result with
    | Result.Ok event ->
        ctx.SetStatusCode 201
        negotiate event next ctx
    | Result.Error error ->
        ctx.SetStatusCode 400
        negotiate error next ctx

// Handlers
let root : HttpHandler =
    text "Welcome to the reservation manager"

let getReservations =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        let result = getAllReservations ()
        negotiate result next ctx

let createReservation cmdData =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        let handler = createCommandHandler ()
        let result = CreateReservation cmdData |> handler

        appendIfSuccessful result
        createResponse result next ctx
