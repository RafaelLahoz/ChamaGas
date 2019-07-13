using AppChamaGas.Interface;
using AppChamaGas.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace AppChamaGas.Services.Azure
{
    public abstract class AzureService<T> where T : IAzureTabela
    {
        MobileServiceClient clienteAzure;
        IMobileServiceTable<T> tabelaAzure;

        public AzureService()
        {
            //Configura o serviço de conexão com Azure
            clienteAzure = new MobileServiceClient(App.uriAzure);
            tabelaAzure = clienteAzure.GetTable<T>();
        }

        /// <summary>
        /// Metodo de inclusão de registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para a inclusão</returns>
        public async Task<bool> IncluirRegistro(T registro)
        {
            try
            {
                await tabelaAzure.InsertAsync(registro);

                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro.Message}");
                return false;
            }

        }
        /// <summary>
        /// Metodo de alteração de registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para a alteração</returns>
        public async Task<bool> AlterarRegistro(T registro)
        {
            try
            {
                await tabelaAzure.UpdateAsync(registro);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }

        }
        /// <summary>
        /// Metodo de exclusão de registro no banco de dados Azure
        /// </summary>
        /// <returns>Retorna verdadeiro ou falso para a exclusão</returns>
        public async Task<bool> ExcluirRegistro(T registro)
        {
            try
            {
                await tabelaAzure.DeleteAsync(registro);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }

        }

        /// <summary>
        /// Metodo que lista todos os registros no banco de dados Azure
        /// </summary>
        /// <param name="registroId">Id do registro</param>
        /// <returns>Retorna todos os registros ou nulo</returns>
        public async Task<IEnumerable<T>> ListarAsync()
        {
            //lista todos os registros da tabela
            return await tabelaAzure.ToListAsync();
        }

        /// <summary>
        /// Metodo que lista todos os registros no banco de dados Azure
        /// </summary>
        /// <param name="registroId">Id do registro</param>
        /// <returns>Retorna o registro ou nulo</returns>
        public async Task<T> ObterAsync(string registroId)
        {
            var tabela = await tabelaAzure.ToListAsync();
            var registro = tabela.FirstOrDefault(r => r.Id == registroId);
            return registro;
        }
    }
}
