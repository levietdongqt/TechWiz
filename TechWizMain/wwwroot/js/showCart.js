
// Select the cart container
const cartContainer = document.getElementById('targetElement');

// Function to check if scrollbar is needed
function checkScroll() {
    if(cartContainer.scrollHeight > cartContainer.offsetHeight) {
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
    $('#showCartLink').click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "Get",
            url: "https://localhost:5000/showCart",
            data: $(this).serialize(),
            success: function (data) {
                var html = '';
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    html += ' <div class="cart_item">'
                        +           '<div class="cart_img">'
                        +               '<a asp-controller="Home" asp-action="Details" asp-route-id="@x.Id"><img src="' + item.product.imageUrl + '" alt="' + item.product.name + '"></a>'
                        +           '</div>' 
                        +           '<div class="cart_info">'
                        +               '<a asp-controller="Home" asp-action="Details" asp-route-id="@x.Id">' + item.product.name + '</a>'
                        +                  '<input type="number" value="' + item.quantity +'"><span> x $'+ item.product.price + '</span>'
                        +            '</div>'
                        +             '<div className="cart_remove">'
                        +             '<a href="#"><i class="fa-solid fa-xmark" style="color: #000000;"></i></a>'
                        +              '</div>'
                        +       '</div>'
                }
                //var html = '<div>' + data.Quantity + 'sdfdsf' + '</div>';
                $('#targetElement').append(html);

            }
        });
    });
});