/*
 * Version      1.0.0
 * Package      IO Exchanger
 * Subpackage   User control digital out
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/*============================================================================*/

namespace IOX.UserControls
{

    /*========================================================================*/

    /// <summary>
    /// Digital out user control
    /// </summary>
    public partial class ucDigitalOut : UserControl
    {

        /*====================================================================*/

        #region Private declarations

        /*====================================================================*/

        private int fBitCount;
        private int fMaxBitCount;
        private List<Label> fBitLabel;
        private Label fLblHexVal;

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Constructor

        /*====================================================================*/

        /// <summary>
        /// Initialisizes a new digital out user control
        /// </summary>
        /// <param name="name">The name to display</param>
        /// <param name="bitCount">The count of Bits</param>
        /// <param name="maxBitCount">The maximum count of Bits for align</param>
        public ucDigitalOut
            (string name = "", int bitCount = 8, int maxBitCount = 8)
        {
            InitializeComponent();

            fBitCount = 0;
            fMaxBitCount = 0;
            fBitLabel = new List<Label>();

            tlp.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            fLblHexVal = new Label();
            fLblHexVal.AutoSize = false;
            fLblHexVal.Dock = DockStyle.Fill;
            fLblHexVal.TextAlign = ContentAlignment.MiddleCenter;
            fLblHexVal.Text = "0x00";

            setName(name);
            setBitCount(bitCount, maxBitCount);
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Public methodes

        /*====================================================================*/

        /// <summary>
        /// Sets the name to display
        /// </summary>
        /// <param name="name">The name</param>
        public void setName
            (string name)
        {
            gb.Text = name;
        }

        /*====================================================================*/

        /// <summary>
        /// Sets the given Bit count
        /// </summary>
        /// <param name="bitCount">The count of Bits to set</param>
        /// <param name="maxBitCount">The maximum of Bits for proper align</param>
        public void setBitCount
            (int bitCount, int maxBitCount)
        {
            if ((bitCount == fBitCount) && (maxBitCount == fMaxBitCount))
            {
                /* Nothing to do */
                return;
            }

            tlp.ColumnStyles.Clear();
            tlp.ColumnCount = 0;
            fBitLabel.Clear();

            int col = tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 
                100 / (maxBitCount + 2) * 2));
            fLblHexVal.Text = string.Format(
                Helper.binDigit2HexStrFormat(bitCount), 0);
            tlp.Controls.Add(fLblHexVal, col, 0);

            if (bitCount < maxBitCount)
            {
                col = tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 
                    100 / (maxBitCount + 2) * (maxBitCount - bitCount)));
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = "";
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                tlp.Controls.Add(lbl, col, 0);
            }

            for (int i = 0; i < bitCount; i++)
            {
                col = tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 
                    100 / (maxBitCount + 2)));

                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = string.Format("Bit {0}", bitCount - i - 1);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.BackColor = Color.Green;
                tlp.Controls.Add(lbl, col, 0);
                fBitLabel.Add(lbl);
            }

            fBitCount = bitCount;
            fMaxBitCount = maxBitCount;
            fBitLabel.Reverse();
        }

        /*====================================================================*/

        /// <summary>
        /// Sets the given value
        /// </summary>
        /// <param name="val">The value to set</param>
        public void setValue
            (int val)
        {
            fLblHexVal.Text = string.Format(
                Helper.binDigit2HexStrFormat(fBitCount), 
                val & (1 << fBitCount) - 1);
            for (int i = 0; i < fBitCount; i++)
            {
                if ((val & (1 << i)) > 0)
                {
                    fBitLabel[i].BackColor = Color.Lime;
                }
                else
                {
                    fBitLabel[i].BackColor = Color.Green;
                }
            }
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

    }

    /*========================================================================*/

}

/*============================================================================*/