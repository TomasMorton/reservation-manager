module EventStore.Sql.Store

open FSharp.Data.Sql
open ReservationManager
open ReservationManager.EventData

[<Literal>]
let resolutionPath = __SOURCE_DIRECTORY__ + "/SqlProviderDependencies/"

type sql =
    SqlDataProvider<
        Common.DatabaseProviderTypes.MSSQLSERVER_DYNAMIC,
        "Server=localhost;Database=ReservationManager;User Id=sa;Password=Passw0rd",
        CaseSensitivityChange=Common.CaseSensitivityChange.ORIGINAL,
        UseOptionTypes=false,
        ResolutionPath=resolutionPath>

exception UnknownEventType of string
exception InvalidEventData of string

//todo
let parseReservationCreatedData data : ReservationCreatedData =
    match Json.deserialize<ReservationCreatedData> data with
    | Ok event -> event
    | Error ex -> raise (InvalidEventData(sprintf "Invalid Event Data:  %s" ex.Message))

let toEvent (dto:sql.dataContext.``dbo.EventsEntity``) : Event =
    match dto.Type with
    | "ReservationCreated" -> parseReservationCreatedData dto.Data |> ReservationCreated
    | _ -> raise (UnknownEventType(sprintf "Unknown Event Type in Database: %s" dto.Type))

let getEvents entityType =
    let ctx = sql.GetDataContext()
    query {
        for aggregate in ctx.Dbo.Aggregates do
            for event in aggregate.``dbo.Events by AggregateId`` do
                where (aggregate.Type = entityType)
                select event
        
    } |> Seq.map toEvent
    
let appendEvents events =
    ()
