$().ready(function () {
    $("#accordion").accordion({
        collapsible: true,
        active: true,
        heightStyle: "content",
        autoHeight: false,
        clearStyle: true,
    });

    $("#datepicker").datepicker({
        dateFormat: 'yy-mm-dd',
        minDate: 0
    });

    $('#timepicker').timepicker({
        timeFormat: 'HH:mm',
        interval: 15,
        defaultTime: 'now',
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });

    if ($("#datepicker").data("date") !== undefined) {
        $('#datepicker').datepicker("setDate", new Date($("#datepicker").data("date")));
    }

    if ($("#timepicker").data("date") !== undefined) {
        $('#timepicker').timepicker("setTime", new Date($("#timepicker").data("date")));
    }    
});

function filterMenu(inputElement, filterListId) {
    var filter, ul, li;
    filter = inputElement.value.toUpperCase();
    ul = document.getElementById(filterListId);
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        stopName = li[i].getAttribute("name");
        if (stopName.toUpperCase().indexOf(filter) > -1) {
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
    window.location.href = `${requestedUri}&date=${$('#datepicker').val()}T${$("#timepicker").val()}`;
}

var map;
var lat;
var lng;
var stopsArray;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: lat, lng: lng },
        zoom: 17
    });

    var startMarker = new google.maps.Marker({
        position: { lat: lat, lng: lng },
        map: map,

    });

    if (stopsArray !== undefined) {
        var bounds = new google.maps.LatLngBounds();
        bounds.extend(startMarker.getPosition());
        stopsArray.forEach(function (item) {
            if (lat !== item.stopLat && lng !== item.stopLon) {
                var marker = new google.maps.Marker({
                    position: { lat: item.stopLat, lng: item.stopLon },
                    map: map,
                    icon: "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|0000ff"
                });
                bounds.extend(marker.getPosition());
            }
        });
        map.fitBounds(bounds)
    }
}

function tripPlannerSearchParams(clickedItem) {
    $(clickedItem).attr('href', `/Stops/TripPlanner?stopFrom=${$("#stopFrom").val()}&stopTo=${$("#stopTo").val()}&date=${$("#datepicker").val()}T${$("#timepicker").val()}`);
}

function chosenItemToInput(value, inputId) {
    document.getElementById(inputId).value = value;
}

function test() {
    console.log('trol');
}