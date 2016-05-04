$(() => {
    initCartCounter();
});

var ItemsInCart;

function initCartCounter() {
    $.getJSON("/api/cartsize", res => {
        ItemsInCart = res.Size;
        $("#cartSummary").html(`Cart(${ItemsInCart} items)`);
    });
}

function addToCart(purchaseOptionId) {
    $.put(`/api/cart/${purchaseOptionId}`, res => {
        console.log("added item " + purchaseOptionId + " to cart");
        $("#cartSummary").html(`Cart(${++ItemsInCart} items)`);
    });
}

function removeFromCart(purchaseOptionId, callback) {
    console.log("started removing..");
    $.ajax({ url: `/api/cart/${purchaseOptionId}`, type: "DELETE" }).done(() => { callback(); });
    console.log("removed item " + purchaseOptionId + " from cart");
    $("#cartSummary").html(`Cart(${--ItemsInCart} items)`);
}