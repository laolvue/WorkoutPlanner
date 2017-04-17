
var publish_key = 'pub-c-67ebfbbd-a2db-4cff-a6d5-daa76c668cbe';
var subscribe_key = 'sub-c-aa11c420-230d-11e7-894d-0619f8945a4f';
var username = window.location.href.split("Email=");
username = username[1].split("&channel=")[0];
var channel = window.location.href.split("channel=")[1];

window.onload = function ()
{
    debugger
    GetName();
    initialize();
}



function GetName() {
    $.ajax({
        url: '/Chatrooms/GetUserName',
        method: 'post',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',

        success: function (data) {
            username= data;
        }
    })
}

function initialize() {
    //initialize chatroom
    pubnub = PUBNUB.init({
        publish_key: publish_key,
        subscribe_key: subscribe_key,
        uuid: username
    });


    //subscribe to receive incoming messages
    pubnub.subscribe({
        channel: channel,
        callback: function (message) {
            $('#chatHistory')[0].innerHTML = $('#chatHistory')[0].innerHTML + '<br />' + message;
        },
        presence: function (state) {
            if (state.action == 'join') {
                if ($('#userList').text().indexOf(state.uuid) == -1) {
                    $('#userList')[0].innerHTML = state.uuid + '<br />' + $('#userList')[0].innerHTML;
                }
            } else if (state.action == 'leave' || state.action == 'timeout') {
                var index = $('#userList')[0].innerHTML.indexOf(state.uuid);
                if (index !== -1) {
                    $('#userList')[0].innerHTML =
                        $('#userList')[0].innerHTML.substring(0, index) +
                        $('#userList')[0].innerHTML.substring(index + state.uuid.length + 4);
                }
            }
        }
    });

    //send message
    pubnub.bind('click', pubnub.$('sendButton'), function (e) {
        pubnub.publish({
            channel: channel,
            message: pubnub.get_uuid() + ' : ' + $('#message').val()
        });

        var message = pubnub.get_uuid() + ' : ' + $('#message').val();
        
        $.ajax({
            url: '/Chatrooms/LogChat',
            data: JSON.stringify({email: message}),
            method: 'post',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',

            success: function (data) {

            }
        })

        $('#message').val('');
    });

}


function leave() {
    pubnub.unsubscribe({
        channel: channel,
        callback: function () {
            window.location = 'http://localhost:8550/Home/Index';
        }
    });
}