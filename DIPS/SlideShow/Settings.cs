/********************************** Module Header **********************************\
* Module Name:  Settings.cs
* Project:      CSImageFullScreenSlideShow
* Copyright (c) Microsoft Corporation.
*
* The code sets the internal for Timer control.
*
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
* All other rights reserved.
*
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
* EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
* MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***********************************************************************************/
using System;
using System.Windows.Forms;

namespace DIPS.SlideShow
{
    public partial class Settings : Form
    {
        Timer _timr;
       
        /// <summary>
        /// Customize Constructer for importing control reference address.
        /// </summary>
        public Settings(ref Timer timr)
        {
            InitializeComponent();
            _timr = timr;
            this.dtpInternal.Value = timr.Interval/1000;
        }

        /// <summary>
        /// Cancel manipulate.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Save the internal for Timer control and close the child Windows.
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            _timr.Interval = int.Parse(this.dtpInternal.Value.ToString())*1000;
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void dtpInternal_ValueChanged(object sender, EventArgs e)
        {

        }

    

    }
}
