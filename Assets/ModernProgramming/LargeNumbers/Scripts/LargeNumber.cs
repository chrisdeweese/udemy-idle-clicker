using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace ModernProgramming
{
    [Serializable]
    public class LargeNumber
    {
        #region ## VARIABLES ##

        [Range(0, 999)] public List<int> number;
        
        private const int MAX_SEGMENTS = 42;
        
        private int MultiplyMaxLength = 0;
        private int[] Top;
        private int[] Bottom;
        private int[] Answer;
        private int[] Carry;
        
        #endregion
        
        #region ## PUBLIC ENUMS ##

        /// <summary>
        /// Names for all large numbers.
        /// </summary>
        public enum Suffixes
        {
            None,                   // 10^0
            Thousand,               // 10^3
            Million,                // 10^6
            Billion,                // 10^9
            Trillion,               // 10^12
            Quadrillion,            // 10^15
            Qintillion,             // 10^18
            Sextillion,             // 10^21
            Septillion,             // 10^24
            Octillion,              // 10^27
            Nonillion,              // 10^30
            Decillion,              // 10^33
            Undecillion,            // 10^36
            Duodecillion,           // 10^39
            Tredecillion,           // 10^42
            Quattuordecillion,      // 10^45
            Quindecillion,          // 10^48
            Sexdecillion,           // 10^51
            Septendecillion,        // 10^54
            Octodecillion,          // 10^57
            Novemdecillion,         // 10^60
            Vigintillion,           // 10^63
            Unvigintillion,         // 10^66
            Duovigintillion,        // 10^69
            Trevigintillion,        // 10^72
            Quattuorvigintillion,   // 10^75
            Quinvigintillion,       // 10^78
            Sexvigintillion,        // 10^81
            Septenvigintillion,     // 10^84
            Octovigintillion,       // 10^87
            Novemvigintillion,      // 10^90
            Trigintillion,          // 10^93
            Untrigintillion,        // 10^96
            Duotrigintillion,       // 10^99
            Trestrigintillion,      // 10^102
            Quattuortrigintillion,  // 10^105
            Quinquatrigintillion,   // 10^108
            Sestrigintillion,       // 10^111
            Septentrigintillion,    // 10^114
            Octotrigintillion,      // 10^117
            Noventrigintillion,     // 10^120
            Quadragintillion        // 10^123
        };

        #endregion
        
        #region ## PUBLIC METHODS ##

        /// <summary>
        /// Creates a new large number with default value of zero.
        /// </summary>
        public LargeNumber()
        {
            number = new List<int> {000};
        }

        /// <summary>
        /// Creates a new large number clamped between 0 and 999.
        /// </summary>
        public LargeNumber(int newNumber)
        {
            number = new List<int> {Mathf.Clamp(newNumber, 0, 999)};
        }

        /// <summary>
        /// Creates a new large number with up to forty-two 3 digit segments (10^123)
        /// </summary>
        /// <param name="newNumber">First segment</param>
        /// <param name="newSegments">Number of following segments</param>
        public LargeNumber(int newNumber, int newSegments)
        {
            if (newSegments > MAX_SEGMENTS)
            {
                newSegments = MAX_SEGMENTS;
            }

            number = new List<int>();
            number.Add(Mathf.Clamp(newNumber, 0, 999));
        }

        /// <summary>
        /// Add one to the large number.
        /// </summary>
        public void AddOne()
        {
            number[0] += 1;
            if (number[0] > 999)
            {
                number[0] = 0;
                AddSegment(1);
            }
        }

        /// <summary>
        /// Subtract one from the large number.
        /// </summary>
        public void SubtractOne()
        {
            number[0] -= 1;
            if (number[0] < 0)
            {
                if (number.Count > 1)
                {
                    SubtractSegment(1);
                }
                else
                {
                    number[0] = 0;
                }
            }
            RemoveLeadingZeros();
        }
        
        /// <summary>
        /// Adds a large number to this large number.
        /// </summary>
        /// <param name="numberToAdd">Number to add.</param>
        public void AddLargeNumber(LargeNumber numberToAdd)
        {
            int difference = number.Count - numberToAdd.number.Count;

            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    numberToAdd.number.Add(000);
                }
            }
            else if (difference < 0)
            {
                for (int i = 0; i > difference; i--)
                {
                    number.Add(000);
                }
            }
            
            for (int i = 0; i < number.Count; i++)
            {
                difference = number.Count - numberToAdd.number.Count;
                if (difference > 0)
                {
                    for (int j = 0; j < difference; j++)
                    {
                        numberToAdd.number.Add(000);
                    }
                }
                else if (difference < 0)
                {
                    for (int j = 0; j > difference; j--)
                    {
                        number.Add(000);
                    }  
                }

                int carryAmount = 0;
                int totalAmount = number[i] + numberToAdd.number[i] + carryAmount;
                
                if (totalAmount > 999)
                {
                    //carry
                    carryAmount = totalAmount / 1000;
                    
                    //remainder
                    int remainder = totalAmount - (carryAmount * 1000);

                    number[i] = remainder;
                    if (i == number.Count - 1)
                    {
                        number.Add(carryAmount);
                        carryAmount = 0;
                    }
                    else
                    {
                        AddLargeNumber(i + 1, carryAmount);
                    }
                }
                else
                {
                    number[i] = totalAmount;
                }
            }
            
            RemoveLeadingZeros();
            numberToAdd.RemoveLeadingZeros();

            if (number.Count > MAX_SEGMENTS)
            {
                for (int i = 0; i < number.Count; i++)
                {
                    number[i] = 999;
                }
                number.RemoveRange(MAX_SEGMENTS, number.Count - MAX_SEGMENTS);
            }
        }

        /// <summary>
        /// Subtracts a large number from this large number.
        /// </summary>
        /// <param name="numberToSubtract"></param>
        public void SubtractLargeNumber(LargeNumber numberToSubtract)
        {
            LargeNumber temp = new LargeNumber();
            temp.Assign(numberToSubtract);
            
            if (IsEqual(temp) || IsLessThan(temp))
            {
                number.Clear();
                number.Add(000);
            }
            else
            {
                string amountString = temp.LargeNumberToString();
                
                int digitDifference = LargeNumberToString().Length - amountString.Length;
                
                for (int i = 0; i < digitDifference; i++)
                {
                    amountString = "0" + amountString;
                }
                
                string number9sCompliment = "";
                for (int i = 0; i < amountString.Length; i++)
                {
                    if (amountString[i].Equals('0'))
                    {
                        number9sCompliment += "9";
                    }
                    if (amountString[i].Equals('1'))
                    {
                        number9sCompliment += "8";
                    }
                    if (amountString[i].Equals('2'))
                    {
                        number9sCompliment += "7";
                    }
                    if (amountString[i].Equals('3'))
                    {
                        number9sCompliment += "6";
                    }
                    if (amountString[i].Equals('4'))
                    {
                        number9sCompliment += "5";
                    }
                    if (amountString[i].Equals('5'))
                    {
                        number9sCompliment += "4";
                    }
                    if (amountString[i].Equals('6'))
                    {
                        number9sCompliment += "3";
                    }
                    if (amountString[i].Equals('7'))
                    {
                        number9sCompliment += "2";
                    }
                    if (amountString[i].Equals('8'))
                    {
                        number9sCompliment += "1";
                    }
                    if (amountString[i].Equals('9'))
                    {
                        number9sCompliment += "0";
                    }
                }
                
                temp.Assign(temp.StringToLargeNumber(number9sCompliment));
                int digits = LargeNumberToString().Length;
                AddLargeNumber(temp);
                
                //check to see if there is a carry
                if (digits < LargeNumberToString().Length)
                {
                    int addCarry = number[number.Count - 1];
                    int subCarry = 1;

                    if (addCarry > 9)
                    {
                        subCarry = 10;
                        addCarry /= 10;
                    }
                    if (addCarry > 9)
                    {
                        subCarry = 100;
                        addCarry /= 10;
                    }
                    subCarry *= addCarry;

                    number[number.Count - 1] -= subCarry;
                    if (number[number.Count - 1] < 1)
                        number[number.Count - 1] = 0;
                    temp = new LargeNumber(addCarry);
                    AddLargeNumber(temp);
                }

                RemoveLeadingZeros();
            }
        }
        
        /// <summary>
        /// Multiplies two large numbers together.
        /// </summary>
        /// <param name="numberA">First large number.</param>
        /// <param name="numberB">Second large number.</param>
        /// <returns></returns>
        public LargeNumber MultiplyLargeNumber(LargeNumber numberA, LargeNumber numberB)
        {
            // Convert the parameters into strings.
            string top = numberA.LargeNumberToString();
            string bottom = numberB.LargeNumberToString();
            
            // Setup our int arrays.
            this.MultiplyMaxLength = top.Length + bottom.Length;
            this.Top = new int[top.Length];
            this.Bottom = new int[bottom.Length];
            this.Answer = new int[this.MultiplyMaxLength];
            this.Carry = new int[this.MultiplyMaxLength];
            
            // Initialize Answer array with zeroes.
            for (int i = 0; i < MultiplyMaxLength; i++)
            {
                this.Answer[i] = 0;
            }
            
            // Convert string into int, then store in top array.
            for (int i = 0; i < top.Length; i++)
            {
                this.Top[i] = int.Parse(top[i].ToString());
            }
            
            // Convert string into int, then store into bottom array.
            for (int i = 0; i < bottom.Length; i++)
            {
                this.Bottom[i] = int.Parse(bottom[i].ToString());
            }
            
            // Create a new large number and int array for our answer.
            LargeNumber result = new LargeNumber();
            int[] resultArray = Multiply();
            
            // Create a new StringBuilder and convert our int array into the string.
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < resultArray.Length; i++)
            {
                sb.Append(resultArray[i].ToString());
            }
            
            // Convert the built string into a large number.
            result = result.StringToLargeNumber(sb.ToString());
            
            // Return the large number.
            return result;
        }

        /// <summary>
        /// Checks if a large number is greater than this large number.
        /// </summary>
        public bool IsGreaterThan(LargeNumber numberToCompare)
        {
            if (number.Count == numberToCompare.number.Count)
            {
                for (int i = number.Count - 1; i >= 0; i--)
                {
                    if (number[i] != numberToCompare.number[i] && number[i] > numberToCompare.number[i])
                    {
                        return true; 
                    }
                }
            }
            else if (number.Count > numberToCompare.number.Count)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a large number is less than this large number.
        /// </summary>
        public bool IsLessThan(LargeNumber numberToCompare)
        {
            if (number.Count == numberToCompare.number.Count)
            {
                for (int i = number.Count - 1; i >= 0; i--)
                {
                    if (number[i] != numberToCompare.number[i] && number[i] > numberToCompare.number[i])
                    {
                        return false;   
                    }
                }
            }
            else if (number.Count > numberToCompare.number.Count)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if a large number is equal to this large number.
        /// </summary>
        public bool IsEqual(LargeNumber numberToCompare)
        {
            if (number.Count == numberToCompare.number.Count)
            {
                for (int i = number.Count - 1; i >= 0; i--)
                {
                    if (number[i] != numberToCompare.number[i] && number[i] == numberToCompare.number[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Assign a large number to this variable.
        /// </summary>
        public void Assign(LargeNumber numberToAssign)
        {
            number.Clear();
            foreach (int i in numberToAssign.number)
            {
                number.Add(i);
            }
        }
        
        /// <summary>
        /// Removes leading zeroes from this large number.
        /// </summary>
        public void RemoveLeadingZeros()
        {
            for (int i = number.Count - 1; i > 0; i--)
            {
                if (number[i] == 0)
                {
                    number.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
        }
        
        /// <summary>
        /// Converts this large number into a string with a suffix.
        /// </summary>
        public string LargeNumberToShortString()
        {
            if (number.Count == 0)
            {
                return "0";
            }
                
            string result = "" + number[number.Count - 1];
            
            if (number.Count > 1)
            {
                result += "." + string.Format("{0:000}", number[number.Count - 2]);
                result = result.Remove(result.Length - 2, 2);
                result += " " + (Suffixes)(number.Count - 1);
            }

            return result;
        }

        /// <summary>
        /// Converts this large number into a string.
        /// </summary>
        public string LargeNumberToString()
        {
            string result = "";
            
            for (int i = number.Count - 1; i >= 0; i--)
            {
                if (number[i] < 100)
                    result += "0";
                if (number[i] < 10)
                    result += "0";
                result += number[i];
            }

            result = result.TrimStart('0');

            if (result == "")
            {
                result = "0";
            }

            return result;
        }

        /// <summary>
        /// Converts this string into a large number.
        /// </summary>
        public LargeNumber StringToLargeNumber(string stringToConvert)
        {
            LargeNumber result = new LargeNumber();

            while (stringToConvert.Length % 3 != 0)
            {
                stringToConvert = "0" + stringToConvert;
            }

            //for substring second parameter is length
            for (int i = 0; i < stringToConvert.Length; i += 3)
            {
                result.number.Add(int.Parse(stringToConvert.Substring(i, 3)));
            }

            result.number.Reverse();

            for (int i = result.number.Count - 1; i > 0; i--)
            {
                if (result.number[i] == 0)
                {
                    result.number.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }

            return result;
        }
        
        /// <summary>
        /// Clamps the number list.
        /// </summary>
        public void ClampList()
        {
            if (number.Count > MAX_SEGMENTS)
            {
                number.RemoveRange(MAX_SEGMENTS, number.Count - MAX_SEGMENTS);
            }  
        }

        #endregion

        #region ## Private Methods ##

        private void AddSegment(int segmentCount)
        {
            if (number.Count == segmentCount)
            {
                if (number.Count < MAX_SEGMENTS)
                {
                    number.Add(1);
                }
            }
            else
            {
                number[segmentCount]++;
                if (number[segmentCount] > 999)
                {
                    number[segmentCount] = 0;
                    AddSegment(segmentCount + 1);
                }
            }
        }
        
        private void SubtractSegment(int segmentCount)
        {
            bool flag = true;
            for (int i = segmentCount; i < number.Count; i++)
            {
                if (number[i] != 0)
                {
                    number[i]--;
                    for (int o = i - 1; o >= 0; o--)
                    {
                        number[o] = 999;
                    }
                    flag = false;
                    break;
                }
                if (flag)
                {
                    number[i - 1] = 0;
                }
            }
        }
        
        private void AddLargeNumber(int count, int carry)
        {
            int total = number[count] + carry;
            if (total > 999)
            {
                //carry
                carry = (int)total / 1000;
                
                //remainder
                int remainder = total - (carry * 1000);

                number[count] = remainder;
                if (count == number.Count - 1)
                {
                    number.Add(carry);
                    carry = 0;
                }
                else
                {
                    AddLargeNumber(count + 1, carry);
                }
            }
            else
            {
                number[count] = total;
            }
        }
        
        private int[] Multiply()
        {
            int bottomIndex = this.Bottom.Length - 1;
            int placeOffset = 1;

            while (bottomIndex >= 0)
            {
                // Reset the carry to 0.
                for (int i = 0; i < this.MultiplyMaxLength; i++)
                {
                    this.Carry[i] = 0;
                }
                
                // Store our next digit index to calculate.
                int answerIndex = this.MultiplyMaxLength - placeOffset;

                // Iterate through each digit index.
                for (int topIndex = this.Top.Length - 1; topIndex >= 0; topIndex--)
                {
                    // Calculate the multiplied value for the current digit index and carry remainder.
                    int topNumber = this.Top[topIndex];
                    int bottomNumber = this.Bottom[bottomIndex];
                    int value = (topNumber * bottomNumber) + this.Carry[answerIndex];
                    int tens = (value / 10) % 10;
                    int ones = value - (tens * 10);
                    this.Carry[answerIndex - 1] += tens;

                    // We have an overflow so calculate the overflow value.
                    if ((this.Answer[answerIndex] + ones) > 9)
                    {
                        PerformOverflow(answerIndex, ones);
                    }
                    else
                    {
                        this.Answer[answerIndex] += ones;
                    }

                    // This is the final result of the current digit index.
                    if (topIndex == 0)
                    {
                        this.Answer[answerIndex - 1] += tens;
                    }

                    // Move down the index and repeat.
                    answerIndex--;
                }

                // Move down the index and increase the offset.
                bottomIndex--;
                placeOffset++;
            }

            // Return the final calculated value as an int array.
            return this.Answer;
        }
        
        private void PerformOverflow(int answerIndex, int ones)
        {
            // Adding the answer to it itself
            int overflow = this.Answer[answerIndex] + ones;
            int tensOver = (overflow / 10) % 10;
            int onesOver = (overflow - (tensOver * 10));

            this.Answer[answerIndex] = onesOver;
            this.Answer[answerIndex - 1] += tensOver;
        }

        #endregion
    }
}