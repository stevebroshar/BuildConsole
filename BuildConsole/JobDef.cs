using System;
using System.Collections;
using System.Diagnostics;

namespace BuildConsole
{
	// Information that defines a job.
	//
	public class JobDefinition
	{
		private string m_Caption = "";
		private string m_Hint = "";
		private string m_Application = "";
		private string m_Arguments = "";
		private string m_WorkingDir = "";
		private long m_SuccessStatus = 0;

		public JobDefinition()
		{
		}

		public string Caption
		{
			get {return m_Caption;}
			set {m_Caption = value;}
		}

		public string Hint
		{
			get {return m_Hint;}
			set {m_Hint = value;}
		}

		public string Application
		{
			get {return m_Application;}
			set {m_Application = value;}
		}

		public string Arguments
		{
			get {return m_Arguments;}
			set {m_Arguments = value;}
		}

		public string WorkingDir
		{
			get {return m_WorkingDir;}
			set {m_WorkingDir = value;}
		}

		public long SuccessStatus
		{
			get {return m_SuccessStatus;}
			set {m_SuccessStatus = value;}
		}

	}

	// A group of job definitions.
	//
	public class JobDefinitionGroup
	{
		private string m_Caption = "";
		private string m_Hint = "";
		private ArrayList m_Items = new ArrayList();

		public JobDefinitionGroup()
		{
		}

		public string Caption
		{
			get {return m_Caption;}
			set {m_Caption = value;}
		}

		public string Hint
		{
			get {return m_Hint;}
			set {m_Hint = value;}
		}

		public JobDefinition[] Items
		{
			get {return (JobDefinition[])m_Items.ToArray(typeof(JobDefinition));}
		}

		public JobDefinition Add()
		{
			JobDefinition NewItem = new JobDefinition();
			m_Items.Add(NewItem);
			return NewItem;
		}

	}

	// A persistable set of job groups.
	//
	public class JobDefinitionSet
	{
		private string m_FilePath = "";
		private string m_Caption = "";
		private ArrayList m_Groups = new ArrayList();

		public JobDefinitionSet()
		{
		}

		public string FilePath
		{
			get {return m_FilePath;}
			set {m_FilePath = value;}
		}

		public string Caption
		{
			get {return m_Caption;}
			set {m_Caption = value;}
		}

		public JobDefinitionGroup[] Groups
		{
			get {return (JobDefinitionGroup[])m_Groups.ToArray(typeof(JobDefinitionGroup));}
		}

		public void Clear()
		{
			m_FilePath = "";
			m_Caption = "";
			m_Groups.Clear();
		}

		private JobDefinitionGroup AddGroup()
		{
			JobDefinitionGroup NewGroup = new JobDefinitionGroup();
			m_Groups.Add(NewGroup);
			return NewGroup;
		}

		private void LoadFromXml(System.Xml.XmlTextReader xml)
		{
			Clear();
			JobDefinitionGroup LastGroup = null;
			JobDefinition LastJob = null;

				while (!xml.EOF)
				{
					xml.Read();
					if (xml.NodeType == System.Xml.XmlNodeType.Element)
					{
						if (xml.Depth == 1)
						{
							System.Diagnostics.Debug.Assert(xml.Name == "Menu");
							m_Caption = xml.GetAttribute("Caption");
						}
						else if (xml.Depth == 2)
						{
							System.Diagnostics.Debug.Assert(xml.Name == "Group");
							LastGroup = AddGroup();
							LastGroup.Caption = xml.GetAttribute("Caption");
							LastGroup.Hint = xml.GetAttribute("Hint");
						}
						else if (xml.Depth == 3)
						{
							System.Diagnostics.Debug.Assert(xml.Name == "Job");
							LastJob = LastGroup.Add();
							LastJob.Caption = xml.GetAttribute("Caption");
							LastJob.Hint = xml.GetAttribute("Hint");
							LastJob.Application = xml.GetAttribute("Application");
							string OptVal;
							OptVal = xml.GetAttribute("WorkingDir");
							if (OptVal != null)
								LastJob.WorkingDir = OptVal;
							OptVal = xml.GetAttribute("SuccessStatus");
							if (OptVal != null)
                                LastJob.SuccessStatus = Convert.ToInt32(OptVal);
						}
						else if (xml.Depth == 4)
						{
							System.Diagnostics.Debug.Assert(xml.Name == "Arguments");
							LastJob.Arguments = xml.ReadString();
						}
					}
				}
		}

		public void LoadFromFile(string FilePath)
		{
			if (FilePath.Length == 0) FilePath = m_FilePath;
			System.Xml.XmlTextReader xml = new System.Xml.XmlTextReader(FilePath);
			try
			{
				LoadFromXml(xml);
				xml.Close();
			}
			catch (Exception x)
			{
				xml.Close();
				throw new Exception("Unable to load job definition file '" + FilePath + "'.  " + x.ToString());
			}
			m_FilePath = FilePath;
		}

		public void SaveToFile(string FilePath)
		{
			throw new Exception("Not implemented.");
		}

	}

	// Information associated with a job instance.
	//
	public class JobInformation
	{	
		private JobDefinition m_Def;
		private int m_StartLogPosition = -1;

		public JobInformation(JobDefinition Definition)
		{
			Debug.Assert(Definition != null);
			m_Def = Definition;
		}

		public JobDefinition Definition
		{
			get {return m_Def;}
		}

		public int StartLogPosition
		{
			get {return m_StartLogPosition;}
			set {m_StartLogPosition = value;}
		}

	}

}
