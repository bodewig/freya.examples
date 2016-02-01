module StaticFileServer.Program

open Freya.Core
open Suave
open Suave.Logging
open Suave.Operators
open Suave.Owin
open Suave.Web

// App

let app =
    Stage4.files

// Main

[<EntryPoint>]
let main _ =

    let config =
        { defaultConfig with
            bindings = [ HttpBinding.mkSimple HTTP "0.0.0.0" 7000 ]
            logger = Loggers.saneDefaultsFor LogLevel.Verbose }

    let owin = OwinApp.ofAppFunc "/" (OwinAppFunc.ofFreya app)

    startWebServer config owin

    0