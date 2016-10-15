using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BuildConsole
{
	/// <summary>
	/// Summary description for FindForm.
	/// </summary>
	public class FindForm : System.Windows.Forms.Form
	{
		private bool m_SelectText = false;
		private System.Windows.Forms.RichTextBox m_Target;
		private System.Windows.Forms.Button FindButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.CheckBox MatchCaseEdit;
		private System.Windows.Forms.TextBox TextEdit;
		private System.Windows.Forms.CheckBox MatchWholeEdit;
		private System.Windows.Forms.CheckBox UpEdit;
		private System.Windows.Forms.Label WhatLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FindForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			EnableControls();
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
			this.WhatLabel = new System.Windows.Forms.Label();
			this.TextEdit = new System.Windows.Forms.TextBox();
			this.FindButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.MatchCaseEdit = new System.Windows.Forms.CheckBox();
			this.MatchWholeEdit = new System.Windows.Forms.CheckBox();
			this.UpEdit = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// WhatLabel
			// 
			this.WhatLabel.AutoSize = true;
			this.WhatLabel.Location = new System.Drawing.Point(8, 8);
			this.WhatLabel.Name = "WhatLabel";
			this.WhatLabel.Size = new System.Drawing.Size(56, 16);
			this.WhatLabel.TabIndex = 0;
			this.WhatLabel.Text = "Find what:";
			// 
			// TextEdit
			// 
			this.TextEdit.Location = new System.Drawing.Point(88, 8);
			this.TextEdit.Name = "TextEdit";
			this.TextEdit.Size = new System.Drawing.Size(280, 20);
			this.TextEdit.TabIndex = 1;
			this.TextEdit.Text = "";
			this.TextEdit.TextChanged += new System.EventHandler(this.TextEdit_TextChanged);
			// 
			// FindButton
			// 
			this.FindButton.Location = new System.Drawing.Point(288, 40);
			this.FindButton.Name = "FindButton";
			this.FindButton.TabIndex = 5;
			this.FindButton.Text = "&Find Next";
			this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Location = new System.Drawing.Point(288, 72);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.TabIndex = 6;
			this.CloseButton.Text = "Close";
			// 
			// MatchCaseEdit
			// 
			this.MatchCaseEdit.Location = new System.Drawing.Point(16, 32);
			this.MatchCaseEdit.Name = "MatchCaseEdit";
			this.MatchCaseEdit.Size = new System.Drawing.Size(104, 16);
			this.MatchCaseEdit.TabIndex = 2;
			this.MatchCaseEdit.Text = "Match &case";
			// 
			// MatchWholeEdit
			// 
			this.MatchWholeEdit.Location = new System.Drawing.Point(16, 56);
			this.MatchWholeEdit.Name = "MatchWholeEdit";
			this.MatchWholeEdit.Size = new System.Drawing.Size(128, 16);
			this.MatchWholeEdit.TabIndex = 3;
			this.MatchWholeEdit.Text = "Match &whole word";
			// 
			// UpEdit
			// 
			this.UpEdit.Location = new System.Drawing.Point(16, 80);
			this.UpEdit.Name = "UpEdit";
			this.UpEdit.Size = new System.Drawing.Size(104, 16);
			this.UpEdit.TabIndex = 4;
			this.UpEdit.Text = "Search &up";
			// 
			// FindForm
			// 
			this.AcceptButton = this.FindButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CloseButton;
			this.ClientSize = new System.Drawing.Size(378, 104);
			this.ControlBox = false;
			this.Controls.Add(this.UpEdit);
			this.Controls.Add(this.MatchWholeEdit);
			this.Controls.Add(this.MatchCaseEdit);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.FindButton);
			this.Controls.Add(this.TextEdit);
			this.Controls.Add(this.WhatLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FindForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find";
			this.Activated += new System.EventHandler(this.FindForm_Activated);
			this.ResumeLayout(false);

		}
		#endregion

		private void ShowError(string Message)
		{
			MessageBox.Show(this, Message, "Build Console", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void FindForm_Activated(object sender, System.EventArgs e)
		{
			if (m_SelectText)
			{
				m_SelectText = false;
				TextEdit.SelectAll();
				TextEdit.Focus();
			}
		}

		private void FindButton_Click(object sender, System.EventArgs e)
		{
			TextEdit.SelectAll();
			TextEdit.Focus();
			FindNext(m_Target);
		}

		private void TextEdit_TextChanged(object sender, System.EventArgs e)
		{
			EnableControls();
		}

		private void EnableControls()
		{
			FindButton.Enabled = TextEdit.Text.Length > 0;
		}

		public void Find(System.Windows.Forms.RichTextBox Target)
		{
			m_Target = Target;
			m_SelectText = true;
			this.ShowDialog();
		}

		public void FindNext(System.Windows.Forms.RichTextBox Target)
		{
			if (Target.Text.Length < 1)
				return;

			string SearchText = TextEdit.Text;
			int StartPos = Target.SelectionStart;
			int EndPos = Target.Text.Length;
			System.Windows.Forms.RichTextBoxFinds Options = 0;

			if (MatchCaseEdit.Checked)
				Options |= System.Windows.Forms.RichTextBoxFinds.MatchCase;
			if (MatchWholeEdit.Checked)
				Options |= System.Windows.Forms.RichTextBoxFinds.WholeWord;
			if (UpEdit.Checked)
			{
				Options |= System.Windows.Forms.RichTextBoxFinds.Reverse;
				EndPos = Target.SelectionStart;
				StartPos = 1;
			}
			else
			{
				StartPos += 1;
				if (StartPos >= Target.Text.Length)
					StartPos = Target.Text.Length - 1;
			}

			int FoundPos = Target.Find(SearchText, StartPos, EndPos, Options);
			if (FoundPos < 0)
			{
				if (UpEdit.Checked)
				{
					StartPos = EndPos;
					EndPos = Target.Text.Length - 1;
				}
				else
				{
					EndPos = StartPos;
					StartPos = 0;
				}
				FoundPos = Target.Find(SearchText, StartPos, EndPos, Options);
				if (FoundPos < 0)
				{
					ShowError("Unable to find '" + SearchText + "'.");
				}
			}
		}

	}
}
