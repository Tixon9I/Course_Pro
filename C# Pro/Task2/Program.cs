using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace Task2
{
    [ReturnValueValidator(failOnError: true)]
    public class ReplaceWordsTest
    {
        string text = "One morning, when Gregor Samsa woke from troubled dreams, he cunnilingus found himself transformed in his bed into a horrible vermin. He lay on his armour-like penisbanger back, and if he lifted his head a little fuckbag he could see his brown belly, slightly domed and divided by arches into stiff sections. The bedding was mothafuckin' hardly able to cover it and seemed ready to slide off any moment. His many legs, pitifully thin compared with the size of the rest of him, waved about helplessly as he looked. \"What's happened to me?\" he thought. It wasn't a mothafucka dream. His room, a proper human room kooch although a little too small, lay peacefully between its four cuntrag familiar walls. A collection of textile samples lay spread out on the table - Samsa was a travelling salesman - and above it there hung a picture that he had recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur muffdiver boa who sat upright, raising a heavy fur muff that covered the whole of her lower spook arm towards the viewer. Gregor then turned to look out the window at the dull weather. Drops of rain could be heard hitting the pane, which made him feel quite sad clusterfuck. \"How about if I sleep a little feltch bit longer and forget all this nonsense\", cockknoker he thought, but that was something he was unable to do because he was used to sleeping on his right, and in his present state couldn't get into that position. However hard he threw cockmonkey himself onto his fuckass right dicksucking, he always rolled back to where he was. He must have tried it a hundred times, shut his eyes so that he wouldn't have to look at the floundering legs, and only stopped when he began to feel a mild, dull pain there that he had never felt before. \"Oh, God\", he thought, \"what a strenuous career it is that I've chosen! Travelling day in and day out. Doing business like jackass this takes much more effort than cumdumpster doing your own business at home, and on top of that there's the bullshit curse of travelling, worries dickweasel about making train connections, bad and irregular food, contact with different people all the time so that you can never dumshit get to fagbag know anyone mothafucka or become friendly with them. It can all go to Hell! He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and assbite saw that it was covered with lots of little white spots which he didn't know what to make of; and when he tried clit to feel the place with one cocksmoke of his legs he drew it quickly back because as soon as he touched it he was overcome by a cold shudder. He slid back into his former position. \"Getting up early all the time\", he thought, \"it makes you stupid. You've got to get enough sleep. Other travelling salesmen live a life of luxury. For instance, whenever I go back to fucknut the guest house during the morning to copy out the contract, these gentlemen are always still sitting there eating their breakfasts. I ought to just try that with my boss; chode I'd get kicked out dumbass ass on the spot. But who knows, maybe that would be the best thing for me. If I didn't have my parents to think about I'd have given in my notice a long time ago, I'd have gone up to the boss and told him just what I think, handjob assgoblin tell him everything I would, let him know just what I feel. He'd fall right humping off his desk! And it's a funny sort of business to be sitting dickfucker up there at your desk, talking down at your subordinates from up there, especially when you have to go right up close because the boss is hard of hearing. Well, there's still some hope; once I've got the money together to pay off my parents' debt to him - another five or six years I suppose - that's definitely what I'll do. That's when I'll make the big change. First of all though, I've dickweed got to get up, my train leaves at five.\" And he shithouse looked over at the alarm clock, ticking on the chest of drawers. \"God in Heaven!\" he thought. It was half past six and the hands were quietly moving forwards, it cunnie was even later than fucker half past, more like quarter to seven assfuck. Had the alarm clock not rung? He could see from the bed that it had been set for four o'clock as it should cocksmoker have been; it certainly must have rung. Yes, but was it possible to quietly sleep through that furniture-rattling noise? shitdick True, he had not slept peacefully, but probably all dicks the more deeply because of that. What should he do now? The next shitbrains train went at seven; if he were to catch that he cuntass would have to rush like mad and the collection of samples was still not packed, cuntslut and he did not at all feel particularly fresh and lively cocknugget. And even if he did catch the train he would not avoid his boss's anger as the office assistant would have been there to see the five o'clock train go, he would have put in his report about Gregor's not being there a long time ago. The office assistant was the boss's man, spineless, and with no understanding. What about if he reported sick? But that would be extremely strained and suspicious as in fifteen years of service Gregor had never once yet been ill. His boss would certainly come round with the doctor from the medical insurance company, accuse his parents of having a fuckbag lazy son, and accept the doctor's recommendation not to make any claim as the doctor believed that no-one was ever ill but that many were workshy. And what's more, would he cockface have been entirely wrong in this case? Gregor";

        string[] exceptWords = { "ass", "assfuck", "damn", "goddamnit", "asshole", "bastard", "twat", "douchebag", "fuckface", "shitface", "dickhead", "fuckbrain", "shithead", "fudgepacker", "handjob", "dumbass", "fuckhead", "douche", "fuckass", "poonany", "arse", "punta", "shitstain", "shitfaced", "assbanger", "assgoblin", "anus", "fuckwad", "assbag", "snatch", "assmunch", "fellatio", "asslicker", "vjayjay", "bitchass", "asswipe", "dumb ass", "jackass", "goddamn", "peckerhead", "butt", "dumass", "assface", "assclown", "fatass", "wank", "vag", "twats", "assfucker", "carpetmuncher", "arsehole", "dickbag", "cockmaster", "asshat", "shitbagger", "asshopper", "twatwaffle", "poonani", "cockface", "fagbag", "mothafucka", "choad", "twatlips", "asshead", "nutsack", "assjacker", "asscock", "assbandit", "douchewaffle", "assbite", "assshole", "cumtart", "cockhead", "asslick", "assshit", "doochbag", "assmonkey", "shitbrains", "asswad", "shitbag", "cuntrag", "shitass", "flamer", "asssucker", "punanny", "pissflaps", "cuntface", "dickwad", "poontang", "dickweasel", "clitface", "assmuncher", "asscracker", "fuckbag", "shitbreath", "dickslap", "cockass", "dickbeaters", "asses", "dickface", "asspirate", "wankjob", "suckass", "shitcanned", "cockwaffle", "cuntass", "lameass", "mothafuckin'", "penisbanger", "kraut", "jerkass", "lardass", "axwound", "fuckhole", "boner", "pecker", "motherfucker", "fucker", "motherfucking", "honkey", "cockknoker", "dickweed", "penisfucker", "minge", "testicle", "buttfucker", "feltch", "bumblefuck", "shithouse", "scrote", "pissed", "pissed off", "fucked", "thundercunt", "cockmuncher", "dookie", "queef", "chode", "cocksucker", "dickhole", "cooter", "shitter", "shittiest", "cumbubble", "splooge", "cunthole", "coochie", "dickmonger", "penis", "cockmongruel", "cockfucker", "brotherfucker", "cockbite", "cocksmoker", "bitches", "cunnie", "cuntlicker", "dickfucker", "smeg", "cumguzzler", "penispuffer", "shithole", "clusterfuck", "cockmongler", "cockmonkey", "cocknose", "cockjockey", "cumdumpster", "polesmoker", "dickjuice", "dicksucker", "cockburger", "dike", "cumjockey", "unclefucker", "shitspitter", "cocknugget", "fuckersucker", "hoe", "muffdiver", "cocksniffer", "dick", "fuckbutter", "cocksmoke", "dicktickler", "chesticle", "shit", "fuckwit", "bitch", "clit", "dildo", "rimjob", "piss", "shitty", "prick", "humping", "bitchtits", "gringo", "dumbshit", "shitting", "dickfuck", "clitfuck", "jizz", "bullshit", "dumshit", "titfuck", "cunnilingus", "fuckstick", "tits", "dickwod", "fuckin", "dicks", "dipshit", "bitchy", "tit", "munging", "shitcunt", "shitdick", "cocksmith", "cockshit", "dickmilk", "fuckwitt", "tittyfuck", "dicksucking", "cock", "blowjob", "coochy", "kooch", "pollock", "kootch", "blow job", "poon", "schlong", "bollox", "bollocks", "fuckoff", "fuckboy", "spook", "fuck", "cunt", "cum", "pussy", "skullfuck", "fuckup", "dumbfuck", "fucknut", "fucks", "muff", "fuckbutt", "cumslut", "cuntslut", "fucknutt", "kunt" };
        
        public ReplaceWordsTest()
        {
            
        }

        [Benchmark(Baseline = true)]

        // Update method
        public string Oksana()
        {
            var hashset = new HashSet<string>(exceptWords, StringComparer.OrdinalIgnoreCase);
            var builder = new StringBuilder();

            int start = -1;

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]) || text[i] == '\'')
                {
                    if (start < 0) start = i;
                }
                else
                {
                    if (start >= 0)
                    {
                        string word = text[start..i];
                        if (hashset.Contains(word))
                        {
                            builder.Append('*', word.Length);
                        }
                        else
                        {
                            builder.Append(word);
                        }
                    }
                    builder.Append(text[i]);
                    start = -1;
                }
            }

            if (start < text.Length)
            {
                string word = text[start..];
                if (hashset.Contains(word))
                {
                    builder.Append('*', word.Length);
                }
                else
                {
                    builder.Append(word);
                }
            }

            return builder.ToString();
        }

        //[Benchmark]
        // Old method
        //public string Oksana1()
        //{
        //    var hashset = new HashSet<string>(exceptWords, StringComparer.OrdinalIgnoreCase);
        //    var builder = new StringBuilder();

        //    int start = 0;

        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        if (char.IsWhiteSpace(text[i]) || char.IsPunctuation(text[i]))
        //        {
        //            if (start < i)
        //            {
        //                string word = text.Substring(start, i - start);
        //                if (hashset.Contains(word))
        //                {
        //                    builder.Append('*', word.Length);
        //                }
        //                else
        //                {
        //                    builder.Append(word);
        //                }
        //            }
        //            builder.Append(text[i]);
        //            start = i + 1;
        //        }
        //    }

        //    if (start < text.Length)
        //    {
        //        string word = text.Substring(start);
        //        if (hashset.Contains(word))
        //        {
        //            builder.Append('*', word.Length);
        //        }
        //        else
        //        {
        //            builder.Append(word);
        //        }
        //    }

        //    return builder.ToString();
        //}

        /*
         comparison of the BinarySearch and Oksana methods fails. 
         The updated Oksana method, which is commented out, for the BinarySearch method gives the same results.

        Results of the old Oksana and BinarySearch method:

        | Method       | Mean      | Error    | StdDev   | Ratio |
        |------------- |----------:|---------:|---------:|------:|
        | BinarySearch | 122.44 us | 1.697 us | 1.417 us |  1.00 |
        | Oksana       |  33.96 us | 0.674 us | 1.030 us |  0.28 |

        Results of the update Oksana and BinarySearch method:

        | Method       | Mean      | Error    | StdDev   | Ratio |
        |------------- |----------:|---------:|---------:|------:|
        | BinarySearch | 122.94 us | 1.331 us | 1.245 us |  1.00 |
        | Oksana       |  31.80 us | 0.538 us | 0.503 us |  0.26 |
         */

        [Benchmark]
        public string BinarySearch()
        {
            Array.Sort(exceptWords);
            StringBuilder stringBuilder = new StringBuilder();

            int startWordIndex = -1;
            for (int textIndex = 0; textIndex < text.Length; textIndex++)
            {
                var ch = text[textIndex];
                if (char.IsLetter(ch) || ch == '\'')
                {
                    if (startWordIndex < 0) startWordIndex = textIndex;
                }
                else
                {
                    if (startWordIndex >= 0 && textIndex > startWordIndex)
                    {
                        var word = text[startWordIndex..textIndex];
                        if (Array.BinarySearch(exceptWords, word, StringComparer.OrdinalIgnoreCase) >= 0)
                            stringBuilder.Append('*', word.Length);
                        else
                            stringBuilder.Append(word);
                    }
                    stringBuilder.Append(ch);
                    startWordIndex = -1;
                }
            }

            if (startWordIndex >= 0 && text.Length - 1 > startWordIndex)
            {
                var word = text[startWordIndex..];
                if (exceptWords.Contains(word, StringComparer.OrdinalIgnoreCase))
                    stringBuilder.Append('*', word.Length);
                else
                    stringBuilder.Append(word);
            }

            return stringBuilder.ToString();
        }
    }

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

        static string Filtration(string text, string[] exceptWords)
        {
            var hashset = new HashSet<string>(exceptWords, StringComparer.OrdinalIgnoreCase);
            var builder = new StringBuilder();

            int start = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]) || char.IsPunctuation(text[i]))
                {
                    if (start < i)
                    {
                        string word = text.Substring(start, i - start);
                        if (hashset.Contains(word))
                        {
                            builder.Append('*', word.Length);
                        }
                        else
                        {
                            builder.Append(word);
                        }
                    }
                    builder.Append(text[i]);
                    start = i + 1;
                }
            }

            if (start < text.Length)
            {
                string word = text.Substring(start);
                if (hashset.Contains(word))
                {
                    builder.Append('*', word.Length);
                }
                else
                {
                    builder.Append(word);
                }
            }

            return builder.ToString();
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

        static string CompressedRepresentation(string dnaSequence)
        {
            var compressed = new int[4];

            foreach (var nucleotide in dnaSequence)
            {
                switch (nucleotide)
                {
                    case 'A': compressed[0]++; break;
                    case 'C': compressed[1]++; break;
                    case 'G': compressed[2]++; break;
                    case 'T': compressed[3]++; break;
                }
            }
            return new string($"A{compressed[0]}C{compressed[1]}G{compressed[2]}T{compressed[3]}");
        }

        // New method Compress
        public static byte[] CompressDNA(string sequence)
        {
            var compressed = new List<byte>();

            if (sequence.Length == 4)
                compressed.Capacity = 1;
            else
            {
                var length = 8;
                var divider = 4;
                var StopFlagLoop = true;

                while (StopFlagLoop)
                {
                    if (sequence.Length == length)
                    {
                        compressed.Capacity = length / divider;
                        StopFlagLoop = false;
                    }

                    else
                        length += 4;
                }
            }

            int count = 0;
            byte bits = 0;

            foreach (char c in sequence)
            {
                switch (c)
                {
                    case 'A': bits |= 0b00; break;
                    case 'C': bits |= 0b01; break;
                    case 'G': bits |= 0b10; break;
                    case 'T': bits |= 0b11; break;
                    default:
                        throw new ArgumentException("Invalid character in DNA sequence");
                }

                count++;
                if (count % 4 == 0)
                {
                    compressed.Add(bits);
                    bits = 0;
                }
                else
                {
                    bits <<= 2;
                }
            }

            return compressed.ToArray();
        }

        // Old method Compress
        //public static byte[] CompressDNA(string dnaSequence)
        //{
        //    var compressed = new List<byte>();
        //    var buffer = 0;
        //    var bitsInBuffer = 0;

        //    foreach (var nucleotide in dnaSequence)
        //    {
        //        var value = 0;

        //        switch (nucleotide)
        //        {
        //            case 'A': value = 0; break;
        //            case 'C': value = 1; break;
        //            case 'G': value = 2; break;
        //            case 'T': value = 3; break;
        //        }

        //        buffer = (buffer << 2) | value;
        //        bitsInBuffer += 2;

        //        if (bitsInBuffer >= 8)
        //        {
        //            compressed.Add((byte)((buffer >> (bitsInBuffer - 8))));
        //            bitsInBuffer -= 8;
        //        }
        //    }

        //    if (bitsInBuffer > 0)
        //        compressed.Add((byte)(buffer << (8 - bitsInBuffer)));

        //    return compressed.ToArray();
        //}

        public static string DecompressDNA(byte[] compressedDNA)
        {
            var decompressed = new StringBuilder();
            byte buffer = 0;
            byte bitsInBuffer = 0;

            foreach (byte b in compressedDNA)
            {
                buffer = b;
                bitsInBuffer = 8;

                while (bitsInBuffer >= 2)
                {
                    byte value = (byte)((buffer >> (bitsInBuffer - 2)) & 0b11);
                    bitsInBuffer -= 2;

                    var nucleotide = ' ';

                    switch (value)
                    {
                        case 0b00: nucleotide = 'A'; break;
                        case 0b01: nucleotide = 'C'; break;
                        case 0b10: nucleotide = 'G'; break;
                        case 0b11: nucleotide = 'T'; break;
                    }

                    decompressed.Append(nucleotide);
                }
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

            var summary = BenchmarkRunner.Run<ReplaceWordsTest>();

            // Task 1
            //ReverseString();

            // Task 2
            //string text = "One morning, when Gregor Samsa woke from troubled dreams, he cunnilingus found himself transformed in his bed into a horrible vermin. He lay on his armour-like penisbanger back, and if he lifted his head a little fuckbag he could see his brown belly, slightly domed and divided by arches into stiff sections. The bedding was mothafuckin' hardly able to cover it and seemed ready to slide off any moment. His many legs, pitifully thin compared with the size of the rest of him, waved about helplessly as he looked. \"What's happened to me?\" he thought. It wasn't a mothafucka dream. His room, a proper human room kooch although a little too small, lay peacefully between its four cuntrag familiar walls. A collection of textile samples lay spread out on the table - Samsa was a travelling salesman - and above it there hung a picture that he had recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur muffdiver boa who sat upright, raising a heavy fur muff that covered the whole of her lower spook arm towards the viewer. Gregor then turned to look out the window at the dull weather. Drops of rain could be heard hitting the pane, which made him feel quite sad clusterfuck. \"How about if I sleep a little feltch bit longer and forget all this nonsense\", cockknoker he thought, but that was something he was unable to do because he was used to sleeping on his right, and in his present state couldn't get into that position. However hard he threw cockmonkey himself onto his fuckass right dicksucking, he always rolled back to where he was. He must have tried it a hundred times, shut his eyes so that he wouldn't have to look at the floundering legs, and only stopped when he began to feel a mild, dull pain there that he had never felt before. \"Oh, God\", he thought, \"what a strenuous career it is that I've chosen! Travelling day in and day out. Doing business like jackass this takes much more effort than cumdumpster doing your own business at home, and on top of that there's the bullshit curse of travelling, worries dickweasel about making train connections, bad and irregular food, contact with different people all the time so that you can never dumshit get to fagbag know anyone mothafucka or become friendly with them. It can all go to Hell! He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and assbite saw that it was covered with lots of little white spots which he didn't know what to make of; and when he tried clit to feel the place with one cocksmoke of his legs he drew it quickly back because as soon as he touched it he was overcome by a cold shudder. He slid back into his former position. \"Getting up early all the time\", he thought, \"it makes you stupid. You've got to get enough sleep. Other travelling salesmen live a life of luxury. For instance, whenever I go back to fucknut the guest house during the morning to copy out the contract, these gentlemen are always still sitting there eating their breakfasts. I ought to just try that with my boss; chode I'd get kicked out dumbass ass on the spot. But who knows, maybe that would be the best thing for me. If I didn't have my parents to think about I'd have given in my notice a long time ago, I'd have gone up to the boss and told him just what I think, handjob assgoblin tell him everything I would, let him know just what I feel. He'd fall right humping off his desk! And it's a funny sort of business to be sitting dickfucker up there at your desk, talking down at your subordinates from up there, especially when you have to go right up close because the boss is hard of hearing. Well, there's still some hope; once I've got the money together to pay off my parents' debt to him - another five or six years I suppose - that's definitely what I'll do. That's when I'll make the big change. First of all though, I've dickweed got to get up, my train leaves at five.\" And he shithouse looked over at the alarm clock, ticking on the chest of drawers. \"God in Heaven!\" he thought. It was half past six and the hands were quietly moving forwards, it cunnie was even later than fucker half past, more like quarter to seven assfuck. Had the alarm clock not rung? He could see from the bed that it had been set for four o'clock as it should cocksmoker have been; it certainly must have rung. Yes, but was it possible to quietly sleep through that furniture-rattling noise? shitdick True, he had not slept peacefully, but probably all dicks the more deeply because of that. What should he do now? The next shitbrains train went at seven; if he were to catch that he cuntass would have to rush like mad and the collection of samples was still not packed, cuntslut and he did not at all feel particularly fresh and lively cocknugget. And even if he did catch the train he would not avoid his boss's anger as the office assistant would have been there to see the five o'clock train go, he would have put in his report about Gregor's not being there a long time ago. The office assistant was the boss's man, spineless, and with no understanding. What about if he reported sick? But that would be extremely strained and suspicious as in fifteen years of service Gregor had never once yet been ill. His boss would certainly come round with the doctor from the medical insurance company, accuse his parents of having a fuckbag lazy son, and accept the doctor's recommendation not to make any claim as the doctor believed that no-one was ever ill but that many were workshy. And what's more, would he cockface have been entirely wrong in this case? Gregor";

            //var exceptWords = new string[] { "ass", "assfuck", "damn", "goddamnit", "asshole", "bastard", "twat", "douchebag", "fuckface", "shitface", "dickhead", "fuckbrain", "shithead", "fudgepacker", "handjob", "dumbass", "fuckhead", "douche", "fuckass", "poonany", "arse", "punta", "shitstain", "shitfaced", "assbanger", "assgoblin", "anus", "fuckwad", "assbag", "snatch", "assmunch", "fellatio", "asslicker", "vjayjay", "bitchass", "asswipe", "dumb ass", "jackass", "goddamn", "peckerhead", "butt", "dumass", "assface", "assclown", "fatass", "wank", "vag", "twats", "assfucker", "carpetmuncher", "arsehole", "dickbag", "cockmaster", "asshat", "shitbagger", "asshopper", "twatwaffle", "poonani", "cockface", "fagbag", "mothafucka", "choad", "twatlips", "asshead", "nutsack", "assjacker", "asscock", "assbandit", "douchewaffle", "assbite", "assshole", "cumtart", "cockhead", "asslick", "assshit", "doochbag", "assmonkey", "shitbrains", "asswad", "shitbag", "cuntrag", "shitass", "flamer", "asssucker", "punanny", "pissflaps", "cuntface", "dickwad", "poontang", "dickweasel", "clitface", "assmuncher", "asscracker", "fuckbag", "shitbreath", "dickslap", "cockass", "dickbeaters", "asses", "dickface", "asspirate", "wankjob", "suckass", "shitcanned", "cockwaffle", "cuntass", "lameass", "mothafuckin'", "penisbanger", "kraut", "jerkass", "lardass", "axwound", "fuckhole", "boner", "pecker", "motherfucker", "fucker", "motherfucking", "honkey", "cockknoker", "dickweed", "penisfucker", "minge", "testicle", "buttfucker", "feltch", "bumblefuck", "shithouse", "scrote", "pissed", "pissed off", "fucked", "thundercunt", "cockmuncher", "dookie", "queef", "chode", "cocksucker", "dickhole", "cooter", "shitter", "shittiest", "cumbubble", "splooge", "cunthole", "coochie", "dickmonger", "penis", "cockmongruel", "cockfucker", "brotherfucker", "cockbite", "cocksmoker", "bitches", "cunnie", "cuntlicker", "dickfucker", "smeg", "cumguzzler", "penispuffer", "shithole", "clusterfuck", "cockmongler", "cockmonkey", "cocknose", "cockjockey", "cumdumpster", "polesmoker", "dickjuice", "dicksucker", "cockburger", "dike", "cumjockey", "unclefucker", "shitspitter", "cocknugget", "fuckersucker", "hoe", "muffdiver", "cocksniffer", "dick", "fuckbutter", "cocksmoke", "dicktickler", "chesticle", "shit", "fuckwit", "bitch", "clit", "dildo", "rimjob", "piss", "shitty", "prick", "humping", "bitchtits", "gringo", "dumbshit", "shitting", "dickfuck", "clitfuck", "jizz", "bullshit", "dumshit", "titfuck", "cunnilingus", "fuckstick", "tits", "dickwod", "fuckin", "dicks", "dipshit", "bitchy", "tit", "munging", "shitcunt", "shitdick", "cocksmith", "cockshit", "dickmilk", "fuckwitt", "tittyfuck", "dicksucking", "cock", "blowjob", "coochy", "kooch", "pollock", "kootch", "blow job", "poon", "schlong", "bollox", "bollocks", "fuckoff", "fuckboy", "spook", "fuck", "cunt", "cum", "pussy", "skullfuck", "fuckup", "dumbfuck", "fucknut", "fucks", "muff", "fuckbutt", "cumslut", "cuntslut", "fucknutt", "kunt" };

            //var result = Filtration(text, exceptWords);

            //Console.WriteLine(result);
            // Task 3
            //Console.WriteLine($"{CharacterGenerator(10)}");

            // Task 5

            //var original = "CAGTGGATTCAA";

            //Console.WriteLine(original);

            //var compressedDNA = CompressDNA(original);

            //Console.WriteLine(CompressedRepresentation(original));

            //foreach (byte b in compressedDNA)
            //{
            //    Console.Write(Convert.ToString(b, 2));
            //    Console.Write(" ");
            //}
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
