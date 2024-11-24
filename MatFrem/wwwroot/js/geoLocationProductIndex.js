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

    
    document.getElementById('geoJsonInput').value = geoJsonString; // Set value of hidden input field to GeoJSON string variable created

    console.log(geoJsonString); //_lastcenter shows the lat lon value 

    // Set value of hidden input field to GeoJSON string variable created
});

function Delete(url) {
    Swal.fire({
        title: "Er du sikkert på at du vil slette?",
        text: "!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Ja, slett!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (result) {
                    if (result.success) {
                        toastr.success(result.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(result.message);
                    }
                }
            });
        }
    });
}