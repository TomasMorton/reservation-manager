module ReservationManager.EventHandler

open ReservationManager.State
open ReservationManager.EventData

let createReservationFromData (data : ReservationCreatedData) : Reservation =
    { Date = data.Date
      NumberOfAds = data.NumberOfAds
      ReservationId = data.ReservationId }

let apply state event =
    match event with
    | ReservationCreated data ->
        let newReservation =
            data |> createReservationFromData
        { state with Reservations = newReservation :: state.Reservations }
