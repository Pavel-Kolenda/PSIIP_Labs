function showCpuType()
{
    setInterval(function() {
        alert(`Тип процессора: ${navigator.hardwareConcurrency} `);
    }, 4000)            
}
showCpuType();