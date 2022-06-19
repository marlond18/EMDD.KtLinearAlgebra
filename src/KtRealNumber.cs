using KtExtensions;
using System;
using System.Numerics;

namespace EMDD.KtNumerics;
/// <summary>
/// Real Number
/// </summary>
public class KtRealNumber : Number
{
    internal double Value { get; }

    public KtRealNumber(double value)
    {
        Value = value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override Number Add(Number other) => other switch
    {
        KtRealNumber real => new KtRealNumber(Value + real.Value),
        _ => other + this
    };

    public override Number Multiply(Number other) => other switch
    {
        KtRealNumber real => new KtRealNumber(Value * real.Value),
        _ => other * this
    };

    public override Number Negate() => new KtRealNumber(-Value);

    public override bool Equals(Number other) => other switch
    {
        KtRealNumber real => (Value - real.Value).NearZero(9),
        KtComplex complex => complex.Imaginary.NearZero(9) && complex.Real.NearEqual(Value, 9),
        _ => false
    };

    public override Number Inverse() => new KtRealNumber(1 / Value);

    public static implicit operator double(KtRealNumber val) => val switch
    {
        null => 0,
        _ => val.Value
    };

    public static implicit operator KtRealNumber(double val) => new(val);

    public static implicit operator KtRealNumber(long val) => new(val);

    public override Number Clone() => Value;

    public override Number Round(int accuracy = 12) => new KtRealNumber(Math.Round(Value, accuracy));

    public override double Magnitude() => Math.Abs(Value);

    public override string ToString() => Value.SmartToString();

    public override (double, double) ComparableValue(Number other) => other switch
    {
        KtRealNumber real => (Value, real.Value),
        _ => (Magnitude(), other.Magnitude())
    };

    public override Number Abs() => new KtRealNumber(Math.Abs(Value));

    public override string ToString(string v) => Value.ToString(v);

    public override Number Sqrt()
    {
        if (Value < 0) return new KtComplex(0, Math.Sqrt(Math.Abs(Value)));
        return new KtRealNumber(Math.Sqrt(Value));
    }

    public override Number Log() => new KtRealNumber(Math.Log(Value));

    public override Number Exp() => new KtRealNumber(Math.Exp(Value));

    public override bool NearZero(int accuracy = 12) => Value.NearZero(accuracy);

    public override Complex ToComplex() => new(Value, 0);

    internal override KtComplex ToKtComplex() => new(Value, 0);

    public override Number RaiseTo(double b) => new KtRealNumber(Math.Pow(Value, b));

    public override Number SmartRound() => new KtRealNumber(Value.SmartRoundActual());

    public override int CompareTo(Number other) => other switch
    {
        KtRealNumber real => Value.CompareTo(real.Value),
        _ => ComparableValue(other).CompareTo()
    };

    public override double ToDouble() => Value;
}