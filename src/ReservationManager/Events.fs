namespace ReservationManager

open System

type ReservationId = ReservationId of Guid

module EventData =
    type ReservationCreatedData =
        { Date : DateTime
          NumberOfAds : int
          ReservationId : ReservationId
          Timestamp : DateTime }

type Events = | ReservationCreated of EventData.ReservationCreatedData
