btn2 = document.getElementById("btn2");

btn2.addEventListener("click", function () {
    var userName = document.getElementById('find').value;
    $.ajax({
        url: '/UserInfoes/FindUser',
        data: JSON.stringify({ name: userName }),
        method: 'post',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',

        success: function (data) {
            window.location.href = data;
        }
    });

})