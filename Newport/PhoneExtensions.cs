using Microsoft.Devices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Newport
{
  public static class PhoneExtensions
  {
    public static WriteableBitmap GetBitmap(this PhotoCamera camera)
    {
      var w = (int)camera.PreviewResolution.Width;
      var h = (int)camera.PreviewResolution.Height;
      var wb = new WriteableBitmap(w, h);
      if (Microsoft.Devices.Environment.DeviceType == DeviceType.Emulator)
      {
        // TODO
        //wb.Clear(TheColors.All.GetRandomElement());
        //var r = new RandomData();
        //wb.DrawLine(r.GetInt(200), r.GetInt(200), r.GetInt(200, w), r.GetInt(200, h), Colors.Red);
      }
      else
      {
        var pixels = new int[w * h];
        // Copy the current viewfinder frame into a buffer.
        camera.GetPreviewBufferArgb32(pixels);
        // Copy to preview image into a writable bitmap.
        pixels.CopyTo(wb.Pixels, 0);
      }
      return wb;
    }
  }
}
