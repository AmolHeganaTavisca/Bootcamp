using System;
using System.Collections.Generic;
using System.Text;

namespace BootCampDemos
{
    internal class FixMultiplication
    {
        public int FindDigit(string equation)
        {
            string[] tokens = equation.Split('=', '*');
            if (tokens.Length != 3)
                throw new ArgumentException("Invalid argument");

            decimal[] numbers = new decimal[3];
            string questionMarkContainingToken = string.Empty;
            int questionMarkContainingTokenIndex = -1;
            for (int i = 0; i < 3; i++)
            {
                bool hasQuestionMark = tokens[i].Contains("?");
                if (hasQuestionMark)
                {
                    numbers[i] = -1;
                    questionMarkContainingToken = tokens[i];
                    questionMarkContainingTokenIndex = i;
                }
                else
                {
                    numbers[i] = decimal.Parse(tokens[i]);
                }
            }
            string result = string.Empty;
            decimal actualResult = CalculateResult(numbers, questionMarkContainingTokenIndex);

            int indexofQuestionMark = questionMarkContainingToken.IndexOf("?");
            int myMissingDigit = CalculateMissingdigit(questionMarkContainingToken, actualResult, indexofQuestionMark);
            return myMissingDigit;
        }

        private int CalculateMissingdigit(string questionMarkContainingToken, decimal actualResult, int indexofQuestionMark)
        {
            int missingDigit = -1;
            char missingCharacter = actualResult.ToString()[indexofQuestionMark];
            questionMarkContainingToken = questionMarkContainingToken.Replace('?', missingCharacter);

            if (questionMarkContainingToken == actualResult.ToString())
            {
                missingDigit = int.Parse(missingCharacter.ToString());
            }

            return missingDigit;
        }

        private decimal CalculateResult(decimal[] numbers, int questionMarkContainingTokenIndex)
        {
            decimal actualResult = -1;
            if (questionMarkContainingTokenIndex == 0)
            {
                actualResult = numbers[2] / numbers[1];
            }
            else if (questionMarkContainingTokenIndex == 1)
            {
                actualResult = numbers[2] / numbers[0];
            }
            else if (questionMarkContainingTokenIndex == 2)
            {
                actualResult = numbers[0] * numbers[1];
            }

            return actualResult;
        }
    }
}
