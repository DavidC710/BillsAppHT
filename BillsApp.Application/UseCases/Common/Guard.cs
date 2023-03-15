public static class Guard
{
    public static T NotNull<T>([NotNull] T? t, [CallerArgumentExpression("t")] string? paramName = default)
    {
        if (t is null)
        {
            throw new ArgumentNullException(paramName);
        }

        return t;
    }

    public static string NotNullOrEmpty([NotNull] string? str,
        [CallerArgumentExpression("str")]
            string? paramName = null)
    {
        NotNull(str, paramName);
        if (str.Length == 0)
        {
            throw new ArgumentException("The argument can not be Empty", paramName);
        }
        return str;
    }

    public static string NotNullOrWhiteSpace([NotNull] string? str,
        [CallerArgumentExpression("str")] string? paramName = null)
    {
        NotNull(str, paramName);
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentException("The argument can not be WhiteSpace", paramName);
        }
        return str;
    }

    public static ICollection<T> NotEmpty<T>([NotNull] ICollection<T> collection, [CallerArgumentExpression("collection")] string? paramName = null)
    {
        NotNull(collection, paramName);
        if (collection.Count == 0)
        {
            throw new ArgumentException("The collection could not be empty", paramName);
        }
        return collection;
    }

    public static T Ensure<T>(Func<T, bool> condition, T t, [CallerArgumentExpression("t")] string? paramName = null)
    {
        NotNull(condition);
        if (!condition(t))
        {
            throw new ArgumentException("The argument does not meet condition", paramName);
        }
        return t;
    }

    public static async Task<T> EnsureAsync<T>(Func<T, Task<bool>> condition, T t, [CallerArgumentExpression("t")] string? paramName = null)
    {
        NotNull(condition);
        if (!await condition(t))
        {
            throw new ArgumentException("The argument does not meet condition", paramName);
        }
        return t;
    }

}
