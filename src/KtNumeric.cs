using KtExtensions;
using System;
using System.Numerics;

namespace EMDD.KtNumerics
{
    public abstract class Number : IEquatable<Number>, IComparable<Number>
    {
        public static implicit operator Number(double val) => new KtRealNumber(val);

        public static implicit operator string(Number val) => val.ToString();

        public abstract double ToDouble();

        public static implicit operator Number(long val) => new KtRealNumber(val);

        public static implicit operator Number(int val) => new KtRealNumber(val);

        public static implicit operator Number((double, double) val) => CreateBaseFromTuple(val.Item1, val.Item2);

        public static implicit operator Number((long, double) val) => CreateBaseFromTuple(val.Item1, val.Item2);

        public static implicit operator Number((long, long) val) => CreateBaseFromTuple(val.Item1, val.Item2);

        public static implicit operator Number((double, long) val) => CreateBaseFromTuple(val.Item1, val.Item2);

        public static implicit operator Number((int, double) val) => CreateBaseFromTuple(val.Item1, val.Item2);

        public static implicit operator Number((int, int) val) => CreateBaseFromTuple(val.Item1, val.Item2);

        public static implicit operator Number((double, int) val) => CreateBaseFromTuple(val.Item1, val.Item2);

        public static explicit operator Complex(Number v) => v.ToComplex();

        public static explicit operator Number(Complex v) => CreateBaseFromTuple(v.Real, v.Imaginary);

        public static Number operator -(Number a) => a?.Negate();

        public static Number operator -(Number a, Number b) => a + -b;

        public static bool operator !=(Number a, Number b) => !(a == b);

        public static Number operator *(Number a, Number b) => a is null || b is null ? null : a.Multiply(b);

        public static Number operator /(Number a, Number b) => a * b.Inverse();

        public static Number operator +(Number a, Number b)
        {
            if (a is null && b is null) return null;
            if (b is null) return a;
            if (a is null) return b;
            return a.Add(b);
        }

        public static bool operator <(Number a, Number b)
        {
            if (a is null && b is null) return false;
            if (a is null) return true;
            if (b is null) return false;
            var comparable = a.ComparableValue(b);
            return comparable.Item1 < comparable.Item2;
        }

        public bool IsWithin(Number a, Number b) => IsWithin((a, b));

        public bool IsWithin(( Number a, Number b) limits)
        {
            var (min, max) = (limits).MinMax();
            return this >= min && this <= max;
        }

        public static bool operator <=(Number a, Number b) => a == b || a < b;

        public static bool operator ==(Number a, Number b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator >(Number a, Number b)
        {
            if (a == null && b == null) return false;
            if (a == null) return false;
            if (b == null) return true;
            var comparable = a.ComparableValue(b);
            return comparable.Item1 > comparable.Item2;
        }

        public static bool operator >=(Number a, Number b) => a == b || a > b;

        public static Number Max(Number a, Number b) => a > b ? a : b;

        public static Number Min(Number a, Number b) => a < b ? a : b;

        public abstract Number Abs();

        public abstract Number Add(Number other);

        public abstract Number Clone();

        public abstract (double, double) ComparableValue(Number other);

        public abstract int CompareTo(Number other);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (this is null || obj is null) return false;
            return obj is Number num && Equals(num);
        }

        public abstract bool Equals(Number other);

        public abstract Number Exp();

        public abstract override int GetHashCode();

        public abstract Number Inverse();

        public abstract Number Log();

        public abstract double Magnitude();

        public abstract Number Multiply(Number other);

        public abstract bool NearZero(int accuracy = 12);

        public abstract Number Negate();

        public abstract Number RaiseTo(double b);

        public abstract Number Round(int accuracy = 12);

        public abstract Number SmartRound();

        public abstract Number Sqrt();

        public abstract Complex ToComplex();

        public abstract override string ToString();

        public abstract string ToString(string v);

        internal static Number CreateBaseFromTuple(double val1, double val2)
        {
            if (val2.NearZero()) return new KtRealNumber(val1);
            return new KtComplex(val1, val2);
        }

        internal abstract KtComplex ToKtComplex();
    }
}