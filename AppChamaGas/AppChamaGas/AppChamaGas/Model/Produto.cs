using System;
using System.Collections.Generic;
using System.Text;
using AppChamaGas.Helper;
using AppChamaGas.Interface;
using AppChamaGas.ViewModel;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace AppChamaGas.Model
{
    public class Produto : ViewModelBase, IAzureTabela
    {
        public string Id { get; set; }
        public string FornecedorId { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        private string foto;
        public string Foto
        {
            get { return foto; }
            set { SetProperty(ref foto, value);
                if(!string.IsNullOrEmpty(null))
                FotoSource = Convert.FromBase64String(value).ToImageSource();
            }
        }
        //byte [] array
        private byte[] fotoByte;
        [JsonIgnore]
        public byte[] FotoByte
        {
            get
            {
                if (fotoByte == null && Foto != null)
                    FotoByte = Convert.FromBase64String(Foto);
                return fotoByte;
            }
            set
            {
                SetProperty(ref fotoByte, value);
                if (value != null)
                {
                    
                    Foto = Convert.ToBase64String(value);
                }
            }
        }
        //imageSource
        private ImageSource fotoSource;
        [JsonIgnore]
        public ImageSource FotoSource
        {
            get { return fotoSource; }
            set { SetProperty(ref fotoSource, value); }
        }
        public string UnidMedida { get; set; }
        [JsonIgnore, Ignore]
        public string FornecedorNome { get; set; }
        [JsonIgnore, Ignore]
        public string Distancia { get; set; }
        [JsonIgnore, Ignore]
        public double Latitude { get; set; }
        [JsonIgnore, Ignore]
        public double Longitude { get; set; }
    }
}
