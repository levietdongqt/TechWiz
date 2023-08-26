$(document).ready(function () {
    $('#showCartLink').click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "Get",
            url: "/showCart",
            data: $(this).serialize(),
            success: function (data) {
                var html = '';
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    html += ' <div class="cart_item">' +
                        + ' <div class="cart_img">'
                        + '<a href="#"><img width="100px" height="100px" src="' + item.product.imageUrl + '" alt="' + item.product.name + '"></a>'
                        + '</div><div class="cart_info">'
                        + '<a asp-controller="Home" asp-action="Details">Details</a>'
                        + '<input type="number" value="' + item.salePrice +'"><span> Price </span>'
                        + '</div>'
                        + '</div>'

                }
                //var html = '<div>' + data.Quantity + 'sdfdsf' + '</div>';
                $('#targetElement').append(html);

            }
        });
    });
});