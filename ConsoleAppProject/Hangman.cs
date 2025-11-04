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
            while (true)
            {
                Console.WriteLine("Word: " + string.Join(" ", _GuessedWord));
                Console.WriteLine($"Lives remaining: {Lives}");

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

                if (!_GuessedWord.Contains('_'))
                {
                    Console.Clear();
                    Console.WriteLine($"\nCongratulations! You guessed the word: {_RandomWord}");
                    break;
                }

                if (Lives == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"\nYou lost! The word was: {_RandomWord}");
                    break;
                }
            }
        }
    }
}
