using System;
using System.IO;

namespace SvgNet.SvgGdi.MetafileTools.EmfTools
{
	public class EmfUnknownRecord : EmfBinaryRecord
	{
		private static byte[] EmptyData = new byte[0];

		public byte[] Data
		{
			get;
			set;
		}

		public override void Read(BinaryReader reader)
		{
			int num = (int)(base.RecordSize - 4u - 4u);
			if (num > 0)
			{
				this.Data = reader.ReadBytes(num);
			}
			else
			{
				this.Data = EmfUnknownRecord.EmptyData;
			}
		}
	}
}
