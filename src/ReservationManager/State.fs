namespace ReservationManager.State

open System
open ReservationManager

type Reservation =
    { Date : DateTime
      NumberOfAds : int
      ReservationId : ReservationId }
