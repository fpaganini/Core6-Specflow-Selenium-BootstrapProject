namespace SpecflowProject.Extensions;

public static class StringExtension
{
    public static bool IsNullOrEmpty(this string? original)
        => String.IsNullOrEmpty(original);

    public static bool IsNullOrWhiteSpace(this string? original)
        => String.IsNullOrWhiteSpace(original);

    public static bool IsNullOrEmptyOrEquals(this string? original, string? other)
        => String.IsNullOrEmpty(original) || original!.Equals(other);

    public static bool IsNullOrWhiteSpaceOrEquals(this string? original, string? other)
        => String.IsNullOrWhiteSpace(original) || original!.Equals(other);

    public static bool IsNullOrEmptyOrEquals(this string? original, string? other, StringComparison stringComparison)
        => String.IsNullOrEmpty(original) || original!.Equals(other, stringComparison);

    public static bool IsNullOrWhiteSpaceOrEquals(this string? original, string? other, StringComparison stringComparison)
        => String.IsNullOrWhiteSpace(original) || original!.Equals(other, stringComparison);
    public static bool IsEmpty(this string? original)
       => original != null && original == String.Empty;
}
