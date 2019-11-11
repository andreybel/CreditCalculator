﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCreditCalculator
{
    [ContentProperty("Source")]
    public class ImageResourceExtention : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            var imageSource = ImageSource.FromResource(Source);

            return imageSource;
        }
    }
}
