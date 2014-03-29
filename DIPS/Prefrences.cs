using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Runtime.InteropServices;

namespace DIPS
{
    public partial class Prefrences : Form
    {
        Boolean Reset = false;
        public Prefrences()
        {
            InitializeComponent();
            Showsettings();
            if (cBProxy.Checked == false)
            {
                gBProxy.Height = 50;
                cBAuth.Hide();
                labelpd.Hide();
                labelport.Hide();
                tBdomain.Hide();
                tBPort.Hide();
            }
            if (cBAuth.Checked == false)
            {
                labelU.Hide();
                labelP.Hide();
                tbProxyUname.Hide();
                tbPassword.Hide();
            }

        }

        private void Prefrences_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.FirstTime = false;
        }

        private void Showsettings()
        {
            cbFB.Checked = Properties.Settings.Default.FacebookPanel;
            cBGoogle.Checked = Properties.Settings.Default.GoogleS;
            cBCalc.Checked = Properties.Settings.Default.Calc;
            cbTwitter.Checked = Properties.Settings.Default.TwitterPanel;
            cBSudoku.Checked = Properties.Settings.Default.Sudoku;
            cBUConvert.Checked = Properties.Settings.Default.UConv;
            cBCalender.Checked = Properties.Settings.Default.Calender;
            cBSlides.Checked = Properties.Settings.Default.Slide;
            cBProxy.Checked = Properties.Settings.Default.IsProxy;
            cBAuth.Checked = Properties.Settings.Default.ProxyAuth;
            cBStock.Checked = Properties.Settings.Default.Stock;
            cBWeather.Checked = Properties.Settings.Default.Weather;
            cBMandV.Checked = Properties.Settings.Default.VideoP;
            cByoutube.Checked = Properties.Settings.Default.Youtube;
            cBSticky.Checked = Properties.Settings.Default.Sticky;
            cBCurrencyConv.Checked = Properties.Settings.Default.CConv;
            cBNews.Checked = Properties.Settings.Default.News;
            tBdomain.Text = Properties.Settings.Default.ProxyDomain;
            tbPassword.Text = DIPS.DIPSEncryption.ToInsecureString(DIPS.DIPSEncryption.DecryptString(Properties.Settings.Default.ProxyPassword));
            tBPort.Text = Properties.Settings.Default.ProxyPort;
            tbProxyUname.Text = Properties.Settings.Default.ProxyUsername;
            cbOffline.Checked = true;
            cBOnline.Checked = true;
            cbOffline.Checked = Properties.Settings.Default.Offline;
            cBOnline.Checked = Properties.Settings.Default.Online;

        }

        private void cBAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (cBAuth.Checked == true)
            {
                gBProxy.Height = 200;
                labelU.Show();
                labelP.Show();
                tbProxyUname.Show();
                tbPassword.Show();
            }
            else
            {
                gBProxy.Height = 130;
                labelU.Hide();
                labelP.Hide();
                tbProxyUname.Hide();
                tbPassword.Hide();
            }
        }

        private void cBProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (cBProxy.Checked == false)
            {
                cBAuth.Checked = false;
                gBProxy.Height = 50;
                cBAuth.Hide();
                labelpd.Hide();
                labelport.Hide();
                tBdomain.Hide();
                tBPort.Hide();
            }
            else
            {
                gBProxy.Height = 130;
                cBAuth.Show();
                labelpd.Show();
                labelport.Show();
                tBdomain.Show();
                tBPort.Show();
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            // MainDIPS.Paneldips = new DIPSPanel(MainDIPS.Screenheight);
            this.Dispose();
        }

        private void bSubmit_Click(object sender, EventArgs e)
        {
            if (!Reset)
            {
                Properties.Settings.Default.ProxyUsername = tbProxyUname.Text;
                Properties.Settings.Default.ProxyPassword = DIPSEncryption.EncryptString(DIPSEncryption.ToSecureString(tbPassword.Text));
                Properties.Settings.Default.ProxyAuth = cBAuth.Checked;
                Properties.Settings.Default.IsProxy = cBProxy.Checked;
                Properties.Settings.Default.ProxyDomain = tBdomain.Text;
                Properties.Settings.Default.ProxyPort = tBPort.Text;
                Properties.Settings.Default.FacebookPanel = cbFB.Checked;
                Properties.Settings.Default.ProxyAddress = "http://" + tBdomain.Text + ":" + tBPort.Text;
                Properties.Settings.Default.Calender = cBCalender.Checked;
                Properties.Settings.Default.GoogleS = cBGoogle.Checked;
                Properties.Settings.Default.Calc = cBCalc.Checked;
                Properties.Settings.Default.TwitterPanel = cbTwitter.Checked;
                Properties.Settings.Default.Sudoku = cBSudoku.Checked;
                Properties.Settings.Default.UConv = cBUConvert.Checked;
                Properties.Settings.Default.Calender = cBCalender.Checked;
                Properties.Settings.Default.Slide = cBSlides.Checked;
                Properties.Settings.Default.Stock = cBStock.Checked;
                Properties.Settings.Default.Weather = cBWeather.Checked;
                Properties.Settings.Default.VideoP = cBMandV.Checked;
                Properties.Settings.Default.Sticky = cBSticky.Checked;
                Properties.Settings.Default.CConv = cBCurrencyConv.Checked;
                Properties.Settings.Default.News = cBNews.Checked;
                Properties.Settings.Default.Youtube = cByoutube.Checked;
                Properties.Settings.Default.Online = cBOnline.Checked;
                Properties.Settings.Default.Offline = cbOffline.Checked;
                Properties.Settings.Default.Save();
                Reset = false;
            }
            // MainDIPS.Paneldips = new DIPSPanel(MainDIPS.Screenheight);
            this.Dispose();
        }
        /* static String SecureStringToString(SecureString value)
         {
             IntPtr bstr = Marshal.SecureStringToBSTR(value);
             try
             {
                 return Marshal.PtrToStringBSTR(bstr);
             }
             finally
             {
                 Marshal.FreeBSTR(bstr);
             }
         }*/


        private void bReset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Reset = true;
        }

        private void cByoutube_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cBOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (!cBOnline.Checked)
            {
                cbFB.Checked = false;
                cbTwitter.Checked = false;
                cBStock.Checked = false;
                cBWeather.Checked = false;
                cByoutube.Checked = false;
                cBCurrencyConv.Checked = false;
                cBNews.Checked = false;
                cBGoogle.Checked = false;
                cbFB.Enabled = false;
                cbTwitter.Enabled = false;
                cBStock.Enabled = false;
                cBWeather.Enabled = false;
                cByoutube.Enabled = false;
                cBCurrencyConv.Enabled = false;
                cBNews.Enabled = false;
                cBGoogle.Enabled = false;
            }
            else
            {
                cbFB.Enabled = true;
                cbTwitter.Enabled = true;
                cBStock.Enabled = true;
                cBWeather.Enabled = true;
                cByoutube.Enabled = true;
                cBCurrencyConv.Enabled = true;
                cBNews.Enabled = true;
                cBGoogle.Enabled = true;
            }
        }

        private void cbOffline_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOffline.Checked)
            {
                cBSlides.Checked = false;
                cBCalc.Checked = false;
                cBCalender.Checked = false;
                cBSticky.Checked = false;
                cBSudoku.Checked = false;
                cBUConvert.Checked = false;
                cBMandV.Checked = false;

                cBSlides.Enabled = false;
                cBCalc.Enabled = false;
                cBCalender.Enabled = false;
                cBSticky.Enabled = false;
                cBSudoku.Enabled = false;
                cBUConvert.Enabled = false;
                cBMandV.Enabled = false;
            }
            else
            {
                cBSlides.Enabled = true;
                cBCalc.Enabled = true;
                cBCalender.Enabled = true;
                cBSticky.Enabled = true;
                cBSudoku.Enabled = true;
                cBUConvert.Enabled = true;
                cBMandV.Enabled = true;
            }
            
        }


    }
}
