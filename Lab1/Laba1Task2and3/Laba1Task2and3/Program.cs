using System;
using System.Reflection;

namespace ReflectionConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom("Laba1Task2and3.dll");
                Type[] types = assembly.GetTypes();

                Console.WriteLine("=== Задание 2: Информация о классах и их элементах ===");
                foreach (Type type in types)
                {
                    if (type.IsEnum || type.Name == "Program") continue;

                    Console.WriteLine($"Class: {type.Name}");

                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        Console.WriteLine($"  Property: {property.Name} ({property.PropertyType.Name})");
                    }

                    FieldInfo[] fields = type.GetFields();
                    foreach (FieldInfo field in fields)
                    {
                        Console.WriteLine($"  Field: {field.Name} ({field.FieldType.Name})");
                    }

                    MethodInfo[] methods = type.GetMethods();
                    foreach (MethodInfo method in methods)
                    {
                        Console.WriteLine($"  Method: {method.Name} ({method.ReturnType.Name})");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("=== Задание 3: Создание объектов и вывод информации ===");
                foreach (Type type in types)
                {
                    if (type.IsEnum || type.Name == "Program") continue;

                    Console.WriteLine($"Working with class: {type.Name}");

                    MethodInfo createMethod = type.GetMethod("Create");
                    if (createMethod == null)
                    {
                        Console.WriteLine("  Method 'Create' not found.");
                        continue;
                    }

                    ParameterInfo[] parameters = createMethod.GetParameters();
                    object[] createParams = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)// не придумал чем их заполнять.
                    {
                        if (parameters[i].ParameterType == typeof(string))
                            createParams[i] = "String";
                        else if (parameters[i].ParameterType == typeof(int))
                            createParams[i] = 0;
                        else if (parameters[i].ParameterType.IsEnum)
                            createParams[i] = Enum.GetValues(parameters[i].ParameterType).GetValue(0);
                    }

                    object instance = createMethod.Invoke(null, createParams);
                    Console.WriteLine("  Instance created.");

                    MethodInfo printMethod = type.GetMethod("PrintObject");
                    if (printMethod == null)
                    {
                        Console.WriteLine("  Method 'PrintObject' not found.");
                        continue;
                    }

                    printMethod.Invoke(instance, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}