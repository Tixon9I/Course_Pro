using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Task2
{
    internal class Program
    {
        // Реверс строки/масиву. Без додаткового масиву. Складність О(n).

        static void ReverseString()
        {
            var inputString = "string. Empty".ToCharArray();

            for (int i = 0, j = inputString.Length - 1; i < j; i++, j--)
            {
                var temp = inputString[i];
                inputString[i] = inputString[j];
                inputString[j] = temp;
            }

            var outputString = new string(inputString);

            Console.WriteLine(outputString);
        }

        // Фільтрування неприпустимих слів у строці. Має бути саме слова, а не частини слів.

        static void Filtration()
        {
            var inputString = "This sentence contains the word and another Word.";

            var unacceptableWord = "word";

            var replacementWord = new string('*', unacceptableWord.Length);

            var outputString = Regex.Replace(inputString, unacceptableWord, replacementWord, RegexOptions.IgnoreCase);

            Console.WriteLine(inputString);
            Console.WriteLine(outputString);
        }

        // Генератор випадкових символів. На вхід кількість символів, на виході рядок з випадковими символами.

        static string CharacterGenerator(int length)
        {
            char[] generatedChars = new char[length];

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new Random();

            for (var i = 0; i < length; i++)
            {
                generatedChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(generatedChars);
        }

        // "Дірка" (пропущене число) у масиві.
        // Масив довжини N у випадковому порядку заповнений цілими числами з діапазону від 0 до N.
        // Кожне число зустрічається в масиві не більше одного разу. Знайти відсутнє число (дірку).
        // Складність алгоритму O(N).
        // Використання додаткової пам'яті, пропорційної довжині масиву не допускається.

         static void SearchForMissingNumber(int length)
        {
            var array = new int[length];

            Random random = new Random();
            
            for(var i = 0; i < array.Length; i++)
            {
                var randomNumber = 0;
                bool isUnique;

                do
                {
                    randomNumber = random.Next(0, length+1);
                    isUnique = true;

                    for(var j = 0; j < i; j++)
                    {
                        if (array[j].Equals(randomNumber))
                        {
                            isUnique = false; 
                            break;
                        }
                    }

                } while (!isUnique);

                array[i] = randomNumber;
            }
                

            foreach(var num in array)
                Console.Write($"{num} ");

            Console.WriteLine();

            var missingNumber = 0;

            for (var i = 0; i <= array.Length; i++)
                missingNumber ^= i;

            foreach (var num in array)
                missingNumber ^= num;

            Console.WriteLine(missingNumber);
            
        }

        // Найпростіше стиснення ланцюжка ДНК.
        // Ланцюг ДНК у вигляді строки на вхід (кожен нуклеотид представлений символом "A", "C", "G", "T").
        // Два методи, один для компресії, інший для декомпресії.

        static string CompressDNA(string input)
        {
            var nucleotides = new int[4];

            for (var i = 0; i < input.Length; i++)
            {
                switch(input[i])
                {
                    case 'A': nucleotides[0]++; break;
                    case 'C': nucleotides[1]++; break;
                    case 'G': nucleotides[2]++; break;
                    case 'T': nucleotides[3]++; break;
                }
            }

            return new string($"A{nucleotides[0]}B{nucleotides[1]}G{nucleotides[2]}T{nucleotides[3]}");
        }

        static string DecompressDNA(string input)
        {
            var nucleotides = new int[4];
            var indexArrayChars = 0;
            var index = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    nucleotides[index++] = input[i] - '0';
                    indexArrayChars += nucleotides[--index];
                    index++;
                }
            }

            char[] charsDNA = new char[indexArrayChars];
            var currentIndex = 0;
            index = 0;

            while (index < nucleotides.Length)
            {
                if (nucleotides[index] > 0)
                {
                    switch (index)
                    {
                        case 0: charsDNA[currentIndex++] = 'A'; nucleotides[index]--; break;
                        case 1: charsDNA[currentIndex++] = 'C'; nucleotides[index]--; break;
                        case 2: charsDNA[currentIndex++] = 'G'; nucleotides[index]--; break;
                        case 3: charsDNA[currentIndex++] = 'T'; nucleotides[index]--; break;
                    }
                }
                else
                    index++;
            }

            return new string(charsDNA);
        }

        // Симетричне шифрування.
        // Є строка на вхід, який має бути зашифрований.
        // Ключ можна задати в коді або згенерувати та зберегти.
        // Два методи, шифрування та дешифрування.

        static void Task6()
        {
            string plainText = "The quick brown fox jumps over the lazy dog.";

            using(Aes aes = Aes.Create())
            {
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                byte[] encrypted = EncryptStringToBytes_Aes(plainText, key, iv);
                string decrypted = DecryptStringFromBytes_Aes(encrypted, key, iv);

                // Вивід результатів
                Console.WriteLine($"Original: {plainText}");
                Console.WriteLine($"Encrypted (b64-encoded): {Convert.ToBase64String(encrypted)}");
                Console.WriteLine($"Decrypted: {decrypted}");
            }
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] IV)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));
            if (IV == null || IV.Length == 0)
                throw new ArgumentNullException(nameof(IV));

            byte[] encrypted;

            using(Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))

                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    encrypted = ms.ToArray();
                }
            }
            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText,  byte[] key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));
            if (IV == null || IV.Length == 0)
                throw new ArgumentNullException(nameof(IV));

            string plainText = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using(MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        plainText = sr.ReadToEnd();
                    }
                }
            }

            return plainText;
        }

        static void Main(string[] args)
        {
            // Task 1
            //ReverseString();

            // Task 2
            //Filtration();

            // Task 3
            //Console.WriteLine($"{CharacterGenerator(10)}");

            // Task 4
            //string compressedDNA = CompressDNA("AAAACCCCGGGGTTTT");

            //Console.WriteLine(compressedDNA);

            //Console.WriteLine(DecompressDNA(compressedDNA));

            // Task 5
            //SearchForMissingNumber(10);

            // Task 6
            //Task6();
        }
    }
}
