using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;

        public Form1()
        {
            InitializeComponent();
            InitializeChromeDriver();
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
        }

        private void InitializeChromeDriver()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                PerformEbaySearch(searchTerm);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda: {ex.Message}");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                driver.Navigate().Back();
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda: {ex.Message}");
            }
        }

        private void PerformEbaySearch(string searchTerm)
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.ebay.com");

                IWebElement searchBox = driver.FindElement(By.Id("gh-ac"));
                searchBox.SendKeys(searchTerm);

                IWebElement searchButton = driver.FindElement(By.Id("gh-btn"));
                searchButton.Click();

                string searchResultUrl = driver.Url;

                textBox2.Text = searchResultUrl;

                richTextBox1.AppendText(searchResultUrl + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            driver?.Quit();
        }
    }
}
