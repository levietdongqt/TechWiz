$(document).ready(function () {
    countCart();
    $(".addToCartButton").click(function () {
        var productId = $(this).data("product-id");
<<<<<<< HEAD
        var salePrice = $(this).data("product-price");
=======
        var price = $(this).data("product-price");
        var discount = $(this).data("product-discount");
        var salePrice = price * (1 - discount);
>>>>>>> 7762fa5d9596d2e358e35280b9d6acf10462f66a
        var url2 = "/addToCart/" + productId + "/" + 1 + "/" + salePrice;
        // Gửi yêu cầu AJAX đến server để thêm sản phẩm vào giỏ hàng Lấy Id sản phẩm từ mô hình
        // Gửi yêu cầu AJAX đến server để thêm sản phẩm vào giỏ hàng
        $.ajax({
            type: "POST",
            url: url2, // Đường dẫn đến action xử lý thêm vào giỏ hàng
            success: function (response) {
                // Xử lý khi yêu cầu thành công
                if (response.success) {
                    countCart();
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Product added to cart!',
                        showConfirmButton: false,
                        timer: 1500
                    });

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Failed to add product to cart. Please try again.',
                    });
                }
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
    });
});

const countCart = () => {
    $.ajax({
        type: "POST",
        url: "/CountCart", 
        success: (response) => {
            var count = response.count;
            $("#item_count").text(count);
        }
    })
};