using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Media;

namespace Newport
{
  public static class WebBrowserExtensions
  {
    #region Html (Attached Property)

    public static readonly DependencyProperty HtmlProperty =
      DependencyProperty.RegisterAttached(
      "Html",
      typeof(string),
      typeof(WebBrowserExtensions),
      new PropertyMetadata(null, OnHtmlChanged));

    public static void SetHtml(WebBrowser webBrowser, string html)
    {
      webBrowser.SetValue(HtmlProperty, html);
    }

    public static string GetHtml(WebBrowser webBrowser)
    {
      return (string)webBrowser.GetValue(HtmlProperty);
    }

    private static void OnHtmlChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
      var webBrowser = (WebBrowser)sender;
      if (webBrowser != null)
      {
        var body = GetHtml(webBrowser);
        var style = "<style>";
        var background = "#FCCE42";// GetColorForCss("PhoneBackgroundColor"); TODO
        var foreground = "black"; //GetColorForCss("PhoneForegroundColor"); TODO
        style += "body{background-color: " + background + "; color: " + foreground + ";}";
        style += "</style>";
        var html = "<!DOCTYPE html><html><head>" + style + "</head><body>" + body + "</body></html>";
        webBrowser.NavigateToString(html);
      }
    }

    private static string GetColorForCss(string sourceResource)
    {
      var color = (Color)Application.Current.Resources[sourceResource];
      return "#" + color.ToString().Substring(3, 6);
    }
    #endregion
  }
}