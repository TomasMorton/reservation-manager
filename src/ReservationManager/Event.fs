namespace ReservationManager

open System

[<RequireQualifiedAccess>]
module ReservationId =
    type T = ReservationId of Guid
    let create () = Guid.NewGuid () |> ReservationId

module EventData =
    type ReservationCreatedData =
        { Date : DateTime
          NumberOfAds : int
          ReservationId : ReservationId.T
          Timestamp : DateTime }

type Event = | ReservationCreated of EventData.ReservationCreatedData
