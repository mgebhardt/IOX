using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IOX.UserControls;

/*============================================================================*/

namespace IOX
{

    /*========================================================================*/

    public partial class FormMain : Form
    {

        /*====================================================================*/

        #region Private declarations

        /*====================================================================*/

        private ucComSettings ucCS;

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Constructor

        /*====================================================================*/

        /// <summary>
        /// Main form's constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            ucCS = new ucComSettings();
            ucCS.Connect += new ucComSettings.ConnectHandler(ucCS_Connect);
            this.Controls.Add(ucCS);
            lblConnectState.Visible = true;
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Private methodes

        /*====================================================================*/



        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Private event handler

        /*====================================================================*/

        private void FormMain_Load(object sender, EventArgs e)
        {
            
        }

        /*====================================================================*/

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Close serial port if open */
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        /*====================================================================*/

        void ucCS_Connect(object sender, ConnectEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            else
            {
                try
                {
                    serialPort.PortName = e.PortName;
                    serialPort.BaudRate = e.BaudRate;
                    serialPort.DataBits = e.DataBits;
                    serialPort.Parity = e.Parity;
                    serialPort.StopBits = e.StopBits;
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not open com port.\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ucCS.ConnectState = serialPort.IsOpen;
            lblConnectState.Text = ucCS.getStatusString();
            if (serialPort.IsOpen)
            {
                lblConnectState.BackColor = Color.LightGreen;
            }
            else
            {
                lblConnectState.BackColor = Color.Red;
            }
        }

        /*====================================================================*/

        #endregion
        
        /*====================================================================*/

    }

    /*========================================================================*/

}

/*============================================================================*/
