using Cosmos.System.Graphics;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace SharpOS.GUI.Windows
{
    public class Window
{
    public Bitmap windowImage;
    public uint width;
    public uint height;
    public uint x;
    public string windowTitle;
    public uint y;

    Point dragStart;
    Point lastMousePos;
    bool hasDetectedMouse = false;

    public Window(uint width, uint height, string title, uint x, uint y)
    {
        this.width = width;
        this.height = height;
        this.x = x;
        this.y = y;
        this.windowTitle = title;
        windowImage = new(width, height, ColorDepth.ColorDepth32);
        Constructor();
    }

    public virtual void Draw() { }

    public virtual void Constructor() { }

    public void CoreRun()
    {
        if (Sys.MouseManager.MouseState == Sys.MouseState.Left)
        {

        }
        if (Sys.MouseManager.MouseState == Sys.MouseState.Left && !hasDetectedMouse && (Sys.MouseManager.X > x && Sys.MouseManager.X < x + width) && (Sys.MouseManager.Y < y && Sys.MouseManager.Y > y - 50))
        {

            hasDetectedMouse = true;
            dragStart = new((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y);
            lastMousePos = new(0, 0);
        }
        if (Sys.MouseManager.MouseState == Sys.MouseState.Left && hasDetectedMouse && lastMousePos != new Point((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y))
        {
            x = (uint)(Sys.MouseManager.X - dragStart.X);
            y = (uint)(Sys.MouseManager.Y - dragStart.Y);
            lastMousePos = new((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y);
        }
        if (Sys.MouseManager.MouseState == Sys.MouseState.None && hasDetectedMouse)
        {
            hasDetectedMouse = false;
        }
    }

    public void DrawLine(Pen pen, int xStart, int yStart, int xEnd, int yEnd)
    {
        int dx = Math.Abs(xEnd - xStart);
        int dy = Math.Abs(yEnd - yStart);
        int sx = xStart < xEnd ? 1 : -1;
        int sy = yStart < yEnd ? 1 : -1;
        int err = dx - dy;
        int e2;

        int halfWidth = pen.Width / 2;

        for (int i = -halfWidth; i <= halfWidth; i++)
        {
            int x0 = xStart;
            int y0 = yStart;
            int x1 = xEnd;
            int y1 = yEnd;

            if (dx > dy)
            {
                x0 += i;
                x1 += i;
            }
            else
            {
                y0 += i;
                y1 += i;
            }

            dx = Math.Abs(x1 - x0);
            dy = Math.Abs(y1 - y0);
            sx = x0 < x1 ? 1 : -1;
            sy = y0 < y1 ? 1 : -1;
            err = dx - dy;

            while (true)
            {
                DrawPixel((uint)x0, (uint)y0, pen.Color);

                if (x0 == x1 && y0 == y1) break;
                e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }
    }

    public void DrawPixel(uint x, uint y, Color color)
    {
        windowImage.RawData[windowImage.Width * y + x] = color.ToArgb();
    }

    public void ClearWithColor(Color color)
    {
        for (int h = 0; h < windowImage.Height; h++)
        {
            for (int w = 0; w < windowImage.Width; w++)
            {
                DrawPixel((uint)w, (uint)h, color);
            }
        }
    }
    public void DrawImageAlpha(Bitmap image, int x, int y)
    {
        for (int h = 0; h < image.Height; h++)
        {
            for (int w = 0; w < image.Width; w++)
            {
                if (Color.FromArgb(image.RawData[image.Width * h + w]).A > 120)
                {
                    DrawPixel((uint)(w + x), (uint)(h + y), Color.FromArgb(image.RawData[image.Width * h + w]));
                }
            }
        }
    }
    public void DrawFilledRectangle(Color color, int x, int y, int width, int height)
    {
        for (int h = 0; h < height; h++)
        {
            DrawLine(new Pen(color), x, y + h, x + width, y + h);
        }
    }
    public void DrawText(string text, string font, Color color, float px, int x, int y, float spacingMultiplier = 1f)
    {
        float offx = 0;

        foreach (char c in text)
        {
            char curr = c;
            GlyphResult g = TTFManager.RenderGlyphAsBitmap(font, curr, color, px);
            DrawImageAlpha(g.bmp, x + (int)offx, y + g.offY + (int)px);
            offx += g.offX;
        }
    }
}
    public class FormWindow : Window
    {
        public FormWindow(uint width, uint height, string title, uint x, uint y) : base(width, height, title, x, y) { }

        public List<Control> Controls = new List<Control>();

        public void AddControl(Control control)
        {
            Controls.Add(control);
            UpdateControls();
        }
        public void UpdateControls()
        {
            foreach (Control control in Controls)
            {
                control.DrawControl(this);
            }
        }
    }
}
