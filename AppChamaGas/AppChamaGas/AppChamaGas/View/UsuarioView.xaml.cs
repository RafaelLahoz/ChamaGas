using AppChamaGas.Model;
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
    public partial class UsuarioView : ContentPage
    {
        PessoaAzureService pessoaAzureServico;
        IEnumerable<Pessoa> usuarios;
        public UsuarioView()
        {
            InitializeComponent();
            pessoaAzureServico = new PessoaAzureService();
            ListarUsuariosAsync(vBusca.Text);
        }

        private async void ListarUsuariosAsync(string busca = null)
        {

            lvUsuarios.IsRefreshing = true;
            try
            {

                //Fez a consulta no banco de dados no serviço de nuvem do Azure
                usuarios = await pessoaAzureServico.ListarAsync();

                //Verifica de existe um texto para a busca
                if (!string.IsNullOrWhiteSpace(busca))
                {
                    usuarios
                        .Where(p =>
                        p.RazaoSocial.Contains(busca) ||
                        p.Endereco.Contains(busca) ||
                        p.CEP == busca);

                }

                //Popula a lista com o resultado da consulta
                lvUsuarios.ItemsSource = usuarios
                        .OrderBy(p => p.RazaoSocial)
                        .ToList();
            }
            catch
            {
                await DisplayAlert("Atenção", "Não foi possível fazer a consulta", "Fechar");
            }
            lvUsuarios.IsRefreshing = false;

        }
        //Executa ao carregar o formulario
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListarUsuariosAsync(vBusca.Text);
        }

        private void VBusca_SearchButtonPressed(object sender, EventArgs e)
        {
            //Faz a consulta pelo texto da busca
            ListarUsuariosAsync(vBusca.Text);
        }

        private void LvUsuarios_Refreshing(object sender, EventArgs e)
        {
            ListarUsuariosAsync(vBusca.Text);
        }

        private async void BtnRemover_Clicked(object sender, EventArgs e)
        {
            //Pega o valor do menu da lista CommandParameter
            string id = ((MenuItem)sender).CommandParameter.ToString();
            //Pega o item (objeto) da lista selecionada
            Pessoa usuario = usuarios.FirstOrDefault(p => p.Id == id);
            if (usuario != null)
            {
                //bool retorno = await pessoaAzureServico.ExcluirRegistro(usuario);
                bool retorno = await pessoaAzureServico.ExcluirRegistro(usuario);
                if (retorno)
                {
                    await DisplayAlert("Sucesso", "Registro excluido", "Fechar");
                    ListarUsuariosAsync();
                    return;
                }
            }
            await DisplayAlert("Atenção", "Não foi possivel escluir o registro", "Fechar");


        }

        private void BtnAdicionar_Clicked(object sender, EventArgs e)
        {
            MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new PessoaView());
        }

        private void LvUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Pega o item selecionado "e" na lista e mostra na tela
            Pessoa usuario = e.SelectedItem as Pessoa;
            //Vai para nova pagina (PessoaView) enviando o usuario
            MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new PessoaView(usuario));
        }
    }
}