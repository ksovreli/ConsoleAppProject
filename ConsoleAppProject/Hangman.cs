namespace ConsoleAppProject
{
    internal class Hangman
    {
        private readonly List<string> _Words = new List<string>
            {
                "apple",
                "banana",
                "cherry",
                "dragonfruit",
                "elderberry"
            };
        private readonly Dictionary<string, string> _Hints = new Dictionary<string, string>
        {
            { "apple", "A common fruit, often red or green." },
            { "banana", "A long yellow fruit." },
            { "cherry", "Small red fruit, often on desserts." },
            { "dragonfruit", "Exotic fruit with pink skin."},
            { "elderberry", "Small dark berry." }
        };

        private readonly int _Index;
        private readonly string _RandomWord;
        private readonly char[] _GuessedWord;
        private readonly List<char> _TriedLetters;

        public uint Lives { get; private set; }

        public Hangman()
        {
            Random random = new Random();
            _Index = random.Next(0, _Words.Count);
            _RandomWord = _Words[_Index];
            _GuessedWord = new string('_', _RandomWord.Length).ToCharArray();
            Lives = (uint)Math.Ceiling(_RandomWord.Length / 2.0) + 3;
            _TriedLetters = new List<char>();
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("\n=================================\n");
            Console.WriteLine("       Welcome to Hangman        ");
            Console.WriteLine("\n=================================\n");

            while (true)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Word: " + string.Join(" ", _GuessedWord));
                Console.WriteLine("Hint: " + _Hints[_RandomWord]);
                Console.WriteLine($"Lives remaining: {Lives}");
                Console.WriteLine("Tried letters: " + string.Join(", ", _TriedLetters));
                Console.WriteLine("---------------------------------");

                Console.Write("\nGuess a letter: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (!char.TryParse(input, out char userGuess))
                {
                    Console.WriteLine("You must enter exactly one letter!");
                    continue;
                }

                if (_TriedLetters.Contains(userGuess))
                {
                    Console.WriteLine($"You have already tried '{userGuess}', try different one");
                    continue;
                }
                _TriedLetters.Add(userGuess);

                bool correctGuess = false;

                if (_RandomWord.Contains(userGuess, StringComparison.OrdinalIgnoreCase))
                {
                    for (int i = 0; i < _RandomWord.Length; i++)
                    {
                        if (char.ToLower(userGuess) == char.ToLower(_RandomWord[i]))
                        {
                            _GuessedWord[i] = _RandomWord[i];
                            correctGuess = true;
                        }
                    }
                }

                if (!correctGuess)
                {
                    Lives--;
                    Console.WriteLine($"Wrong guess! The letter '{userGuess}' is not in the word.");
                }

                else
                {
                    Console.WriteLine($"Good job! '{userGuess}' is in the word!\n");
                }

                if (!_GuessedWord.Contains('_'))
                {
                    Console.Clear();
                    Console.WriteLine("\n=================================\n");
                    Console.WriteLine("       Congratulations!          ");
                    Console.WriteLine("\n=================================\n");
                    Console.WriteLine($"\nYou guessed the word: {_RandomWord}\n");
                    break;
                }

                if (Lives == 0)
                {
                    Console.Clear();
                    Console.WriteLine("\n=================================\n");
                    Console.WriteLine("             Game Over           ");
                    Console.WriteLine("\n=================================\n");
                    Console.WriteLine($"\nThe word was: {_RandomWord}\n");
                    break;
                }
            }
        }
    }
}
