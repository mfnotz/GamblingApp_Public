using Core.Abstractions.Helpers;

namespace Services.Helpers
{
    public class ControlledRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly int _value;

        public ControlledRandomNumberGenerator(int value)
        {
            _value = value;
        }

        public int Generate(int minValue, int maxValue)
        {
            return _value;
        }
    }
}
