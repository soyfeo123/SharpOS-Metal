using Cosmos.System.Graphics;
using Cosmos.Core.Memory;
using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using Sys = Cosmos.System;
using SharpOS.Drivers;
using SharpOS.Drivers.GUI;

namespace SharpOS
{
    public class Pen
    {
        public Color Color;
        public int Width;
        public Pen(Color Color, int Width = 1)
        {
            this.Color = Color;
            this.Width = Width;
        }
    }
    public class Kernel : Sys.Kernel
    {
        public List<Driver> drivers;
        public static Kernel instance;
        public int passedFrames = 0;
        public static int _frames;
        public static int _fps = 200;
        public static int _deltaT = 0;
        protected override void BeforeRun()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Kernel: Setting up Kernel instance...");
            instance = this;
            Console.WriteLine("Kernel: Setting up drivers...");
            drivers = new List<Driver>()
            {
                
            };
            AddDriver(new FileSystem());
            FullScreenCanvas.Disable();
            AddDriver(new CanvasGraphics());
            AddDriver(new MouseManager());
            AddDriver(new FPSCounter());
        }

        public void DrawErrorScreen(Exception ex, string reason = "Occured while running system.")
        {
            if(CanvasGraphics.instance.canvas != null)
            {
                CanvasGraphics.instance.canvas.Disable();
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine($"SharpOS Unhandled Exception!\n{reason}.\n" + ex.Message + "\n\nFull exception:" + ex.ToString());
            Console.WriteLine("Press any key to restart.");
            Console.Beep();
            Console.ReadKey();
            Sys.Power.Reboot();
        }

        public void AddDriver(Driver driver)
        {
            try
            {
                drivers.Add(driver);
                driver.InitDriver();
            }
            catch(Exception ex)
            {
                DrawErrorScreen(ex, "Occured while initializing drivers.");
            }
        }

        protected override void Run()
        {
            try
            {
                foreach (Driver driver in drivers)
                {
                    driver.Run();
                }
            }
            catch(Exception ex)
            {
                DrawErrorScreen(ex);
            }
            passedFrames++;
            if(passedFrames >= 20)
            {
                Heap.Collect();
                passedFrames = 0;
            }
            if (_deltaT != RTC.Second)
            {
                _fps = _frames;
                _frames = 0;
                _deltaT = RTC.Second;
            }
            _frames++;
        }
        
    }
}
