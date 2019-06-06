using AppChamaGas.Model;
using AppChamaGas.Services;
using AppChamaGas.Services.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppChamaGas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PessoaView : ContentPage
	{
        //Chama o Servicos do Azure
        PessoaAzureService pessoaAzureService;
        Pessoa pessoa;
    

        //Forma de chamar essa tarefa para todas as tabelas.
        //AzureService<Pessoa> pessoaServico;
        Base_Service client_cep = new Base_Service(Base_Service.URL_VIACEP);
        //Base_Service client_api = new Base_Service(Base_Service.URL_MINHAAPI);
        ReqRes_Service client_ReqRes_user = new ReqRes_Service("users");
        ReqRes_Service client_ReqRes_register = new ReqRes_Service("register");
        User_ReqRes md = new User_ReqRes();

        public PessoaView ()
		{
			InitializeComponent ();
            //Instanciando o serviço criado
            pessoaAzureService = new PessoaAzureService();
            pessoa = new Pessoa();
            ListarTipo();
        }

        private async void EtCEP_Unfocused(object sender, FocusEventArgs e)
        {
            var cep_ret = await client_cep.Get<Cep>(etCEP.Text);

            this.etCEP.Text = cep_ret.CEP;
            this.etLogradouro.Text = cep_ret.Logradouro;
            this.etComplemento.Text = cep_ret.Complemento;
            this.etBairro.Text = cep_ret.Bairro;
            this.etLocalidade.Text = cep_ret.Localidade;
            this.etUF.Text = cep_ret.Uf;

            //var pessoa_ret = await client_api.get<Pessoa>("");
            //this.etPessoaNome.Text = pessoa_ret.Nome;
            //var obj_reqres_user = new ReqRes_Service("users");

            await client_ReqRes_user.Get<RetornoTeste>("2");

            md.name = cep_ret.Bairro;
            md.job = cep_ret.Localidade;

            var retorno = await client_ReqRes_user.Post<User_ReqRes>(md);
            var atualiza = await client_ReqRes_user.Put<User_ReqRes>(md);

            await this.DisplayAlert("Meu retorno", $"{retorno.id},{retorno.createdAt},{retorno.name},{retorno.job}", "Entendi");

            await this.DisplayAlert("Atualizacao", $"{atualiza.updatedAt},{atualiza.name},{atualiza.job}", "Certinho");

            //await DisplayAlert("Dados", $"Id: { user_ret.data.FirstOrDefault().id} - Email: { user_ret.data.FirstOrDefault().email}", "Fechar");
            var usuarioentrada = new Usuario
            {
                email = "eve.holt2@reqres.in",
                password = "pistol"
            };

            try
            {
                var usuariosaida = await client_ReqRes_register.Post<Usuario, Usuario>(usuarioentrada);

                await this.DisplayAlert("Usuario Saida", $"{usuariosaida.Token}", "Feito");

                var usuarioPut = await client_ReqRes_register.Post<Usuario, Usuario>(usuarioentrada);


            }
            catch (Exception ex)
            {

                await this.DisplayAlert("Erro", ex.Message, "OK");
            }
            
            //var reg = await client_ReqRes_register.Post<Usuario, Usuario>();

        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            var resultado = await SalvarAsync();
            if (resultado)
            {
                await DisplayAlert("Confirma","Registro salvo com sucesso","Fechar");
            }
            else
            {
                await DisplayAlert("Atenção","Não foi possivel salvar o registro","Fechar");
            }
            
        }

        private async Task<bool> SalvarAsync()
        {
            pessoa = new Pessoa();
            pessoa.Id = "";
            pessoa.RazaoSocial = etRazaoSocial.Text;
            pessoa.Tipo = pkTipo.SelectedItem.ToString();
            pessoa.Endereco = etLogradouro.Text;
            pessoa.Numero = etNumero.Text;
            pessoa.Bairro = etBairro.Text;
            pessoa.CEP = etCEP.Text;
            pessoa.Cidade = etLocalidade.Text;
            pessoa.UF = etUF.Text;
            pessoa.Telefone = etTelefone.Text;
            pessoa.Email = etEmail.Text;
            pessoa.Senha = etSenha.Text;

            if (string.IsNullOrWhiteSpace(pessoa.Id))
            {
                return await pessoaAzureService.IncluirRegistro(pessoa);
            }
            else
            {
                return await pessoaAzureService.AlterarRegistro(pessoa);
            }
        }

        private void ListarTipo()
        {
            List<string> tipos = new List<string>();
            tipos.Add("Consumidor");
            tipos.Add("Distribuidor");
            pkTipo.ItemsSource = tipos;
            pkTipo.SelectedIndex = 0;
        }
    }
}