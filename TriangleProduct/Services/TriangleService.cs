using TriangleProduct.Models;

namespace TriangleProduct.Services;

public sealed class TriangleService
{
    public TriangleAnalysisResult Analyze(string? sideA, string? sideB, string? sideC)
    {
        if (string.IsNullOrWhiteSpace(sideA) || string.IsNullOrWhiteSpace(sideB) || string.IsNullOrWhiteSpace(sideC))
        {
            return TriangleAnalysisResult.Error("Заполните все три поля.");
        }

        if (!int.TryParse(sideA.Trim(), out int a) ||
            !int.TryParse(sideB.Trim(), out int b) ||
            !int.TryParse(sideC.Trim(), out int c))
        {
            return TriangleAnalysisResult.Error("Стороны должны быть целыми числами без букв и посторонних символов.");
        }

        return Analyze(a, b, c);
    }

    public TriangleAnalysisResult Analyze(int a, int b, int c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
        {
            return TriangleAnalysisResult.Error("Длины сторон должны быть больше нуля.");
        }

        if (a + b <= c || a + c <= b || b + c <= a)
        {
            return TriangleAnalysisResult.Error("Треугольник с такими сторонами не существует: нарушено правило суммы двух сторон.");
        }

        TriangleType type = Classify(a, b, c);

        string message = type switch
        {
            TriangleType.Equilateral => "Треугольник равносторонний: все три стороны равны.",
            TriangleType.Isosceles => "Треугольник равнобедренный: две стороны равны.",
            TriangleType.Scalene => "Треугольник разносторонний: все стороны различаются.",
            _ => "Не удалось определить тип треугольника."
        };

        return TriangleAnalysisResult.Success(type, message);
    }

    public TriangleType Classify(int a, int b, int c)
    {
        if (a == b && b == c)
        {
            return TriangleType.Equilateral;
        }

        if (a == b || a == c || b == c)
        {
            return TriangleType.Isosceles;
        }

        return TriangleType.Scalene;
    }
}
