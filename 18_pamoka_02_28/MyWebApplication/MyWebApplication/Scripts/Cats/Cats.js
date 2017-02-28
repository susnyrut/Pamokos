$(document).ready(function() {

    $(".ShowDetailsUgly").click(function() {
        var catId = $(this).data("cat-id");

        window.location.href = 'Cats/Cat?catId=' + catId;
    });

    $(".ShowDetailsNice").click(function () {
        var catId = $(this).data("cat-id");

        window.location.href = 'Cats/' + catId;
    });
});
