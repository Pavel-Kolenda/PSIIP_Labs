document.addEventListener('DOMContentLoaded', () => {
    
    const btn = document.querySelector('#change-style-btn');
    const body = document.querySelector('body');
    const text = document.querySelectorAll('p');
    const links = document.querySelectorAll('a');
    
    btn.addEventListener('click', () => {
      body.style.backgroundImage = "url('img/img1.jpg')";
    
      text.forEach(te => {
        te.style.fontFamily = "Arial"
        te.style.fontSize = "11px";
        te.style.color = "red";
      });

      links.forEach(link => {
        link.style.color = "green";
        link.style.textDecoration = "none";
      });
    });
  });