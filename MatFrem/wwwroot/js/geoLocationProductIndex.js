var map = L.map('map').setView([60.145, 10.25], 15);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

var drawItems = new L.FeatureGroup();
map.addLayer(drawItems);

var drawControl = new L.Control.Draw({
    draw: {
        polyline: true,
        polygon: true,
        circle: false,
        marker: true,
        rectangle: true
    },
    edit: {
        featureGroup: drawItems,
    }
});
map.addControl(drawControl); // Add control to map

map.on(L.Draw.Event.CREATED, function (e) { 
    var type = e.layerType,
        layer = e.layer;

    drawItems.addLayer(layer); // Add layer to map

    var geoJsonData = layer.toGeoJSON(); // Convert layer to GeoJSON
    var geoJsonString = JSON.stringify(geoJsonData); // Convert GeoJSON to string!! since we sending it to server



    // Fetches and displays the address of a location based on GeoJSON data.

    var lat = geoJsonData.geometry.coordinates[1]; //Extracts the lat of the location from the GeoJSON data (not the JSON.Stringy!)
    var lon = geoJsonData.geometry.coordinates[0]; //--

    console.log("Latitude: " + lat);
    console.log("Longitude: " + lon);

    var nominatimUrl = `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lon}&format=json`;

    fetch(nominatimUrl)
        .then(response => response.json()) //Parse the JSON data from the response object to a JavaScript object
        .then(data => {                    //Gets the data from the response object
            var address = data.display_name || "Ingen addresse funnet"; //display_name is a string that contains addresses in the Nominatim json
            //we are binding the data response to display_name, then to the address variable

            
            document.getElementById('geoJsonInput').value = address; // Set value of input field in the html to the address (the variable)

        })

        .catch(error => {
            console.error("Error during reverse geocoding:", error);
            document.getElementById('geoConvert').value = "Error fetching address";
        });
    
  

   

    // Set value of hidden input field to GeoJSON string variable created
});


