using AppChamaGas.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AppChamaGas.Services.Azure
{
    public class PessoaAzureService : AzureService<Pessoa>
    {
        //Regras de negocio no banco de dados
        //Autenticacao Usuario

        public async Task<Pessoa> AutenticaUsuarioAsync(string email, string senha)
        {
            try
            {
                //Lista a tabela pessoas
                var pessoas = await this.ListarAsync();
                //Faz a consulta especifica do usuarion na tabela.
                return pessoas.FirstOrDefault(p => p.Email == email && p.Senha == senha);
            }
            catch (Exception erro)
            {
                Debug.WriteLine(erro);
                return null;
            }
        }






        public async Task<IEnumerable<Pessoa>> List(string busca)
        {
            List<Pessoa> listaRetorno = new List<Pessoa>();
            listaRetorno.Add(new Pessoa
            {
                RazaoSocial = "Senac",
                Longitude = -49.3758587,
                Latitude = -20.8141467,
            });
            listaRetorno.Add(new Pessoa
            {
                RazaoSocial = "Terminal",
                Longitude = -49.3766612,
                Latitude = -20.8096339,
            });
            listaRetorno.Add(new Pessoa
            {
                RazaoSocial = "Mc Donalds",
                Longitude = -49.3865877,
                Latitude = -20.822495,
            });
            listaRetorno.Add(new Pessoa
            {
                RazaoSocial = "Habbib's",
                Longitude = -49.381770,
                Latitude = -20.817285,
            });

            return listaRetorno;
        }
    }
    //MobileServiceClient clienteAzure;
    //IMobileServiceTable<Pessoa> tabelaPessoaAzure;

    //public PessoaAzureService()
    //{
    //    //Configura o serviço de conexão com Azure
    //    clienteAzure = new MobileServiceClient(App.uriAzure);
    //    tabelaPessoaAzure = clienteAzure.GetTable<Pessoa>();
    //}

    //public async Task<bool> IncluirPessoa(Pessoa pessoa)
    //{
    //    try
    //    {
    //        await tabelaPessoaAzure.InsertAsync(pessoa);
    //        return true;
    //    }
    //    catch (Exception erro)
    //    {
    //        Debug.WriteLine($"Erro Azure:{erro}");
    //        return false;
    //    }

    //}
    //public async Task<bool> AlterarPessoa(Pessoa pessoa)
    //{
    //    try
    //    {
    //        await tabelaPessoaAzure.UpdateAsync(pessoa);
    //        return true;
    //    }
    //    catch (Exception erro)
    //    {
    //        Debug.WriteLine($"Erro Azure:{erro}");
    //        return false;
    //    }

    //}
    //public async Task<bool> ExcluirPessoa(Pessoa pessoa)
    //{
    //    try
    //    {
    //        await tabelaPessoaAzure.DeleteAsync(pessoa);
    //        return true;
    //    }
    //    catch (Exception erro)
    //    {
    //        Debug.WriteLine($"Erro Azure:{erro}");
    //        return false;
    //    }

    //}
}
