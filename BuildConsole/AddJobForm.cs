using System;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace BuildConsole
{
	/// <summary>
	/// Summary description for AddJobForm.
	/// </summary>
	public class AddJobForm : System.Windows.Forms.Form
	{
		private JobDefinitionSet m_JobDefSet = new JobDefinitionSet();
		private Preferences m_Prefs = null;
		private string m_JobTreeSelections = "";
		private bool m_FirstShow = true;
		private bool m_AllowExpandCollapse = false;

		private System.Windows.Forms.ListView m_QueueList;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.Button TheCancelButton;
        private System.Windows.Forms.MenuItem SelectAllContextItem;
        private System.Windows.Forms.MenuItem ClearAllContextItem;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.Label HintLabel;
		private System.Windows.Forms.Button LoadButton;
		private System.Windows.Forms.ContextMenu TreeViewMenu;
		private System.Windows.Forms.ContextMenu RecentDefMenu;
		private System.Windows.Forms.OpenFileDialog JobDefOpenDialog;
        private System.ComponentModel.IContainer components;

		public AddJobForm(Preferences Preferences)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_Prefs = Preferences;
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
			this.TheCancelButton = new System.Windows.Forms.Button();
			this.TreeViewMenu = new System.Windows.Forms.ContextMenu();
			this.SelectAllContextItem = new System.Windows.Forms.MenuItem();
			this.ClearAllContextItem = new System.Windows.Forms.MenuItem();
			this.AddButton = new System.Windows.Forms.Button();
			this.TreeView = new System.Windows.Forms.TreeView();
			this.HintLabel = new System.Windows.Forms.Label();
			this.LoadButton = new System.Windows.Forms.Button();
			this.RecentDefMenu = new System.Windows.Forms.ContextMenu();
			this.JobDefOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// TheCancelButton
			// 
			this.TheCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TheCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.TheCancelButton.Location = new System.Drawing.Point(184, 368);
			this.TheCancelButton.Name = "TheCancelButton";
			this.TheCancelButton.Size = new System.Drawing.Size(72, 23);
			this.TheCancelButton.TabIndex = 4;
			this.TheCancelButton.Text = "&Close";
			// 
			// TreeViewMenu
			// 
			this.TreeViewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.SelectAllContextItem,
																						 this.ClearAllContextItem});
			// 
			// SelectAllContextItem
			// 
			this.SelectAllContextItem.Index = 0;
			this.SelectAllContextItem.Text = "&Select All";
			this.SelectAllContextItem.Click += new System.EventHandler(this.SelectAllContextItem_Click);
			// 
			// ClearAllContextItem
			// 
			this.ClearAllContextItem.Index = 1;
			this.ClearAllContextItem.Text = "&Clear All";
			this.ClearAllContextItem.Click += new System.EventHandler(this.ClearAllContextItem_Click);
			// 
			// AddButton
			// 
			this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.AddButton.Location = new System.Drawing.Point(96, 368);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(72, 23);
			this.AddButton.TabIndex = 2;
			this.AddButton.Text = "&Add";
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// TreeView
			// 
			this.TreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TreeView.CheckBoxes = true;
			this.TreeView.ContextMenu = this.TreeViewMenu;
			this.TreeView.ImageIndex = -1;
			this.TreeView.Location = new System.Drawing.Point(8, 8);
			this.TreeView.Name = "TreeView";
			this.TreeView.SelectedImageIndex = -1;
			this.TreeView.ShowLines = false;
			this.TreeView.ShowPlusMinus = false;
			this.TreeView.ShowRootLines = false;
			this.TreeView.Size = new System.Drawing.Size(248, 296);
			this.TreeView.TabIndex = 1;
			this.TreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
			this.TreeView.DoubleClick += new System.EventHandler(this.TreeView_DoubleClick);
			this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
			this.TreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeCollapse);
			this.TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
			// 
			// HintLabel
			// 
			this.HintLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.HintLabel.Location = new System.Drawing.Point(8, 312);
			this.HintLabel.Name = "HintLabel";
			this.HintLabel.Size = new System.Drawing.Size(248, 56);
			this.HintLabel.TabIndex = 40;
			// 
			// LoadButton
			// 
			this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LoadButton.ContextMenu = this.RecentDefMenu;
			this.LoadButton.Location = new System.Drawing.Point(8, 368);
			this.LoadButton.Name = "LoadButton";
			this.LoadButton.Size = new System.Drawing.Size(72, 23);
			this.LoadButton.TabIndex = 41;
			this.LoadButton.Text = "&Load";
			this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
			// 
			// JobDefOpenDialog
			// 
			this.JobDefOpenDialog.Filter = "Build Job Definitions (*.buildjobdef)|*.buildjobdef|Any (*.*)|*.*";
			this.JobDefOpenDialog.Title = "Load Job Definitions";
			// 
			// AddJobForm
			// 
			this.AcceptButton = this.AddButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.TheCancelButton;
			this.ClientSize = new System.Drawing.Size(266, 398);
			this.Controls.Add(this.LoadButton);
			this.Controls.Add(this.HintLabel);
			this.Controls.Add(this.TreeView);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.TheCancelButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(274, 180);
			this.Name = "AddJobForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Build Jobs";
			this.Load += new System.EventHandler(this.AddJobForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		// Returns the list of recent job def files.
		//
		private string[] StringArrayFromArrayList(ArrayList Item)
		{
			return (string[])Item.ToArray(typeof(string));
		}

		// Returns the list of recent job def files.
		//
		private ArrayList ArrayListFromStringArray(string[] Item)
		{
			ArrayList Result = new ArrayList();
			Result.AddRange(Item);
			return Result;
		}

		// Serializes the current job tree item selections to a string.
		//
		private string SerializeJobDefSetPrefs()
		{
			// collect node settings
			System.Collections.Specialized.NameValueCollection CheckedNodes = 
				new System.Collections.Specialized.NameValueCollection();
			foreach (System.Windows.Forms.TreeNode Node in TreeView.Nodes)
			{
				CheckedNodes.Add(Node.FullPath, Node.Checked.ToString());
				foreach (System.Windows.Forms.TreeNode Subnode in Node.Nodes)
					CheckedNodes.Add(Subnode.FullPath, Subnode.Checked.ToString());
			}

			// serialize node settings
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			System.Runtime.Serialization.Formatters.Soap.SoapFormatter formatter = 
				new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
			formatter.Serialize(stream, CheckedNodes);
			stream.Position = 0;
			System.IO.StreamReader sr = new System.IO.StreamReader(stream);
			return sr.ReadToEnd();
		}
		
		// Returns a bool based on a persisted checked value.
		//
		private bool CvtToChecked(string Text)
		{
			if (Text != null)
			{
				try
				{
					return Convert.ToBoolean(Text);
				}
				catch (Exception x)
				{
					// this happens when two job tree items have the same caption
					Debug.WriteLine("CvtToChecked: " + x.Message);
				}
			}
			return false;
		}
		
		// Sets the job tree item selections from a serialized settings string.
		//
		private void ApplyJobDefSetPrefs(string Text)
		{
			// deserialize node settings
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
			sw.Write(Text);
			sw.Flush();
			System.Runtime.Serialization.Formatters.Soap.SoapFormatter formatter = 
				new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
			stream.Position = 0;
			System.Collections.Specialized.NameValueCollection CheckedNodes = 
				(System.Collections.Specialized.NameValueCollection)formatter.Deserialize(stream);
			
			// apply node settings
			foreach (System.Windows.Forms.TreeNode Node in TreeView.Nodes)
			{
				string Value = CheckedNodes.Get(Node.FullPath);
				Node.Checked = CvtToChecked(Value);
				foreach (System.Windows.Forms.TreeNode Subnode in Node.Nodes)
				{
					Value = CheckedNodes.Get(Subnode.FullPath);
					Subnode.Checked = CvtToChecked(Value);
				}
			}
		}

		// The registry key name used for the active job-def-set file.
		//
		private string JobDefSetPrefsKeyName()
		{
			return "JobSetPrefs-" + m_JobDefSet.FilePath.ToUpper();
		}

		// Reads the selection settings for the active job-def-set from the
		// registry.
		//
		private string ReadJobDefSetPrefs()
		{
			Microsoft.Win32.RegistryKey Reg = m_Prefs.OpenKeyForLoading();
			string Result = "";
			if (Reg != null)
			{
				try
				{
					object RegValue;
					RegValue = Reg.GetValue(JobDefSetPrefsKeyName());
					if (RegValue != null)
						Result = RegValue.ToString();
				}
				catch (Exception x)
				{
					// ignore error
					System.Diagnostics.Debug.Assert(false);
					System.Diagnostics.Debug.WriteLine(x.ToString());
				}
			}
			return Result;
		}

		// Writes the job tree view selections to the registry associated with
		// the active job-def-set.
		//
		private void WriteJobDefSetPrefs(string Text)
		{
			Microsoft.Win32.RegistryKey Reg = m_Prefs.OpenKeyForSaving();
			Reg.SetValue(JobDefSetPrefsKeyName(), Text);
		}

		// Returns the last-open job-def file path as read from the registry.
		//
		private string ReadLastJobDefFilePath()
		{
			Microsoft.Win32.RegistryKey Reg = m_Prefs.OpenKeyForLoading();
			string Result = "";
			if (Reg != null)
			{
				try
				{
					Result = Reg.GetValue("JobDefFile", "").ToString();
				}
				catch (Exception x)
				{
					Debug.WriteLine("Unable to read JobDefFile preference.  " + x.ToString());
				}
			}
			return Result;
		}

		// Writes the last-open job-def file path to the registry.
		//
		private void WriteLastJobDefFilePath(string FilePath)
		{
			Microsoft.Win32.RegistryKey Reg = m_Prefs.OpenKeyForSaving();
			Reg.SetValue("JobDefFile", FilePath);
		}

		// Returns the array of recent job-def file paths as read from the
		// registry.
		//
		private string[] ReadRecentJobDefFilePaths()
		{
			Microsoft.Win32.RegistryKey Reg = m_Prefs.OpenKeyForLoading();
			string[] Result = new string[0];
			if (Reg != null)
			{
				try
				{
					string Value = Reg.GetValue("RecentJobDefFiles").ToString();
					if (Value.Length > 0)
						Result = Value.Split("|".ToCharArray());
				}
				catch (Exception x)
				{
					Debug.WriteLine("Unable to load RecentJobDefFiles preference.  " + x.ToString());
				}
			}
			return Result;
		}

		// Writes the array of recent job-def file paths to the registry.
		//
		private void WriteRecentJobDefFilePaths(string[] FilePaths)
		{
			Microsoft.Win32.RegistryKey Reg = m_Prefs.OpenKeyForSaving();
			Reg.SetValue("RecentJobDefFiles", String.Join("|", FilePaths));
		}

		// Adds a file path to the list of recent job def file paths in the
		// registry.  Reads the current list from the registry, removes any
		// items matching the new item, adds the new item to the beginning
		// and saves the list back to the registry.
		//
		// LIMITATION
		// If there is more than one instance of this app running, there is a
		// potential race condition in this function.  
		//
		private void AddRecentJobDefFilePath(string FilePath)
		{
			ArrayList FilePaths = ArrayListFromStringArray(ReadRecentJobDefFilePaths());
			FilePath = System.IO.Path.GetFullPath(FilePath);
			string UpperFilePath = FilePath.ToUpper();
			for (int i = 0; i < FilePaths.Count; ++i)
				if (FilePaths[i].ToString().ToUpper() == UpperFilePath)
					FilePaths.RemoveAt(i);
			FilePaths.Insert(0, FilePath);
			if (FilePaths.Count > 8)
				FilePaths.RemoveAt(FilePaths.Count-1);
			WriteRecentJobDefFilePaths(StringArrayFromArrayList(FilePaths));
		}

		// Removes a file path from the list of recent job def file paths in the
		// registry.  Reads the current list from the registry, removes any
		// items matching the item, and saves the list back to the registry.
		//
		// LIMITATION
		// If there is more than one instance of this app running, there is a
		// potential race condition in this function.  
		//
		private void RemoveRecentJobDefFilePath(string FilePath)
		{
			ArrayList FilePaths = ArrayListFromStringArray(ReadRecentJobDefFilePaths());
			string UpperFilePath = FilePath.ToUpper();
			for (int i = 0; i < FilePaths.Count; ++i)
				if (FilePaths[i].ToString().ToUpper() == UpperFilePath)
					FilePaths.RemoveAt(i);
			WriteRecentJobDefFilePaths(StringArrayFromArrayList(FilePaths));
		}

		// Loads the job tree from the loaded job-def-set.
		//
		private void LoadJobTreeItems()
		{
			HintLabel.Text = "";
			TreeView.Nodes.Clear();
			foreach (JobDefinitionGroup JobGroup in m_JobDefSet.Groups)
			{
				TreeNode GroupNode = TreeView.Nodes.Add(JobGroup.Caption);
				GroupNode.ForeColor = System.Drawing.Color.MediumBlue;
				GroupNode.Tag = JobGroup;
				foreach (JobDefinition Job in JobGroup.Items)
				{
					System.Windows.Forms.TreeNode JobNode =	GroupNode.Nodes.Add(Job.Caption);
					JobNode.Tag = Job;
				}
			}
		}

		// Adds a job to the job queue.
		//
		private void QueueJob(JobDefinition Job)
		{
			string WorkingDir = System.IO.Path.Combine
				(System.IO.Path.GetDirectoryName(m_JobDefSet.FilePath), 
				Job.WorkingDir);

			JobDefinition QJob = new JobDefinition();
			QJob.Application = Job.Application;
			QJob.Arguments = Job.Arguments;
			QJob.Caption = m_JobDefSet.Caption + ": " + Job.Caption;
			QJob.Hint = Job.Hint;
			QJob.SuccessStatus = Job.SuccessStatus;
			QJob.WorkingDir = WorkingDir;
			JobInformation QJobInfo = new JobInformation(QJob);

			System.Windows.Forms.ListViewItem Item = m_QueueList.Items.Add(QJob.Caption);
			Item.Tag = QJobInfo;
			Item.SubItems.Add("Pending");
			Item.SubItems.Add(Job.Application + " " + Job.Arguments);
		}

		// Adds a job to the queue for each selected job node.
		//
		private void AddSelectedItemsToQueue()
		{
			foreach (System.Windows.Forms.TreeNode Node in TreeView.Nodes)
			{
				if (Node.Checked)
				{
					foreach (System.Windows.Forms.TreeNode Subnode in Node.Nodes)
					{
						if (Subnode.Checked)
						{
							JobDefinition JobDef = (JobDefinition)Subnode.Tag;
							QueueJob(JobDef);
						}
					}
				}
			}
		}

		// Updates the enables state of controls based on current state.
		//
		private void EnableControls()
		{
			bool AnyBoxesChecked = false;
			bool AnyBoxesUnchecked = false;
			foreach (System.Windows.Forms.TreeNode Node in TreeView.Nodes)
			{
				if (Node.Checked)
				{
					foreach (System.Windows.Forms.TreeNode Subnode in Node.Nodes)
					{
						if (Subnode.Checked) AnyBoxesChecked = true;
						if (!Subnode.Checked) AnyBoxesUnchecked = true;
					}
				}
			}
			ClearAllContextItem.Enabled = AnyBoxesChecked;
			SelectAllContextItem.Enabled = AnyBoxesUnchecked;
			AddButton.Enabled = AnyBoxesChecked;
		}

		// Checks each job node that is visible -- under a checked group node.
		//
		private void SetAllVisibleJobsSelected(bool To)
		{
			foreach (System.Windows.Forms.TreeNode Node in TreeView.Nodes)
			{
				if (Node.Checked)
				{
					foreach (System.Windows.Forms.TreeNode Subnode in Node.Nodes)
						Subnode.Checked = To;
				}
			}
		}

		// Loads a job definition file.
		//
		public void LoadJobDefFile(string FilePath)
		{
			// load job def file into new object since may fail
			JobDefinitionSet NewJobDefSet = new JobDefinitionSet();
			NewJobDefSet.LoadFromFile(FilePath);
			
			// save job tree selections for open job-def file -- if any
			if (m_JobDefSet.FilePath.Length > 0)
				WriteJobDefSetPrefs(SerializeJobDefSetPrefs());

			m_JobDefSet = NewJobDefSet;

			LoadJobTreeItems();

			m_JobTreeSelections = ReadJobDefSetPrefs();
			if (m_JobTreeSelections.Length > 0)
				ApplyJobDefSetPrefs(m_JobTreeSelections);// does nothing when showing dialog
			EnableControls();

			this.Text = "Add Jobs - " + m_JobDefSet.Caption + " - " + FilePath;
		}

		// Try to open a job-def without allowing exceptions to propagate to
		// the caller.
		//
		private bool TryToLoadJobDef(string FilePath)
		{
			try
			{
				LoadJobDefFile(FilePath);
				return true;
			}
			catch (Exception xx)
			{
				HintLabel.Text = "Select the Load button and open a build job definitions file.";
				Debug.WriteLine("Unable to load job definitions file '" + FilePath + "'.  " + xx.ToString());
			}
			return false;
		}
		
		// Shows the form modally.
		//
		public void Execute(System.Windows.Forms.ListView JobQueueList)
		{
			if (m_FirstShow)
			{
				m_FirstShow = false;
				TryToLoadJobDef(ReadLastJobDefFilePath());
			}

			m_QueueList = JobQueueList;
			System.Windows.Forms.DialogResult DlgResult = this.ShowDialog();
			m_QueueList = null;

			if (m_JobDefSet.FilePath.Length > 0)
			{
				WriteLastJobDefFilePath(m_JobDefSet.FilePath);
				m_JobTreeSelections = SerializeJobDefSetPrefs();
				WriteJobDefSetPrefs(m_JobTreeSelections);
			}
		}

		private void AddJobForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				// load job tree with selections
				// NOTE: this is done here instead of before the dialog is
				//		 shown since showing causes all nodes to collapse and
				//       trying to expand them before showing does not work 
				//       unless you re-load the tree items.
				//TryToLoadJobDef(ReadLastJobDefFilePath());
				if (m_JobTreeSelections.Length > 0)
				{
					LoadJobTreeItems();
					ApplyJobDefSetPrefs(m_JobTreeSelections);
				}
			}
			catch (Exception x)
			{
				m_Prefs.ShowError("AddJobForm_Load: " + x.Message);
			}
		}

		private void LoadButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				RecentDefMenu.MenuItems.Clear();
				
				System.Windows.Forms.MenuItem BrowseItem = RecentDefMenu.MenuItems.Add("&Browse...");
				BrowseItem.Click += new System.EventHandler(this.BrowseJobDefMenuItem_Click);

				string[] RecentJobDefFilePaths = ReadRecentJobDefFilePaths();
				foreach (string JobDefFilePath in RecentJobDefFilePaths)
				{
					if (JobDefFilePath.ToUpper() != m_JobDefSet.FilePath.ToUpper())
					{
						System.Windows.Forms.MenuItem NewItem = RecentDefMenu.MenuItems.Add(JobDefFilePath);
						NewItem.Click += new System.EventHandler(this.LoadRecentJobDefMenuItem_Click);
					}
				}

				System.Drawing.Point pt = new System.Drawing.Point();
				pt.X = LoadButton.Location.X;
				pt.Y = LoadButton.Location.Y + LoadButton.Height;
				RecentDefMenu.Show(this, pt);
			}
			catch (Exception x)
			{
				m_Prefs.ShowError(x.Message);
			}
		}

		private void BrowseJobDefMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				JobDefOpenDialog.FileName = m_JobDefSet.FilePath;
				JobDefOpenDialog.ShowDialog();
				if (JobDefOpenDialog.FileName == m_JobDefSet.FilePath)
					return;
				LoadJobDefFile(JobDefOpenDialog.FileName);
				AddRecentJobDefFilePath(JobDefOpenDialog.FileName);
			}
			catch (Exception x)
			{
				m_Prefs.ShowError(x.Message);
			}
		}

		private void LoadRecentJobDefMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Windows.Forms.MenuItem Item = (System.Windows.Forms.MenuItem)sender;
				string FilePath = Item.Text;
				if (!System.IO.File.Exists(FilePath))
				{
					string Msg = 
						"Unable to find '" + FilePath + "'.\n\n" +
						"Do you want to remove the file path from the list of recently used files?";
					if (m_Prefs.PromptQuestion(Msg))
					{
						RemoveRecentJobDefFilePath(FilePath);
					}
					return;
				}
				LoadJobDefFile(FilePath);
				AddRecentJobDefFilePath(FilePath);
			}
			catch (Exception x)
			{
				m_Prefs.ShowError(x.Message);
			}
		}

		private void SelectAllContextItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				SetAllVisibleJobsSelected(true);
			}
			catch (Exception x)
			{
				m_Prefs.ShowError(x.Message);
			}
		}

		private void ClearAllContextItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				SetAllVisibleJobsSelected(false);
			}
			catch (Exception x)
			{
				m_Prefs.ShowError(x.Message);
			}
		}

		private void AddButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				AddSelectedItemsToQueue();
			}
			catch (Exception x)
			{
				m_Prefs.ShowError(x.Message);
			}
		}

		private void TreeView_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (e.Node.Parent == null)
			{
				m_AllowExpandCollapse = true;
				if (e.Node.Checked)
					e.Node.Expand();
				else
					e.Node.Collapse();
				m_AllowExpandCollapse = false;
			}
			EnableControls();
		}

		private void TreeView_BeforeCollapse(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if (!m_AllowExpandCollapse)
				e.Cancel = true;
		}

		private void TreeView_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if (!m_AllowExpandCollapse)
				e.Cancel = true;
		}

		private void TreeView_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
				System.Windows.Forms.TreeNode Node = TreeView.SelectedNode;
				if (Node.Parent != null)
				{
					JobDefinition JobDef = (JobDefinition)Node.Tag;
					QueueJob(JobDef);
				}
			}
			catch (Exception x)
			{
				m_Prefs.ShowError(x.Message);
			}
		}

		private void TreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			System.Windows.Forms.TreeNode Node = e.Node;
			if (Node == null || Node.Tag == null)
			{
				HintLabel.Text = "";
			}
			else if (Node.Parent == null)
			{
				// group node
				JobDefinitionGroup JobDefGroup = (JobDefinitionGroup)Node.Tag;
				HintLabel.Text = JobDefGroup.Hint;
			}
			else
			{
				// job node
				JobDefinition JobDef = (JobDefinition)Node.Tag;
				HintLabel.Text = JobDef.Hint;
			}
		}

	}
}
