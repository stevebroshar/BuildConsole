using System;
using System.Windows.Forms;

namespace BuildConsole
{
	public class Preferences
	{
		private readonly string APP_ROOT_KEY = "Software\\Software Success\\BuildConsole";
        
		public Microsoft.Win32.RegistryKey OpenKeyForSaving()
		{
			return Microsoft.Win32.Registry.CurrentUser.CreateSubKey(APP_ROOT_KEY);
		}

		public Microsoft.Win32.RegistryKey OpenKeyForLoading()
		{
			return Microsoft.Win32.Registry.CurrentUser.OpenSubKey(APP_ROOT_KEY);
		}

		private const string APP_TITLE = "Build Console";

		// Shows a message box to the user with application name as the window
		// title and returns the message box result.
		//
		public DialogResult ShowMsgBox(string Message, MessageBoxButtons Buttons, MessageBoxIcon Icon)
		{
			return MessageBox.Show(null, Message, APP_TITLE, Buttons, Icon);
		}

		// Shows a question message box and returns the MsgBox result.
		//
		public bool PromptQuestion(string Message)
		{
			return ShowMsgBox(Message, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
		}
		public DialogResult PromptQuestion(string Message, MessageBoxButtons Buttons)
		{
			return ShowMsgBox(Message, Buttons, MessageBoxIcon.Question);
		}

		// Shows an error message box with an OK button.
		//
		public void ShowError(string Message)
		{
			ShowMsgBox(Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
 
		// Shows a warning message box with an OK button.
		//
		public void ShowWarning(string Message)
		{
			ShowMsgBox(Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		// Shows a warning message box with an OK button and a Cancel button.
		// Returns True if the user selects OK or False if the user selects Cancel.
		//
		public bool PromptWarning(string Message)
		{
			return ShowMsgBox(Message, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;
		}
 
		// Shows an informational message box with an OK button.
		//
		public void ShowInformation(string Message)
		{
			ShowMsgBox(Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
