using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Telerik.WinControls.UI;

using UserRegister.Helpers;
using UserRegister.Model;

namespace UserRegister.UI
{
    public partial class frmMain : RadForm
    {
        PersonalDBDataContext PersonalDB = new PersonalDBDataContext(GlobalVariables.sqlConnStrPersonal);

        List<User> lstUser = new List<User>();

        public frmMain()
        {
            InitializeComponent();
            this.lstUser = PersonalDB.Users.Where(x => x.Status == true).ToList();
        }
    }
}
