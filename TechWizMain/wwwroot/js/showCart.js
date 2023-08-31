
// Select the cart container
const cartContainer = document.getElementById('targetElement');

// Function to check if scrollbar is needed
function checkScroll() {
    if (cartContainer.scrollHeight > cartContainer.offsetHeight) {
        cartContainer.classList.add('scrolled');
    } else {
        cartContainer.classList.remove('scrolled');
    }
}

// Initialize
checkScroll();
// Check when new item added
cartContainer.addEventListener('DOMNodeInserted', checkScroll);
$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/CountCart",
        success: (response) => {
            var count = response.count;
            $("#item_count").text(count);
        }
    })
    $('#showCartLink').click(function (e) {
        e.preventDefault();
        showCartt();
    });
    $(document).on("change", ".itemQuantity", function () {
        var productId = $(this).data("product-id");
        var newValue = $(this).val();

        updateCart(newValue, productId);

    });
});
$(document).ready(function () {
    $(document).on("click", ".deleteFromCart", function (e) {
        e.preventDefault();
        Swal.fire({
            icon: 'question',
            title: 'Confirmation',
            text: 'This item will be remove from  the cart?',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
            allowOutsideClick: false
        }).then((result) => {
            if (result.isConfirmed) {
                var productBillId = $(this).data("product-id");
                var url2 = "DeleteFromCart/" + productBillId;
                $.ajax({
                    type: "Get",
                    url: url2,
                    contentType: false,
                    processData: false,
                    data: $(this).serialize(),
                    success: function (data) {
                        if (data.success) {
                            // Display SweetAlert2 success message with confirmation
                            Swal.fire({
                                icon: 'success',
                                title: 'Success',
                                text: 'Delete successfully!',
                                showConfirmButton: true,
                                confirmButtonText: 'OK',
                                allowOutsideClick: false // Prevent closing on backdrop click
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    showCartt();
                                }
                            });
                        } else {
                            // Display SweetAlert2 error message
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: 'Delete failed. Please try again later.'
                            });
                        }
                    }
                });
            }
        });

    });

});
const showCartt = () => {
    $.ajax({
        type: "Get",
        url: "/showCart",
        data: $(this).serialize(),
        success: function (data) {
            var html = '';
            var total = 0;
            var subTotal = 0;
            for (var i = 0; i < data.length; i++) {
                var item = data[i];
                var price = Number(item.product.price);
                subTotal += item.product.price * item.quantity;
                if (item.product.discount.id != 1) {
                    price = (item.product.price * (1 - item.product.discount.percent));
                }
                total += price * item.quantity;
                html += ' <div class="cart_item">'
                    + '<div class="cart_img">'
                    + '<a asp-controller="Home" asp-action="Details" asp-route-id="@x.Id"><img src="' + item.product.imageUrl + '" alt="' + item.product.name + '"></a>'
                    + '</div>'
                    + '<div class="cart_info">'
                    + '<a asp-controller="Home" asp-action="Details" asp-route-id="@x.Id">' + item.product.name + '</a>'
                    + '<input class="itemQuantity" type="number" min=1 max=10 value="' + item.quantity + '" data-product-id = "' + item.id + '"><span> x $' + price.toFixed(2) + '</span>'
                    + '</div>'
                    + '<div className="cart_remove">'
                    + '<a class="deleteFromCart" data-product-id = "' + item.id + '" href="#"><i class="fa-solid fa-xmark" style="color: #000000;"></i></a>'
                    + '</div>'
                    + '</div>'
            }
            $('#targetElement').empty();
            //var html = '<div>' + data.Quantity + 'sdfdsf' + '</div>';
            $('#targetElement').append(html)
            $('#subTotal').text('$' + subTotal.toFixed(2));
            $('#total').text('$' + total.toFixed(2));

        }
    });

}
const updateCart = (newVal, id) => {

    console.log("ID: " + id)
    var url2 = "/UpdateCart/" + id + "/" + newVal;
    // Gửi yêu cầu AJAX đến server để thêm sản phẩm vào giỏ hàng Lấy Id sản phẩm từ mô hình
    // Gửi yêu cầu AJAX đến server để thêm sản phẩm vào giỏ hàng
    $.ajax({
        type: "POST",
        url: url2, // Đường dẫn đến action xử lý thêm vào giỏ hàng
        success: function (response) {
            // Xử lý khi yêu cầu thành công
            $.ajax({
                type: "POST",
                url: "/CountCart",
                success: (response) => {
                    var count = response.count;
                    $("#item_count").text(count);
                }
            })
            $.ajax({
                type: "Get",
                url: "/showCart",
                data: $(this).serialize(),
                success: function (data) {
                    var total = 0;
                    var subTotal = 0;
                    for (var i = 0; i < data.length; i++) {
                        var item = data[i];
                        var price = Number(item.product.price)
                        subTotal += item.product.price * item.quantity;
                        if (item.product.discount.id != 1) {
                            price = (item.product.price * (1 - item.product.discount.percent));
                        }
                        total += price * item.quantity;
                    }
                    console.log(total);
                    $('#subTotal').text('$' + subTotal.toFixed(2));
                    $('#total').text('$' + total.toFixed(2));

                }
            });

        },
        error: function () {
            // Xử lý khi yêu cầu thất bại
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'An error occurred. Please try again later.',
            });
        }
    });
}

