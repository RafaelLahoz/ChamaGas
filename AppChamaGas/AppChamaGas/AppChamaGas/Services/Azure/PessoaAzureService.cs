using AppChamaGas.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Services.Azure
{
    public class PessoaAzureService
    {
        MobileServiceClient clienteAzure;
        IMobileServiceTable<Pessoa> tabelaPessoaAzure;

        public PessoaAzureService()
        {
            //Configura o serviço de conexão com Azure
            clienteAzure = new MobileServiceClient(App.uriAzure);
            tabelaPessoaAzure = clienteAzure.GetTable<Pessoa>();
        }

        public async Task<bool> IncluirPessoa(Pessoa pessoa)
        {
            try
            {
                await tabelaPessoaAzure.InsertAsync(pessoa);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }

        }
        public async Task<bool> AlterarPessoa(Pessoa pessoa)
        {
            try
            {
                await tabelaPessoaAzure.UpdateAsync(pessoa);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }

        }
        public async Task<bool> ExcluirPessoa(Pessoa pessoa)
        {
            try
            {
                await tabelaPessoaAzure.DeleteAsync(pessoa);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure:{erro}");
                return false;
            }

        }
    }
}
