using AppChamaGas.Model;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Helper
{
    public class Photo
    {
        public static async Task<FotoMD> TiraFoto(string nomeFoto = "test.jpg", string dir = "myDir", bool saveInAlbum = true)
        {
            var md = new FotoMD();

            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                Name = "test.jpg",
                Directory = dir,
                SaveToAlbum = saveInAlbum,
                CompressionQuality = 10,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CustomPhotoSize = 10,
            });
            md.pathGaleria = photo.AlbumPath;
            md.pathInterna = photo.Path;
            md.fotoArray = photo.GetStream().ToByteArray();

            return md;
        }
    }
}
