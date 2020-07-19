namespace ReservationManager

open System

module CommandData =
    type CreateReservationData =
        { Date : DateTime
          NumberOfAds : int }

type Command = | CreateReservation of CommandData.CreateReservationData
