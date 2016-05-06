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
    $.ajax({
        url: `/api/cart/${purchaseOptionId}`,
        dataType: "json",
        type: "PUT",
        success: (data) => {
            if (data.result === true) {
                $("#cartSummary").html(`Cart(${++ItemsInCart} items)`);
                console.log("added item " + purchaseOptionId + " from cart");
            }
        }
    });
}

function removeFromCart(purchaseOptionId, callback) {
    console.log("started removing..");
    $.ajax({ url: `/api/cart/${purchaseOptionId}`, type: "DELETE" }).done(() => {
        $("#cartSummary").html(`Cart(${--ItemsInCart} items)`);
        callback();
        console.log("removed item " + purchaseOptionId + " from cart");
    });
    
}