using System;
using System.Collections.Generic;
using System.Linq;
using SharpOS.Drivers;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;
using Cosmos.Core.Memory;
using Cosmos.HAL;
using Sys = Cosmos.System;
using Cobalt.TTF;
using CosmosTTF;
using SharpOS.Files;
using System.Drawing;

namespace SharpOS.Drivers.GUI
{
    public class MouseManager : Driver
    {
        public static CanvasGraphics instance;
        public Bitmap cursorImg;
        public override string DriverName => "Mouse";
        public override void InitDriver()
        {
            Log("Init Mouse");
            cursorImg = new(SystemFonts.cursorImg);
        }
        public override void Draw(Canvas canvas)
        {
            CanvasGraphics.DrawImageAlpha(cursorImg, (int)Sys.MouseManager.X, (int)Sys.MouseManager.Y);
        }
    }
}
