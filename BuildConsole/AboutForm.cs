using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BuildConsole
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.PictureBox AppIcon;
		private System.Windows.Forms.Label AppNameLabel;
		private System.Windows.Forms.Label VersionLabel;
		private System.Windows.Forms.Label CopyrightLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.OkButton = new System.Windows.Forms.Button();
			this.AppIcon = new System.Windows.Forms.PictureBox();
			this.AppNameLabel = new System.Windows.Forms.Label();
			this.VersionLabel = new System.Windows.Forms.Label();
			this.CopyrightLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OkButton
			// 
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.OkButton.Location = new System.Drawing.Point(120, 120);
			this.OkButton.Name = "OkButton";
			this.OkButton.TabIndex = 0;
			this.OkButton.Text = "&OK";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// AppIcon
			// 
			this.AppIcon.Image = ((System.Drawing.Image)(resources.GetObject("AppIcon.Image")));
			this.AppIcon.Location = new System.Drawing.Point(16, 16);
			this.AppIcon.Name = "AppIcon";
			this.AppIcon.Size = new System.Drawing.Size(56, 56);
			this.AppIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.AppIcon.TabIndex = 1;
			this.AppIcon.TabStop = false;
			// 
			// AppNameLabel
			// 
			this.AppNameLabel.AutoSize = true;
			this.AppNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AppNameLabel.Location = new System.Drawing.Point(88, 16);
			this.AppNameLabel.Name = "AppNameLabel";
			this.AppNameLabel.Size = new System.Drawing.Size(109, 25);
			this.AppNameLabel.TabIndex = 2;
			this.AppNameLabel.Text = "<appname>";
			// 
			// VersionLabel
			// 
			this.VersionLabel.AutoSize = true;
			this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.VersionLabel.Location = new System.Drawing.Point(88, 48);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(78, 22);
			this.VersionLabel.TabIndex = 3;
			this.VersionLabel.Text = "<version>";
			// 
			// CopyrightLabel
			// 
			this.CopyrightLabel.AutoSize = true;
			this.CopyrightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CopyrightLabel.Location = new System.Drawing.Point(88, 80);
			this.CopyrightLabel.Name = "CopyrightLabel";
			this.CopyrightLabel.Size = new System.Drawing.Size(63, 16);
			this.CopyrightLabel.TabIndex = 4;
			this.CopyrightLabel.Text = "<copyright>";
			// 
			// AboutForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.OkButton;
			this.ClientSize = new System.Drawing.Size(306, 152);
			this.Controls.Add(this.CopyrightLabel);
			this.Controls.Add(this.VersionLabel);
			this.Controls.Add(this.AppNameLabel);
			this.Controls.Add(this.AppIcon);
			this.Controls.Add(this.OkButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void OkButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process ThisProcess = System.Diagnostics.Process.GetCurrentProcess();
			System.Diagnostics.FileVersionInfo Info = System.Diagnostics.FileVersionInfo.GetVersionInfo(ThisProcess.MainModule.FileName);
			AppNameLabel.Text = Info.FileDescription;
			VersionLabel.Text = "Version " + Info.FileVersion;
			CopyrightLabel.Text = Info.LegalCopyright;
		}
	}
}
