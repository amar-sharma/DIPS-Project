namespace DIPS
{
    partial class CurrencyConv
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbValue = new System.Windows.Forms.TextBox();
            this.tBresult = new System.Windows.Forms.TextBox();
            this.bConv = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cBFrom = new System.Windows.Forms.ComboBox();
            this.CBTo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(58, 66);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(133, 20);
            this.tbValue.TabIndex = 2;
            this.tbValue.TextChanged += new System.EventHandler(this.tbValue_TextChanged);
            // 
            // tBresult
            // 
            this.tBresult.Location = new System.Drawing.Point(58, 96);
            this.tBresult.Multiline = true;
            this.tBresult.Name = "tBresult";
            this.tBresult.Size = new System.Drawing.Size(197, 22);
            this.tBresult.TabIndex = 3;
            // 
            // bConv
            // 
            this.bConv.Location = new System.Drawing.Point(197, 64);
            this.bConv.Name = "bConv";
            this.bConv.Size = new System.Drawing.Size(58, 24);
            this.bConv.TabIndex = 4;
            this.bConv.Text = "Convert";
            this.bConv.UseVisualStyleBackColor = true;
            this.bConv.Click += new System.EventHandler(this.bConv_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label2.Location = new System.Drawing.Point(9, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(5, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label4.Location = new System.Drawing.Point(5, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Result";
            // 
            // cBFrom
            // 
            this.cBFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cBFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.cBFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBFrom.FormattingEnabled = true;
            this.cBFrom.Items.AddRange(new object[] {
            "AED\tUnited Arab Emirates Dirham",
            "ANG\tNetherlands Antilles Guilder",
            "ARS\tArgentina Peso",
            "AUD\tAustralia Dollar",
            "BGN\tBulgaria Lev",
            "BHD\tBahrain Dinar",
            "BND\tBrunei Darussalam Dollar",
            "BOB\tBolivia Boliviano",
            "BRL\tBrazil Real",
            "BWP\tBotswana Pula",
            "CAD\tCanada Dollar",
            "CHF\tSwitzerland Franc",
            "CLP\tChile Peso",
            "CNY\tChina Yuan Renminbi",
            "COP\tColombia Peso",
            "CRC\tCosta Rica Colon",
            "CZK\tCzech Republic Koruna",
            "DKK\tDenmark Krone",
            "DOP\tDominican Republic Peso",
            "DZD\tAlgeria Dinar",
            "EGP\tEgypt Pound",
            "EUR\tEuro Member Countries",
            "FJD\tFiji Dollar",
            "GBP\tUnited Kingdom Pound",
            "HKD\tHong Kong Dollar",
            "HNL\tHonduras Lempira",
            "HRK\tCroatia Kuna",
            "HUF\tHungary Forint",
            "IDR\tIndonesia Rupiah",
            "ILS\tIsrael Shekel",
            "INR\tIndia Rupee",
            "JMD\tJamaica Dollar",
            "JOD\tJordan Dinar",
            "JPY\tJapan Yen",
            "KES\tKenya Shilling",
            "KRW\tKorea (South) Won",
            "KWD\tKuwait Dinar",
            "KYD\tCayman Islands Dollar",
            "KZT\tKazakhstan Tenge",
            "LBP\tLebanon Pound",
            "LKR\tSri Lanka Rupee",
            "LTL\tLithuania Litas",
            "LVL\tLatvia Lat",
            "MAD\tMorocco Dirham",
            "MDL\tMoldova Leu",
            "MKD\tMacedonia Denar",
            "MUR\tMauritius Rupee",
            "MXN\tMexico Peso",
            "MYR\tMalaysia Ringgit",
            "NAD\tNamibia Dollar",
            "NGN\tNigeria Naira",
            "NIO\tNicaragua Cordoba",
            "NOK\tNorway Krone",
            "NPR\tNepal Rupee",
            "NZD\tNew Zealand Dollar",
            "OMR\tOman Rial",
            "PEN\tPeru Nuevo Sol",
            "PGK\tPapua New Guinea Kina",
            "PHP\tPhilippines Peso",
            "PKR\tPakistan Rupee",
            "PLN\tPoland Zloty",
            "PYG\tParaguay Guarani",
            "QAR\tQatar Riyal",
            "RON\tRomania New Leu",
            "RSD\tSerbia Dinar",
            "RUB\tRussia Ruble",
            "SAR\tSaudi Arabia Riyal",
            "SCR\tSeychelles Rupee",
            "SEK\tSweden Krona",
            "SGD\tSingapore Dollar",
            "SVC\tEl Salvador Colon",
            "THB\tThailand Baht",
            "TND\tTunisia Dinar",
            "TRY\tTurkey Lira",
            "TTD\tTrinidad and Tobago Dollar",
            "TWD\tTaiwan New Dollar",
            "TZS\tTanzania Shilling",
            "UAH\tUkraine Hryvna",
            "UGX\tUganda Shilling",
            "USD\tUnited States Dollar",
            "UYU\tUruguay Peso",
            "UZS\tUzbekistan Som",
            "VEF\tVenezuela Bolivar",
            "VND\tViet Nam Dong",
            "YER\tYemen Rial",
            "ZAR\tSouth Africa Rand"});
            this.cBFrom.Location = new System.Drawing.Point(58, 8);
            this.cBFrom.Name = "cBFrom";
            this.cBFrom.Size = new System.Drawing.Size(197, 21);
            this.cBFrom.Sorted = true;
            this.cBFrom.TabIndex = 9;
            this.cBFrom.Text = "Currency";
            // 
            // CBTo
            // 
            this.CBTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CBTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.CBTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBTo.FormattingEnabled = true;
            this.CBTo.Items.AddRange(new object[] {
            "AED\tUnited Arab Emirates Dirham",
            "ANG\tNetherlands Antilles Guilder",
            "ARS\tArgentina Peso",
            "AUD\tAustralia Dollar",
            "BGN\tBulgaria Lev",
            "BHD\tBahrain Dinar",
            "BND\tBrunei Darussalam Dollar",
            "BOB\tBolivia Boliviano",
            "BRL\tBrazil Real",
            "BWP\tBotswana Pula",
            "CAD\tCanada Dollar",
            "CHF\tSwitzerland Franc",
            "CLP\tChile Peso",
            "CNY\tChina Yuan Renminbi",
            "COP\tColombia Peso",
            "CRC\tCosta Rica Colon",
            "CZK\tCzech Republic Koruna",
            "DKK\tDenmark Krone",
            "DOP\tDominican Republic Peso",
            "DZD\tAlgeria Dinar",
            "EGP\tEgypt Pound",
            "EUR\tEuro Member Countries",
            "FJD\tFiji Dollar",
            "GBP\tUnited Kingdom Pound",
            "HKD\tHong Kong Dollar",
            "HNL\tHonduras Lempira",
            "HRK\tCroatia Kuna",
            "HUF\tHungary Forint",
            "IDR\tIndonesia Rupiah",
            "ILS\tIsrael Shekel",
            "INR\tIndia Rupee",
            "JMD\tJamaica Dollar",
            "JOD\tJordan Dinar",
            "JPY\tJapan Yen",
            "KES\tKenya Shilling",
            "KRW\tKorea (South) Won",
            "KWD\tKuwait Dinar",
            "KYD\tCayman Islands Dollar",
            "KZT\tKazakhstan Tenge",
            "LBP\tLebanon Pound",
            "LKR\tSri Lanka Rupee",
            "LTL\tLithuania Litas",
            "LVL\tLatvia Lat",
            "MAD\tMorocco Dirham",
            "MDL\tMoldova Leu",
            "MKD\tMacedonia Denar",
            "MUR\tMauritius Rupee",
            "MXN\tMexico Peso",
            "MYR\tMalaysia Ringgit",
            "NAD\tNamibia Dollar",
            "NGN\tNigeria Naira",
            "NIO\tNicaragua Cordoba",
            "NOK\tNorway Krone",
            "NPR\tNepal Rupee",
            "NZD\tNew Zealand Dollar",
            "OMR\tOman Rial",
            "PEN\tPeru Nuevo Sol",
            "PGK\tPapua New Guinea Kina",
            "PHP\tPhilippines Peso",
            "PKR\tPakistan Rupee",
            "PLN\tPoland Zloty",
            "PYG\tParaguay Guarani",
            "QAR\tQatar Riyal",
            "RON\tRomania New Leu",
            "RSD\tSerbia Dinar",
            "RUB\tRussia Ruble",
            "SAR\tSaudi Arabia Riyal",
            "SCR\tSeychelles Rupee",
            "SEK\tSweden Krona",
            "SGD\tSingapore Dollar",
            "SVC\tEl Salvador Colon",
            "THB\tThailand Baht",
            "TND\tTunisia Dinar",
            "TRY\tTurkey Lira",
            "TTD\tTrinidad and Tobago Dollar",
            "TWD\tTaiwan New Dollar",
            "TZS\tTanzania Shilling",
            "UAH\tUkraine Hryvna",
            "UGX\tUganda Shilling",
            "USD\tUnited States Dollar",
            "UYU\tUruguay Peso",
            "UZS\tUzbekistan Som",
            "VEF\tVenezuela Bolivar",
            "VND\tViet Nam Dong",
            "YER\tYemen Rial",
            "ZAR\tSouth Africa Rand"});
            this.CBTo.Location = new System.Drawing.Point(58, 37);
            this.CBTo.Name = "CBTo";
            this.CBTo.Size = new System.Drawing.Size(197, 21);
            this.CBTo.Sorted = true;
            this.CBTo.TabIndex = 10;
            this.CBTo.Text = "Currency";
            // 
            // CurrencyConv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 121);
            this.ControlBox = false;
            this.Controls.Add(this.CBTo);
            this.Controls.Add(this.cBFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bConv);
            this.Controls.Add(this.tBresult);
            this.Controls.Add(this.tbValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CurrencyConv";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Currency Converter";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Load += new System.EventHandler(this.CurrencyConv_Load);
            this.VisibleChanged += new System.EventHandler(this.CurrencyConv_VisibleChanged);
            this.DoubleClick += new System.EventHandler(this.CurrencyConv_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.TextBox tBresult;
        private System.Windows.Forms.Button bConv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBFrom;
        private System.Windows.Forms.ComboBox CBTo;
    }
}