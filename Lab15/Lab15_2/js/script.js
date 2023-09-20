function go()
{
    document.location.assign("https://www.wikipedia.org/");
}

function dispMes(event)
{
    document.getElementById('par').style.display = 'block';
    document.getElementById('resultX').innerHTML = event.clientX;
    document.getElementById('resultY').innerHTML = event.clientX;
}

function start()
{
    document.getElementById('par1').style.color = 'red ';
}
function stop()
{
    document.getElementById('par1').style.color = 'black ';
}

function timeMsg()
{
    var t = setTimeout("alertMsg()", 3000)
}
function alertMsg()
{
    alert("Привет");
}


var c = 0;
var timer_is_on = false;

function timedCount() 
{
  document.getElementById('txt').value = c;
  c++;
  t = setTimeout(timedCount, 1000);
}

function doTimer() {
  if (!timer_is_on)
  {
    timer_is_on = true;
    timedCount();
  }
}
function stopTimer() 
{
    clearTimeout(t);
    timer_is_on = false;
}

function startTime()
{
    var today = new Date();
    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    minutes = checkTime(minutes);
    seconds = checkTime(seconds);
    document.getElementById('time-txt').innerHTML = hours + ":" + minutes + ":" + seconds;
    t = setTimeout("startTime()", 500);
}
function checkTime(i)
{
    if(i < 10)
        i = "0" + i;
    return i;
}