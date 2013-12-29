/*
 * Version      1.0.0
 * Package      IO Exchanger
 * Subpackage   Helper class
 * Copyright    (c) 2013 Mathias Gebhardt
 * License      GNU General Public License version 3 or later 
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>
 *
 */

/*============================================================================*/

using System;

/*============================================================================*/

namespace IOX
{

    /*========================================================================*/

    /// <summary>
    /// Porvides conversion methodes for other classes
    /// </summary>
    public class Helper
    {

        /*====================================================================*/
        
        /// <summary>
        /// Converts binary digit count to hex digit count
        /// </summary>
        /// <param name="binDigit">The binary digit count</param>
        /// <returns>The hex digit count</returns>
        public static int binDigit2HexDigit
            (int binDigit)
        {
            if (binDigit < 0)
            {
                throw new ArgumentOutOfRangeException("binValue", binDigit,
                    "Max value must be positiv.");
            }

            return ((binDigit - 1) / 8 + 1) * 2;
        }

        /*====================================================================*/

        /// <summary>
        /// Generates hex string format with digit count need to display binary
        /// digits
        /// </summary>
        /// <param name="binDigit">The binary digit count</param>
        /// <returns>The hex string formater</returns>
        public static string binDigit2HexStrFormat
            (int binDigit)
        {
            if (binDigit < 0)
            {
                throw new ArgumentOutOfRangeException("binValue", binDigit,
                    "Max value must be positiv.");
            }
            return "0x{0:X" + binDigit2HexDigit(binDigit).ToString() + "}";
        }

        /*====================================================================*/

        /// <summary>
        /// Calculates hex digit count to display max value
        /// </summary>
        /// <param name="maxVal">The max value</param>
        /// <returns>The hex digit count</returns>
        public static int maxVal2HexDigit
            (int maxVal)
        {
            if (maxVal <= 0)
            {
                throw new ArgumentOutOfRangeException("maxVal", maxVal,
                    "Max value must be greaten than 0.");
            }

            return ((int)Math.Log(maxVal, 256) + 1) * 2;
        }

        /*====================================================================*/

        /// <summary>
        /// Generates hex string format with digit count need to display max val
        /// </summary>
        /// <param name="maxVal">The max value</param>
        /// <returns>The hex string formater</returns>
        public static string maxVal2HexStrFormat
            (int maxVal)
        {
            if (maxVal <= 0)
            {
                throw new ArgumentOutOfRangeException("maxVal", maxVal,
                    "Max value must be grater than 0.");
            }

            return "0x{0:X" + maxVal2HexDigit(maxVal).ToString() + "}";
        }

        /*====================================================================*/

    }

    /*========================================================================*/

}

/*============================================================================*/
