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

var lat = geoJsonData.geometry.coordinates[1];
var lon = geoJsonData.geometry.coordinates[0];

var nominatimUrl = `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lon}&format=json`;

fetch(nominatimUrl)
    .then(response => response.json()) //Parse the JSON data from the response object to a JavaScript object
    .then(data => {                    //Gets the data from the response object
        var address = data.display_name || "Ingen addresse funnet"; //display_name is a string that contains addresses in the Nominatim json
                                                                    //we are binding the data response to display_name, then to the address variable

        document.getElementById('locationIdForm').value = address;
        
    })

    .catch(error => {
        console.error("Error during reverse geocoding:", error);
        document.getElementById('locationIdForm').value = "Error fetching address";
    });

{ 

}

function getShoppingCart() {

    //document.cookie contains all cookies in a single string
    // cookie1 = value1; cookie2 = value2; cookie3 = value3;

    const cookieName = "shopping_cart";
    let cookiesArray = document.cookie.split(';');
    for (let i = 0; i < cookiesArray.length; i++) {
        let cookie = cookiesArray[i];
        if (cookie.includes(cookieName)) {
            let cookie_value = cookie.substring(cookie.indexOf("=") + 1);

            try {
                let cart = JSON.parse(atob(cookie_value));
                return cart;
            }

            catch (exception) {
                break;
            }

        }
    }

    return {};
}

function saveCart(cart) {
    let cartString = btoa(JSON.stringify)(cart);

    //save cookie
    let d = new Date();
    d.setDate(d.getDate() + 365);
    let expires = d.toUTCString();
    document.cookie = "shopping_cart=" + cartString + ";expires=" + expires + ";path=/; SameSite=Strict; Secure"
}

function addToCart(button, id) {
    let cart = getShoppingCart();
    let quantity = cart[id]
    if (isNaN(quantity))
    {
        //if its not a number, user havent increased the quantity, cart = 1
        cart[id] = 1;
    }
    else {
        //if its a number, user have increased the quantity, cart = quantity + 1
        cart[id] = Number(quantity) + 1;
    }
    saveCart(cart);
    button.innerHTML = "Added <i class='bi bi-check-lg'></i>";

    let cartSize = 0;
    for (var cartItem of Object.entries(cart)) {
        quantity = cartItem[1];
        if (isNaN(quantity)) continue;

        cartSize += Number(quantity);

        
    }

    document.getElementById("cartSize").innerHTML = cartSize;
}


getShoppingCart();
saveCart();
addToCart();


