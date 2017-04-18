using System;
using System.IO;

namespace SvgNet.SvgGdi.MetafileTools.EmfTools
{
	public interface IBinaryRecord
	{
		void Read(BinaryReader reader);
	}
}
