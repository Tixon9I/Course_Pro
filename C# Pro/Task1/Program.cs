// Андрусенко

using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Task1
{
    internal class Program
    {
        // Знайти позицію літери в алфавіті та перевести її в інший регістр
        static void ChangeOfCase(char letter)
        {
            var positionLetter = 0;
            var changeOfCaseLetter = '\0';
            // Український алфавіт
            char[] ukrainianAlphabet = { 'А', 'Б', 'В', 'Г', 'Ґ', 'Д', 'Е', 'Є', 'Ж', 'З', 'И', 'І', 'Ї', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ю', 'Я' };

            var ukrainianLetter = Regex.IsMatch(letter.ToString(), "[а-яА-Я]");

            if (ukrainianLetter)
            {
                if(char.IsUpper(letter))
                {
                    positionLetter = Array.IndexOf(ukrainianAlphabet, letter) + 1;

                    changeOfCaseLetter = char.ToLower(letter);
                }
                else
                {
                    changeOfCaseLetter = char.ToUpper(letter);
                    positionLetter = Array.IndexOf(ukrainianAlphabet, changeOfCaseLetter) + 1;
                }
            }
            else
            {
                if (char.IsUpper(letter))
                {
                    positionLetter = letter - 'A' + 1;

                    changeOfCaseLetter = char.ToLower(letter);
                }
                else
                {
                    positionLetter = letter - 'a' + 1;
                    changeOfCaseLetter = char.ToUpper(letter);
                }
            }

            Console.WriteLine($"Changing the case: {changeOfCaseLetter}\nPosition of the letter in the alphabet: {positionLetter}");
        }

        /*
         Розділювач рядка. Дана строка та символ, потрібно розділити строку на кілька строк (масив строк) виходячи із заданого символу. 
         Наприклад: строка = "Лондон, Париж, Рим", а символ = ','. Результат = string[] { "Лондон", "Париж", "Рим" }.
        */

        static void SplittingLine()
        {
            var line = "Лондон, Рим, Париж";
            var symbol = ',';

            var length = 1;

            foreach (var sym in line)
                if (sym.Equals(symbol))
                    length++;

            var arrayOfLines = new string[length];

            var start = 0;
            var index = 0;

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i].Equals(symbol))
                {
                    arrayOfLines[index++] = line.Substring(start, i - start).Trim();

                    start = i + 1;
                }
            }

            arrayOfLines[index] = line.Substring(start).Trim();

            Console.Write("Result splitting of line: ");
            foreach (var lline in arrayOfLines)
            {
                Console.Write($"\n{lline}");
            }
        }

        // Пошук підстроки у строці
        // Алгоритм Кнута-Морріса-Пратта

        static int[] SearchSubstring()
        {
            var line = "abcabeabcabcabd";
            var substring = "abcabd";

            List<int> indexes = new List<int>();

            if (line.Length == 0 || substring.Length == 0 || line.Length < substring.Length)
            {
                return indexes.ToArray();
            }

            int i = 1;
            int j = 0;
            int[] lps = new int[substring.Length];
            Array.Fill(lps, 0);

            while (i < substring.Length)
            {
                if (substring[i] == substring[j])
                {
                    j += 1;
                    lps[i] = j;
                    i++;
                }
                else if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }

            i = 0;
            j = 0;

            while (line.Length - i >= substring.Length - j)
            {
                if (substring[j] == line[i])
                {
                    i++;
                    j++;
                }
                if (j == substring.Length)
                {
                    indexes.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < line.Length && substring[j] != line[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return indexes.ToArray();
        }

        // Написати програму, яка виводить число літерами. Приклад: 117 - сто сімнадцять (сподіваюсь, я правильно зрозуміла Вас, програма гручка до змін умови :))

        static void NumberToString (long number)
        {
            if (number == 0)
                Console.WriteLine("Нуль");

            var result = string.Empty;

            if (number < 0)
                result += "- ";

            if ((number / 1000000) > 0)
                result += SearchMillion(number);
            if ((number / 1000) > 0)
                result += SearchThousand(number);
            if ((number / 100) % 10 > 0)
                result += SearchHundred(number);
            if ((number % 100) > 9 && (number % 100) < 20)
            {
                result += SearchNumbersToTwelve(number);
                Console.WriteLine(result);
                return;
            }
            if ((number % 100) >= 20)
                result += SearchDozens(number);
            if ((number % 10) > 0)
                result += SearchDigit(number);

                Console.WriteLine(result);
        }

        static string SearchDigit(long value)
        {
            string[] digits = { "один", "два", "три", "чотири", "п'ять", "шiсть", "сiм", "вiсiм", "дев'ять" };

            var result = string.Empty;

            return result = digits[(value % 10) - 1];
        }

        static string SearchNumbersToTwelve(long value)
        {
            string[] numbersToTwelve = { "десять", "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "п'ятнадцять", "шiстнадцять", "сiмнадцять", "вiсiмнадцять", "дев'ятнадцять" };

            var result = string.Empty;

            return result = numbersToTwelve[(value % 100) - 10];
        }

        static string SearchDozens(long value)
        {
            string[] dozens = { "двадцять ", "тридцять ", "сорок ", "п'ятдесят ", "шiстдесят ", "сiмдесят ", "вiсiмдесят ", "дев'яносто " };

            var result = string.Empty;

            return result = dozens[(value % 100) / 10 - 2];
        }

        static string SearchHundred(long value)
        {
            string[] hundreds = { "сто ", "двiстi ", "триста ", "чотириста ", "п'ятсот ", "шiстсот ", "сiмсот ", "вiсiмсот ", "дев'ятсот " };

            var result = string.Empty;

            return result = hundreds[(value / 100) % 10 - 1];
        }

        static string SearchThousand(long value)
        {
            var result = string.Empty;

            if ((value / 1000) % 1000 >= 100)
                result += SearchHundred((value / 1000) % 1000);
            if (((value / 1000) % 1000) % 100 >= 20 && ((value / 1000) % 1000) % 100 < 100)
                result += SearchDozens((value / 1000) % 1000);
            if (((value / 1000) % 1000) % 100 > 9 && ((value / 1000) % 1000) % 100 < 20)
                result += SearchNumbersToTwelve((value / 1000) % 1000);
            else if (((value / 1000) % 1000) % 10 < 9 && ((value / 1000) % 1000) % 10 > 0)
                result += SearchDigit((value / 1000) % 1000);

            return $"{result} тисяч ";
        }

        static string SearchMillion(long value)
        {
            var result = string.Empty;

            if ((value / 1000000) >= 100)
                result += SearchHundred(value / 1000000);
            if ((value / 1000000) % 100 >= 20 && (value / 1000000)% 100 < 100)
                result += SearchDozens(value / 1000000);
            if ((value / 1000000) % 100 > 9 && (value / 1000000) % 100 < 20)
                result += SearchNumbersToTwelve(value / 1000000);
            else if ((value / 1000000) % 10 < 9 && (value / 1000000) % 10 > 0)
                result += SearchDigit(value / 1000000);

            return $"{result} мiльйон ";
        }


        // Поміняти місцями значення двох змінних (типу int) (без використання 3й)

        static void SwapValues()
        {
            var x = 10;
            var y = 12;

            Console.WriteLine($" x = {x} y = {y}");

            //x = x ^ y;
            //y = x ^ y;

            //x ^= y;

            x = x + y;
            y = x - y;

            x -= y;

            Console.WriteLine($" x = {x} y = {y}");
        }

        static void Main(string[] args)
        {
            // Task 1
            /*
            Console.Write("Enter letter (only ua or en): ");
            var inputLetter = char.TryParse(Console.ReadLine(), out var result);

            if (inputLetter)
                ChangeOfCase(result);
            else
                Console.WriteLine("Please input letter!");
            */

            // Task 2
            //SplittingLine();

            // Task 3
            /*
            int[] result = SearchSubstring();

            foreach (int i in result)
            {
                Console.WriteLine(i);
            }
            */

            // Task 4
            //NumberToString(17018019);

            // Task 5
            //SwapValues();
        }
    }
}
