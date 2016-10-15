using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace ProcessTools
{
	class ProcessEntry
	{
		// Win32 C++ declaration:
		//	typedef struct tagPROCESSENTRY32 
		//	{
		//		DWORD dwSize;
		//		DWORD cntUsage;
		//		DWORD th32ProcessID;
		//		ULONG_PTR th32DefaultHeapID;
		//		DWORD th32ModuleID;
		//		DWORD cntThreads;
		//		DWORD th32ParentProcessID;
		//		LONG pcPriClassBase;
		//		DWORD dwFlags;
		//		TCHAR szExeFile[MAX_PATH];
		//	} PROCESSENTRY32, *PPROCESSENTRY32;

		// structure definition constants
		private const int INT_SIZE = 4;
		private const int MAX_PATH = 260;
		private const int SIZE_OFFSET = 0;
		private const int USAGE_OFFSET = SIZE_OFFSET + INT_SIZE;
		private const int PROCESSID_OFFSET = USAGE_OFFSET + INT_SIZE;
		private const int DEFAULTHEAPID_OFFSET = PROCESSID_OFFSET + INT_SIZE;
		private const int MODULEID_OFFSET = DEFAULTHEAPID_OFFSET + INT_SIZE;
		private const int THREADS_OFFSET = MODULEID_OFFSET + INT_SIZE;
		private const int PARENTPROCESSID_OFFSET = THREADS_OFFSET + INT_SIZE;
		private const int PRICLASSBASE_OFFSET = PARENTPROCESSID_OFFSET + INT_SIZE;
		private const int FLAGS_OFFSET = PRICLASSBASE_OFFSET + INT_SIZE;
		private const int EXEFILE_OFFSET = FLAGS_OFFSET + INT_SIZE;
		private const int SIZE = EXEFILE_OFFSET + MAX_PATH*2;

		// data members
		public uint dwSize; 
		public uint cntUsage; 
		public uint th32ProcessID; 
		public uint th32DefaultHeapID; 
		public uint th32ModuleID; 
		public uint cntThreads; 
		public uint th32ParentProcessID; 
		public uint pcPriClassBase; 
		public uint dwFlags; 
		public string szExeFile;
	
		public ProcessEntry()
		{
		}

		// Creates an instance based on a ToolHelp API byte array.
		//
		public ProcessEntry(byte[] aData)
		{
			dwSize = GetUInt(aData, SIZE_OFFSET);
			cntUsage = GetUInt(aData, USAGE_OFFSET);
			th32ProcessID = GetUInt(aData, PROCESSID_OFFSET);
			th32DefaultHeapID = GetUInt(aData, DEFAULTHEAPID_OFFSET);
			th32ModuleID = GetUInt(aData, MODULEID_OFFSET);
			cntThreads = GetUInt(aData, THREADS_OFFSET);
			th32ParentProcessID = GetUInt(aData, PARENTPROCESSID_OFFSET);
			pcPriClassBase = (uint)GetUInt(aData, PRICLASSBASE_OFFSET);
			dwFlags = GetUInt(aData, FLAGS_OFFSET);
			szExeFile = GetString(aData, EXEFILE_OFFSET, MAX_PATH);
		}

		// Gets a uint from a byte array.
		//
		private static uint GetUInt(byte[] aData, int Offset)
		{
			return BitConverter.ToUInt32(aData, Offset);
		}
	
		// Sets a uint in a byte array.
		//
		private static void SetUInt(byte[] aData, int Offset, int Value)
		{
			byte[] buint = BitConverter.GetBytes(Value);
			Buffer.BlockCopy(buint, 0, aData, Offset, buint.Length);
		}

		// Gets a unicode string from a byte array.
		//
		private static string GetString(byte[] aData, int Offset, int Length)
		{
			String sReturn =  Encoding.Unicode.GetString(aData, Offset, Length);
			return sReturn;
		}

		// Streams the object to the byte array required by the ToolHelp API.
		//
		public byte[] ToByteArray()
		{
			byte[] aData = new byte[SIZE];
			SetUInt(aData, SIZE_OFFSET, SIZE);
			return aData;
		}

		// Extracts the process name.
		//
		public string GetName()
		{
			return szExeFile.Substring(0, szExeFile.IndexOf('\0'));
		}
	}

	public class ProcessEx
	{
		Process m_Process;
		private int m_CreatorId;
		private string m_ExeFileName;

		public Process Process
		{
			get {return m_Process;}
		}
		public int CreatorId
		{
			get {return m_CreatorId;}
		}
		public string ExeFileName
		{
			get {return m_ExeFileName;}
		}

		public ProcessEx(Process Process, int CreatorId, string ExeFileName)
		{
			m_Process = Process;
			m_CreatorId = CreatorId;
			m_ExeFileName = ExeFileName;
		}

	}

	public class ProcessQuery
	{
		#region PInvoke declarations

		//private const int INVALID_HANDLE_VALUE = -1;
		private const int TH32CS_SNAPPROCESS = 0x00000002;
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
		[DllImport("kernel32.dll")]
		public static extern int Process32FirstW(IntPtr handle, byte[] pe);
		[DllImport("kernel32.dll")]
		public static extern int Process32NextW(IntPtr handle, byte[] pe);
		private const int PROCESS_TERMINATE = 1;
		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(IntPtr handle);

		#endregion

		public static ProcessEx[] Execute()
		{
			IntPtr SnapshotHandle = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
			if ((int)SnapshotHandle <= 0)
				throw new Exception("Unable to create process snapshot.");

			ArrayList ProcList = new ArrayList();
			try
			{
				// get byte array to pass to the API calls
				ProcessEntry peBuffer = new ProcessEntry();
				byte[] peBytes = peBuffer.ToByteArray();
				
				// get the first snapshot item
				int retval = Process32FirstW(SnapshotHandle, peBytes);

				while (retval == 1)
				{
					// convert bytes to class
					ProcessEntry pe = new ProcessEntry(peBytes);

					// new process info item
					int PID = (int)pe.th32ProcessID;
					Process NewProc = Process.GetProcessById(PID); // NOTE: this is time-expensive
					ProcessEx NewProcEx = new ProcessEx(NewProc, (int)pe.th32ParentProcessID, pe.GetName());
					ProcList.Add(NewProcEx);

					// get next snapshot item
					retval = Process32NextW(SnapshotHandle, peBytes);
				}
			}
			catch (Exception ex)
			{
				//throw new Exception("Exception: " + ex.Message);
				Debug.WriteLine("Ignoring process.  " + ex.ToString());
			}
			
			CloseHandle(SnapshotHandle); 
			
			return (ProcessEx[])ProcList.ToArray(typeof(ProcessEx));
		}
	}

	class SubProcessQuery
	{
		// Returns a list of process infos that includes the specified process
		// and the line of sub-processes created directly and indirectly from
		// this process.  This function assumes that no process has more than
		// one sub-process.
		//
		public static ProcessEx[] Execute(Process RootProcess)
		{
			int PID = RootProcess.Id;
			ArrayList SubProcArray = new ArrayList();

			// get list of active processes
			ProcessEx[] ProcessList = ProcessQuery.Execute();

			// index list by creator-id
			Hashtable ProcsByCreator = new Hashtable();
			foreach (ProcessEx Proc in ProcessList)
			{
				if (Proc.Process.Id == PID)
				{
					SubProcArray.Add(Proc);
				}
				if (!ProcsByCreator.Contains(Proc.CreatorId))
				{
					ProcsByCreator.Add(Proc.CreatorId, Proc);
				}
			}

			// determine process hierarchy
			while (true)
			{
				ProcessEx Proc = (ProcessEx)ProcsByCreator[PID];
				if (Proc == null)
					break;
				SubProcArray.Add(Proc);
				if (SubProcArray.Count > 31)
					break; // prevent infinite loop which can happen in weird situations
				PID = Proc.Process.Id;
			}
			return (ProcessEx[])SubProcArray.ToArray(typeof(ProcessEx));
		}
	}

	class ProcessEnder
	{
		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		static extern IntPtr CreateRemoteThread(
			IntPtr hProcess,
			int lpThreadAttributes,
			int dwStackSize,  
			IntPtr lpStartAddress,
			uint lpParameter,
			int dwCreationFlags,
			ref int lpThreadId);

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		static extern IntPtr GetModuleHandle(string lpModuleName);

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		static extern bool CloseHandle(IntPtr hObject);

		// Injects a thread into a process and invokes ExitProcess.
		// This ends a process in a more graceful way than Process.Kill, but
		// it does not solve the problem of killing all sub-process of the 
		// process.
		//
		public static void ExitProcess(Process p)
		{
			IntPtr pHandle = p.Handle;
			int ThreadID = 0;
			IntPtr ExitProcAddr = GetProcAddress(GetModuleHandle("KERNEL32.DLL"), "ExitProcess");
			IntPtr ThreadHandle =
				CreateRemoteThread(pHandle, 0, 0, ExitProcAddr, 54, 0, ref ThreadID);
			CloseHandle(ThreadHandle);
		}

		// Invokes ExitProcess for a process and its sub-processes.  This does
		// not support processes that have more than one child process.
		//
		public static void ExitProcessTree(Process p)
		{
			ProcessEx[] ProcList = SubProcessQuery.Execute(p);
			foreach (ProcessEx Proc in ProcList)
			{
				ExitProcess(Proc.Process);
			}
		}

	}

}
