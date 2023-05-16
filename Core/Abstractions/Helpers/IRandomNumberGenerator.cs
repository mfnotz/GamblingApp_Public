using System;

namespace Core.Abstractions.Helpers
{
    public interface IRandomNumberGenerator
    {
        int Generate(int minValue, int maxValue);
    }

}
