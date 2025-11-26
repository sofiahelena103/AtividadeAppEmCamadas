using AppNomesBr.Domain.Interfaces.ExternalIntegrations.IBGE.Censos;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace AppNomesBr.Infrastructure.ExternalIntegrations.IBGE.Censos
{
    public class NomesApi : INomesApi
    {
        private readonly string? baseUrl = "api/v2/censos/nomes/";
        private readonly string rankingEndpoint = "ranking";
        private readonly HttpClient httpClient;

        public NomesApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.rankingEndpoint = baseUrl + this.rankingEndpoint;
        }

        public async Task<string> RetornaCensosNomesRanking()
        {
            var response = await httpClient.GetAsync(rankingEndpoint);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RetornaCensosNomesPeriodo(string nome)
        {
            var url = baseUrl + nome;
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
