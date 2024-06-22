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
    public class FPSCounter : Driver
    {
        public override void Draw(Canvas canvas)
        {
            CanvasGraphics.instance.DrawText("FPS: " + Kernel._fps.ToString(), 0, 0, Color.White);
        }
    }
}
