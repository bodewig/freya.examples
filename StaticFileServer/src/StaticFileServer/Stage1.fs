module StaticFileServer.Stage1

open Freya.Core
open Freya.Machine

// Resources

let files =
    freyaMachine {
        including defaults }