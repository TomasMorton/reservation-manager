namespace ReservationManager

open System

module CommandData =
    type CreateReservationData =
        { Date : DateTime
          NumberOfAds : int }

type Commands = | CreateReservation of CommandData.CreateReservationData
