$(document).ready(function () {
    $(function () {
        $("#accordion").accordion({
            collapsible: true,
            active: true,
            heightStyle: "content",
            autoHeight: false,
            clearStyle: true, 
        });
        $("#datepicker").datepicker();
    });
});

function filterMenu() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("filterInput");
    filter = input.value.toUpperCase();
    ul = document.getElementById("filterList");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";

        }
    }
}

function ChangeFavourite(clickedItem, stopName, routeShortName, directionId) {
    $.post(
        "/Stops/AddToFavourite",
        {
            stopName,
            routeShortName,
            directionId
        },
        function (data, status) {
            if (data)
                $(clickedItem).html('<img class="favouritesStarImage" src="images/starRed.svg" />');
            else
                $(clickedItem).html('<img class="favouritesStarImage" src="images/starGray.svg" />');
        }
    );
}

function RemoveFavourite(clickedItem, stopName, routeShortName, directionId) {
    $.post(
        "/Stops/AddToFavourite",
        {
            stopName,
            routeShortName,
            directionId
        },
        function (data, status) {
            if (!data)
                $(clickedItem).parent().remove();
        }
    );
}

function navigateWithDate(requestedUri) {
    window.location.href = requestedUri + '&date=' + $('#timetableDate').val(); 
}

var map;
var lat;
var lng;
var stopsArray;

function initMap() {
    console.log(stopsArray);
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: lat, lng: lng },
        zoom: 17
    });

    var marker = new google.maps.Marker({
        position: { lat: lat, lng: lng },
        map: map
    });

    stopsArray.forEach(function (item) {
        new google.maps.Marker({
            position: { lat: item.stopLat, lng: item.stopLon },
            map: map
        });
    });
}

function tripPlannerSearchParams(clickedItem) {
    $(clickedItem).attr('href', `/Stops/TripPlanner?stopFrom=${$("#stopFrom").val()}&stopTo=${$("#stopTo").val()}&date=${$("#datepicker").val()}`);
}