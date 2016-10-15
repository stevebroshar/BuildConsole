using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BuildConsole
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class JobInfoDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox AppEdit;
		private System.Windows.Forms.TextBox ArgEdit;
		private System.Windows.Forms.TextBox HintEdit;
		private System.Windows.Forms.TextBox WorkingDirEdit;
		private System.Windows.Forms.TextBox SuccessStatusEdit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public JobInfoDialog()
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.AppEdit = new System.Windows.Forms.TextBox();
			this.ArgEdit = new System.Windows.Forms.TextBox();
			this.HintEdit = new System.Windows.Forms.TextBox();
			this.WorkingDirEdit = new System.Windows.Forms.TextBox();
			this.SuccessStatusEdit = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Application:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Arguments:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 134);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(85, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "Success Status:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(97, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Working Directory:";
			// 
			// AppEdit
			// 
			this.AppEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.AppEdit.Location = new System.Drawing.Point(8, 24);
			this.AppEdit.Name = "AppEdit";
			this.AppEdit.ReadOnly = true;
			this.AppEdit.Size = new System.Drawing.Size(326, 20);
			this.AppEdit.TabIndex = 1;
			this.AppEdit.Text = "textBox1";
			// 
			// ArgEdit
			// 
			this.ArgEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ArgEdit.Location = new System.Drawing.Point(8, 64);
			this.ArgEdit.Name = "ArgEdit";
			this.ArgEdit.ReadOnly = true;
			this.ArgEdit.Size = new System.Drawing.Size(326, 20);
			this.ArgEdit.TabIndex = 2;
			this.ArgEdit.Text = "textBox2";
			// 
			// HintEdit
			// 
			this.HintEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.HintEdit.Location = new System.Drawing.Point(8, 160);
			this.HintEdit.Multiline = true;
			this.HintEdit.Name = "HintEdit";
			this.HintEdit.ReadOnly = true;
			this.HintEdit.Size = new System.Drawing.Size(326, 64);
			this.HintEdit.TabIndex = 5;
			this.HintEdit.Text = "textBox3";
			// 
			// WorkingDirEdit
			// 
			this.WorkingDirEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.WorkingDirEdit.Location = new System.Drawing.Point(8, 104);
			this.WorkingDirEdit.Name = "WorkingDirEdit";
			this.WorkingDirEdit.ReadOnly = true;
			this.WorkingDirEdit.Size = new System.Drawing.Size(326, 20);
			this.WorkingDirEdit.TabIndex = 3;
			this.WorkingDirEdit.Text = "textBox5";
			// 
			// SuccessStatusEdit
			// 
			this.SuccessStatusEdit.Location = new System.Drawing.Point(96, 131);
			this.SuccessStatusEdit.Name = "SuccessStatusEdit";
			this.SuccessStatusEdit.ReadOnly = true;
			this.SuccessStatusEdit.Size = new System.Drawing.Size(80, 20);
			this.SuccessStatusEdit.TabIndex = 4;
			this.SuccessStatusEdit.Text = "textBox6";
			// 
			// JobInfoDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 230);
			this.Controls.Add(this.WorkingDirEdit);
			this.Controls.Add(this.SuccessStatusEdit);
			this.Controls.Add(this.HintEdit);
			this.Controls.Add(this.ArgEdit);
			this.Controls.Add(this.AppEdit);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(352, 256);
			this.Name = "JobInfoDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Job Description";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		public void LoadInfo(JobDefinition Job)
		{
			this.Text = Job.Caption;
			AppEdit.Text = Job.Application;
			ArgEdit.Text = Job.Arguments;
			HintEdit.Text = Job.Hint;
			SuccessStatusEdit.Text = Convert.ToString(Job.SuccessStatus);
			WorkingDirEdit.Text = Job.WorkingDir;
		}

		public void ClearInfo(long Count)
		{
			this.Text = "(" + Convert.ToString(Count) + " jobs selected)";
			AppEdit.Text = "";
			ArgEdit.Text = "";
			HintEdit.Text = "";
			SuccessStatusEdit.Text = "";
			WorkingDirEdit.Text = "";
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}

	}
}
