using KtExtensions;
using System;

namespace EMDD.KtNumerics
{
    public static class NumberHelper
    {
        public static double GetRealNumber(Number number)
        {
            return number.ToComplex().Real;
        }

        public static (Number min, Number max) MinMax(this (Number a, Number b) numbers) => (numbers.a < numbers.b) ? (numbers.a, numbers.b) : (numbers.b, numbers.a);

        public static Number Sqrt(Number number)
        {
            return number.Sqrt();
        }

        /// <summary>
        /// Greatest Common Dinaminator for two numbers
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static long Gcd(long left, long right)
        {
            if (left < 0) left = -left;
            if (right < 0) right = -right;
            if (left < 2 || right < 2) return 1;
            do
            {
                if (left < right)
                {
                    var temp = left;
                    left = right;
                    right = temp;
                }
                left %= right;
            } while (left != 0);
            return right;
        }

        public static bool NumberIsWhole(Number val) => val is KtRealNumber real && (real.Value - Math.Truncate(real.Value)).NearZero();

        public static Number Gcd1(Number left, Number right)
        {
            return (left, right) switch
            {
                (KtRealNumber realLeft, KtRealNumber realRight) when NumberIsWhole(realLeft) && NumberIsWhole(realRight) => Gcd((long)realLeft.Value, (long)realRight.Value),
                _ => right
            };
        }

        public static Number Gcd(Number left, Number right) =>
            left is KtRealNumber realLeft && NumberIsWhole(realLeft) && right is KtRealNumber realRight && NumberIsWhole(realRight) ? Gcd((long)realLeft.Value, (long)realRight.Value) : right;
    }
}