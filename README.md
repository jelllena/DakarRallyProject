# DakarRallyProject

README.md
*** POST api/races AddRaceAndVehicles.json for initial import race + race vehicles

Create race (parameters: year)
POST api/races/{year}

Add vehicle to the race (parameters: vehicle)
PUT api/races/{id}/vehicle

Update vehicle info (parameters: vehicle) PUT: api/vehicles

Remove vehicle from the race: (parameters: vehicle identifier)

DELETE api/races/vehicle/{id}

Start the race (parameters: race identifier)
POST api/races/{id}/run

Get leaderboard including all vehicles
GET api/statistic/leaderboard

Get leaderboard for specific vehicle type: cars, trucks, motorcycles (parameters: type)
GET api/statistic/leaderboard/type

Get vehicle statistics: distance, malfunction statistics, status, finish time (parameters: vehicle identifier)
GET: api/statistic/vehicles/{id}/

Find vehicle(s) (parameters: team AND/OR model AND/OR manufacturing date AND/OR status AND/OR distance, sort order)
GET: api/statistic/vehicles/filter/?team={}?model={}?date={}?isFinish=?distance={}

Get race status that includes: race status (pending, running, finished), number of vehicles grouped by vehicle status, number of vehicles grouped by vehicle type (parameters: race identifier)
GET api/statistic/races/{id}
