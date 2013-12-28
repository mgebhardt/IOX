using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
