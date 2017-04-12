btn = document.getElementById("btn");

btn.addEventListener("click", function ()
{
    var post = document.getElementById('userPost').value;
    var date = new Date().toLocaleString();
    $.ajax({
        url: '/userPosts/PostMessage',
        data: JSON.stringify({ message: post, dateSent: date }),
        method: 'post',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',

        success: function (data) {
            window.location.href = data;
        }
    });

    //$.ajax(option).success(function (data) {
    //    alert(data);
    //    window.location.href = 'Workouts/Index/2';
    //})
    ////"Monday": window.location.href = 'Workouts/Index/2';

})
