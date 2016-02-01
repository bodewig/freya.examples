//----------------------------------------------------------------------------
//
// Copyright (c) 2016
//
//    Ryan Riley (@panesofglass) and Andrew Cherry (@kolektiv)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
//----------------------------------------------------------------------------

module Freya.TodoBackend.Program

open System
open global.Owin
open Microsoft.Owin.Hosting
open Freya.Core
open Suave
open Suave.Logging
open Suave.Operators
open Suave.Owin
open Suave.Web

(* Katana
   Katana (Owin Self Hosting) expects us to expose a type with a specific
   method. Freya lets us do see easily, the OwinAppFunc module providing
   functions to turn any Freya<'a> function in to a suitable value for
   OWIN compatible hosts such as Katana. *)

type TodoBackend () =
    member __.Configuration () =
        OwinAppFunc.ofFreya Api.todoRoutes

(* Main
   A very simple program, simply a console app, with a blocking read from
   the console to keep our server from shutting down immediately. Though
   we are self hosting here as a console application, the same application
   should be easily transferrable to any OWIN compatible server, including
   IIS. *)

[<EntryPoint>]
let main _ =
    // Katana
    let url = "http://localhost:7000"
    let app = WebApp.Start<TodoBackend> url
    printfn "Listening on %s" url
    printfn "Press <enter> to stop"
    let _ = System.Console.ReadLine ()
    app.Dispose()

    (*
    // Suave
    let config =
        { defaultConfig with
            bindings = [ HttpBinding.mkSimple HTTP "127.0.0.1" 7000 ]
            logger = Loggers.saneDefaultsFor LogLevel.Verbose }
     
    let owin =
        OwinApp.ofAppFunc "/" (OwinAppFunc.ofFreya Freya.TodoBackend.Api.api)

    startWebServer config owin
    *)
    0