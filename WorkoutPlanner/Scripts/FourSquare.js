window.onload = GetUserToken();

var venue = [];
var token;
var venueId = [];

function GetUserToken() {
    var url = window.location.href;
    token = url.split("=")[1];
    //$.post("/Eventfuls/method" + id);
}


function AcceptCheckIn(ourData) {
    var date = new Date().toLocaleString();
    var fourSquareUserId = ourData.response.checkin.user.id;
    var checkInName = ourData.response.checkin.venue.name;
    var checkInAddress = ourData.response.checkin.venue.location.address;

    $.ajax({
        url: '/Eventfuls/GetCheckIn',
        data: JSON.stringify({ userIds: fourSquareUserId, checkInDate: date, locationName: checkInName, locationAddress: checkInAddress }),
        method: "post",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            window.location.href = data;
        }
    });
}


//GETS ADDRESSES OF SEARCHED LOCATION
var btn = document.getElementById("btn");
btn.addEventListener("click", function () {
    var place = document.getElementById("searchPlace").value;
    var ourRequest = new XMLHttpRequest();
    var bill = "";
    bill = "https://api.foursquare.com/v2/venues/search?ll=" + pos.lat + "," + pos.lng + "&query=" + place + "&client_id=3HSMBQSSNW2CWTD0GKFWRIQIDWSZNRSTIRHCWSUARTCTLBGY&client_secret=PYNMOUEEW2JUBV45CZU5EMMRZLQCYBUSB3R0DQXGYK24EZEA&m=foursquare&v=20170407";
    ourRequest.open('GET', bill);
    ourRequest.onload = function () {
        var ourData = JSON.parse(ourRequest.responseText);
        renderHTML2(ourData);
        var addresser = [];
        var addressName = [];

        for (i = 0; i < ourData.response.venues.length; i++)
        {
            if (ourData.response.venues[i].name.toLowerCase().indexOf(place.toLowerCase()) >= 0) {
                addresser.push(ourData.response.venues[i].location.address);
                addressName.push(ourData.response.venues[i].name);
                venueId.push(ourData.response.venues[i].id + "$" + ourData.response.venues[i].location.address);
            }
        }
        var geocoder = new google.maps.Geocoder;
        var count = 0;
        for (i = 0; i < addresser.length; i++) {
                if (addresser[i] != undefined && count<6)
                {
                    count++;
                    codeAddress(geocoder, addresser[i], addressName[i]);
                }
        }
    }
    ourRequest.send();
});


//OUTPUT JSON DATA ONTO CONSOLE LOG
function renderHTML2(data) {
    console.log(data);
}




var geocoder;
var map;
var pos;

//INITIALIZE MAP
function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(43.034134, -87.9141417);
    var mapOptions = {
        zoom: 8,
        center: latlng
    }
    map = new google.maps.Map(document.getElementById('map'), mapOptions);
    var geocoder = new google.maps.Geocoder;
    var infowindow = new google.maps.InfoWindow;
    geocodeLatLng(geocoder, map, infowindow);
}
var labels = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
var labelIndex = 0;


//GET CURRENT LOCATION  AND ADD MARKER FOR CURRENT LOCATION
function geocodeLatLng(geocoder, map, infowindow) {
    var infoWindow = new google.maps.InfoWindow({ map: map });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            geocoder.geocode({ 'location': pos }, function (results, status) {
                if (status === 'OK') {
                    if (results[1]) {
                        map.setZoom(11);
                        var marker = new google.maps.Marker({
                            position: pos,
                            map: map
                        });
                        infowindow.setContent("Your location: "+results[0].formatted_address);
                        infowindow.open(map, marker);
                    } else {
                        window.alert('No results found');
                    }
                } else {
                    window.alert('Geocoder failed due to: ' + status);
                }

            });
            //infoWindow.setPosition(pos);
            //infoWindow.setContent('Location found.');
            map.setCenter(pos);
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    }
    else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

//CREATE MARKER FOR ADDRESS
function codeAddress(geocoder, address, name) {

    var select = document.getElementById("year");
    var option = document.createElement('option');
    option.text = option.value = name+": "+address;
    select.add(option, 0);




    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location,
                label: labels[labelIndex++ % labels.length],
                title: name+":  "+results[0].formatted_address
            });

            venue.push(address + "?" + results[0].formatted_address);
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}


//CHECK IN
function CheckIn(){


    var address;
    var place = document.getElementById("year");
    var placeAddress = place.options[place.selectedIndex].value;
    placeAddress = placeAddress.split(": ");


    for (i = 0; i < venue.length; i++)
    {
        var testAddress = venue[i].split("?");
        if (testAddress[0] == placeAddress[1])
        {
            for (j = 0; j < venueId.length; j++)
            {
                if (venueId[j].split("$")[1] == placeAddress[1])
                {
                    address = venueId[j].split("$")[0];
                    break;
                }
            }
            break;
        }
    }

    var params = "venueId="+address+"&oauth_token="+token+"&v=20170407";
    var url = "https://api.foursquare.com/v2/checkins/add?"+params;
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.onload = function () {
        var ourData = JSON.parse(xhr.responseText);
        renderHTML2(ourData);
        AcceptCheckIn(ourData);

    }
    xhr.send();
   
}