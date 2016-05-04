$(() => {
    loadCart();
});

function loadCart() { //Load all items currently in cart
    $("#cartTable tbody").html("");
    $.getJSON("/api/cart", res => {
        $("#cartPageCost").html(res.TotalPrice);
        $("#cartPageSize").html(res.ItemsInCart);
        for (let i = 0; i < res.Items.length; i++) {
            var guid = res.Items[i].PurchaseOptionId;
            var rowbtns = `<img onclick="removeFromCartPage('${guid.toString()}');$(this).closest('tr').remove();" class="cartRemoveBtn" src="/content/img/remove-icon.png" alt="${("Remove " + res.Items[i].Name)}" />`;
            $("#cartTable").append(`<tr><td>${res.Items[i].Name}</td><td>${res.Items[i].Resolution}</td><td>${res.Items[i].Price}</td><td>${rowbtns}</td></tr>`);
        }
    });
};

function removeFromCartPage(purchaseOptionId) {
    removeFromCart(purchaseOptionId, () => {
        console.log("refetching price etc..");
        $.getJSON("/api/cart", res => {
            $("#cartPageCost").html(res.TotalPrice);
            $("#cartPageSize").html(res.ItemsInCart);
        });
    });
}