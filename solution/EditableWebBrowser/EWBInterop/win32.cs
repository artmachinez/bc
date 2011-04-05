using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.CompilerServices;
using mshtml;

namespace EditableWebBrowser.EWBInterop
{
    class win32
    {
        //win32 functions

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;

        public const int WM_SETFOCUS = 0x7;
        public const int WM_MOUSEACTIVATE = 0x21;
        public const int WM_PARENTNOTIFY = 0x210;
        public const int WM_ACTIVATE = 0x6;
        public const int WM_KILLFOCUS = 0x8;
        public const int WM_CLOSE = 0x10;
        public const int WM_DESTROY = 0x2;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_KEYFIRST = 0x0100;
        public const int WM_KEYLAST = 0x0109;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_NEXTDLGCTL = 0x0028; // see also GetNextDlgTabItem
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        public const int WM_XBUTTONDOWN = 0x020B;
        public const int WM_XBUTTONUP = 0x020C;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_MOUSELEAVE = 0x02A3;
        public const int WM_MOUSEHOVER = 0x02A1;

        public const int WS_TABSTOP = 0x00010000;
        public const int MK_LBUTTON = 0x0001;
        public const int MK_RBUTTON = 0x0002;
        public const int MK_SHIFT = 0x0004;
        public const int MK_CONTROL = 0x0008;
        public const int MK_MBUTTON = 0x0010;
        public const int MK_XBUTTON1 = 0x0020;
        public const int MK_XBUTTON2 = 0x0040;

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern Boolean GetClientRect(IntPtr hWnd, [In, Out] RECT rect);

        [DllImport("User32.dll")]
        public static extern int GetMessageTime();

        [DllImport("User32.dll")]
        public static extern int GetMessagePos();

        [DllImport("User32.dll")]
        public static extern int GetWindowLong([In] IntPtr hWnd, [In] int nIndex);

        [DllImport("User32.dll")]
        public static extern IntPtr GetTopWindow([In] IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern Boolean IsWindowVisible([In] IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern Boolean IsWindowEnabled([In] IntPtr hWnd);

        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern void CreateStreamOnHGlobal([In] IntPtr hGlobal,
            [In] int fDeleteOnRelease, [Out] out IStream pStream);

        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern void GetHGlobalFromStream(IStream pStream, [Out] out IntPtr pHGlobal);

        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool GlobalUnlock(IntPtr handle);


        [DllImport("ole32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CreateBindCtx(int dwReserved, [Out] out IBindCtx ppbc);

        [DllImport("urlmon.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int CreateURLMoniker(IMoniker pmkContext, String szURL, [Out]
                        out IMoniker ppmk);

        [DllImport("ole32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int OleRun(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnknown
            );

        [DllImport("ole32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int OleLockRunning(
        [In, MarshalAs(UnmanagedType.IUnknown)] object pUnknown,
        [In, MarshalAs(UnmanagedType.Bool)] bool flock,
        [In, MarshalAs(UnmanagedType.Bool)] bool fLastUnlockCloses
        );

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd,      // handle to destination window
            uint Msg,     // message
            int wParam,  // first message parameter
            int lParam   // second message parameter
            );

        [DllImport("user32.Dll")]
        public static extern IntPtr PostMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            int lParam);

        [DllImport("user32.Dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.Dll")]
        public static extern IntPtr GetWindow([In] IntPtr hWnd, [In] uint wCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

    }

    public struct HRESULT
    {
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const int E_NOTIMPL = unchecked((int)0x80004001);
        public const int E_INVALIDARG = unchecked((int)0x80070057);
        public const int E_NOINTERFACE = unchecked((int)0x80004002);
        public const int E_FAIL = unchecked((int)0x80004005);
        public const int E_UNEXPECTED = unchecked((int)0x8000FFFF);
    }

    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    public class RECT
    {
        public int left = 0;
        public int top = 0;
        public int right = 0;
        public int bottom = 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        public IntPtr hwnd;
        public int message;
        public IntPtr wParam;
        public IntPtr lParam;
        public int time;
        public int pt_x;
        public int pt_y;
    }


}