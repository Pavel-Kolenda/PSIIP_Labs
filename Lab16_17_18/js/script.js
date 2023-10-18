function handleSubmit(event) {
    event.preventDefault();
    var name = document.getElementById("name").value;
    if(name === "")
    {
        alert("Введите имя");
        setInputBackroundColor("name", "red");
        return;
    }
    setInputBackroundColor("name", "white");

    var email = document.getElementById("email").value;
    var emailRegex = /^[^\s@?*:%№"!(*?)]+@[^\s@#$%^&*]+\.[^\s@#$%^&*]+$/;2
    if(email === "" || !emailRegex.test(email))
    {
        alert("Введите корректный email");
        setInputBackroundColor("email", "red");
        return;
    }
    setInputBackroundColor("email", "white");
    var password = document.getElementById("password").value;
    var passwordRegex = /^(?=.*[0-9])(?=.*[a-zA-Z])(?=.*[\s!@#$%^&*]).*$/;
    if(password === "" || password.length < 8 || !passwordRegex.test(password))
    {
        alert("Пароль должен быть больше или равен 8 символам и содержать" +
        " один из специальных символов ! @ # $ % ^ & * ");
        setInputBackroundColor("password", "red");
        return;
    }
    setInputBackroundColor("password", "white");
    var age = document.getElementById("age").value;
    if(age <= 14)
    {
        alert("Дождитесь пока вам исполнится 14 лет");
        setInputBackroundColor("age", "red");
        return;
    }
    if(age >= 100)
    {
        alert("Валидный возраст от 15 до 99");
        setInputBackroundColor("age", "red");
        return;
    }
    setInputBackroundColor("age", "white");
    var gender = document.getElementById("gender").value;
    var check = document.getElementById("check").checked;
    if(check !== true)
    {  
        alert("Согласитесь с Privacy Policy");
        return;
    }
    
    var message = document.getElementById("message").value;
    if(message === "")
    {
        alert("Введите сообщение");
        setInputBackroundColor("message", "red");
        return;
    }
    setInputBackroundColor("message", "white");

    var outputDiv = document.getElementById("output");
    outputDiv.innerHTML = "";
    
    createParagraph("Имя: " + name, outputDiv);
    createParagraph("Email: " + email, outputDiv);
    createParagraph("Пароль: " + password, outputDiv);
    createParagraph("Возраст: " + age, outputDiv);
    createParagraph("Пол: " + gender, outputDiv);
    createParagraph("Согласны с Privacy Policy: " + check, outputDiv);
    createParagraph("Сообщение: " + message, outputDiv);

    AddCookie("name", name);
    AddCookie("age", age);
    AddCookie("gender", gender);
}


function AddCookie(varName, varValue){
    document.cookie = varName + "=" + varValue + "; ";
}

function createParagraph(text, parentElement) {
    var paragraph = document.createElement("p");
    paragraph.innerText = text;
    parentElement.appendChild(paragraph);
}

function setInputBackroundColor(elementId, backgroundColor){
    document.getElementById(elementId).style.backgroundColor = backgroundColor;
}



// save to cookies, local storage, convert to json