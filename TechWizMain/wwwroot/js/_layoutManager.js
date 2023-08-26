$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/CountCart",
        success: (response) => {
            var count = response.count;
            $("#item_count").text(count);
        }
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