using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coins
{
    public partial class MainForm : Form
    {
        private CoinGame _coinGame = new CoinGame();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Exit event lambda expression
            exitButton.Click += (from, ea) => this.Close();

            // Reset event lambda expression
            resetButton.Click += (from, ea) =>
            {
                _coinGame.Reset();
                moneyPictureBox.Invalidate();
                CheckAmount();
            };

            // PictureBox Mouseup lambda expression
            addPennyButton.Click += (from, ea) => { _coinGame.Add(new Penny()); moneyPictureBox.Invalidate(); CheckAmount(); };
            addThreeCentButton.Click += (from, ea) => { _coinGame.Add(new ThreeCent()); moneyPictureBox.Invalidate(); CheckAmount(); };
            addNickelButton.Click += (from, ea) => { _coinGame.Add(new Nickel()); moneyPictureBox.Invalidate(); CheckAmount(); };
            addDimeButton.Click += (from, ea) => { _coinGame.Add(new Dime()); moneyPictureBox.Invalidate(); CheckAmount(); };
            addQuarterButton.Click += (from, ea) => { _coinGame.Add(new Quarter()); moneyPictureBox.Invalidate(); CheckAmount(); };

            // Setup Picturebox Paint event
            moneyPictureBox.Paint += (from, ea) =>
            {
                _coinGame.Update(ea.Graphics, moneyPictureBox.ClientSize);
                currentValueLabel.Text = _coinGame.TotalWorth.ToString();
                targetValueLabel.Text = _coinGame.TargetAmount.ToString();
                countPennyLabel.Text = _coinGame.CountPenny.ToString();
                countThreeCentLabel.Text = _coinGame.CountThreeCent.ToString();
                countNickelLabel.Text = _coinGame.CountNickel.ToString();
                countDimeLabel.Text = _coinGame.CountDime.ToString();
                countQuarterLabel.Text = _coinGame.CountQuarter.ToString();
            };

            // PictureBox Mouseup lambda expression
            moneyPictureBox.MouseUp += (from, ea) => { _coinGame.Remove(ea.Location); moneyPictureBox.Invalidate(); CheckAmount(); };
        }

        private void CheckAmount()
        {
            if (_coinGame.TotalWorth == 0)
                matchStatusLabel.Text = "";
            else if (_coinGame.TargetAmount == _coinGame.TotalWorth)
                matchStatusLabel.Text = "Matched!";
            else if (_coinGame.TotalWorth > _coinGame.TargetAmount)
                matchStatusLabel.Text = "Over!";
            else
                matchStatusLabel.Text = "Under...";
        }
    }
}
