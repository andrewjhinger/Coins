using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Coins
{
    public class Money
    {
        // Private backing class variables
        private decimal _worth;
        private Point _location;
        private Size _dimensions;
        private string _text;
        private Color _color;

        // Accessors
        public decimal Worth { get { return _worth; } }
        public Point Location { get { return _location; } set { _location = value; } }
        public Size Dimensions { get { return _dimensions; } set { _dimensions = value; } }
        public string Text { get { return _text; } set { _text = value; } }
        public Color Color { get { return _color; } set { _color = value; } }

        // Constructors
        public Money(decimal worth, string text, Color color)
        {
            _worth = worth;
            _text = text;
            _color = color;
        }

        // Public methods
        public bool Hit(Point location)
        {
            return new Rectangle(_location, _dimensions).Contains(location);
        }
    }

    public class Penny : Money
    {
        public Penny() : base(0.01m, "Penny", Color.Brown) { }
    }

    public class ThreeCent : Money
    {
        public ThreeCent() : base(0.03m, "ThreeCent", Color.Black) { }
    }

    public class Nickel : Money
    {
        public Nickel() : base(0.05m, "Nickel", Color.Silver) { }
    }

    public class Dime : Money
    {
        public Dime() : base(0.10m, "Dime", Color.SlateGray) { }
    }

    public class Quarter : Money
    {
        public Quarter() : base(0.25m, "Quarter", Color.Gold) { }
    }

}
