using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace onlyconnect
{
    public delegate void DragEnterHandler(DataObject sender, DragEventArgs e);
    public delegate void DropHandler(DataObject sender, DragEventArgs e);
    public delegate void DragOverHandler(DataObject sender, DragEventArgs e);

    public class DropTarget : IOleDropTarget
    {
        public event DragEnterHandler dragEnter;
        public event DragOverHandler dragOver;
        public event DropHandler drop;

        HtmlEditor container;

        public DropTarget(HtmlEditor container)
        {
            this.container = container;
        }

        public int OleDragEnter(IntPtr pDataObj, int grfKeyState, tagPOINT pt, ref int pdwEffect)
        {
            DataObject theDataObject;
            try
            {
                DataObject theObject = (DataObject)Marshal.GetObjectForIUnknown(pDataObj);
                theDataObject = new DataObject(theObject);
                this.dragEnter(theDataObject, (new DragEventArgs(null, 0, pt.x, pt.y, DragDropEffects.All, DragDropEffects.All)));
                return HRESULT.S_OK;
            }
            catch (Exception)
            {
                return HRESULT.E_FAIL;
            }
        }

        public int OleDragOver(int grfKeyState, tagPOINT pt, ref int pdwEffect)
        {
            this.dragOver(null, (new DragEventArgs(null, 0, pt.x, pt.y, DragDropEffects.All, DragDropEffects.All)));
            return HRESULT.S_OK;
        }

        public int OleDragLeave()
        {
            return HRESULT.S_OK;
        }

        public int OleDrop(IntPtr pDataObj, int grfKeyState, tagPOINT pt, ref int pdwEffect)
        {
            DataObject theDataObject;
            DataObject theObject = (DataObject)Marshal.GetObjectForIUnknown(pDataObj);
            theDataObject = new DataObject(theObject);
            this.drop(theDataObject, (new DragEventArgs(null, 0, pt.x, pt.y, DragDropEffects.All, DragDropEffects.All)));
            return HRESULT.S_OK;
        }
    }
}
