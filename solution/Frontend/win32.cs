using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.CompilerServices;
using mshtml;

namespace Frontend
{
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

    public enum ELEMENT_CORNER
    {
        ELEMENT_CORNER_NONE = 0,
        ELEMENT_CORNER_TOP = 1,
        ELEMENT_CORNER_LEFT = 2,
        ELEMENT_CORNER_BOTTOM = 3,
        ELEMENT_CORNER_RIGHT = 4,
        ELEMENT_CORNER_TOPLEFT = 5,
        ELEMENT_CORNER_TOPRIGHT = 6,
        ELEMENT_CORNER_BOTTOMLEFT = 7,
        ELEMENT_CORNER_BOTTOMRIGHT = 8,
        ELEMENT_CORNER_Max = 2147483647
    }

}