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

open Freya.Core
open Suave
open Suave.Logging
open Suave.Operators
open Suave.Owin
open Suave.Web

[<EntryPoint>]
let main _ =
    let config =
        { defaultConfig with
            bindings = [ HttpBinding.mkSimple HTTP "0.0.0.0" 7000 ]
            logger = Loggers.saneDefaultsFor LogLevel.Verbose }
     
    let owin =
        OwinApp.ofAppFunc "/" (OwinAppFunc.ofFreya Freya.TodoBackend.Api.api)

    startWebServer config owin
    0