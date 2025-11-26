using AppNomesBr.Domain.DataTransferObject.ExternalIntegrations.IBGE.Censos;

namespace AppNomesBr.Domain.Interfaces.Services
{
    public interface INomesBrService
    {
        Task<RankingNomesRoot[]> ListaTop20Nacional();
        Task<RankingNomesRoot[]> ListaMeuRanking();
        Task InserirNovoRegistroNoRanking(string nome);
    }
}
