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

// Freya

open Freya.Core

let app =
    OwinAppFunc.ofFreya Api.api

// Katana

open Microsoft.Owin.Hosting

type Katana () =
    member __.Configuration () =
        app

let katana () =
    WebApp.Start<Katana> "http://localhost:7000"

// Suave

open Suave
open Suave.Logging
open Suave.Owin
open Suave.Web

let config =
    { defaultConfig with
        bindings = [ HttpBinding.mkSimple HTTP "127.0.0.1" 7000 ]
        logger = Loggers.saneDefaultsFor LogLevel.Verbose }

let suave () =
    startWebServer config (OwinApp.ofAppFunc "/" app)

// Main

[<EntryPoint>]
let main _ =

    // Comment out as appropriate!

    let _ = katana ()
    //let _ = suave ()
    let _ = Console.ReadLine ()

    0