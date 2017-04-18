using System;
using System.Data;
using System.Runtime.Serialization;

namespace MES.Utils.Data
{

	/// <summary>
	/// Replacement for the standard DataSet to allow Fast Serialization
	/// during remoting.
	/// </summary>
	[Serializable]
	public class FastSerializableDataSet: DataSet, ISerializable
	{
		#region Constructors
		public FastSerializableDataSet(): base() {}
		public FastSerializableDataSet(string dataSetName): base(dataSetName) {}
		#endregion Constructors
		
		#region ISerializable Members
		protected FastSerializableDataSet(SerializationInfo info, StreamingContext context)
		{
			AdoNetHelper.DeserializeDataSet(this, (byte[]) info.GetValue("_", typeof(byte[])));
		}

#if NET20		
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
#else		
		public void GetObjectData(SerializationInfo info, StreamingContext context)
#endif		
		{
			info.AddValue("_", AdoNetHelper.SerializeDataSet(this));
		}
		#endregion
	}

}
