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
using SharpOS.GUI.Windows;

namespace SharpOS.Drivers.GUI
{
    public class CanvasGraphics : Driver
    {
        public static CanvasGraphics instance;
        public override string DriverName => "SharpOS Canvas Graphics";
        public List<Window> windows = new List<Window>();
        public override ConsoleColor DriverConsoleColor => ConsoleColor.Cyan;
        public SVGAIICanvas canvas;
        public void DrawText(string text, int x, int y, Color color, int size = 16)
        {
            TTFManager.DrawStringTTF(canvas, text, "System", color, size, x, y);
        }
        public override void InitDriver()
        {
            instance = this;
            Log("Welcome to SharpOS-Metal!");
            Log("This is supposed to be a SharpOS port to actual hardware.");
            Log("If you're running this on real hardware, please be careful, as data loss can happen.");
            Log("If you REALLY want to run this on real hardware, don't use it on your main.");

            Log("Press any key to enter!");
            Console.ReadKey();
            Log("Loading system font...");
            Log("Setting up FullScreenCanvas...");
            Global.debugger.Send("Setup fullscreencanvas");
            canvas = new SVGAIICanvas(new Mode(1280, 720, ColorDepth.ColorDepth32));
            Sys.MouseManager.ScreenWidth = 1280;
            Sys.MouseManager.ScreenHeight = 720;
            TTFManager.RegisterFont("System", SystemFonts.defaultSystemFont);
            TTFCache.CacheKMB18Default();
            canvas.Clear(Color.FromArgb(255, 153, 28));
            windows.Add(new SharpOSSetup());
            Log("You are now in text-mode! If you're seeing this, something happened.");
            Log("Make sure your video driver works properly.");
        }
        public override void Run()
        {
            //Log("Before DrawFilledRectangle");
            this.canvas.DrawFilledRectangle(Color.FromArgb(255, 153, 28), 0, 0, 1280, 720);

            foreach (Window window in windows)
            {
                window.CoreRun();
                //Log("Draw window " + window.windowTitle);
                window.Draw();
                //Log("Draw topbar " + window.windowTitle);
                this.canvas.DrawFilledRectangle(Color.FromArgb(18, 9, 0), (int)window.x, (int)window.y - 50, (int)window.width, 50);
                //Log("Draw topbar title " + window.windowTitle);
                DrawText(window.windowTitle, (int)window.x, (int)window.y - 50, Color.White, 24);
                //Log("Draw main window");
                this.canvas.DrawImage(window.windowImage, (int)window.x, (int)window.y);
            }
            foreach (Driver driver in Kernel.instance.drivers)
            {
                driver.Draw(canvas);
            }
            //Log("Window Display");
            canvas.Display();
        }

        public override void Draw(Canvas canvas)
        {

        }

        public static void DrawImageAlpha(Bitmap image, int x, int y)
        {
            for (int h = 0; h < image.Height; h++)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    if (Color.FromArgb(image.RawData[image.Width * h + w]).A > 120)
                    {
                        CanvasGraphics.instance.canvas.DrawPoint(Color.FromArgb(image.RawData[image.Width * h + w]), w + x, h + y);
                    }
                }
            }
        }
    }
}
