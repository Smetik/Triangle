namespace TriangleProduct.Models;

public sealed class TriangleAnalysisResult
{
    public bool IsValid { get; init; }
    public TriangleType TriangleType { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;

    public static TriangleAnalysisResult Error(string message) => new()
    {
        IsValid = false,
        TriangleType = TriangleType.Invalid,
        Title = "Ошибка проверки",
        Message = message
    };

    public static TriangleAnalysisResult Success(TriangleType triangleType, string message) => new()
    {
        IsValid = true,
        TriangleType = triangleType,
        Title = "Результат",
        Message = message
    };
}
