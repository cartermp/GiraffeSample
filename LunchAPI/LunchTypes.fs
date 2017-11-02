module LunchTypes

[<CLIMutable>]
type LunchSpot =
    { ID: int
      Name: string
      Latitude: float
      Longitude: float
      Cuisine: string
      VegetarianOptions: bool
      VeganOptions: bool }

[<CLIMutable>]
type LunchFilter =
    { Cuisine: Option<string>
      VegetarianOptions: Option<bool>
      VeganOptions: Option<bool> }