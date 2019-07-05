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
	public partial class GaleriaView : ContentPage
	{
		public GaleriaView ()
		{
			InitializeComponent ();
            ListarImagens();
		}
        private void ListarImagens()
        {
            //URL da Imagem
            var uri = new Uri("https://picsum.photos/30/300");
            //Contador de 10 imagens
            for (int i = 0; i < 10; i++)
            {
                //Instancia de cada imagem
                var imagem = new Image
                {
                    Source = ImageSource.FromUri(uri),
                    //Aspect = Aspect.AspectFill
                };
                //Adiciona no Layout
                flxLayout.Children.Add(imagem);
            }
        }
	}
}