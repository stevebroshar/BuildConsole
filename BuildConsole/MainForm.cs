using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Collections.Specialized;

namespace BuildConsole
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Threading.Thread m_StdOutThread = null;
		private System.Threading.Thread m_StdErrThread = null;
		private System.Windows.Forms.ListViewItem m_ActiveJobListItem = null;
		private Preferences m_Prefs = new Preferences();
		private MainPrefs m_MainPrefs = null;
		private AddJobForm m_AddJobForm = null;
		private FindForm m_FindForm = new FindForm();
		private int m_NextJobIndex = 0;
		private bool m_Paused = false;
		private bool m_PauseOnError = true;
		private DateTime m_LastProcessListUpdateTime = DateTime.Now;
		private DateTime m_JobStartTime;
		private string m_LogFilePath = "";
		private JobInfoDialog m_JobInfoDialog = new JobInfoDialog();
		private System.IO.StreamWriter m_LogStream = null;

		private System.Text.RegularExpressions.Regex m_ErrRE;
		private System.Text.RegularExpressions.Regex m_WarnRE;
		
		private System.Diagnostics.Process JobProcess;
		private System.Windows.Forms.StatusBar StatusBar;
		private System.Windows.Forms.ListView JobQueueList;
		private System.Windows.Forms.ColumnHeader JobQueueNameColumnHeader;
		private System.Windows.Forms.ColumnHeader JobQueueCmdColumnHeader;
		private System.Windows.Forms.ColumnHeader JobQueueStatusColumnHeader;
		private System.Windows.Forms.ContextMenu LogMenu;
		private System.Windows.Forms.SaveFileDialog LogSaveFileDialog;
		private System.Windows.Forms.MenuItem JobsMenuItem;
		private System.Windows.Forms.MenuItem HelpMenuItem;
		private System.Windows.Forms.MenuItem AboutMenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.MainMenu MainMenu;
		private System.Windows.Forms.Splitter MainSplitter;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem RemoveSelectedJobsMenuItem;
		private System.Windows.Forms.MenuItem RemoveCompletedJobsMenuItem;
		private System.Windows.Forms.MenuItem RemovePendingJobsMenuItem;
		private System.Windows.Forms.MenuItem RemoveAllJobsMenuItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.Timer JobTimer;
		private System.Windows.Forms.Timer StartupTimer;
		private System.Windows.Forms.StatusBarPanel IgnoreErrStatusBarPanel;
		private System.Windows.Forms.MenuItem AddMenuItem;
		private System.Windows.Forms.ContextMenu JobQueueMenu;
		private System.Windows.Forms.MenuItem ResumeHereJobQueueMenuItem;
		private System.Windows.Forms.MenuItem RemoveJobQueueMenuItem;
		private System.Windows.Forms.MenuItem PauseMenuItem;
		private System.Windows.Forms.MenuItem ContinueOnErrorMenuItem;
		private System.Windows.Forms.MenuItem ResumeMenuItem;
		private System.Windows.Forms.MenuItem PauseOnErrorMenuItem;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.RichTextBox LogView;
		private System.Windows.Forms.MenuItem SaveLogContextMenuItem;
		private System.Windows.Forms.MenuItem ClearLogContextMenuItem;
		private System.Windows.Forms.MenuItem LogMenuItem;
		private System.Windows.Forms.MenuItem SaveLogMenuItem;
		private System.Windows.Forms.MenuItem ClearLogMenuItem;
		private System.Windows.Forms.MenuItem ConsoleMenuItem;
		private System.Windows.Forms.MenuItem ResetMenuItem;
		private System.Windows.Forms.MenuItem AbortActiveJobMenuItem;
		private System.Windows.Forms.MenuItem FileMenuItem;
		private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
		private System.Windows.Forms.ToolBar ToolBar;
		private System.Windows.Forms.ImageList ImageList;
		private System.Windows.Forms.ToolBarButton ResetAllToolBarButton;
		private System.Windows.Forms.ToolBarButton AddJobsToolBarButton;
		private System.Windows.Forms.ToolBarButton AbortJobToolBarButton;
		private System.Windows.Forms.ToolBarButton PauseJobsToolBarButton;
		private System.Windows.Forms.ToolBarButton ToolBarSep1;
		private System.Windows.Forms.ToolBarButton RemoveAllJobsToolBarButton;
		private System.Windows.Forms.ToolBarButton ToolBarSep2;
		private System.Windows.Forms.MenuItem FindMenuItem;
		private System.Windows.Forms.MenuItem FindNextMenuItem;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem PriorityMenuItem;
		private System.Windows.Forms.MenuItem HighPriorityMenuItem;
		private System.Windows.Forms.MenuItem AbovePriorityMenuItem;
		private System.Windows.Forms.MenuItem NormalPriorityMenuItem;
		private System.Windows.Forms.MenuItem BelowPriorityMenuItem;
		private System.Windows.Forms.MenuItem IdlePriorityMenuItem;
		private System.Windows.Forms.Panel TopPanel;
		private System.Windows.Forms.Splitter TopSplitter;
		private System.Windows.Forms.ColumnHeader ProcessNameColumn;
		private System.Windows.Forms.ColumnHeader ProcessPidColumn;
		private System.Windows.Forms.ListView ProcessListView;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem HistoryMenuItem;
		private System.Windows.Forms.MenuItem ExploreHistoryMenuItem;
		private System.Windows.Forms.MenuItem PurgeAgeHistoryMenuItem;
		private System.Windows.Forms.MenuItem Days7PurgeAgeHistoryMenuItem;
		private System.Windows.Forms.MenuItem Days30PurgeAgeHistoryMenuItem;
		private System.Windows.Forms.MenuItem Days60PurgeAgeHistoryMenuItem;
		private System.Windows.Forms.MenuItem Days90PurgeAgeHistoryMenuItem;
		private System.Windows.Forms.MenuItem Days14PurgeAgeHistoryMenuItem;
		private System.Windows.Forms.MenuItem NeverPurgeHistoryMenuItem;
		private System.Windows.Forms.StatusBarPanel JobStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel LogHistoryStatusBarPanel;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem PropertiesJobQueueMenuItem;
		private System.Windows.Forms.MenuItem FindInLogJobQueueMenuItem;
		private System.Windows.Forms.MenuItem ClearToJobLogContextMenuItem;
		private System.Windows.Forms.MenuItem ClearToLineLogContextMenuItem;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			InitRE();
			m_AddJobForm = new AddJobForm(m_Prefs);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LogMenu = new System.Windows.Forms.ContextMenu();
            this.SaveLogContextMenuItem = new System.Windows.Forms.MenuItem();
            this.ClearLogContextMenuItem = new System.Windows.Forms.MenuItem();
            this.ClearToLineLogContextMenuItem = new System.Windows.Forms.MenuItem();
            this.ClearToJobLogContextMenuItem = new System.Windows.Forms.MenuItem();
            this.JobProcess = new System.Diagnostics.Process();
            this.JobTimer = new System.Windows.Forms.Timer(this.components);
            this.StatusBar = new System.Windows.Forms.StatusBar();
            this.JobStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.IgnoreErrStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.LogHistoryStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.JobQueueList = new System.Windows.Forms.ListView();
            this.JobQueueNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.JobQueueStatusColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.JobQueueCmdColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.JobQueueMenu = new System.Windows.Forms.ContextMenu();
            this.FindInLogJobQueueMenuItem = new System.Windows.Forms.MenuItem();
            this.ResumeHereJobQueueMenuItem = new System.Windows.Forms.MenuItem();
            this.RemoveJobQueueMenuItem = new System.Windows.Forms.MenuItem();
            this.PropertiesJobQueueMenuItem = new System.Windows.Forms.MenuItem();
            this.LogSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.FileMenuItem = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.JobsMenuItem = new System.Windows.Forms.MenuItem();
            this.AddMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.RemoveSelectedJobsMenuItem = new System.Windows.Forms.MenuItem();
            this.RemoveAllJobsMenuItem = new System.Windows.Forms.MenuItem();
            this.RemovePendingJobsMenuItem = new System.Windows.Forms.MenuItem();
            this.RemoveCompletedJobsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.AbortActiveJobMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.PauseMenuItem = new System.Windows.Forms.MenuItem();
            this.ResumeMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.ContinueOnErrorMenuItem = new System.Windows.Forms.MenuItem();
            this.PauseOnErrorMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.PriorityMenuItem = new System.Windows.Forms.MenuItem();
            this.HighPriorityMenuItem = new System.Windows.Forms.MenuItem();
            this.AbovePriorityMenuItem = new System.Windows.Forms.MenuItem();
            this.NormalPriorityMenuItem = new System.Windows.Forms.MenuItem();
            this.BelowPriorityMenuItem = new System.Windows.Forms.MenuItem();
            this.IdlePriorityMenuItem = new System.Windows.Forms.MenuItem();
            this.LogMenuItem = new System.Windows.Forms.MenuItem();
            this.SaveLogMenuItem = new System.Windows.Forms.MenuItem();
            this.ClearLogMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.FindMenuItem = new System.Windows.Forms.MenuItem();
            this.FindNextMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.HistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.ExploreHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.PurgeAgeHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.Days7PurgeAgeHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.Days14PurgeAgeHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.Days30PurgeAgeHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.Days60PurgeAgeHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.Days90PurgeAgeHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.NeverPurgeHistoryMenuItem = new System.Windows.Forms.MenuItem();
            this.ConsoleMenuItem = new System.Windows.Forms.MenuItem();
            this.ResetMenuItem = new System.Windows.Forms.MenuItem();
            this.HelpMenuItem = new System.Windows.Forms.MenuItem();
            this.AboutMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.MainSplitter = new System.Windows.Forms.Splitter();
            this.StartupTimer = new System.Windows.Forms.Timer(this.components);
            this.LogView = new System.Windows.Forms.RichTextBox();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ToolBar = new System.Windows.Forms.ToolBar();
            this.AddJobsToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ToolBarSep1 = new System.Windows.Forms.ToolBarButton();
            this.RemoveAllJobsToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ResetAllToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ToolBarSep2 = new System.Windows.Forms.ToolBarButton();
            this.PauseJobsToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.AbortJobToolBarButton = new System.Windows.Forms.ToolBarButton();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.TopPanel = new System.Windows.Forms.Panel();
            this.TopSplitter = new System.Windows.Forms.Splitter();
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.ProcessNameColumn = new System.Windows.Forms.ColumnHeader();
            this.ProcessPidColumn = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.JobStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IgnoreErrStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogHistoryStatusBarPanel)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogMenu
            // 
            this.LogMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SaveLogContextMenuItem,
            this.ClearLogContextMenuItem,
            this.ClearToLineLogContextMenuItem,
            this.ClearToJobLogContextMenuItem});
            this.LogMenu.Popup += new System.EventHandler(this.LogMenu_Popup);
            // 
            // SaveLogContextMenuItem
            // 
            this.SaveLogContextMenuItem.Index = 0;
            this.SaveLogContextMenuItem.Text = "Save &As...";
            this.SaveLogContextMenuItem.Click += new System.EventHandler(this.SaveLogMenuItem_Click);
            // 
            // ClearLogContextMenuItem
            // 
            this.ClearLogContextMenuItem.Index = 1;
            this.ClearLogContextMenuItem.Text = "&Clear";
            this.ClearLogContextMenuItem.Click += new System.EventHandler(this.ClearLogMenuItem_Click);
            // 
            // ClearToLineLogContextMenuItem
            // 
            this.ClearToLineLogContextMenuItem.Index = 2;
            this.ClearToLineLogContextMenuItem.Text = "Clear To &Line";
            this.ClearToLineLogContextMenuItem.Click += new System.EventHandler(this.ClearToLineMenuItem_Click);
            // 
            // ClearToJobLogContextMenuItem
            // 
            this.ClearToJobLogContextMenuItem.Index = 3;
            this.ClearToJobLogContextMenuItem.Text = "Clear To &Job";
            this.ClearToJobLogContextMenuItem.Visible = false;
            this.ClearToJobLogContextMenuItem.Click += new System.EventHandler(this.ClearToJobMenuItem_Click);
            // 
            // JobProcess
            // 
            this.JobProcess.EnableRaisingEvents = true;
            this.JobProcess.StartInfo.Domain = "";
            this.JobProcess.StartInfo.LoadUserProfile = false;
            this.JobProcess.StartInfo.Password = null;
            this.JobProcess.StartInfo.StandardErrorEncoding = null;
            this.JobProcess.StartInfo.StandardOutputEncoding = null;
            this.JobProcess.StartInfo.UserName = "";
            this.JobProcess.SynchronizingObject = this;
            this.JobProcess.Exited += new System.EventHandler(this.JobProcess_Exited);
            // 
            // JobTimer
            // 
            this.JobTimer.Enabled = true;
            this.JobTimer.Interval = 1000;
            this.JobTimer.Tick += new System.EventHandler(this.JobTimer_Tick);
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 427);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.JobStatusBarPanel,
            this.IgnoreErrStatusBarPanel,
            this.LogHistoryStatusBarPanel});
            this.StatusBar.ShowPanels = true;
            this.StatusBar.Size = new System.Drawing.Size(640, 22);
            this.StatusBar.TabIndex = 25;
            this.StatusBar.DoubleClick += new System.EventHandler(this.StatusBar_DoubleClick);
            this.StatusBar.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.StatusBar_PanelClick);
            // 
            // JobStatusBarPanel
            // 
            this.JobStatusBarPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.JobStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.JobStatusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.JobStatusBarPanel.Name = "JobStatusBarPanel";
            this.JobStatusBarPanel.ToolTipText = "Running Status";
            this.JobStatusBarPanel.Width = 10;
            // 
            // IgnoreErrStatusBarPanel
            // 
            this.IgnoreErrStatusBarPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.IgnoreErrStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.IgnoreErrStatusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.IgnoreErrStatusBarPanel.Name = "IgnoreErrStatusBarPanel";
            this.IgnoreErrStatusBarPanel.Text = "Ignoring Errors";
            this.IgnoreErrStatusBarPanel.ToolTipText = "Ignoring Errors Indicator";
            this.IgnoreErrStatusBarPanel.Width = 89;
            // 
            // LogHistoryStatusBarPanel
            // 
            this.LogHistoryStatusBarPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.LogHistoryStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.LogHistoryStatusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.LogHistoryStatusBarPanel.Name = "LogHistoryStatusBarPanel";
            this.LogHistoryStatusBarPanel.ToolTipText = "Log History File";
            this.LogHistoryStatusBarPanel.Width = 524;
            // 
            // JobQueueList
            // 
            this.JobQueueList.BackColor = System.Drawing.SystemColors.Window;
            this.JobQueueList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JobQueueNameColumnHeader,
            this.JobQueueStatusColumnHeader,
            this.JobQueueCmdColumnHeader});
            this.JobQueueList.ContextMenu = this.JobQueueMenu;
            this.JobQueueList.Dock = System.Windows.Forms.DockStyle.Left;
            this.JobQueueList.FullRowSelect = true;
            this.JobQueueList.HideSelection = false;
            this.JobQueueList.Location = new System.Drawing.Point(0, 0);
            this.JobQueueList.Name = "JobQueueList";
            this.JobQueueList.Size = new System.Drawing.Size(496, 184);
            this.JobQueueList.TabIndex = 12;
            this.JobQueueList.UseCompatibleStateImageBehavior = false;
            this.JobQueueList.View = System.Windows.Forms.View.Details;
            this.JobQueueList.DoubleClick += new System.EventHandler(this.JobQueueList_DoubleClick);
            this.JobQueueList.SelectedIndexChanged += new System.EventHandler(this.JobQueueList_SelectedIndexChanged);
            // 
            // JobQueueNameColumnHeader
            // 
            this.JobQueueNameColumnHeader.Text = "Name";
            this.JobQueueNameColumnHeader.Width = 201;
            // 
            // JobQueueStatusColumnHeader
            // 
            this.JobQueueStatusColumnHeader.Text = "Status";
            this.JobQueueStatusColumnHeader.Width = 82;
            // 
            // JobQueueCmdColumnHeader
            // 
            this.JobQueueCmdColumnHeader.Text = "Command";
            this.JobQueueCmdColumnHeader.Width = 209;
            // 
            // JobQueueMenu
            // 
            this.JobQueueMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FindInLogJobQueueMenuItem,
            this.ResumeHereJobQueueMenuItem,
            this.RemoveJobQueueMenuItem,
            this.PropertiesJobQueueMenuItem});
            this.JobQueueMenu.Popup += new System.EventHandler(this.JobQueueMenu_Popup);
            // 
            // FindInLogJobQueueMenuItem
            // 
            this.FindInLogJobQueueMenuItem.Index = 0;
            this.FindInLogJobQueueMenuItem.Text = "&Find in Log";
            this.FindInLogJobQueueMenuItem.Click += new System.EventHandler(this.FindInLogJobQueueMenuItem_Click);
            // 
            // ResumeHereJobQueueMenuItem
            // 
            this.ResumeHereJobQueueMenuItem.Index = 1;
            this.ResumeHereJobQueueMenuItem.Text = "Resume &Here";
            this.ResumeHereJobQueueMenuItem.Click += new System.EventHandler(this.ResumeHereMenuItem_Click);
            // 
            // RemoveJobQueueMenuItem
            // 
            this.RemoveJobQueueMenuItem.Index = 2;
            this.RemoveJobQueueMenuItem.Text = "&Remove";
            this.RemoveJobQueueMenuItem.Click += new System.EventHandler(this.RemoveSelectedJobsMenuItem_Click);
            // 
            // PropertiesJobQueueMenuItem
            // 
            this.PropertiesJobQueueMenuItem.Index = 3;
            this.PropertiesJobQueueMenuItem.Text = "&Properties";
            this.PropertiesJobQueueMenuItem.Click += new System.EventHandler(this.PropertiesJobQueueMenuItem_Click);
            // 
            // LogSaveFileDialog
            // 
            this.LogSaveFileDialog.DefaultExt = "rtf";
            this.LogSaveFileDialog.Filter = "Rich Text (*.rtf)|*.rtf|ANSI Text (*.txt)|*.txt|Unicode Text (*.txt)|*.txt|Any (*" +
                ".*)|*.*";
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileMenuItem,
            this.JobsMenuItem,
            this.LogMenuItem,
            this.ConsoleMenuItem,
            this.HelpMenuItem,
            this.menuItem8});
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.Index = 0;
            this.FileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ExitMenuItem});
            this.FileMenuItem.Text = "&File";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 0;
            this.ExitMenuItem.Text = "E&xit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // JobsMenuItem
            // 
            this.JobsMenuItem.Index = 1;
            this.JobsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AddMenuItem,
            this.menuItem1,
            this.RemoveSelectedJobsMenuItem,
            this.RemoveAllJobsMenuItem,
            this.RemovePendingJobsMenuItem,
            this.RemoveCompletedJobsMenuItem,
            this.menuItem7,
            this.AbortActiveJobMenuItem,
            this.menuItem2,
            this.PauseMenuItem,
            this.ResumeMenuItem,
            this.menuItem6,
            this.ContinueOnErrorMenuItem,
            this.PauseOnErrorMenuItem,
            this.menuItem3,
            this.PriorityMenuItem});
            this.JobsMenuItem.Text = "&Jobs";
            this.JobsMenuItem.Popup += new System.EventHandler(this.JobsMenuItem_Popup);
            // 
            // AddMenuItem
            // 
            this.AddMenuItem.Index = 0;
            this.AddMenuItem.Shortcut = System.Windows.Forms.Shortcut.Ins;
            this.AddMenuItem.Text = "&Add...";
            this.AddMenuItem.Click += new System.EventHandler(this.AddMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // RemoveSelectedJobsMenuItem
            // 
            this.RemoveSelectedJobsMenuItem.Index = 2;
            this.RemoveSelectedJobsMenuItem.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.RemoveSelectedJobsMenuItem.Text = "Remove &Selected";
            this.RemoveSelectedJobsMenuItem.Click += new System.EventHandler(this.RemoveSelectedJobsMenuItem_Click);
            // 
            // RemoveAllJobsMenuItem
            // 
            this.RemoveAllJobsMenuItem.Index = 3;
            this.RemoveAllJobsMenuItem.Shortcut = System.Windows.Forms.Shortcut.ShiftDel;
            this.RemoveAllJobsMenuItem.Text = "Remove A&ll";
            this.RemoveAllJobsMenuItem.Click += new System.EventHandler(this.RemoveAllJobsMenuItem_Click);
            // 
            // RemovePendingJobsMenuItem
            // 
            this.RemovePendingJobsMenuItem.Index = 4;
            this.RemovePendingJobsMenuItem.Shortcut = System.Windows.Forms.Shortcut.ShiftF11;
            this.RemovePendingJobsMenuItem.Text = "Remove Pendin&g";
            this.RemovePendingJobsMenuItem.Click += new System.EventHandler(this.RemovePendingJobsMenuItem_Click);
            // 
            // RemoveCompletedJobsMenuItem
            // 
            this.RemoveCompletedJobsMenuItem.Index = 5;
            this.RemoveCompletedJobsMenuItem.Shortcut = System.Windows.Forms.Shortcut.ShiftF12;
            this.RemoveCompletedJobsMenuItem.Text = "Remove &Done";
            this.RemoveCompletedJobsMenuItem.Click += new System.EventHandler(this.RemoveCompletedJobsMenuItem_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 6;
            this.menuItem7.Text = "-";
            // 
            // AbortActiveJobMenuItem
            // 
            this.AbortActiveJobMenuItem.Index = 7;
            this.AbortActiveJobMenuItem.Shortcut = System.Windows.Forms.Shortcut.ShiftF4;
            this.AbortActiveJobMenuItem.Text = "A&bort";
            this.AbortActiveJobMenuItem.Click += new System.EventHandler(this.AbortActiveJobMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 8;
            this.menuItem2.Text = "-";
            // 
            // PauseMenuItem
            // 
            this.PauseMenuItem.Index = 9;
            this.PauseMenuItem.Shortcut = System.Windows.Forms.Shortcut.ShiftF5;
            this.PauseMenuItem.Text = "&Pause";
            this.PauseMenuItem.Click += new System.EventHandler(this.PauseMenuItem_Click);
            // 
            // ResumeMenuItem
            // 
            this.ResumeMenuItem.Index = 10;
            this.ResumeMenuItem.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.ResumeMenuItem.Text = "&Resume";
            this.ResumeMenuItem.Click += new System.EventHandler(this.ResumeMenuItem_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 11;
            this.menuItem6.Text = "-";
            // 
            // ContinueOnErrorMenuItem
            // 
            this.ContinueOnErrorMenuItem.Index = 12;
            this.ContinueOnErrorMenuItem.Shortcut = System.Windows.Forms.Shortcut.ShiftF9;
            this.ContinueOnErrorMenuItem.Text = "&Continue On Error";
            this.ContinueOnErrorMenuItem.Click += new System.EventHandler(this.ContinueOnErrorMenuItem_Click);
            // 
            // PauseOnErrorMenuItem
            // 
            this.PauseOnErrorMenuItem.Index = 13;
            this.PauseOnErrorMenuItem.Shortcut = System.Windows.Forms.Shortcut.F9;
            this.PauseOnErrorMenuItem.Text = "Pause On &Error";
            this.PauseOnErrorMenuItem.Click += new System.EventHandler(this.PauseOnErrorMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 14;
            this.menuItem3.Text = "-";
            // 
            // PriorityMenuItem
            // 
            this.PriorityMenuItem.Index = 15;
            this.PriorityMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.HighPriorityMenuItem,
            this.AbovePriorityMenuItem,
            this.NormalPriorityMenuItem,
            this.BelowPriorityMenuItem,
            this.IdlePriorityMenuItem});
            this.PriorityMenuItem.Text = "Priorit&y";
            this.PriorityMenuItem.Select += new System.EventHandler(this.PriorityMenuItem_Select);
            // 
            // HighPriorityMenuItem
            // 
            this.HighPriorityMenuItem.Index = 0;
            this.HighPriorityMenuItem.Text = "&High";
            this.HighPriorityMenuItem.Click += new System.EventHandler(this.HighPriorityMenuItem_Click);
            // 
            // AbovePriorityMenuItem
            // 
            this.AbovePriorityMenuItem.Index = 1;
            this.AbovePriorityMenuItem.Text = "&Above Normal";
            this.AbovePriorityMenuItem.Click += new System.EventHandler(this.AbovePriorityMenuItem_Click);
            // 
            // NormalPriorityMenuItem
            // 
            this.NormalPriorityMenuItem.Index = 2;
            this.NormalPriorityMenuItem.Text = "&Normal";
            this.NormalPriorityMenuItem.Click += new System.EventHandler(this.NormalPriorityMenuItem_Click);
            // 
            // BelowPriorityMenuItem
            // 
            this.BelowPriorityMenuItem.Index = 3;
            this.BelowPriorityMenuItem.Text = "&Below Normal";
            this.BelowPriorityMenuItem.Click += new System.EventHandler(this.BelowPriorityMenuItem_Click);
            // 
            // IdlePriorityMenuItem
            // 
            this.IdlePriorityMenuItem.Index = 4;
            this.IdlePriorityMenuItem.Text = "&Idle";
            this.IdlePriorityMenuItem.Click += new System.EventHandler(this.IdlePriorityMenuItem_Click);
            // 
            // LogMenuItem
            // 
            this.LogMenuItem.Index = 2;
            this.LogMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SaveLogMenuItem,
            this.ClearLogMenuItem,
            this.menuItem4,
            this.FindMenuItem,
            this.FindNextMenuItem,
            this.menuItem5,
            this.HistoryMenuItem});
            this.LogMenuItem.Text = "&Log";
            this.LogMenuItem.Popup += new System.EventHandler(this.LogMenu_Popup);
            // 
            // SaveLogMenuItem
            // 
            this.SaveLogMenuItem.Index = 0;
            this.SaveLogMenuItem.Text = "Save &As...";
            this.SaveLogMenuItem.Click += new System.EventHandler(this.SaveLogMenuItem_Click);
            // 
            // ClearLogMenuItem
            // 
            this.ClearLogMenuItem.Index = 1;
            this.ClearLogMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlDel;
            this.ClearLogMenuItem.Text = "&Clear";
            this.ClearLogMenuItem.Click += new System.EventHandler(this.ClearLogMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // FindMenuItem
            // 
            this.FindMenuItem.Index = 3;
            this.FindMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.FindMenuItem.Text = "&Find";
            this.FindMenuItem.Click += new System.EventHandler(this.FindMenuItem_Click);
            // 
            // FindNextMenuItem
            // 
            this.FindNextMenuItem.Index = 4;
            this.FindNextMenuItem.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.FindNextMenuItem.Text = "Find &Next";
            this.FindNextMenuItem.Click += new System.EventHandler(this.FindNextMenuItem_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 5;
            this.menuItem5.Text = "-";
            // 
            // HistoryMenuItem
            // 
            this.HistoryMenuItem.Index = 6;
            this.HistoryMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ExploreHistoryMenuItem,
            this.PurgeAgeHistoryMenuItem});
            this.HistoryMenuItem.Text = "&History";
            // 
            // ExploreHistoryMenuItem
            // 
            this.ExploreHistoryMenuItem.Index = 0;
            this.ExploreHistoryMenuItem.Text = "E&xplore...";
            this.ExploreHistoryMenuItem.Click += new System.EventHandler(this.ExploreHistoryMenuItem_Click);
            // 
            // PurgeAgeHistoryMenuItem
            // 
            this.PurgeAgeHistoryMenuItem.Index = 1;
            this.PurgeAgeHistoryMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Days7PurgeAgeHistoryMenuItem,
            this.Days14PurgeAgeHistoryMenuItem,
            this.Days30PurgeAgeHistoryMenuItem,
            this.Days60PurgeAgeHistoryMenuItem,
            this.Days90PurgeAgeHistoryMenuItem,
            this.NeverPurgeHistoryMenuItem});
            this.PurgeAgeHistoryMenuItem.Text = "Purge &Age";
            // 
            // Days7PurgeAgeHistoryMenuItem
            // 
            this.Days7PurgeAgeHistoryMenuItem.Index = 0;
            this.Days7PurgeAgeHistoryMenuItem.Text = "&7 Days";
            this.Days7PurgeAgeHistoryMenuItem.Click += new System.EventHandler(this.Days7PurgeAgeHistoryMenuItem_Click);
            // 
            // Days14PurgeAgeHistoryMenuItem
            // 
            this.Days14PurgeAgeHistoryMenuItem.Index = 1;
            this.Days14PurgeAgeHistoryMenuItem.Text = "&14 Days";
            this.Days14PurgeAgeHistoryMenuItem.Click += new System.EventHandler(this.Days14PurgeAgeHistoryMenuItem_Click);
            // 
            // Days30PurgeAgeHistoryMenuItem
            // 
            this.Days30PurgeAgeHistoryMenuItem.Index = 2;
            this.Days30PurgeAgeHistoryMenuItem.Text = "&30 Days";
            this.Days30PurgeAgeHistoryMenuItem.Click += new System.EventHandler(this.Days30PurgeAgeHistoryMenuItem_Click);
            // 
            // Days60PurgeAgeHistoryMenuItem
            // 
            this.Days60PurgeAgeHistoryMenuItem.Index = 3;
            this.Days60PurgeAgeHistoryMenuItem.Text = "&60 Days";
            this.Days60PurgeAgeHistoryMenuItem.Click += new System.EventHandler(this.Days60PurgeAgeHistoryMenuItem_Click);
            // 
            // Days90PurgeAgeHistoryMenuItem
            // 
            this.Days90PurgeAgeHistoryMenuItem.Index = 4;
            this.Days90PurgeAgeHistoryMenuItem.Text = "&90 Days";
            this.Days90PurgeAgeHistoryMenuItem.Click += new System.EventHandler(this.Days90PurgeAgeHistoryMenuItem_Click);
            // 
            // NeverPurgeHistoryMenuItem
            // 
            this.NeverPurgeHistoryMenuItem.Index = 5;
            this.NeverPurgeHistoryMenuItem.Text = "&Never Purge";
            this.NeverPurgeHistoryMenuItem.Click += new System.EventHandler(this.NeverPurgeHistoryMenuItem_Click);
            // 
            // ConsoleMenuItem
            // 
            this.ConsoleMenuItem.Index = 3;
            this.ConsoleMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ResetMenuItem});
            this.ConsoleMenuItem.Text = "&Tools";
            // 
            // ResetMenuItem
            // 
            this.ResetMenuItem.Index = 0;
            this.ResetMenuItem.Shortcut = System.Windows.Forms.Shortcut.AltBksp;
            this.ResetMenuItem.Text = "&Reset All";
            this.ResetMenuItem.Click += new System.EventHandler(this.ResetMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Index = 4;
            this.HelpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AboutMenuItem});
            this.HelpMenuItem.Text = "&Help";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Index = 0;
            this.AboutMenuItem.Text = "&About...";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 5;
            this.menuItem8.Text = "TEST";
            this.menuItem8.Visible = false;
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // MainSplitter
            // 
            this.MainSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainSplitter.Location = new System.Drawing.Point(0, 210);
            this.MainSplitter.Name = "MainSplitter";
            this.MainSplitter.Size = new System.Drawing.Size(640, 3);
            this.MainSplitter.TabIndex = 28;
            this.MainSplitter.TabStop = false;
            // 
            // StartupTimer
            // 
            this.StartupTimer.Enabled = true;
            this.StartupTimer.Interval = 1;
            this.StartupTimer.Tick += new System.EventHandler(this.StartupTimer_Tick);
            // 
            // LogView
            // 
            this.LogView.BackColor = System.Drawing.SystemColors.Window;
            this.LogView.ContextMenu = this.LogMenu;
            this.LogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogView.HideSelection = false;
            this.LogView.Location = new System.Drawing.Point(0, 213);
            this.LogView.Name = "LogView";
            this.LogView.ReadOnly = true;
            this.LogView.Size = new System.Drawing.Size(640, 214);
            this.LogView.TabIndex = 29;
            this.LogView.Text = "";
            // 
            // ToolBar
            // 
            this.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.AddJobsToolBarButton,
            this.ToolBarSep1,
            this.RemoveAllJobsToolBarButton,
            this.ResetAllToolBarButton,
            this.ToolBarSep2,
            this.PauseJobsToolBarButton,
            this.AbortJobToolBarButton});
            this.ToolBar.Divider = false;
            this.ToolBar.DropDownArrows = true;
            this.ToolBar.ImageList = this.ImageList;
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.ShowToolTips = true;
            this.ToolBar.Size = new System.Drawing.Size(640, 26);
            this.ToolBar.TabIndex = 30;
            this.ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar_ButtonClick);
            // 
            // AddJobsToolBarButton
            // 
            this.AddJobsToolBarButton.ImageIndex = 19;
            this.AddJobsToolBarButton.Name = "AddJobsToolBarButton";
            this.AddJobsToolBarButton.ToolTipText = "Add Jobs";
            // 
            // ToolBarSep1
            // 
            this.ToolBarSep1.Name = "ToolBarSep1";
            this.ToolBarSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // RemoveAllJobsToolBarButton
            // 
            this.RemoveAllJobsToolBarButton.ImageIndex = 23;
            this.RemoveAllJobsToolBarButton.Name = "RemoveAllJobsToolBarButton";
            this.RemoveAllJobsToolBarButton.ToolTipText = "Remove All Jobs";
            // 
            // ResetAllToolBarButton
            // 
            this.ResetAllToolBarButton.ImageIndex = 14;
            this.ResetAllToolBarButton.Name = "ResetAllToolBarButton";
            this.ResetAllToolBarButton.ToolTipText = "Reset All";
            // 
            // ToolBarSep2
            // 
            this.ToolBarSep2.Name = "ToolBarSep2";
            this.ToolBarSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // PauseJobsToolBarButton
            // 
            this.PauseJobsToolBarButton.ImageIndex = 22;
            this.PauseJobsToolBarButton.Name = "PauseJobsToolBarButton";
            this.PauseJobsToolBarButton.ToolTipText = "Paused";
            // 
            // AbortJobToolBarButton
            // 
            this.AbortJobToolBarButton.Enabled = false;
            this.AbortJobToolBarButton.ImageIndex = 21;
            this.AbortJobToolBarButton.Name = "AbortJobToolBarButton";
            this.AbortJobToolBarButton.ToolTipText = "Abort";
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Silver;
            this.ImageList.Images.SetKeyName(0, "");
            this.ImageList.Images.SetKeyName(1, "");
            this.ImageList.Images.SetKeyName(2, "");
            this.ImageList.Images.SetKeyName(3, "");
            this.ImageList.Images.SetKeyName(4, "");
            this.ImageList.Images.SetKeyName(5, "");
            this.ImageList.Images.SetKeyName(6, "");
            this.ImageList.Images.SetKeyName(7, "");
            this.ImageList.Images.SetKeyName(8, "");
            this.ImageList.Images.SetKeyName(9, "");
            this.ImageList.Images.SetKeyName(10, "");
            this.ImageList.Images.SetKeyName(11, "");
            this.ImageList.Images.SetKeyName(12, "");
            this.ImageList.Images.SetKeyName(13, "");
            this.ImageList.Images.SetKeyName(14, "");
            this.ImageList.Images.SetKeyName(15, "");
            this.ImageList.Images.SetKeyName(16, "");
            this.ImageList.Images.SetKeyName(17, "");
            this.ImageList.Images.SetKeyName(18, "");
            this.ImageList.Images.SetKeyName(19, "");
            this.ImageList.Images.SetKeyName(20, "");
            this.ImageList.Images.SetKeyName(21, "");
            this.ImageList.Images.SetKeyName(22, "");
            this.ImageList.Images.SetKeyName(23, "");
            this.ImageList.Images.SetKeyName(24, "");
            this.ImageList.Images.SetKeyName(25, "");
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.TopSplitter);
            this.TopPanel.Controls.Add(this.ProcessListView);
            this.TopPanel.Controls.Add(this.JobQueueList);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 26);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(640, 184);
            this.TopPanel.TabIndex = 31;
            // 
            // TopSplitter
            // 
            this.TopSplitter.Location = new System.Drawing.Point(496, 0);
            this.TopSplitter.Name = "TopSplitter";
            this.TopSplitter.Size = new System.Drawing.Size(3, 184);
            this.TopSplitter.TabIndex = 13;
            this.TopSplitter.TabStop = false;
            // 
            // ProcessListView
            // 
            this.ProcessListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProcessNameColumn,
            this.ProcessPidColumn});
            this.ProcessListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessListView.FullRowSelect = true;
            this.ProcessListView.HideSelection = false;
            this.ProcessListView.Location = new System.Drawing.Point(496, 0);
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.Size = new System.Drawing.Size(144, 184);
            this.ProcessListView.TabIndex = 0;
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.Details;
            // 
            // ProcessNameColumn
            // 
            this.ProcessNameColumn.Text = "Process";
            this.ProcessNameColumn.Width = 100;
            // 
            // ProcessPidColumn
            // 
            this.ProcessPidColumn.Text = "PID";
            this.ProcessPidColumn.Width = 40;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(640, 449);
            this.Controls.Add(this.LogView);
            this.Controls.Add(this.MainSplitter);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.ToolBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Build Console";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.JobStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IgnoreErrStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogHistoryStatusBarPanel)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		string FormatTimeSpanAsMinutes(TimeSpan Duration)
		{
			long DurationInSeconds = Convert.ToInt32(Math.Floor(Duration.TotalMinutes * 60));
			long SecondCount;
			long MinuteCount = Math.DivRem(DurationInSeconds, 60, out SecondCount);
			if (SecondCount < 10)
				return MinuteCount + ":0" + SecondCount;
			else
				return MinuteCount + ":" + SecondCount;
		}

		string FormatElapsedJobTime()
		{
			return FormatTimeSpanAsMinutes(DateTime.Now - m_JobStartTime);
		}

		private class SavedSelection
		{
			private bool mOrigFocused;
			private bool mOrigEnabled;
			private int mOrigSelStart = -1;
			private int mOrigSelLen = -1;
			private System.Windows.Forms.RichTextBox mRichTextBox = null;
			public SavedSelection(System.Windows.Forms.RichTextBox RichTextBox)
			{
				mRichTextBox = RichTextBox;

				// store whether has focus since disabling the control switches focus
				mOrigFocused = mRichTextBox.Focused;
				
				// disable control to prevent user from changing selection until Restore
				mOrigEnabled = mRichTextBox.Enabled;
				mRichTextBox.Enabled = false;

				// cache current selection
				if (RichTextBox.SelectionStart < RichTextBox.TextLength)
				{
					mOrigSelStart = mRichTextBox.SelectionStart;
					mOrigSelLen = mRichTextBox.SelectionLength;
				}
			}
			public void Restore()
			{
				if (mOrigSelStart >= 0)
				{
					// restore selection
					mRichTextBox.SelectionStart = mOrigSelStart;
					mRichTextBox.SelectionLength = mOrigSelLen;
				}
				mRichTextBox.Enabled = mOrigEnabled;
				if (mOrigFocused) mRichTextBox.Focus();
			}
		};

		private readonly Font HEAD_FONT = new Font("Microsoft Sans Serif", (float)16, System.Drawing.FontStyle.Bold);
		private readonly Font TEXT_FONT = new Font("Microsoft Sans Serif", (float)8.25);

		private void LogHeader(string Text)
		{
			lock(this)
			{
				if (m_LogStream != null)
				{
					String LineText = new String('=', Text.Length);
					m_LogStream.WriteLine("");
					m_LogStream.WriteLine(LineText);
					m_LogStream.WriteLine(Text);
					m_LogStream.WriteLine(LineText);
					m_LogStream.WriteLine("");
				}

				SavedSelection ss = new SavedSelection(LogView);

				LogView.SelectionStart = LogView.TextLength;
				LogView.SelectionFont = HEAD_FONT;
				LogView.SelectionHangingIndent = 8;
				LogView.SelectionColor = Color.Black;
				LogView.SelectedText = Text + "\n";

				ss.Restore();
			}
		}

		private void LogErrorLine(string Text)
		{
			lock(this)
			{
				if (m_LogStream != null)
					m_LogStream.WriteLine(Text);

				SavedSelection ss = new SavedSelection(LogView);

				LogView.SelectionStart = LogView.TextLength;
				LogView.SelectionFont = TEXT_FONT;
				LogView.SelectionHangingIndent = 8;
				LogView.SelectionColor = Color.Red;
				LogView.SelectedText = Text + "\n";

				ss.Restore();
			}
		}

		private StringCollection m_LogQueue = new StringCollection();
		
		// Initializes the regular expressions used for testing whether a
		// line is a warning or an error.
		//
		// Examples of matches for an error line (similar for warnings):
		//	 error
		//   Error text
		//   ERROR: text
		//   text: ERROR
		//   text: ERROR text
		//   text: Error: text
		//   text: Error err-code: text
		//
		private void InitRE()
		{
			string ErrLabel = "(error|ERROR|Error)";
			string WrnLabel = "(warning|WARNING|Warning)";
			string MsgMarker = "<MSG>";

			// matches on messages that do not have a semi-colon after 
			// the word 'error'
			string LogMsgExp = "((^|:\\s*)"+ MsgMarker + "(\\s|$))";

			// matches a Visual Studio message code consisting of one or more
			// letters followed by one or more digits
			string VsMsgCode = "[a-zA-Z]+\\d+";

			// matches a Visual Studio error message which is the word 'error'
			// followed by a semi-colon with possibly a message code before
			// the semi-colon.
			string VsMsgCodeExp = "(\\s" + VsMsgCode + ")?";
			string VsMsgExp = "((^|\\W)" + MsgMarker + VsMsgCodeExp + "\\s*:)";

			string ErrRegEx = LogMsgExp.Replace(MsgMarker, ErrLabel) + "|" +
				VsMsgExp.Replace(MsgMarker, ErrLabel);
			string WrnRegEx = LogMsgExp.Replace(MsgMarker, WrnLabel) + "|" +
				VsMsgExp.Replace(MsgMarker, WrnLabel);

			m_ErrRE =
				new System.Text.RegularExpressions.Regex
				(ErrRegEx, System.Text.RegularExpressions.RegexOptions.Compiled);

			m_WarnRE =
				new System.Text.RegularExpressions.Regex
				(WrnRegEx, System.Text.RegularExpressions.RegexOptions.Compiled);
		}

		// Implements optimized writing to the log view based on the color of
		// the text to be written and the color to be used for the queued
		// text block.
		//
		// NOTE
		// The original algorithm for styling inserted text was to set the
		// style (font color, size ...) at the insertion point (zero length
		// selection) and then set SelectedText.  But, sometimes the
		// RichTextBox does not apply the insertion point style to all of the
		// text.  This only	happens when many lines are appended at the same
		// time.  I can't figure out the line/character count or what exactly
		// would indicate that the styling problem will occur before inserting
		// the text.  But, I have not seen it happen and hope it won't happen
		// for 800 characters or less.  So, below this threashold we use the
		// more visually appealing algorithm of setting the style before
		// appending and for above the threshold we use the less appealing
		// algorithm  of setting the style after appending.  By less
		// appealing I mean that sometimes you see the appended text
		// highlighted as it is selected for an instant and sometimes the
		// inserted text is obviously in the wrong style for an instant.
		// To minimize this latter aspect, the style is applied before and
		// after for above the threshold.  If the RichTextBox chooses to
		// respect the insertion point style, then the user won't see the
		// text in the wrong style at all.  If the RichTextBox chooses to
		// disregard the insertin point style, then the text will look wrong
		// for an instant before we fix it.
		//
		private void LogColoredTextWithQueing
			(Color LineColor, 
			string LineText, 
			ref Color BlockColor,
			ref string BlockText)
		{
			if (BlockColor == LineColor)
			{
				// add the line to the buffer since it is the same color as previous
				BlockText += LineText;
			}
			else
			{
				// flush the buffer since line is a different color than previous
				if (BlockText.Length > 0)
				{
					// set insertion point to end
					LogView.SelectionStart = LogView.TextLength;
					LogView.SelectionLength = 0;

					// set style
					LogView.SelectionFont = TEXT_FONT;
					LogView.SelectionHangingIndent = 8;
					LogView.SelectionColor = BlockColor;

					if (BlockText.Length <= 800)
					{
						// append by setting selected-text
						LogView.SelectedText = BlockText;
					}
					else
					{
						// cache pre-append length
						int PreAppendLength = LogView.TextLength;

						// append by setting selected-text
						LogView.SelectedText = BlockText;

						// style appended text
						LogView.SelectionStart = PreAppendLength;
						LogView.SelectionLength = BlockText.Length;
						LogView.SelectionFont = TEXT_FONT;
						LogView.SelectionHangingIndent = 8;
						LogView.SelectionColor = BlockColor;
					}
				}

				// start a new buffer
				BlockText = LineText;
				BlockColor = LineColor;
			}
		}

		// Writes a collection of lines to the log view and to the history
		// log file if it's open.
		//
		private void LogLines(StringCollection Lines)
		{
			lock(this)
			{
				if (m_LogStream != null)
				{
					// write lines to log file
					foreach (string Line in Lines)
					{
						string LineText = Line.Substring(1);
						m_LogStream.WriteLine(LineText);
					}
					m_LogStream.Flush();
				}

				SavedSelection ss = new SavedSelection(LogView);
				
				string BlockText = "";
				Color BlockColor = Color.White;
				foreach (string Line in Lines)
				{
					char Type = Line[0];
					string LineText = Line.Substring(1) + "\n";
					Color LineColor;
					if (Type == 'e' || m_ErrRE.IsMatch(LineText))
						LineColor = Color.Red;
					else if (m_WarnRE.IsMatch(LineText))
						LineColor = Color.DarkGoldenrod;
					else
						LineColor = Color.Blue;
					LogColoredTextWithQueing(LineColor, LineText, ref BlockColor, ref BlockText);
				}
				LogColoredTextWithQueing(Color.White, "", ref BlockColor, ref BlockText);

				// set insertion point to end
				//NOTE: this is needed in case insertion point was at the end
				//		when this procedure was entered and since SavedSelection
				//		does not handle that case.
				LogView.SelectionStart = LogView.TextLength;
				LogView.SelectionLength = 0;				

				ss.Restore();
			}
		}

		// Flushes the queued log lines.
		//
		private void ProcessQueuedLogLines()
		{
			lock(this)
			{
				if (m_LogQueue.Count > 0)
				{
					LogLines(m_LogQueue);
					m_LogQueue.Clear();
				}
			}
		}

		// Adds a line to the log queue with the line type as the first
		// character.
		//
		private void QueueLogLine(string Line, char Type)
		{
			lock(this)
			{
				m_LogQueue.Add(Type + Line);
			}
		}

		// Returns the folder path to use for history log files.
		//
		private string GetLogFolderPath()
		{
			string LogFolderPath = System.IO.Path.GetTempPath() + "BuildConsole";
			if (!System.IO.Directory.Exists(LogFolderPath))
				System.IO.Directory.CreateDirectory(LogFolderPath);
			return LogFolderPath;
		}

		// Write applicaiton information into the log.
		//
		private void LogAppInfo()
		{
			System.Diagnostics.Process ThisProcess = System.Diagnostics.Process.GetCurrentProcess();
			System.Diagnostics.FileVersionInfo Info = System.Diagnostics.FileVersionInfo.GetVersionInfo(ThisProcess.MainModule.FileName);
			m_LogStream.WriteLine(Info.FileDescription);
			m_LogStream.WriteLine("Version " + Info.FileVersion);
		}

		// Opens a log history file with a named based on the current time.
		//
		private void OpenLogFile()
		{
			CloseLogFile();
			string Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
			string LogFilePath = System.IO.Path.Combine(GetLogFolderPath(), Timestamp + ".log");
			m_LogStream = System.IO.File.CreateText(LogFilePath);
			LogAppInfo();
			m_LogFilePath = LogFilePath;
			ShowLogHistoryStatus();
		}

		// Closes the log history file if it is open.
		//
		private void CloseLogFile()
		{
			if (m_LogStream == null)
				return;
			m_LogStream.Close();
			m_LogStream = null;
			m_LogFilePath = "";
			ShowLogHistoryStatus();
		}

		// Starts processing the next job in the job queue.
		//
		private void StartNextJob()
		{
			if (m_LogStream == null)
				OpenLogFile();

			// get next job item
			System.Windows.Forms.ListViewItem JobItem = 
				JobQueueList.Items[m_NextJobIndex];
			++m_NextJobIndex;
			JobInformation JobInfo = (JobInformation)JobItem.Tag;
			JobDefinition JobDef = JobInfo.Definition;

			// update UI to indicate starting job
			m_JobStartTime = DateTime.Now;
			JobStatusBarPanel.Text = "Running";
			JobInfo.StartLogPosition = LogView.TextLength;
			LogHeader(JobDef.Caption);
			JobItem.SubItems[1].Text = "Starting";
			AbortJobToolBarButton.Enabled = true;

			// cache active job item
			//NOTE: Do this just before trying to start the job.
			//		If start succeeds, the output stream threads
			//		read this reference and exit if it's null.
			Debug.Assert(m_ActiveJobListItem == null);
			m_ActiveJobListItem = JobItem;

			JobProcess.StartInfo.WorkingDirectory = JobDef.WorkingDir;
			JobProcess.StartInfo.FileName = JobDef.Application;
			JobProcess.StartInfo.Arguments = JobDef.Arguments;
			JobProcess.StartInfo.UseShellExecute = false;
			JobProcess.StartInfo.RedirectStandardOutput = true;
			JobProcess.StartInfo.RedirectStandardError = true;
			JobProcess.StartInfo.CreateNoWindow = true;

			try
			{
				JobProcess.Start();
			}
			catch (Exception x)
			{
				CompleteActiveJob("Failed to start");
				LogErrorLine
					("Unable to start job process '" + JobDef.Application + 
					"' with arguments '" + JobDef.Arguments + 
					"'.  " + x.Message);
				return;
			}
			
			JobProcess.PriorityClass = m_MainPrefs.ProcessPriority;
			StartProcessOutputMonitoringThreads();
		}

		// Completes processing of a job.
		//
		private void CompleteActiveJob(string ErrorMsg)
		{
			Debug.Assert(m_ActiveJobListItem != null);
			if (ErrorMsg == null)
			{
				m_ActiveJobListItem.SubItems[1].Text = "Done " + FormatElapsedJobTime();
			}
			else
			{
				m_ActiveJobListItem.SubItems[1].Text = ErrorMsg;
				if (m_PauseOnError)
				{
					SetPausedStatus(true);
				}
			}
			m_ActiveJobListItem = null;
			AbortJobToolBarButton.Enabled = false;
		}

		// Updates the process list view with information about the active job's
		// process and its sub-processes.
		//
		private void UpdateProcessList()
		{
			// don't update unless minimum interval has elapsed
			// NOTE: updating is expensive WRT processor use/time
			const int MIN_INTERVAL_SEC = 10;
			TimeSpan TimeSinceLastUpdate = DateTime.Now - m_LastProcessListUpdateTime;
			if (TimeSinceLastUpdate.Seconds < MIN_INTERVAL_SEC) return;
			m_LastProcessListUpdateTime = DateTime.Now;

			if (m_ActiveJobListItem == null)
			{
				ProcessListView.Items.Clear();
			}
			else
			{
				ProcessTools.ProcessEx[] ProcList = 
					ProcessTools.SubProcessQuery.Execute(JobProcess);
				for (int i = 0; i < ProcList.Length; ++i)
				{
					ProcessTools.ProcessEx ProcInfo = ProcList[i];
					string ExeName = ProcInfo.ExeFileName;
					string ProcID = ProcInfo.Process.Id.ToString();
					if (i < ProcessListView.Items.Count)
					{
						ListViewItem ViewItem = ProcessListView.Items[i];
						if (ViewItem.Text != ExeName || ViewItem.SubItems[1].Text != ProcID)
						{
							while (ProcessListView.Items.Count >= i+1)
								ProcessListView.Items.RemoveAt(i);
							ViewItem = ProcessListView.Items.Add(ExeName);
							ViewItem.SubItems.Add(ProcID);
						}
					}
					else
					{
						ListViewItem ViewItem = ProcessListView.Items.Add(ExeName);
						ViewItem.SubItems.Add(ProcID);
					}
				}
			}
		}

		private void JobTimer_Tick(object sender, System.EventArgs e)
		{
			try
			{
				try
				{
					UpdateProcessList();
				}
				catch (Exception x)
				{
					// add information useful for debugging an error
					throw new Exception("UpdateProcessList failed.  " + x.ToString());
				}

				try
				{
					ProcessQueuedLogLines();
				}
				catch (Exception x)
				{
					// add information useful for debugging an error
					throw new Exception("ProcessQueuedLogLines failed.  " + x.ToString());
				}
                
				if (m_ActiveJobListItem != null)
				{
					m_ActiveJobListItem.SubItems[1].Text = "Running " + FormatElapsedJobTime();
				}
				else
				{
					if (m_Paused)
					{
						JobStatusBarPanel.Text = "Paused";
					}
					else if (JobQueueList.Items.Count > m_NextJobIndex)
					{
						JobStatusBarPanel.Text = "Starting next job";
						StartNextJob();
					}
					else
					{
						JobStatusBarPanel.Text = "Ready";
					}
				}
			}
			catch (Exception x)
			{
				JobTimer.Enabled = false;
				m_Prefs.ShowError("Error in job timer handler.  " + x.Message);
			}
		}

		private void JobProcess_Exited(object sender, System.EventArgs e)
		{
			JobInformation JobInfo = (JobInformation)m_ActiveJobListItem.Tag;
			string ErrorMsg = null;
			if (JobProcess.ExitCode != JobInfo.Definition.SuccessStatus)
			{
				ErrorMsg = "Error (" + JobProcess.ExitCode.ToString() + ") " + FormatElapsedJobTime();
			}
			CompleteActiveJob(ErrorMsg);
			WaitForOutputMonitoringThreadsToEnd();
		}

		private void JobsMenuItem_Popup(object sender, System.EventArgs e)
		{
			AbortActiveJobMenuItem.Enabled = m_ActiveJobListItem != null;
			RemoveCompletedJobsMenuItem.Enabled = m_NextJobIndex > 0;
			RemovePendingJobsMenuItem.Enabled = JobQueueList.Items.Count > m_NextJobIndex;
			RemoveAllJobsMenuItem.Enabled = JobQueueList.Items.Count > 0;
			RemoveSelectedJobsMenuItem.Enabled = JobQueueList.SelectedItems.Count > 0;
			PauseMenuItem.Enabled = !m_Paused;
			ResumeMenuItem.Enabled = m_Paused;
			PauseOnErrorMenuItem.Enabled = !m_PauseOnError;
			ContinueOnErrorMenuItem.Enabled = m_PauseOnError;
		}

		private void AbortActiveJob()
		{
			if (m_ActiveJobListItem != null)
			{
				if (!JobProcess.HasExited)
				{
					ProcessTools.ProcessEnder.ExitProcessTree(JobProcess);
				}
			}
		}

		private void AbortActiveJobMenuItem_Click(object sender, System.EventArgs e)
		{
			string Msg = 
				"Stopping the active job kills the top-level build process and all " +
				"processes that seem to be sub-process.  In rare situations " +
				"non-sub-processes are killed.";
			System.Windows.Forms.DialogResult DlgResult = 
				MessageBox.Show
				(Msg, 
				this.Text, 
				MessageBoxButtons.OKCancel, 
				MessageBoxIcon.Warning);
			if (DlgResult == System.Windows.Forms.DialogResult.Cancel)
				return;

			AbortActiveJob();
		}

		private bool ActiveJobExists()
		{
			return (m_ActiveJobListItem != null);
		}

		private bool AbortActiveJobConfirmed()
		{
			string Msg = "Abort the running job?";
			if (MessageBox.Show(Msg, this.Text, 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question) == 
				System.Windows.Forms.DialogResult.No)
				return false;
			AbortActiveJob();
			return true;
		}

		private void RemoveSelectedJobsMenuItem_Click(object sender, System.EventArgs e)
		{
			if (ActiveJobExists())
			{
				foreach (ListViewItem Item in JobQueueList.SelectedItems)
				{
					if (Item == m_ActiveJobListItem)
					{
						if (!AbortActiveJobConfirmed())
							return;
					}
				}
			}
            int LastIndex = -1;
			foreach (ListViewItem Item in JobQueueList.SelectedItems)
			{
				if (Item.Index < m_NextJobIndex)
				{
					--m_NextJobIndex;
				}
                if (Item.Index > LastIndex)
                    LastIndex = Item.Index;
                JobQueueList.Items.Remove(Item);
			}
            if (LastIndex >= 0)
            {
                if (LastIndex >= JobQueueList.Items.Count)
                {
                    if (LastIndex > 0)
                    {
                        JobQueueList.Items[LastIndex - 1].Selected = true;
                    }
                }
                else
                {
                    JobQueueList.Items[LastIndex].Selected = true;
                }
            }
		}

		private void RemoveCompletedJobsMenuItem_Click(object sender, System.EventArgs e)
		{
			//remove items from 0 to just before active item, if any
			int CountToRemove = m_NextJobIndex;
			if (m_ActiveJobListItem != null)
			{
				--CountToRemove;
			}
			for (int i = 0; i < CountToRemove; ++i)
			{
				JobQueueList.Items.RemoveAt(0);
				--m_NextJobIndex;
			}
		}

		private void RemovePendingJobsMenuItem_Click(object sender, System.EventArgs e)
		{
			//remove items from m_NextJobIndex to the end
			int CountToRemove = JobQueueList.Items.Count - m_NextJobIndex;
			for (int i = 0; i < CountToRemove; ++i)
			{
				JobQueueList.Items.RemoveAt(m_NextJobIndex);
			}
		}

		private void RemoveAllJobsMenuItem_Click(object sender, System.EventArgs e)
		{
			if (ActiveJobExists())
				if (!AbortActiveJobConfirmed())
					return;
			JobQueueList.Items.Clear();
			m_NextJobIndex = 0;
		}

		private void SetPausedStatus(bool To)
		{
			m_Paused = To;
			ShowPausedStatus();
		}

		private void ShowPausedStatus()
		{
			if (m_Paused)
			{
				JobQueueList.BackColor = Color.LightGray;
				PauseJobsToolBarButton.ToolTipText = "Resume";
				PauseJobsToolBarButton.ImageIndex = 20;
			}
			else
			{
				JobQueueList.BackColor = System.Drawing.SystemColors.Window;
				PauseJobsToolBarButton.ToolTipText = "Pause";
				PauseJobsToolBarButton.ImageIndex = 22;
			}
		}

		private void ShowOnErrorStatus()
		{
			if (m_PauseOnError)
				IgnoreErrStatusBarPanel.Text = "";
			else
				IgnoreErrStatusBarPanel.Text = "Ignoring Errors";
		}

		private void ShowLogHistoryStatus()
		{
			if (m_LogStream == null)
			{
				LogHistoryStatusBarPanel.Text = "";
			}
			else
			{
				LogHistoryStatusBarPanel.Text = m_LogFilePath;
			}
		}

		private void PauseMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPausedStatus(true);
		}

		private void ResumeMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPausedStatus(false);
		}

		private void PauseOnErrorMenuItem_Click(object sender, System.EventArgs e)
		{
			m_PauseOnError = true;
			ShowOnErrorStatus();
		}

		private void ContinueOnErrorMenuItem_Click(object sender, System.EventArgs e)
		{
			m_PauseOnError = false;
			ShowOnErrorStatus();
		}

		private void SaveLogMenuItem_Click(object sender, System.EventArgs e)
		{
			lock(this)
			{
				if (LogSaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
					return;
				if (LogSaveFileDialog.FilterIndex == 2)
					LogView.SaveFile(LogSaveFileDialog.FileName, System.Windows.Forms.RichTextBoxStreamType.PlainText);
				else if (LogSaveFileDialog.FilterIndex == 3)
					LogView.SaveFile(LogSaveFileDialog.FileName, System.Windows.Forms.RichTextBoxStreamType.UnicodePlainText);
				else
					LogView.SaveFile(LogSaveFileDialog.FileName, System.Windows.Forms.RichTextBoxStreamType.RichText);
			}
		}

		private void ClearLogText()
		{
			LogView.Clear();
			foreach (ListViewItem Item in JobQueueList.Items)
			{
				JobInformation JobInfo = (JobInformation)Item.Tag;
				JobInfo.StartLogPosition = -1;
			}
		}

		private void ClearLogText(int CutEnd)
		{
			if (CutEnd < 0)
			{
				ClearLogText();
			}
			else
			{
				LogView.SelectionStart = 0;
				LogView.SelectionLength = CutEnd;
				LogView.ReadOnly = false;
				LogView.SelectedText = "";
				LogView.ReadOnly = true;
				foreach (ListViewItem Item in JobQueueList.Items)
				{
					JobInformation JobInfo = (JobInformation)Item.Tag;
					JobInfo.StartLogPosition -= CutEnd;
				}
			}
		}

		private void ClearLogMenuItem_Click(object sender, System.EventArgs e)
		{
			lock(this)
			{
				ClearLogText();
			}
		}

		// Returns the position of the end-of-line character that follows the
		// specified position or -1 if not found.
		//
		private int FindNextEOL(string Text, int Position)
		{
			for (int i = Position; i<Text.Length; ++i)
				if (Text[i] == '\n')
					return i;
			return -1;
		}

		// Returns the position of the end-of-line character that preceeds the
		// specified position or -1 if not found.
		//
		private int FindPrevEOL(string Text, int Position)
		{
			for (int i = Position-1; i>=0; --i)
				if (Text[i] == '\n')
					return i;
			return -1;
		}

		private void ClearToLineMenuItem_Click(object sender, System.EventArgs e)
		{
			lock(this)
			{
				int SelEnd = LogView.SelectionStart;
				int PrevEolPos = FindPrevEOL(LogView.Text, SelEnd);
				if (PrevEolPos >= 0)
					ClearLogText(PrevEolPos+1);
			}
		}

		// Returns the position of the start of the first line of the last
		// job log section that preceeds the specified position.
		//
		private int FindPrevJobSection(int Position)
		{
			int Result = -1;
			foreach (ListViewItem Item in JobQueueList.Items)
			{
				JobInformation JobInfo = (JobInformation)Item.Tag;
				if (JobInfo.StartLogPosition > Position)
					return Result;
				Result = JobInfo.StartLogPosition;
			}
			return Result;
		}

		private void ClearToJobMenuItem_Click(object sender, System.EventArgs e)
		{
			lock(this)
			{
				int PrevJobPos = FindPrevJobSection(LogView.SelectionStart);
				ClearLogText(PrevJobPos);
			}
		}

		private void LogMenu_Popup(object sender, System.EventArgs e)
		{
			bool LogHasData = LogView.TextLength > 0;
			SaveLogMenuItem.Enabled = LogHasData;
			ClearLogMenuItem.Enabled = LogHasData;
			SaveLogContextMenuItem.Enabled = LogHasData;
			ClearLogContextMenuItem.Enabled = LogHasData;
			ClearToJobLogContextMenuItem.Enabled = LogHasData;
			ClearToLineLogContextMenuItem.Enabled = LogHasData;
			Days7PurgeAgeHistoryMenuItem.Checked = m_MainPrefs.MaxLogFileAge == 7;
			Days14PurgeAgeHistoryMenuItem.Checked = m_MainPrefs.MaxLogFileAge == 14;
			Days30PurgeAgeHistoryMenuItem.Checked = m_MainPrefs.MaxLogFileAge == 30;
			Days60PurgeAgeHistoryMenuItem.Checked = m_MainPrefs.MaxLogFileAge == 60;
			Days90PurgeAgeHistoryMenuItem.Checked = m_MainPrefs.MaxLogFileAge == 90;
			NeverPurgeHistoryMenuItem.Checked = m_MainPrefs.MaxLogFileAge == 0;
		}

		private void ResetMenuItem_Click(object sender, System.EventArgs e)
		{
			if (ActiveJobExists())
				if (!AbortActiveJobConfirmed())
					return;
			JobQueueList.Items.Clear();
			m_NextJobIndex = 0;
			lock(this)
			{
				LogView.Clear();
			}
			SetPausedStatus(false);
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			//if (ActiveJobExists())
			//	if (!AbortActiveJobConfirmed())
			//		return;
			this.Close();
		}

		private void AddMenuItem_Click(object sender, System.EventArgs e)
		{
			m_AddJobForm.Execute(JobQueueList);
		}

		private void AboutMenuItem_Click(object sender, System.EventArgs e)
		{
			AboutForm AboutForm = new AboutForm();
			AboutForm.ShowDialog(this);
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			m_MainPrefs = new MainPrefs(m_Prefs);
			ShowPausedStatus();
			ShowOnErrorStatus();
			ShowLogHistoryStatus();
			PurgeLogs();
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (ActiveJobExists())
			{
				if (!AbortActiveJobConfirmed())
				{
					e.Cancel = true;
					return;
				}
			}

			if (m_MainPrefs != null)
			{
				m_MainPrefs.Save(m_Prefs);
			}
		}

		// The sole purpose for this timer is to show the add-jobs dialog
		// when the app starts.
		//
		private void StartupTimer_Tick(object sender, System.EventArgs e)
		{
			StartupTimer.Enabled = false;
			AddMenuItem.PerformClick();		
		}

		private void StdErrThreadProc()
		{
			try
			{
				System.IO.StreamReader Stream = JobProcess.StandardError;
				string Line;
				while (m_ActiveJobListItem != null)
					while ((Line = Stream.ReadLine()) != null)
						QueueLogLine(Line, 'e');
			}
			catch (Exception e)
			{
				m_Prefs.ShowError("Error in StdErrThreadProc.  " + e.Message);
			}
		}

		private void StdOutThreadProc()
		{
			try
			{
				System.IO.StreamReader Stream = JobProcess.StandardOutput;
				string Line;
				while (m_ActiveJobListItem != null)
					while ((Line = Stream.ReadLine()) != null)
						QueueLogLine(Line, 'o');
			}
			catch (Exception e)
			{
				m_Prefs.ShowError("Error in StdOutThreadProc.  " + e.Message);
			}
		}

		private void StartProcessOutputMonitoringThreads()
		{
			m_StdOutThread = new System.Threading.Thread(new System.Threading.ThreadStart(StdOutThreadProc));
			m_StdErrThread = new System.Threading.Thread(new System.Threading.ThreadStart(StdErrThreadProc));
			m_StdErrThread.Start();
			m_StdOutThread.Start();
		}

		private void WaitForOutputMonitoringThreadsToEnd()
		{
			m_StdOutThread.Join();
			m_StdErrThread.Join();
		}

		private void JobQueueMenu_Popup(object sender, System.EventArgs e)
		{
			bool OneJobSelected = JobQueueList.SelectedItems.Count == 1;
			RemoveJobQueueMenuItem.Enabled = JobQueueList.SelectedItems.Count > 0;
			ResumeHereJobQueueMenuItem.Enabled = 
				m_Paused && 
				m_ActiveJobListItem == null &&
				OneJobSelected && 
				JobQueueList.SelectedIndices[0] < m_NextJobIndex;
			FindInLogJobQueueMenuItem.Enabled = OneJobSelected && 
				((JobInformation)JobQueueList.SelectedItems[0].Tag).StartLogPosition>=0;
			PropertiesJobQueueMenuItem.Enabled = OneJobSelected;
		}

		private void ResumeHereMenuItem_Click(object sender, System.EventArgs e)
		{
			Debug.Assert(m_Paused);
			Debug.Assert(JobQueueList.SelectedIndices.Count == 1);
			int SelectedIndex = JobQueueList.SelectedIndices[0];
			for (int i = SelectedIndex; i < JobQueueList.Items.Count; ++i)
			{
				JobQueueList.Items[i].SubItems[1].Text = "Pending";
			}
			m_NextJobIndex = SelectedIndex;
			SetPausedStatus(false);
		}

		private void FindInLogView()
		{
			if (JobQueueList.SelectedItems.Count != 1)
			{
				m_Prefs.ShowError("Select just one job for find-in-log.");
				return;
			}
			JobInformation JobInfo = (JobInformation)JobQueueList.SelectedItems[0].Tag;
			if (JobInfo.StartLogPosition < 0)
			{
				// move cursor to end
				LogView.SelectionStart = LogView.TextLength;
				LogView.SelectionLength = 0;
			}
			else
			{
				// move cursor to heading and select line
				LogView.SelectionStart = JobInfo.StartLogPosition;
				int NextEolPos = FindNextEOL(LogView.Text, JobInfo.StartLogPosition);
				if (NextEolPos > 0)
					LogView.SelectionLength = NextEolPos - JobInfo.StartLogPosition;
				else
					LogView.SelectionLength = LogView.TextLength - LogView.SelectionStart;
			}
			LogView.Focus();
		}

		private void FindInLogJobQueueMenuItem_Click(object sender, System.EventArgs e)
		{
			FindInLogView();
		}

		private void PropertiesJobQueueMenuItem_Click(object sender, System.EventArgs e)
		{
			if (m_JobInfoDialog.IsDisposed)
				m_JobInfoDialog = new JobInfoDialog();
			UpdateJobInfoView();
			m_JobInfoDialog.Show();		
		}

		private void ToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button == AddJobsToolBarButton)
				AddMenuItem.PerformClick();
			else if (e.Button == AbortJobToolBarButton)
				AbortActiveJobMenuItem.PerformClick();
			else if (e.Button == PauseJobsToolBarButton)
				SetPausedStatus(!m_Paused);
			else if (e.Button == RemoveAllJobsToolBarButton)
				RemoveAllJobsMenuItem.PerformClick();
			else if (e.Button == ResetAllToolBarButton)
				ResetMenuItem.PerformClick();
		}

		private void FindMenuItem_Click(object sender, System.EventArgs e)
		{
			m_FindForm.Find(LogView);
		}

		private void FindNextMenuItem_Click(object sender, System.EventArgs e)
		{
			m_FindForm.FindNext(LogView);		
		}

		private void PriorityMenuItem_Select(object sender, System.EventArgs e)
		{
			HighPriorityMenuItem.Checked = (m_MainPrefs.ProcessPriority == ProcessPriorityClass.High);
			AbovePriorityMenuItem.Checked = (m_MainPrefs.ProcessPriority == ProcessPriorityClass.AboveNormal);
			NormalPriorityMenuItem.Checked = (m_MainPrefs.ProcessPriority == ProcessPriorityClass.Normal);
			BelowPriorityMenuItem.Checked = (m_MainPrefs.ProcessPriority == ProcessPriorityClass.BelowNormal);
			IdlePriorityMenuItem.Checked = (m_MainPrefs.ProcessPriority == ProcessPriorityClass.Idle);
		}

		private void SetPriority(ProcessPriorityClass To)
		{
			m_MainPrefs.ProcessPriority = To;
			if (ActiveJobExists())
			{
				ProcessTools.ProcessEx[] ProcList = 
					ProcessTools.SubProcessQuery.Execute(JobProcess);
				foreach (ProcessTools.ProcessEx ProcInfo in ProcList)
				{
					ProcInfo.Process.PriorityClass = To;
				}
			}
		}

		private void HighPriorityMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPriority(ProcessPriorityClass.High);
		}

		private void AbovePriorityMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPriority(ProcessPriorityClass.AboveNormal);
		}

		private void NormalPriorityMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPriority(ProcessPriorityClass.Normal);
		}

		private void BelowPriorityMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPriority(ProcessPriorityClass.BelowNormal);
		}

		private void IdlePriorityMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPriority(ProcessPriorityClass.Idle);
		}

		private void UpdateJobInfoView()
		{
			if (JobQueueList.SelectedItems.Count == 1)
			{
				JobInformation JobInfo = (JobInformation)JobQueueList.SelectedItems[0].Tag;
				m_JobInfoDialog.LoadInfo(JobInfo.Definition);
			}
			else
			{
				m_JobInfoDialog.ClearInfo(JobQueueList.SelectedItems.Count);
			}
		}

		private void JobQueueList_DoubleClick(object sender, System.EventArgs e)
		{
			FindInLogView();
		}

		private void JobQueueList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateJobInfoView();
			if (m_JobInfoDialog.Visible)
			{
				UpdateJobInfoView();
			}		
		}

		private void ExploreHistoryMenuItem_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process ExplorerProcess = new System.Diagnostics.Process();
			ExplorerProcess.StartInfo.FileName = "explorer.exe";
			ExplorerProcess.StartInfo.Arguments = GetLogFolderPath();
			ExplorerProcess.Start();
		}

		public StringCollection FindOldLogs(int MaxAge)
		{
			StringCollection Result = new StringCollection();
			if (MaxAge <= 0)
				return Result;
			string[] Paths = System.IO.Directory.GetFiles(GetLogFolderPath(), "*.log");
			foreach (string Path in Paths)
			{
				System.IO.FileInfo FileInfo = new System.IO.FileInfo(Path);
				TimeSpan Age = DateTime.Now - FileInfo.LastWriteTime;
				if (Age.Days > MaxAge)
					Result.Add(Path);
			}
			return Result;
		}

		public void DeleteLogFiles(StringCollection Paths)
		{
			foreach (string Path in Paths)
			{
				Debug.WriteLine("Deleting old log file '" + Path + "'...");
				try
				{
					System.IO.File.Delete(Path);
				}
				catch (Exception x)
				{
					Debug.WriteLine("Unable to delete old log file '" + Path + "'.  " + x.Message);
				}			
			}
		}

		public void PurgeLogs()
		{
			int MaxAge = m_MainPrefs.MaxLogFileAge;
			StringCollection OldLogPaths = FindOldLogs(MaxAge);
			if (OldLogPaths.Count > 0)
			{
				JobStatusBarPanel.Text = "Deleting " + OldLogPaths.Count + " old history log file(s)...";
				/*
				string Msg = 
					"Found " + OldLogPaths.Count + 
					" history log file(s) older than the maximum age allowed (" + MaxAge + 
					" days).  Do you want to permanently delete the file(s)?";
				if (m_Prefs.PromptQuestion(Msg))
				*/
				DeleteLogFiles(OldLogPaths);
			}
		}

		private void SetPurgeAge(int MaxAge)
		{
			StringCollection LogPaths = FindOldLogs(MaxAge);
			if (LogPaths.Count > 0)
			{
				string Msg = LogPaths.Count + " history log files will be deleted.";
				if (!m_Prefs.PromptWarning(Msg))
					return;
				DeleteLogFiles(LogPaths);
			}
			m_MainPrefs.MaxLogFileAge = MaxAge;
		}

		private void Days7PurgeAgeHistoryMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPurgeAge(7);
		}

		private void Days14PurgeAgeHistoryMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPurgeAge(14);
		}

		private void Days30PurgeAgeHistoryMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPurgeAge(30);
		}

		private void Days60PurgeAgeHistoryMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPurgeAge(60);
		}

		private void Days90PurgeAgeHistoryMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPurgeAge(90);
		}

		private void NeverPurgeHistoryMenuItem_Click(object sender, System.EventArgs e)
		{
			SetPurgeAge(0);
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			string LineText = " This is the time for all good men to come to the aid of their";
			int CharCount = 0;
			StringCollection Lines = new StringCollection();
			for (int i = 0; i < 1000; ++i)
			{
				CharCount += LineText.Length;
				string CharCountText = "" + CharCount;
				CharCount += CharCountText.Length;
				Lines.Add("e" + CharCountText + LineText);
				//Lines.Add("e" + i + " This is the time for all good men to come to the aid of their party.");
			}
			LogLines(Lines);
		}

		private bool m_LastStatusBarClickOnLogFilePath = false;
		private void StatusBar_PanelClick(object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e)
		{
			m_LastStatusBarClickOnLogFilePath = (e.StatusBarPanel == LogHistoryStatusBarPanel);
		}

		private void StatusBar_DoubleClick(object sender, System.EventArgs e)
		{
			if (m_LastStatusBarClickOnLogFilePath && m_LogFilePath.Length > 0)
			{
				System.Diagnostics.Process Proc = new Process();
				Proc.StartInfo.FileName = m_LogFilePath;
				Proc.StartInfo.UseShellExecute = true;
				Proc.Start();
			}
		}
	}
}
