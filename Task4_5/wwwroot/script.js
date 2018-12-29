$(document).ready(function () {
    $(document).ajaxStart(function () {
        $("#loading").show();
    }).ajaxStop(function () {
        $("#loading").hide();
    });
});

$('#search').keyup(function () {
    var urlForJson = "/api/talents";
    
    var urlForCloudImage = "https://res.cloudinary.com/potbottom/image/upload/v1545019478/images/";

    var searchField = $('#search').val();

    var myExp = new RegExp(searchField, "i");
    $.getJSON(urlForJson, function (data) {
        var output = '<ul class="searchresults">';
        $.each(data, function (key, val) {
            //for debug
            console.log(myExp);
            if ((val.name.search(myExp) !== -1) ||
			(val.bio.search(myExp) !== -1)) {
                output += '<li>';
                output += '<h2>' + val.name + '</h2>';
                output += '<img src="' + urlForCloudImage + val.shortName + '_tn.jpg" alt="images/' + val.shortName + '_tn.jpg" />';
                output += '<p>' + val.bio + '</p>';
                output += '</li>';
            }
        });
        output += '</ul>';
        $('#update').html(output);
    }); //get JSON
});
