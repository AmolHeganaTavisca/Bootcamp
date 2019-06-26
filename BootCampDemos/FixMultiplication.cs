using System;

namespace BootCampDemos
{
    internal class FixMultiplication
    {
        public int FindDigit(string equation)
        {
            string[] tokens = equation.Split('=', '*');
            if (tokens.Length != 3)
                throw new ArgumentException("Invalid argument");

            ParseResult parseResult = ParseTokens(tokens);
            decimal actualResult = CalculateResult(parseResult.Numbers, parseResult.QuestionMarkContainingTokenIndex);
            int indexofQuestionMark = parseResult.QuestionMarkContainingToken.IndexOf("?");
            int missingDigit = CalculateMissingdigit(parseResult.QuestionMarkContainingToken, actualResult, indexofQuestionMark);

            return missingDigit;
        }        

        private ParseResult ParseTokens(string[] tokens)
        {
            ParseResult result = new ParseResult();
            decimal[] numbers = new decimal[3];
            for (int i = 0; i < 3; i++)
            {
                bool hasQuestionMark = tokens[i].Contains("?");
                if (hasQuestionMark)
                {
                    numbers[i] = -1;
                    result.QuestionMarkContainingToken = tokens[i];
                    result.QuestionMarkContainingTokenIndex = i;
                }
                else
                {
                    numbers[i] = decimal.Parse(tokens[i]);
                }
            }
            result.Numbers = numbers;
            
            return result;
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

    internal class ParseResult
    {
        public decimal[] Numbers { get; set; } = new decimal[3];

        public string QuestionMarkContainingToken { get; set; }

        public int QuestionMarkContainingTokenIndex { get; internal set; }
    }
}
