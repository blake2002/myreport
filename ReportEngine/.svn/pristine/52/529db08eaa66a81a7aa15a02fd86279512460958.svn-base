using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using SimmoTech.Utils.Data;
using SimmoTech.Utils.Serialization;

namespace SimmoTech.Utils.Data
{

	public class AdoNetComparer
	{
#if NET20		
		public static readonly FieldInfo TableCaseSensitiveAmbientFieldInfo = typeof(DataTable).GetField("_caseSensitiveUserSet", BindingFlags.Instance | BindingFlags.NonPublic);
		public static readonly FieldInfo TableCultureFieldInfo = typeof(DataTable).GetField("_cultureUserSet", BindingFlags.Instance | BindingFlags.NonPublic);
#else		
		public static readonly FieldInfo TableCaseSensitiveAmbientFieldInfo = typeof(DataTable).GetField("caseSensitiveAmbient", BindingFlags.Instance | BindingFlags.NonPublic);
		public static readonly FieldInfo TableCultureFieldInfo = typeof(DataTable).GetField("culture", BindingFlags.Instance | BindingFlags.NonPublic);
#endif		
		public static readonly FieldInfo AutoIncrementCurrentFieldInfo = typeof(DataColumn).GetField("autoIncrementCurrent", BindingFlags.Instance | BindingFlags.NonPublic);
		
		private static void CompareDataArrays(object[] data1, object[] data2)
		{
			if (data1 == null)
				Assert.IsNull(data2);
			else
			{
				Assert.IsNotNull(data2);
				Assert.IsFalse(data1 == data2);

				Assert.AreEqual(data1.Length, data2.Length);
				for(int i = 0; i < data1.Length; i++)
				{
					if (data1[i].GetType().FullName == "System.Object")
						Assert.IsTrue(data2[i].GetType().FullName == "System.Object");
					else if (data1[i] is DateTime)
						Assert.AreEqual(((DateTime) data1[i]).ToUniversalTime(), ((DateTime) data2[i]).ToUniversalTime());
					else
					{
						Assert.AreEqual(data1[i], data2[i]);
					}
				}
			}
		}
		
		public static void CompareDataTables(DataTable table1, DataTable table2) { CompareDataTables(table1, table2, false); }
		public static void CompareDataTables(DataTable table1, DataTable table2, bool allowDifferentRowStates)
		{
			CompareTables(table1, table2, false, allowDifferentRowStates);
		}
		
		public static void CompareTypedDataSets(DataSet dataSet1, DataSet dataSet2)
		{
			Assert.IsFalse(dataSet1 == dataSet2);
			Assert.AreEqual(dataSet1.GetType(), dataSet2.GetType());
			CompareDataSets(dataSet1, dataSet2);
		}

		public static void CompareDataSets(DataSet dataSet1, DataSet dataSet2)
		{
			Assert.IsFalse(dataSet1 == dataSet2);
			Assert.AreEqual(dataSet1.Prefix, dataSet2.Prefix);
			Assert.AreEqual(dataSet1.CaseSensitive, dataSet2.CaseSensitive);
			Assert.AreEqual(dataSet1.Locale.LCID, dataSet2.Locale.LCID);
			Assert.AreEqual(dataSet1.Locale.UseUserOverride, dataSet2.Locale.UseUserOverride);
			Assert.AreEqual(dataSet1.DataSetName, dataSet2.DataSetName);
			Assert.AreEqual(dataSet1.EnforceConstraints, dataSet2.EnforceConstraints);
			CompareExtendedProperties(dataSet1.ExtendedProperties, dataSet2.ExtendedProperties);
			Assert.AreEqual(dataSet1.Namespace, dataSet2.Namespace);
			
			Assert.AreEqual(dataSet1.Tables.Count, dataSet2.Tables.Count);
			for (int i = 0; i < dataSet1.Tables.Count; i++)
			{
				CompareTables(dataSet1.Tables[i], dataSet2.Tables[i], true, false);
			}
			
			Assert.AreEqual(dataSet1.Relations.Count, dataSet2.Relations.Count);
			for (int i = 0; i < dataSet1.Relations.Count; i++)
			{
				CompareRelations(dataSet1.Relations[i], dataSet2.Relations[i]);
			}
			
			// Add any reflection compares here
			
		}
		
		public static void CompareTables(DataTable table1, DataTable table2, bool compareForeignKeyConstraints, bool allowDifferentRowStates)
		{
			if (table1 == null)
				Assert.IsNull(table2);
			else
			{
				Assert.IsNotNull(table2);
				Assert.IsFalse(table1 == table2);
				
				Assert.AreEqual(table1.CaseSensitive, table2.CaseSensitive);
				Assert.AreEqual(TableCaseSensitiveAmbientFieldInfo.GetValue(table1), TableCaseSensitiveAmbientFieldInfo.GetValue(table2));
				Assert.AreEqual(TableCultureFieldInfo.GetValue(table1), TableCultureFieldInfo.GetValue(table2));
				Assert.AreEqual(table1.Locale.LCID, table2.Locale.LCID);
				Assert.AreEqual(table1.Locale.UseUserOverride, table2.Locale.UseUserOverride);
				Assert.AreEqual(table1.DisplayExpression, table2.DisplayExpression);
				Assert.AreEqual(table1.TableName, table2.TableName);
				Assert.AreEqual(table1.Namespace, table2.Namespace);
				Assert.AreEqual(table1.Prefix, table2.Prefix);
				Assert.AreEqual(table1.HasErrors, table2.HasErrors);
				Assert.AreEqual(table1.MinimumCapacity, table2.MinimumCapacity);
				
				CompareExtendedProperties(table1.ExtendedProperties, table2.ExtendedProperties);
				
				Assert.AreEqual(table1.PrimaryKey.Length, table2.PrimaryKey.Length, table1.TableName);
				for(int i = 0; i < table1.PrimaryKey.Length; i++)
				{
					Assert.AreEqual(table1.PrimaryKey[i].Ordinal, table2.PrimaryKey[i].Ordinal);
				}
				
				Assert.AreEqual(table1.Columns.Count, table2.Columns.Count);
				for(int i = 0; i < table1.Columns.Count; i++)
				{
					CompareColumns(table1.Columns[i], table2.Columns[i]);
				}
				
				UniqueConstraint[] uniqueConstraints1;
				ForeignKeyConstraint[] foreignKeyConstraints1;
				splitConstraints(table1.Constraints, out uniqueConstraints1, out foreignKeyConstraints1);

				UniqueConstraint[] uniqueConstraints2;
				ForeignKeyConstraint[] foreignKeyConstraints2;
				splitConstraints(table2.Constraints, out uniqueConstraints2, out foreignKeyConstraints2);

				Assert.AreEqual(uniqueConstraints1.Length, uniqueConstraints2.Length);
				for(int i = 0; i < uniqueConstraints1.Length; i++)
				{
					Assert.AreEqual(uniqueConstraints1[i].Table.TableName, uniqueConstraints2[i].Table.TableName);
					Assert.AreEqual(uniqueConstraints1[i].IsPrimaryKey, uniqueConstraints2[i].IsPrimaryKey);
					Assert.AreEqual(uniqueConstraints1[i].Columns.Length, uniqueConstraints2[i].Columns.Length);
					for(int j = 0; j < uniqueConstraints1[i].Columns.Length; j++)
					{
						CompareColumns(uniqueConstraints1[i].Columns[j], uniqueConstraints2[i].Columns[j]);
					}
				}
				
				if (compareForeignKeyConstraints)
				{
					Assert.AreEqual(foreignKeyConstraints1.Length, foreignKeyConstraints2.Length);
					for(int i = 0; i < foreignKeyConstraints1.Length; i++)
					{
						Assert.AreEqual(foreignKeyConstraints1[i].AcceptRejectRule, foreignKeyConstraints2[i].AcceptRejectRule);
						Assert.AreEqual(foreignKeyConstraints1[i].DeleteRule, foreignKeyConstraints2[i].DeleteRule);
						Assert.AreEqual(foreignKeyConstraints1[i].Table.TableName, foreignKeyConstraints2[i].Table.TableName);
						Assert.AreEqual(foreignKeyConstraints1[i].RelatedTable.TableName, foreignKeyConstraints2[i].RelatedTable.TableName);
						Assert.AreEqual(foreignKeyConstraints1[i].Columns.Length, foreignKeyConstraints2[i].Columns.Length);
						for (int j = 0; j < foreignKeyConstraints1[i].Columns.Length; j++)
						{
							CompareColumns(foreignKeyConstraints1[i].Columns[j], foreignKeyConstraints2[i].Columns[j]);
						}
						Assert.AreEqual(foreignKeyConstraints1[i].RelatedColumns.Length, foreignKeyConstraints2[i].RelatedColumns.Length);
						for (int j = 0; j < foreignKeyConstraints1[i].RelatedColumns.Length; j++)
						{
							CompareColumns(foreignKeyConstraints1[i].RelatedColumns[j], foreignKeyConstraints2[i].RelatedColumns[j]);
						}
					}
				}
				
				Assert.AreEqual(table1.Rows.Count, table2.Rows.Count);
				for(int i = 0; i < table1.Rows.Count; i++)
				{
					CompareRows(table1.Rows[i], table2.Rows[i], allowDifferentRowStates);
				}
				
			}
			
			
		}

		private static void splitConstraints(ConstraintCollection constraints, out UniqueConstraint[] uniqueConstraints, out ForeignKeyConstraint[] foreignKeyConstraints)
		{
			ArrayList uniqueConstraintList = new ArrayList();
			ArrayList foreignKeyConstraintList = new ArrayList();
			foreach(Constraint constraint in constraints)
			{
				if (constraint is UniqueConstraint)
					uniqueConstraintList.Add(constraint);
				else if (constraint is ForeignKeyConstraint)
					foreignKeyConstraintList.Add(constraint);
			}
			uniqueConstraints = (UniqueConstraint[]) uniqueConstraintList.ToArray(typeof(UniqueConstraint));
			foreignKeyConstraints = (ForeignKeyConstraint[]) foreignKeyConstraintList.ToArray(typeof(ForeignKeyConstraint));
		}

		static object[] GetDataArray(DataRow row, DataRowVersion version)
		{
			if (!row.HasVersion(version))
				return null;
			else
			{
				object[] result = new object[row.Table.Columns.Count];
				for(int i = 0; i < result.Length; i++)
				{
					result[i] = row[i, version];
				}
				return result;
			}
		}

		static void CompareRows(DataRow row1, DataRow row2, bool allowDifferentRowStates)
		{
			if (row1 == null)
				Assert.IsNull(row2);
			else
			{
				Assert.IsNotNull(row2);
				Assert.IsFalse(row1 == row2);
				
				if (!allowDifferentRowStates)
				{
					Assert.AreEqual(row1.RowState, row2.RowState);
				}
				
				if (row1.HasVersion(DataRowVersion.Original))
				{
					Assert.IsTrue(row2.HasVersion(DataRowVersion.Original));
					CompareDataArrays(GetDataArray(row1, DataRowVersion.Original), GetDataArray(row2, DataRowVersion.Original));
				}
				
				if (row1.HasVersion(DataRowVersion.Proposed))
				{
					Assert.IsTrue(row2.HasVersion(DataRowVersion.Proposed));
					CompareDataArrays(GetDataArray(row1, DataRowVersion.Proposed), GetDataArray(row2, DataRowVersion.Proposed));
				}
				
				// Check errors here
				Assert.AreEqual(row1.RowError, row2.RowError);
				Assert.AreEqual(row1.HasErrors, row2.HasErrors);

				for(int i = 0; i < row1.Table.Columns.Count; i++)
				{
					Assert.AreEqual(row1.GetColumnError(i), row2.GetColumnError(i));
				}
			}
		}

		static void CompareColumns(DataColumn column1, DataColumn column2)
		{
			if (column1 == null)
				Assert.IsNull(column2);
			else
			{
				Assert.IsNotNull(column2);
				Assert.IsFalse(column1 == column2);
				
				Assert.AreEqual(column1.ColumnName, column2.ColumnName);
				Assert.AreEqual(column1.Prefix, column2.Prefix);
				Assert.AreEqual(column1.Namespace, column2.Namespace);
				Assert.AreEqual(column1.AllowDBNull, column2.AllowDBNull);
				Assert.AreEqual(column1.AutoIncrement, column2.AutoIncrement);
				Assert.AreEqual(column1.AutoIncrementSeed, column2.AutoIncrementSeed);
				Assert.AreEqual(column1.AutoIncrementStep, column2.AutoIncrementStep);
			//	Assert.AreEqual(AutoIncrementCurrentFieldInfo.GetValue(column1), AutoIncrementCurrentFieldInfo.GetValue(column2));
				Assert.AreEqual(column1.Caption, column2.Caption);
				Assert.AreEqual(column1.ColumnMapping, column2.ColumnMapping);
				Assert.AreEqual(column1.DataType, column2.DataType);
				Assert.AreEqual(column1.Ordinal, column2.Ordinal);
				Assert.AreEqual(column1.DefaultValue, column2.DefaultValue);
				Assert.AreEqual(column1.Expression, column2.Expression);
				CompareExtendedProperties(column1.ExtendedProperties, column2.ExtendedProperties);
				Assert.AreEqual(column1.MaxLength, column2.MaxLength);
				Assert.AreEqual(column1.ReadOnly, column2.ReadOnly);
				Assert.AreEqual(column1.Unique, column2.Unique);
			}
		}

		static void CompareRelations(DataRelation relation1, DataRelation relation2)
		{
			if (relation1 == null)
				Assert.IsNull(relation2);
			else
			{
				Assert.IsNotNull(relation2);
				Assert.IsFalse(relation1 == relation2);
				
				Assert.AreEqual(relation1.RelationName, relation2.RelationName);
				Assert.AreEqual(relation1.Nested, relation2.Nested);
				CompareExtendedProperties(relation1.ExtendedProperties, relation2.ExtendedProperties);
				
				Assert.AreEqual(relation1.ChildTable.TableName, relation2.ChildTable.TableName);
				Assert.AreEqual(relation1.ChildColumns.Length, relation2.ChildColumns.Length);
				for(int i = 0; i < relation1.ChildColumns.Length; i++) {
					Assert.AreEqual(relation1.ChildColumns[i].ColumnName, relation2.ChildColumns[i].ColumnName);
					Assert.AreEqual(relation1.ChildColumns[i].DataType, relation2.ChildColumns[i].DataType);
					Assert.AreEqual(relation1.ChildColumns[i].Ordinal, relation2.ChildColumns[i].Ordinal);
				}
				
				Assert.AreEqual(relation1.ParentTable.TableName, relation2.ParentTable.TableName);
				Assert.AreEqual(relation1.ParentColumns.Length, relation2.ParentColumns.Length);
				for(int i = 0; i < relation1.ParentColumns.Length; i++) {
					Assert.AreEqual(relation1.ParentColumns[i].ColumnName, relation2.ParentColumns[i].ColumnName);
					Assert.AreEqual(relation1.ParentColumns[i].DataType, relation2.ParentColumns[i].DataType);
					Assert.AreEqual(relation1.ParentColumns[i].Ordinal, relation2.ParentColumns[i].Ordinal);
				}
			}
			
		}

		public static void CompareExtendedProperties(PropertyCollection properties1, PropertyCollection properties2)
		{
			if (properties1 == null)
				Assert.IsNull(properties2);
			else
			{
				Assert.IsNotNull(properties2);
				Assert.IsFalse(properties1 == properties2);
				
				Assert.AreEqual(properties1.Count, properties2.Count);
				foreach(object key in properties1.Keys)
				{
					Assert.IsTrue(properties2.ContainsKey(key));
					object value1 = properties1[key];
					object value2 = properties2[key];
					if (value1 == null)
						Assert.IsNull(value2);
					else
					{
						Assert.IsNotNull(value2);
						Assert.IsTrue(value1.Equals(value2));
					}
					
				}
			}
		}
	}
	

	[TestFixture]
	public class AdoNetHelperTests
	{
		DataSet dataSet;
		DataTable parentTable;
		DataTable childTable;
		byte[] serializedData;
		bool serializerRan = false;

		[SetUp]
		public void CreateDataSetAndSampleTables()
		{
			dataSet = new DataSet();
			parentTable = CreateParentTable();
			childTable = CreateChildTable();
			serializerRan = false;
		}
		
		[TearDown]
		public void CheckSerializerRan()
		{
			Assert.IsTrue(serializerRan, "The Serializer did not run for this test!");
		}
		
		private byte[] SerializeDataSet(DataSet source)
		{
			serializedData = AdoNetHelper.SerializeDataSet(source);
			return serializedData;
		}
		
		private DataSet DeserializeDataSet(byte[] serializedData)
		{
			return AdoNetHelper.DeserializeDataSet(serializedData);
		}
		
		private byte[] SerializeDataTable(DataTable source)
		{
			serializedData = AdoNetHelper.SerializeDataTable(source);
			return serializedData;
		}
		
		private DataTable DeserializeDataTable(byte[] serializedData)
		{
			return AdoNetHelper.DeserializeDataTable(serializedData);
		}
		
		private string getDataSetXml(DataSet source)
		{
			using(StringWriter stream = new StringWriter())
			{
				source.WriteXml(stream, XmlWriteMode.WriteSchema);
				
				return stream.ToString();
			}
		}
		
		private void SerializeAndCompare(DataSet source) { SerializeAndCompare(source, -1); }
		private void SerializeAndCompare(DataSet source, int expectedSize)
		{
			DataSet copy = DeserializeDataSet(SerializeDataSet(source));
			Assert.AreEqual(source.GetType(), copy.GetType());
			AdoNetComparer.CompareDataSets(source, copy);
			if (expectedSize != -1) Assert.AreEqual(expectedSize, serializedData.Length);
			serializerRan = true;
		}
		
		private void SerializeAndCompare(DataTable source) { SerializeAndCompare(source, -1); }
		private void SerializeAndCompare(DataTable source, int expectedSize)
		{
			DataTable copy = DeserializeDataTable(SerializeDataTable(source));
			Assert.AreEqual(source.GetType(), copy.GetType());
			AdoNetComparer.CompareDataTables(source, copy);
			if (expectedSize != -1) Assert.AreEqual(expectedSize, serializedData.Length);
			serializerRan = true;
		}
		
		private DataTable SerializeAndCompareSimpleDataTable(DataTable source, DataTable destination) { return SerializeAndCompareSimpleDataTable(source, destination, -1); }
		private DataTable SerializeAndCompareSimpleDataTable(DataTable source, DataTable destination, int expectedSize)
		{
			serializedData = AdoNetHelper.SerializeSimpleTypedDataTable(source);
			
			DataTable copy;
			if (destination == null)
				copy = AdoNetHelper.DeserializeSimpleTypedDataTable(source.GetType(), serializedData);
			else
			{
				copy = AdoNetHelper.DeserializeSimpleTypedDataTable(destination, serializedData);
			}

			Assert.AreEqual(source.GetType(), copy.GetType());
			
			AdoNetComparer.CompareDataTables(source, destination, true);
			if (expectedSize != -1) Assert.AreEqual(expectedSize, serializedData.Length);
			serializerRan = true;
			return copy;
		}

		private void SerializeAndCompareViaBinaryFormatter(object source) { SerializeAndCompareViaBinaryFormatter(source, -1); }
		private void SerializeAndCompareViaBinaryFormatter(object source, int expectedSize)
		{
			byte[] data = SerializeNotOptimized(source);
			DataSet copy = DeserializeNotOptimized(data);
			Assert.AreEqual(source is WrappedDataSet ? typeof(DataSet) : source.GetType(), copy.GetType());
			AdoNetComparer.CompareDataSets(source is DataSet ? (source as DataSet) : (source as WrappedDataSet).DataSet, copy);
			if (expectedSize != -1) Assert.AreEqual(expectedSize, data.Length);
			serializerRan = true;
		}
		
		private void SerializeAndCompareViaSurrogate(object source) { SerializeAndCompareViaSurrogate(source, -1); }
		private void SerializeAndCompareViaSurrogate(object source, int expectedSize)
		{
			AdoNetFastSerializerSurrogate surrogateSelector = new AdoNetFastSerializerSurrogate();
			byte[] data = SerializeNotOptimized(source, surrogateSelector);
			DataSet copy = DeserializeNotOptimized(data, surrogateSelector);
			Assert.AreEqual(source is WrappedDataSet ? typeof(DataSet) : source.GetType(), copy.GetType());
			AdoNetComparer.CompareDataSets(source is DataSet ? (source as DataSet) : (source as WrappedDataSet).DataSet, copy);
			if (expectedSize != -1) Assert.AreEqual(expectedSize, data.Length);
			serializerRan = true;
		}
		
		private byte[] SerializeNotOptimized(object source) { return SerializeNotOptimized(source, null); }
		private byte[] SerializeNotOptimized(object source, ISurrogateSelector surrogateSelector)
		{
			BinaryFormatter formatter = new BinaryFormatter(surrogateSelector, new StreamingContext());
			formatter.AssemblyFormat = FormatterAssemblyStyle.Full;			
			using(MemoryStream stream = new MemoryStream())
			{
				formatter.Serialize(stream, source);
				return stream.ToArray();
			}
		}
		
		private DataSet DeserializeNotOptimized(byte[] data) { return DeserializeNotOptimized(data, null); }
		private DataSet DeserializeNotOptimized(byte[] data, ISurrogateSelector surrogateSelector)
		{
			BinaryFormatter formatter = new BinaryFormatter(surrogateSelector, new StreamingContext());
			using(MemoryStream stream = new MemoryStream(data))
			{
				object result = formatter.Deserialize(stream);
				if (result is DataSet)
					return result as DataSet;
				else
				{
					return (result as WrappedDataSet).DataSet;
				}
			}
		}

		void CompareToStandardSerialization(string methodName) {
			byte[] oldSerializedData = SerializeNotOptimized(dataSet);

			Console.WriteLine(
				string.Format(
					"{0}: Old={1:n0} bytes, New={2:n0} bytes (reduced to {3:n2}%) ",
					methodName,
					oldSerializedData.Length,
					serializedData.Length,
					100f * serializedData.Length / oldSerializedData.Length));
		}
		
		private const string ParentTableName = "Parent";
		private const string ChildTableName = "Child";
		private const string PrimaryKeyColumnName = "PrimaryKey";
		private const string ChildPrimaryKeyColumnName = "ChildKey";
		private const string BooleanColumnName = "BooleanColumn";
		private const string ByteColumnName = "ByteColumn";
		private const string DateTimeColumnName = "DateTimeColumn";
		private const string DecimalColumnName = "DecimalColumn";
		private const string Int16ColumnName = "Int16Column";
		private const string Int32ColumnName = "Int32Column";
		private const string Int64ColumnName = "Int64Column";
		private const string SingleColumnName = "SingleColumn";
		private const string DoubleColumnName = "DoubleColumn";
		private const string StringColumnName = "StringColumn";
		private const string TimeSpanColumnName = "TimeSpanColumn";
		private const string NullColumnName = "NullColumn";
		private const string ObjectColumnName = "ObjectColumn";
		
		private DataTable CreateParentTable()
		{
			DataTable table = new DataTable(ParentTableName);
			table.Columns.Add(new DataColumn(PrimaryKeyColumnName, typeof(int)));
			table.Columns[PrimaryKeyColumnName].AutoIncrement = true;
			table.Columns[PrimaryKeyColumnName].AutoIncrementSeed = -1;
			table.Columns[PrimaryKeyColumnName].AutoIncrementStep = -1;
			table.Columns[PrimaryKeyColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(BooleanColumnName, typeof(bool)));
			table.Columns[BooleanColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(ByteColumnName, typeof(byte)));
			table.Columns[ByteColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(DateTimeColumnName, typeof(DateTime)));
			table.Columns[DateTimeColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(DecimalColumnName, typeof(decimal)));
			table.Columns[DecimalColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(Int16ColumnName, typeof(short)));
			table.Columns[Int16ColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(Int32ColumnName, typeof(int)));
			table.Columns[Int32ColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(Int64ColumnName, typeof(long)));
			table.Columns[Int64ColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(SingleColumnName, typeof(float)));
			table.Columns[SingleColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(DoubleColumnName, typeof(double)));
			table.Columns[DoubleColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(StringColumnName, typeof(string)));
			table.Columns[StringColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(TimeSpanColumnName, typeof(TimeSpan)));
			table.Columns[TimeSpanColumnName].AllowDBNull = false;
			table.Columns.Add(new DataColumn(NullColumnName, typeof(string)));
			table.Columns[NullColumnName].AllowDBNull = true;
			table.Columns.Add(new DataColumn(ObjectColumnName, typeof(object)));
			table.Columns[ObjectColumnName].AllowDBNull = true;

			table.PrimaryKey = new DataColumn[]{ table.Columns[PrimaryKeyColumnName] };
			return table;
		}
		
		private DataRow CreateSimpleRow() { return CreateSimpleRow(Int32.MaxValue); }
		private DataRow CreateSimpleRow(object int32Value)
		{
			DataRow row = parentTable.NewRow();
			row[BooleanColumnName] = true;
			row[ByteColumnName] = byte.MaxValue;
			row[DateTimeColumnName] = DateTime.MaxValue;
			row[DecimalColumnName] = Decimal.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[Int16ColumnName] = Int16.MaxValue;
			if (int32Value != null) row[Int32ColumnName] = int32Value;
			row[Int64ColumnName] = Int64.MaxValue;
			row[SingleColumnName] = Single.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[StringColumnName] = "hello dolly this is louis dolly";
			row[TimeSpanColumnName] = TimeSpan.MaxValue;
			row[NullColumnName] = DBNull.Value;
			row[ObjectColumnName] = new object();
			return row;
		}
		
		private void SetupMixedData(DataSet dataSet)
		{
			DataRow row;
			
			parentTable.Rows.Add(CreateSimpleRow());
			row = parentTable.NewRow();
			row[BooleanColumnName] = true;
			row[ByteColumnName] = byte.MaxValue;
			row[DateTimeColumnName] = DateTime.MaxValue;
			row[DecimalColumnName] = Decimal.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[Int16ColumnName] = Int16.MaxValue;
			row[Int32ColumnName] = Int32.MaxValue;
			row[Int64ColumnName] = Int64.MaxValue;
			row[SingleColumnName] = Single.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[StringColumnName] = "hello dolly this is louis dolly";
			row[TimeSpanColumnName] = TimeSpan.MaxValue;
			row[NullColumnName] = DBNull.Value;
			row[ObjectColumnName] = new object();
			parentTable.AcceptChanges();
			parentTable.Rows[0].Delete();
			parentTable.Rows[0].Delete();
			
			parentTable.Rows.Add(CreateSimpleRow());
			row = parentTable.NewRow();
			row[BooleanColumnName] = true;
			row[ByteColumnName] = byte.MaxValue;
			row[DateTimeColumnName] = DateTime.MaxValue;
			row[DecimalColumnName] = Decimal.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[Int16ColumnName] = Int16.MaxValue;
			row[Int32ColumnName] = Int32.MaxValue;
			row[Int64ColumnName] = Int64.MaxValue;
			row[SingleColumnName] = Single.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[StringColumnName] = "hello dolly this is louis dolly";
			row[TimeSpanColumnName] = TimeSpan.MaxValue;
			row[NullColumnName] = DBNull.Value;
			row[ObjectColumnName] = new object();
			parentTable.AcceptChanges();
			
			parentTable.Rows.Add(CreateSimpleRow());
			row = parentTable.NewRow();
			row[BooleanColumnName] = true;
			row[ByteColumnName] = byte.MaxValue;
			row[DateTimeColumnName] = DateTime.MaxValue;
			row[DecimalColumnName] = Decimal.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[Int16ColumnName] = Int16.MaxValue;
			row[Int32ColumnName] = Int32.MaxValue;
			row[Int64ColumnName] = Int64.MaxValue;
			row[SingleColumnName] = Single.MaxValue;
			row[DoubleColumnName] = Double.MaxValue;
			row[StringColumnName] = "hello dolly this is louis dolly";
			row[TimeSpanColumnName] = TimeSpan.MaxValue;
			row[NullColumnName] = DBNull.Value;
			row[ObjectColumnName] = new object();
			parentTable.AcceptChanges();
			
			dataSet.Tables.Add(parentTable);
		}
		
		private DataTable CreateChildTable()
		{
			DataTable table = new DataTable(ChildTableName);
			table.Columns.Add(new DataColumn(ChildPrimaryKeyColumnName, typeof(int)));
			table.Columns[ChildPrimaryKeyColumnName].AutoIncrement = true;
			table.Columns.Add(new DataColumn(PrimaryKeyColumnName, typeof(int)));
			return table;
		}
		

		[Test]
		public void TestEmptyDataSet()
		{
			SerializeAndCompare(dataSet, 9);
		}
		
		[Test]
		public void TestEmptyUSDataSet()
		{
			dataSet.Locale = new CultureInfo(0x409);
			SerializeAndCompare(dataSet, 9);
		}
		
		[Test]
		public void TestEmptyUKDataSet()
		{
			dataSet.Locale = new CultureInfo(2057);
			SerializeAndCompare(dataSet, 9);
		}
		
		[Test]
		public void TestWithEmptyTable()
		{
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet, 425);
		}
		
		[Test]
		public void TestWithTableWithoutPrimaryKey()
		{
			dataSet.Tables.Add(childTable);
			SerializeAndCompare(dataSet, 64);
		}
		
		[Test]
		public void TestWithTwoTables()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			SerializeAndCompare(dataSet, 455);
		}
		
		[Test]
		public void TestWithTwoTablesAndNoPrimaryKeys()
		{
			parentTable.Constraints.Clear();
			childTable.Constraints.Clear();
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			SerializeAndCompare(dataSet, 452);
		}
		
		[Test]
		public void TestWithTwoTablesChildFirst()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			SerializeAndCompare(dataSet, 455);
		}
		
		[Test]
		public void TestWithTwoJoinedTables()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			SerializeAndCompare(dataSet, 489);
		}
		
		[Test]
		public void TestWithTwoJoinedTablesNoRelationName()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			SerializeAndCompare(dataSet, 479);
		}
		
		[Test]
		public void TestWithTwoJoinedTablesChildFirst()
		{
			dataSet.Tables.Add(childTable);
			dataSet.Tables.Add(parentTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			SerializeAndCompare(dataSet, 489);
		}
		
		[Test]
		public void TestWithTwoJoinedTablesAndCreateConstraints()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName], true);
			SerializeAndCompare(dataSet, 489);
		}
		
		[Test]
		public void TestWithTwoJoinedTablesAndCreateConstraintsChildFirst()
		{
			dataSet.Tables.Add(childTable);
			dataSet.Tables.Add(parentTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName], true);
			SerializeAndCompare(dataSet, 489);
		}
		
		[Test]
		public void TestWithTwoJoinedTablesAndPrecreatedPrimaryKey()
		{
			parentTable.PrimaryKey = null;
			parentTable.Constraints.Add(new UniqueConstraint("MyPrecreatedPrimaryKeyConstraint", parentTable.Columns[PrimaryKeyColumnName], true));
			
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			SerializeAndCompare(dataSet, 523);
		}
		
		[Test]
		public void TestWithTwoJoinedTablesAndPrecreatedPrimaryKeyChildFirst()
		{
			parentTable.PrimaryKey = null;
			parentTable.Constraints.Add(new UniqueConstraint("MyPrecreatedPrimaryKeyConstraint", parentTable.Columns[PrimaryKeyColumnName], true));
			
			dataSet.Tables.Add(childTable);
			dataSet.Tables.Add(parentTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			SerializeAndCompare(dataSet, 523);
		}
		
		[Test]
		public void TestWithTwoNestedJoinedTables()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]).Nested = true;
			SerializeAndCompare(dataSet, 489);
		}
		
		[Test]
		public void TestWithTwoNestedJoinedTablesAndCreateConstraints()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName], true).Nested = true;
			SerializeAndCompare(dataSet, 489);
		}
		
		[Test]
		public void TestWithTwoNestedJoinedTablesAndPrecreatedPrimaryKey()
		{
			parentTable.PrimaryKey = null;
			parentTable.Constraints.Add(new UniqueConstraint("MyPrecreatedPrimaryKeyConstraint", parentTable.Columns[PrimaryKeyColumnName], true));
			
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]).Nested = true;
			SerializeAndCompare(dataSet, 523);
		}

		[Test]
		public void TestWithTwoJoinedTablesVariousForeignKeyOptions()
		{
			int expectedSize = 490;
			ForeignKeyConstraint foreignKeyConstraint;
			
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			foreignKeyConstraint = dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]).ChildKeyConstraint;
			SerializeAndCompare(dataSet, expectedSize - 1); // Still in first byte of BitVector32
			
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.Cascade;
			SerializeAndCompare(dataSet, expectedSize); // Still in first byte of BitVector32
			foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;

			foreignKeyConstraint.UpdateRule = Rule.None;
			SerializeAndCompare(dataSet, expectedSize);
			foreignKeyConstraint.UpdateRule = Rule.Cascade;

			foreignKeyConstraint.UpdateRule = Rule.SetNull;
			SerializeAndCompare(dataSet, expectedSize);
			foreignKeyConstraint.UpdateRule = Rule.Cascade;

			foreignKeyConstraint.UpdateRule = Rule.SetDefault;
			SerializeAndCompare(dataSet, expectedSize);
			foreignKeyConstraint.UpdateRule = Rule.Cascade;

			foreignKeyConstraint.DeleteRule = Rule.None;
			SerializeAndCompare(dataSet, expectedSize);
			foreignKeyConstraint.DeleteRule = Rule.Cascade;

			foreignKeyConstraint.DeleteRule = Rule.SetNull;
			SerializeAndCompare(dataSet, expectedSize);
			foreignKeyConstraint.DeleteRule = Rule.Cascade;

			foreignKeyConstraint.DeleteRule = Rule.SetDefault;
			SerializeAndCompare(dataSet, expectedSize);
			foreignKeyConstraint.DeleteRule = Rule.Cascade;

		}
		
		[Test]
		public void TestExtendedProperties()
		{
			dataSet.ExtendedProperties.Add("key", "DataSet"); // On DataSet
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			
			parentTable.ExtendedProperties.Add("key", "Table"); // On Table
			
			DataRelation relation = dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName], true);
			relation.ExtendedProperties.Add("key", "Relationship"); // On Relationship
			
			relation.ParentKeyConstraint.ExtendedProperties.Add("key", "Constraint"); // On Constraint

			SerializeAndCompare(dataSet);
			
		}
		
		[Test]
		public void TestOneUnchangedRow()
		{
			dataSet.Tables.Add(parentTable);
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet);
		}
		
		[Test]
		public void TestTwoUnchangedRows()
		{
			dataSet.Tables.Add(parentTable);
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet);
		}
		
		[Test]
		public void TestOneModifiedRow()
		{
			dataSet.Tables.Add(parentTable);
			DataRow row = CreateSimpleRow();
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			
			row.BeginEdit();
			row[BooleanColumnName] = false;
			row[ByteColumnName] = byte.MinValue;
			row[DateTimeColumnName] = DateTime.MinValue;
			row[DecimalColumnName] = Decimal.MinValue;
			row[DoubleColumnName] = Double.MinValue;
			row[Int16ColumnName] = Int16.MinValue;
			row[Int32ColumnName] = Int32.MinValue;
			row[Int64ColumnName] = Int64.MinValue;
			row[SingleColumnName] = Single.MinValue;
			row[StringColumnName] = "changed";
			row[TimeSpanColumnName] = TimeSpan.MinValue;
			row[NullColumnName] = "object to string";
			row.EndEdit();
			
			SerializeAndCompare(dataSet);
		}
		
		[Test]
		public void TestTwoModifiedRows()
		{
			dataSet.Tables.Add(parentTable);
			DataRow row1 = CreateSimpleRow();
			DataRow row2 = CreateSimpleRow();
			parentTable.Rows.Add(row1);
			parentTable.Rows.Add(row2);
			parentTable.AcceptChanges();
			
			row1[BooleanColumnName] = false;
			row2[BooleanColumnName] = false;

			SerializeAndCompare(dataSet, 684);

			CompareToStandardSerialization("TestTwoModifiedRows()");
		}

		[Test]
		public void TestOneDeletedRow()
		{
			dataSet.Tables.Add(parentTable);
			DataRow row = CreateSimpleRow();
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			row.Delete();
			
			SerializeAndCompare(dataSet);
		}
		
		[Test]
		public void TestTwoDeletedRows()
		{
			dataSet.Tables.Add(parentTable);
			DataRow row1 = CreateSimpleRow();
			DataRow row2 = CreateSimpleRow();
			parentTable.Rows.Add(row1);
			parentTable.Rows.Add(row2);
			parentTable.AcceptChanges();

			row1.Delete();
			row2.Delete();
			
			SerializeAndCompare(dataSet);
		}
		
		[Test]
		public void TestOneAddedRow()
		{
			dataSet.Tables.Add(parentTable);
			parentTable.Rows.Add(CreateSimpleRow());
			SerializeAndCompare(dataSet);
		}
		
		[Test]
		public void TestTwoAddedRows()
		{
			dataSet.Tables.Add(parentTable);
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.Rows.Add(CreateSimpleRow());
			SerializeAndCompare(dataSet);
		}

		[Test]
		public void TestMixedData()
		{
			SetupMixedData(dataSet);
			SerializeAndCompare(dataSet);
		}
		
		[Test]
		public void TestMixedDataViaBinaryFormatterAndInheritedDataSet()
		{
			FastSerializableDataSet inheritedDataSet = new FastSerializableDataSet();
			SetupMixedData(inheritedDataSet);
			SerializeAndCompareViaBinaryFormatter(inheritedDataSet);
		}
		
		[Test]
		public void TestMixedDataViaBinaryFormatterAndSurrogate()
		{
			SetupMixedData(dataSet);
			SerializeAndCompareViaSurrogate(dataSet);
		}
		
		[Test]
		public void TestRowWithErrors()
		{
			dataSet.Tables.Add(parentTable);
			DataRow row = CreateSimpleRow();
			parentTable.AcceptChanges();
			parentTable.Rows.Add(row);
			row.SetColumnError(0, "Error0");
			row.SetColumnError(5, "Error5");
			row.RowError = "row error";
			SerializeAndCompare(dataSet);
		}
		
		public void TestDataSetEnforceConstraints()
		{
			dataSet.EnforceConstraints = false;
			SerializeAndCompare(dataSet);

			dataSet.EnforceConstraints = true;
			SerializeAndCompare(dataSet);
		}
		
		public void TestDataSetCaseSensitive()
		{
			dataSet.CaseSensitive = false;
			SerializeAndCompare(dataSet);

			dataSet.CaseSensitive = true;
			SerializeAndCompare(dataSet);
		}
		
		public void TestDataSetName()
		{
			string originalName = dataSet.DataSetName;
			dataSet.DataSetName = "Test";
			SerializeAndCompare(dataSet);

			dataSet.DataSetName = originalName;
			SerializeAndCompare(dataSet);
		}

		public void TestDataSetNameSpace()
		{
			dataSet.Namespace = "namespace";
			SerializeAndCompare(dataSet);

			dataSet.Namespace = null;
			SerializeAndCompare(dataSet);
		}
		
		public void TestDataSetPrefix()
		{
			dataSet.Prefix = "prefix";
			SerializeAndCompare(dataSet);

			dataSet.Prefix = null;
			SerializeAndCompare(dataSet);
		}

		public void TestTableName()
		{
			string originalName = parentTable.TableName;
			parentTable.TableName = "Test";
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);

			parentTable.TableName = originalName;
			SerializeAndCompare(dataSet);
		}

		public void TestTableNameSpace()
		{
			parentTable.Namespace = "namespace";
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);

			parentTable.Namespace = null;
			SerializeAndCompare(dataSet);
		}
		
		public void TestTablePrefix()
		{
			parentTable.Prefix = "prefix";
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);

			parentTable.Prefix = null;
			SerializeAndCompare(dataSet);
		}

		public void TestTableIsCaseSensitive()
		{
			parentTable.CaseSensitive = true;
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);

			parentTable.CaseSensitive = false;
			SerializeAndCompare(dataSet);
		}

		public void TestTableIsCaseSensitiveAmbient()
		{
			bool initialValue = (bool) AdoNetComparer.TableCaseSensitiveAmbientFieldInfo.GetValue(parentTable);
			Assert.AreEqual(initialValue, (bool) AdoNetComparer.TableCaseSensitiveAmbientFieldInfo.GetValue(parentTable));
			Assert.IsTrue(parentTable.CaseSensitive == false);
			Assert.IsTrue(parentTable.CaseSensitive == dataSet.CaseSensitive);
			
			dataSet.Tables.Add(parentTable);
			Assert.AreEqual(initialValue, (bool) AdoNetComparer.TableCaseSensitiveAmbientFieldInfo.GetValue(parentTable));
			Assert.IsTrue(parentTable.CaseSensitive == false);
			Assert.IsTrue(parentTable.CaseSensitive == dataSet.CaseSensitive);
			SerializeAndCompare(dataSet);

			dataSet.CaseSensitive = true;
			Assert.AreEqual(initialValue, (bool) AdoNetComparer.TableCaseSensitiveAmbientFieldInfo.GetValue(parentTable));
			Assert.IsTrue(parentTable.CaseSensitive);
			Assert.IsTrue(parentTable.CaseSensitive == dataSet.CaseSensitive);
			SerializeAndCompare(dataSet);
			
			parentTable.CaseSensitive = false;
			SerializeAndCompare(dataSet);
			Assert.AreNotEqual(initialValue, (bool) AdoNetComparer.TableCaseSensitiveAmbientFieldInfo.GetValue(parentTable));
			Assert.IsFalse(parentTable.CaseSensitive);
			Assert.IsFalse(parentTable.CaseSensitive == dataSet.CaseSensitive);
			Assert.IsTrue(dataSet.CaseSensitive);
			
		}
		
		public void TestTableDisplayExpression()
		{
			parentTable.DisplayExpression = PrimaryKeyColumnName;
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);

			parentTable.DisplayExpression = null;
			SerializeAndCompare(dataSet);
		}
		
		public void TestTableMinimumCapacity()
		{
			parentTable.MinimumCapacity = 50;
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);

			parentTable.MinimumCapacity = 10;
			SerializeAndCompare(dataSet);
		}
		
		public void TestTableWithUniqueConstraint()
		{
			SerializeAndCompare(parentTable, 421);
		}
		
		[Test]
		public void TestTableSpecificLocale()
		{
			Assert.IsTrue(parentTable.Locale.LCID == CultureInfo.CurrentCulture.LCID);
			parentTable.Locale = new CultureInfo(0x409);
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);
			Assert.IsTrue(parentTable.Locale.LCID == 0x409);
			Assert.IsFalse(parentTable.Locale.LCID == CultureInfo.CurrentCulture.LCID);
		}

		[Test]
		public void TestTableDataSetLocale()
		{
			Assert.IsTrue(parentTable.Locale.LCID == CultureInfo.CurrentCulture.LCID);
			
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(dataSet);
			Assert.IsTrue(parentTable.Locale.LCID == CultureInfo.CurrentCulture.LCID);
			
			dataSet.Locale = new CultureInfo(0x409);
			SerializeAndCompare(dataSet);
			Assert.IsTrue(parentTable.Locale.LCID == 0x409);
			Assert.IsFalse(parentTable.Locale.LCID == CultureInfo.CurrentCulture.LCID);
		}
		
		[Test]
		public void TestColumnMappingType()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn keyColumn = parentTable.Columns[PrimaryKeyColumnName];
			
			keyColumn.ColumnMapping = MappingType.Attribute;
			SerializeAndCompare(dataSet);
			
			keyColumn.ColumnMapping = MappingType.Hidden;
			SerializeAndCompare(dataSet);

		}

		[Test]
		public void TestColumnAllowNull()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.AllowDBNull = true;
			SerializeAndCompare(dataSet);
			
			column.AllowDBNull = false;
			SerializeAndCompare(dataSet);

		}
		
		[Test]
		public void TestColumnHasCaption()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.Caption = "Caption";
			SerializeAndCompare(dataSet);
			
			column.Caption = null;
			SerializeAndCompare(dataSet);

		}
		
		[Test]
		public void TestColumnHasMaxLength()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.MaxLength = 10;
			SerializeAndCompare(dataSet);
			
		}
		
		[Test]
		public void TestColumnHasMaxLengthAsMaxInt()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.MaxLength = int.MaxValue;
			SerializeAndCompare(dataSet);
			
		}
		
		[Test]
		public void TestColumnIsReadOnly()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.ReadOnly = true;
			SerializeAndCompare(dataSet);
			
			column.ReadOnly = false;
			SerializeAndCompare(dataSet);

		}
		
		[Test]
		public void TestColumnHasExpression()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.Expression = "[Int32Column] < 33";
			SerializeAndCompare(dataSet);
			
			column.Expression = null;
			SerializeAndCompare(dataSet);

		}
		
		[Test]
		public void TestColumnHasPrefix()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.Prefix = "prefix";
			SerializeAndCompare(dataSet);
			
			column.Prefix = null;
			SerializeAndCompare(dataSet);

		}
		[Test]
		public void TestColumnHasUri()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.Namespace = "namespace";
			SerializeAndCompare(dataSet);
			
			column.Namespace = null;
			SerializeAndCompare(dataSet);

		}
		
		[Test]
		public void TestColumnHasDefaultNonStringValue()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			
			column.DefaultValue = "0";
			SerializeAndCompare(dataSet);
			
			column.DefaultValue = null;
			SerializeAndCompare(dataSet);
			
			column.DefaultValue = DBNull.Value;
			SerializeAndCompare(dataSet);
			
		}
		
		[Test]
		public void TestColumnHasDefaultStringValue()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[StringColumnName];
			
			column.DefaultValue = "fred";
			SerializeAndCompare(dataSet);
			
			column.DefaultValue = null;
			SerializeAndCompare(dataSet);
			
			column.DefaultValue = DBNull.Value;
			SerializeAndCompare(dataSet);
			
		}
		
		[Test]
		public void TestColumnNoAutoIncrement()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			
			Assert.IsFalse(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == 0);
			Assert.IsTrue(column.AutoIncrementStep == 1);
			SerializeAndCompare(dataSet, 425);

			column.AutoIncrementSeed = -1;
			column.AutoIncrementStep = -1;
			SerializeAndCompare(dataSet, 425);
			Assert.IsFalse(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == -1);
			Assert.IsTrue(column.AutoIncrementStep == -1);
			
			column.AutoIncrementSeed = 100;
			column.AutoIncrementStep = 10;
			SerializeAndCompare(dataSet, 427);
			Assert.IsFalse(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == 100);
			Assert.IsTrue(column.AutoIncrementStep == 10);

			column.AutoIncrementSeed = -100;
			column.AutoIncrementStep = -10;
			SerializeAndCompare(dataSet, 427);
			Assert.IsFalse(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == -100);
			Assert.IsTrue(column.AutoIncrementStep == -10);
			
		}

		[Test]
		public void TestColumnHasUnusedAutoIncrement()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			column.AutoIncrement = true;
			
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == 0);
			Assert.IsTrue(column.AutoIncrementStep == 1);
			SerializeAndCompare(dataSet, 425);

			column.AutoIncrementSeed = -1;
			column.AutoIncrementStep = -1;
			SerializeAndCompare(dataSet, 425);
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == -1);
			Assert.IsTrue(column.AutoIncrementStep == -1);
			
			column.AutoIncrementSeed = 100;
			column.AutoIncrementStep = 10;
			SerializeAndCompare(dataSet, 427);
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == 100);
			Assert.IsTrue(column.AutoIncrementStep == 10);

			column.AutoIncrementSeed = -100;
			column.AutoIncrementStep = -10;
			SerializeAndCompare(dataSet, 427);
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == -100);
			Assert.IsTrue(column.AutoIncrementStep == -10);
		}

		[Test]
		public void TestColumnHasUsedAutoIncrementPositiveDefault()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			column.AutoIncrement = true;
			
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == 0);
			Assert.IsTrue(column.AutoIncrementStep == 1);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == 0);
			DataRow row = CreateSimpleRow(null);
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet, 564);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == 1);
		}

		[Test]
		public void TestColumnHasUsedAutoIncrementNegativeDefault()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			column.AutoIncrement = true;
			column.AutoIncrementSeed = -1;
			column.AutoIncrementStep = -1;
			
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == -1);
			Assert.IsTrue(column.AutoIncrementStep == -1);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == -1);
			DataRow row = CreateSimpleRow(null);
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet, 564);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == -2);
		}

		[Test]
		public void TestColumnHasUsedAutoIncrementPositivePrimaryKeyDefault()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[PrimaryKeyColumnName];
			column.AutoIncrementSeed = 0;
			column.AutoIncrementStep = 1;
			
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == 0);
			Assert.IsTrue(column.AutoIncrementStep == 1);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == 0);
			DataRow row = CreateSimpleRow();
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet, 566);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == 1);
		}

		[Test]
		public void TestColumnHasUsedAutoIncrementNegativePrimaryKeyDefault()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[PrimaryKeyColumnName];
			
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == -1);
			Assert.IsTrue(column.AutoIncrementStep == -1);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == -1);
			DataRow row = CreateSimpleRow();
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet, 566);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == -2);
		}

		[Test]
		public void TestColumnHasUsedAutoIncrementPositiveSetValue()
		{
			int setValue = 100000;
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			column.AutoIncrement = true;
			
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == 0);
			Assert.IsTrue(column.AutoIncrementStep == 1);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == 0);
			DataRow row = CreateSimpleRow(setValue);
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet, 567);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == setValue + 1);
		}

		[Test]
		public void TestColumnHasUsedAutoIncrementNegativeSetValue()
		{
			int setValue = -100000;
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			column.AutoIncrement = true;
			column.AutoIncrementSeed = -1;
			column.AutoIncrementStep = -1;
			
			Assert.IsTrue(column.AutoIncrement);
			Assert.IsTrue(column.AutoIncrementSeed == -1);
			Assert.IsTrue(column.AutoIncrementStep == -1);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == -1);
			DataRow row = CreateSimpleRow(setValue);
			parentTable.Rows.Add(row);
			parentTable.AcceptChanges();
			SerializeAndCompare(dataSet, 568);
			Assert.IsTrue((long) AdoNetComparer.AutoIncrementCurrentFieldInfo.GetValue(column) == setValue - 1);
		}

		[Test]
		public void TestConstraintHasName()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column = parentTable.Columns[Int32ColumnName];
			UniqueConstraint constraint = new UniqueConstraint("named", column);
			
			parentTable.Constraints.Add(constraint);
			SerializeAndCompare(dataSet, 435);
			
			constraint.ConstraintName = "another name";
			SerializeAndCompare(dataSet, 442);
		}
	
		[Test]
		public void TestConstraintHasMultipleColumns()
		{
			dataSet.Tables.Add(parentTable);
			DataColumn column1 = parentTable.Columns[Int32ColumnName];
			DataColumn column2 = parentTable.Columns[StringColumnName];
			UniqueConstraint constraint = new UniqueConstraint("named", new DataColumn[] {column1, column2});
			
			parentTable.Constraints.Add(constraint);
			SerializeAndCompare(dataSet, 437);
		}
		
		[Test]
		public void TestUnnamedFastSerializableDataSet()
		{
			FastSerializableDataSet fastSerializableDataSet = new FastSerializableDataSet();
			fastSerializableDataSet.Tables.Add(parentTable);
			fastSerializableDataSet.Tables.Add(childTable);
			fastSerializableDataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			SerializeAndCompareViaBinaryFormatter(fastSerializableDataSet);
		}
		
		[Test]
		public void TestNamedFastSerializableDataSet()
		{
			FastSerializableDataSet fastSerializableDataSet = new FastSerializableDataSet("My inherited dataset");
			fastSerializableDataSet.Tables.Add(parentTable);
			fastSerializableDataSet.Tables.Add(childTable);
			fastSerializableDataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			SerializeAndCompareViaBinaryFormatter(fastSerializableDataSet);
		}
		
		[Test]
		public void TestUnnamedWrappedDataSet()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			
			WrappedDataSet wrappedDataSet = new WrappedDataSet(dataSet);
			SerializeAndCompareViaBinaryFormatter(wrappedDataSet);
		}
		
		[Test]
		public void TestNamedWrappedDataSet()
		{
			dataSet.DataSetName = "My wrapped dataset";
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			
			WrappedDataSet wrappedDataSet = new WrappedDataSet(dataSet);
			SerializeAndCompareViaBinaryFormatter(wrappedDataSet);
		}
		
		[Test]
		public void TestUniqueKeyConstraintNamed()
		{
			dataSet.Tables.Add(childTable);

			childTable.Constraints.Add(new UniqueConstraint("ConstraintX", childTable.Columns[0]));
			SerializeAndCompare(dataSet, 80);
		}
		
		[Test]
		public void TestUniqueKeyConstraintDefaultName()
		{
			dataSet.Tables.Add(childTable);
			
			childTable.Constraints.Add(new UniqueConstraint(null, childTable.Columns[0]));
			SerializeAndCompare(dataSet, 67);
		}
		
		[Test]
		public void TestPrimaryKeyConstraintNamed()
		{
			dataSet.Tables.Add(childTable);
			
			childTable.Constraints.Add(new UniqueConstraint("ConstraintX", childTable.Columns[0], true));
			SerializeAndCompare(dataSet, 80);
		}
		
		[Test]
		public void TestPrimaryKeyConstraintDefaultName()
		{
			dataSet.Tables.Add(childTable);
			
			childTable.Constraints.Add(new UniqueConstraint(null, childTable.Columns[0], true));
			SerializeAndCompare(dataSet, 67);
		}
		
		[Test]
		public void TestForeignKeyConstraintDefaultName()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			
			childTable.Constraints.Add(new ForeignKeyConstraint(null, parentTable.Columns[0], childTable.Columns[0]));
			childTable.Constraints.Add(new ForeignKeyConstraint(null, parentTable.Columns[0], childTable.Columns[1]));
			SerializeAndCompare(dataSet, 466);
		}
		
		[Test]
		public void TestForeignKeyConstraintToSelf()
		{
			dataSet.Tables.Add(childTable);
			
			childTable.Constraints.Add(new ForeignKeyConstraint("selfRef", childTable.Columns[0], childTable.Columns[1]));
			SerializeAndCompare(dataSet, 83);
		}
		
		[Test]
		public void TestForeignKeyConstraintToSelfDefaultName()
		{
			dataSet.Tables.Add(childTable);
			
			childTable.Constraints.Add(new ForeignKeyConstraint(null, childTable.Columns[0], childTable.Columns[1]));
			SerializeAndCompare(dataSet, 74);
		}
		
		[Test]
		public void Temp()
		{
			dataSet.Tables.Add(parentTable);
			Assert.IsTrue(parentTable.Constraints.Count == 1);
			
			parentTable.Constraints.Add(new UniqueConstraint("", parentTable.Columns[1]));
			Assert.IsTrue(parentTable.Constraints.Count == 2);
			Assert.AreEqual("Constraint2", parentTable.Constraints[1].ConstraintName);
			
			parentTable.Constraints.Add(new UniqueConstraint("Constraint3", parentTable.Columns[2]));
			Assert.IsTrue(parentTable.Constraints.Count == 3);
			Assert.AreEqual("Constraint3", parentTable.Constraints[2].ConstraintName);
			
			parentTable.Constraints.Add(new UniqueConstraint("", parentTable.Columns[3]));
			Assert.IsTrue(parentTable.Constraints.Count == 4);
			Assert.AreEqual("Constraint4", parentTable.Constraints[3].ConstraintName);
			
			serializerRan = true;
		}

		[Test]
		public void Temp2()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			Assert.AreEqual(0, dataSet.Relations.Count);
			
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			Assert.AreEqual(1, dataSet.Relations.Count);
			Assert.AreEqual("ParentChildRelation", dataSet.Relations[0].RelationName);
			Assert.AreEqual(1, childTable.Constraints.Count);
			Assert.AreEqual("ParentChildRelation", childTable.Constraints[0].ConstraintName);
			
			dataSet.Relations.Clear();
			Assert.AreEqual(0, dataSet.Relations.Count);
			Assert.AreEqual(1, childTable.Constraints.Count);
			Assert.AreEqual("ParentChildRelation", childTable.Constraints[0].ConstraintName);
			
			dataSet.Relations.Add(null, parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName]);
			Assert.AreEqual(1, dataSet.Relations.Count);
			Assert.AreEqual("Relation1", dataSet.Relations[0].RelationName);
			Assert.AreEqual(1, childTable.Constraints.Count);
			Assert.AreEqual("ParentChildRelation", childTable.Constraints[0].ConstraintName);
			
			serializerRan = true;
		}
		
		[Test]
		public void Temp3()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			Assert.AreEqual(0, dataSet.Relations.Count);
			
			childTable.Constraints.Add(new ForeignKeyConstraint(null, parentTable.Columns[0], childTable.Columns[1]));
			Assert.AreEqual(1, childTable.Constraints.Count);
			Assert.AreEqual("Constraint1", childTable.Constraints[0].ConstraintName);
			
			serializerRan = true;
		}
		
		[Test]
		public void TestXml()
		{
			dataSet.Tables.Add(parentTable);
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.Columns.Remove(ObjectColumnName); // Xml doesn't like columns of type 'object'
			SerializeAndCompare(dataSet);

			string xml = getDataSetXml(dataSet);
			
			DataSet newDataSet = null;
			using(StringReader stringReader = new StringReader(xml))
			{
				newDataSet = new DataSet();
				newDataSet.ReadXml(stringReader, XmlReadMode.Auto);
			}
			AdoNetComparer.CompareDataSets(dataSet, newDataSet);
			
			string newXml = getDataSetXml(newDataSet);
			Assert.AreEqual(xml, newXml);
			
			newXml = getDataSetXml(newDataSet);
			Assert.AreEqual(xml, newXml);
			
			SerializeAndCompare(newDataSet);
		}
		
		[Test]
		public void TestWrappedDataSet()
		{
			DataSet source = new DataSet();
			WrappedDataSet wrappedDataSet = source;
			Assert.AreSame(source, wrappedDataSet.DataSet);
			
			SerializeAndCompareViaBinaryFormatter(wrappedDataSet);
			
			DataSet destination = wrappedDataSet;
			Assert.AreSame(wrappedDataSet.DataSet, destination);
			Assert.AreSame(source, destination);
			
			serializerRan = true;
		}

		[Test]
		public void TestSingleTable()
		{
			SerializeAndCompare(parentTable);
		}
		
		[Test]
		public void TestSingleTableFromDataSet()
		{
			dataSet.Tables.Add(parentTable);
			SerializeAndCompare(parentTable, 421);
		}
		
		[Test]
		public void TestParentTableRelatedInDataSet()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName], true);
			SerializeAndCompare(parentTable, 421);
		}
		
		[Test]
		public void TestChildTableRelatedInDataSet()
		{
			dataSet.Tables.Add(parentTable);
			dataSet.Tables.Add(childTable);
			dataSet.Relations.Add("ParentChildRelation", parentTable.PrimaryKey[0], childTable.Columns[PrimaryKeyColumnName], true);
			SerializeAndCompare(childTable, 60);
		}
		
		[Test]
		public void TestSimpleDataTable()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();

			DataTable copy = CreateParentTable();
			
			SerializeAndCompareSimpleDataTable(parentTable, copy, 144);
		}
		
		[Test]
		public void TestReadOnlyColumnsWithUnchangedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			parentTable.Columns[StringColumnName].ReadOnly = true;
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 563);
			
		}
		
		[Test]
		public void TestReadOnlyColumnsWithAddedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.Columns[StringColumnName].ReadOnly = true;
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 563);
			
		}
		
		[Test]
		public void TestReadOnlyColumnsWithDeletedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			parentTable.Rows[0].Delete();
			parentTable.Columns[StringColumnName].ReadOnly = true;
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 563);
			
		}
		
		[Test]
		public void TestReadOnlyColumnsWithModifiedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			parentTable.Rows[0][StringColumnName] = "simmo";
			parentTable.Columns[StringColumnName].ReadOnly = true;
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 575);
			
		}
		
		[Test]
		public void TestCalculatedColumnsWithUnchangedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			parentTable.Columns.Add("MyCalculatedColumn", typeof(string), "[" + StringColumnName + "] + 'xx'");
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 610);
			
		}
		
		[Test]
		public void TestCalculatedColumnsWithAddedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.Columns.Add("MyCalculatedColumn", typeof(string), "[" + StringColumnName + "] + 'xx'");
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 610);
			
		}
		
		[Test]
		public void TestCalculatedColumnsWithDeletedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			parentTable.Rows[0].Delete();
			parentTable.Columns.Add("MyCalculatedColumn", typeof(string), "[" + StringColumnName + "] + 'xx'");
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 610);
			
		}
		
		[Test]
		public void TestCalculatedColumnsWithModifiedData()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.AcceptChanges();
			parentTable.Rows[0][StringColumnName] = "simmo";
			parentTable.Columns.Add("MyCalculatedColumn", typeof(string), "[" + StringColumnName + "] + 'xx'");
			dataSet.Tables.Add(parentTable);
			
			SerializeAndCompare(parentTable, 622);
			
		}
		
		[Test]
		public void TestCalculatedColumnsOnSimpleTable()
		{
			parentTable.Rows.Add(CreateSimpleRow());
			parentTable.Columns.Add("MyCalculatedColumn", typeof(string), "[" + StringColumnName + "] + 'xx'");
			parentTable.AcceptChanges();

			DataTable copy = CreateParentTable();
			copy.Columns.Add("MyCalculatedColumn", typeof(string), "[" + StringColumnName + "] + 'xx'");
			
			SerializeAndCompareSimpleDataTable(parentTable, copy, 145);
		}
		
		[Test]
		public void TestSelfReferencingRelationshipWithNonPrimaryKey()
		{
			DataColumn parentColumn = parentTable.Columns.Add("SelfReferencingParentColumn", typeof(int));
			DataColumn childColumn = parentTable.Columns.Add("SelfReferencingChildColumn", typeof(int));
			dataSet.Tables.Add(parentTable);
			
			dataSet.Relations.Add(new DataRelation("SelfReferencingRelation", parentColumn, childColumn, false));

			SerializeAndCompare(dataSet);
		}
	
	}

	/// <summary>
	/// A Timing class based on milliseconds.
	/// 
	/// The timer can be started and stopped and an elapsed time function is available
	/// </summary>
	public class MillisecondTimer {

		#region Static members
		private static ulong _millisecondFrequency;
    [DllImport("kernel32.dll")]
    private static extern int QueryPerformanceCounter(out ulong lpPerformanceCount);

    [DllImport("kernel32.dll")]
    private static extern int QueryPerformanceFrequency(out ulong lpFrequency);

		static MillisecondTimer() {
			ulong APIFrequency;

			QueryPerformanceFrequency(out APIFrequency);

			_millisecondFrequency = APIFrequency / 1000;

		}
		#endregion

		#region Private members
		private ulong _endTime;
		private bool _isRunning;
		private ulong _marker;
		private ulong _startTime;

		/// <summary>
		/// Gets the 64bit system tick counter
		/// </summary>
		/// <returns>Number of ticks</returns>
		private ulong GetTime() {
			ulong currentTime;

			QueryPerformanceCounter(out currentTime);

			return currentTime;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// No parameter constructor
		/// 
		/// Creates an instance but does not start counting
		/// </summary>
		public MillisecondTimer(): this(false) {}

		/// <summary>
		/// Constructor
		/// 
		/// if autoStart is true then the timer is started automatically
		/// </summary>
		/// <param name="autoStart"></param>
		public MillisecondTimer(bool autoStart) {

			if (autoStart) Start();

		}
		#endregion

		#region Public members

		/// <summary>
		/// Returns the number of milliseconds since the last time ElapsedTime was called or since the timer was started.
		/// Returns 0 if the timer is not started.
		/// </summary>
		/// 
		public long ElapsedTime {
			get {
				long elapsedTime;
				if (_isRunning == false)
					return 0;
				else {
					ulong currentTime = GetTime();

					elapsedTime = (long) ((currentTime - _marker) / MillisecondTimer._millisecondFrequency);

					_marker = currentTime;

					return elapsedTime;
				}
			}
		}

		public long PeekElapsedTime() {
			if (_isRunning == false)
				return 0;
			else {
				ulong currentTime = GetTime();

				long elapsedTime = (long) ((currentTime - _marker) / MillisecondTimer._millisecondFrequency);

				return elapsedTime;
			}
		}

		/// <summary>
		/// Stops the timer and stores the current ticks.
		/// </summary>
		public void Stop() {

			if (_isRunning) {

				_endTime = GetTime();

				_isRunning = false;

			}

		}

		/// <summary>
		/// Return true if the timer is currently running
		/// </summary>
		public bool IsRunning {
			get { return _isRunning; }
		}

		/// <summary>
		/// Starts the timer
		/// </summary>
		public void Start() {

			_startTime = _endTime = _marker = GetTime();

			_isRunning = true;

		}

		/// <summary>
		/// Returns the number of milliseconds since the timer was started.
		/// </summary>
		public long TotalTime {
			get {

				if (_isRunning)
					return (long) ((GetTime() - _startTime) / MillisecondTimer._millisecondFrequency);
				else {
					return (long) ((_endTime - _startTime) / MillisecondTimer._millisecondFrequency);
				}
			}
		}
		#endregion

	}
	
}
