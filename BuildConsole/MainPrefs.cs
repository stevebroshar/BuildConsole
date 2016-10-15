using System;
using System.Diagnostics;
using System.Collections;

namespace BuildConsole
{
	class MainPrefs : System.Object
	{
		private ProcessPriorityClass m_ProcessPriority = ProcessPriorityClass.Normal;
		private int m_MaxLogFileAge = 7;

		public ProcessPriorityClass ProcessPriority
		{
			get {return m_ProcessPriority;}
			set {m_ProcessPriority = value;} 
		}

		public int MaxLogFileAge
		{
			get {return m_MaxLogFileAge;}
			set {m_MaxLogFileAge = value;} 
		}

		private void Load(Preferences Prefs)
		{
			Microsoft.Win32.RegistryKey Reg = Prefs.OpenKeyForLoading();			
			if (Reg == null) return;

			try
			{
				string Value = Reg.GetValue("ProcessPriority").ToString();
				m_ProcessPriority = 
					(ProcessPriorityClass)
					System.Enum.Parse(typeof(ProcessPriorityClass), Value);
			} 
			catch (Exception x)
			{
				Debug.WriteLine("Unable to load ProcessPriority preference.  " + x.ToString());
			}

			try
			{
				string Value = Reg.GetValue("MaxLogFileAge").ToString();
				m_MaxLogFileAge = Convert.ToInt32(Value);
			} 
			catch (Exception x)
			{
				Debug.WriteLine("Unable to load MaxLogFileAge preference.  " + x.ToString());
			}
		}

		public void Save(Preferences Prefs)
		{
			Microsoft.Win32.RegistryKey Reg = Prefs.OpenKeyForSaving();
			Reg.SetValue("ProcessPriority", m_ProcessPriority.ToString());
			Reg.SetValue("MaxLogFileAge", m_MaxLogFileAge.ToString());
		}

		public MainPrefs(Preferences Prefs)
		{
			Load(Prefs);
		}

	}

}
