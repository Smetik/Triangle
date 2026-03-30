using System.Windows.Input;
using TriangleProduct.Services;

namespace TriangleProduct.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    private readonly TriangleService _triangleService = new();

    private string _sideA = string.Empty;
    private string _sideB = string.Empty;
    private string _sideC = string.Empty;
    private string _resultTitle = "Результат проверки";
    private string _resultMessage = "Введите длины сторон и нажмите кнопку проверки.";
    private bool _isSuccess;

    public MainWindowViewModel()
    {
        CheckTriangleCommand = new DelegateCommand(CheckTriangle);
        ClearCommand = new DelegateCommand(Clear);
    }

    public string SideA
    {
        get => _sideA;
        set => SetProperty(ref _sideA, value);
    }

    public string SideB
    {
        get => _sideB;
        set => SetProperty(ref _sideB, value);
    }

    public string SideC
    {
        get => _sideC;
        set => SetProperty(ref _sideC, value);
    }

    public string ResultTitle
    {
        get => _resultTitle;
        private set => SetProperty(ref _resultTitle, value);
    }

    public string ResultMessage
    {
        get => _resultMessage;
        private set => SetProperty(ref _resultMessage, value);
    }

    public bool IsSuccess
    {
        get => _isSuccess;
        private set => SetProperty(ref _isSuccess, value);
    }

    public string ResultBadge => IsSuccess ? "Корректный ввод" : "Нужна проверка";

    public ICommand CheckTriangleCommand { get; }

    public ICommand ClearCommand { get; }

    private void CheckTriangle()
    {
        var result = _triangleService.Analyze(SideA, SideB, SideC);
        ResultTitle = result.Title;
        ResultMessage = result.Message;
        IsSuccess = result.IsValid;
        RaisePropertyChanged(nameof(ResultBadge));
    }

    private void Clear()
    {
        SideA = string.Empty;
        SideB = string.Empty;
        SideC = string.Empty;
        ResultTitle = "Результат проверки";
        ResultMessage = "Введите длины сторон и нажмите кнопку проверки.";
        IsSuccess = false;
        RaisePropertyChanged(nameof(ResultBadge));
    }
}
