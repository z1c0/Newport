using System.Linq;
using System.Collections.Generic;
#if UNIVERSAL
using Windows.UI;
#else
using System.Windows.Media;
#endif

namespace Newport
{
  public static class TheColors
  {
    static TheColors()
    {
      All = new List<Color>
      {
      Color.FromArgb(255, 240, 248, 255), //AliceBlue
Color.FromArgb(255, 250, 235, 215), //AntiqueWhite
Color.FromArgb(255, 0, 255, 255), //Aqua
Color.FromArgb(255, 127, 255, 212), //Aquamarine
Color.FromArgb(255, 240, 255, 255), //Azure
Color.FromArgb(255, 245, 245, 220), //Beige
Color.FromArgb(255, 255, 228, 196), //Bisque
Color.FromArgb(255, 0, 0, 0), //Black
Color.FromArgb(255, 255, 235, 205), //BlanchedAlmond
Color.FromArgb(255, 0, 0, 255), //Blue
Color.FromArgb(255, 138, 43, 226), //BlueViolet
Color.FromArgb(255, 165, 42, 42), //Brown
Color.FromArgb(255, 222, 184, 135), //BurlyWood
Color.FromArgb(255, 95, 158, 160), //CadetBlue
Color.FromArgb(255, 127, 255, 0), //Chartreuse
Color.FromArgb(255, 210, 105, 30), //Chocolate
Color.FromArgb(255, 255, 127, 80), //Coral
Color.FromArgb(255, 100, 149, 237), //CornflowerBlue
Color.FromArgb(255, 255, 248, 220), //Cornsilk
Color.FromArgb(255, 220, 20, 60), //Crimson
Color.FromArgb(255, 0, 255, 255), //Cyan
Color.FromArgb(255, 0, 0, 139), //DarkBlue
Color.FromArgb(255, 0, 139, 139), //DarkCyan
Color.FromArgb(255, 184, 134, 11), //DarkGoldenrod
Color.FromArgb(255, 169, 169, 169), //DarkGray
Color.FromArgb(255, 0, 100, 0), //DarkGreen
Color.FromArgb(255, 189, 183, 107), //DarkKhaki
Color.FromArgb(255, 139, 0, 139), //DarkMagenta
Color.FromArgb(255, 85, 107, 47), //DarkOliveGreen
Color.FromArgb(255, 255, 140, 0), //DarkOrange
Color.FromArgb(255, 153, 50, 204), //DarkOrchid
Color.FromArgb(255, 139, 0, 0), //DarkRed
Color.FromArgb(255, 233, 150, 122), //DarkSalmon
Color.FromArgb(255, 143, 188, 143), //DarkSeaGreen
Color.FromArgb(255, 72, 61, 139), //DarkSlateBlue
Color.FromArgb(255, 47, 79, 79), //DarkSlateGray
Color.FromArgb(255, 0, 206, 209), //DarkTurquoise
Color.FromArgb(255, 148, 0, 211), //DarkViolet
Color.FromArgb(255, 255, 20, 147), //DeepPink
Color.FromArgb(255, 0, 191, 255), //DeepSkyBlue
Color.FromArgb(255, 105, 105, 105), //DimGray
Color.FromArgb(255, 30, 144, 255), //DodgerBlue
Color.FromArgb(255, 178, 34, 34), //Firebrick
Color.FromArgb(255, 255, 250, 240), //FloralWhite
Color.FromArgb(255, 34, 139, 34), //ForestGreen
Color.FromArgb(255, 255, 0, 255), //Fuchsia
Color.FromArgb(255, 220, 220, 220), //Gainsboro
Color.FromArgb(255, 248, 248, 255), //GhostWhite
Color.FromArgb(255, 255, 215, 0), //Gold
Color.FromArgb(255, 218, 165, 32), //Goldenrod
Color.FromArgb(255, 128, 128, 128), //Gray
Color.FromArgb(255, 0, 128, 0), //Green
Color.FromArgb(255, 173, 255, 47), //GreenYellow
Color.FromArgb(255, 240, 255, 240), //Honeydew
Color.FromArgb(255, 255, 105, 180), //HotPink
Color.FromArgb(255, 205, 92, 92), //IndianRed
Color.FromArgb(255, 75, 0, 130), //Indigo
Color.FromArgb(255, 255, 255, 240), //Ivory
Color.FromArgb(255, 240, 230, 140), //Khaki
Color.FromArgb(255, 230, 230, 250), //Lavender
Color.FromArgb(255, 255, 240, 245), //LavenderBlush
Color.FromArgb(255, 124, 252, 0), //LawnGreen
Color.FromArgb(255, 255, 250, 205), //LemonChiffon
Color.FromArgb(255, 173, 216, 230), //LightBlue
Color.FromArgb(255, 240, 128, 128), //LightCoral
Color.FromArgb(255, 224, 255, 255), //LightCyan
Color.FromArgb(255, 250, 250, 210), //LightGoldenrodYellow
Color.FromArgb(255, 211, 211, 211), //LightGray
Color.FromArgb(255, 144, 238, 144), //LightGreen
Color.FromArgb(255, 255, 182, 193), //LightPink
Color.FromArgb(255, 255, 160, 122), //LightSalmon
Color.FromArgb(255, 32, 178, 170), //LightSeaGreen
Color.FromArgb(255, 135, 206, 250), //LightSkyBlue
Color.FromArgb(255, 119, 136, 153), //LightSlateGray
Color.FromArgb(255, 176, 196, 222), //LightSteelBlue
Color.FromArgb(255, 255, 255, 224), //LightYellow
Color.FromArgb(255, 0, 255, 0), //Lime
Color.FromArgb(255, 50, 205, 50), //LimeGreen
Color.FromArgb(255, 250, 240, 230), //Linen
Color.FromArgb(255, 255, 0, 255), //Magenta
Color.FromArgb(255, 128, 0, 0), //Maroon
Color.FromArgb(255, 102, 205, 170), //MediumAquamarine
Color.FromArgb(255, 0, 0, 205), //MediumBlue
Color.FromArgb(255, 186, 85, 211), //MediumOrchid
Color.FromArgb(255, 147, 112, 219), //MediumPurple
Color.FromArgb(255, 60, 179, 113), //MediumSeaGreen
Color.FromArgb(255, 123, 104, 238), //MediumSlateBlue
Color.FromArgb(255, 0, 250, 154), //MediumSpringGreen
Color.FromArgb(255, 72, 209, 204), //MediumTurquoise
Color.FromArgb(255, 199, 21, 133), //MediumVioletRed
Color.FromArgb(255, 25, 25, 112), //MidnightBlue
Color.FromArgb(255, 245, 255, 250), //MintCream
Color.FromArgb(255, 255, 228, 225), //MistyRose
Color.FromArgb(255, 255, 228, 181), //Moccasin
Color.FromArgb(255, 255, 222, 173), //NavajoWhite
Color.FromArgb(255, 0, 0, 128), //Navy
Color.FromArgb(255, 253, 245, 230), //OldLace
Color.FromArgb(255, 128, 128, 0), //Olive
Color.FromArgb(255, 107, 142, 35), //OliveDrab
Color.FromArgb(255, 255, 165, 0), //Orange
Color.FromArgb(255, 255, 69, 0), //OrangeRed
Color.FromArgb(255, 218, 112, 214), //Orchid
Color.FromArgb(255, 238, 232, 170), //PaleGoldenrod
Color.FromArgb(255, 152, 251, 152), //PaleGreen
Color.FromArgb(255, 175, 238, 238), //PaleTurquoise
Color.FromArgb(255, 219, 112, 147), //PaleVioletRed
Color.FromArgb(255, 255, 239, 213), //PapayaWhip
Color.FromArgb(255, 255, 218, 185), //PeachPuff
Color.FromArgb(255, 205, 133, 63), //Peru
Color.FromArgb(255, 255, 192, 203), //Pink
Color.FromArgb(255, 221, 160, 221), //Plum
Color.FromArgb(255, 176, 224, 230), //PowderBlue
Color.FromArgb(255, 128, 0, 128), //Purple
Color.FromArgb(255, 255, 0, 0), //Red
Color.FromArgb(255, 188, 143, 143), //RosyBrown
Color.FromArgb(255, 65, 105, 225), //RoyalBlue
Color.FromArgb(255, 139, 69, 19), //SaddleBrown
Color.FromArgb(255, 250, 128, 114), //Salmon
Color.FromArgb(255, 244, 164, 96), //SandyBrown
Color.FromArgb(255, 46, 139, 87), //SeaGreen
Color.FromArgb(255, 255, 245, 238), //SeaShell
Color.FromArgb(255, 160, 82, 45), //Sienna
Color.FromArgb(255, 192, 192, 192), //Silver
Color.FromArgb(255, 135, 206, 235), //SkyBlue
Color.FromArgb(255, 106, 90, 205), //SlateBlue
Color.FromArgb(255, 112, 128, 144), //SlateGray
Color.FromArgb(255, 255, 250, 250), //Snow
Color.FromArgb(255, 0, 255, 127), //SpringGreen
Color.FromArgb(255, 70, 130, 180), //SteelBlue
Color.FromArgb(255, 210, 180, 140), //Tan
Color.FromArgb(255, 0, 128, 128), //Teal
Color.FromArgb(255, 216, 191, 216), //Thistle
Color.FromArgb(255, 255, 99, 71), //Tomato
Color.FromArgb(0, 255, 255, 255), //Transparent
Color.FromArgb(255, 64, 224, 208), //Turquoise
Color.FromArgb(255, 238, 130, 238), //Violet
Color.FromArgb(255, 245, 222, 179), //Wheat
Color.FromArgb(255, 255, 255, 255), //White
Color.FromArgb(255, 245, 245, 245), //WhiteSmoke
Color.FromArgb(255, 255, 255, 0), //Yellow
Color.FromArgb(255, 154, 205, 50), //YellowGreen
      };
    }

    public static Color AliceBlue
    {
      get
      {
        return All.ElementAt(0);
      }
    }
    public static Color AntiqueWhite
    {
      get
      {
        return All.ElementAt(1);
      }
    }
    public static Color Aqua
    {
      get
      {
        return All.ElementAt(2);
      }
    }
    public static Color Aquamarine
    {
      get
      {
        return All.ElementAt(3);
      }
    }
    public static Color Azure
    {
      get
      {
        return All.ElementAt(4);
      }
    }
    public static Color Beige
    {
      get
      {
        return All.ElementAt(5);
      }
    }
    public static Color Bisque
    {
      get
      {
        return All.ElementAt(6);
      }
    }
    public static Color Black
    {
      get
      {
        return All.ElementAt(7);
      }
    }
    public static Color BlanchedAlmond
    {
      get
      {
        return All.ElementAt(8);
      }
    }
    public static Color Blue
    {
      get
      {
        return All.ElementAt(9);
      }
    }
    public static Color BlueViolet
    {
      get
      {
        return All.ElementAt(10);
      }
    }
    public static Color Brown
    {
      get
      {
        return All.ElementAt(11);
      }
    }
    public static Color BurlyWood
    {
      get
      {
        return All.ElementAt(12);
      }
    }
    public static Color CadetBlue
    {
      get
      {
        return All.ElementAt(13);
      }
    }
    public static Color Chartreuse
    {
      get
      {
        return All.ElementAt(14);
      }
    }
    public static Color Chocolate
    {
      get
      {
        return All.ElementAt(15);
      }
    }
    public static Color Coral
    {
      get
      {
        return All.ElementAt(16);
      }
    }
    public static Color CornflowerBlue
    {
      get
      {
        return All.ElementAt(17);
      }
    }
    public static Color Cornsilk
    {
      get
      {
        return All.ElementAt(18);
      }
    }
    public static Color Crimson
    {
      get
      {
        return All.ElementAt(19);
      }
    }
    public static Color Cyan
    {
      get
      {
        return All.ElementAt(20);
      }
    }
    public static Color DarkBlue
    {
      get
      {
        return All.ElementAt(21);
      }
    }
    public static Color DarkCyan
    {
      get
      {
        return All.ElementAt(22);
      }
    }
    public static Color DarkGoldenrod
    {
      get
      {
        return All.ElementAt(23);
      }
    }
    public static Color DarkGray
    {
      get
      {
        return All.ElementAt(24);
      }
    }
    public static Color DarkGreen
    {
      get
      {
        return All.ElementAt(25);
      }
    }
    public static Color DarkKhaki
    {
      get
      {
        return All.ElementAt(26);
      }
    }
    public static Color DarkMagenta
    {
      get
      {
        return All.ElementAt(27);
      }
    }
    public static Color DarkOliveGreen
    {
      get
      {
        return All.ElementAt(28);
      }
    }
    public static Color DarkOrange
    {
      get
      {
        return All.ElementAt(29);
      }
    }
    public static Color DarkOrchid
    {
      get
      {
        return All.ElementAt(30);
      }
    }
    public static Color DarkRed
    {
      get
      {
        return All.ElementAt(31);
      }
    }
    public static Color DarkSalmon
    {
      get
      {
        return All.ElementAt(32);
      }
    }
    public static Color DarkSeaGreen
    {
      get
      {
        return All.ElementAt(33);
      }
    }
    public static Color DarkSlateBlue
    {
      get
      {
        return All.ElementAt(34);
      }
    }
    public static Color DarkSlateGray
    {
      get
      {
        return All.ElementAt(35);
      }
    }
    public static Color DarkTurquoise
    {
      get
      {
        return All.ElementAt(36);
      }
    }
    public static Color DarkViolet
    {
      get
      {
        return All.ElementAt(37);
      }
    }
    public static Color DeepPink
    {
      get
      {
        return All.ElementAt(38);
      }
    }
    public static Color DeepSkyBlue
    {
      get
      {
        return All.ElementAt(39);
      }
    }
    public static Color DimGray
    {
      get
      {
        return All.ElementAt(40);
      }
    }
    public static Color DodgerBlue
    {
      get
      {
        return All.ElementAt(41);
      }
    }
    public static Color Firebrick
    {
      get
      {
        return All.ElementAt(42);
      }
    }
    public static Color FloralWhite
    {
      get
      {
        return All.ElementAt(43);
      }
    }
    public static Color ForestGreen
    {
      get
      {
        return All.ElementAt(44);
      }
    }
    public static Color Fuchsia
    {
      get
      {
        return All.ElementAt(45);
      }
    }
    public static Color Gainsboro
    {
      get
      {
        return All.ElementAt(46);
      }
    }
    public static Color GhostWhite
    {
      get
      {
        return All.ElementAt(47);
      }
    }
    public static Color Gold
    {
      get
      {
        return All.ElementAt(48);
      }
    }
    public static Color Goldenrod
    {
      get
      {
        return All.ElementAt(49);
      }
    }
    public static Color Gray
    {
      get
      {
        return All.ElementAt(50);
      }
    }
    public static Color Green
    {
      get
      {
        return All.ElementAt(51);
      }
    }
    public static Color GreenYellow
    {
      get
      {
        return All.ElementAt(52);
      }
    }
    public static Color Honeydew
    {
      get
      {
        return All.ElementAt(53);
      }
    }
    public static Color HotPink
    {
      get
      {
        return All.ElementAt(54);
      }
    }
    public static Color IndianRed
    {
      get
      {
        return All.ElementAt(55);
      }
    }
    public static Color Indigo
    {
      get
      {
        return All.ElementAt(56);
      }
    }
    public static Color Ivory
    {
      get
      {
        return All.ElementAt(57);
      }
    }
    public static Color Khaki
    {
      get
      {
        return All.ElementAt(58);
      }
    }
    public static Color Lavender
    {
      get
      {
        return All.ElementAt(59);
      }
    }
    public static Color LavenderBlush
    {
      get
      {
        return All.ElementAt(60);
      }
    }
    public static Color LawnGreen
    {
      get
      {
        return All.ElementAt(61);
      }
    }
    public static Color LemonChiffon
    {
      get
      {
        return All.ElementAt(62);
      }
    }
    public static Color LightBlue
    {
      get
      {
        return All.ElementAt(63);
      }
    }
    public static Color LightCoral
    {
      get
      {
        return All.ElementAt(64);
      }
    }
    public static Color LightCyan
    {
      get
      {
        return All.ElementAt(65);
      }
    }
    public static Color LightGoldenrodYellow
    {
      get
      {
        return All.ElementAt(66);
      }
    }
    public static Color LightGray
    {
      get
      {
        return All.ElementAt(67);
      }
    }
    public static Color LightGreen
    {
      get
      {
        return All.ElementAt(68);
      }
    }
    public static Color LightPink
    {
      get
      {
        return All.ElementAt(69);
      }
    }
    public static Color LightSalmon
    {
      get
      {
        return All.ElementAt(70);
      }
    }
    public static Color LightSeaGreen
    {
      get
      {
        return All.ElementAt(71);
      }
    }
    public static Color LightSkyBlue
    {
      get
      {
        return All.ElementAt(72);
      }
    }
    public static Color LightSlateGray
    {
      get
      {
        return All.ElementAt(73);
      }
    }
    public static Color LightSteelBlue
    {
      get
      {
        return All.ElementAt(74);
      }
    }
    public static Color LightYellow
    {
      get
      {
        return All.ElementAt(75);
      }
    }
    public static Color Lime
    {
      get
      {
        return All.ElementAt(76);
      }
    }
    public static Color LimeGreen
    {
      get
      {
        return All.ElementAt(77);
      }
    }
    public static Color Linen
    {
      get
      {
        return All.ElementAt(78);
      }
    }
    public static Color Magenta
    {
      get
      {
        return All.ElementAt(79);
      }
    }
    public static Color Maroon
    {
      get
      {
        return All.ElementAt(80);
      }
    }
    public static Color MediumAquamarine
    {
      get
      {
        return All.ElementAt(81);
      }
    }
    public static Color MediumBlue
    {
      get
      {
        return All.ElementAt(82);
      }
    }
    public static Color MediumOrchid
    {
      get
      {
        return All.ElementAt(83);
      }
    }
    public static Color MediumPurple
    {
      get
      {
        return All.ElementAt(84);
      }
    }
    public static Color MediumSeaGreen
    {
      get
      {
        return All.ElementAt(85);
      }
    }
    public static Color MediumSlateBlue
    {
      get
      {
        return All.ElementAt(86);
      }
    }
    public static Color MediumSpringGreen
    {
      get
      {
        return All.ElementAt(87);
      }
    }
    public static Color MediumTurquoise
    {
      get
      {
        return All.ElementAt(88);
      }
    }
    public static Color MediumVioletRed
    {
      get
      {
        return All.ElementAt(89);
      }
    }
    public static Color MidnightBlue
    {
      get
      {
        return All.ElementAt(90);
      }
    }
    public static Color MintCream
    {
      get
      {
        return All.ElementAt(91);
      }
    }
    public static Color MistyRose
    {
      get
      {
        return All.ElementAt(92);
      }
    }
    public static Color Moccasin
    {
      get
      {
        return All.ElementAt(93);
      }
    }
    public static Color NavajoWhite
    {
      get
      {
        return All.ElementAt(94);
      }
    }
    public static Color Navy
    {
      get
      {
        return All.ElementAt(95);
      }
    }
    public static Color OldLace
    {
      get
      {
        return All.ElementAt(96);
      }
    }
    public static Color Olive
    {
      get
      {
        return All.ElementAt(97);
      }
    }
    public static Color OliveDrab
    {
      get
      {
        return All.ElementAt(98);
      }
    }
    public static Color Orange
    {
      get
      {
        return All.ElementAt(99);
      }
    }
    public static Color OrangeRed
    {
      get
      {
        return All.ElementAt(100);
      }
    }
    public static Color Orchid
    {
      get
      {
        return All.ElementAt(101);
      }
    }
    public static Color PaleGoldenrod
    {
      get
      {
        return All.ElementAt(102);
      }
    }
    public static Color PaleGreen
    {
      get
      {
        return All.ElementAt(103);
      }
    }
    public static Color PaleTurquoise
    {
      get
      {
        return All.ElementAt(104);
      }
    }
    public static Color PaleVioletRed
    {
      get
      {
        return All.ElementAt(105);
      }
    }
    public static Color PapayaWhip
    {
      get
      {
        return All.ElementAt(106);
      }
    }
    public static Color PeachPuff
    {
      get
      {
        return All.ElementAt(107);
      }
    }
    public static Color Peru
    {
      get
      {
        return All.ElementAt(108);
      }
    }
    public static Color Pink
    {
      get
      {
        return All.ElementAt(109);
      }
    }
    public static Color Plum
    {
      get
      {
        return All.ElementAt(110);
      }
    }
    public static Color PowderBlue
    {
      get
      {
        return All.ElementAt(111);
      }
    }
    public static Color Purple
    {
      get
      {
        return All.ElementAt(112);
      }
    }
    public static Color Red
    {
      get
      {
        return All.ElementAt(113);
      }
    }
    public static Color RosyBrown
    {
      get
      {
        return All.ElementAt(114);
      }
    }
    public static Color RoyalBlue
    {
      get
      {
        return All.ElementAt(115);
      }
    }
    public static Color SaddleBrown
    {
      get
      {
        return All.ElementAt(116);
      }
    }
    public static Color Salmon
    {
      get
      {
        return All.ElementAt(117);
      }
    }
    public static Color SandyBrown
    {
      get
      {
        return All.ElementAt(118);
      }
    }
    public static Color SeaGreen
    {
      get
      {
        return All.ElementAt(119);
      }
    }
    public static Color SeaShell
    {
      get
      {
        return All.ElementAt(120);
      }
    }
    public static Color Sienna
    {
      get
      {
        return All.ElementAt(121);
      }
    }
    public static Color Silver
    {
      get
      {
        return All.ElementAt(122);
      }
    }
    public static Color SkyBlue
    {
      get
      {
        return All.ElementAt(123);
      }
    }
    public static Color SlateBlue
    {
      get
      {
        return All.ElementAt(124);
      }
    }
    public static Color SlateGray
    {
      get
      {
        return All.ElementAt(125);
      }
    }
    public static Color Snow
    {
      get
      {
        return All.ElementAt(126);
      }
    }
    public static Color SpringGreen
    {
      get
      {
        return All.ElementAt(127);
      }
    }
    public static Color SteelBlue
    {
      get
      {
        return All.ElementAt(128);
      }
    }
    public static Color Tan
    {
      get
      {
        return All.ElementAt(129);
      }
    }
    public static Color Teal
    {
      get
      {
        return All.ElementAt(130);
      }
    }
    public static Color Thistle
    {
      get
      {
        return All.ElementAt(131);
      }
    }
    public static Color Tomato
    {
      get
      {
        return All.ElementAt(132);
      }
    }
    public static Color Transparent
    {
      get
      {
        return All.ElementAt(133);
      }
    }
    public static Color Turquoise
    {
      get
      {
        return All.ElementAt(134);
      }
    }
    public static Color Violet
    {
      get
      {
        return All.ElementAt(135);
      }
    }
    public static Color Wheat
    {
      get
      {
        return All.ElementAt(136);
      }
    }
    public static Color White
    {
      get
      {
        return All.ElementAt(137);
      }
    }
    public static Color WhiteSmoke
    {
      get
      {
        return All.ElementAt(138);
      }
    }
    public static Color Yellow
    {
      get
      {
        return All.ElementAt(139);
      }
    }
    public static Color YellowGreen
    {
      get
      {
        return All.ElementAt(140);
      }
    }

    public static IEnumerable<Color> All { get; private set; }
  }
}

