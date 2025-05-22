using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GUI
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int msg, int wParam, int lParam);

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "USUARIO")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.White;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "USUARIO";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "CONTRASEÑA")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.White;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "CONTRASEÑA";
                txtpass.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = false;
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            if (txtuser.Text == "1" && txtpass.Text == "1")
            {
                FormWelcome welcome = new FormWelcome();
                gui mainMenu = new gui();
                this.Hide();              
                welcome.ShowDialog();
                mainMenu.Show();
            }
            else
            {
                msgError("contraseña incorrecta /usuario incorrecto");
                txtuser.Focus();
                txtpass.Clear();
            }

            //if (txtuser.Text != "USUARIO")
            //{
            //    if (txtpass.Text != "CONTRASEÑA")
            //    {
            //        this.Hide();
            //        FormWelcome welcome = new FormWelcome();
            //        var validlogin = UserControl.loginuser(txtuser.Text, txtpass.Text);
            //        if (validlogin) 
            //        { gui mainMenu = new gui();

            //            welcome.ShowDialog();
            //            mainMenu.Show(); 


            //    }
            //        else
            //        {
            //            msgError("contraseña incorrecta /usuario incorrecto");
            //            txtuser.Focus();
            //            txtpass.Clear();
                        
            //        }
            //    }
            //    else msgError("por favor ingrese la contraseña");
            //    }
            //    else msgError("Por favor ingrese su usuario");
            }

            private void msgError(string msg)
            {
                lblErrorMessage.Text = "     " + msg;
                lblErrorMessage.Visible = true;
            }

        private void lblErrorMessage_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

