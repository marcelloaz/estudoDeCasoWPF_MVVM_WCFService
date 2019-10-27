using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HAPPYWPF.UI.Comum
{
    public class ColorPicker
    {
        const uint CC_ANYCOLOR = 0x00000100; // Causes the dialog box to display all available colors in the set of basic colors. 
        const uint CC_ENABLEHOOK = 0x00000010;// Enables the hook procedure specified in the lpfnHook member of this structure. This flag is used only to initialize the dialog box.
        const uint CC_ENABLETEMPLATE = 0x00000020;// The hInstance and lpTemplateName members specify a dialog box template to use in place of the default template. This flag is used only to initialize the dialog box.
        const uint CC_ENABLETEMPLATEHANDLE = 0x00000040; // The hInstance member identifies a data block that contains a preloaded dialog box template. The system ignores the lpTemplateName member if this flag is specified. This flag is used only to initialize the dialog box.
        const uint CC_FULLOPEN = 0x00000002; // Causes the dialog box to display the additional controls that allow the user to create custom colors. If this flag is not set, the user must click the Define Custom Color button to display the custom color controls.
        const uint CC_PREVENTFULLOPEN = 0x00000004; // Disables the Define Custom Color button.
        const uint CC_RGBINIT = 0x00000001; // Causes the dialog box to use the color specified in the rgbResult member as the initial color selection.
        const uint CC_SHOWHELP = 0x00000008;// Causes the dialog box to display the Help button. The hwndOwner member must specify the window to receive the HELPMSGSTRING registered messages that the dialog box sends when the user clicks the Help button.
        const uint CC_SOLIDCOLOR = 0x00000080;

        [StructLayout(LayoutKind.Sequential)]
        private struct CHOOSECOLOR
        {
            public int lStructSize;
            public IntPtr hwndOwner;
            public IntPtr hInstance;
            public uint rgbResult;
            public IntPtr lpCustColors;
            public uint Flags;
            public IntPtr lCustData;
            public IntPtr lpfnHook;
            public IntPtr lpTemplateName;
        }

        [DllImport("comdlg32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ChooseColorW(out CHOOSECOLOR lpcc);

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public Color Color { get; set; }

        public static uint ColorToUInt(Color color)
        {
            return (uint)color.B << 16 | (uint)color.G << 8 | color.R;
        }

        public static Color ColorFromUInt(uint value)
        {
            return Color.FromRgb((byte)(value & 0xFF), (byte)(value >> 8 & 0xFF), (byte)(value >> 16 & 0xFF));
        }

        public bool ShowDialog()
        {
            var cc = new CHOOSECOLOR();

            cc.lStructSize = (int)Marshal.SizeOf<CHOOSECOLOR>();
            cc.hwndOwner = GetActiveWindow();
            cc.rgbResult = ColorPicker.ColorToUInt(Color);
            cc.Flags = CC_ANYCOLOR | CC_RGBINIT;
            cc.lpCustColors = Marshal.AllocHGlobal(64);
            for (int i = 0; i < 16; ++i)
                Marshal.WriteInt32(cc.lpCustColors + i * 4, 0);

            var ret = ChooseColorW(out cc);

            if (ret)
                Color = ColorPicker.ColorFromUInt(cc.rgbResult);

            Marshal.FreeHGlobal(cc.lpCustColors);

            return ret;
        }
    }
}
