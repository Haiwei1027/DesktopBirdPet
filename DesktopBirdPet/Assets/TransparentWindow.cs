using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{
    public LayerMask clickMask;

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);   

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    const int GWL_EXSTYLE = -20;

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    const uint LWA_COLORKEY = 0x00000001;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    private Camera camera;
    bool mouseOver;
    RaycastHit mouseOverInfo;

    IntPtr hWnd;

    private void Start()
    {
        camera = Camera.main;
#if !UNITY_EDITOR
        hWnd = GetActiveWindow(); //Gets the ptr to the game window
        MARGINS margins = new MARGINS { cxLeftWidth = -1 }; //Negative margin for removing window frame
        DwmExtendFrameIntoClientArea(hWnd, ref margins); //Remove the window frame

        SetClickthrough(true);
        
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);

        Application.runInBackground = true;
#endif
    }

    private void Update()
    {
        mouseOver = CheckMouseover();
        SetClickthrough(!mouseOver);
    }

    private bool CheckMouseover()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction);
        return Physics.Raycast(ray, out mouseOverInfo, 50, clickMask);
    }

    public void SetClickthrough(bool clickthrough)
    {
        if (clickthrough)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }
}
