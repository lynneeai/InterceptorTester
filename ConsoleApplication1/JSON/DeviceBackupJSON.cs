using System;

namespace ConsoleApplication1
{


	/// <summary>
	/// Used to parse the Json of the b element of the DeviceBackup class
	/// </summary>
	public class BackupItem
	{
		public String d;
		public int s;

		public DateTimeOffset t;

		public bool? c;

	}

	public class DeviceBackupJSON
	{
		/// <summary>
		/// Gets or sets a
		/// </summary>
		public string a { get; set; }

		/// <summary>
		/// Gets or sets i
		/// </summary>
		public string i { get; set; }

		/// <summary>
		/// Gets or sets b
		/// </summary>
		public object[] b { get; set; }

		public int s { get; set; }

		public override string ToString()
		{
			return string.Format("[DeviceBackup s: {0}, i: {1}, b: {2}, a: {3} ]", s, i, b, a);
		}
	}


}

