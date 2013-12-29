/*
 * Version      1.0.0
 * Package      IO Exchanger
 * Subpackage   User control analog in
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
    /// Analog in user control
    /// </summary>
    public partial class ucAnalogIn : UserControl
    {

        /*====================================================================*/

        #region Private declarations

        /*====================================================================*/

        private int fChannels;
        private int fMaxVal;
        private List<TrackBar> fChannelTrackBar;
        private List<Label> fChannelName;
        private List<Label> fChannelValue;

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Constructor

        /*====================================================================*/
        
        /// <summary>
        /// Initialisizes a new analog in user control
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="channels">The channel count</param>
        /// <param name="maxVal">The channel's max value </param>
        public ucAnalogIn
            (string name = "", int channels = 2, int maxVal = 255)
        {
            InitializeComponent();

            fChannels = 0;
            fMaxVal = 0;
            fChannelTrackBar = new List<TrackBar>();
            fChannelName = new List<Label>();
            fChannelValue = new List<Label>();

            tlp.RowStyles.Clear();
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            tlp.RowCount = 3;

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
        /// Sets the channel count and channel's max value
        /// </summary>
        /// <param name="channels">The channel count</param>
        /// <param name="maxVal">The channel's max value</param>
        public void setChannels
            (int channels, int maxVal)
        {
            if ((channels == fChannels) && (maxVal == fMaxVal))
            {
                /* Nothing to do */
                return;
            }

            /* Remove all old buttons */
            tlp.ColumnStyles.Clear();
            tlp.ColumnCount = 0;
            fChannelTrackBar.Clear();
            fChannelName.Clear();
            fChannelValue.Clear();

            /* Add Channels */
            for (int i = 0; i < channels; i++)
            {
                int col = tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,
                    100 / channels));

                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = string.Format("CH {0}", channels - i - 1);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                tlp.Controls.Add(lbl, col, 0);
                fChannelName.Add(lbl);

                TrackBar tkb = new TrackBar();
                tkb.AutoSize = false;
                tkb.Maximum = maxVal;
                tkb.Dock = DockStyle.Fill;
                tkb.Orientation = Orientation.Vertical;
                tkb.TickStyle = TickStyle.Both;
                tkb.TickFrequency = maxVal / 128;
                tkb.LargeChange = maxVal / 8;
                tlp.Controls.Add(tkb, col, 1);
                fChannelTrackBar.Add(tkb);

                lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = string.Format(
                    Helper.maxVal2HexStrFormat(maxVal), 0);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                tkb.ValueChanged += new EventHandler(TrackBar_ValueChanged);
                tlp.Controls.Add(lbl, col, 2);
                fChannelValue.Add(lbl);
            }

            /* Clean up */
            fChannels = channels;
            fMaxVal = maxVal;
            fChannelName.Reverse();
            fChannelTrackBar.Reverse();
            fChannelValue.Reverse();
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the channel's values
        /// </summary>
        /// <returns>The List of channel's values</returns>
        public List<int> getValues
            ()
        {
            List<int> values = new List<int>();

            foreach (TrackBar tkb in fChannelTrackBar)
            {
                values.Add(tkb.Value);
            }

            if (values.Count != fChannels)
            {
                throw new Exception("Values.Count missmatch channel count.");
            }

            return values;
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Private event handler

        /*====================================================================*/

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(TrackBar))
            {
                return;
            }

            TrackBar tkb = (TrackBar)sender;
            int index = fChannelTrackBar.IndexOf(tkb);

            fChannelValue[index].Text = string.Format(
                    Helper.maxVal2HexStrFormat(fMaxVal), tkb.Value);
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

    }

    /*========================================================================*/

}

/*============================================================================*/
