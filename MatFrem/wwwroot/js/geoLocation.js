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
map.addControl(drawControl);

map.on(L.Draw.Event.CREATED, function (e) { // On draw event
    var type = e.layerType,
        layer = e.layer;

    drawItems.addLayer(layer); // Add layer to map

    var geoJsonData = layer.toGeoJSON(); // Convert layer to GeoJSON
    var geoJsonString = JSON.stringify(geoJsonData); // Convert GeoJSON to string!! since we sending it to server

    document.getElementById('geoId').value = geoJsonString;
    // Set value of hidden input field to GeoJSON string variable created

    console.log(geoJsonString); //_lastcenter shows the lat lon value 

    // Set value of hidden input field to GeoJSON string variable created
});