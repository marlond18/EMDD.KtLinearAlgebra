&nbsp; [![Nuget](https://img.shields.io/nuget/v/EMDD.KtNumerics)](https://www.nuget.org/packages/EMDD.KtNumerics/)[![Nuget](https://img.shields.io/nuget/dt/EMDD.KtNumerics)](https://www.nuget.org/stats/packages/EMDD.KtNumerics?groupby=Version&groupby=ClientName&groupby=ClientVersion)[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/marlond18/EMDD.KtNumerics/Run%20Tests)](https://github.com/marlond18/EMDD.KtNumerics/actions/workflows/runTest.yml)
&nbsp; 
----------------

# EMDD.KtNumerics
a library to represent real and complex numbers. This is being used by other EMDD librabies such as [```EMDD.KtMatrix```](https://github.com/marlond18/EMDD.KtMatrix)

## Requirements

[Visual Studio 16.8](https://visualstudio.microsoft.com/vs) or greater

[.Net 5.0.102 sdk](https://dotnet.microsoft.com/download/dotnet/5.0) or greater

## How it works
- Numbers can either be represented by ```KtRealNumber``` and ```KtComplex``` both inheriting from an abstract base class ```EMDD.KtNumerics.Number```. This gives us the ability to use this number types when doing much more advanced Math Operations such as Matrix operations on not just real numbers, but also Complex numbers.


## Number types
### KtRealNumbers
- it has an internal get only property of type `double` which holds the actual numeric value.

### KtComplex
- two values, real and imaginary coeff, are stored internally.

### Methods
- the base class ```EMDD.KtNumerics.Number``` can be used for basic arithmetic ops such as adding, subtracting, multiplying and dividing (with the corresponding static operators).