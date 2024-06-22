using SharpOS.GUI;
using SharpOS.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOS.GUI.Controls
{
    public class Label : Control
{
    public int X = 0;
    public int Y = 0;
    public int FontSize = 0;
    public string Text = "label";
    public Label()
    {

    }
    public Label(int x, int y, int fontSize = 24, string text = "label")
    {
        X = x;
        Y = y;
        FontSize = fontSize;
        Text = text;
    }

    public override void DrawControl(Window window)
    {
        window.DrawText(Text, "System", Color.Black, FontSize, X, Y);
    }
}
}
