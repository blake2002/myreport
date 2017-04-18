#if DEBUG
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using NUnit.Framework;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;

#if NET20
using System.Collections.Generic;
#endif

namespace SimmoTech.Utils.Serialization
{
	
	[TestFixture]
	public class FastSerializerTests
	{
		SerializationWriter Writer;
		private int writerPosition;
		private long writerLength;
		private long objectWriterLength;
		private readonly static TimeSpan NotOptimizableTimeSpan = TimeSpan.FromTicks(1);
		private readonly static DateTime OptimizableDateTime = new DateTime(2006, 11, 8);
		private readonly static DateTime NotOptimizableDateTime = OptimizableDateTime.AddTicks(1);

		[SetUp]
		public void CreateWriter()
		{
			Writer = new SerializationWriter();
			reader = null;
			writerPosition = 0;
			writerLength = -1;
			objectWriterLength = -1;
		}

		[Browsable(false)]
		public SerializationReader Reader
		{
			get
			{
				if(reader == null)
				{
					writerPosition = (int) Writer.BaseStream.Position;
					reader = getReaderFromWriter(Writer);
				}
				return reader;
			}
		}

		SerializationReader reader;

		private void CheckValueAsObject(int expectedSizeAsObject, object value, IComparer comparer)
		{
			SerializationWriter objectWriter = new SerializationWriter();
			objectWriter.WriteObject(value);
			Assert.AreEqual(expectedSizeAsObject, objectWriter.BaseStream.Position - 4, "Incorrect length on Write As Object");

			SerializationReader objectReader = getReaderFromWriter(objectWriter);
			object newValue = objectReader.ReadObject();
			Assert.AreEqual(expectedSizeAsObject, objectReader.BaseStream.Position - 4, "Incorrect length on Read As Object");
			if(comparer == null)
				Assert.AreEqual(value, newValue, "Object Value Read does not match Object Value Write");
			else
			{
				Assert.AreEqual(0, comparer.Compare(value, newValue), "Object Value Read does not match Object Value Write");
			}
		}

		private void CheckValue(int expectedSize, int expectedSizeAsObject, object value, object newValue)
		{
			CheckValue(expectedSize, expectedSizeAsObject, value, newValue, value is IList ? new ListComparer() : null);
		}

		private void CheckValue(int expectedSize, int expectedSizeAsObject, object value, object newValue, IComparer comparer)
		{
			if(expectedSize != -1)
			{
				Assert.AreEqual(expectedSize, writerPosition - 4, "Incorrect length on Write Direct");
				Assert.AreEqual(expectedSize, Reader.BaseStream.Position - 4, "Incorrect length on Read Direct");
			}
			if(value != null) Assert.AreSame(value.GetType(), newValue.GetType());
			if(comparer == null)
				Assert.AreEqual(value, newValue, "Direct Value Read does not match Direct Value Write");
			else
			{
				Assert.AreEqual(0, comparer.Compare(value, newValue), "Direct Value Read does not match Direct Value Write");
			}

			if(expectedSizeAsObject != -1) CheckValueAsObject(expectedSizeAsObject, value, comparer);
		}

		private SerializationReader getReaderFromWriter(SerializationWriter writer)
		{
			byte[] data = writer.ToArray();
			if(writerLength == -1)
				writerLength = data.Length;
			else
			{
				objectWriterLength = data.Length;
			}
			return new SerializationReader(data);
		}

		[Test]
		public void CheckTrueBoolean()
		{
			bool value = true;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadBoolean());
		}

		[Test]
		public void CheckFalseBoolean()
		{
			bool value = false;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadBoolean());
		}

		[Test]
		public void CheckByte()
		{
			Byte value = 33;
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadByte());
		}

		[Test]
		public void CheckByteAsZero()
		{
			Byte value = 0;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadByte());
		}

		[Test]
		public void CheckByteAsOne()
		{
			Byte value = 1;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadByte());
		}

		[Test]
		public void CheckByteAsMaxValue()
		{
			Byte value = Byte.MaxValue;
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadByte());
		}

		[Test]
		public void CheckSByte()
		{
			SByte value = 33;
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadSByte());
		}

		[Test]
		public void CheckSByteNegative()
		{
			SByte value = -33;
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadSByte());
		}

		[Test]
		public void CheckSByteAsZero()
		{
			SByte value = 0;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadSByte());
		}

		[Test]
		public void CheckSByteAsMinValue()
		{
			SByte value = SByte.MinValue;
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadSByte());
		}

		[Test]
		public void CheckSByteAsMaxValue()
		{
			SByte value = SByte.MaxValue;
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadSByte());
		}

		[Test]
		public void CheckSByteAsOne()
		{
			SByte value = 1;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadSByte());
		}

		[Test]
		public void CheckChar()
		{
			Char value = (Char) 33;
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadChar());
		}

		[Test]
		public void CheckCharAsZero()
		{
			Char value = (char) 0;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadChar());
		}

		[Test]
		public void CheckCharAsOne()
		{
			Char value = (Char) 1;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadChar());
		}

		[Test]
		public void CheckDecimal()
		{
			Decimal value = 33;
			Writer.Write(value);
			CheckValue(16, 3, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckDecimalLarge()
		{
			Decimal value = Decimal.MaxValue - 1;
			Writer.Write(value);
			CheckValue(16, 14, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckDecimalNegative()
		{
			Decimal value = 33;
			Writer.Write(value);
			CheckValue(16, 3, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckDecimalLargeNegative()
		{
			Decimal value = Decimal.MinValue + 1;
			Writer.Write(value);
			CheckValue(16, 14, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckDecimalAsZero()
		{
			Decimal value = 0;
			Writer.Write(value);
			CheckValue(16, 1, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckDecimalAsOne()
		{
			Decimal value = 1;
			Writer.Write(value);
			CheckValue(16, 1, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckDecimalAsMinValue()
		{
			Decimal value = Decimal.MinValue;
			Writer.Write(value);
			CheckValue(16, 14, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckDecimalAsMaxValue()
		{
			Decimal value = Decimal.MaxValue;
			Writer.Write(value);
			CheckValue(16, 14, value, Reader.ReadDecimal());
		}

		[Test]
		public void CheckOptimizedDecimal()
		{
			Decimal value = 33;
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedDecimal());
		}

		[Test]
		public void CheckOptimizedDecimalLarge()
		{
			Decimal value = Decimal.MaxValue - 1;
			Writer.WriteOptimized(value);
			CheckValue(13, 14, value, Reader.ReadOptimizedDecimal());
		}

		[Test]
		public void CheckOptimizedDecimalNegative()
		{
			Decimal value = -33;
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedDecimal());
		}

		[Test]
		public void CheckOptimizedDecimalLargeNegative()
		{
			Decimal value = Decimal.MinValue + 1;
			Writer.WriteOptimized(value);
			CheckValue(13, 14, value, Reader.ReadOptimizedDecimal());
		}

		[Test]
		public void CheckOptimizedDecimalAsZero()
		{
			Decimal value = 0;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedDecimal());
		}

		[Test]
		public void CheckOptimizedDecimalAsOne()
		{
			Decimal value = 1;
			Writer.WriteOptimized(value);
			CheckValue(2, 1, value, Reader.ReadOptimizedDecimal());
		}

		public void CheckOptimizedDecimalAsMinValue()
		{
			Decimal value = Decimal.MinValue;
			Writer.WriteOptimized(value);
			CheckValue(16, 13, value, Reader.ReadOptimizedDecimal());
		}

		public void CheckOptimizedDecimalAsMaxValue()
		{
			Decimal value = Decimal.MaxValue;
			Writer.WriteOptimized(value);
			CheckValue(16, 13, value, Reader.ReadOptimizedDecimal());
		}

		[Test]
		public void CheckDouble()
		{
			Double value = 33;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckDoubleLarge()
		{
			Double value = Double.MaxValue - 1;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckDoubleNegative()
		{
			Double value = -33;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckDoubleLargeNegative()
		{
			Double value = Double.MinValue + 1;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckDoubleAsZero()
		{
			Double value = 0;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckDoubleAsOne()
		{
			Double value = 1;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckDoubleAsMinValue()
		{
			Double value = Double.MinValue;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckDoubleAsMaxValue()
		{
			Double value = Double.MaxValue;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadDouble());
		}

		[Test]
		public void CheckSingle()
		{
			Single value = 33;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckSingleLarge()
		{
			Single value = Single.MaxValue - 1;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckSingleNegative()
		{
			Single value = -33;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckSingleLargeNegative()
		{
			Single value = Single.MinValue + 1;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckSingleAsZero()
		{
			Single value = 0;
			Writer.Write(value);
			CheckValue(4, 1, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckSingleAsOne()
		{
			Single value = 1;
			Writer.Write(value);
			CheckValue(4, 1, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckSingleAsMinValue()
		{
			Single value = Single.MinValue;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckSingleAsMaxValue()
		{
			Single value = Single.MaxValue;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadSingle());
		}

		[Test]
		public void CheckInt16()
		{
			Int16 value = 33;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16Large()
		{
			Int16 value = Int16.MaxValue - 1;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16Negative()
		{
			Int16 value = -33;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16LargeNegative()
		{
			Int16 value = Int16.MinValue + 1;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16AsZero()
		{
			Int16 value = 0;
			Writer.Write(value);
			CheckValue(2, 1, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16AsMinusOne()
		{
			Int16 value = -1;
			Writer.Write(value);
			CheckValue(2, 1, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16AsOne()
		{
			Int16 value = 1;
			Writer.Write(value);
			CheckValue(2, 1, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16AsMinValue()
		{
			Int16 value = Int16.MinValue;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt16AsMaxValue()
		{
			Int16 value = Int16.MaxValue;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadInt16());
		}

		[Test]
		public void CheckInt32()
		{
			Int32 value = 33;
			Writer.Write(value);
			CheckValue(4, 2, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32Large()
		{
			Int32 value = Int32.MaxValue - 1;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32Negative()
		{
			Int32 value = -33;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32LargeNegative()
		{
			Int32 value = Int32.MinValue + 1;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32AsZero()
		{
			Int32 value = 0;
			Writer.Write(value);
			CheckValue(4, 1, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32AsMinusOne()
		{
			Int32 value = -1;
			Writer.Write(value);
			CheckValue(4, 1, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32AsOne()
		{
			Int32 value = 1;
			Writer.Write(value);
			CheckValue(4, 1, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32AsMinValue()
		{
			Int32 value = Int32.MinValue;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckInt32AsMaxValue()
		{
			Int32 value = Int32.MaxValue;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadInt32());
		}

		[Test]
		public void CheckOptimizedInt32()
		{
			Int32 value = 33;
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedInt32());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedInt32Large()
		{
			Int32 value = Int32.MaxValue - 1;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedInt32Negative()
		{
			Int32 value = -33;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedInt32LargeNegative()
		{
			Int32 value = Int32.MinValue + 1;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedInt32AsZero()
		{
			Int32 value = 0;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedInt32());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedInt32AsMinusOne()
		{
			Int32 value = -1;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedInt32AsOne()
		{
			Int32 value = 1;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedInt32());
		}

		[Test]
		public void CheckInt64()
		{
			Int64 value = 33;
			Writer.Write(value);
			CheckValue(8, 2, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64Large()
		{
			Int64 value = Int64.MaxValue - 1;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64Negative()
		{
			Int64 value = -33;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64LargeNegative()
		{
			Int64 value = Int64.MinValue + 1;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64AsZero()
		{
			Int64 value = 0;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64AsMinusOne()
		{
			Int64 value = -1;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64AsOne()
		{
			Int64 value = 1;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64AsMinValue()
		{
			Int64 value = Int64.MinValue;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64AsMaxValue()
		{
			Int64 value = Int64.MaxValue;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadInt64());
		}

		[Test]
		public void CheckInt64Optimized()
		{
			Int64 value = 33;
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedInt64());
		}

		[Test]
		public void CheckInt64OptimizedAsZero()
		{
			Int64 value = 0;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedInt64());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt64OptimizedAsMinusOne()
		{
			Int64 value = -1;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckInt64OptimizedAsOne()
		{
			Int64 value = 1;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedInt64());
		}

		[Test]
		public void CheckUInt16()
		{
			UInt16 value = 33;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadUInt16());
		}

		[Test]
		public void CheckUInt16Large()
		{
			UInt16 value = UInt16.MaxValue - 1;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadUInt16());
		}

		[Test]
		public void CheckUInt16AsZero()
		{
			UInt16 value = 0;
			Writer.Write(value);
			CheckValue(2, 1, value, Reader.ReadUInt16());
		}

		[Test]
		public void CheckUInt16AsOne()
		{
			UInt16 value = 1;
			Writer.Write(value);
			CheckValue(2, 1, value, Reader.ReadUInt16());
		}

		[Test]
		public void CheckUInt16AsMaxValue()
		{
			UInt16 value = UInt16.MaxValue;
			Writer.Write(value);
			CheckValue(2, 3, value, Reader.ReadUInt16());
		}

		[Test]
		public void CheckUInt32()
		{
			UInt32 value = 33;
			Writer.Write(value);
			CheckValue(4, 2, value, Reader.ReadUInt32());
		}

		[Test]
		public void CheckUInt32Large()
		{
			UInt32 value = UInt32.MaxValue - 1;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadUInt32());
		}

		[Test]
		public void CheckUInt32AsZero()
		{
			UInt32 value = 0;
			Writer.Write(value);
			CheckValue(4, 1, value, Reader.ReadUInt32());
		}

		[Test]
		public void CheckUInt32AsOne()
		{
			UInt32 value = 1;
			Writer.Write(value);
			CheckValue(4, 1, value, Reader.ReadUInt32());
		}

		[Test]
		public void CheckUInt32AsMaxValue()
		{
			UInt32 value = UInt32.MaxValue;
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadUInt32());
		}

		[Test]
		public void CheckOptimizedUInt32()
		{
			UInt32 value = 33;
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedUInt32());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedUInt32Large()
		{
			UInt32 value = UInt32.MaxValue - 1;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedUInt32AsZero()
		{
			UInt32 value = 0;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedUInt32());
		}

		[Test]
		public void CheckOptimizedUInt32AsOne()
		{
			UInt32 value = 1;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedUInt32());
		}

		[Test]
		public void CheckUInt64()
		{
			UInt64 value = 33;
			Writer.Write(value);
			CheckValue(8, 2, value, Reader.ReadUInt64());
		}

		[Test]
		public void CheckUInt64Large()
		{
			UInt64 value = UInt64.MaxValue - 1;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadUInt64());
		}

		[Test]
		public void CheckUInt64AsZero()
		{
			UInt64 value = 0;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadUInt64());
		}

		[Test]
		public void CheckUInt64AsOne()
		{
			UInt64 value = 1;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadUInt64());
		}

		[Test]
		public void CheckUInt64AsMaxValue()
		{
			UInt64 value = UInt64.MaxValue;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadUInt64());
		}

		[Test]
		public void CheckString()
		{
			string value = "Fast";
			Writer.WriteOptimized(value);
			CheckValue(2, 2, value, Reader.ReadOptimizedString());
		}

		[Test]
		public void CheckStringAsNull()
		{
			string value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedString());
		}

		[Test]
		public void CheckStringAsEmpty()
		{
			string value = "";
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedString());
		}

		[Test]
		public void CheckStringAsSingleChar()
		{
			string value = "X";
			Writer.WriteOptimized(value);
			CheckValue(2, 2, value, Reader.ReadOptimizedString());
		}

		[Test]
		public void CheckStringAsY()
		{
			string value = "Y";
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedString());
		}

		[Test]
		public void CheckStringAsN()
		{
			string value = "N";
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedString());
		}

		[Test]
		public void CheckStringDirect()
		{
			string value = "Fast";
			Writer.WriteStringDirect(value);
			CheckValue(5, 2, value, Reader.ReadStringDirect());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckStringDirectAsNull()
		{
			string value = null;
			Writer.WriteStringDirect(value);
		}

		[Test]
		public void CheckStringDirectAsEmpty()
		{
			string value = "";
			Writer.WriteStringDirect(value);
			CheckValue(1, 1, value, Reader.ReadStringDirect());
		}

		[Test]
		public void CheckStringDirectAsSingleChar()
		{
			string value = "X";
			Writer.WriteStringDirect(value);
			CheckValue(2, 2, value, Reader.ReadStringDirect());
		}

		[Test]
		public void CheckDateTime()
		{
			DateTime value = new DateTime(2006, 11, 16, 10, 31, 11, 11).AddTicks(1);
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadDateTime());
		}

		[Test]
		public void CheckDateTimeAsMinValue()
		{
			DateTime value = DateTime.MinValue;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadDateTime());
		}

		[Test]
		public void CheckDateTimeAsMaxValue()
		{
			DateTime value = DateTime.MaxValue;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadDateTime());
		}

		[Test]
		public void CheckTimeSpan()
		{
			TimeSpan value = TimeSpan.FromDays(1);
			Writer.Write(value);
			CheckValue(8, 4, value, Reader.ReadTimeSpan());
		}

		[Test]
		public void CheckTimeSpanNegative()
		{
			TimeSpan value = TimeSpan.FromDays(-6.44);
			Writer.Write(value);
			CheckValue(8, 5, value, Reader.ReadTimeSpan());
		}

		[Test]
		public void CheckTimeSpanMinValue()
		{
			TimeSpan value = TimeSpan.MinValue;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadTimeSpan());
		}

		[Test]
		public void CheckTimeSpanMaxValue()
		{
			TimeSpan value = TimeSpan.MaxValue;
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadTimeSpan());
		}

		[Test]
		public void CheckTimeSpanMaxValueNoOddTicks()
		{
			TimeSpan value = new TimeSpan(TimeSpan.MaxValue.Ticks - (TimeSpan.MaxValue.Ticks % TimeSpan.TicksPerMillisecond));
			Writer.Write(value);
			CheckValue(8, 9, value, Reader.ReadTimeSpan());
		}

		[Test]
		public void CheckTimeSpanMaxDaysValue()
		{
			TimeSpan value = new TimeSpan((int) TimeSpan.MaxValue.TotalDays, 0, 0);
			Writer.Write(value);
			CheckValue(8, 6, value, Reader.ReadTimeSpan());
		}

		[Test]
		public void CheckTimeSpanAsZero()
		{
			TimeSpan value = TimeSpan.Zero;
			Writer.Write(value);
			CheckValue(8, 1, value, Reader.ReadTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpan()
		{
			TimeSpan value = TimeSpan.FromDays(1);
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanNegative()
		{
			TimeSpan value = TimeSpan.FromDays(-6.44);
			Writer.WriteOptimized(value);
			CheckValue(4, 5, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedTimeSpanMinValue()
		{
			TimeSpan value = TimeSpan.MinValue;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedTimeSpanMaxValue()
		{
			TimeSpan value = TimeSpan.MaxValue;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedTimeSpanMaxValueNoOddTicks()
		{
			TimeSpan value = new TimeSpan(TimeSpan.MaxValue.Ticks - (TimeSpan.MaxValue.Ticks % TimeSpan.TicksPerMillisecond));
			Writer.WriteOptimized(value);
			CheckValue(8, 9, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanMaxDaysValue()
		{
			TimeSpan value = new TimeSpan((int) TimeSpan.MaxValue.TotalDays, 0, 0);
			Writer.WriteOptimized(value);
			CheckValue(5, 6, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanMinValueNoOddTicks()
		{
			TimeSpan value = new TimeSpan(TimeSpan.MinValue.Ticks - (TimeSpan.MinValue.Ticks % TimeSpan.TicksPerMillisecond));
			Writer.WriteOptimized(value);
			CheckValue(8, 9, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanMinDaysValue()
		{
			TimeSpan value = new TimeSpan((int) TimeSpan.MinValue.TotalDays, 0, 0);
			Writer.WriteOptimized(value);
			CheckValue(5, 6, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanAsZero()
		{
			TimeSpan value = TimeSpan.Zero;
			Writer.WriteOptimized(value);
			CheckValue(2, 1, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanSecondsOnly()
		{
			TimeSpan value = TimeSpan.FromSeconds(30);
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanMinutesOnly()
		{
			TimeSpan value = TimeSpan.FromMinutes(59);
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanHoursOnly()
		{
			TimeSpan value = TimeSpan.FromHours(23);
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanMillisecondsOnly()
		{
			TimeSpan value = TimeSpan.FromMilliseconds(999);
			Writer.WriteOptimized(value);
			CheckValue(4, 5, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedTimeSpanTicksOnly()
		{
			TimeSpan value = TimeSpan.FromTicks(59);
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedTimeSpanTimeOnly()
		{
			TimeSpan value = new TimeSpan(21, 10, 0);
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanTimeAndSeconds()
		{
			TimeSpan value = new TimeSpan(0, 21, 10, 1);
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanTimeAndMilliseconds()
		{
			TimeSpan value = new TimeSpan(0, 21, 10, 1);
			value = value.Add(TimeSpan.FromMilliseconds(4));
			Writer.WriteOptimized(value);
			CheckValue(4, 5, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeDayAndSpanSecondsOnly()
		{
			TimeSpan value = TimeSpan.FromSeconds(30);
			value = value.Add(TimeSpan.FromDays(14));
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeDayAndSpanMinutesOnly()
		{
			TimeSpan value = TimeSpan.FromMinutes(59);
			value = value.Add(TimeSpan.FromDays(14));
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeDayAndSpanHoursOnly()
		{
			TimeSpan value = TimeSpan.FromHours(23);
			value = value.Add(TimeSpan.FromDays(14));
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanDayAndMillisecondsOnly()
		{
			TimeSpan value = TimeSpan.FromMilliseconds(999);
			value = value.Add(TimeSpan.FromDays(14));
			Writer.WriteOptimized(value);
			CheckValue(5, 6, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanDayAndTimeOnly()
		{
			TimeSpan value = new TimeSpan(21, 10, 0);
			value = value.Add(TimeSpan.FromDays(14));
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanDayAndTimeAndSeconds()
		{
			TimeSpan value = new TimeSpan(0, 21, 10, 1);
			value = value.Add(TimeSpan.FromDays(140000));
			Writer.WriteOptimized(value);
			CheckValue(6, 7, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckOptimizedTimeSpanTimeDayAndMilliseconds()
		{
			TimeSpan value = new TimeSpan(0, 21, 10, 1);
			value = value.Add(TimeSpan.FromMilliseconds(4));
			value = value.Add(TimeSpan.FromDays(140000));
			Writer.WriteOptimized(value);
			CheckValue(7, 8, value, Reader.ReadOptimizedTimeSpan());
		}

		[Test]
		public void CheckGuid()
		{
			Guid value = Guid.NewGuid();
			Writer.Write(value);
			CheckValue(16, 17, value, Reader.ReadGuid());
		}

		[Test]
		public void CheckGuidAsEmpty()
		{
			Guid value = Guid.Empty;
			Writer.Write(value);
			CheckValue(16, 1, value, Reader.ReadGuid());
		}

		[Test]
		public void CheckObjectArray()
		{
			// 1+2 1+5 1+8 1 1+16   + 1 for length
			object[] value = new object[] {(int) 132, "Fast", (double) 10.64, DateTime.MinValue, Guid.NewGuid()};
			Writer.Write(value);
			CheckValue(34, 34, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayAsNull()
		{
			object[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadObjectArray());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedObjectArrayAsNull()
		{
			object[] value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckObjectArrayAsEmpty()
		{
			object[] value = new object[0];
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithAllEmbeddedNulls()
		{
			object[] value = new object[20];
			Writer.Write(value);
			CheckValue(4, 4, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithAllEmbeddedDBNulls()
		{
			object[] value = new object[20];
			for(int i = 0; i < value.Length; i++) value[i] = DBNull.Value;
			Writer.Write(value);
			CheckValue(4, 4, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithEmbeddedNullsAtEnd()
		{
			object[] value = new object[20];
			for(int i = 0; i < 10; i++) value[i] = i;
			Writer.Write(value);
			CheckValue(22, 22, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithEmbeddedDBNullsAtEnd()
		{
			object[] value = new object[20];
			for(int i = 0; i < value.Length; i++) value[i] = DBNull.Value;
			for(int i = 0; i < 10; i++) value[i] = i;
			Writer.Write(value);
			CheckValue(22, 22, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithEmbeddedNullsAtStart()
		{
			object[] value = new object[20];
			for(int i = 10; i < 20; i++) value[i] = i - 10;
			Writer.Write(value);
			CheckValue(22, 22, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithEmbeddedDBNullsAtStart()
		{
			object[] value = new object[20];
			for(int i = 0; i < value.Length; i++) value[i] = DBNull.Value;
			for(int i = 10; i < 20; i++) value[i] = i - 10;
			Writer.Write(value);
			CheckValue(22, 22, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithEmbeddedNullsInMiddle()
		{
			object[] value = new object[20];
			for(int i = 0; i < 5; i++) value[i] = i;
			for(int i = 15; i < 20; i++) value[i] = i;
			Writer.Write(value);
			CheckValue(22, 22, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithEmbeddedDBNullsInMiddle()
		{
			object[] value = new object[20];
			for(int i = 0; i < value.Length; i++) value[i] = DBNull.Value;
			for(int i = 0; i < 5; i++) value[i] = i;
			for(int i = 15; i < 20; i++) value[i] = i;
			Writer.Write(value);
			CheckValue(22, 22, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithAllEmbeddedNullsExceptOneAtStart()
		{
			object[] value = new object[20];
			value[0] = 1;
			Writer.Write(value);
			CheckValue(5, 5, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithAllEmbeddedDBNullsExceptOneAtStart()
		{
			object[] value = new object[20];
			for(int i = 0; i < value.Length; i++) value[i] = DBNull.Value;
			value[0] = 1;
			Writer.Write(value);
			CheckValue(5, 5, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithAllEmbeddedNullsExceptOneAtEnd()
		{
			object[] value = new object[20];
			value[19] = 1;
			Writer.Write(value);
			CheckValue(5, 5, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckObjectArrayWithAllEmbeddedDBNullsExceptOneAtEnd()
		{
			object[] value = new object[20];
			for(int i = 0; i < value.Length; i++) value[i] = DBNull.Value;
			value[19] = 1;
			Writer.Write(value);
			CheckValue(5, 5, value, Reader.ReadObjectArray());
		}

		[Test]
		public void CheckOptimizedObjectArray()
		{
			// 1+2 1+5 1+8 1 1+16   + 1 for length
			object[] value = new object[] {(int) 132, "Fast", (double) 10.64, DateTime.MinValue, Guid.NewGuid()};
			Writer.WriteOptimized(value);
			CheckValue(33, 34, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckObjectArrayNotNullAsEmpty()
		{
			object[] value = new object[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckObjectArrayNotNullWithAllEmbeddedNulls()
		{
			object[] value = new object[20];
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckObjectArrayNotNullWithEmbeddedNullsAtEnd()
		{
			object[] value = new object[20];
			for(int i = 0; i < 10; i++) value[i] = i;
			Writer.WriteOptimized(value);
			CheckValue(21, 22, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckObjectArrayNotNullWithEmbeddedNullsAtStart()
		{
			object[] value = new object[20];
			for(int i = 10; i < 20; i++) value[i] = i - 10;
			Writer.WriteOptimized(value);
			CheckValue(21, 22, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckObjectArrayNotNullWithEmbeddedNullsInMiddle()
		{
			object[] value = new object[20];
			for(int i = 0; i < 5; i++) value[i] = i;
			for(int i = 15; i < 20; i++) value[i] = i;
			Writer.WriteOptimized(value);
			CheckValue(21, 22, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckObjectArrayNotNullWithAllEmbeddedNullsExceptOneAtStart()
		{
			object[] value = new object[20];
			value[0] = 1;
			Writer.WriteOptimized(value);
			CheckValue(4, 5, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckObjectArrayNotNullWithAllEmbeddedNullsExceptOneAtEnd()
		{
			object[] value = new object[20];
			value[19] = 1;
			Writer.WriteOptimized(value);
			CheckValue(4, 5, value, Reader.ReadOptimizedObjectArray());
		}

		[Test]
		public void CheckBitVector32()
		{
			BitVector32 value = new BitVector32();
			Writer.Write(value);
			CheckValue(4, 5, value, Reader.ReadBitVector32());
		}

		[Test]
		public void CheckBitVector32Optimized()
		{
			BitVector32 value = new BitVector32();
			Writer.WriteOptimized(value);
			CheckValue(1, 5, value, Reader.ReadOptimizedBitVector32());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckBitVector32OptimizedWithTopBitSet()
		{
			BitVector32 value = new BitVector32(-1);
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckArrayList()
		{
			ArrayList value = new ArrayList();
			value.Add(123456);
			value.Add("ABC");
			Writer.Write(value);
			CheckValue(8, 8, value, Reader.ReadArrayList(), new ListComparer());
		}

		[Test]
		public void CheckArrayListNull()
		{
			ArrayList value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadArrayList());
		}

		[Test]
		public void CheckArrayListEmpty()
		{
			ArrayList value = new ArrayList();
			Writer.Write(value);
			CheckValue(2, 2, value, Reader.ReadArrayList(), new ListComparer());
		}

		[Test]
		public void CheckArrayListOptimized()
		{
			ArrayList value = new ArrayList();
			value.Add(123456);
			value.Add("ABC");
			Writer.WriteOptimized(value);
			CheckValue(7, 8, value, Reader.ReadOptimizedArrayList(), new ListComparer());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedArrayListNull()
		{
			ArrayList value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedArrayListEmpty()
		{
			ArrayList value = new ArrayList();
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedArrayList(), new ListComparer());
		}

		[Test]
		public void CheckObjectArrayPairAsAllNulls()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(5, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsAllDBNulls()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++) values1[i] = values2[i] = DBNull.Value;
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(5, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsDifferentNullTypes()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++) values1[i] = DBNull.Value;
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(5, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsSameIntValues()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++) values1[i] = values2[i] = i + 20;
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(23, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsSameStringValues()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++) values1[i] = values2[i] = "abc";
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(7, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsDifferentIntValues()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++)
			{
				values1[i] = -(i + 20);
				values2[i] = -(i + 21);
			}
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(101, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsDifferentStringValues()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++)
			{
				values1[i] = "abc";
				values2[i] = "abcd";
			}
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(25, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsPartlyDifferentLongValues()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++)
			{
				values1[i] = (long) -(i + 20);
				values2[i] = (long) -(i + 20 + (i % 2));
			}
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(141, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckObjectArrayPairAsPartlyDifferentStringValues()
		{
			object[] values1 = new object[10];
			object[] values2 = new object[10];
			for(int i = 0; i < values1.Length; i++)
			{
				values1[i] = "abc";
				values2[i] = "abc" + (((i % 2) == 0) ? "" : "d");
			}
			Writer.WriteOptimized(values1, values2);
			object[] newValues1, newValues2;
			Reader.ReadOptimizedObjectArrayPair(out newValues1, out newValues2);
			CheckValue(20, -1, values1, newValues1);
			CheckValue(-1, -1, values2, newValues2);
		}

		[Test]
		public void CheckTypeAsNull()
		{
			Type value = null;
			Writer.Write(value, true);
			CheckValue(1, 1, value, Reader.ReadType());
		}

		[Test]
		public void CheckSystemTypeFullyQualified()
		{
			Type value = typeof(string);
			Writer.Write(value, true);
			CheckValue(3, 3, value, Reader.ReadType());
			Assert.AreEqual(10 + typeof(string).AssemblyQualifiedName.Length, writerLength);
			Assert.AreEqual(23, objectWriterLength);
		}

		[Test]
		public void CheckSystemTypeNotFullyQualified()
		{
			Type value = typeof(string);
			Writer.Write(value, false);
			CheckValue(3, 3, value, Reader.ReadType());
			Assert.AreEqual(23, writerLength);
			Assert.AreEqual(23, objectWriterLength);
		}

		[Test]
		public void CheckNonSystemTypeFullyQualified()
		{
			Type value = typeof(DataTable);
			Writer.Write(value, true);
			CheckValue(3, 3, value, Reader.ReadType());
			Assert.AreEqual(10 + typeof(DataTable).AssemblyQualifiedName.Length, writerLength);
			Assert.AreEqual(10 + typeof(DataTable).AssemblyQualifiedName.Length, objectWriterLength);
		}

		[Test, ExpectedException(typeof(TypeLoadException))]
		public void CheckNonSystemTypeNotFullyQualified()
		{
			Type value = typeof(DataTable);
			Writer.Write(value, false);
			CheckValue(3, 3, value, Reader.ReadType(true));
		}

		[Test]
		public void CheckSystemOptimizedType()
		{
			Type value = typeof(string);
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedType());
			Assert.AreEqual(22, writerLength);
			Assert.AreEqual(23, objectWriterLength);
		}

		[Test]
		public void CheckNonSystemOptimizedType()
		{
			Type value = typeof(DataTable);
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedType());
			Assert.AreEqual(9 + typeof(DataTable).AssemblyQualifiedName.Length, writerLength);
			Assert.AreEqual(10 + typeof(DataTable).AssemblyQualifiedName.Length, objectWriterLength);
		}

		[Test]
		public void CheckFactoryClass()
		{
			object value = new SampleFactoryClass();
			Writer.WriteTokenizedObject(value);
			object value1 = Reader.ReadTokenizedObject();
			Assert.IsFalse(value == value1);
			Assert.AreEqual(44 + value.GetType().AssemblyQualifiedName.Length, Reader.BaseStream.Length);
		}

		[Test]
		public void CheckFactoryClassMultipleTokens()
		{
			object value = new SampleFactoryClass();
			Writer.WriteTokenizedObject(value);
			Writer.WriteTokenizedObject(value);
			object value1 = Reader.ReadTokenizedObject();
			object value2 = Reader.ReadTokenizedObject();
			Assert.IsFalse(value == value1);
			Assert.AreSame(value1, value2);
			Assert.AreEqual(45 + value.GetType().AssemblyQualifiedName.Length, Reader.BaseStream.Length);
		}

		[Test]
		public void CheckFactoryClassAsType()
		{
			object value = new SampleFactoryClass();
			Writer.WriteTokenizedObject(value, true);
			object value1 = Reader.ReadTokenizedObject();
			Assert.IsFalse(value == value1);

			// This test gets around a .Net serialization feature where the size is off by 1 *sometimes*
			int typeSize = (int) (Reader.BaseStream.Length - value.GetType().AssemblyQualifiedName.Length);
			Assert.IsTrue(typeSize == 9 || typeSize == 10);
		}

		[Test]
		public void CheckFactoryClassAsTypeMultipleTokens()
		{
			object value = new SampleFactoryClass();
			Writer.WriteTokenizedObject(value, true);
			Writer.WriteTokenizedObject(value, true);
			object value1 = Reader.ReadTokenizedObject();
			object value2 = Reader.ReadTokenizedObject();
			Assert.IsFalse(value == value1);
			Assert.AreSame(value1, value2);

			// This test gets around a .Net serialization feature where the size is off by 1 *sometimes*
			int typeSize = (int) (Reader.BaseStream.Length - value.GetType().AssemblyQualifiedName.Length);
			Assert.IsTrue(typeSize == 10 || typeSize == 11);
		}

		[Test]
		public void CheckInt64OptimizedRange()
		{
			long value;

			value = 0;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 1;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 127;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);

			value = 128;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);
			value = 16383;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);

			value = 16384;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);
			value = 2097151;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);

			value = 2097152;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);
			value = 268435455;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);

			value = 268435456;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(5, Writer.BaseStream.Position);
			value = 34359738367;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(5, Writer.BaseStream.Position);

			value = 34359738368;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(6, Writer.BaseStream.Position);
			value = 4398046511103;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(6, Writer.BaseStream.Position);

			value = 4398046511104;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(7, Writer.BaseStream.Position);
			value = 562949953421311;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(7, Writer.BaseStream.Position);

			value = 562949953421312;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(8, Writer.BaseStream.Position);
			value = 72057594037927935;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(8, Writer.BaseStream.Position);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt64OptimizedTooLowMin()
		{
			long value = long.MinValue;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt64OptimizedTooLow()
		{
			long value = -1;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt64OptimizedTooHigh()
		{
			long value = 72057594037927936;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt64OptimizedTooHighMax()
		{
			long value = long.MaxValue;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckUInt64OptimizedRange()
		{
			ulong value;

			value = ulong.MinValue;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 0;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 1;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 127;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);

			value = 128;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);
			value = 16383;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);

			value = 16384;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);
			value = 2097151;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);

			value = 2097152;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);
			value = 268435455;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);

			value = 268435456;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(5, Writer.BaseStream.Position);
			value = 34359738367;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(5, Writer.BaseStream.Position);

			value = 34359738368;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(6, Writer.BaseStream.Position);
			value = 4398046511103;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(6, Writer.BaseStream.Position);

			value = 4398046511104;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(7, Writer.BaseStream.Position);
			value = 562949953421311;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(7, Writer.BaseStream.Position);

			value = 562949953421312;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(8, Writer.BaseStream.Position);
			value = 72057594037927935;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(8, Writer.BaseStream.Position);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckUInt64OptimizedTooHigh()
		{
			ulong value = 72057594037927936;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckUInt64OptimizedTooHighMax()
		{
			ulong value = ulong.MaxValue;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckInt32OptimizedRange()
		{
			int value;

			value = 0;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 1;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 127;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);

			value = 128;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);
			value = 16383;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);

			value = 16384;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);
			value = 2097151;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);

			value = 2097152;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);
			value = 268435455;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt32OptimizedTooLowMin()
		{
			int value = int.MinValue;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt32OptimizedTooLow()
		{
			int value = -1;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt32OptimizedTooHigh()
		{
			int value = 268435456;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckInt32OptimizedTooHighMax()
		{
			int value = int.MaxValue;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckUInt32OptimizedRange()
		{
			uint value;

			value = uint.MinValue;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 0;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 1;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);
			value = 127;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(1, Writer.BaseStream.Position);

			value = 128;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);
			value = 16383;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(2, Writer.BaseStream.Position);

			value = 16384;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);
			value = 2097151;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(3, Writer.BaseStream.Position);

			value = 2097152;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);
			value = 268435455;
			Writer.BaseStream.Position = 0;
			Writer.WriteOptimized(value);
			Assert.AreEqual(4, Writer.BaseStream.Position);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckUInt32OptimizedTooHigh()
		{
			uint value = 268435456;
			Writer.WriteOptimized(value);
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckUInt32OptimizedTooHighMax()
		{
			uint value = uint.MaxValue;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedDateTimeWithDateOnly()
		{
			DateTime value = new DateTime(2006, 9, 17);
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedDateTime());
		}

		[Test]
		public void CheckOptimizedDateTimeWithDateHoursAndMinutes()
		{
			DateTime value = new DateTime(2006, 9, 17, 12, 20, 0);
			Writer.WriteOptimized(value);
			CheckValue(5, 6, value, Reader.ReadOptimizedDateTime());
		}

		[Test]
		public void CheckOptimizedDateTimeWithDateHoursAndMinutesAndSeconds()
		{
			DateTime value = new DateTime(2006, 9, 17, 12, 20, 22);
			Writer.WriteOptimized(value);
			CheckValue(6, 7, value, Reader.ReadOptimizedDateTime());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedDateTimeWithOddTicks()
		{
			DateTime value = new DateTime(2006, 9, 17, 12, 20, 22);
			value = value.AddTicks(1);
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckCustomClassArrayNull()
		{
			CustomClass[] value = null;
			Writer.Write(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadObjectArray(typeof(CustomClass));
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedCustomClassArrayNull()
		{
			CustomClass[] value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckCustomClassTypedArrayNull()
		{
			CustomClass[] value = null;
			Writer.WriteTypedArray(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadTypedArray();
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassArrayEmpty()
		{
			CustomClass[] value = new CustomClass[0];
			Writer.Write(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadObjectArray(typeof(CustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedCustomClassArrayEmpty()
		{
			CustomClass[] value = new CustomClass[0];
			Writer.WriteOptimized(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadOptimizedObjectArray(typeof(CustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassTypedArrayEmpty()
		{
			CustomClass[] value = new CustomClass[0];
			Writer.WriteTypedArray(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadTypedArray();
			CheckValue(3, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassArrayOne()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.Write(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadObjectArray(typeof(CustomClass));
			CheckValue(200, 202, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedCustomClassArrayOne()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteOptimized(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadOptimizedObjectArray(typeof(CustomClass));
			CheckValue(199, 202, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckCustomClassTypedArrayOne()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteTypedArray(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadTypedArray();
			CheckValue(202, 202, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassArrayMulti()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass(), new CustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadObjectArray(typeof(CustomClass));
			CheckValue(398, 400, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedCustomClassArrayMulti()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass(), new CustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadOptimizedObjectArray(typeof(CustomClass));
			CheckValue(397, 400, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckCustomClassTypedArrayMulti()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass(), new CustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadTypedArray();
			CheckValue(400, 400, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassArrayMultiNulls()
		{
			CustomClass[] value = new CustomClass[] { null, null, null };
			Writer.Write(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadObjectArray(typeof(CustomClass));
			CheckValue(4, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedCustomClassArrayMultiNulls()
		{
			CustomClass[] value = new CustomClass[] { null, null, null };
			Writer.WriteOptimized(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadOptimizedObjectArray(typeof(CustomClass));
			CheckValue(3, 6, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckCustomClassTypedArrayMultiNulls()
		{
			CustomClass[] value = new CustomClass[] { null, null, null };
			Writer.WriteTypedArray(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadTypedArray();
			CheckValue(6, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassArrayMultiNoNulls()
		{
			CustomClass[] value = new CustomClass[] { null, new CustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadObjectArray(typeof(CustomClass));
			CheckValue(202, 204, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedCustomClassArrayMultiNoNulls()
		{
			CustomClass[] value = new CustomClass[] { null, new CustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadOptimizedObjectArray(typeof(CustomClass));
			CheckValue(201, 204, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassTypedArrayMultiNoNulls()
		{
			CustomClass[] value = new CustomClass[] { null, new CustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadTypedArray();
			CheckValue(204, 204, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomClassArrayMixed()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass(), null, new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.Write(value);
			CustomClass[] result = (CustomClass[]) Reader.ReadObjectArray(typeof(CustomClass));
			CheckValue(425, 427, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedCustomClassArrayMixed()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass(), null, new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.WriteOptimized(value);
			CustomClass[] result = (CustomClass[])Reader.ReadOptimizedObjectArray(typeof(CustomClass));
			CheckValue(424, 427, value, result, new ListComparer());
		}

		[Test, ExpectedException(typeof(ArrayTypeMismatchException))]
		public void CheckCustomClassArrayMixedWrongElementType()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass(), null, new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.Write(value);
			Reader.ReadObjectArray(typeof(InheritedCustomClass));
		}

		[Test, ExpectedException(typeof(ArrayTypeMismatchException))]
		public void CheckOptimizedCustomClassArrayMixedWrongElementType()
		{
			CustomClass[] value = new CustomClass[] { new CustomClass(), null, new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.WriteOptimized(value);
			Reader.ReadOptimizedObjectArray(typeof(InheritedCustomClass));
		}
		
		
		[Test]
		public void CheckInheritedCustomClassArrayNull()
		{
			InheritedCustomClass[] value = null;
			Writer.Write(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadObjectArray(typeof(InheritedCustomClass));
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedInheritedCustomClassArrayNull()
		{
			InheritedCustomClass[] value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckInheritedCustomClassTypedArrayNull()
		{
			InheritedCustomClass[] value = null;
			Writer.WriteTypedArray(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadTypedArray();
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test]
		public void CheckInheritedCustomClassArrayEmpty()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[0];
			Writer.Write(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadObjectArray(typeof(InheritedCustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedInheritedCustomClassArrayEmpty()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[0];
			Writer.WriteOptimized(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(InheritedCustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckInheritedCustomClassTypedArrayEmpty()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[0];
			Writer.WriteTypedArray(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadTypedArray();
			CheckValue(3, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckInheritedCustomClassArrayOne()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.Write(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadObjectArray(typeof(InheritedCustomClass));
			CheckValue(226, 228, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedInheritedCustomClassArrayOne()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteOptimized(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(InheritedCustomClass));
			CheckValue(225, 228, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckInheritedCustomClassTypedArrayOne()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteTypedArray(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadTypedArray();
			CheckValue(228, 228, value, result, new ListComparer());
		}

		[Test]
		public void CheckInheritedCustomClassArrayMulti()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { new InheritedCustomClass(), new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadObjectArray(typeof(InheritedCustomClass));
			CheckValue(450, 452, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedInheritedCustomClassArrayMulti()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { new InheritedCustomClass(), new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(InheritedCustomClass));
			CheckValue(449, 452, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckInheritedCustomClassTypedArrayMulti()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { new InheritedCustomClass(), new InheritedCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadTypedArray();
			CheckValue(452, 452, value, result, new ListComparer());
		}

		[Test]
		public void CheckInheritedCustomClassArrayMultiNulls()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { null, null, null };
			Writer.Write(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadObjectArray(typeof(InheritedCustomClass));
			CheckValue(4, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedInheritedCustomClassArrayMultiNulls()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { null, null, null };
			Writer.WriteOptimized(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(InheritedCustomClass));
			CheckValue(3, 6, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckInheritedCustomClassTypedArrayMultiNulls()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { null, null, null };
			Writer.WriteTypedArray(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadTypedArray();
			CheckValue(6, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckInheritedCustomClassArrayMultiNoNulls()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { null, new InheritedCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadObjectArray(typeof(InheritedCustomClass));
			CheckValue(228, 230, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedInheritedCustomClassArrayMultiNoNulls()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { null, new InheritedCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(InheritedCustomClass));
			CheckValue(227, 230, value, result, new ListComparer());
		}

		[Test, Category("Improve this")]
		public void CheckInheritedCustomClassTypedArrayMultiNoNulls()
		{
			InheritedCustomClass[] value = new InheritedCustomClass[] { null, new InheritedCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			InheritedCustomClass[] result = (InheritedCustomClass[]) Reader.ReadTypedArray();
			CheckValue(230, 230, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassArrayNull()
		{
			IntelligentCustomClass[] value = null;
			Writer.Write(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadObjectArray(typeof(IntelligentCustomClass));
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedIntelligentCustomClassArrayNull()
		{
			IntelligentCustomClass[] value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckIntelligentCustomClassTypedArrayNull()
		{
			IntelligentCustomClass[] value = null;
			Writer.WriteTypedArray(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassArrayEmpty()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[0];
			Writer.Write(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadObjectArray(typeof(IntelligentCustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedIntelligentCustomClassArrayEmpty()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[0];
			Writer.WriteOptimized(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(IntelligentCustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassTypedArrayEmpty()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[0];
			Writer.WriteTypedArray(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(3, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassArrayOne()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.Write(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadObjectArray(typeof(IntelligentCustomClass));
			CheckValue(8, 7, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedIntelligentCustomClassArrayOne()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteOptimized(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(IntelligentCustomClass));
			CheckValue(7, 7, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassTypedArrayOne()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteTypedArray(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(7, 7, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassArrayMulti()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass(), new IntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadObjectArray(typeof(IntelligentCustomClass));
			CheckValue(14, 10, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedIntelligentCustomClassArrayMulti()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass(), new IntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(IntelligentCustomClass));
			CheckValue(13, 10, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassTypedArrayMulti()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass(), new IntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(10, 10, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassArrayMultiNulls()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { null, null, null };
			Writer.Write(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadObjectArray(typeof(IntelligentCustomClass));
			CheckValue(4, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedIntelligentCustomClassArrayMultiNulls()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { null, null, null };
			Writer.WriteOptimized(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(IntelligentCustomClass));
			CheckValue(3, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassTypedArrayMultiNulls()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { null, null, null };
			Writer.WriteTypedArray(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(6, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassArrayMultiNoNulls()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { null, new IntelligentCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadObjectArray(typeof(IntelligentCustomClass));
			CheckValue(10, 9, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedIntelligentCustomClassArrayMultiNoNulls()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { null, new IntelligentCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(IntelligentCustomClass));
			CheckValue(9, 9, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassTypedArrayMultiNoNulls()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { null, new IntelligentCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(9, 9, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomClassArrayMixed()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass(), null, new InheritedIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.Write(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[])Reader.ReadObjectArray(typeof(IntelligentCustomClass));
			CheckValue(19, 21, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedIntelligentCustomClassArrayMixed()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass(), null, new InheritedIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.WriteOptimized(value);
			IntelligentCustomClass[] result = (IntelligentCustomClass[])Reader.ReadOptimizedObjectArray(typeof(IntelligentCustomClass));
			CheckValue(18, 21, value, result, new ListComparer());
		}

		[Test, ExpectedException(typeof(ArrayTypeMismatchException))]
		public void CheckIntelligentCustomClassArrayMixedWrongElementType()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass(), null, new InheritedIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.Write(value);
			Reader.ReadObjectArray(typeof(InheritedIntelligentCustomClass));
		}

		[Test, ExpectedException(typeof(ArrayTypeMismatchException))]
		public void CheckOptimizedIntelligentCustomClassArrayMixedWrongElementType()
		{
			IntelligentCustomClass[] value = new IntelligentCustomClass[] { new IntelligentCustomClass(), null, new InheritedIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.WriteOptimized(value);
			Reader.ReadOptimizedObjectArray(typeof(InheritedIntelligentCustomClass));
		}
		
		[Test]
		public void CheckSemiIntelligentCustomClassArrayNull()
		{
			SemiIntelligentCustomClass[] value = null;
			Writer.Write(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedSemiIntelligentCustomClassArrayNull()
		{
			SemiIntelligentCustomClass[] value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckSemiIntelligentCustomClassTypedArrayNull()
		{
			SemiIntelligentCustomClass[] value = null;
			Writer.WriteTypedArray(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassArrayEmpty()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[0];
			Writer.Write(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedSemiIntelligentCustomClassArrayEmpty()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[0];
			Writer.WriteOptimized(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(1, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassTypedArrayEmpty()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[0];
			Writer.WriteTypedArray(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(3, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassArrayOne()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.Write(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(215, 217, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedSemiIntelligentCustomClassArrayOne()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteOptimized(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(214, 217, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassTypedArrayOne()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			Writer.WriteTypedArray(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(217, 217, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassArrayMulti()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass(), new SemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(428, 430, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedSemiIntelligentCustomClassArrayMulti()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass(), new SemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(427, 430, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassTypedArrayMulti()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass(), new SemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(430, 430, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassArrayMultiNulls()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { null, null, null };
			Writer.Write(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(4, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedSemiIntelligentCustomClassArrayMultiNulls()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { null, null, null };
			Writer.WriteOptimized(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(3, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassTypedArrayMultiNulls()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { null, null, null };
			Writer.WriteTypedArray(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(6, 6, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassArrayMultiNoNulls()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { null, new SemiIntelligentCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.Write(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(217, 219, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedSemiIntelligentCustomClassArrayMultiNoNulls()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { null, new SemiIntelligentCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteOptimized(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadOptimizedObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(216, 219, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassTypedArrayMultiNoNulls()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { null, new SemiIntelligentCustomClass(), null };
			value[1].IntValue = 6;
			value[1].StringValue = "def";
			Writer.WriteTypedArray(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[]) Reader.ReadTypedArray();
			CheckValue(219, 219, value, result, new ListComparer());
		}

		[Test]
		public void CheckSemiIntelligentCustomClassArrayMixed()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass(), null, new InheritedSemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.Write(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[])Reader.ReadObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(455, 457, value, result, new ListComparer());
		}

		[Test]
		public void CheckOptimizedSemiIntelligentCustomClassArrayMixed()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass(), null, new InheritedSemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.WriteOptimized(value);
			SemiIntelligentCustomClass[] result = (SemiIntelligentCustomClass[])Reader.ReadOptimizedObjectArray(typeof(SemiIntelligentCustomClass));
			CheckValue(454, 457, value, result, new ListComparer());
		}

		[Test, ExpectedException(typeof(ArrayTypeMismatchException))]
		public void CheckSemiIntelligentCustomClassArrayMixedWrongElementType()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass(), null, new InheritedSemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.Write(value);
			Reader.ReadObjectArray(typeof(InheritedSemiIntelligentCustomClass));
		}

		[Test, ExpectedException(typeof(ArrayTypeMismatchException))]
		public void CheckOptimizedSemiIntelligentCustomClassArrayMixedWrongElementType()
		{
			SemiIntelligentCustomClass[] value = new SemiIntelligentCustomClass[] { new SemiIntelligentCustomClass(), null, new InheritedSemiIntelligentCustomClass() };
			value[0].IntValue = 5;
			value[0].StringValue = "abc";
			value[2].IntValue = 6;
			value[2].StringValue = "def";
			Writer.WriteOptimized(value);
			Reader.ReadOptimizedObjectArray(typeof(InheritedSemiIntelligentCustomClass));
		}

		[Test]
		public void CheckCustomStruct()
		{
			CustomStruct value = new CustomStruct(10, "abc");
			Writer.WriteObject(value);
			CheckValue(199, -1, value, Reader.ReadObject());
		}

		[Test]
		public void CheckIntelligentCustomStruct()
		{
			IntelligentCustomStruct value = new IntelligentCustomStruct(10, "abc");
			Writer.WriteObject(value);
			CheckValue(6, -1, value, Reader.ReadObject());
		}

		[Test]
		public void CheckStringArrayNull()
		{
			string[] value = null;
			Writer.Write(value);

			CheckValue(1, 1, value, Reader.ReadStringArray());
		}

		[Test]
		public void CheckStringArrayEmpty()
		{
			string[] value = new string[0];
			Writer.Write(value);

			CheckValue(1, 2, value, Reader.ReadStringArray());
		}

		[Test]
		public void CheckStringArrayOne()
		{
			string[] value = new string[] {"Simon"};
			Writer.Write(value);

			CheckValue(4, 4, value, Reader.ReadStringArray());
		}

		[Test]
		public void CheckStringArrayMulti()
		{
			string[] value = new string[] {"abc", "defgh", "ijkl"};
			Writer.Write(value);

			CheckValue(8, 8, value, Reader.ReadStringArray());
		}

		[Test]
		public void CheckStringArrayMultiNulls()
		{
			string[] value = new string[] {null, null, null};
			Writer.Write(value);

			CheckValue(4, 4, value, Reader.ReadStringArray());
		}

		[Test]
		public void CheckStringArrayMultiNoNulls()
		{
			string[] value = new string[] {null, "hewitt", null};
			Writer.Write(value);

			CheckValue(6, 6, value, Reader.ReadStringArray());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedStringArrayNull()
		{
			string[] value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedStringArrayEmpty()
		{
			string[] value = new string[0];
			Writer.WriteOptimized(value);

			CheckValue(1, 2, value, Reader.ReadOptimizedStringArray());
		}

		[Test]
		public void CheckOptimizedStringArrayOne()
		{
			string[] value = new string[] {"Simon"};
			Writer.WriteOptimized(value);

			CheckValue(3, 4, value, Reader.ReadOptimizedStringArray());
		}

		[Test]
		public void CheckOptimizedStringArrayMulti()
		{
			string[] value = new string[] {"abc", "defgh", "ijkl"};
			Writer.WriteOptimized(value);

			CheckValue(7, 8, value, Reader.ReadOptimizedStringArray());
		}

		[Test]
		public void CheckOptimizedStringArrayMultiNulls()
		{
			string[] value = new string[] {null, null, null};
			Writer.WriteOptimized(value);

			CheckValue(3, 4, value, Reader.ReadOptimizedStringArray());
		}

		[Test]
		public void CheckOptimizedStringArrayNoMultiNulls()
		{
			string[] value = new string[] {null, "hewitt", null};
			Writer.WriteOptimized(value);

			CheckValue(5, 6, value, Reader.ReadOptimizedStringArray());
		}

		[Test]
		public void CheckInt16ArrayNull()
		{
			Int16[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadInt16Array());
		}

		[Test]
		public void CheckInt16ArrayEmpty()
		{
			Int16[] value = new Int16[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadInt16Array());
		}

		[Test]
		public void CheckInt16ArrayOne()
		{
			Int16[] value = new Int16[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 2), 2 + (1 * 2), value, Reader.ReadInt16Array());
		}

		[Test]
		public void CheckInt16ArrayTwo()
		{
			Int16[] value = new Int16[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 2), 2 + (2 * 2), value, Reader.ReadInt16Array());
		}

		[Test]
		public void CheckInt16ArrayMany()
		{
			Int16[] value = new Int16[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 2), 2 + (100 * 2), value, Reader.ReadInt16Array());
		}

		[Test]
		public void CheckUInt16ArrayNull()
		{
			UInt16[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadUInt16Array());
		}

		[Test]
		public void CheckUInt16ArrayEmpty()
		{
			UInt16[] value = new UInt16[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadUInt16Array());
		}

		[Test]
		public void CheckUInt16ArrayOne()
		{
			UInt16[] value = new UInt16[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 2), 2 + (1 * 2), value, Reader.ReadUInt16Array());
		}

		[Test]
		public void CheckUInt16ArrayTwo()
		{
			UInt16[] value = new UInt16[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 2), 2 + (2 * 2), value, Reader.ReadUInt16Array());
		}

		[Test]
		public void CheckUInt16ArrayMany()
		{
			UInt16[] value = new UInt16[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 2), 2 + (100 * 2), value, Reader.ReadUInt16Array());
		}

		[Test]
		public void CheckInt32ArrayNull()
		{
			Int32[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadInt32Array());
		}

		[Test]
		public void CheckInt32ArrayEmpty()
		{
			Int32[] value = new Int32[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadInt32Array());
		}

		[Test]
		public void CheckInt32ArrayOne()
		{
			Int32[] value = new Int32[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 4), 3 + (1 * 1), value, Reader.ReadInt32Array());
		}

		[Test]
		public void CheckInt32ArrayTwo()
		{
			Int32[] value = new Int32[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 4), 3 + (2 * 1), value, Reader.ReadInt32Array());
		}

		[Test]
		public void CheckInt32ArrayMany()
		{
			Int32[] value = new Int32[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 4), 3 + (100 * 1), value, Reader.ReadInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayNull()
		{
			Int32[] value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayEmpty()
		{
			Int32[] value = new Int32[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayOneOptimizable()
		{
			Int32[] value = new Int32[1];
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 1), 3 + (1 * 1), value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayOneNotOptimizable()
		{
			Int32[] value = new Int32[] {-1};
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 4), 3 + (1 * 4), value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayTwoOptimizable()
		{
			Int32[] value = new Int32[2];
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 1), 3 + (2 * 1), value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayTwoNotOptimizable()
		{
			Int32[] value = new Int32[] {-1, -1};
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 4), 3 + (2 * 4), value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayTwoPartOptimizable()
		{
			Int32[] value = new Int32[] {1, -1};
			Writer.WriteOptimized(value);
			int expected = 2 + (1 * 1 + 1 * 4);
			expected += 1 + 1; // For BitArray
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayManyNoneOptimizable()
		{
			Int32[] value = new Int32[100];
			for(int i = 0; i < value.Length; i++) value[i] = -1;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 4);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayManyAllOptimizable()
		{
			Int32[] value = new Int32[100];
			for(int i = 0; i < value.Length; i++) value[i] = 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 1);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayManyPartiallyOptimizableAtLimit()
		{
			Int32[] value = new Int32[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 80) ? -1 : 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (20 * 1) + (80 * 4);
			expected += 1 + 1 + (100 / 8); // For BitArray 
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckOptimizedInt32ArrayManyPartiallyOptimizableAboveLimit()
		{
			Int32[] value = new Int32[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 81) ? -1 : 1;
			Writer.WriteOptimized(value);
			int expected = 1 + 1 + (100 * 4);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt32Array());
		}

		[Test]
		public void CheckUInt32ArrayNull()
		{
			UInt32[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadUInt32Array());
		}

		[Test]
		public void CheckUInt32ArrayEmpty()
		{
			UInt32[] value = new UInt32[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadUInt32Array());
		}

		[Test]
		public void CheckUInt32ArrayOne()
		{
			UInt32[] value = new UInt32[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 4), 3 + (1 * 1), value, Reader.ReadUInt32Array());
		}

		[Test]
		public void CheckUInt32ArrayTwo()
		{
			UInt32[] value = new UInt32[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 4), 3 + (2 * 1), value, Reader.ReadUInt32Array());
		}

		[Test]
		public void CheckUInt32ArrayMany()
		{
			UInt32[] value = new UInt32[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 4), 3 + (100 * 1), value, Reader.ReadUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayNull()
		{
			UInt32[] value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayEmpty()
		{
			UInt32[] value = new UInt32[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayOneOptimizable()
		{
			UInt32[] value = new UInt32[1];
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 1), 3 + (1 * 1), value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayOneNotOptimizable()
		{
			UInt32[] value = new UInt32[] {UInt32.MaxValue};
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 4), 3 + (1 * 4), value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayTwoOptimizable()
		{
			UInt32[] value = new UInt32[2];
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 1), 3 + (2 * 1), value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayTwoNotOptimizable()
		{
			UInt32[] value = new UInt32[] {UInt32.MaxValue, UInt32.MaxValue};
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 4), 3 + (2 * 4), value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayTwoPartOptimizable()
		{
			UInt32[] value = new UInt32[] {1, UInt32.MaxValue};
			Writer.WriteOptimized(value);
			int expected = 2 + (1 * 1 + 1 * 4);
			expected += 1 + 1; // For BitArray
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayManyNoneOptimizable()
		{
			UInt32[] value = new UInt32[100];
			for(int i = 0; i < value.Length; i++) value[i] = UInt32.MaxValue;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 4);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayManyAllOptimizable()
		{
			UInt32[] value = new UInt32[100];
			for(int i = 0; i < value.Length; i++) value[i] = 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 1);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayManyPartiallyOptimizableAtLimit()
		{
			UInt32[] value = new UInt32[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 80) ? UInt32.MaxValue : 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (20 * 1) + (80 * 4);
			expected += 1 + 1 + (100 / 8); // For BitArray 
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckOptimizedUInt32ArrayManyPartiallyOptimizableAboveLimit()
		{
			UInt32[] value = new UInt32[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 81) ? UInt32.MaxValue : 1;
			Writer.WriteOptimized(value);
			int expected = 1 + 1 + (100 * 4);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt32Array());
		}

		[Test]
		public void CheckUInt64ArrayNull()
		{
			UInt64[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadUInt64Array());
		}

		[Test]
		public void CheckUInt64ArrayEmpty()
		{
			UInt64[] value = new UInt64[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadUInt64Array());
		}

		[Test]
		public void CheckUInt64ArrayOne()
		{
			UInt64[] value = new UInt64[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 1), value, Reader.ReadUInt64Array());
		}

		[Test]
		public void CheckUInt64ArrayTwo()
		{
			UInt64[] value = new UInt64[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 1), value, Reader.ReadUInt64Array());
		}

		[Test]
		public void CheckUInt64ArrayMany()
		{
			UInt64[] value = new UInt64[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 8), 3 + (100 * 1), value, Reader.ReadUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayNull()
		{
			UInt64[] value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayEmpty()
		{
			UInt64[] value = new UInt64[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayOneOptimizable()
		{
			UInt64[] value = new UInt64[1];
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 1), 3 + (1 * 1), value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayOneNotOptimizable()
		{
			UInt64[] value = new UInt64[] {UInt64.MaxValue};
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 8), value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayTwoOptimizable()
		{
			UInt64[] value = new UInt64[2];
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 1), 3 + (2 * 1), value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayTwoNotOptimizable()
		{
			UInt64[] value = new UInt64[] {UInt64.MaxValue, UInt64.MaxValue};
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 8), value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayTwoPartOptimizable()
		{
			UInt64[] value = new UInt64[] {1, UInt64.MaxValue};
			Writer.WriteOptimized(value);
			int expected = 2 + (1 * 1 + 1 * 8);
			expected += 1 + 1; // For BitArray
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayManyNoneOptimizable()
		{
			UInt64[] value = new UInt64[100];
			for(int i = 0; i < value.Length; i++) value[i] = UInt64.MaxValue;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayManyAllOptimizable()
		{
			UInt64[] value = new UInt64[100];
			for(int i = 0; i < value.Length; i++) value[i] = 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 1);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayManyPartiallyOptimizableAtLimit()
		{
			UInt64[] value = new UInt64[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 80) ? UInt64.MaxValue : 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (20 * 1) + (80 * 8);
			expected += 1 + 1 + (100 / 8); // For BitArray 
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckOptimizedUInt64ArrayManyPartiallyOptimizableAboveLimit()
		{
			UInt64[] value = new UInt64[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 81) ? UInt64.MaxValue : 1;
			Writer.WriteOptimized(value);
			int expected = 1 + 1 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedUInt64Array());
		}

		[Test]
		public void CheckInt64ArrayNull()
		{
			Int64[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadInt64Array());
		}

		[Test]
		public void CheckInt64ArrayEmpty()
		{
			Int64[] value = new Int64[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadInt64Array());
		}

		[Test]
		public void CheckInt64ArrayOne()
		{
			Int64[] value = new Int64[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 1), value, Reader.ReadInt64Array());
		}

		[Test]
		public void CheckInt64ArrayTwo()
		{
			Int64[] value = new Int64[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 1), value, Reader.ReadInt64Array());
		}

		[Test]
		public void CheckInt64ArrayMany()
		{
			Int64[] value = new Int64[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 8), 3 + (100 * 1), value, Reader.ReadInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayNull()
		{
			Int64[] value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayEmpty()
		{
			Int64[] value = new Int64[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayOneOptimizable()
		{
			Int64[] value = new Int64[1];
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 1), 3 + (1 * 1), value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayOneNotOptimizable()
		{
			Int64[] value = new Int64[] {-1};
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 8), value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayTwoOptimizable()
		{
			Int64[] value = new Int64[2];
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 1), 3 + (2 * 1), value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayTwoNotOptimizable()
		{
			Int64[] value = new Int64[] {-1, -1};
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 8), value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayTwoPartOptimizable()
		{
			Int64[] value = new Int64[] {1, -1};
			Writer.WriteOptimized(value);
			int expected = 2 + (1 * 1 + 1 * 8);
			expected += 1 + 1; // For BitArray
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayManyNoneOptimizable()
		{
			Int64[] value = new Int64[100];
			for(int i = 0; i < value.Length; i++) value[i] = -1;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayManyAllOptimizable()
		{
			Int64[] value = new Int64[100];
			for(int i = 0; i < value.Length; i++) value[i] = 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 1);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayManyPartiallyOptimizableAtLimit()
		{
			Int64[] value = new Int64[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 80) ? -1 : 1;
			Writer.WriteOptimized(value);
			int expected = 2 + (20 * 1) + (80 * 8);
			expected += 1 + 1 + (100 / 8); // For BitArray 
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckOptimizedInt64ArrayManyPartiallyOptimizableAboveLimit()
		{
			Int64[] value = new Int64[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 81) ? -1 : 1;
			Writer.WriteOptimized(value);
			int expected = 1 + 1 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedInt64Array());
		}

		[Test]
		public void CheckTimeSpanArrayNull()
		{
			TimeSpan[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadTimeSpanArray());
		}

		[Test]
		public void CheckTimeSpanArrayEmpty()
		{
			TimeSpan[] value = new TimeSpan[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadTimeSpanArray());
		}

		[Test]
		public void CheckTimeSpanArrayOne()
		{
			TimeSpan[] value = new TimeSpan[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 2), value, Reader.ReadTimeSpanArray());
		}

		[Test]
		public void CheckTimeSpanArrayTwo()
		{
			TimeSpan[] value = new TimeSpan[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 2), value, Reader.ReadTimeSpanArray());
		}

		[Test]
		public void CheckTimeSpanArrayMany()
		{
			TimeSpan[] value = new TimeSpan[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 8), 3 + (100 * 2), value, Reader.ReadTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayNull()
		{
			TimeSpan[] value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayEmpty()
		{
			TimeSpan[] value = new TimeSpan[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayOneOptimizable()
		{
			TimeSpan[] value = new TimeSpan[1];
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 2), 3 + (1 * 2), value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayOneNotOptimizable()
		{
			TimeSpan[] value = new TimeSpan[] {NotOptimizableTimeSpan};
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 8), value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayTwoOptimizable()
		{
			TimeSpan[] value = new TimeSpan[2];
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 2), 3 + (2 * 2), value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayTwoNotOptimizable()
		{
			TimeSpan[] value = new TimeSpan[] {NotOptimizableTimeSpan, NotOptimizableTimeSpan};
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 8), value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayTwoPartOptimizable()
		{
			TimeSpan[] value = new TimeSpan[] {TimeSpan.Zero, NotOptimizableTimeSpan};
			Writer.WriteOptimized(value);
			int expected = 2 + (1 * 2 + 1 * 8);
			expected += 1 + 1; // For BitArray
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayManyNoneOptimizable()
		{
			TimeSpan[] value = new TimeSpan[100];
			for(int i = 0; i < value.Length; i++) value[i] = NotOptimizableTimeSpan;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayManyAllOptimizable()
		{
			TimeSpan[] value = new TimeSpan[100];
			for(int i = 0; i < value.Length; i++) value[i] = TimeSpan.Zero;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 2);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayManyPartiallyOptimizableAtLimit()
		{
			TimeSpan[] value = new TimeSpan[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 80) ? NotOptimizableTimeSpan : TimeSpan.Zero;
			Writer.WriteOptimized(value);
			int expected = 2 + (20 * 2) + (80 * 8);
			expected += 1 + 1 + (100 / 8); // For BitArray 
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckOptimizedTimeSpanArrayManyPartiallyOptimizableAboveLimit()
		{
			TimeSpan[] value = new TimeSpan[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 81) ? NotOptimizableTimeSpan : TimeSpan.Zero;
			Writer.WriteOptimized(value);
			int expected = 1 + 1 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedTimeSpanArray());
		}

		[Test]
		public void CheckDateTimeArrayNull()
		{
			DateTime[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadDateTimeArray());
		}

		[Test]
		public void CheckDateTimeArrayEmpty()
		{
			DateTime[] value = new DateTime[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadDateTimeArray());
		}

		[Test]
		public void CheckDateTimeArrayOne()
		{
			DateTime[] value = new DateTime[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 3), value, Reader.ReadDateTimeArray());
		}

		[Test]
		public void CheckDateTimeArrayTwo()
		{
			DateTime[] value = new DateTime[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 3), value, Reader.ReadDateTimeArray());
		}

		[Test]
		public void CheckDateTimeArrayMany()
		{
			DateTime[] value = new DateTime[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 8), 3 + (100 * 3), value, Reader.ReadDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayNull()
		{
			DateTime[] value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayEmpty()
		{
			DateTime[] value = new DateTime[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayOneOptimizable()
		{
			DateTime[] value = new DateTime[1];
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 3), 3 + (1 * 3), value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayOneNotOptimizable()
		{
			DateTime[] value = new DateTime[] {NotOptimizableDateTime};
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 8), 3 + (1 * 8), value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayTwoOptimizable()
		{
			DateTime[] value = new DateTime[2];
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 3), 3 + (2 * 3), value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayTwoNotOptimizable()
		{
			DateTime[] value = new DateTime[] {NotOptimizableDateTime, NotOptimizableDateTime};
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 8), 3 + (2 * 8), value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayTwoPartOptimizable()
		{
			DateTime[] value = new DateTime[] {OptimizableDateTime, NotOptimizableDateTime};
			Writer.WriteOptimized(value);
			int expected = 2 + (1 * 3 + 1 * 8);
			expected += 1 + 1; // For BitArray
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayManyNoneOptimizable()
		{
			DateTime[] value = new DateTime[100];
			for(int i = 0; i < value.Length; i++) value[i] = NotOptimizableDateTime;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayManyAllOptimizable()
		{
			DateTime[] value = new DateTime[100];
			for(int i = 0; i < value.Length; i++) value[i] = OptimizableDateTime;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 3);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayManyPartiallyOptimizableAtLimit()
		{
			DateTime[] value = new DateTime[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 80) ? NotOptimizableDateTime : OptimizableDateTime;
			Writer.WriteOptimized(value);
			int expected = 2 + (20 * 3) + (80 * 8);
			expected += 1 + 1 + (100 / 8); // For BitArray 
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckOptimizedDateTimeArrayManyPartiallyOptimizableAboveLimit()
		{
			DateTime[] value = new DateTime[100];
			for(int i = 0; i < value.Length; i++) value[i] = (i < 81) ? NotOptimizableDateTime : OptimizableDateTime;
			Writer.WriteOptimized(value);
			int expected = 1 + 1 + (100 * 8);
			CheckValue(expected, 1 + expected, value, Reader.ReadOptimizedDateTimeArray());
		}

		[Test]
		public void CheckDecimalArrayNull()
		{
			Decimal[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadDecimalArray());
		}

		[Test]
		public void CheckDecimalArrayEmpty()
		{
			Decimal[] value = new Decimal[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadDecimalArray());
		}

		[Test]
		public void CheckDecimalArrayOne()
		{
			Decimal[] value = new Decimal[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 1), 2 + (1 * 1), value, Reader.ReadDecimalArray());
		}

		[Test]
		public void CheckDecimalArrayTwo()
		{
			Decimal[] value = new Decimal[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 1), 2 + (2 * 1), value, Reader.ReadDecimalArray());
		}

		[Test]
		public void CheckDecimalArrayMany()
		{
			Decimal[] value = new Decimal[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 1), 2 + (100 * 1), value, Reader.ReadDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayNull()
		{
			Decimal[] value = null;
			Writer.WriteOptimized(value);
			CheckValue(1, 1, value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayEmpty()
		{
			Decimal[] value = new Decimal[0];
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayOneOptimizable()
		{
			Decimal[] value = new Decimal[1];
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 1), 2 + (1 * 1), value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayOneNotOptimizable()
		{
			Decimal[] value = new Decimal[] {-1};
			Writer.WriteOptimized(value);
			CheckValue(2 + (1 * 2), 2 + (1 * 2), value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayTwoOptimizable()
		{
			Decimal[] value = new Decimal[2];
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 1), 2 + (2 * 1), value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayTwoNotOptimizable()
		{
			Decimal[] value = new Decimal[] {-1, -1};
			Writer.WriteOptimized(value);
			CheckValue(2 + (2 * 2), 2 + (2 * 2), value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayTwoPartOptimizable()
		{
			Decimal[] value = new Decimal[] {1, -1};
			Writer.WriteOptimized(value);
			int expected = 2 + (1 * 1 + 1 * 1);
			expected += 1 + 1; // For BitArray
			CheckValue(expected, expected, value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayManyNoneOptimizable()
		{
			Decimal[] value = new Decimal[100];
			for(int i = 0; i < value.Length; i++) value[i] = -1;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 2);
			CheckValue(expected, expected, value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckOptimizedDecimalArrayManyAllOptimizable()
		{
			Decimal[] value = new Decimal[100];
			for(int i = 0; i < value.Length; i++) value[i] = 0;
			Writer.WriteOptimized(value);
			int expected = 2 + (100 * 1);
			CheckValue(expected, expected, value, Reader.ReadOptimizedDecimalArray());
		}

		[Test]
		public void CheckSByteArrayNull()
		{
			SByte[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadSByteArray());
		}

		[Test]
		public void CheckSByteArrayEmpty()
		{
			SByte[] value = new SByte[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadSByteArray());
		}

		[Test]
		public void CheckSByteArrayOne()
		{
			SByte[] value = new SByte[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 1), 2 + (1 * 1), value, Reader.ReadSByteArray());
		}

		[Test]
		public void CheckSByteArrayTwo()
		{
			SByte[] value = new SByte[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 1), 2 + (2 * 1), value, Reader.ReadSByteArray());
		}

		[Test]
		public void CheckSByteArrayMany()
		{
			SByte[] value = new SByte[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 1), 2 + (100 * 1), value, Reader.ReadSByteArray());
		}

		[Test]
		public void CheckSingleArrayNull()
		{
			Single[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadSingleArray());
		}

		[Test]
		public void CheckSingleArrayEmpty()
		{
			Single[] value = new Single[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadSingleArray());
		}

		[Test]
		public void CheckSingleArrayOne()
		{
			Single[] value = new Single[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 4), 2 + (1 * 4), value, Reader.ReadSingleArray());
		}

		[Test]
		public void CheckSingleArrayTwo()
		{
			Single[] value = new Single[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 4), 2 + (2 * 4), value, Reader.ReadSingleArray());
		}

		[Test]
		public void CheckSingleArrayMany()
		{
			Single[] value = new Single[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 4), 2 + (100 * 4), value, Reader.ReadSingleArray());
		}

		[Test]
		public void CheckDoubleArrayNull()
		{
			Double[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadDoubleArray());
		}

		[Test]
		public void CheckDoubleArrayEmpty()
		{
			Double[] value = new Double[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadDoubleArray());
		}

		[Test]
		public void CheckDoubleArrayOne()
		{
			Double[] value = new Double[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 8), 2 + (1 * 8), value, Reader.ReadDoubleArray());
		}

		[Test]
		public void CheckDoubleArrayTwo()
		{
			Double[] value = new Double[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 8), 2 + (2 * 8), value, Reader.ReadDoubleArray());
		}

		[Test]
		public void CheckDoubleArrayMany()
		{
			Double[] value = new Double[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 8), 2 + (100 * 8), value, Reader.ReadDoubleArray());
		}

		[Test]
		public void CheckGuidArrayNull()
		{
			Guid[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadGuidArray());
		}

		[Test]
		public void CheckGuidArrayEmpty()
		{
			Guid[] value = new Guid[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadGuidArray());
		}

		[Test]
		public void CheckGuidArrayOne()
		{
			Guid[] value = new Guid[1];
			Writer.Write(value);
			CheckValue(2 + (1 * 16), 2 + (1 * 16), value, Reader.ReadGuidArray());
		}

		[Test]
		public void CheckGuidArrayTwo()
		{
			Guid[] value = new Guid[2];
			Writer.Write(value);
			CheckValue(2 + (2 * 16), 2 + (2 * 16), value, Reader.ReadGuidArray());
		}

		[Test]
		public void CheckGuidArrayMany()
		{
			Guid[] value = new Guid[100];
			Writer.Write(value);
			CheckValue(2 + (100 * 16), 2 + (100 * 16), value, Reader.ReadGuidArray());
		}

		[Test]
		public void CheckCharArrayNull()
		{
			Char[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadCharArray());
		}

		[Test]
		public void CheckCharArrayEmpty()
		{
			Char[] value = new char[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadCharArray());
		}

		[Test]
		public void CheckCharArrayOne()
		{
			Char[] value = new char[] {'a'};
			Writer.Write(value);
			CheckValue(2 + 1, 2 + 1, value, Reader.ReadCharArray());
		}

		[Test]
		public void CheckCharArrayTwo()
		{
			Char[] value = new char[] {'a', 'b'};
			Writer.Write(value);
			CheckValue(2 + 2, 2 + 2, value, Reader.ReadCharArray());
		}

		[Test]
		public void CheckCharArrayMany()
		{
			Char[] value = new char[100];
			Writer.Write(value);
			CheckValue(2 + 100, 2 + 100, value, Reader.ReadCharArray());
		}

		[Test]
		public void CheckByteArrayNull()
		{
			Byte[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadByteArray());
		}

		[Test]
		public void CheckByteArrayEmpty()
		{
			Byte[] value = new Byte[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadByteArray());
		}

		[Test]
		public void CheckByteArrayOne()
		{
			Byte[] value = new Byte[] {10};
			Writer.Write(value);
			CheckValue(2 + 1, 2 + 1, value, Reader.ReadByteArray());
		}

		[Test]
		public void CheckByteArrayTwo()
		{
			Byte[] value = new Byte[] {10, 11};
			Writer.Write(value);
			CheckValue(2 + 2, 2 + 2, value, Reader.ReadByteArray());
		}

		[Test]
		public void CheckByteArrayMany()
		{
			Byte[] value = new Byte[100];
			Writer.Write(value);
			CheckValue(2 + 100, 2 + 100, value, Reader.ReadByteArray());
		}

		[Test]
		public void CheckBooleanArrayNull()
		{
			Boolean[] value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadBooleanArray());
		}

		[Test]
		public void CheckBooleanArrayEmpty()
		{
			Boolean[] value = new Boolean[0];
			Writer.Write(value);
			CheckValue(1, 2, value, Reader.ReadBooleanArray());
		}

		[Test]
		public void CheckBooleanArrayOne()
		{
			Boolean[] value = new Boolean[1];
			Writer.Write(value);
			CheckValue(3, 3, value, Reader.ReadBooleanArray());
		}

		[Test]
		public void CheckBooleanArrayTwo()
		{
			Boolean[] value = new Boolean[2];
			Writer.Write(value);
			CheckValue(3, 3, value, Reader.ReadBooleanArray());
		}

		[Test]
		public void CheckBooleanArrayOneHundred()
		{
			Boolean[] value = new Boolean[100];
			Writer.Write(value);
			CheckValue(15, 15, value, Reader.ReadBooleanArray());
		}

		[Test]
		public void CheckBitArrayNull()
		{
			BitArray value = null;
			Writer.Write(value);
			CheckValue(1, 1, value, Reader.ReadBitArray());
		}

		[Test]
		public void CheckBitArrayEmpty()
		{
			BitArray value = new BitArray(0);
			Writer.Write(value);
			CheckValue(2, 2, value, Reader.ReadBitArray(), new BitArrayComparer());
		}

		[Test]
		public void CheckBitArrayOne()
		{
			BitArray value = new BitArray(1, true);
			Writer.Write(value);
			CheckValue(3, 3, value, Reader.ReadBitArray(), new BitArrayComparer());
		}

		[Test]
		public void CheckBitArrayNine()
		{
			BitArray value = new BitArray(9, true);
			Writer.Write(value);
			CheckValue(4, 4, value, Reader.ReadBitArray(), new BitArrayComparer());
		}

		[Test]
		public void CheckBitArrayOneHundred()
		{
			BitArray value = new BitArray(100, true);
			Writer.Write(value);
			CheckValue(15, 15, value, Reader.ReadBitArray(), new BitArrayComparer());
		}

		[Test, ExpectedException(typeof(OptimizationException))]
		public void CheckOptimizedBitArrayNull()
		{
			BitArray value = null;
			Writer.WriteOptimized(value);
		}

		[Test]
		public void CheckOptimizedBitArrayEmpty()
		{
			BitArray value = new BitArray(0);
			Writer.WriteOptimized(value);
			CheckValue(1, 2, value, Reader.ReadOptimizedBitArray(), new BitArrayComparer());
		}

		[Test]
		public void CheckOptimizedBitArrayOne()
		{
			BitArray value = new BitArray(1);
			Writer.WriteOptimized(value);
			CheckValue(2, 3, value, Reader.ReadOptimizedBitArray(), new BitArrayComparer());
		}

		[Test]
		public void CheckOptimizedBitArrayNine()
		{
			BitArray value = new BitArray(9);
			Writer.WriteOptimized(value);
			CheckValue(3, 4, value, Reader.ReadOptimizedBitArray(), new BitArrayComparer());
		}

		[Test]
		public void CheckOptimizedBitArrayOneHundred()
		{
			BitArray value = new BitArray(100);
			Writer.WriteOptimized(value);
			CheckValue(14, 15, value, Reader.ReadOptimizedBitArray(), new BitArrayComparer());
		}

#if NET20
		private static Dictionary<string, int> createSimpleStringIntDictionary()
		{
			Dictionary<string, int> value = new Dictionary<string, int>();
			value.Add("a", 1);
			value.Add("b", 2);
			value.Add("c", 3);
			value.Add("d", 4);
			value.Add("e", 5);
			return value;
		}

		private static Dictionary<int, string> createSimpleIntStringDictionary()
		{
			Dictionary<int, string> value = new Dictionary<int, string>();
			value.Add(1, "a");
			value.Add(2, "b");
			value.Add(3, "c");
			value.Add(4, "d");
			value.Add(5, "e");
			return value;
		}

		private static Dictionary<int, int> createSimpleIntIntDictionary()
		{
			Dictionary<int, int> value = new Dictionary<int, int>();
			value.Add(1, 1);
			value.Add(2, 4);
			value.Add(3, 9);
			value.Add(4, 16);
			value.Add(5, 25);
			return value;
		}

		private static Dictionary<int, object> createSimpleIntObjectDictionary()
		{
			Dictionary<int, object> value = new Dictionary<int, object>();
			value.Add(1, "a");
			value.Add(2, "b");
			value.Add(3, "c");
			value.Add(4, "d");
			value.Add(5, "e");
			return value;
		}

		private static Dictionary<int, CustomClass> createSimpleIntCustomClassDictionary()
		{
			Dictionary<int, CustomClass> value = new Dictionary<int, CustomClass>();
			value.Add(1, new CustomClass(1, "a"));
			value.Add(2, new CustomClass(2, "b"));
			value.Add(3, new CustomClass(3, "b"));
			value.Add(4, new CustomClass(4, "b"));
			value.Add(5, new CustomClass(5, "b"));
			return value;
		}

		private static Dictionary<int, IntelligentCustomClass> createSimpleIntIntelligentCustomClassDictionary()
		{
			Dictionary<int, IntelligentCustomClass> value = new Dictionary<int, IntelligentCustomClass>();
			value.Add(1, new IntelligentCustomClass(1, "a"));
			value.Add(2, new IntelligentCustomClass(2, "b"));
			value.Add(3, new IntelligentCustomClass(3, "b"));
			value.Add(4, new IntelligentCustomClass(4, "b"));
			value.Add(5, new IntelligentCustomClass(5, "b"));
			return value;
		}

		private static Dictionary<string, string> createSimpleStringStringDictionary()
		{
			Dictionary<string, string> value = new Dictionary<string, string>();
			value.Add("a", "Alpha");
			value.Add("b", "Bravo");
			value.Add("c", "Charlie");
			value.Add("d", "Delta");
			value.Add("e", "Echo");
			return value;
		}

		[Test]
		public void CheckDictionaryStringAndInt()
		{
			Dictionary<string, int> value = createSimpleStringIntDictionary();
			Writer.Write(value);
			CheckValue(20, 1441, value, Reader.ReadDictionary<string, int>(), new DictionaryComparer<string, int>());
		}

		[Test]
		public void CheckDictionaryStringAndIntPreCreate()
		{
			Dictionary<string, int> value = createSimpleStringIntDictionary();
			Writer.Write(value);
			Dictionary<string, int> result = new Dictionary<string, int>();
			Reader.ReadDictionary(result);
			CheckValue(20, 1441, value, result, new DictionaryComparer<string, int>());
		}

		[Test]
		public void CheckDictionaryIntAndString()
		{
			Dictionary<int, string> value = createSimpleIntStringDictionary();
			Writer.Write(value);
			CheckValue(20, 1439, value, Reader.ReadDictionary<int, string>(), new DictionaryComparer<int, string>());
		}

		[Test]
		public void CheckDictionaryIntAndInt()
		{
			Dictionary<int, int> value = createSimpleIntIntDictionary();
			Writer.Write(value);
			CheckValue(16, 1421, value, Reader.ReadDictionary<int, int>(), new DictionaryComparer<int, int>());
		}

		[Test]
		public void CheckDictionaryIntAndObject()
		{
			Dictionary<int, object> value = createSimpleIntObjectDictionary();
			Writer.Write(value);
			CheckValue(20, 1439, value, Reader.ReadDictionary<int, object>(), new DictionaryComparer<int, object>());
		}

		[Test]
		public void CheckDictionaryIntAndCustomClass()
		{
			Dictionary<int, CustomClass> value = createSimpleIntCustomClassDictionary();
			Writer.Write(value);
			CheckValue(990, 1862, value, Reader.ReadDictionary<int, CustomClass>(), new DictionaryComparer<int, CustomClass>());
		}

		[Test]
		public void CheckDictionaryIntAndIntelligentCustomClass()
		{
			Dictionary<int, IntelligentCustomClass> value = createSimpleIntIntelligentCustomClassDictionary();
			Writer.Write(value);
			CheckValue(25, 1928, value, Reader.ReadDictionary<int, IntelligentCustomClass>(), new DictionaryComparer<int, IntelligentCustomClass>());
		}

		[Test]
		public void CheckDictionaryStringAndString()
		{
			Dictionary<string, string> value = createSimpleStringStringDictionary();
			Writer.Write(value);
			CheckValue(24, 1480, value, Reader.ReadDictionary<string, string>(), new DictionaryComparer<string, string>());
		}
		
		[Test]
		public void CheckDictionaryIntAndCustomStruct()
		{
			Dictionary<int, CustomStruct> value = new Dictionary<int, CustomStruct>();
			CustomStruct s;
			s = new CustomStruct();  s.IntValue = 1; s.StringValue = "A"; value.Add(1, s);
			s = new CustomStruct();  s.IntValue = 2; s.StringValue = "B"; value.Add(2, s);
			s = new CustomStruct();  s.IntValue = 3; s.StringValue = "C"; value.Add(3, s);
			Writer.Write(value);
			CheckValue(599, 1783, value, Reader.ReadDictionary<int, CustomStruct>(), new DictionaryComparer<int, CustomStruct>());
		}
		
		[Test]
		public void CheckListOfInt()
		{
			List<int> value = new List<int>();
			value.Add(1);
			value.Add(2);
			value.Add(3);
			value.Add(4);
			value.Add(5);
			Writer.Write(value);
			CheckValue(8, 238, value, Reader.ReadList<int>(), new ListComparer<int>());
		}

		[Test]
		public void CheckListOfString()
		{
			List<string> value = new List<string>();
			value.Add("a");
			value.Add("b");
			value.Add("c");
			value.Add("d");
			value.Add("e");
			Writer.Write(value);
			CheckValue(12, 242, value, Reader.ReadList<string>(), new ListComparer<string>());
		}

		[Test]
		public void CheckListOfObject()
		{
			List<object> value = new List<object>();
			value.Add(new object());
			value.Add(new object());
			value.Add(new object());
			value.Add(new object());
			value.Add(new object());
			Writer.Write(value);
			CheckValue(212, -1, value, Reader.ReadList<object>());
		}

		[Test]
		public void CheckListOfCustomStruct()
		{
			List<CustomStruct> value = new List<CustomStruct>();
			CustomStruct s;
			s = new CustomStruct(); s.IntValue = 1; s.StringValue = "A"; value.Add(s);
			s = new CustomStruct(); s.IntValue = 2; s.StringValue = "B"; value.Add(s);
			s = new CustomStruct(); s.IntValue = 3; s.StringValue = "C"; value.Add(s);
			Writer.Write(value);
			CheckValue(593, 575, value, Reader.ReadList<CustomStruct>(), new ListComparer<CustomStruct>());
		}

		[Test]
		public void CheckListOfIntelligentCustomStruct()
		{
			List<IntelligentCustomStruct> value = new List<IntelligentCustomStruct>();
			IntelligentCustomStruct s;
			s = new IntelligentCustomStruct(); s.IntValue = 1; s.StringValue = "A"; value.Add(s);
			s = new IntelligentCustomStruct(); s.IntValue = 2; s.StringValue = "B"; value.Add(s);
			s = new IntelligentCustomStruct(); s.IntValue = 3; s.StringValue = "C"; value.Add(s);
			Writer.Write(value);
			CheckValue(11, 619, value, Reader.ReadList<IntelligentCustomStruct>(), new ListComparer<IntelligentCustomStruct>());
		}

		[Test]
		public void CheckListOfCustomClass()
		{
			List<CustomClass> value = new List<CustomClass>();
			CustomClass s;
			s = new CustomClass(); s.IntValue = 1; s.StringValue = "A"; value.Add(s);
			s = new CustomClass(); s.IntValue = 2; s.StringValue = "B"; value.Add(s);
			s = new CustomClass(); s.IntValue = 3; s.StringValue = "C"; value.Add(s);
			Writer.Write(value);
			CheckValue(590, 573, value, Reader.ReadList<CustomClass>(), new ListComparer<CustomClass>());
		}

		[Test]
		public void CheckListOfSemiIntelligentCustomClass()
		{
			List<SemiIntelligentCustomClass> value = new List<SemiIntelligentCustomClass>();
			SemiIntelligentCustomClass s;
			s = new SemiIntelligentCustomClass(); s.IntValue = 1; s.StringValue = "A"; value.Add(s);
			s = new SemiIntelligentCustomClass(); s.IntValue = 2; s.StringValue = "B"; value.Add(s);
			s = new SemiIntelligentCustomClass(); s.IntValue = 3; s.StringValue = "C"; value.Add(s);
			Writer.Write(value);
			CheckValue(635, 633, value, Reader.ReadList<SemiIntelligentCustomClass>(), new ListComparer<SemiIntelligentCustomClass>());
		}

		[Test]
		public void CheckListOfIntelligentCustomClass()
		{
			List<IntelligentCustomClass> value = new List<IntelligentCustomClass>();
			IntelligentCustomClass s;
			s = new IntelligentCustomClass(); s.IntValue = 1; s.StringValue = "A"; value.Add(s);
			s = new IntelligentCustomClass(); s.IntValue = 2; s.StringValue = "B"; value.Add(s);
			s = new IntelligentCustomClass(); s.IntValue = 3; s.StringValue = "C"; value.Add(s);
			Writer.Write(value);
			CheckValue(11, 617, value, Reader.ReadList<IntelligentCustomClass>(), new ListComparer<IntelligentCustomClass>());
		}

		[Test]
		public void CheckListOfMixedCustomClass()
		{
			List<CustomClass> value = new List<CustomClass>();
			CustomClass baseClass = new CustomClass(); baseClass.IntValue = 1; baseClass.StringValue = "A"; value.Add(baseClass);
			InheritedCustomClass inheritedClass = new InheritedCustomClass(); inheritedClass.IntValue = 1; inheritedClass.StringValue = "A"; value.Add(inheritedClass);
			Writer.Write(value);
			CheckValue(420, 643, value, Reader.ReadList<CustomClass>(), new ListComparer<CustomClass>());
		}

		[Test]
		public void CheckListOfMixedSemiIntelligentCustomClass()
		{
			List<SemiIntelligentCustomClass> value = new List<SemiIntelligentCustomClass>();
			SemiIntelligentCustomClass baseClass = new SemiIntelligentCustomClass(); baseClass.IntValue = 1; baseClass.StringValue = "A"; value.Add(baseClass);
			InheritedSemiIntelligentCustomClass inheritedClass = new InheritedSemiIntelligentCustomClass(); inheritedClass.IntValue = 1; inheritedClass.StringValue = "A"; value.Add(inheritedClass);
			Writer.Write(value);
			CheckValue(450, 718, value, Reader.ReadList<SemiIntelligentCustomClass>(), new ListComparer<SemiIntelligentCustomClass>());
		}

		[Test]
		public void CheckListOfMixedIntelligentCustomClass()
		{
			List<IntelligentCustomClass> value = new List<IntelligentCustomClass>();
			IntelligentCustomClass baseClass = new IntelligentCustomClass(); baseClass.IntValue = 1; baseClass.StringValue = "A"; value.Add(baseClass);
			InheritedIntelligentCustomClass inheritedClass = new InheritedIntelligentCustomClass(); inheritedClass.IntValue = 1; inheritedClass.StringValue = "A"; value.Add(inheritedClass);
			Writer.Write(value);
			CheckValue(18, 698, value, Reader.ReadList<IntelligentCustomClass>(), new ListComparer<IntelligentCustomClass>());
		}

		[Test]
		public void CheckNullableInt16Null()
		{
			Int16? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableInt16());
		}

		[Test]
		public void CheckInt16AsNullable()
		{
			Int16 value = 33;
			Writer.WriteNullable(value);
			CheckValue(3, 3, value, Reader.ReadNullableInt16());
		}

		[Test]
		public void CheckNullableInt16()
		{
			Int16? value = 33;
			Writer.WriteNullable(value);
			CheckValue(3, 3, value, Reader.ReadNullableInt16());
		}

		[Test]
		public void CheckNullableByteNull()
		{
			Byte? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableByte());
		}

		[Test]
		public void CheckNullableByte()
		{
			Byte? value = 33;
			Writer.WriteNullable(value);
			CheckValue(2, 2, value, Reader.ReadNullableByte());
		}

		[Test]
		public void CheckNullableGuidNull()
		{
			Guid? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableGuid());
		}

		[Test]
		public void CheckNullableGuid()
		{
			Guid? value = Guid.NewGuid();
			Writer.WriteNullable(value);
			CheckValue(17, 17, value, Reader.ReadNullableGuid());
		}

		[Test]
		public void CheckNullableCharNull()
		{
			Char? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableChar());
		}

		[Test]
		public void CheckNullableChar()
		{
			Char? value = 'A';
			Writer.WriteNullable(value);
			CheckValue(2, 2, value, Reader.ReadNullableChar());
		}

		[Test]
		public void CheckNullableBooleanNull()
		{
			Boolean? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableBoolean());
		}

		[Test]
		public void CheckNullableBooleanTrue()
		{
			Boolean? value = true;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableBoolean());
		}

		[Test]
		public void CheckNullableBooleanFalse()
		{
			Boolean? value = true;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableBoolean());
		}

		[Test]
		public void CheckNullableSingleNull()
		{
			Single? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableSingle());
		}

		[Test]
		public void CheckNullableSingle()
		{
			Single? value = 33;
			Writer.WriteNullable(value);
			CheckValue(5, 5, value, Reader.ReadNullableSingle());
		}

		[Test]
		public void CheckNullableDoubleNull()
		{
			Double? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableDouble());
		}

		[Test]
		public void CheckNullableDouble()
		{
			Double? value = 33;
			Writer.WriteNullable(value);
			CheckValue(9, 9, value, Reader.ReadNullableDouble());
		}

		[Test]
		public void CheckNullableSByteNull()
		{
			SByte? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableSByte());
		}

		[Test]
		public void CheckNullableSByte()
		{
			SByte? value = 33;
			Writer.WriteNullable(value);
			CheckValue(2, 2, value, Reader.ReadNullableSByte());
		}

		[Test]
		public void CheckNullableUInt16Null()
		{
			UInt16? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableUInt16());
		}

		[Test]
		public void CheckNullableUInt16()
		{
			UInt16? value = 33;
			Writer.WriteNullable(value);
			CheckValue(3, 3, value, Reader.ReadNullableUInt16());
		}

		[Test]
		public void CheckNullableInt32Null()
		{
			Int32? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableInt32());
		}

		[Test]
		public void CheckNullableInt32()
		{
			Int32? value = 33;
			Writer.WriteNullable(value);
			CheckValue(2, 2, value, Reader.ReadNullableInt32());
		}

		[Test]
		public void CheckNullableDecimalNull()
		{
			Decimal? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableDecimal());
		}

		[Test]
		public void CheckNullableDecimal()
		{
			Decimal? value = 33;
			Writer.WriteNullable(value);
			CheckValue(3, 3, value, Reader.ReadNullableDecimal());
		}

		[Test]
		public void CheckNullableDateTimeNull()
		{
			DateTime? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableDateTime());
		}

		[Test]
		public void CheckNullableDateTime()
		{
			DateTime? value = new DateTime(2006, 9, 17);
			Writer.WriteNullable(value);
			CheckValue(4, 4, value, Reader.ReadNullableDateTime());
		}

		[Test]
		public void CheckNullableTimeSpanNull()
		{
			TimeSpan? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableTimeSpan());
		}

		[Test]
		public void CheckNullableTimeSpan()
		{
			TimeSpan? value = TimeSpan.FromDays(1);
			Writer.WriteNullable(value);
			CheckValue(4, 4, value, Reader.ReadNullableTimeSpan());
		}

		[Test]
		public void CheckNullableUInt32Null()
		{
			UInt32? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableUInt32());
		}

		[Test]
		public void CheckNullableUInt32()
		{
			UInt32? value = 33;
			Writer.WriteNullable(value);
			CheckValue(2, 2, value, Reader.ReadNullableUInt32());
		}

		[Test]
		public void CheckNullableUInt64Null()
		{
			UInt64? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableUInt64());
		}

		[Test]
		public void CheckNullableUInt64()
		{
			UInt64? value = 33;
			Writer.WriteNullable(value);
			CheckValue(2, 2, value, Reader.ReadNullableUInt64());
		}

		[Test]
		public void CheckNullableInt64Null()
		{
			Int64? value = null;
			Writer.WriteNullable(value);
			CheckValue(1, 1, value, Reader.ReadNullableInt64());
		}

		[Test]
		public void CheckNullableInt64()
		{
			Int64? value = 33;
			Writer.WriteNullable(value);
			CheckValue(2, 2, value, Reader.ReadNullableInt64());
		}
		
		[Test]
		public void CheckNullableNonPrimitiveStructNull()
		{
			CustomStruct? value = null;
			Writer.WriteNullable(value);
			CustomStruct? result = (CustomStruct?) Reader.ReadNullable();
			CheckValue(1, 1, value, result);
			Assert.IsFalse(result.HasValue);
		}

		[Test]
		public void CheckNullableNonPrimitiveStruct()
		{
			CustomStruct structValue = new CustomStruct();
			structValue.IntValue = 21;
			structValue.StringValue = "Simon";
			CustomStruct? value = structValue;
			Writer.WriteNullable(value);
			CustomStruct? result = (CustomStruct?) Reader.ReadNullable();
			CheckValue(-1, -1, value, result);
			Assert.IsTrue(result.HasValue == value.HasValue);
			Assert.IsTrue(result.Value.IntValue == value.Value.IntValue);
			Assert.IsTrue(result.Value.StringValue == value.Value.StringValue);
		}
		
		[Test]
		public void CheckNullableArrayNull()
		{
			int?[] value = new int?[10];
			Writer.WriteTypedArray(value);
			int?[] result = (int?[]) Reader.ReadTypedArray();
			Assert.AreEqual(value.Length, result.Length);
			for(int i = 0; i < value.Length; i++)
			{
				Assert.IsNull(result[i]);
			}
		}

		[Test]
		public void CheckNullableArray()
		{
			int?[] value = new int?[10];
			for (int i = 0; i < value.Length; i++) value[i] = i;
			Writer.WriteTypedArray(value);
			int?[] result = (int?[]) Reader.ReadTypedArray();
			Assert.AreEqual(value.Length, result.Length);
			for(int i = 0; i < value.Length; i++)
			{
				Assert.AreEqual(value[i], result[i]);
			}
		}

		[Test]
		public void CheckDateTimeNet20Unspecified()
		{
			DateTime value = new DateTime(2006, 11, 05, 15, 08, 20, DateTimeKind.Unspecified);
			value = value.AddTicks(2);
			Writer.Write(value);
			DateTime result = Reader.ReadDateTime();
			CheckValue(8, 9, value, result);
			Assert.AreEqual(value.Kind, result.Kind);
		}

		[Test]
		public void CheckDateTimeNet20Utc()
		{
			DateTime value = new DateTime(2006, 11, 05, 15, 08, 20, DateTimeKind.Utc);
			value = value.AddTicks(2);
			Writer.Write(value);
			DateTime result = Reader.ReadDateTime();
			CheckValue(8, 9, value, result);
			Assert.AreEqual(value.Kind, result.Kind);
		}

		[Test]
		public void CheckDateTimeNet20Local()
		{
			DateTime value = new DateTime(2006, 11, 05, 15, 08, 20, DateTimeKind.Local);
			value = value.AddTicks(2);
			Writer.Write(value);
			DateTime result = Reader.ReadDateTime();
			CheckValue(8, 9, value, result);
			Assert.AreEqual(value.Kind, result.Kind);
		}

		[Test]
		public void CheckOptimizedDateTimeUtcWithDateOnly()
		{
			DateTime value = DateTime.SpecifyKind(new DateTime(2006, 9, 17), DateTimeKind.Utc);
			Writer.WriteOptimized(value);
			CheckValue(5, 6, value, Reader.ReadOptimizedDateTime());
		}

		[Test]
		public void CheckOptimizedDateTimeLocalWithDateOnly()
		{
			DateTime value = DateTime.SpecifyKind(new DateTime(2006, 9, 17), DateTimeKind.Local);
			Writer.WriteOptimized(value);
			CheckValue(5, 6, value, Reader.ReadOptimizedDateTime());
		}

		[Test]
		public void CheckOptimizedDateTimeUtcWithDateHoursAndMinutes()
		{
			DateTime value = new DateTime(2006, 9, 17, 12, 20, 0, DateTimeKind.Utc);
			Writer.WriteOptimized(value);
			DateTime result = Reader.ReadOptimizedDateTime();
			CheckValue(5, 6, value, result);
			Assert.AreEqual(value.Kind, result.Kind);
		}

		[Test]
		public void CheckOptimizedDateTimeLocalWithDateHoursAndMinutes()
		{
			DateTime value = new DateTime(2006, 9, 17, 12, 20, 0, DateTimeKind.Local);
			Writer.WriteOptimized(value);
			DateTime result = Reader.ReadOptimizedDateTime();
			CheckValue(5, 6, value, result);
			Assert.AreEqual(value.Kind, result.Kind);
		}

		[Test]
		public void CheckOptimizedDateTimeUtcWithDateHoursAndMinutesAndSeconds()
		{
			DateTime value = new DateTime(2006, 9, 17, 12, 20, 22, DateTimeKind.Utc);
			Writer.WriteOptimized(value);
			DateTime result = Reader.ReadOptimizedDateTime();
			CheckValue(6, 7, value, result);
			Assert.AreEqual(value.Kind, result.Kind);
		}

		[Test]
		public void CheckOptimizedDateTimeLocalWithDateHoursAndMinutesAndSeconds()
		{
			DateTime value = new DateTime(2006, 9, 17, 12, 20, 22, DateTimeKind.Local);
			Writer.WriteOptimized(value);
			DateTime result = Reader.ReadOptimizedDateTime();
			CheckValue(6, 7, value, result);
			Assert.AreEqual(value.Kind, result.Kind);
		}

#endif

		[Test]
		public void CheckCustomStructTypedArrayNull()
		{
			CustomStruct[] value = null;
			Writer.WriteTypedArray(value);
			CustomStruct[] result = (CustomStruct[]) Reader.ReadTypedArray();

			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test] //!!!
		public void CheckCustomStructTypedArrayEmpty()
		{
			CustomStruct[] value = new CustomStruct[0];
			Writer.WriteTypedArray(value);
			CustomStruct[] result = (CustomStruct[]) Reader.ReadTypedArray();

			CheckValue(3, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomStructTypedArrayOne()
		{
			CustomStruct[] value = new CustomStruct[] { new CustomStruct() };
			value[0].IntValue = 3;
			value[0].StringValue = "a";
			Writer.WriteTypedArray(value);
			CustomStruct[] result = (CustomStruct[]) Reader.ReadTypedArray();

			CheckValue(201, 201, value, result, new ListComparer());
		}

		[Test]
		public void CheckCustomStructTypedArrayMulti()
		{
			CustomStruct[] value = new CustomStruct[] { new CustomStruct(), new CustomStruct() };
			value[0].IntValue = 3;
			value[0].StringValue = "a";
			value[1].IntValue = 4;
			value[1].StringValue = "b";
			Writer.WriteTypedArray(value);
			CustomStruct[] result = (CustomStruct[]) Reader.ReadTypedArray();

			CheckValue(398, 398, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomStructTypedArrayNull()
		{
			IntelligentCustomStruct[] value = null;
			Writer.WriteTypedArray(value);
			IntelligentCustomStruct[] result = (IntelligentCustomStruct[]) Reader.ReadTypedArray();

			CheckValue(1, 1, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomStructTypedArrayEmpty()
		{
			IntelligentCustomStruct[] value = new IntelligentCustomStruct[0];
			Writer.WriteTypedArray(value);
			IntelligentCustomStruct[] result = (IntelligentCustomStruct[]) Reader.ReadTypedArray();

			CheckValue(3, 3, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomStructTypedArrayOne()
		{
			IntelligentCustomStruct[] value = new IntelligentCustomStruct[] { new IntelligentCustomStruct() };
			value[0].IntValue = 3;
			value[0].StringValue = "a";
			Writer.WriteTypedArray(value);
			IntelligentCustomStruct[] result = (IntelligentCustomStruct[]) Reader.ReadTypedArray();

			CheckValue(7, 7, value, result, new ListComparer());
		}

		[Test]
		public void CheckIntelligentCustomStructTypedArrayMulti()
		{
			IntelligentCustomStruct[] value = new IntelligentCustomStruct[] { new IntelligentCustomStruct(), new IntelligentCustomStruct() };
			value[0].IntValue = 3;
			value[0].StringValue = "a";
			value[1].IntValue = 4;
			value[1].StringValue = "b";
			Writer.WriteTypedArray(value);
			IntelligentCustomStruct[] result = (IntelligentCustomStruct[]) Reader.ReadTypedArray();

			CheckValue(10, 10, value, result, new ListComparer());
		}
		
		private CustomClass createCustomClass()
		{
			CustomClass result = new CustomClass();
			result.IntValue = 10;
			result.StringValue = "abc";
			return result;
		}
		
		private SemiIntelligentCustomClass createSemiIntelligentCustomClass()
		{
			SemiIntelligentCustomClass result = new SemiIntelligentCustomClass();
			result.IntValue = 10;
			result.StringValue = "abc";
			return result;
		}
		
		private IntelligentCustomClass createIntelligentCustomClass()
		{
			IntelligentCustomClass result = new IntelligentCustomClass();
			result.IntValue = 10;
			result.StringValue = "abc";
			return result;
		}
		
		[Test]
		public void CheckCustomClass()
		{
			CustomClass value = createCustomClass();
			Writer.WriteObject(value);
			CheckValue(198, -1, value, Reader.ReadObject(), Comparer.DefaultInvariant);
		}
		
		[Test]
		public void CheckCustomClassNull()
		{
			CustomClass value = null;
			Writer.WriteObject(value);
			CheckValue(1, -1, value, Reader.ReadObject());
		}
		
		[Test]
		public void CheckSemiIntelligentCustomClass()
		{
			SemiIntelligentCustomClass value = createSemiIntelligentCustomClass();
			Writer.WriteObject(value);
			CheckValue(213, -1, value, Reader.ReadObject(), Comparer.DefaultInvariant);
		}
		
		[Test]
		public void CheckSemiIntelligentCustomClassNull()
		{
			SemiIntelligentCustomClass value = null;
			Writer.WriteObject(value);
			CheckValue(1, -1, value, Reader.ReadObject());
		}
		
		[Test]
		public void CheckIntelligentCustomClass()
		{
			IntelligentCustomClass value = createIntelligentCustomClass();
			Writer.WriteObject(value);
			CheckValue(6, -1, value, Reader.ReadObject(), Comparer.DefaultInvariant);
		}
		
		[Test]
		public void CheckIntelligentCustomClassNull()
		{
			IntelligentCustomClass value = null;
			Writer.WriteObject(value);
			CheckValue(1, -1, value, Reader.ReadObject());
		}
		
	}

#if NET20	
	public class ListComparer<T>: IComparer {
		public int Compare(object x, object y)
		{
			List<T> list1 = (List<T>) x;
			List<T> list2 = (List<T>) y;
			
			int result = list1.Count.CompareTo(list2.Count);
			if (result == 0)
			{
				IComparer<T> comparer = Comparer<T>.Default;
				for (int i = 0; i < list1.Count && result == 0; i++)
				{
					result = comparer.Compare(list1[i], list2[i]);
				}
			}
			return result;
		}
	}

	public class DictionaryComparer<K, V>: IComparer
	{

		public int Compare(object x, object y)
	  {
	    Dictionary<K, V> dictionary1 = (Dictionary<K, V>) x;
	    Dictionary<K, V> dictionary2 = (Dictionary<K, V>) y;
	    int result = dictionary1.Count.CompareTo(dictionary2.Count);
	    if (result == 0)
	    {
				List<K> keys1 = new List<K>(dictionary1.Keys);
				List<K> keys2 = new List<K>(dictionary2.Keys);
				result = new ListComparer<K>().Compare(keys1, keys2);

				if (result == 0)
				{
					List<V> values1 = new List<V>(dictionary1.Values);
					List<V> values2 = new List<V>(dictionary2.Values);
					result = new ListComparer<V>().Compare(values1, values2);
				}
	    }
	    return result;
	  }
	}
#endif
	
	public class BitArrayComparer: IComparer
	{
		public int Compare(object x, object y)
		{
			BitArray bitArray1 = (BitArray) x;
			BitArray bitArray2 = (BitArray) y;
			int result = bitArray1.Length.CompareTo(bitArray2.Length);
			if(result == 0)
			{
				for(int i = 0; i < bitArray1.Length; i++)
				{
					result = bitArray1[i].CompareTo(bitArray2[i]);
					if(result != 0) break;
				}
			}
			return result;
		}
	}

	public class ListComparer: IComparer
	{
		public int Compare(object x, object y)
		{
			if(x == null) return y == null ? 0 : -1;

			IList list1 = (IList) x;
			IList list2 = (IList) y;
			if (x.GetType() != y.GetType()) throw new InvalidOperationException(string.Format("Not the same types: x={0}, y={1}", x.GetType(), y.GetType()));
			int result = list1.Count.CompareTo(list2.Count);
			if(result == 0)
			{
				for(int i = 0; i < list1.Count; i++)
				{
					if(list1[i] == null) return list2[i] == null ? 0 : -1;

					result = list1[i].GetType().ToString().CompareTo(list2[i].GetType().ToString());
					if(result != 0) break;

					IComparable left = list1[i] as IComparable;
					if(left != null)
						result = left.CompareTo(list2[i]);
					else
					{
						result = list2[i] == list2[i] ? 0 : -1;
					}
					if(result != 0) break;
				}
			}
			return result;
		}
	}

	#region Sample Classes
	[Serializable]
	public class CustomClass: IComparable
	{
		public int IntValue = 55;
		public string StringValue = "fred";
		
		public CustomClass() {}
		public CustomClass(int intValue, string stringValue)
		{
			IntValue = intValue;
			StringValue = stringValue;
		}

		#region IComparable Members
		public virtual int CompareTo(object obj)
		{
			CustomClass compare = obj as CustomClass;
			if(compare == null) throw new ArgumentException();
			int result = IntValue.CompareTo(compare.IntValue);
			if(result == 0) result = StringValue.CompareTo(compare.StringValue);
			return result;
		}
		#endregion
	}

	[Serializable]
	public class InheritedCustomClass: CustomClass, IComparable
	{
		public float FloatValue = 1023.232f;

		#region IComparable Members
		public override int CompareTo(object obj)
		{
			InheritedCustomClass compare = obj as InheritedCustomClass;
			if(compare == null) throw new ArgumentException();
			int result = IntValue.CompareTo(compare.IntValue);
			if(result == 0) result = StringValue.CompareTo(compare.StringValue);
			if(result == 0) result = FloatValue.CompareTo(compare.FloatValue);
			return result;
		}
		#endregion
	}

	[Serializable]
	public class SemiIntelligentCustomClass : IComparable, IOwnedDataSerializable
	{
		public int IntValue = 55;
		public string StringValue = "fred";

		#region IComparable Members
		public virtual int CompareTo(object obj)
		{
			SemiIntelligentCustomClass compare = obj as SemiIntelligentCustomClass;
			if (compare == null) throw new ArgumentException();
			int result = IntValue.CompareTo(compare.IntValue);
			if (result == 0) result = StringValue.CompareTo(compare.StringValue);
			return result;
		}
		#endregion

		#region IOwnedDataSerializable Members
		public virtual void SerializeOwnedData(SerializationWriter writer, object context)
		{
			writer.WriteOptimized(IntValue);
			writer.WriteOptimized(StringValue);
		}

		public virtual void DeserializeOwnedData(SerializationReader reader, object context)
		{
			IntValue = reader.ReadOptimizedInt32();
			StringValue = reader.ReadOptimizedString();
		}
		#endregion
	}

	[Serializable]
	public class InheritedSemiIntelligentCustomClass : SemiIntelligentCustomClass, IComparable
	{
		public float FloatValue = 1023.232f;

		#region IComparable Members
		public override int CompareTo(object obj)
		{
			InheritedSemiIntelligentCustomClass compare = obj as InheritedSemiIntelligentCustomClass;
			if (compare == null) throw new ArgumentException();
			int result = base.CompareTo(obj);
			if (result == 0) result = FloatValue.CompareTo(compare.FloatValue);
			return result;
		}
		#endregion

		#region IOwnedDataSerializable Members
		public override void SerializeOwnedData(SerializationWriter writer, object context)
		{
			base.SerializeOwnedData(writer, context);
			writer.Write(FloatValue);
		}

		public override void DeserializeOwnedData(SerializationReader reader, object context)
		{
			base.DeserializeOwnedData(reader, context);
			FloatValue = reader.ReadSingle();
		}
		#endregion

	}
	
	
	[Serializable]
	public class IntelligentCustomClass: IComparable, IOwnedDataSerializableAndRecreatable
	{
		public int IntValue = 55;
		public string StringValue = "fred";
		
		public IntelligentCustomClass() {}
		public IntelligentCustomClass(int intValue, string stringValue)
		{
			IntValue = intValue;
			StringValue = stringValue;
		}

		#region IComparable Members
		public virtual int CompareTo(object obj)
		{
			IntelligentCustomClass compare = obj as IntelligentCustomClass;
			if (compare == null) throw new ArgumentException();
			int result = IntValue.CompareTo(compare.IntValue);
			if (result == 0) result = StringValue.CompareTo(compare.StringValue);
			return result;
		}
		#endregion

		#region IOwnedDataSerializable Members
		public virtual void SerializeOwnedData(SerializationWriter writer, object context)
		{
			writer.WriteOptimized(IntValue);
			writer.WriteOptimized(StringValue);
		}

		public virtual void DeserializeOwnedData(SerializationReader reader, object context)
		{
			IntValue = reader.ReadOptimizedInt32();
			StringValue = reader.ReadOptimizedString();
		}
		#endregion
	}

	[Serializable]
	public class InheritedIntelligentCustomClass : IntelligentCustomClass, IComparable
	{
		public float FloatValue = 1023.232f;

		#region IComparable Members
		public override int CompareTo(object obj)
		{
			InheritedIntelligentCustomClass compare = obj as InheritedIntelligentCustomClass;
			if (compare == null) throw new ArgumentException();
			int result = base.CompareTo(obj);
			if (result == 0) result = FloatValue.CompareTo(compare.FloatValue);
			return result;
		}
		#endregion

		#region IOwnedDataSerializable Members
		public override void SerializeOwnedData(SerializationWriter writer, object context)
		{
			base.SerializeOwnedData(writer, context);
			writer.Write(FloatValue);
		}

		public override void DeserializeOwnedData(SerializationReader reader, object context)
		{
			base.DeserializeOwnedData(reader, context);
			FloatValue = reader.ReadSingle();
		}
		#endregion

	}
	
	[Serializable]
	public struct CustomStruct: IComparable
	{
		public int IntValue;
		public string StringValue;

		public CustomStruct(int intValue, string stringValue)
		{
			this.IntValue = intValue;
			this.StringValue = stringValue;
		}
		
		#region IComparable Members
		int IComparable.CompareTo(object obj)
		{
			CustomStruct compare = (CustomStruct)obj;
			int result = IntValue.CompareTo(compare.IntValue);
			if (result == 0) result = StringValue.CompareTo(compare.StringValue);
			return result;
		}
		#endregion
	}

	[Serializable]
	public struct IntelligentCustomStruct: IOwnedDataSerializable, IComparable
	{
		public int IntValue;
		public string StringValue;
		
		public IntelligentCustomStruct(int intValue, string stringValue)
		{
			this.IntValue = intValue;
			this.StringValue = stringValue;
		}

		#region IOwnedDataSerializable Members
		void IOwnedDataSerializable.SerializeOwnedData(SerializationWriter writer, object context)
		{
			writer.WriteOptimized(IntValue);
			writer.WriteOptimized(StringValue);
		}

		void IOwnedDataSerializable.DeserializeOwnedData(SerializationReader reader, object context)
		{
			IntValue = reader.ReadOptimizedInt32();
			StringValue = reader.ReadOptimizedString();
		}
		#endregion

		#region IComparable Members
		int IComparable.CompareTo(object obj)
		{
			IntelligentCustomStruct compare = (IntelligentCustomStruct)obj;
			int result = IntValue.CompareTo(compare.IntValue);
			if (result == 0) result = StringValue.CompareTo(compare.StringValue);
			return result;
		}
		#endregion

	}

	[Serializable]
	public class SampleFactoryClass {}
	#endregion Sample Classes
}

#endif
