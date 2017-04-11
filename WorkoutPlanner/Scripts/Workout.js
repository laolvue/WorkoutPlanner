var btn = document.getElementById("btn");
btn.addEventListener("click", function () {
    var day = document.getElementById("day");
    var dayName = day.options[day.selectedIndex].value;
    var url = window.location.href;
    if (url == 'http://localhost:35171/Workouts') {
        switch (dayName) {
            case "Monday": window.location.href = 'Workouts/Index/2';
                break;
            case "Tuesday": window.location.href = 'Workouts/Index/3';
                break;
            case "Wednesday": window.location.href = 'Workouts/Index/4';
                break;
            case "Thursday": window.location.href = 'Workouts/Index/5';
                break;
            case "Friday": window.location.href = 'Workouts/Index/6';
                break;
            case "Saturday": window.location.href = 'Workouts/Index/7';
                break;
            case "Sunday": window.location.href = 'Workouts/Index/8';
                break;
            case "Select Specific Day": window.location.href = 'Workouts/Index/1';
                break;
        }
    }
    //else if (url == ('http://localhost:35171/Workouts/index/' + '1') || ('http://localhost:35171/Workouts/index/' + '2') || ('http://localhost:35171/Workouts/index/' + '3') || ('http://localhost:35171/Workouts/index/' + 4) || ('http://localhost:35171/Workouts/index/' + '5') || ('http://localhost:35171/Workouts/index/' + '6') || ('http://localhost:35171/Workouts/index/' + '7') || ('http://localhost:35171/Workouts/index/' + '8')) {
    //    switch (dayName) {
    //        case "Monday": window.location.href = '2';
    //            break;
    //        case "Tuesday": window.location.href = '3';
    //            break;
    //        case "Wednesday": window.location.href = '4';
    //            break;
    //        case "Thursday": window.location.href = '5';
    //            break;
    //        case "Friday": window.location.href = '6';
    //            break;
    //        case "Saturday": window.location.href = '7';
    //            break;
    //        case "Sunday": window.location.href = '8';
    //            break;
    //        case "Select Specific Day": window.location.href = '1';
    //            break;
    //    }
    //}
    
    else {
        switch (dayName) {
            case "Monday": window.location.href = '2';
                break;
            case "Tuesday": window.location.href = '3';
                break;
            case "Wednesday": window.location.href = '4';
                break;
            case "Thursday": window.location.href = '5';
                break;
            case "Friday": window.location.href = '6';
                break;
            case "Saturday": window.location.href = '7';
                break;
            case "Sunday": window.location.href = '8';
                break;
            case "Select Specific Day": window.location.href = '1';
                break;
        }
    }
    
})