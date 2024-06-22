using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace SharpOS.Drivers
{
    public class FileSystem : Driver
    {
        public static FileSystem instance;
        public Cosmos.System.FileSystem.CosmosVFS filesystem;
        public override string DriverName => "SharpOS File System";
        public override ConsoleColor DriverConsoleColor => ConsoleColor.Green;
        public override void InitDriver()
        {
            instance = this;
            Log("Init file system; VFS");
            filesystem = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(filesystem);
            filesystem.Initialize(true);
            Log(@"Checking disk format... (disk: 0:\)");
            string format = filesystem.GetFileSystemType(@"0:\");
            Log(format);
            Console.ReadKey();
            if (format != "FAT32")
            {
                Log("WARNING! Your disk's file system type is NOT FAT32. SharpOS can function without it, but it is recommended to use FAT32 to avoid bugs, leading to data loss.\nAre you sure you want to proceed?\n(y/n)");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.N)
                {
                    Kernel.instance.Stop();
                }
            }
        }
    }
}
