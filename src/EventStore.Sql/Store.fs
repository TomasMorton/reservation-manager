module EventStore.Sql.Store

open FSharp.Data.Sql

[<Literal>]
let resolutionPath = __SOURCE_DIRECTORY__ + "/SqlProviderDependencies/"

type sql =
    SqlDataProvider<
        Common.DatabaseProviderTypes.MSSQLSERVER_DYNAMIC,
        "Server=localhost;Database=ReservationManager;User Id=sa;Password=Passw0rd",
        CaseSensitivityChange=Common.CaseSensitivityChange.ORIGINAL,
        UseOptionTypes=true,
        ResolutionPath=resolutionPath>

let getEvents entityType =
    let ctx = sql.GetDataContext()
    query {
        for aggregate in ctx.Dbo.Aggregates do
            for event in aggregate.``dbo.Events by AggregateId`` do
                select event
    }
