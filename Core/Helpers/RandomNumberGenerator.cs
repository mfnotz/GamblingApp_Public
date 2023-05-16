using Core.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random;

        public RandomNumberGenerator()
        {
            _random = new Random();
        }

        public int Generate(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }

}
