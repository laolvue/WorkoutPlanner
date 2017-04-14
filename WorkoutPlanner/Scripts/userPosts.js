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
           // window.location.href = data + "/1";

            $.ajax({
                url: data,
                data: JSON.stringify({ isCheckIn: 1 }),
                method: 'post',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',

                success: function (data) {
                    window.location.href = data;
                }
            });

        }
    });

})



