// SOFIA HELENA PEREIRA
using AppNomesBr.Domain.DataTransferObject.ExternalIntegrations.IBGE.Censos;
using AppNomesBr.Domain.Interfaces.Services;
using System.Text.Json;

namespace AppNomesBr.Pages;

public partial class RankingNomesBrasileiros : ContentPage
{
    private readonly INomesBrService service;

    public RankingNomesBrasileiros(INomesBrService service)
    {
        InitializeComponent();
        this.service = service;
    }

    protected override async void OnAppearing()
    {
        await CarregarNomes();
        base.OnAppearing();
    }

    private async Task CarregarNomes()
    {
        // ALTERAÇÃO: lê filtros inseridos pelo usuário
        var cidade = TxtCidade?.Text;  
        var sexo = PickerSexo?.SelectedItem?.ToString();

        // ALTERAÇÃO: chamada ao serviço agora envia cidade e sexo
        var result = await service.ListaTop20Nacional(cidade, sexo);

        this.GrdNomesBr.ItemsSource = result.FirstOrDefault()?.Resultado;
    }

    // ALTERAÇÃO: botão Atualizar agora chama CarregarNomes()
    private async void BtnAtualizar_Clicked(object sender, EventArgs e)
    {
        await CarregarNomes();
    }
}