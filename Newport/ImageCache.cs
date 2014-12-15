using System;
using System.Collections.Generic;
#if UNIVERSAL
using Windows.UI.Xaml.Media.Imaging;
#else
using System.Windows.Media.Imaging;
#endif

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
          bmi = new BitmapImage { UriSource = uri };
#if (!UNIVERSAL)
          bmi.CreateOptions = BitmapCreateOptions.BackgroundCreation;
#endif
          _dict[uri] = bmi;
        }
        return bmi;
      }
    }
  }
}
