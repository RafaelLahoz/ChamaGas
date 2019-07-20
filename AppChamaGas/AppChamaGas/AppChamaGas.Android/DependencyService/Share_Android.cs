﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppChamaGas.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(AppChamaGas.Droid.DependencyService.Share_Android))]
namespace AppChamaGas.Droid.DependencyService
{
    public class Share_Android : IShare
    {
        private readonly Context _context;
        public Share_Android()
        {
            _context = Android.App.Application.Context;
        }
        public Task Share(string path, string title)
        {
            // meu metodo for, if, return
            //var extension = path.Substring(path.LastIndexOf(".") + 1).ToLower();
            //var contentType = string.Empty;

            // You can manually map more ContentTypes here if you want.
            //switch (extension)
            //{
            //    case "pdf":
            //        contentType = "application/pdf";
            //        break;
            //    case "png":
            //        contentType = "image/png";
            //        break;
            //    default:
            //        contentType = "application/octetstream";
            //        break;
            //}

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("application/html");
            //intent.PutExtra(Intent.ExtraStream, Uri.Parse("file://" + path));
            intent.PutExtra(Intent.ExtraStream, new Java.IO.File(path));
            intent.PutExtra(Intent.ExtraText, string.Empty);
            //intent.PutExtra(Intent.ExtraSubject, message ?? string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            _context.StartActivity(chooserIntent);

            return Task.FromResult(true);
        }
    }
}