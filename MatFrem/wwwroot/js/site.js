var map = L.map('map').setView([60.145, 10.25], 15);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

geoChanges.forEach(function (change) {
    var geoJsonData = JSON.parse(change.geoJson); // Convert GeoJSON string Model to JSON object
    L.geoJSON(geoJsonData).addTo(map);

    var showInLayer = L.geoJSON(geoJsonData).bindPopup(change.description);
    showInLayer.addTo(map);

    console.log(geoJsonData);
    console.log(change.description);

});