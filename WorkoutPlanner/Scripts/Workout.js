var btn = document.getElementById("btn");
var userId = document.getElementById("idOfUser").name;



btn.addEventListener("click", function () {
    var day = document.getElementById("day");
    var dayName = day.options[day.selectedIndex].value;
    var url = window.location.href;
    
    debugger
    switch (dayName) {
        case "Monday": window.location.href = 'http://localhost:8550/Workouts/Index?id=2&userId=' + userId;
            break;
        case "Tuesday": window.location.href = 'http://localhost:8550/Workouts/Index?id=3&userId=' + userId;
            break;
        case "Wednesday": window.location.href = 'http://localhost:8550/Workouts/Index?id=4&userId=' + userId;
            break;
        case "Thursday": window.location.href = 'http://localhost:8550/Workouts/Index?id=5&userId=' + userId;
            break;
        case "Friday": window.location.href = 'http://localhost:8550/Workouts/Index?id=6&userId=' + userId;
            break;
        case "Saturday": window.location.href = 'http://localhost:8550/Workouts/Index?id=7&userId=' + userId;
            break;
        case "Sunday": window.location.href = 'http://localhost:8550/Workouts/Index?id=8&userId=' + userId;
            break;
        case "Select Specific Day": window.location.href = 'http://localhost:8550/Workouts/Index?id=1&userId=' + userId;
            break;
    }

    

    //if (url == 'http://localhost:35171/Workouts') {
    //    switch (dayName) {
    //        case "Monday": window.location.href = 'Workouts/Index?id=2&userId='+userId;
    //            break;
    //        case "Tuesday": window.location.href = 'Workouts/Index?id=3&userId=' + userId;
    //            break;
    //        case "Wednesday": window.location.href = 'Workouts/Index/4' + userId;
    //            break;
    //        case "Thursday": window.location.href = 'Workouts/Index/5' + userId;
    //            break;
    //        case "Friday": window.location.href = 'Workouts/Index/6' + userId;
    //            break;
    //        case "Saturday": window.location.href = 'Workouts/Index/7' + userId;
    //            break;
    //        case "Sunday": window.location.href = 'Workouts/Index/8' + userId;
    //            break;
    //        case "Select Specific Day": window.location.href = 'Workouts/Index/1' + userId;
    //            break;
    //    }
    //}

    //else {
    //    switch (dayName) {
    //        case "Monday": window.location.href = '?id=2&userId=' + userId;
    //            break;
    //        case "Tuesday": window.location.href = '?id=3&userId=' + userId;
    //            break;
    //        case "Wednesday": window.location.href = '4/' + userId;
    //            break;
    //        case "Thursday": window.location.href = '5/' + userId;
    //            break;
    //        case "Friday": window.location.href = '6/' + userId;
    //            break;
    //        case "Saturday": window.location.href = '7/' + userId;
    //            break;
    //        case "Sunday": window.location.href = '8/' + userId;
    //            break;
    //        case "Select Specific Day": window.location.href = '1/' + userId;
    //            break;
    //    }
    //}
    
})