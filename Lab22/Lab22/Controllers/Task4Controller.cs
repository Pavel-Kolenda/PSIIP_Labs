using Lab22.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab22.Controllers;
public class Task4Controller : Controller
{
    public IActionResult Index()
    {
        int[] array = new int[4];

        Random random = new Random();
        int sumOfNegative = 0;

        for(int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(-100, 100);
            if (array[i] < 0)
                sumOfNegative += array[i];
        }

        Task4Model task4Model = new Task4Model()
        {
            Array = array,
            SumOfNegativeNumbers = sumOfNegative,
        };

        return View(task4Model);
    }
}
