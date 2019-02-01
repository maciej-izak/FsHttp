﻿
#r @"../../packages/fsharp.data/lib/net45/FSharp.Data.dll"
#r @"../../packages/NUnit/lib/netstandard2.0/nunit.framework.dll"
// #r @"../FsHttp/bin/Debug/netstandard2.0/FsHttp.dll"
#load @"../FsHttp/bin/Debug/netstandard2.0/FsHttp.fsx"

open FsHttp
open FsHttp.Dsl


let req = get "http://www.google.de" .> go
req |> toText

// should throw
get "http://www.google.de" --timeoutInSeconds 0.1 .> go


let r = get "https://reqres.in/api/users" .> go
r.content.Headers.ContentType.MediaType.Contains("application/json")

post "https://reqres.in/api/users"
--cacheControl "no-cache"
--body
--json """
{
    "name": "morpheus",
    "job": "leader"
}
"""
.> go


async {
    let! response =
        post "https://reqres.in/api/users"
        --cacheControl "no-cache"
        --body
        --json """
        {
            "name": "morpheus",
            "job": "leader"
        }
        """
        >. go
    let content = response |> toText
    printfn "Content is: %s" content
}
|> Async.RunSynchronously


httpLazy {
    GET "http://www.google.de"
}
.> go

http {
    GET "http://www.google.de"
    timeoutInSeconds 1.0
}