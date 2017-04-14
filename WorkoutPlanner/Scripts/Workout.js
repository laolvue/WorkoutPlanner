var btn = document.getElementById("btn");
btn.addEventListener("click", function () {
    var day = document.getElementById("day");
    var dayName = day.options[day.selectedIndex].value;
    var userId = document.getElementById("userId").value;
    var url = window.location.href;
    
    if (url == 'http://localhost:35171/Workouts') {
        switch (dayName) {
            case "Monday": window.location.href = 'Workouts/Index/2/'+userId;
                break;
            case "Tuesday": window.location.href = 'Workouts/Index/3' + userId;
                break;
            case "Wednesday": window.location.href = 'Workouts/Index/4' + userId;
                break;
            case "Thursday": window.location.href = 'Workouts/Index/5' + userId;
                break;
            case "Friday": window.location.href = 'Workouts/Index/6' + userId;
                break;
            case "Saturday": window.location.href = 'Workouts/Index/7' + userId;
                break;
            case "Sunday": window.location.href = 'Workouts/Index/8' + userId;
                break;
            case "Select Specific Day": window.location.href = 'Workouts/Index/1' + userId;
                break;
        }
    }

    else {
        switch (dayName) {
            case "Monday": window.location.href = '2/' + userId;
                break;
            case "Tuesday": window.location.href = '3/' + userId;
                break;
            case "Wednesday": window.location.href = '4/' + userId;
                break;
            case "Thursday": window.location.href = '5/' + userId;
                break;
            case "Friday": window.location.href = '6/' + userId;
                break;
            case "Saturday": window.location.href = '7/' + userId;
                break;
            case "Sunday": window.location.href = '8/' + userId;
                break;
            case "Select Specific Day": window.location.href = '1/' + userId;
                break;
        }
    }
    
})