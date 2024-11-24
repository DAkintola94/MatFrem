/**
 * Fetches and displays the address of a location based on GeoJSON data.
 *
 * This script parses the GeoJSON data from an ASP.NET model, extracts the latitude 
 * and longitude of the location, and uses the Nominatim reverse geocoding API to 
 * fetch the corresponding address. The address is then displayed in an HTML input 
 * field with the ID 'locationIdForm'.
 *
 * @param {string} modelValues - The GeoJSON data in string format (typically from an ASP.NET model).
 * @param {Object} geoJsonData - The parsed GeoJSON object containing geographical data.
 * @param {Object} response - The HTTP response object returned by the fetch request.
 * @param {Object} data - The parsed JSON data returned by Nominatim, which contains address information.
 * @param {number} lat - Latitude of the location, extracted from geoJsonData.geometry.coordinates[1].
 * @param {number} lon - Longitude of the location, extracted from geoJsonData.geometry.coordinates[0].
 * @param {string} nominatimUrl - The URL used to query the Nominatim API for reverse geocoding.
 * @returns {void} - This method doesn't return anything, but it updates the value of the input field.
 * 
 * 
 */

var geoJsonData = JSON.parse(modelValues);

console.log(geoJsonData);

var lat = geoJsonData.geometry.coordinates[1];
var lon = geoJsonData.geometry.coordinates[0];

console.log("Latitude: " + lat);
console.log("Longitude: " + lon);

var nominatimUrl = `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lon}&format=json`;

fetch(nominatimUrl)
    .then(response => response.json()) //Parse the JSON data from the response object to a JavaScript object
    .then(data => {                    //Gets the data from the response object
        var address = data.display_name || "Ingen addresse funnet"; //display_name is a string that contains addresses in the Nominatim json
        //we are binding the data response to display_name, then to the address variable

        document.getElementById('geoConvert').innerText = address;

    })

    .catch(error => {
        console.error("Error during reverse geocoding:", error);
        document.getElementById('geoConvert').value = "Error fetching address";
    });

