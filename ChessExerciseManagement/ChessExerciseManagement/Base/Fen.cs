using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ChessExerciseManagement.Base {
    public class Fen {
        public static bool CheckJonasFen(string fen) {
            if (fen == null || fen.Length == 0) {
                return false;
            }

            var lines = fen.Split(new[] { '\\', '_', '-', '/' });
            var positionCode = lines[0];

            var listOfLegalChars = new List<char>() {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'r', 'R', 'n', 'N', 'b', 'B', 'q', 'Q', 'k', 'K', 'p', 'P'
            };

            foreach (var character in positionCode) {
                if (!listOfLegalChars.Contains(character)) {
                    MessageBox.Show("The character " + character + " is not allowed in this notation.");
                    return false;
                }
            }

            var emptyFieldList = ExtractNumbersOfJonasFen(fen);
            var sum = emptyFieldList.Aggregate((a, b) => a + b);
            var letters = CountLetters(emptyFieldList);
            var emptyFields = sum - letters;

            var len = 0;
            len += lines[0].Length;

            if (len + emptyFields != 64) {
                return false;
            }

            return true;
        }

        private static List<int> ExtractNumbersOfJonasFen(string positionCode) {
            var listOfNumbers = new List<int>();
            if (positionCode == null || positionCode.Length == 0) {
                return listOfNumbers;
            }

            var sb = new StringBuilder();
            byte dummyVal;
            foreach (var character in positionCode) {
                var flag = byte.TryParse(character.ToString(), out dummyVal);
                if (flag) {
                    sb.Append(character);
                } else if (sb.Length != 0) {
                    var numStr = sb.ToString();
                    sb.Clear();
                    var num = int.Parse(numStr, System.Globalization.NumberStyles.Integer);
                    listOfNumbers.Add(num);
                }
            }

            if (sb.Length != 0) {
                var numStr = sb.ToString();
                sb.Clear();
                var num = int.Parse(numStr, System.Globalization.NumberStyles.Integer);
                listOfNumbers.Add(num);
            }

            return listOfNumbers;
        }

        private static int CountLetters(List<int> numbers) {
            if (numbers == null || numbers.Count == 0) {
                return 0;
            }

            var numberOfLetters = 0;

            foreach (var num in numbers) {
                var tmpNum = num;

                if (tmpNum < 0) {
                    tmpNum = -tmpNum;
                    numberOfLetters++;
                }

                do {
                    numberOfLetters++;
                    tmpNum = tmpNum / 10;
                } while (tmpNum > 0);
            }

            return numberOfLetters;
        }
    }
}
