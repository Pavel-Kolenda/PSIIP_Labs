var s1 = "Я люблю Беларусь"
var s2 = "Я учусь в политехническом колледже";
document.writeln(`<p>В строке "${s1}"  ${s2.length} символов<p>`);
document.writeln(`<p>Содержит ли строка "${s1}": ${s1.includes('Беларусь')}<p>`);
document.writeln(`<p>ASCII код 5ой буквы в строке "${s2}" = ${s2.charCodeAt(5)} <p>`)
