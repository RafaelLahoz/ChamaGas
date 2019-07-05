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
	public partial class IntroView : ContentPage
	{
		public IntroView ()
		{
			InitializeComponent ();
		}

        
        private void BtnFechar_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
            //App.Current.MainPage = new MasterView();
        }
    }
}