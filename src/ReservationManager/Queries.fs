module ReservationManager.Queries

open System
open ReservationManager.Types

let getAllReservations () =
    [ { Date = DateTime(2020, 01, 01)
        NumberOfAds = 3 }
      { Date = DateTime(2020, 02, 01)
        NumberOfAds = 5 } ]
