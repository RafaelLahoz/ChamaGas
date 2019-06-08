﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppChamaGas.Model
{
    public class FotoMD
    {
        [PrimaryKey, AutoIncrement,NotNull]
        public int id { get; set; }
        [NotNull]
        public byte[] fotoArray { get; set; }
        [NotNull]
        public string pathGaleria { get; set; }
        [NotNull]
        public string pathInterna { get; set; }

    }
}
