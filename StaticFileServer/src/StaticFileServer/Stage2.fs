module StaticFileServer.Stage2

open System.IO
open Freya.Core
open Freya.Machine

let getFileInfo (path: string) =
    let filePath = Path.Combine (root.FullName, path.Trim ([| '/' |]))
    let fileInfo = FileInfo (filePath)

    fileInfo

let readFile (file: FileInfo) =
    File.ReadAllBytes (file.FullName)

// Resources

let files =
    freyaMachine {
        including defaults }