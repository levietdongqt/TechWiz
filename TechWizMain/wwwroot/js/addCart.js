$(document).ready(function () {
    $(".addToCartButton").click(function () {
        var productId = $(this).data("product-id");
        var salePrice = $(this).data("product-price");
        var url2 = "/addToCart/" + productId + "/" + 1 + "/" + salePrice;
        // Gửi yêu cầu AJAX đến server để thêm sản phẩm vào giỏ hàng Lấy Id sản phẩm từ mô hình
        // Gửi yêu cầu AJAX đến server để thêm sản phẩm vào giỏ hàng
        $.ajax({
            type: "POST",
            url: url2, // Đường dẫn đến action xử lý thêm vào giỏ hàng
            success: function (response) {
                // Xử lý khi yêu cầu thành công
                if (response.success) {
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