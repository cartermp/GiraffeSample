module LunchTypes

type LunchSpot() =
    member val ID = 0 with get, set
    member val Name = "" with get, set
    member val Latitude = 0.0 with get, set
    member val Longitude = 0.0 with get, set
    member val Cuisine = "" with get, set
    member val VegetarianOptions = false with get, set
    member val VeganOptions = false with get, set

[<CLIMutable>]
type LunchFilter =
    { Cuisine: Option<string>
      VegetarianOptions: Option<bool>
      VeganOptions: Option<bool> }