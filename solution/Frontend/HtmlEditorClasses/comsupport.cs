using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Needed classes for com integration of mshtml which are not in onlyconnect.HtmlEditor
/// </summary>
namespace Frontend.HtmlEditorClasses
{
    public struct BUTTON
    {
        public static int NONE = 0;
        public static int LEFT = 1;
        public static int RIGHT = 2;
        public static int LEFT_RIGHT = 3;
        public static int MIDDLE = 4;
        public static int LEFT_MIDDLE = 5;
        public static int RIGHT_MIDDLE = 6;
        public static int LEFT_MIDDLE_RIGHT = 7;
    }

    public struct BSTR
    {
        public static string beforeBegin = "beforeBegin";
        public static string afterBegin = "afterBegin";
        public static string beforeEnd = "beforeEnd";
        public static string afterEnd = "afterEnd";
    }
}
