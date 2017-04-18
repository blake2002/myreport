using System;
using System.IO;

namespace NetSCADA.ReportEngine
{
	using System.Runtime.Serialization.Formatters.Binary;
	public class SerializationUnit
	{        
		/// <summary>
		/// 把对象序列化为字节数组
		/// </summary>
		public static byte[] SerializeObject(object obj)
		{
			if (obj == null)
				return null;
			MemoryStream ms = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(ms, obj);
			ms.Position = 0;
			byte[] bytes = new byte[ms.Length];
			ms.Read(bytes, 0, bytes.Length);
			ms.Close();
			return bytes;
		}

		/// <summary>
		/// 把字节数组反序列化成对象
		/// </summary>
		public static object DeserializeObject(byte[] bytes)
		{
			object obj = null;
			if (bytes == null)
				return obj;
			MemoryStream ms = new MemoryStream(bytes);
			ms.Position = 0;
			BinaryFormatter formatter = new BinaryFormatter();
			obj = formatter.Deserialize(ms);
			ms.Close();
			return obj;
		}
	}
}

