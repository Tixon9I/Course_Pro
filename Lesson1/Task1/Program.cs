// Андрусенко

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

            var arrayOfLines = new string[0];
            StringBuilder sb = new StringBuilder();

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i].Equals(symbol))
                {
                    Array.Resize(ref arrayOfLines, arrayOfLines.Length + 1);
                    arrayOfLines[arrayOfLines.Length - 1] = sb.ToString().Trim();
                    sb.Clear();
                }
                else
                    sb.Append(line[i]);
            }

            Array.Resize(ref arrayOfLines, arrayOfLines.Length + 1);
            arrayOfLines[arrayOfLines.Length - 1] = sb.ToString().Trim();

            Console.Write("Result splitting of line: ");
            foreach(var lline in  arrayOfLines)
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
        }
    }
}
