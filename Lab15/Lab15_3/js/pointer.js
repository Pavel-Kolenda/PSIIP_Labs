var count = 0;

function pointerTest()
{
  var outputElement = document.getElementById("output");
  var target = document.getElementById("target");
  target.addEventListener("pointermove", incrementAndDisplay);
}

function incrementAndDisplay() {
  count++;
  outputElement.textContent = count;
}