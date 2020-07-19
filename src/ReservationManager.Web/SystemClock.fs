module ReservationManager.SystemClock

open System

let clock () = DateTimeOffset.UtcNow
