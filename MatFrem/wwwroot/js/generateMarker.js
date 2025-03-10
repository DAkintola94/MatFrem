var markerGroup;

// The function below gets its parameter value from the HTML in driverpage.
function generateMarkers(id, geoJsonString, orderStatus) {
    console.log(geoJsonString + " is the coordination");

    // Parse the geoJsonString into a JavaScript object
    var geoJson = JSON.parse(geoJsonString);

    L.geoJson(geoJson, {
        onEachFeature: function (feature, layer) {
            var centroid = turf.centroid(feature);
            var cenLon = centroid.geometry.coordinates[0];
            var cenLat = centroid.geometry.coordinates[1];
            var marker = L.marker([cenLat, cenLon]);
            var title = `<a>${id}</a>`;
            var status = `<a>${orderStatus}</a>`;
            marker.bindPopup(title + "<br>" + status);
            marker.on('click', function (e) {
                $('#previewInfo').collapse('show');
                $.get(`/Driver/DriverPage/${id}`, function (data) {
                    $('#loadPreview').html(data);
                });
            });
            markerGroup.addLayer(marker);
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    var map = L.map('map', {
        center: [60.14, 10.25],
        zoom: 6,
        zoomControl: false
    });

    // Define base maps
    var baseMaps = {
        "OpenStreetMap": L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: 'Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        })
    };

    // Add the default base map to the map
    baseMaps["OpenStreetMap"].addTo(map);

    L.control.zoom({
        position: 'bottomright'
    }).addTo(map);

    L.control.layers(baseMaps).addTo(map);

    // Define a FeatureGroup to manage markers
    markerGroup = new L.FeatureGroup();
    map.addLayer(markerGroup);

    // Add a default marker to verify the map is working
    var defaultMarker = L.marker([60.14, 10.25]).addTo(map);
    defaultMarker.bindPopup("Default Marker").openPopup();

    // Functionality for hiding preview card
    map.on('click', function (e) {
        $('#previewInfo').collapse('hide');
    });
});
