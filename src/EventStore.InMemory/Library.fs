namespace EventStore.InMemory

module Events =
    let getEvents entityType =
        match entityType with
        | "Reservations" -> ()
