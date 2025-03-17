using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("введите имя класса: ");
            string className = Console.ReadLine();
            Type type = Type.GetType($"TestClasses.{className}");
            if (type == null)
            {
                Console.WriteLine("ошибка: класс не найден.");
                return;
            }

            Console.Write("введите имя метода: ");
            string methodName = Console.ReadLine();
            MethodInfo method = type.GetMethod(methodName);
            if (method == null)
            {
                Console.WriteLine("ошибка: метод не найден.");
                return;
            }

            ParameterInfo[] parameters = method.GetParameters();
            object[] convertedArgs = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Console.Write($"введите аргумент {i + 1} ({parameters[i].ParameterType.Name}): ");
                string inputArg = Console.ReadLine();
                try
                {
                    convertedArgs[i] = Convert.ChangeType(inputArg, parameters[i].ParameterType);
                }
                catch
                {
                    Console.WriteLine($"ошибка: невозможно преобразовать аргумент {i + 1} к {parameters[i].ParameterType.Name}.");
                    return;
                }
            }

            object instance = Activator.CreateInstance(type);
            object result = method.Invoke(instance, convertedArgs);

            if (method.ReturnType != typeof(void))
            {
                Console.WriteLine($"результат: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ошибка: {ex.Message}");
        }
    }
}
