using KtExtensions;
using System;
using System.Numerics;

namespace EMDD.KtNumerics;

/// <summary>
/// Complex Number
/// </summary>
public class KtComplex : Number
{
    /// <summary>
    /// Real Part
    /// </summary>
    public double Real { get; }
    /// <summary>
    /// Imaginary Part
    /// </summary>
    public double Imaginary { get; }
    /// <summary>
    /// Create an imaginary number
    /// </summary>
    public static KtComplex I => new(0, 1);
    /// <summary>
    /// Create a zero complex number
    /// </summary>
    public static KtComplex Zero => new(0, 0);
    /// <summary>
    /// Create Unity Complex number
    /// </summary>
    public static KtComplex One => new(1, 0);

    /// <summary>
    /// Real=0, imaginary=0
    /// </summary>
    public KtComplex()
    {
        Real = 0;
        Imaginary = 0;
    }

    /// <summary>
    /// create complex number
    /// </summary>
    /// <param name="realPart"></param>
    /// <param name="imaginaryPart"></param>
    public KtComplex(double realPart, double imaginaryPart)
    {
        Real = realPart;
        Imaginary = imaginaryPart;
    }

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override Number Add(Number other) => other switch
    {
        KtRealNumber r => CreateBaseFromTuple(r.Value + Real, Imaginary),
        KtComplex c => CreateBaseFromTuple(c.Real + Real, c.Imaginary + Imaginary),
        _ => other + this
    };

    /// <summary>
    /// change sign
    /// </summary>
    /// <returns></returns>
    public override Number Negate() => new KtComplex(-Real, -Imaginary);

    /// <summary>
    /// Multiply with
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override Number Multiply(Number other) => other switch
    {
        KtRealNumber real => CreateBaseFromTuple(Real * real.Value, Imaginary * real.Value),
        KtComplex(double real, double im) => CreateBaseFromTuple((Real * real) - (Imaginary * im), (Imaginary * real) + (Real * im)),
        _ => other * this
    };

    /// <summary>
    /// defaut equals
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override bool Equals(Number other) => other switch
    {
        KtComplex complex => Real.NearEqual(complex.Real, 9) && Imaginary.NearEqual(complex.Imaginary, 9),
        KtRealNumber real => Imaginary.NearZero(9) && Real.NearEqual(real, 9),
        _ => false
    };

    private double SumOfSquareOfCoeff()
    {
        return Real.RaiseTo(2) + Imaginary.RaiseTo(2);
    }

    /// <summary>
    /// sqrt of the square of the components
    /// </summary>
    /// <returns></returns>
    public double Modulus() => Math.Sqrt(SumOfSquareOfCoeff());

    /// <summary>
    /// inverse of a complex number
    /// </summary>
    /// <returns></returns>
    public override Number Inverse()
    {
        var (real, im) = Conjugate();
        var div = SumOfSquareOfCoeff();
        return new KtComplex(real / div, im / div);
    }

    public KtComplex Conjugate() => new(Real, -Imaginary);

    public void Deconstruct(out double real, out double imaginary)
    {
        real = Real;
        imaginary = Imaginary;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public double Argument() => NumericExtensions.Atan3(Imaginary, Real);

    public override string ToString() => ToString("#0.###");

    public override string ToString(string v)
    {
        if (this == Zero) return "0";
        if (double.IsInfinity(Real) || double.IsInfinity(Imaginary)) return "∞";
        if (double.IsNaN(Real) || double.IsNaN(Imaginary)) return "?";
        var re = Real.NearZero() ? "" : Real.ToString(v);
        var sign = Imaginary < 0 ? "-" : (Imaginary > 0 && !Real.NearZero() ? "+" : "");
        var im = Imaginary.NearZero() ? "" :
            Imaginary.NearEqual(1) || Imaginary.NearEqual(-1) ? "i" :
            Math.Abs(Imaginary).ToString(v) + "i";
        return re + sign + im;
    }

    public override int GetHashCode() => Real.GetHashCode() ^ (Imaginary.GetHashCode() * 31);

    public bool IsReal() => Imaginary.NearZero();

    public bool IsImaginary() => Real.NearZero();

    /// <summary>
    /// convert complex value to tuple
    /// </summary>
    /// <param name="val"></param>
    public static implicit operator (double, double)(KtComplex val) => val is null ? (0, 0) : (val.Real, val.Imaginary);

    /// <summary>
    /// Create a rectangular complex number from polar
    /// </summary>
    /// <param name="modulus"> radius or modulus </param>
    /// <param name="argumentInDegrees"> angle in degrees</param>
    /// <returns></returns>
    public static Number CreateComplexNumberFromPolar(double modulus, double argumentInDegrees)
    {
        var angleInRads = argumentInDegrees * Math.PI / 180;
        var real = modulus * Math.Cos(angleInRads);
        var im = modulus * Math.Sin(angleInRads);
        return CreateBaseFromTuple(real, im);
    }

    public (double mod, double arg) ToPolar() => (Modulus(), Argument());

    public override Number Clone() => new KtComplex(Real, Imaginary);

    public override Number Round(int accuracy = 12) => CreateBaseFromTuple(Math.Round(Real, accuracy), Math.Round(Imaginary, accuracy));
    public override double Magnitude() => Modulus();

    public override (double, double) ComparableValue(Number other) => (Magnitude(), other.Magnitude());

    public override Number Abs() => new KtComplex(Math.Abs(Real), Math.Abs(Imaginary));

    public override Number Sqrt() => CreateComplexNumberFromPolar(Math.Sqrt(Modulus()), Argument() / 2);

    public override Number Log() => new KtComplex(Math.Log(Magnitude()), Argument() * Math.PI / 180);

    public override Number Exp() => new KtComplex(Math.Exp(Real) * Math.Cos(Imaginary), Math.Exp(Real) * Math.Sin(Imaginary));

    public override bool NearZero(int accuracy = 12) => Real.NearZero(accuracy) && Imaginary.NearZero(accuracy);

    public override Complex ToComplex() => new(Real, Imaginary);

    internal override KtComplex ToKtComplex() => new(Real, Imaginary);

    public override Number RaiseTo(double b) => CreateComplexNumberFromPolar(Modulus().RaiseTo(b), Argument() * b);

    public override Number SmartRound() => CreateBaseFromTuple(Real.SmartRoundActual(), Imaginary.SmartRoundActual());

    public override int CompareTo(Number other)
    {
        //todo:make a better logic for this
        if (other is KtComplex complex)
        {
            var compareReal = Real.CompareTo(complex.Real);
            var compareIm = Imaginary.CompareTo(complex.Imaginary);
            return compareReal == 0 ? compareIm : compareReal;
        }
        var comparable = ComparableValue(other);
        return comparable.Item1.CompareTo(comparable.Item2);
    }

    public override double ToDouble() => Real;
}