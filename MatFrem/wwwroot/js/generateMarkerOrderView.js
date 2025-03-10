var markerGroup1 = L.markerClusterGroup();
var markerGroup2 = L.markerClusterGroup();
var markerGroup3 = L.markerClusterGroup();
var markerGroup4 = L.markerClusterGroup();
var markerGroup5 = L.markerClusterGroup();

var parentMarkerGroup = [markerGroup1, markerGroup2, markerGroup3, markerGroup4, markerGroup5];

// The function below gets its parameter value from the HTML in driverpage.
function generateMarkers(id, geoJsonString, orderStatus) {

    //the id, geosrting and orderstatus are being passed from the html, the function names is used to get the data for the parameter

    console.log(id, geoJsonString, orderStatus);
    var geoJson = L.geoJson(geoJsonString,
        {
            onEachFeature: function (feature, layer) {
                var centroid = turf.centroid(feature);
                var cenLon = centroid.geometry.coordinates[0];
                var cenLat = centroid.geometry.coordinates[1];
                var marker = L.marker([cenLat, cenLon]);
                console.log(marker);
                var title = `<a>OrderID: ${id}</a>`; //Creates a link to the order overview page, attach the id to the link
                var status = `<a>Status: ${orderStatus}</a>`;
                var pickInfo = `<a>Hentes her</a>`;
                marker.bindPopup(title + "<br>" + status + "<br>" + pickInfo);
                console.log(title + status);

                marker.on('click', function (e) {
                    $('#previewInfo').collapse('show');
                    $.get(`/Driver/OrderOverview/${id}`, function (data) {
                        $('#loadPreview').html(data);
                    });
                });

                parentMarkerGroup.forEach(function (group) {
                    group.addLayer(marker); //Add layer to map, loop through the parentMarkerGroup array and add the marker to the group
                });
            }
        });
}

document.addEventListener("DOMContentLoaded", function () {

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

    map.addLayer(markerGroup1); // Add marker group to map


    // Functionality for hiding preview card
    map.on('click', function (e) {
        $('#previewInfo').collapse('hide');
    });

    map.setZoom(8); // Set zoom level of map
});
