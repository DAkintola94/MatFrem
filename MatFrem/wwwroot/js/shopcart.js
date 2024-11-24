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
    let cartString = btoa(JSON.stringify(cart));

    //save cookie
    let d = new Date();
    d.setDate(d.getDate() + 365);
    let expires = d.toUTCString();
    document.cookie = "shopping_cart=" + cartString + ";expires=" + expires + ";path=/; SameSite=Strict; Secure"
}

function addToCart(button, id) {
    let cart = getShoppingCart();
    let quantity = cart[id]
    if (isNaN(quantity)) {
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

    document.getElementById("CartSize").innerHTML = cartSize;
}
