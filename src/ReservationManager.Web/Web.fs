namespace ReservationManager

open Giraffe
open ReservationManager.CommandData

module WebApp =
    open HttpHandlers

    let apiAddress = sprintf "/api/%s"
    let apiRoute address = apiAddress address |> route

    let routes : HttpHandler =
        choose
            [ route "/" >=> root
              GET >=> apiRoute "reservations" >=> getReservations
              POST >=> apiRoute "reservations" >=> bindJson<CreateReservationData> createReservation ]
