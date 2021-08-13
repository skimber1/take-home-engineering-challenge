
## Variables
$CreateFoodTruckUrl = "http://localhost:46401/api/foodtrucks"

## Read in the CSV File.
$FoodTrucks = Get-Content ..\Mobile_Food_Facility_Permit.csv | ConvertFrom-Csv

Write-Host "Located $($FoodTrucks.Count) Food Trucks."
Write-Host "Importing the food trucks."

$TrucksAdded = 0
$TrucksFailed = 0

foreach ($foodTruck in $foodTrucks)
{
    try
    {
        Write-Host "Importing FoodTruck [LocationId='$($foodTruck.LocationId)']..." -NoNewline

        $webResult = Invoke-WebRequest -Uri $CreateFoodTruckUrl -Method Put -Body ($foodTruck | ConvertTo-Json) -ContentType "application/json" -UseBasicParsing

        if ($webResult.StatusCode -eq 200)
        {
            $TrucksAdded++
            Write-Host "Done!" -ForegroundColor Green
        }
        else
        {
            $TrucksFailed++
            Write-Host "Fail!" -ForegroundColor Red
        }
    }
    catch
    {
        $TrucksFailed++
        Write-Host "Fail!" -ForegroundColor Red
    }
}

Write-Host "Trucks Successfully Added: $($TrucksAdded)"
Write-Host "Trucks Failed to Add:      $($TrucksFailed)"