namespace EventStore.InMemory

open System
open ReservationManager
open ReservationManager.State

module Events =
    exception UnknownEntityType of string

    let private getReservations () =
        [ { Date = DateTime(2020, 01, 01)
            NumberOfAds = 3
            ReservationId = ReservationId.create () }
          { Date = DateTime(2020, 02, 01)
            NumberOfAds = 5
            ReservationId = ReservationId.create () } ]

    let getEvents entityType =
        match entityType with
        | "Reservations" -> getReservations ()
        | _ -> raise (UnknownEntityType(sprintf "Unknown EntityType: %s" entityType))
