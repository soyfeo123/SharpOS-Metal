using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace SharpOS.Drivers
{
    public class Driver
    {
        public virtual string DriverName { get { return "SharpOS"; } }
        public virtual ConsoleColor DriverConsoleColor { get { return ConsoleColor.Green; } }
        public virtual void InitDriver() { }
        public virtual void Run() { }
        public virtual void Draw(Sys.Graphics.Canvas canvas) { }
        public virtual void Quitting() { }
        public void Log(string obj)
        {
            Console.Write("[");
            Console.ForegroundColor = DriverConsoleColor;
            Console.Write(DriverName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]: " + obj + "\n");
        }
    }
}
