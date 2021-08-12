# SAFTehnika back-end task
.Net web API service that returns data in json format
## Build/Run
* Clone project on your computer
* Run command from terminal 
```
dotnet run
```
## Requierments
* .Net Framework installed
## View data
Opening link `https://localhost:5001/sensors` in your browser should return following data

```
[{"id":22,"name":"Classroom","metrics":[{"id":1,"name":"Temperature","unit":"°C","value":23.45,"time":"2019-09-09 23:53:27"},{"id":2,"name":"Humidity","unit":"%","value":57,"time":"2019-09-09 23:53:27"}]},{"id":34,"name":"Hall","metrics":[{"id":1,"name":"Temperature","unit":"°C","value":23.85,"time":"2019-09-09 23:56:26"},{"id":2,"name":"Humidity","unit":"%","value":49,"time":"2019-09-09 23:56:26"}]},{"id":58,"name":"Gate","metrics":[{"id":1,"name":"Temperature","unit":"°C","value":18.8,"time":"2019-09-09 23:59:22"}]},{"id":73,"name":"Lobby","metrics":[]},{"id":97,"name":"Meeting room","metrics":[{"id":1,"name":"Temperature","unit":"°C","value":25.45,"time":"2019-09-06 10:39:26"},{"id":2,"name":"Humidity","unit":"%","value":41,"time":"2019-09-06 10:39:26"},{"id":3,"name":"CO₂","unit":"ppm","value":659,"time":"2019-09-06 10:39:26"},{"id":4,"name":"Atmospheric Pressure","unit":"Pa","value":101680,"time":"2019-09-06 10:39:26"}]}]
```


`https://localhost:5001/sensors/2019-09-09` 
```
[{"id":22,"name":"Classroom","metrics":[{"id":1,"min":22.55,"max":23.65},{"id":2,"min":52,"max":58}]},{"id":34,"name":"Hall","metrics":[{"id":1,"min":23.25,"max":25.6},{"id":2,"min":49,"max":59}]},{"id":58,"name":"Gate","metrics":[{"id":1,"min":15.7,"max":26.95}]}]
```
