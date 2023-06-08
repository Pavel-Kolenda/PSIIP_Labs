function getCookiesInfo()
{
    setInterval(function(){
        alert(document.cookie);
    }, 222000);
}

document.addEventListener('DOMContentLoaded', () => {
    const element = document.querySelector('body p:first-child');
    element.addEventListener('click', () => {
      element.remove();
    });
});

getCookiesInfo();