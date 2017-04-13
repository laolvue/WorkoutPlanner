

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

        for (i = 0; i < ourData.response.venues.length; i++)
        {
            if (ourData.response.venues[i].name.toLowerCase().indexOf(place.toLowerCase()) >= 0) {
                addresser.push(ourData.response.venues[i].location.address);
            }
        }
        var geocoder = new google.maps.Geocoder;
        var count = 0;
        for (i = 0; i < addresser.length; i++) {
                if (addresser[i] != undefined && count<6)
                {
                    count++;
                    codeAddress(geocoder, addresser[i]);
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
                        infowindow.setContent(results[0].formatted_address);
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
function codeAddress(geocoder, address) {

    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

//CHECK IN
btn4 = document.getElementById("btn4");
btn4.addEventListener("click", function () {
    var params = "venueId=4e7f4653b6347496661df558&oauth_token=MAG2XPHY0FR51H3GGOHPPCP5KUPHGDR4UFVTCKHKH4UPZDLP&v=20170407";
    var url = "https://api.foursquare.com/v2/checkins/add?"+params;
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    debugger
    xhr.onload = function () {
        var ourData = JSON.parse(xhr.responseText);
        renderHTML2(ourData);
    }
    xhr.send();

    
})


////Get AUTH TOKEN function
//var btn646 = document.getElementById("btn646");
//btn646.addEventListener("click", function () {
//    debugger
//    var place = document.getElementById("searchPlace").value;
//    var ourRequest = new XMLHttpRequest();
//    var bill = "";
//    bill = "https://foursquare.com/oauth2/access_token?client_id=N3VEEGBW2Y31BO4MOAEKR02JBGIW3UVXTMSHZO4JW1JZKMW3&client_secret=EAPQEHISZZ4DU2E4AWHNK4MSOVBIWGKLYTD0WYGFDIZWIK1W&grant_type=authorization_code&redirect_uri=http://localhost:8550/Home/Index&code=MAG2XPHY0FR51H3GGOHPPCP5KUPHGDR4UFVTCKHKH4UPZDLP";
//    ourRequest.open('GET', bill);
//    ourRequest.onload = function () {
//        var ourData = JSON.parse(ourRequest.responseText);
//        renderHTML2(ourData);
//    }
//    ourRequest.send();
//});