# Vehicle Tracking

1. Description  
This is the BackEnd platform of vehicle tracking. It's used for update current location, get current location and journey of vehicle in period time

2. Technical applied
- Asp .Net Core web API
- Identity4 for .Net Core
- Repository patten
- Entity framework Code first

3. How to run

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

4. API list

Login API
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
Response:
  VehicleNumber
  Longitude
  Latitude
  LatestUpdate
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
5. Database Structure

  I had 2 database: Vehicle and trackingHistory
  
  - For Vehicle database: it's used for management data of system and identity user
  - For TrackingHistory database: this database is used for tracking location of all vehicles. TrackingSessions table which will create 
  new record per day for each vehicle, is index of each vehicle by date. When we want to find a journey of vehicle in period time, 
  TrackingSessions like a filter that will be reduce the reading time and boost up the query so much than query directly in 
  TrackingHistories. This database will be easy for scaling and relication without touch in to identity and orther datas.
  
6. Testing user  
  For testing purpose, I created 2 user:
  - Admin: admin@test.com/12345abc
  - Vehicle User: vehicle1@test.com/12345
  
 7. Future plane to improve
  - Writing UT
  - Apply Swagger for API Doc
  - Add/Edit more informations for User, Role, Vehicle
  - Apply CIDC
  - Apply Docker
