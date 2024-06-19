using System.IO;
using System.Security.Cryptography;
using System.Text;
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
            var inputString = @"This text includes several words that are considered unacceptable. 
For instance, using forbidden words like curse, slander, or insults can lead to negative consequences. 
Additionally, words that are offensive should be avoided to prevent hurting others. 
Therefore, it's important to refrain from using such words in any context.";

            var unacceptableWords = new string[] { "unacceptable", "forbidden", "curse", "slander", "insults" };

            Console.WriteLine(inputString);

            foreach (var word in unacceptableWords)
            {
                var replacementWord = new string('*', word.Length);
                var pattern = $@"\b{Regex.Escape(word)}\b";
                inputString = Regex.Replace(inputString, pattern, replacementWord, RegexOptions.IgnoreCase);
            }

            Console.WriteLine();
            Console.WriteLine(inputString);
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

        static void SearchForMissingNumber(int[] array)
        {
            var missingNumber = 0;

            for (var i = 0; i <= array.Length; i++)
                missingNumber ^= i;

            foreach (var num in array)
                missingNumber ^= num;

            Console.WriteLine(missingNumber);
            
        }

        static int[] GeneratorNumbers(int length)
        {
            var array = new int[length];

            Random random = new Random();

            for (var i = 0; i < array.Length; i++)
            {
                var randomNumber = 0;
                bool isUnique;

                do
                {
                    randomNumber = random.Next(0, length+1);
                    isUnique = true;

                    for (var j = 0; j < i; j++)
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

            return array;
        }

        // Найпростіше стиснення ланцюжка ДНК.
        // Ланцюг ДНК у вигляді строки на вхід (кожен нуклеотид представлений символом "A", "C", "G", "T").
        // Два методи, один для компресії, інший для декомпресії.

        static List<KeyValuePair<char, int>> CompressDNA(string input)
        {
            var compressed = new List<KeyValuePair<char, int>>();
            var currentChar = input[0];
            int count = 1;

            for (var i = 1; i < input.Length; i++)
            {
                if (input[i].Equals(currentChar))
                    count++;
                else
                {
                    compressed.Add(new KeyValuePair<char, int>(currentChar, count));
                    currentChar = input[i];
                    count = 1;
                }
            }

            compressed.Add((new KeyValuePair<char, int>(currentChar, count)));

            return compressed;
        }

        static string DecompressDNA(List<KeyValuePair<char, int>> input)
        {
            StringBuilder decompressed = new StringBuilder();

            foreach (var pair in input)
            {
                for(var i = 0; i < pair.Value; i++)
                    decompressed.Append(pair.Key);
            }

            return decompressed.ToString();
        }

        // Симетричне шифрування.
        // Є строка на вхід, який має бути зашифрований.
        // Ключ можна задати в коді або згенерувати та зберегти.
        // Два методи, шифрування та дешифрування.

        static void Task6()
        {
            string plainText = "The quick brown fox jumps over the lazy dog.";
            string key = "SecretKey";

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var encryptedBytes = XorEcrypt(plainTextBytes, keyBytes);
            var decryptedBytes = XorDecrypt(encryptedBytes, keyBytes);

            var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

            Console.WriteLine(plainText);
            Console.WriteLine($"Encrypted: {BitConverter.ToString(encryptedBytes)}");
            Console.WriteLine(decryptedString);
        }

        static byte[] XorEcrypt(byte[] plainText, byte[] key)
        {
            var encrypted = new byte[plainText.Length];

            for(var i = 0; i < encrypted.Length; i++)
            {
                encrypted[i] = (byte)(plainText[i] ^ key[i % key.Length]);
            }

            return encrypted;
        }

        static byte[] XorDecrypt(byte[] cipherText, byte[] key)
        {
            return XorEcrypt(cipherText, key);
        }

        //static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] IV)
        //{
        //    if (string.IsNullOrEmpty(plainText))
        //        throw new ArgumentNullException(nameof(plainText));
        //    if (key == null || key.Length == 0)
        //        throw new ArgumentNullException(nameof(key));
        //    if (IV == null || IV.Length == 0)
        //        throw new ArgumentNullException(nameof(IV));

        //    byte[] encrypted;

        //    using(Aes aes = Aes.Create())
        //    {
        //        aes.Key = key;
        //        aes.IV = IV;

        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))

        //            using (StreamWriter sw = new StreamWriter(cs))
        //            {
        //                sw.Write(plainText);
        //            }

        //            encrypted = ms.ToArray();
        //        }
        //    }
        //    return encrypted;
        //}

        //static string DecryptStringFromBytes_Aes(byte[] cipherText,  byte[] key, byte[] IV)
        //{
        //    if (cipherText == null || cipherText.Length == 0)
        //        throw new ArgumentNullException(nameof(cipherText));
        //    if (key == null || key.Length == 0)
        //        throw new ArgumentNullException(nameof(key));
        //    if (IV == null || IV.Length == 0)
        //        throw new ArgumentNullException(nameof(IV));

        //    string plainText = string.Empty;

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = key;
        //        aes.IV = IV;

        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        //        using(MemoryStream ms = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        //            using (StreamReader sr = new StreamReader(cs))
        //            {
        //                plainText = sr.ReadToEnd();
        //            }
        //        }
        //    }

        //    return plainText;
        //}


        static void Main(string[] args)
        {
            // Task 1
            //ReverseString();

            // Task 2
            //Filtration();

            // Task 3
            //Console.WriteLine($"{CharacterGenerator(10)}");

            // Task 5

            //var original = "CAGTGGATTCAA";

            //Console.WriteLine(original);

            //var compressedDNA = CompressDNA(original);

            //foreach (var dna in compressedDNA)
            //    Console.Write($"{dna.Key}{dna.Value}"); 
            //Console.WriteLine();

            //Console.WriteLine(DecompressDNA(compressedDNA));

            // Task 4
            //var array = GeneratorNumbers(10);

            //foreach (var num in array)
            //    Console.Write($"{num} ");

            //Console.WriteLine();

            //SearchForMissingNumber(array);

            // Task 6
            //Task6();
        }
    }
}
