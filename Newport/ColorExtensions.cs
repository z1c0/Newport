using System;
#if UNIVERSAL
using Windows.UI;
#else
using System.Windows.Media;
#endif

namespace Newport
{
  public enum GrayScaleMethod
  {
    Lightness,
    Average,
    Luminosity
  }

  public static class ColorExtensions
  {
    public static Color Lerp(this Color fromColor, Color toColor, double amount)
    {
      var r = (byte)Lerp(fromColor.R, toColor.R, amount);
      var g = (byte)Lerp(fromColor.G, toColor.G, amount);
      var b = (byte)Lerp(fromColor.B, toColor.B, amount);
      return Color.FromArgb(255, r, g, b);
    }

    private static double Lerp(double start, double end, double amount)
    {
      var difference = end - start;
      var adjusted = difference * amount;
      return start + adjusted;
    }

    public static int ToARGB32(this Color color)
    {
      return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
    }

    public static Color ToColor(this int argb32)
    {
      var b = (byte)(argb32 & 0xFF);
      argb32 >>= 8;
      var g = (byte)(argb32 & 0xFF);
      argb32 >>= 8;
      var r = (byte)(argb32 & 0xFF);
      argb32 >>= 8;
      var a = (byte)(argb32 & 0xFF);
      return Color.FromArgb(a, r, g, b);
    }

    public static Color GrayScale(this Color color, GrayScaleMethod method = GrayScaleMethod.Luminosity)
    {
      byte v = 0;
      switch (method)
      {
        case GrayScaleMethod.Lightness:
          v = (byte)((Math.Max(color.R, Math.Max(color.G, color.B)) + Math.Min(color.R, Math.Min(color.G, color.B))) / 2);
          break;

        case GrayScaleMethod.Average:
          v = (byte)((color.R + color.G + color.B) / 3);
          break;

        case GrayScaleMethod.Luminosity:
          v = (byte)(0.21 * color.R + 0.71 * color.G + 0.07 * color.B);
          break;
      }

      return Color.FromArgb(255, v, v, v);
    }

    public static Color Sepia(this Color color)
    {
      var r = (byte)Math.Min(255, ((color.R * .393) + (color.G * .769) + (color.B * .189)));
      var g = (byte)Math.Min(255, ((color.R * .349) + (color.G * .686) + (color.B * .168)));
      var b = (byte)Math.Min(255, ((color.R * .272) + (color.G * .534) + (color.B * .131)));
      return Color.FromArgb(255, r, g, b);
    }
  }
}
