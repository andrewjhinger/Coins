using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Coins
{
    public class CoinGame
    {
        // Private class variables
        private Counter<Money> _myCounter = new Counter<Money>();
        private Counter<Penny> _myPennies = new Counter<Penny>();
        private Counter<ThreeCent> _myThreeCent = new Counter<ThreeCent>();
        private Counter<Nickel> _myNickels = new Counter<Nickel>();
        private Counter<Dime> _myDimes = new Counter<Dime>();
        private Counter<Quarter> _myQuarters = new Counter<Quarter>();

        private int _coinHeight = 20;
        private Random _random = new Random();

        // Private backing class variables
        private decimal _targetAmount = 0.00m;

        // Accessors
        public decimal TargetAmount
        {
            set { _targetAmount = value; }
            get { return _targetAmount; }
        }
        public decimal TotalWorth { get { return _myCounter.TotalWorth; } }
        public int CountPenny { get { return _myPennies.Count; } }
        public int CountThreeCent { get { return _myThreeCent.Count; } }
        public int CountNickel { get { return _myNickels.Count; } }
        public int CountDime { get { return _myDimes.Count; } }
        public int CountQuarter { get { return _myQuarters.Count; } }

        // Constructors
        public CoinGame()
        {
            SetTargetAmount();
        }

        //  Public methods
        public void Add(Money money)
        {
            _myCounter.Add(money);
            switch (money.GetType().Name)
            {
                case "Penny":
                    _myPennies.Add(money as Penny);
                    break;
                case "ThreeCent":
                    _myThreeCent.Add(money as ThreeCent);
                    break;
                case "Nickel":
                    _myNickels.Add(money as Nickel);
                    break;
                case "Dime":
                    _myDimes.Add(money as Dime);
                    break;
                case "Quarter":
                    _myQuarters.Add(money as Quarter);
                    break;
            }
        }

        public void Update(Graphics graphics, Size boardSize)
        {
            // Note using for loop to allow updating money
            for (int i = 0; i < _myCounter.Count; i++)
            {
                Money money = _myCounter[i];
                // Set location
                int startingY = boardSize.Height - (i * (_coinHeight + 1)) - _coinHeight - 1;
                money.Location = new Point(1, startingY);
                money.Dimensions = new Size(boardSize.Width - 2, _coinHeight);
                // Draw coin
                using (SolidBrush solidBrush = new SolidBrush(money.Color))
                    graphics.FillRectangle(solidBrush, new Rectangle(money.Location, money.Dimensions));
                // Draw text
                RectangleF boundingRectangle = new RectangleF(money.Location.X, money.Location.Y, money.Dimensions.Width, money.Dimensions.Height);
                using (Font font = new Font("Arial", 12, FontStyle.Bold))
                using (StringFormat stringFormat = new StringFormat())
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    // Align text horizontally and vertically
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    graphics.DrawString(money.Text, font, brush, boundingRectangle, stringFormat);
                }
            }
        }

        public void Remove(Point location)
        {
            // Loop through each coin to see which was selected
            foreach (Money money in _myCounter)
            {
                if (money.Hit(location))
                {
                    switch (money.GetType().Name)
                    {
                        case "Penny":
                            _myPennies.Remove(money as Penny);
                            break;
                        case "ThreeCent":
                            _myThreeCent.Remove(money as ThreeCent);
                            break;
                        case "Nickel":
                            _myNickels.Remove(money as Nickel);
                            break;
                        case "Dime":
                            _myDimes.Remove(money as Dime);
                            break;
                        case "Quarter":
                            _myQuarters.Remove(money as Quarter);
                            break;
                    }
                    _myCounter.Remove(money);
                    break;
                }
            }
        }

        public void Reset()
        {
            _myCounter.Reset();
            _myPennies.Reset();
            _myThreeCent.Reset();
            _myNickels.Reset();
            _myDimes.Reset();
            _myQuarters.Reset();
            SetTargetAmount();
        }

        // Private methods
        private void SetTargetAmount()
        {
            _targetAmount = _random.Next(1, 100) / 100.00m;
        }
    }
}