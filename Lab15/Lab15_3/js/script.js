function Hello()
{
    alert("Hello");
}

function handleKeyPress(event)
{
    var input = document.getElementById("userInput");
    var userInput = input.value;
    if(event.key === "Enter")
    {
        alert("Вы ввели: " + userInput);
        document.getElementById("userInput").value = ""; 
    }
}

function dragStart(event) {
    event.dataTransfer.setData("text", event.target.id);
  }
  
  function allowDrop(event) {
    event.preventDefault();
  }
  
  function drop(event) {
    event.preventDefault();
    var data = event.dataTransfer.getData("text");
    event.target.appendChild(document.getElementById(data));
  }