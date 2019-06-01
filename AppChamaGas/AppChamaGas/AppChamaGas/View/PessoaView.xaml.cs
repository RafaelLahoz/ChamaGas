using AppChamaGas.Model;
using AppChamaGas.Services;
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
        Base_Service client_cep = new Base_Service(Base_Service.URL_VIACEP);
        //Base_Service client_api = new Base_Service(Base_Service.URL_MINHAAPI);
        ReqRes_Service client_ReqRes_user = new ReqRes_Service("users");
        ReqRes_Service client_ReqRes_register = new ReqRes_Service("register");
        User_ReqRes md = new User_ReqRes();

        public PessoaView ()
		{
			InitializeComponent ();
            
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
                Email = "rafael@senacsp.edu.br",
                Password = "123"
            };
            var usuariosaida = await client_ReqRes_register.Post<Usuario, Usuario>(usuarioentrada);

            await this.DisplayAlert("Usuario Saida", $"{usuariosaida.Token}", "Feito");

            //var reg = await client_ReqRes_register.Post<Usuario, Usuario>();

        }


    }
}