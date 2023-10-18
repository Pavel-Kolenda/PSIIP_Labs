function handleSubmit(event) {
    event.preventDefault();
    const userObject = {};  

    var name = document.getElementById("name").value;
    if(!isInputValid("name", "Введите имя", name === "")) 
        return;
    userObject["name"] = name;

    var email = document.getElementById("email").value;
    var emailRegex = /^[^\s@?*:%№"!(*?)]+@[^\s@#$%^&*]+\.[^\s@#$%^&*]+$/;
    if(!isInputValid("email", "Введите корректный email", email === ""))
        return;
    if(!isInputValid("email", "Перепроверьте правильность введеного email", !emailRegex.test(email)))
        return;
    userObject["email"] = email;

    var password = document.getElementById("password").value;
    var passwordRegex = /^(?=.*[0-9])(?=.*[a-zA-Z])(?=.*[\s!@#$%^&*]).*$/;    
    if(!isInputValid("password", "Пароль должен быть больше или равен 8 символам и содержать один из специальных символов ! @ # $ % ^ & * ",
    password === "" || password.length < 8 || !passwordRegex.test(password)))
        return;
    userObject["password"] = password;

    var age = document.getElementById("age").value;
    if(!isInputValid("age", "Дождитесь пока вам исполнится 14 лет", age <= 14)) 
        return;
    if(!isInputValid("age", "Валидный возраст от 15 до 99", age >= 100)) 
        return;
    userObject["age"] = age;

    var gender = document.getElementById("gender").value;
    userObject["gender"] = gender;
    var agreeWithPrivacyPolicy = document.getElementById("check").checked;
    if(!agreeWithPrivacyPolicy)
    {  
        alert("Согласитесь с Privacy Policy");
        return;
    }
    userObject["agreeWithPrivacyPolicy"] = agreeWithPrivacyPolicy;
    
    var message = document.getElementById("message").value;
    if(!isInputValid("message", "Введите сообщение", message === ""))
        return;
    userObject["message"] = message;

    var outputDiv = document.getElementById("output");
    outputDiv.innerHTML = "";
    
    createParagraph("Имя: " + name, outputDiv);
    createParagraph("Email: " + email, outputDiv);
    createParagraph("Пароль: " + password, outputDiv);
    createParagraph("Возраст: " + age, outputDiv);
    createParagraph("Пол: " + gender, outputDiv);
    createParagraph("Согласны с Privacy Policy: " + agreeWithPrivacyPolicy, outputDiv);
    createParagraph("Сообщение: " + message, outputDiv);
    
    
    const userJson = JSON.stringify(userObject);
    console.log(userJson);

    // add json object to cookies and local storage
    // document.cookie = `userJson=${userJson};`;
    // localStorage.setItem("userJson", userJson);
  
    
    addToCookie("name", name);
    addToCookie("email", email);
    addToCookie("password", password);
    addToCookie("age", age);
    addToCookie("gender", gender);
    addToCookie("Agree with privacy policy", agreeWithPrivacyPolicy);
    addToCookie("Message", message);
    
    addToLocalStorage("name", name);
    addToLocalStorage("email", email)
    addToLocalStorage("password", password);
    addToLocalStorage("age", age);
    addToLocalStorage("gender", gender);
    addToLocalStorage("Agree with privacy policy", agreeWithPrivacyPolicy);
    addToLocalStorage("Message", message);
}

function addToCookie(elementName, elementValue){
    document.cookie = `${elementName}=${elementValue};`;
}

function addToLocalStorage(elementName, elementValue){
    localStorage.setItem(elementName, elementValue);
}

function isInputValid(id, errorMessage, condition) {
    const input = document.getElementById(id);
    if(condition) {
      alert(errorMessage);
      input.style.backgroundColor = "red";
      return false;
    }
    input.style.backgroundColor = "white";
    return true;
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