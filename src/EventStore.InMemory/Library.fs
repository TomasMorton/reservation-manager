namespace EventStore.InMemory

open System
open ReservationManager.Types

module Events =
    exception UnknownEntityType of string

    let private getReservations () =
        [ { Date = DateTime(2020, 01, 01)
            NumberOfAds = 3 }
          { Date = DateTime(2020, 02, 01)
            NumberOfAds = 5 } ]

    let getEvents entityType =
        match entityType with
        | "Reservations" -> getReservations ()
        | _ -> raise (UnknownEntityType(sprintf "Unknown EntityType: %s" entityType))
