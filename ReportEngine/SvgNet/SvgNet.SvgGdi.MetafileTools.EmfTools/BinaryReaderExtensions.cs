using System;
using System.IO;

namespace SvgNet.SvgGdi.MetafileTools.EmfTools
{
	public static class BinaryReaderExtensions
	{
		public static void Skip(this BinaryReader reader, int excess)
		{
			if (excess > 0)
			{
				reader.BaseStream.Seek((long)excess, SeekOrigin.Current);
			}
		}
	}
}
