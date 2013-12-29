using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;

/*============================================================================*/

namespace IOX.UserControls
{

    /*========================================================================*/

    /// <summary>
    /// Com port settings user control
    /// </summary>
    public partial class ucComSettings : UserControl
    {
        
        /*====================================================================*/

        #region Public events
        
        /*====================================================================*/

        /// <summary>
        /// Com port connect request handler
        /// </summary>
        /// <param name="sender">The calling object</param>
        /// <param name="e">The event args</param>
        public delegate void ConnectHandler(object sender, ConnectEventArgs e);

        /*====================================================================*/

        /// <summary>
        /// Com port connect request
        /// </summary>
        public event ConnectHandler Connect;

        /*====================================================================*/

        #endregion
        
        /*====================================================================*/

        #region Private declarations
        
        /*====================================================================*/

        private List<string> fPortNames;

        /*====================================================================*/

        #endregion
        
        /*====================================================================*/

        #region Private consts
        
        /*====================================================================*/

        private static readonly List<int> fBaudRates = new List<int> 
            { 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 76800, 
                115200, 230400 };
        private const int fDefaultBaudRate = 9600;

        private const int fMinDataBits = 5;
        private const int fMaxDataBits = 9;
        private const int fDefaultDataBits = 8;

        private static readonly List<Parity> fParities = new List<Parity>
            { Parity.None, Parity.Even, Parity.Odd };
        private const Parity fDefaultParity = Parity.None;

        private static readonly List<StopBits> fStopBits = new List<StopBits> 
            { StopBits.One, StopBits.OnePointFive, StopBits.Two };
        private const StopBits fDefaultStopBits = StopBits.One;

        /*====================================================================*/

        #endregion
        
        /*====================================================================*/

        #region Constructor
        
        /*====================================================================*/

        /// <summary>
        /// Initialisizes a new com port settings user control
        /// </summary>
        /// <param name="PortName">The com port's name to select</param>
        /// <param name="BaudRate">The com port's baud rate to select</param>
        /// <param name="DataBits">The number of data bits to select</param>
        /// <param name="Parity">The parity mode to select</param>
        /// <param name="StopBits">The number of stop bits to select</param>
        public ucComSettings
            (string PortName = null, int BaudRate = fDefaultBaudRate,
            int DataBits = fDefaultDataBits, Parity Parity = fDefaultParity, 
            StopBits StopBits = fDefaultStopBits)
        {
            InitializeComponent();

            fPortNames = new List<string>();
            refreshComPortList(PortName);

            cbBaudRate.Items.Clear();
            cbBaudRate.Items.AddRange(fBaudRates.Cast<object>().ToArray());
            cbBaudRate.SelectedIndex = fBaudRates.IndexOf(BaudRate);
            if (-1 == cbBaudRate.SelectedIndex)
            {
                cbBaudRate.SelectedIndex = fBaudRates.IndexOf(fDefaultBaudRate);
            }

            nudDataBits.Minimum = fMinDataBits;
            nudDataBits.Maximum = fMaxDataBits;
            if ((DataBits < fMinDataBits) || (DataBits > fMaxDataBits))
            {
                DataBits = 8;
            }
            nudDataBits.Value = DataBits;

            cbParity.Items.Clear();
            cbParity.Items.AddRange(fParities.Cast<object>().ToArray());
            cbParity.SelectedIndex = fParities.IndexOf(Parity);
            if (-1 == cbParity.SelectedIndex)
            {
                cbParity.SelectedIndex = fParities.IndexOf(fDefaultParity);
            }

            cbStopBits.Items.Clear();
            cbStopBits.Items.AddRange(fStopBits.Cast<object>().ToArray());
            cbStopBits.SelectedIndex = fStopBits.IndexOf(StopBits);
            if (-1 == cbStopBits.SelectedIndex)
            {
                cbStopBits.SelectedIndex = fStopBits.IndexOf(fDefaultStopBits);
            }

            btnConnect.Checked = false;
            btnConnect.Text = "Connect";
        }

        /*====================================================================*/

        #endregion
        
        /*====================================================================*/

        #region Public methodes
        
        /*====================================================================*/

        /// <summary>
        /// Refreshes the port name list
        /// </summary>
        /// <param name="PortName">The com port's name to select</param>
        public void refreshComPortList
            (string PortName = null)
        {
            if (string.IsNullOrEmpty(PortName) && (0 < fPortNames.Count))
            {
                PortName = fPortNames[cbPortName.SelectedIndex];
            }

            fPortNames.Clear();
            cbPortName.Items.Clear();
            fPortNames.AddRange(SerialPort.GetPortNames());
            if (0 == fPortNames.Count)
            {
                btnConnect.Enabled = false;
                return;
            }
            else
            {
                btnConnect.Enabled = true;
            }
            
            cbPortName.Items.AddRange(fPortNames.Cast<object>().ToArray());
            if (!string.IsNullOrEmpty(PortName))
            {
                cbPortName.SelectedIndex = fPortNames.IndexOf(PortName);
                if (-1 == cbPortName.SelectedIndex)
                {
                    cbPortName.SelectedIndex = 0;
                }
            }
            else
            {
                cbPortName.SelectedIndex = 0;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Generates a status string from com port settings
        /// </summary>
        /// <returns>The status string</returns>
        public string getStatusString
            ()
        {
            string str;
            if (ConnectState)
            {
                str = "Connected - ";
                str += PortName;
                str += string.Format(" {0:f1}kbps - ", BaudRate / 1000.0);
                str += DataBits.ToString();
                str += Parity.ToString().Substring(0, 1);
                switch(StopBits)
                {
                    case StopBits.One:
                        str += "1";
                        break;
                    case StopBits.OnePointFive:
                        str += "1.5";
                        break;
                    case StopBits.Two:
                        str += "2";
                        break;
                }
            }
            else
            {
                str = "- Disconnected -";
            }
            return str;
        }

        /*====================================================================*/

        #endregion
        
        /*====================================================================*/

        #region Public properties
        
        /*====================================================================*/

        /// <summary>
        /// Gets selected port name
        /// </summary>
        public string PortName
        {
            get
            {
                return fPortNames[cbPortName.SelectedIndex];
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets selected baud rate
        /// </summary>
        public int BaudRate
        {
            get
            {
                return fBaudRates[cbBaudRate.SelectedIndex];
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the default baud rate
        /// </summary>
        public int DefaultBaudRate
        {
            get
            {
                return fDefaultBaudRate;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets selected number of data bits
        /// </summary>
        public int DataBits
        {
            get
            {
                return (int)nudDataBits.Value;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the default number of data bits
        /// </summary>
        public int DefaultDataBits
        {
            get
            {
                return fDefaultDataBits;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Get the minimum number of data bits
        /// </summary>
        public int MinDataBits
        {
            get
            {
                return fMinDataBits;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the maximum number of data bits
        /// </summary>
        public int MaxDataBits
        {
            get
            {
                return fMaxDataBits;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets selected parity mode
        /// </summary>
        public Parity Parity
        {
            get
            {
                return fParities[cbParity.SelectedIndex];
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the default parity mode
        /// </summary>
        public Parity DefaultParity
        {
            get
            {
                return fDefaultParity;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets selected number of stop bits
        /// </summary>
        public StopBits StopBits
        {
            get
            {
                return fStopBits[cbStopBits.SelectedIndex];
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the default number of stop bits
        /// </summary>
        public StopBits DefaultStopBits
        {
            get
            {
                return fDefaultStopBits;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets or sets state of connect button
        /// </summary>
        public bool ConnectState
        {
            get
            {
                return btnConnect.Checked;
            }
            set
            {
                cbPortName.Enabled = !value;
                btnComPortRefresh.Enabled = !value;
                cbBaudRate.Enabled = !value;
                nudDataBits.Enabled = !value;
                cbParity.Enabled = !value;
                cbStopBits.Enabled = !value;
                btnConnect.Checked = value;
                btnConnect.Text = value ? "Disconnect" : "Connect";
            }
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Private event handler

        /*====================================================================*/

        private void btnComPortRefresh_Click(object sender, EventArgs e)
        {
            refreshComPortList();
        }

        /*====================================================================*/

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Connect != null)
            {
                Connect(this, new ConnectEventArgs(PortName, BaudRate, DataBits,
                    Parity, StopBits, ConnectState));
            }
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

    }

    /*========================================================================*/

    /// <summary>
    /// Event arguments for com port setting's conntect request
    /// </summary>
    public class ConnectEventArgs : EventArgs
    {

        /*====================================================================*/

        #region Private declarations

        /*====================================================================*/

        private string fPortName;
        private int fBaudRate;
        private int fDataBits;
        private Parity fParity;
        private StopBits fStopBits;
        private bool fConnectState;

        /*====================================================================*/
        
        #endregion
        
        /*====================================================================*/

        #region Constructor
        
        /*====================================================================*/

        /// <summary>
        /// Initialisizes a new connect request event argument
        /// </summary>
        /// <param name="PortName">The selected port name</param>
        /// <param name="BaudRate">The selected baud rate</param>
        /// <param name="DataBits">The selected number of data bits</param>
        /// <param name="Parity">The selected parity mode</param>
        /// <param name="StopBits">The selected number of stop bits</param>
        /// <param name="ConnectState">The current state of connect button</param>
        public ConnectEventArgs
            (string PortName, int BaudRate, int DataBits, Parity Parity,
            StopBits StopBits, bool ConnectState)
        {
            fPortName = PortName;
            fBaudRate = BaudRate;
            fDataBits = DataBits;
            fStopBits = StopBits;
            fParity = Parity;
            fConnectState = ConnectState;
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

        #region Public properties

        /*====================================================================*/

        /// <summary>
        /// Gets the selected port name
        /// </summary>
        public string PortName
        {
            get
            {
                return fPortName;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the selected baud rate
        /// </summary>
        public int BaudRate
        {
            get
            {
                return fBaudRate;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the selected number of data bits
        /// </summary>
        public int DataBits   
        {
            get
            {
                return fDataBits;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the selected parity mode
        /// </summary>
        public Parity Parity
        {
            get
            {
                return fParity;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the selected number of stop bits
        /// </summary>
        public StopBits StopBits
        {
            get
            {
                return fStopBits;
            }
        }

        /*====================================================================*/

        /// <summary>
        /// Gets the current state of connect button
        /// </summary>
        public bool ConnectState
        {
            get
            {
                return fConnectState;
            }
        }

        /*====================================================================*/

        #endregion

        /*====================================================================*/

    }

    /*========================================================================*/

}

/*============================================================================*/
