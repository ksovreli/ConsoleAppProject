namespace ConsoleAppProject
{
    internal class Hangman
    {
        private readonly List<string> Words = new List<string>
            {
                "apple",
                "banana",
                "cherry",
                "dragonfruit",
                "elderberry"
            };

        private readonly int Index;
        private readonly string RandomWord;
        private readonly char[] GuessedWord;
        private readonly List<char> TriedLetters;

        public uint Lives { get; private set; }

        public Hangman()
        {
            Random random = new Random();
            Index = random.Next(0, Words.Count);
            RandomWord = Words[Index];
            GuessedWord = new string('_', RandomWord.Length).ToCharArray();
            Lives = (uint)Math.Ceiling(RandomWord.Length / 2.0) + 3;
            TriedLetters = new List<char>();
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("Word: " + string.Join(" ", GuessedWord));
                Console.WriteLine($"Lives remaining: {Lives}");

                Console.Write("\nGuess a letter: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (!char.TryParse(input, out char userGuess))
                {
                    Console.WriteLine("You must enter exactly one letter!");
                    continue;
                }

                if (TriedLetters.Contains(userGuess))
                {
                    Console.WriteLine($"You have already tried '{userGuess}', try different one");
                    continue;
                }
                TriedLetters.Add(userGuess);

                bool correctGuess = false;

                if (RandomWord.Contains(userGuess, StringComparison.OrdinalIgnoreCase))
                {
                    for (int i = 0; i < RandomWord.Length; i++)
                    {
                        if (char.ToLower(userGuess) == char.ToLower(RandomWord[i]))
                        {
                            GuessedWord[i] = RandomWord[i];
                            correctGuess = true;
                        }
                    }
                }

                if (!correctGuess)
                {
                    Lives--;
                    Console.WriteLine($"Wrong guess! The letter '{userGuess}' is not in the word.");
                }

                if (!GuessedWord.Contains('_'))
                {
                    Console.Clear();
                    Console.WriteLine($"\nCongratulations! You guessed the word: {RandomWord}");
                    break;
                }

                if (Lives == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"\nYou lost! The word was: {RandomWord}");
                    break;
                }
            }
        }
    }
}
