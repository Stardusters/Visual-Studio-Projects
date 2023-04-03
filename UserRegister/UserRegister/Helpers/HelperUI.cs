using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System;
using System.Linq;

namespace UserRegister.Helpers
{
    class HelperUI
    {
        public static void ShowDAlert(string _content, string _caption = "Update Successful")
        {
            RadDesktopAlert _da = new RadDesktopAlert()
            {
                CaptionText = _caption,
                ContentText = _content,
                ThemeName = "Office2019Dark",
                AutoCloseDelay = 4,
                FadeAnimationType = FadeAnimationType.None,
                PopupAnimation = true,
                PopupAnimationDirection = RadDirection.Up,
                PopupAnimationEasing = RadEasingType.InOutBack,
                Opacity = 1,
                ShowPinButton = false,
                ShowOptionsButton = false,
            };
            _da.Popup.AlertElement.ContentElement.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            _da.Popup.AlertElement.CaptionElement.TextAndButtonsElement.TextElement.Font = new Font("Segoe UI", 12f, FontStyle.Regular);
            _da.Show();
        }
        public static void ErrorMsg(string _content, string _caption = "Error")
        {
            ShowMsg(_content, _caption, "", RadMessageIcon.Error);
        }
        public static void ErrorMsgWithDetail(string _content, string _caption = "Error", string _detail = "No Details")
        {
            ShowMsg(_content, _caption, _detail, RadMessageIcon.Error);
        }
        public static DialogResult WarningMsg(string _content, string _caption = "Warning", MessageBoxButtons button = MessageBoxButtons.OK)
        {
            return ShowMsg(_content, _caption, "", RadMessageIcon.Exclamation, button);
        }
        public static DialogResult InfoMsg(string _content, string _caption = "Info")
        {
            return ShowMsg(_content, _caption, "", RadMessageIcon.Info);
        }
        private static Bitmap GetRadMessageIcon(RadMessageIcon icon)
        {
            switch (icon)
            {
                case RadMessageIcon.Info:
                    return GetBitmap("Telerik.WinControls.UI.Resources.RadMessageBox.MessageInfo.png");
                case RadMessageIcon.Question:
                    return GetBitmap("Telerik.WinControls.UI.Resources.RadMessageBox.MessageQuestion.png");
                case RadMessageIcon.Exclamation:
                    return GetBitmap("Telerik.WinControls.UI.Resources.RadMessageBox.MessageExclamation.png");
                case RadMessageIcon.Error:
                    return GetBitmap("Telerik.WinControls.UI.Resources.RadMessageBox.MessageError.png");
            }
            return null;
        }
        private static Bitmap GetBitmap(string name)
        {
            Bitmap image;
            using (var stream = (System.Reflection.Assembly.GetAssembly(typeof(RadMessageBox)).GetManifestResourceStream(name)))
                image = Bitmap.FromStream(stream) as Bitmap;
            return image;
        }
        private static DialogResult ShowMsg(string content, string caption, string details = "", RadMessageIcon icon = RadMessageIcon.Info, MessageBoxButtons button = MessageBoxButtons.OK)
        {
            var _frm = CreateBaseMessageBox;
            _frm.MessageText = content;
            _frm.Text = caption;
            if (!string.IsNullOrWhiteSpace(details))
            {
                _frm.DetailsText = details;
            }
            _frm.MessageIcon = GetRadMessageIcon(icon);
            _frm.ShowIcon = false;

            _frm.ButtonsConfiguration = button;
            _frm.ButtonSize = new Size(100, 50);
            return _frm.ShowDialog();
        }
        public static bool IsDialogYes(string _question, string caption = "Warning")
        {
            var _frm = CreateBaseMessageBox;
            _frm.Text = caption;
            _frm.ShowIcon = false;
            _frm.MessageText = _question;
            _frm.MessageIcon = GetRadMessageIcon(RadMessageIcon.Exclamation);
            _frm.ButtonsConfiguration = MessageBoxButtons.YesNo;
            return _frm.ShowDialog() == DialogResult.Yes;
        }
        public static RadMessageBoxForm CreateBaseMessageBox
        {
            get
            {
                var form = new RadMessageBoxForm()
                {
                    AutoSize = true,
                    TopMost = true,
                    ThemeName = "Office2019Dark",
                    StartPosition = FormStartPosition.CenterScreen,
                };
                form.ControlBox = false;
                form.ButtonSize = new Size(120, 60);
                form.FormElement.AutoSize = true;
                form.FormElement.Size = new Size(640, 360);
                form.FormElement.TitleBar.Font = new Font("Segoe UI", 20f);
                form.Controls["radLabel1"].Font = new Font("Segoe UI", 18f);
                form.Controls.OfType<RadButton>().ToList().ForEach(x => x.ButtonElement.Font = new Font("Segoe UI", 16f));

                return form;
            }
        }
    }
}
