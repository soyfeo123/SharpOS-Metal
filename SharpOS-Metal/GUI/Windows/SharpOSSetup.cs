using SharpOS.GUI.Windows;
using SharpOS.GUI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOS.GUI.Windows
{
    public class SharpOSSetup : FormWindow
{
    public SharpOSSetup() : base(960, 540, "SharpOS Setup", 100, 100) { }
    public Label title;
    public Label description;

    public override void Constructor()
    {
        ClearWithColor(Color.FromArgb(237, 237, 237));
        title = new Label(20, 20, 42, "Welcome to SharpOS!");
        description = new Label(20, 72, 24, "SharpOS is still in an early stage of development. Expect a lot of bugs. Use this ONLY in a VM.");
        AddControl(title);
        AddControl(description);
    }
}
}
