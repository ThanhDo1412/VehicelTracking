# Vehicle Tracking

## Description
This is the BackEnd platform of vehicle tracking. It's used for update current location, get current location and journey of vehicle in period time

## Technical applied
- Asp .Net Core web API
- .NET Core Identity
- Repository patten
- Entity framework Code first

## How to run
Firstly, please download project and open cmnd in this folder. Please check connectionstring before execute commands to initalize database as below: 
```
dotnet ef database update -c VehicleContext -p VehicleTracking.WebApi
dotnet ef database update -c TrackingHistoryContext -p VehicleTracking.WebApi
```
   After finished to 2 database, run commands to restore and build project:
```
dotnet restore
dotnet build
```
  Follow by this, run the webapi project by this command
```
dotnet run --project ./VehicleTracking.WebApi/VehicleTracking.WebApi.csproj
```
  Finally, project is working. Enjoy it.

## API list
Login API - After get token from Login API, please add token to header: __authorization: Bearer {Token}__
```
Method: POST
Route: api/Login
Request:
  Email - required
  Password - required
Response:
  Token for attached to another requests
```
Register API
```
Method: POST
Route: api/Register
Request:
  Email - required
  Password - required - at least 5 characters
  UserName - required - at least 5 characters
Response:
  Token for attached to another requests
```
Register Vehicle API (User Role)
```
Method: POST
Route: api/vehicle
Request:
  VehicleNumber - required
Response:
  null
```
Update vehicle location - (User Role)
```
Method: POST
Route: api/tracking/current
Request:
  VehicleNumber - required
  Longitude - required
  Latitude - required
Response:
  null
```
Get vehicle location - (Admin Role)
```
Method: GET
Route: api/tracking/current/{vehicleNumber}
Request:
  VehicleNumber - required
  IsGetAddress - optional - true/false
Response:
  VehicleNumber
  Longitude
  Latitude
  LatestUpdate
  Address - will have data when IsGetAddress = true
```
Get vehicle journey - (Admin Role)
```
Method: GET
Route: api/tracking/journey/{vehicleNumber}/{from}/{to}
Request:
  VehicleNumber - required
  From - required
  To - required
Response:
  VehicleNumber
  Array [] {
    Longitude
    Latitude
    LatestUpdate
  }
```
## Database Structure
My database structure included 2 databases:
  - For Vehicle database: it's used for managing data of system and identity user
  - For TrackingHistory database: this database is used for tracking location of all vehicles. TrackingSessions table will create 
  new record per day for each vehicle, it like index of tracking history for each vehicle. For example, when we want to find a journey of vehicle in period time, TrackingSessions like a filter that will reduce the reading time and boost the query up compare to query directly in TrackingHistories. This approach will give us the scalability and easier for replication without touching the identity and other data.
  
## Testing user  
  For testing purpose, I created 2 user:
  - Admin: admin@test.com/12345abc
  - Vehicle User: vehicle1@test.com/12345  
  
  Note for test: because I just used free api_key from Google, so that will limit request 10 times/day
  
## Future plane to improve
  - Writing UT
  - Apply Swagger for API Doc
  - Add/Edit more informations for User, Role, Vehicle
  - Apply CI/CD
  - Apply Docker
