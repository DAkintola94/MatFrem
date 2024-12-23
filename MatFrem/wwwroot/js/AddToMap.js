var map = L.map('map').setView([60.145, 10.25], 15);

var address = geoJsonData;
console.log(address);
function geoCodeAddress(address) {

    var geocodingUrl = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(address)}`;
    console.log(geocodingUrl);

    fetch(geocodingUrl)
        .then(response => response.json())
        .then(data => {
            if (data && data.length > 0) {
                var lat = parseFloat(data[0].lat);
                var lon = parseFloat(data[0].lon);

                map.setView([lat, lon], 15);

                L.marker([lat, lon]).addTo(map)
                    .bindPopup(`Address: ${address}`).openPopup();

                var mapsUrl = `https://www.google.com/maps/place/${lat},${lon}`;
                document.getElementById('googleMapsLink').href = mapsUrl;
            }

            else {
                console.error('No data found');
            }
        })

        .catch(error => {
            console.error("Error geocoding the address:", error);
        });

}

// Check if geoJsonData contains a valid address
if (typeof address !== 'undefined' && address) {
    geocodeAddress(address);
} else {
    console.error("Address data is missing or undefined.");
}