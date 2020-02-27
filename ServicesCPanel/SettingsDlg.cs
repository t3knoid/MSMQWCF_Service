using ActiveDirectoryObjectPicker;
using LSALib;
using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Windows.Forms;

namespace ServicesCPanel
{
    public partial class SettingsDlg : Form
    {
        private bool settingschanged = false;

        public string StartupType { get; set; }
        public string LogonAs { get; set; }
        public string Password { get; set; }
        public bool InteractWithDesktop { get; set; }

        bool SettingsChanged
        {
            get { return settingschanged; }
            set
            {
                settingschanged = value;
                if (settingschanged == true)
                {
                    btCancel.Enabled = true;
                    btApply.Enabled = true;
                    btOK.Enabled = true;
                }
                else
                {
                    btCancel.Enabled = true;
                    btApply.Enabled = false;
                    btOK.Enabled = false;
                }
            }
        }
            
        public SettingsDlg()
        {
            InitializeComponent();
        }

        private bool CheckCredentials(string username, string password, ContextType contexttype = ContextType.Domain)
        {
            bool valid = false;

            using (PrincipalContext context = new PrincipalContext(contexttype))
            {
                valid = context.ValidateCredentials(username, password);
            }

            return valid;
        }

        private bool AddLogonAsServiceRight()
        {
            bool privadded = false;

            long result = LsaUtility.SetRight(tbUsername.Text, "SeServiceLogonRight");
            if (result == 0)
            {
                MessageBox.Show("Privilege added");
                privadded = true;
            }
            else
            {
                MessageBox.Show("Privilege not added: +winErrorCode");
                privadded = false;
            }
            return privadded;
        }
        private void btBrowse_Click(object sender, EventArgs e)
        {
            DirectoryObjectPickerDialog picker = new DirectoryObjectPickerDialog();
            picker.AllowedObjectTypes = ObjectTypes.Users;
            picker.DefaultObjectTypes = ObjectTypes.Users;
            picker.AllowedLocations = Locations.All;
            picker.DefaultLocations = Locations.EnterpriseDomain;
            picker.MultiSelect = false;
            picker.TargetComputer = "";
            DialogResult dialogResult = picker.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DirectoryObject[] results;
                results = picker.SelectedObjects;
                if (String.IsNullOrEmpty(results[0].Upn.Trim()))
                {
                    tbUsername.Text = @".\" + results[0].Name;
                }
                else
                {
                    tbUsername.Text = results[0].Upn;
                }
                
            }
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            ApplyPropertyValues();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ApplyPropertyValues()
        {
            if (rbLocalSystemAccount.Checked)
            {
                LogonAs = rbLocalSystemAccount.Text;
            }
            else
            {
                LogonAs = tbUsername.Text;
            }
            StartupType = comboBoxStartupType.Text;
            InteractWithDesktop = cbInteractWithDesktop.Checked;
            Password = tbPassword.Text;
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            ApplyPropertyValues();
            btApply.Enabled = false;
        }

        private void rbThisAccount_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged = true;
            if (rbThisAccount.Checked)
            {
                tbUsername.Enabled = true;
                btBrowse.Enabled = true;
                tbPassword.Enabled = true;
                tbConfirmPassword.Enabled = true;
                lbPassword.Enabled = true;
                lbConfirmPassword.Enabled = true;
            }
            else
            {
                tbUsername.Enabled = false;
                btBrowse.Enabled = false;
                tbPassword.Enabled = false;
                tbConfirmPassword.Enabled = false;
                lbPassword.Enabled = false;
                lbConfirmPassword.Enabled = false;
            }
        }

        private void comboBoxStartupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsChanged = true;
        }

        private void cbInteractWithDesktop_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged = true;
        }

        private void SettingsDlg_Load(object sender, EventArgs e)
        {
            btCancel.Enabled = true;
            btOK.Enabled = false;
            btApply.Enabled = false;

            if (LogonAs == rbLocalSystemAccount.Text)
            {
                rbLocalSystemAccount.Checked = true;
                rbThisAccount.Checked = false;
            }
            else
            {
                rbLocalSystemAccount.Checked = false;
                rbThisAccount.Checked = true;
                tbUsername.Text = LogonAs;
            }
            comboBoxStartupType.Text = StartupType;
            cbInteractWithDesktop.Checked = InteractWithDesktop;
            tbPassword.Text = Password;
            tbConfirmPassword.Text = Password;
        }
    }
}


