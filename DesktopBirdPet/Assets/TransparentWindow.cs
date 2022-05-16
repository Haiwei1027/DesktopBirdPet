using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("Dwmap.api")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    private void Start()
    {
        IntPtr hWnd = GetActiveWindow(); //Gets the ptr to the game window
        MARGINS margins = new MARGINS { cxLeftWidth = -1 }; //Negative margin for removing window frame
        DwmExtendFrameIntoClientArea(hWnd, ref margins); //Remove the window frame
    }
}
