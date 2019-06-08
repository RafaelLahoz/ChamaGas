using AppChamaGas.Helper;
using AppChamaGas.ViewModel;
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
	public partial class CameraView : ContentPage
	{
		public CameraView ()
		{
			InitializeComponent ();
            this.BindingContext = new CameraViewModel();
		}

        //private async void BtnCapturaImagem_Clicked(object sender, EventArgs e)
        //{
        //    var foto_md = await Photo.TiraFoto(DateTime.Now.ToString()+".jpg");

        //    imgBanco.Source = foto_md.fotoArray.ToImageSource();
        //    imgGaleria.Source = foto_md.pathGaleria;
        //    imgInterna.Source = foto_md.pathInterna;

        //}
    }
}