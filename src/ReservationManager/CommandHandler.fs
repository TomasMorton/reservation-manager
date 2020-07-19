module ReservationManager.CommandHandler

open ReservationManager.CommandData
open ReservationManager.EventData

let toEventData timestamp (commandData : CreateReservationData) =
    { Date = commandData.Date
      NumberOfAds = commandData.NumberOfAds
      ReservationId = ReservationId.create ()
      Timestamp = timestamp }

let execute clock state command =
    let timestamp = clock ()

    let event =
        match command with
        | CreateReservation data ->
            data
            |> toEventData timestamp
            |> ReservationCreated
            |> Result.Ok

    event
