using System;
using System.Collections.Generic;

namespace QuadaxProgrammingExcercise
{
    class Program
    {
        static void Main(string[] args)
        {
            string answer = GenerateAnswer();
            Boolean guessedCorrectly = PlayGame(answer);
            Finish(guessedCorrectly);

            return;
        }

        static string GenerateAnswer()
        {
            int digitToAdd;
            string answer = "";
            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                digitToAdd = random.Next(1, 7);
                answer += digitToAdd.ToString();
            }

            return answer;
        }

        static Boolean PlayGame(string answer)
        {
            int numberOfTries = 0;
            string guess;
            string results;
            Boolean guessedCorrectly = false;

            while (!guessedCorrectly && numberOfTries <= 10)
            {
                results = "";
                numberOfTries++;

                guess = GetGuess();
                if (guess.Equals(answer))
                {
                    guessedCorrectly = true;
                }
                else
                {
                    results += GetPlusses(answer, guess);
                    results += GetMinuses(answer, guess, results);
                }

                Console.WriteLine(ResultsReversed(results));
            }

            return guessedCorrectly;
        }

        static string GetGuess()
        {
            Console.WriteLine("Enter your guess:");
            string guess = Console.ReadLine();

            return guess;
        }

        static string GetPlusses(string answer, string guess)
        {
            string plusses = "";

            for (int i = 0; i < 4; i++)
            {
                if (answer[i].Equals(guess[i]))
                {
                    plusses += '+';
                }
            }

            return plusses;
        }

        static string GetMinuses(string answer, string guess, string plusses)
        {
            int numberOfMinusesToAdd = 0;
            string minuses = "";
            List<char> distinctDigitsInGuess = new List<char>();
            distinctDigitsInGuess.Add(guess[0]);

            //use only the distinct digits in the guess to check with so that there are no repeated - for the same digit
            for (int i = 0; i < 4; i++)
            {
                for (int k = i; k < 4; k++)
                {
                    if (guess[i] != guess[k] && !distinctDigitsInGuess.Contains(guess[k]))
                    {
                        distinctDigitsInGuess.Add(guess[k]);
                    }
                }
            }

            for (int i = 0; i < distinctDigitsInGuess.Count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (answer[k].Equals(distinctDigitsInGuess[i]))
                    {
                        numberOfMinusesToAdd++;
                    }
                }
            }
            
            //remove instances of - being added when it should be a +
            if (!plusses.Equals(""))
            {
                numberOfMinusesToAdd -= plusses.Length;
            }

            for (int i = 1; i <= numberOfMinusesToAdd; i++)
            {
                minuses += '-';
            }

            return minuses;
        }

        //reverse the results so -/+ are in right order
        static string ResultsReversed(string results)
        {
            char[] arrayOfPlussesAndMinuses = results.ToCharArray();
            Array.Reverse(arrayOfPlussesAndMinuses);

            return new string(arrayOfPlussesAndMinuses);
        }

        static void Finish(Boolean guessedCorrectly)
        {
            if (guessedCorrectly)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lost!");
            }
        }
    }
}
