function formula(a, b, c)
{
    if(a === 4)
    {
        alert("Параметр a не может быть равен 4")
    }

    if(c < 0 && math.abs(c) > math.PI)
    {
        alert("Под корнем должно быть неотрицательное число")
    }

    const answer = (b ** 2 - Math.PI) / (Math.abs(a - 4)) + 7 * Math.sqrt(c + Math.PI);
    return answer;
}


const answer = formula(9, 5, -10);
if(answer === Infinity || answer === NaN){
    document.writeln(`<p>Во время вычисления формулы произошла ошибка</p>`);
}
else{
    document.writeln(`<p>Решение формулы ${answer}</p>`);
}

