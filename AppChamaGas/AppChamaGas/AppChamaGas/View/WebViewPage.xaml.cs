﻿using AppChamaGas.Helper;
using AppChamaGas.Interface;
using AppChamaGas.Model;
using Plugin.Share;
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
    public partial class WebViewPage : ContentPage
    {
        string html;
        public WebViewPage(Pedido pedido, IEnumerable<PedidoItem> itens)
        {
            InitializeComponent();

            this.html = GeradorRelatorioPedido.GerarHtml(pedido, itens);
            wvPrincipal.Source = new HtmlWebViewSource { Html = this.html };
           
        }

        public string SalvarArquivo()
        {
            return FileManager.Save(this.html);
        }
        public async void btnShare_Clicked(object sender, EventArgs args)
        {
            var caminho = SalvarArquivo();
            //CrossShare.Current.ShareLink("https://www.google.com");
            var comp = DependencyService.Get<IShare>();

            await comp.Share(caminho, "relat");
        }
    }
}