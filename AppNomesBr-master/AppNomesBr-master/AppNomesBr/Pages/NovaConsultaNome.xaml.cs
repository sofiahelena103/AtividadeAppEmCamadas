using AppNomesBr.Domain.Interfaces.Repositories;
using AppNomesBr.Domain.Interfaces.Services;

namespace AppNomesBr.Pages;

public partial class NovaConsultaNome : ContentPage
{
    private readonly INomesBrService _service;
    private readonly INomesBrRepository _repository;

    public NovaConsultaNome(INomesBrService service, INomesBrRepository repository)
    {
        InitializeComponent();

        _service = service;
        _repository = repository;

        BtnPesquisar.Clicked += BtnPesquisar_Clicked;
        BtnDeleteAll.Clicked += BtnDeleteAll_Clicked;
    }

    protected override async void OnAppearing()
    {
        await CarregarNomes();
        base.OnAppearing();
    }

    private async void BtnPesquisar_Clicked(object? sender, EventArgs e)
    {
        var nome = TxtNome.Text?.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            await DisplayAlert("Atenção", "Digite um nome para pesquisar.", "PROSSEGUIR");
            return;
        }

        await _service.InserirNovoRegistroNoRanking(nome.ToUpper());
        await CarregarNomes();
    }

    private async void BtnDeleteAll_Clicked(object? sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Excluir tudo",
            "Tem certeza de que gostaria de apagar todo o ranking?",
            "Sim", "Não");

        if (!confirm)
            return;

        var registros = await _repository.GetAll();

        foreach (var registro in registros)
            await _repository.Delete(registro.Id);

        await CarregarNomes();
    }

    private async Task CarregarNomes()
    {
        var result = await _service.ListaMeuRanking();

        // Garante que não dê erro se estiver vazio
        GrdNomesBr.ItemsSource = result.FirstOrDefault()?.Resultado ?? new();
    }
}