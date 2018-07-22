﻿function filterMenu() {
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

function initMap() {
    console.log(lat);
    console.log(lng);
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: lat, lng: lng },
        zoom: 17
    });
    var marker = new google.maps.Marker({
        position: { lat: lat, lng: lng },
        map: map,
        title: 'Hello World!'
    });
}