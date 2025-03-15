using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введите имя класса:");
            string className = Console.ReadLine();

            Console.Write("Введите имя метода:");
            string methodName = Console.ReadLine();

            Console.Write("Введите аргументы через пробел:");
            string[] inputArgs = Console.ReadLine()?.Split(' ');

            Type type = Type.GetType($"TestClasses.{className}");
            if (type == null)
            {
                Console.WriteLine("Ошибка: Класс не найден.");
                return;
            }

            object instance = Activator.CreateInstance(type);

            MethodInfo method = type.GetMethod(methodName);
            if (method == null)
            {
                Console.WriteLine("Ошибка: Метод не найден.");
                return;
            }

            ParameterInfo[] parameters = method.GetParameters();
            object[] convertedArgs = parameters
                .Select((param, index) => Convert.ChangeType(inputArgs[index], param.ParameterType)).ToArray();

            object result = method.Invoke(instance, convertedArgs);
            Console.WriteLine($"Результат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
