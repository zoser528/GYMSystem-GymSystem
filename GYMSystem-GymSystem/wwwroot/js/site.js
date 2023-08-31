// currentTime.js
document.addEventListener("DOMContentLoaded", function () {
    updateCurrentTime();
    setInterval(updateCurrentTime, 1000); // Update every second
});

function updateCurrentTime() {
    var currentTimeElement = document.getElementById("currentTime");
    if (currentTimeElement) {
        var now = new Date();
        var formattedTime = now.toLocaleTimeString(); // Format the time as needed
        currentTimeElement.textContent = formattedTime;
    }
}
0