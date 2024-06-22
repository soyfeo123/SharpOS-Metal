using Cobalt.GetIMG;
using Cosmos.System.Graphics;
using SharpOS;
using CosmosTTF;
using System;
using System.Drawing;
using SharpOS.Drivers.GUI;

namespace Cobalt.TTF
{
    /// <summary>
    /// Here we can cache TTF fonts to be rendered as DrawImage, not DrawImageAlpha.
    /// Text must have a static background!!
    /// </summary>
    public static class TTFCache
    {
        public static CharData[] KMB18Def = new CharData[255];
        public static CharData[] KMB18Dark = new CharData[255];
        public static CharData[] KMB18VVDark = new CharData[255];
        public static void CacheKMB18Default()
        {
            for (int i = 32; i < 127; i++) //Only normal characters
            {
                CanvasGraphics.instance.canvas.DrawFilledRectangle(Color.FromArgb(255, 153, 28), 0, 0, 14, 28); //Background = GUI.colors.Main
                char character = (char)i;
                GlyphResult g = TTFManager.RenderGlyphAsBitmap("System", character, Color.FromArgb(255, 255, 255), 18);
                KMB18Def[i] = new CharData();
                KMB18Def[i].offY = g.offY;
                KMB18Def[i].offX = g.offX;
                CanvasGraphics.instance.canvas.DrawImageAlpha(g.bmp, 0, 18 + (int)g.offY);
                KMB18Def[i].bitmap = TakeBitmap.GetImage(0, 4, 9, 18);
            }
            for (int i = 0; i < 32; i++) //Rest = '?'
            {
                KMB18Def[i] = KMB18Def[63];
            }
            for (int i = 128; i < 255; i++)
            {
                KMB18Def[i] = KMB18Def[63];
            }
        }
        

    }
    public class CharData
    {
        public Bitmap bitmap;
        public int offX;
        public int offY;
    }
}