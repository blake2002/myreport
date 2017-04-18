using System;
using System.Drawing.Imaging;
using System.IO;

namespace SvgNet.SvgGdi.MetafileTools.EmfTools
{
	public abstract class EmfBinaryRecord : IBinaryRecord
	{
		public uint RecordSize
		{
			get;
			set;
		}

		public EmfPlusRecordType RecordType
		{
			get;
			set;
		}

		public EmfBinaryRecord()
		{
		}

		public virtual void Read(BinaryReader reader)
		{
		}
	}
}
