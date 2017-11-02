module Routing

open Giraffe
open Giraffe.HttpHandlers
open Giraffe.HttpContextExtensions
open Microsoft.AspNetCore.Http
open LunchTypes
open DataAccess

let handleLunchFilter (next: HttpFunc) (ctx : HttpContext) =
    let filter = ctx.BindQueryString<LunchFilter>()
    let lunchSpots = LunchAccess.getLunches filter
    json lunchSpots next ctx

let handleAddLunch (next: HttpFunc) (ctx : HttpContext) =
    task {
        let! lunch = ctx.BindJson<LunchSpot>()
        LunchAccess.addLunch lunch
        return! text (sprintf "Added %s to the lunch spots." lunch.Name) next ctx
    }

let routes: HttpFunc -> HttpFunc =
    choose [
        GET >=> route "/lunch" >=> handleLunchFilter
        POST >=> route "/lunch/add" >=> handleAddLunch
        setStatusCode 404 >=> text "Not Found" ]