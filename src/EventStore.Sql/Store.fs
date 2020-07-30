module EventStore.Sql.Store

open System
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

exception UnknownEntityType of string

//todo
let parseReservationCreatedData data : ReservationCreatedData =
    { Date = DateTime.UtcNow
      NumberOfAds = -1
      ReservationId = ReservationId.create ()
      Timestamp = DateTimeOffset.UtcNow }

let toEvent (dto:sql.dataContext.``dbo.EventsEntity``) : Event =
    match dto.Type with
    | "ReservationCreated" -> parseReservationCreatedData dto.Data |> ReservationCreated
    | _ -> raise (UnknownEntityType(sprintf "Unknown Event Type in Database: %s" dto.Type))

let getEvents entityType =
    let ctx = sql.GetDataContext()
    query {
        for aggregate in ctx.Dbo.Aggregates do
            for event in aggregate.``dbo.Events by AggregateId`` do
                where (aggregate.Type = entityType)
                select event
        
    } |> Seq.map toEvent
