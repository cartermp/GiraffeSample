namespace DataAccess

open System.IO
open NPoco
open Microsoft.Data.Sqlite

open LunchTypes

module LunchAccess =
    let private connString = "Filename=" + Path.Combine(Directory.GetCurrentDirectory(), "Sample.db")

    let addLunch (lunchSpot: LunchSpot) =
        use conn = new SqliteConnection(connString)
        conn.Open()
        
        use txn: SqliteTransaction = conn.BeginTransaction()

        let cmd = conn.CreateCommand()
        cmd.Transaction <- txn
        cmd.CommandText <- @"
insert into LunchSpots (Name, Latitude, Longitude, Cuisine, VegetarianOptions, VeganOptions)
values ($Name, $Latitude, $Longitude, $Cuisine, $VegetarianOptions, $VeganOptions)"

        cmd.Parameters.AddWithValue("$Name", lunchSpot.Name) |> ignore
        cmd.Parameters.AddWithValue("$Latitude", lunchSpot.Latitude) |> ignore
        cmd.Parameters.AddWithValue("$Longitude", lunchSpot.Longitude) |> ignore
        cmd.Parameters.AddWithValue("$Cuisine", lunchSpot.Cuisine) |> ignore
        cmd.Parameters.AddWithValue("$VegetarianOptions", lunchSpot.VegetarianOptions) |> ignore
        cmd.Parameters.AddWithValue("$VeganOptions", lunchSpot.VeganOptions) |> ignore

        cmd.ExecuteNonQuery() |> ignore

        txn.Commit()

    let private getLunchFetchingQuery filter =
        let cuisinePart, hasCuisine =
            match filter.Cuisine with
            | Some c -> (sprintf "Cuisine = \"%s\"" c, true)
            | None -> ("", false)

        let vegetarianPart, hasVegetarianPart =
            match filter.VegetarianOptions with
            | Some v -> (sprintf "VegetarianOptions = \"%d\"" (if v then 1 else 0), true) // Sqlite uses ints 0 and 1 for bools.
            | None -> ("", false)

        let veganPart, hasVeganPart =
            match filter.VeganOptions with
            | Some v -> (sprintf "VeganOptions = \"%d\"" (if v then 1 else 0), true) // Sqlite uses ints 0 and 1 for bools.
            | None -> ("", false)

        let hasWhereClause = hasCuisine || hasVegetarianPart || hasVeganPart

        let query = 
            "select * from LunchSpots" + 
            (if hasWhereClause then " where " else "") +
            cuisinePart +
            (if hasCuisine && hasVegetarianPart then " and " + vegetarianPart else vegetarianPart) +
            (if (hasCuisine || hasVegetarianPart) && hasVeganPart then " and " + veganPart else veganPart)

        query

    let getLunches (filter: LunchFilter) =
        let query = getLunchFetchingQuery filter

        use conn = new SqliteConnection(connString)
        conn.Open()

        use db = new Database(conn)
        db.Fetch<LunchSpot>(query)