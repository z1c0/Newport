using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Newport
{
  public class ImageCache
  {
    private readonly Dictionary<Uri, BitmapImage> _dict;

    public ImageCache()
    {
      _dict = new Dictionary<Uri, BitmapImage>();
    }

    public BitmapImage this[Uri uri]
    {
      get
      {
        BitmapImage bmi;
        if (_dict.ContainsKey(uri))
        {
          bmi = _dict[uri];
        }
        else
        {
          bmi = new BitmapImage();
          bmi.CreateOptions = BitmapCreateOptions.BackgroundCreation;
          bmi.UriSource = uri;
          _dict[uri] = bmi;
        }
        return bmi;
      }
    }
  }
}
