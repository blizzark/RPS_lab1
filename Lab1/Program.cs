using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1

{
    class Lab1
    {
        public enum Menu : int
        {
            Start = 1,
            Exit,
        }
        public enum NoYes : int
        {
            Yes = 1,
            No,
        }
        public enum Input : int
        {
            Manual = 1,
            Random,
            File,
        }
        public enum SizeAndInputArray : int
        {
            MinInput = -99,
            MinShift = 1,
            MinSize = 2,
            MaxInput = 99,
            MaxSize = 100,
        }

        static void Main()
        {
            Info();
            MainMenu();
        }
        /// Вывод информации о программе
        public static void Info()
        {
            Console.WriteLine("Лабораторная работа №1");
            Console.WriteLine("Выполнил студент 475 группы: Овчинников Роман");
            Console.WriteLine("Написать программу для циклического сдвига массива вправо на N позиций.");
        }

        /// Стартовое меню
        public static void MainMenu()
        {
            Console.WriteLine("1. Начать программу");
            Console.WriteLine("2. Выход из программы");

            int caseSwitch = CheckInt((int)Menu.Start, (int)Menu.Exit);
            switch (caseSwitch)
            {
                case (int)Menu.Start:
                    SecondMenu();
                    break;
                case (int)Menu.Exit:
                    break;
            }
        }
        /// <summary>
        /// Получение массива
        /// <summary>
        public static void WorkingOnArray(int[] array)
        {
            Console.WriteLine("Введённый массив:");
            ShowArray(array);
            SaveArray(array);
            Console.WriteLine("Введите сдвиг:");
            int shift = CheckInt((int)SizeAndInputArray.MinShift, array.Length - 1);
            ShowArray(MainFunction(array, shift));
            SaveArray(MainFunction(array, shift), array, shift);
        }
        /// </summary>
        /// Выбор способа ввода
        /// </summary>
        public static void SecondMenu()
        {
            Console.WriteLine("Выбор способа ввода:");
            Console.WriteLine("1. Ручное заполнение");
            Console.WriteLine("2. Случайное заполнение");
            Console.WriteLine("3. Заполнение из файла");
            int caseSwitch = CheckInt((int)Input.Manual, (int)Input.File);
            switch (caseSwitch)
            {
                case (int)Input.Manual:
                    WorkingOnArray(ManualInput());
                    MainMenu();
                    break;
                case (int)Input.Random:
                    WorkingOnArray(RandomInput());
                    MainMenu();
                    break;
                case (int)Input.File:
                    FileInput();
                    MainMenu();
                    break;
            }
        }
        /// <summary>
        /// Вывод массива на экран
        /// <summary>
        public static void ShowArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine(" ");
        }
        /// <summary>
        // Ручной ввод
        /// <summary>
        public static int[] ManualInput()
        {

            Console.WriteLine("Введите рамер массива от 2 до 100:");
            int size = CheckInt((int)SizeAndInputArray.MinSize, (int)SizeAndInputArray.MaxSize);
            int[] array = new int[size];
            Console.WriteLine("Введите массив (значение от -99 до 99) через Enter:");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = CheckInt((int)SizeAndInputArray.MinInput, (int)SizeAndInputArray.MaxInput);
            }
            return array;
        }
        /// <summary>
        /// Случайное заполнение
        /// <summary>
        public static int[] RandomInput()
        {
            Console.WriteLine("Введите рамер массива от 2 до 100:");
            int size = CheckInt((int)SizeAndInputArray.MinSize, (int)SizeAndInputArray.MaxSize);
            int[] array = new int[size];
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(-100, 100);
            }
            return array;
        }
        /// <summary>
        // Файловый ввод
        /// <summary>
        public static void FileInput()
        {
            try
            {
                Console.WriteLine("Введите путь к файлу или имя файла:");
                string path = Console.ReadLine();
                string[] array = File.ReadAllLines(path);
                int[] arr = new int[array.Length];

                for (int i = 0; i < array.Length; i++)
                {
                    arr[i] = Convert.ToInt32(array[i]);
                }
                Console.WriteLine("Введённый массив:");
                ShowArray(arr);

                Console.WriteLine("Введите сдвиг:");
                int shift = CheckInt((int)SizeAndInputArray.MinShift, arr.Length);
                ShowArray(MainFunction(arr, shift));
                SaveArray(MainFunction(arr, shift), arr, shift);

            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка! " + e.Message);
            }

        }
        /// Функция циклического сдвига
        public static int[] MainFunction(int[] array, int shift)
        {
            while (shift != 0)
            {
                int tmp = array[0];
                for (int i = 0; i < array.Length; i++)
                {
                    array[0] = array[i];// сохраняем текущий элемент во временную ячейку 1
                    array[i] = tmp;   // вставляем в текущую ячейку предыдущий
                    tmp = array[0];
                }
                shift--;
            }
            return array;
        }
        /// <summary>
        /// Возврат целого числа 
        /// <summary>
        static int CheckInt(int start, int finish)
        {
            bool cycle = true;
            int num = 0;


            while (cycle)
            {
                string strPick = Console.ReadLine();
                if (int.TryParse(strPick, out num))
                {

                    if ((num >= start) && (num <= finish))
                    {
                        cycle = false;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Вводите числа от " + start + " до " + finish);
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод! Вводите цифры!");
                }
            }
            return num;
        }
        /// <summary>
        /// Меню выбора Да\Нет
        /// <summary>
        public static bool YesNo()
        {
            Console.WriteLine("1. Да");
            Console.WriteLine("2. Нет");
            Console.Write("Ваш выбор: ");

            int UserChoice = CheckInt((int)NoYes.Yes, (int)NoYes.No);
            if (UserChoice == (int)NoYes.Yes)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Сохранение в файл основого массива
        /// <summary>
        public static void SaveArray(int[] array)
        {
            Console.WriteLine("Хотите сохранить полученный массив в файл?");
            if (YesNo())
            {
                try
                {
                    Console.WriteLine("Введите путь к файлу или имя файла:");
                    string path = Console.ReadLine();
                    if (File.Exists(path))
                    {
                        Console.WriteLine("Данный файл уже существует. Пезераписать?");
                        if (YesNo())
                        {
                            Save(path, array);
                        }
                        else
                        {
                            SaveArray(array);
                        }
                    }
                    else
                    {
                        Save(path, array);
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка! " + e.Message);
                    Console.WriteLine("Попробовать ещё раз?");
                    SaveArray(array);
                }
            }
        }
        /// <summary>
        /// Сохрарение в файл переделанного массива
        /// <summary>
    
        public static void SaveArray(int[] arr, int[] array, int shift)
        {
            Console.WriteLine("Хотите сохранить полученный массив в файл?");
            if (YesNo())
            {
               
                    Console.WriteLine("Введите путь к файлу или имя файла:");
                    string path = Console.ReadLine();
                try
                {
                    if (File.Exists(path))
                    {
                        Console.WriteLine("Данный файл уже существует. Пезераписать?");
                        if (!YesNo())
                        {
                            Save(path, arr, array, shift);
                        }
                        else
                        {
                            SaveArray(arr, array, shift);
                        }
                    }
                    else
                    {
                        File.WriteAllText(path, "");
                        Save(path, arr, array, shift);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка! " + e.Message);
                    Console.WriteLine("Попробовать ещё раз?");
                    SaveArray(array);
                }
            }
        }

        public static void Save(string path, int[] arr, int[] array, int shift)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Сдвиг на " + shift);
                sw.WriteLine("Изначальный массив:");
                for (int i = 0; i < array.Length; i++)
                {
                    sw.Write(array[i] + " ");
                }
                sw.WriteLine("");
                sw.WriteLine("Переделанный массив:");
                for (int i = 0; i < array.Length; i++)
                {
                    sw.Write(arr[i] + " ");
                }
            }
            Console.WriteLine("Файл успешно записан!");
        }

        public static void Save(string path, int[] array)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {

                for (int i = 0; i < array.Length; i++)
                {
                    sw.WriteLine(array[i]);
                }
            }
            Console.WriteLine("Файл успешно записан!");
        }
    }
}


