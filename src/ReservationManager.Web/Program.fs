namespace ReservationManager

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Giraffe

module Program =
    let configureApp (app : IApplicationBuilder) =
        app.UseGiraffe WebApp.routes

    let configureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    [<EntryPoint>]
    let main _ =
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(
                fun webHostBuilder ->
                    webHostBuilder
                        .Configure(configureApp)
                        .ConfigureServices(configureServices)
                        |> ignore)
            .Build()
            .Run()
        0