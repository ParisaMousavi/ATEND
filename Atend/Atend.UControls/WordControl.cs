using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Atend.UControls
{
    public partial class WordControl : UserControl
    {
        public WordControl()
        {
            InitializeComponent();
        }

        private void WordControl_Load(object sender, EventArgs e)
        {
        }



        #region "API usage declarations"

        [DllImport("user32.dll")]
        public static extern int FindWindow(string strclassName, string strWindowName);

        [DllImport("user32.dll")]
        static extern int SetParent(int hWndChild, int hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
            int hWnd,               // handle to window
            int hWndInsertAfter,    // placement-order handle
            int X,                  // horizontal position
            int Y,                  // vertical position
            int cx,                 // width
            int cy,                 // height
            uint uFlags             // window-positioning options
            );

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        static extern bool MoveWindow(
            int hWnd,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            bool bRepaint
            );

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        static extern Int32 DrawMenuBar(
            Int32 hWnd
            );

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        static extern Int32 GetMenuItemCount(
            Int32 hMenu
            );

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern Int32 GetSystemMenu(
            Int32 hWnd,
            bool bRevert
            );

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern Int32 RemoveMenu(
            Int32 hMenu,
            Int32 nPosition,
            Int32 wFlags
            );


        private const int MF_BYPOSITION = 0x400;
        private const int MF_REMOVE = 0x1000;


        const int SWP_DRAWFRAME = 0x20;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOSIZE = 0x1;
        const int SWP_NOZORDER = 0x4;

        #endregion


        public Word.Document document;
        public static Word.ApplicationClass wd = null;
        public static int wordWnd = 0;
        public static string filename = null;
        private static bool deactivateevents = false;

        private System.ComponentModel.Container components = null;

        public void PreActivate()
        {
            if (wd == null) wd = new Word.ApplicationClass();
        }

        public void CloseControl()
        {
            try
            {
                deactivateevents = true;
                object dummy = null;
                object dummy2 = (object)false;
                document.Close(ref dummy, ref dummy, ref dummy);
                // Change the line below.
                wd.Quit(ref dummy2, ref dummy, ref dummy);
                deactivateevents = false;
            }
            catch (Exception ex)
            {
                String strErr = ex.Message;
            }
        }

        private void OnClose(Word.Document doc, ref bool cancel)
        {
            if (!deactivateevents)
            {
                cancel = true;
            }
        }

        private void OnOpenDoc(Word.Document doc)
        {
            OnNewDoc(doc);
        }

        private void OnNewDoc(Word.Document doc)
        {
            if (!deactivateevents)
            {
                deactivateevents = true;
                object dummy = null;
                doc.Close(ref dummy, ref dummy, ref dummy);
                deactivateevents = false;
            }
        }

        private void OnQuit()
        {
            //wd=null;
        }

        public void LoadDocument(string t_filename)
        {
            deactivateevents = true;
            filename = t_filename;

            if (wd == null) wd = new Word.ApplicationClass();
            try
            {
                wd.CommandBars.AdaptiveMenus = false;
                wd.DocumentBeforeClose += new Word.ApplicationEvents2_DocumentBeforeCloseEventHandler(OnClose);
                wd.NewDocument += new Word.ApplicationEvents2_NewDocumentEventHandler(OnNewDoc);
                wd.DocumentOpen += new Word.ApplicationEvents2_DocumentOpenEventHandler(OnOpenDoc);
                wd.ApplicationEvents2_Event_Quit += new Word.ApplicationEvents2_QuitEventHandler(OnQuit);

            }
            catch { }

            if (document != null)
            {
                try
                {
                    object dummy = null;
                    wd.Documents.Close(ref dummy, ref dummy, ref dummy);
                }
                catch { }
            }

            if (wordWnd == 0) wordWnd = FindWindow("Opusapp", null);
            if (wordWnd != 0)
            {
                SetParent(wordWnd, this.Handle.ToInt32());

                object fileName = filename;
                object newTemplate = false;
                object docType = 0;
                object readOnly = true;
                object isVisible = true;
                object missing = System.Reflection.Missing.Value;

                try
                {
                    if (wd == null)
                    {
                        throw new WordInstanceException();
                    }

                    if (wd.Documents == null)
                    {
                        throw new DocumentInstanceException();
                    }

                    if (wd != null && wd.Documents != null)
                    {
                        document = wd.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
                    }

                    if (document == null)
                    {
                        throw new ValidDocumentException();
                    }
                }
                catch
                {
                }

                try
                {
                    wd.ActiveWindow.DisplayRightRuler = false;
                    wd.ActiveWindow.DisplayScreenTips = false;
                    wd.ActiveWindow.DisplayVerticalRuler = false;
                    wd.ActiveWindow.DisplayRightRuler = false;
                    wd.ActiveWindow.ActivePane.DisplayRulers = false;
                    wd.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdWebView;
                    //wd.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdPrintView;//wdWebView; // .wdNormalView;
                }
                catch
                {

                }


                int counter = wd.ActiveWindow.Application.CommandBars.Count;
                for (int i = 1; i <= counter; i++)
                {
                    try
                    {

                        String nm = wd.ActiveWindow.Application.CommandBars[i].Name;
                        if (nm == "Standard")
                        {
                            int count_control = wd.ActiveWindow.Application.CommandBars[i].Controls.Count;
                            for (int j = 1; j <= 2; j++)
                            {
                                wd.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled = false;

                            }
                        }

                        if (nm == "Menu Bar")
                        {
                            wd.ActiveWindow.Application.CommandBars[i].Enabled = false;

                        }

                        nm = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }



                // Show the word-document
                try
                {
                    wd.Visible = true;
                    wd.Activate();

                    SetWindowPos(wordWnd, this.Handle.ToInt32(), 0, 0, this.Bounds.Width, this.Bounds.Height, SWP_NOZORDER | SWP_NOMOVE | SWP_DRAWFRAME | SWP_NOSIZE);

                    //Call onresize--I dont want to write the same lines twice
                    OnResize();
                }
                catch
                {
                    MessageBox.Show("Error: do not load the document into the control until the parent window is shown!");
                }

                /// We want to remove the system menu also. The title bar is not visible, but we want to avoid accidental minimize, maximize, etc ..by disabling the system menu(Alt+Space)
                try
                {
                    int hMenu = GetSystemMenu(wordWnd, false);
                    if (hMenu > 0)
                    {
                        int menuItemCount = GetMenuItemCount(hMenu);
                        RemoveMenu(hMenu, menuItemCount - 1, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 2, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 3, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 4, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 5, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 6, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 7, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 8, MF_REMOVE | MF_BYPOSITION);
                        DrawMenuBar(wordWnd);
                    }
                }
                catch { };



                this.Parent.Focus();

            }
            deactivateevents = false;
        }

        public void RestoreWord()
        {
            try
            {
                int counter = wd.ActiveWindow.Application.CommandBars.Count;
                for (int i = 0; i < counter; i++)
                {
                    try
                    {
                        wd.ActiveWindow.Application.CommandBars[i].Enabled = true;
                    }
                    catch
                    {

                    }
                }
            }
            catch { };

        }

        private void OnResize()
        {
            int borderWidth = SystemInformation.Border3DSize.Width;
            int borderHeight = SystemInformation.Border3DSize.Height;
            int captionHeight = SystemInformation.CaptionHeight;
            int statusHeight = SystemInformation.ToolWindowCaptionHeight;
            MoveWindow(
                wordWnd,
                -2 * borderWidth,
                -2 * borderHeight - captionHeight,
                this.Bounds.Width + 4 * borderWidth,
                this.Bounds.Height + captionHeight + 4 * borderHeight + statusHeight,
                true);

        }

        private void OnResize(object sender, System.EventArgs e)
        {
            OnResize();
        }

        public void RestoreCommandBars()
        {
            try
            {
                int counter = wd.ActiveWindow.Application.CommandBars.Count;
                for (int i = 1; i <= counter; i++)
                {
                    try
                    {

                        String nm = wd.ActiveWindow.Application.CommandBars[i].Name;
                        if (nm == "Standard")
                        {
                            int count_control = wd.ActiveWindow.Application.CommandBars[i].Controls.Count;
                            for (int j = 1; j <= 2; j++)
                            {
                                wd.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled = true;
                            }
                        }
                        if (nm == "Menu Bar")
                        {
                            wd.ActiveWindow.Application.CommandBars[i].Enabled = true;
                        }
                        nm = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch { }

        }

        private void WinWordControl_Load(object sender, EventArgs e)
        {

        }

    }

    public class DocumentInstanceException : Exception
    { }

    public class ValidDocumentException : Exception
    { }

    public class WordInstanceException : Exception
    { }

}
