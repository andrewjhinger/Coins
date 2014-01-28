using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coins
{
    public class Counter<T> where T : Money
    {
        // Private class variables
        private List<T> _counterList;

        // Private backing class variables
        private decimal _totalWorth = 0.00m;

        // Accessors
        public decimal TotalWorth { get { return _totalWorth; } }
        public int Count { get { return _counterList.Count; } }

        // Indexers
        public T this[int index]
        {
            get
            {
                if (_counterList.Count > 0 && index < _counterList.Count)
                    return _counterList[index];
                else
                    return default(T);
            }
        }

        // Constructors
        public Counter()
        {
            _counterList = new List<T>();
        }

        // Public methods
        public void Add(T t)
        {
            _counterList.Add(t);
            _totalWorth += t.Worth;
        }

        // Public enumerator
        public IEnumerator<T> GetEnumerator()
        {
            return _counterList.GetEnumerator();
        }

        public void Reset()
        {
            _counterList.Clear();
            _totalWorth = 0.00m;
        }

        public void Remove(T t)
        {
            T foundT = _counterList.Find(item => item == t);
            if (foundT != null)
            {
                _totalWorth -= foundT.Worth;
                _counterList.Remove(foundT);
            }
        }
    }
}