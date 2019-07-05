using AppChamaGas.DataAccess;
using AppChamaGas.Helper;
using AppChamaGas.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace AppChamaGas.ViewModel
{
    public class CameraViewModel: ViewModelBase
    {
        Command tirarFotoCommand;
        public Command TirarFotoCommand
        {
            get { return tirarFotoCommand; }
            set { SetProperty(ref tirarFotoCommand, value); }
        }

        ImageSource imgSBanco;
        public ImageSource ImgSBanco
        {
            get { return imgSBanco; }
            set { SetProperty(ref imgSBanco, value); }
        }

        ImageSource imgSGaleria;
        public ImageSource ImgSGaleria
        {
            get { return imgSGaleria; }
            set { SetProperty(ref imgSGaleria, value); }
        }

        
        ImageSource imgSInterna;
        public ImageSource ImgSInterna
        {
            get { return imgSInterna; }
            set { SetProperty(ref imgSInterna, value); }
        }

        public CameraViewModel()
        {
            var conn = Conexao.GetConn();
            var md = conn.Table<FotoMD>().LastOrDefault();
            if (md != null)
                PreencheFotos(md);


            this.TirarFotoCommand = new Command(TirarFoto);
            
        }
        private void PreencheFotos(FotoMD md)
        {
            
            this.ImgSBanco = md.fotoArray.ToImageSource();
            this.ImgSGaleria = md.pathGaleria;
            this.ImgSInterna = md.pathInterna;
        }
        private async void TirarFoto()
        {
            var md = await Photo.TiraFoto();

            if (md == null)
                return;

            var conn = Conexao.GetConn();
            conn.BeginTransaction();
            conn.Insert(md);
            conn.Commit();
            conn.Close();



            PreencheFotos(md);

            //this.ImgSBanco = md.fotoArray.ToImageSource();
            //this.ImgSGaleria = md.pathGaleria;
            //this.ImgSInterna = md.pathInterna;
        }

        
    }
}
