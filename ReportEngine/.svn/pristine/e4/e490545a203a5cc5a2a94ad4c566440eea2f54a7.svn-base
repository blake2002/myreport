using System;
using System.Data;
using System.Runtime.Serialization;

namespace MES.Utils.Data
{

	/// <summary>
	/// Takes a DataSet and 'wraps' it so that it can be passed via Remoting
	/// using Fast serialization.
	/// </summary>
	[Serializable]
	public class WrappedDataSet: ISerializable
	{
		#region Casting Operators
		public static implicit operator DataSet (WrappedDataSet wrappedDataSet)
		{
			return wrappedDataSet.DataSet;
		}
		
		public static implicit operator WrappedDataSet (DataSet dataSet)
		{
			return new WrappedDataSet(dataSet);
		}
		#endregion Casting Operators
		
		#region Constructors
		public WrappedDataSet(DataSet dataSet)
		{
			if (dataSet == null) throw new ArgumentNullException("dataSet");
			this.dataSet = dataSet;
		}
		#endregion Constructors
		
		#region Properties
		public DataSet DataSet {
			get { return dataSet; }
		} DataSet dataSet;
		#endregion Properties
		
		#region ISerializable Members
		protected WrappedDataSet(SerializationInfo info, StreamingContext context)
		{
			dataSet = AdoNetHelper.DeserializeDataSet((byte[]) info.GetValue("_", typeof(byte[])));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_", AdoNetHelper.SerializeDataSet(dataSet));
		}
		#endregion
	}

}
