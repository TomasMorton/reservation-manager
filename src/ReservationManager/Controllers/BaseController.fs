namespace ReservationManager.Controllers

open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("api/[controller]")>]
type BaseController() =
    inherit ControllerBase()
