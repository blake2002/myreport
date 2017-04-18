using System;
using System.Drawing.Imaging;
using System.IO;

namespace SvgNet.SvgGdi.MetafileTools.EmfTools
{
	public class EmfReader : IDisposable
	{
		private Stream stream;

		private BinaryReader reader;

		public bool IsEndOfFile
		{
			get
			{
				return this.stream.Length == this.stream.Position;
			}
		}

		public EmfReader(Stream stream)
		{
			this.stream = stream;
			this.reader = new BinaryReader(stream);
		}

		public IBinaryRecord Read()
		{
			long position = this.reader.BaseStream.Position;
			EmfPlusRecordType recordType = (EmfPlusRecordType)this.reader.ReadUInt32();
			uint num = this.reader.ReadUInt32();
			EmfBinaryRecord emfBinaryRecord = new EmfUnknownRecord();
			emfBinaryRecord.RecordType = recordType;
			emfBinaryRecord.RecordSize = num;
			emfBinaryRecord.Read(this.reader);
			long position2 = this.reader.BaseStream.Position;
			long num2 = position2 - position;
			long num3 = (long)((ulong)num - (ulong)num2);
			if (num3 > 0L)
			{
				this.reader.Skip((int)num3);
			}
			return emfBinaryRecord;
		}

		public void Dispose()
		{
			if (this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
		}
	}
}
