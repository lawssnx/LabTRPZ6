using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Program
{
    enum CollectionType
    {
        ArrayList = 1,
        SortedList,
        Stack,
        Dictionary
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Оберіть завдання для виконання:");
            Console.WriteLine("1. Заповнення двовимірного масиву.");
            Console.WriteLine("2. Робота з колекцією студентів.");
            Console.WriteLine("3. Робота з різними колекціями.");
            Console.WriteLine("4. Обробка речення.");
            Console.WriteLine("5. Вихід.");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Task3_2();
                    break;
                case "2":
                    Task3_3();
                    break;
                case "3":
                    Task3_4();
                    break;
                case "4":
                    Task3_5();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неправильний вибір. Спробуйте знову.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }

    // Task 3.2: 
    static void Task3_2()
    {
        try
        {
            Console.Write("Введіть розмірність матриці n: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Невірний ввід. Спробуйте ще раз.");
                return;
            }

            int[,] matrix = new int[n, n];

            Console.WriteLine("Оберіть метод заповнення:");
            Console.WriteLine("1. Випадкові значення");
            Console.WriteLine("2. Одиниці");
            Console.WriteLine("3. Ромб з нулів");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    FillRandom(matrix);
                    break;
                case "2":
                    FillOnes(matrix);
                    break;
                case "3":
                    FillDiamond(matrix);
                    break;
                default:
                    Console.WriteLine("Неправильний вибір.");
                    return;
            }

            PrintMatrix(matrix);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void FillRandom(int[,] matrix)
    {
        Random rand = new Random();
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                matrix[i, j] = rand.Next(1, 10);
    }

    static void FillOnes(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                matrix[i, j] = 1;
    }

    static void FillDiamond(int[,] matrix)
    {
        int center = matrix.GetLength(0) / 2;
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                if (Math.Abs(center - i) + Math.Abs(center - j) <= center)
                    matrix[i, j] = 0;
                else
                    matrix[i, j] = 1;
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                Console.Write(matrix[i, j] + " ");
            Console.WriteLine();
        }
    }

    // Task 3.3: 
    static void Task3_3()
    {
        List<Student> students = new List<Student>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Видалити студента");
            Console.WriteLine("3. Пошук студента");
            Console.WriteLine("4. Сортування");
            Console.WriteLine("5. Очистити колекцію");
            Console.WriteLine("6. Повернення до меню");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введіть ім'я студента: ");
                    students.Add(new Student { Name = Console.ReadLine() });
                    break;
                case "2":
                    Console.Write("Введіть ім'я студента для видалення: ");
                    string nameToRemove = Console.ReadLine();
                    students.RemoveAll(s => s.Name == nameToRemove);
                    break;
                case "3":
                    Console.Write("Введіть ім'я студента для пошуку: ");
                    string nameToSearch = Console.ReadLine();
                    var found = students.Where(s => s.Name == nameToSearch).ToList();
                    Console.WriteLine("Знайдені студенти:");
                    foreach (var student in found)
                        Console.WriteLine(student.Name);
                    break;
                case "4":
                    Console.WriteLine("1. За зростанням");
                    Console.WriteLine("2. За спаданням");
                    string sortChoice = Console.ReadLine();
                    students = sortChoice == "1" ? students.OrderBy(s => s.Name).ToList() : students.OrderByDescending(s => s.Name).ToList();
                    break;
                case "5":
                    students.Clear();
                    Console.WriteLine("Колекцію очищено.");
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Неправильний вибір.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }

    class Student
    {
        public string Name { get; set; }
    }

    // Task 3.4: 
    static void Task3_4()
    {
        ArrayList arrayList = new ArrayList();
        SortedList<int, string> sortedList = new SortedList<int, string>();
        Stack stack = new Stack();
        Dictionary<int, string> dictionary = new Dictionary<int, string>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Оберіть колекцію:");
            Console.WriteLine("1. ArrayList");
            Console.WriteLine("2. SortedList");
            Console.WriteLine("3. Stack");
            Console.WriteLine("4. Dictionary");
            Console.WriteLine("5. Повернутися до головного меню");
            string choice = Console.ReadLine();

            if (choice == "5") break;

            if (Enum.TryParse<CollectionType>(choice, out CollectionType collectionType))
            {
                HandleCollection(collectionType, arrayList, sortedList, stack, dictionary);
            }
            else
            {
                Console.WriteLine("Неправильний вибір. Спробуйте знову.");
            }
        }
    }

    static void HandleCollection(CollectionType collectionType, ArrayList arrayList, SortedList<int, string> sortedList, Stack stack, Dictionary<int, string> dictionary)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Вибрана колекція: {collectionType}");
            Console.WriteLine("Оберіть дію:");
            Console.WriteLine("1. Вивести елементи");
            Console.WriteLine("2. Додати елемент");
            Console.WriteLine("3. Повернутися до вибору колекції");

            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    DisplayCollection(collectionType, arrayList, sortedList, stack, dictionary);
                    break;
                case "2":
                    AddElementToCollection(collectionType, arrayList, sortedList, stack, dictionary);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неправильний вибір. Спробуйте знову.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }

    static void DisplayCollection(CollectionType collectionType, ArrayList arrayList, SortedList<int, string> sortedList, Stack stack, Dictionary<int, string> dictionary)
    {
        Console.WriteLine("Елементи колекції:");
        switch (collectionType)
        {
            case CollectionType.ArrayList:
                foreach (var item in arrayList)
                    Console.WriteLine(item);
                break;

            case CollectionType.SortedList:
                foreach (var kvp in sortedList)
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                break;

            case CollectionType.Stack:
                foreach (var item in stack)
                    Console.WriteLine(item);
                break;

            case CollectionType.Dictionary:
                foreach (var kvp in dictionary)
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                break;
        }
    }

    static void AddElementToCollection(CollectionType collectionType, ArrayList arrayList, SortedList<int, string> sortedList, Stack stack, Dictionary<int, string> dictionary)
    {
        Console.Write("Введіть елемент (ключ для Dictionary/SortedList також): ");
        string input = Console.ReadLine();

        switch (collectionType)
        {
            case CollectionType.ArrayList:
                arrayList.Add(input);
                break;

            case CollectionType.SortedList:
                if (int.TryParse(input, out int sortedKey))
                {
                    Console.Write("Введіть значення: ");
                    sortedList[sortedKey] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Невірний ключ.");
                }
                break;

            case CollectionType.Stack:
                stack.Push(input);
                break;

            case CollectionType.Dictionary:
                if (int.TryParse(input, out int dictKey))
                {
                    Console.Write("Введіть значення: ");
                    dictionary[dictKey] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Невірний ключ.");
                }
                break;
        }
    }

    // Task 3.5: 
    static void Task3_5()
    {
        Console.Write("Введіть речення: ");
        string sentence = Console.ReadLine();

        string[] words = sentence.Split(new[] { ' ', ',', '.', ';', ':', '-', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        int longestWordLength = words.Max(w => w.Length);

        Console.WriteLine($"Кількість слів у реченні: {words.Length}");
        Console.WriteLine($"Найдовше слово має {longestWordLength} символів.");
    }
}
