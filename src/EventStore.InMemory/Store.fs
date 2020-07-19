namespace EventStore.InMemory

open System.Collections.Concurrent
open ReservationManager

module Store =
    exception UnknownEntityType of string

    let getStoreSection (store : ConcurrentDictionary<string, 'a list>) entityType =
        store.GetOrAdd(entityType, [])

    let getKeyForEvent event =
        match event with
        | Event.ReservationCreated -> "Reservations"

    let addToStore store entityType newItems =
        let existingItems = getStoreSection store entityType
        let updatedItems = List.append existingItems newItems
        if store.TryUpdate(entityType, updatedItems, existingItems)
        then ()
        else failwith "Another reservation has been placed at the same time. Please try again"


    let appendEvents store events =
        events
        |> List.groupBy getKeyForEvent
        |> List.iter (fun (k, v) -> addToStore store k v)

    let getEvents store entityType =
        match entityType with
        | "Reservations" -> getStoreSection store "Reservations"
        | _ -> raise (UnknownEntityType(sprintf "Unknown EntityType: %s" entityType))
