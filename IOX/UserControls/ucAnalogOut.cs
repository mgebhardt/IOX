/*
 * Version      1.0.0
 * Package      IO Exchanger
 * Subpackage   User control analog out
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
    /// Analog out user control
    /// </summary>
    public partial class ucAnalogOut : UserControl
    {

        /*====================================================================*/

        #region Private declarations

        /*====================================================================*/

        private int fChannels;
        private int fMaxVal;
        private List<ProgressBar> fChannelProgressBar;
        private List<Label> fChannelName;
        private List<Label> fChannelValue;

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Constructor

        /*====================================================================*/

        /// <summary>
        /// Initialisizes a new analog out user control
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="channels">The channel count</param>
        /// <param name="maxVal">The max channel value</param>
        public ucAnalogOut
            (string name = "", int channels = 2, int maxVal = 255)
        {
            InitializeComponent();

            fChannels = 0;
            fMaxVal = 0;
            fChannelProgressBar = new List<ProgressBar>();
            fChannelName = new List<Label>();
            fChannelValue = new List<Label>();

            tlp.ColumnStyles.Clear();
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            tlp.ColumnCount = 3;

            setName(name);
            setChannels(channels, maxVal);
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Public methodes

        /*====================================================================*/

        /// <summary>
        /// Sets the field name
        /// </summary>
        /// <param name="name"></param>
        public void setName
            (string name)
        {
            gb.Text = name;
        }

        /*====================================================================*/

        /// <summary>
        /// Sets the channel count and max channel value
        /// </summary>
        /// <param name="channels">The channel count</param>
        /// <param name="maxVal">The max channel value</param>
        public void setChannels
            (int channels, int maxVal)
        {
            if ((channels == fChannels) && (maxVal == fMaxVal))
            {
                /* Nothing to do */
                return;
            }

            /* Remove all old buttons */
            tlp.RowStyles.Clear();
            tlp.RowCount = 0;
            fChannelProgressBar.Clear();
            fChannelName.Clear();
            fChannelValue.Clear();

            /* Add Channels */
            for (int i = 0; i < channels; i++)
            {
                int row = tlp.RowStyles.Add(new RowStyle(SizeType.Percent,
                    100 / channels));

                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = string.Format("CH {0}", channels - i - 1);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                tlp.Controls.Add(lbl, 0, row);
                fChannelName.Add(lbl);

                lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = string.Format(
                    Helper.maxVal2HexStrFormat(maxVal), 0);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                tlp.Controls.Add(lbl, 1, row);
                fChannelValue.Add(lbl);

                ProgressBar pgb = new ProgressBar();
                pgb.AutoSize = false;
                pgb.Maximum = maxVal;
                pgb.Dock = DockStyle.Fill;
                tlp.Controls.Add(pgb, 2, row);
                fChannelProgressBar.Add(pgb);
            }

            /* Clean up */
            fChannels = channels;
            fMaxVal = maxVal;
            fChannelName.Reverse();
            fChannelProgressBar.Reverse();
            fChannelValue.Reverse();
        }

        /*====================================================================*/

        /// <summary>
        /// Sets the value for each channel
        /// </summary>
        /// <param name="values">List containing the value of each channel</param>
        public void setValues
            (List<int> values)
        {
            if (values.Count != fChannels)
            {
                throw new ArgumentException(
                    "Each analog channel needs one value.", "values.Count");
            }

            for (int i = 0; i < fChannels; i++)
            {
                fChannelProgressBar[i].Value = values[i];
                fChannelValue[i].Text = string.Format(
                    Helper.maxVal2HexStrFormat(fMaxVal), values[i]);
            }
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

    }

    /*========================================================================*/

}

/*============================================================================*/
