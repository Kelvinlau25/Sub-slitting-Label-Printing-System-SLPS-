var count = 300;
var counter = setInterval(timer, 1000); //1000 will  run it every 1 second

function reset() {
count = 300;
}

function timer() {
    count = count - 1;
    
    if (count == -1) {
    var oIndex = document.getElementById("hidIndex");
        clearInterval(counter);
        location.href = hidIndex.value;
        return;
    }

    var seconds = count % 60;
    var minutes = Math.floor(count / 60);
    var hours = Math.floor(minutes / 60);
    minutes %= 60;
    hours %= 60;

}