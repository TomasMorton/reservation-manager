namespace EventStore.InMemory

open System
open System.Collections.Concurrent
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

    let getStoreSection (store : ConcurrentDictionary<string, 'a list>) entityType =
        store.GetOrAdd(entityType, [])
          
    let getKeyForEvent event =
        match event with
        | Event.ReservationCreated -> "Reservations"
    
    let addToStore store entityType newItems =
        let existingItems = getStoreSection store entityType
        let updatedItems = List.append existingItems newItems
        store.[entityType] = updatedItems
    
    let appendEvents store events =
        events
        |> List.groupBy getKeyForEvent
        |> List.map (fun (k, v) -> addToStore store k v)
        |> ignore