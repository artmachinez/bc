using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Frontend.Helpers;

namespace Frontend.UserControls
{
    /// <summary>
    /// Tabcontrol with all opened projects.
    /// Implements own closetab mechanism.
    /// </summary>
    public partial class ClosableTabControl : TabControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ClosableTabControl()
        {
            InitializeComponent();
            CFormController.Instance.mainTabControl = this;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Rectangle tabArea = GetTabRect(e.Index);
            Font titleFont;
            StringFormat titleFormat;
            Brush titleBrush;
            String title;

            //selected tab
            if (e.Index == this.SelectedIndex)
            {
                //throw away tab focus, because of ugly dotted focusrectangle
                this.Parent.Focus();

                e.Graphics.DrawImage(ProjectResources.active_close_button, this.GetCloseButtonArea(tabArea));// tabArea.X + tabArea.Width - 14, 3, 13, 13);

                title = this.TabPages[e.Index].Text;
                titleBrush = new SolidBrush(Color.Black);
                titleFont = this.Font;
                titleFormat = new StringFormat();
                titleFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(title, titleFont, titleBrush, new PointF(tabArea.X + 1, tabArea.Y), titleFormat);
            }
            //not selected tab
            else
            {
                e.Graphics.DrawImage(ProjectResources.inactive_close_button, this.GetCloseButtonArea(tabArea));// tabArea.X + tabArea.Width - 14, 3, 13, 13);

                title = this.TabPages[e.Index].Text;
                titleBrush = new SolidBrush(Color.Gray);
                titleFont = this.Font;
                titleFormat = new StringFormat();
                titleFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(title, titleFont, titleBrush, new PointF(tabArea.X + 1, tabArea.Y), titleFormat);
            }
        }

        /// <summary>
        /// gets close button rectangle from tab rectangle
        /// </summary>
        /// <param name="tabRectangle"></param>
        /// <returns></returns>
        private RectangleF GetCloseButtonArea(RectangleF tabRectangle)
        {
            return new RectangleF(tabRectangle.X + tabRectangle.Width - 14, 3, 13, 13);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            RectangleF tabTextArea;
            RectangleF closeButtonArea;
            Graphics graphics = this.CreateGraphics();

            for (int index = 0; index < this.TabCount; index++)
            {
                tabTextArea = (RectangleF)this.GetTabRect(index);
                closeButtonArea = this.GetCloseButtonArea(tabTextArea);

                //active+hover button
                if (closeButtonArea.Contains(e.Location) && index == this.SelectedIndex)
                {
                    graphics.DrawImage(ProjectResources.active_hover_close_button, closeButtonArea);
                }
                //inactive+hover button
                else if (closeButtonArea.Contains(e.Location))
                {
                    graphics.DrawImage(ProjectResources.inactive_hover_close_button, closeButtonArea);
                }
                //active button
                else if (index == this.SelectedIndex)
                {
                    graphics.DrawImage(ProjectResources.active_close_button, closeButtonArea);
                }
                //inactive button
                else
                {
                    graphics.DrawImage(ProjectResources.inactive_close_button, closeButtonArea);
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            RectangleF tabTextArea = (RectangleF)this.GetTabRect(this.SelectedIndex);
            RectangleF closeButtonArea = this.GetCloseButtonArea(tabTextArea); //new RectangleF(tabTextArea.X + tabTextArea.Width - 14, 3, 13, 13);
            if (closeButtonArea.Contains(e.Location) && e.Button == MouseButtons.Left )
            {
                if (SaveBeforeExitBox())
                {
                    this.TabPages.RemoveAt(SelectedIndex);
                }
            }
            else
            {
                base.OnMouseClick(e);
            }
        }

        private bool SaveBeforeExitBox()
        {
            return true;
        }



    }
}
