using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/*============================================================================*/

namespace IOX.UserControls
{
    
    /*========================================================================*/

    /// <summary>
    /// Digital in user control
    /// </summary>
    public partial class ucDigitalIn : UserControl
    {

        /*====================================================================*/

        #region Private declarations

        /*====================================================================*/

        private int fBitCount;
        private int fMaxBitCount;
        private int fValue;
        private List<CheckBox> fBitSwitch;
        private List<Button> fBitButton;
        private Label fLblHexVal;

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Constructor

        /*====================================================================*/

        /// <summary>
        /// Initialisizes a new digital in user control
        /// </summary>
        /// <param name="name">The name to display</param>
        /// <param name="bitCount">The count of Bits</param>
        /// <param name="maxBitCount">The maximum count of Bits for align</param>
        public ucDigitalIn
            (string name = "", int bitCount = 8, int maxBitCount = 8)
        {
            InitializeComponent();

            fBitCount = 0;
            fMaxBitCount = 0;
            fValue = 0;
            fBitSwitch = new List<CheckBox>();
            fBitButton = new List<Button>();

            tlp.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            tlp.RowStyles.Clear();
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.RowCount = 2;

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

        #region Private event handler

        /*====================================================================*/

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            /* Check type before typecast */
            if(sender.GetType() != typeof(Button))
            {
                return;
            }

            /* Get button and index */
            Button btn = (Button) sender;
            int index = fBitButton.IndexOf(btn);

            /* Switch color and update value */
            btn.BackColor = Color.Purple;
            fValue &= ~(1 << index);
            fLblHexVal.Text = string.Format(
                Helper.binDigit2HexStrFormat(fBitCount), fValue);
        }

        /*====================================================================*/

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            /* Check type before typecast */
            if (sender.GetType() != typeof(Button))
            {
                return;
            }

            /* Get button and index */
            Button btn = (Button)sender;
            int index = fBitButton.IndexOf(btn);

            /* Switch color and update value */
            btn.BackColor = Color.Violet;
            fValue |= (1 << index);
            fLblHexVal.Text = string.Format(
                Helper.binDigit2HexStrFormat(fBitCount), fValue);
        }

        /*====================================================================*/

        private void switch_Click(object sender, EventArgs e)
        {
            /* Check type before typecast */
            if (sender.GetType() != typeof(CheckBox))
            {
                return;
            }

            /* Get checkbox and index */
            CheckBox chb = (CheckBox)sender;
            int index = fBitSwitch.IndexOf(chb);

            /* Switch color and update value */
            if (chb.Checked)
            {
                chb.BackColor = Color.LightBlue;
                fBitButton[index].Enabled = false;
                fValue |= (1 << index); 
            }
            else
            {
                chb.BackColor = Color.Blue;
                fBitButton[index].Enabled = true;
                fValue &= ~(1 << index);
            }
            fLblHexVal.Text = string.Format(
                Helper.binDigit2HexStrFormat(fBitCount), fValue);
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
        void setName
            (string name)
        {
            gb.Text = name;
        }

        /*====================================================================*/

        /// <summary>
        /// Sets the Bit count
        /// </summary>
        /// <param name="bitCount">The count of Bits to set</param>
        /// <param name="maxBitCount">The maximum of Bits for proper align</param>
        void setBitCount
            (int bitCount, int maxBitCount)
        {
            if ((bitCount == fBitCount) && (maxBitCount == fMaxBitCount))
            {
                /* Nothing to do */
                return;
            }

            /* Remove all old buttons */
            tlp.ColumnStyles.Clear();
            tlp.ColumnCount = 0;
            fBitSwitch.Clear();
            fBitButton.Clear();

            /* Add fist column with hex value */
            int col = tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,
                100 / (maxBitCount + 2) * 2));
            fLblHexVal.Text = string.Format(
                Helper.binDigit2HexStrFormat(bitCount), 0);
            tlp.Controls.Add(fLblHexVal, col, 0);
            tlp.SetRowSpan(fLblHexVal, 2);

            /* Add spacer */
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

                lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = "";
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                tlp.Controls.Add(lbl, col, 1);
            }

            /* Add buttons */
            for (int i = 0; i < bitCount; i++)
            {
                col = tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,
                    100 / (maxBitCount + 2)));

                CheckBox chb = new CheckBox();
                chb.AutoSize = false;
                chb.Text = string.Format("Bit {0}", bitCount - i - 1);
                chb.Dock = DockStyle.Fill;
                chb.TextAlign = ContentAlignment.MiddleCenter;
                chb.ForeColor = Color.Yellow;
                chb.BackColor = Color.Blue;
                chb.Appearance = Appearance.Button;
                chb.Cursor = Cursors.Hand;
                chb.Click += new EventHandler(switch_Click);
                tlp.Controls.Add(chb, col, 0);
                fBitSwitch.Add(chb);

                Button btn = new Button();
                btn.AutoSize = false;
                btn.Text = string.Format("Bit {0}", bitCount - i - 1);
                btn.Dock = DockStyle.Fill;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.ForeColor = Color.Yellow;
                btn.BackColor = Color.Purple;
                btn.Cursor = Cursors.Hand;
                btn.MouseDown += new MouseEventHandler(button_MouseDown);
                btn.MouseUp += new MouseEventHandler(button_MouseUp);
                tlp.Controls.Add(btn, col, 1);
                fBitButton.Add(btn);
            }

            /* Clean up */
            fBitCount = bitCount;
            fMaxBitCount = maxBitCount;
            fValue = 0;
            fBitSwitch.Reverse();
            fBitButton.Reverse();
        }

        /*====================================================================*/
        
        /// <summary>
        /// Gets the value
        /// </summary>
        /// <returns>the value</returns>
        int getValue
            ()
        {
            return fValue;
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

    }

    /*========================================================================*/

}

/*============================================================================*/