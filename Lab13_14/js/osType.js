window.addEventListener('load', () => {
    const osInfo = `OS: ${navigator.platform}\n ${navigator.userAgent}`
    const newWindow = window.open('', '_blank', 'width=300,height=400');
    newWindow.document.write(`<p>${osInfo}</p>`);
    setTimeout(() => newWindow.close(), 3000);
  });