namespace ReservationManager

open System

module CommandData =

    [<CLIMutable>]
    type CreateReservationData =
        { Date : DateTime
          NumberOfAds : int }

type Command = CreateReservation of CommandData.CreateReservationData
