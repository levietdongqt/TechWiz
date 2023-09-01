
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
    countCart2();
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
                var baseUrl = window.location.origin; // Lấy gốc của URL (bao gồm cả port)
                var relativeUrl = "/DeleteFromCart/" + productBillId; // Phần tương đối của URL
                var fullUrl = baseUrl + relativeUrl;
                console.log("delete: " + fullUrl)
                $.ajax({
                    type: "Get",
                    url: fullUrl,
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
                                    countCart2();
                                    loadCheckout();
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
            if (data == null || data.length === 0) {
                $('.cartStatus').empty();
                $('#targetElement').empty();
                $('#targetElement').append("<h3 style='text-align: center'>Cart is empty </h3> <p> Buy something and come back here later !! </p>")
            } else {
                var html = '';
                var total = 0;
                var subTotal = 0;
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    var price = Number(item.product.price);
                    subTotal += item.product.price * item.quantity;

                    html += ' <div class="cart_item">'
                        + '<div class="cart_img">'
                        + '<a asp-controller="Home" asp-action="Details" asp-route-id="@x.Id"><img src="' + item.product.imageUrl + '" alt="' + item.product.name + '"></a>'
                        + '</div>'
                        + '<div class="cart_info price_box">'
                        + '<a asp-controller="Home" asp-action="Details" asp-route-id="@x.Id">' + item.product.name + '</a>'
                        + '<input class="itemQuantity" type="number" min=1 max=10 value="' + item.quantity + '" data-product-id = "' + item.id + '">'

                    if (item.product.discount.id != 1) {
                        price = (item.product.price * (1 - item.product.discount.percent));
                        html += '<span > x $' + price.toFixed(2) + '</span>';
                        html += '<span class = "old_price"> $ ' + item.product.price.toFixed(2) + '</span>';
                    } else {
                        html += '<span > x $' + price.toFixed(2) + '</span>';
                    }
                    total += price * item.quantity;

                    html += '</div>'
                        + '<div className="cart_remove">'
                        + '<a class="deleteFromCart" data-product-id = "' + item.id + '" href="#"><i class="fa-solid fa-xmark" style="color: #000000;"></i></a>'
                        + '</div>'
                        + '</div>'
                }
                $('#targetElement').empty();
                //var html = '<div>' + data.Quantity + 'sdfdsf' + '</div>';
                $('#targetElement').append(html)
                $('.discount').text('- $' + (-total + subTotal).toFixed(2));
                $('.subTotal').text('$' + subTotal.toFixed(2));
                $('.total').text('$' + total.toFixed(2));
            }
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
            countCart2();
            loadCheckout();
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
                    $('.discount').text('- $' + (-total + subTotal).toFixed(2));
                    $('.subTotal').text('$' + subTotal.toFixed(2));
                    $('.total').text('$' + total.toFixed(2));

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
const countCart2 = () => {
    $.ajax({
        type: "POST",
        url: "/CountCart",
        success: (response) => {
            var count = response.count;
            $("#item_count").text(count);
        }
    })
};
const loadCheckout = () => {
    $.ajax({
        type: "GET",
        url: "/ShowCart",
        success: function (data) {
            if (data == null || data.length === 0) {
                $('#playOrder22').hide();
            }
            var html = '';
            var total = 0;
            var subTotal = 0;
            for (var i = 0; i < data.length; i++) {
                var item = data[i];
                var price = Number(item.product.price);
                subTotal += price * item.quantity;

                if (item.product.discount.id != 1) {
                    price = (item.product.price * (1 - item.product.discount.percent));
                }
                total += price * item.quantity;
                html +=
                    '<tr>'
                    + '<td>'
                    + (i + 1)
                    + '</td>'
                    + '<td>'
                    + item.product.name
                    + '</td>'
                    + '<td>$'
                    + price.toFixed(2)
                    + '</td>'
                    + '<td>'
                    + item.quantity
                    + '</td>'
                    + '<td>$'
                    + (price * item.quantity).toFixed(2)
                    + '</td>'
                    + '</tr>'
            }
            $('#targetElement1').empty();
            $('#targetElement1').append(html);
            $('.discount').text('- $' + (-total + subTotal).toFixed(2));
            $('.subTotal').text('$' + subTotal.toFixed(2));
            $('.total').text('$' + total.toFixed(2));
            $("#submitTotal").val(total.toFixed(2));
        }
    });
}

