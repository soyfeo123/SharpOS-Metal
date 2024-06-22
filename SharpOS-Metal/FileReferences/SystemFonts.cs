using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOS.Files
{
    public static class SystemFonts
    {
        [ManifestResourceStream(ResourceName = "SharpOS-Metal.isoFiles.Jost.ttf")]
        public static byte[] defaultSystemFont;
        [ManifestResourceStream(ResourceName = "SharpOS-Metal.isoFiles.cursor.bmp")]
        public static byte[] cursorImg;
    }
}
