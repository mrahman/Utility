using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
 
namespace Game.Utility
{   
	public class JsonValue : JsonData
	{
		private object mValue;
		private JsonDataType mType;
		
		public override T GetValue<T>()
		{
			if (mValue == null) {
				return default(T);
			} else {
				Type tType = typeof(T);
				if (tType == typeof(bool)) {
					if (string.IsNullOrEmpty(mValue.ToString())) {
						return default(T);
					}
					return (T)mValue;
				} else {
					return (T)mValue;
				}
			}
		}
		
		public override JsonDataType GetValueType()
		{
			return mType;
		}
		
		public override void SetValue(string pValue)
		{
			mType = JsonDataType.StringValue;
			mValue = pValue;
		}
		
		public override void SetValue(float pValue)
		{
			mType = JsonDataType.FloatValue;
			mValue = pValue;
		}
		
		public override void SetValue(double pValue)
		{
			mType = JsonDataType.DoubleValue;
			mValue = pValue;
		}
		
		public override void SetValue(int pValue)
		{
			mType = JsonDataType.IntValue;
			mValue = pValue;
		}
		
		public override void SetValue(long pValue)
		{
			mType = JsonDataType.LongValue;
			mValue = pValue;
		}
		
		public override void SetValue(bool pValue)
		{
			mType = JsonDataType.BoolValue;
			mValue = pValue;
		}
		
		public JsonValue(string pValue)
		{
			SetValue(pValue);
		}
		
		public override int GetInt()
		{
			if (mType == JsonDataType.StringValue) {
				if (mValue == null) {
					return 0;
				}
				int tRet;
				if (int.TryParse(mValue.ToString(), out tRet)) {
					return tRet;
				} else {
					throw new NotSupportedException("The value is not an integer type");
				}
			}
			return GetValue<int>();
		}
		
		public override long GetLong()
		{
			if (mType == JsonDataType.StringValue) {
				if (mValue == null) {
					return 0;
				}
				long tRet;
				if (long.TryParse(mValue.ToString(), out tRet)) {
					return tRet;
				} else {
					throw new NotSupportedException("The value is not a long type");
				}
			}
			return GetValue<long>();
		}
		
		public override float GetFloat()
		{
			if (mType == JsonDataType.StringValue) {
				if (mValue == null) {
					return 0;
				}
				float tRet;
				if (float.TryParse(mValue.ToString(), out tRet)) {
					return tRet;
				} else {
					throw new NotSupportedException("The value is not a float type");
				}
			}
			return GetValue<float>();
		}
		
		public override double GetDouble()
		{
			if (mType == JsonDataType.StringValue) {
				if (mValue == null) {
					return 0;
				}
				double tRet;
				if (double.TryParse(mValue.ToString(), out tRet)) {
					return tRet;
				} else {
					throw new NotSupportedException("The value is not a double type");
				}
			}
			return GetValue<double>();
		}
		
		public override bool GetBool()
		{
			if (mType == JsonDataType.StringValue) {
				if (mValue == null) {
					return false;
				}
				bool tRet;
				if (bool.TryParse(mValue.ToString(), out tRet)) {
					return tRet;
				} else {
					throw new NotSupportedException("The value is not a bool type");
				}
			}
			return GetValue<bool>();
		}
		
		public JsonValue(float pValue)
		{
			SetValue(pValue);
		}
		
		public JsonValue(double pValue)
		{
			SetValue(pValue);
		}
		
		public JsonValue(bool pValue)
		{
			SetValue(pValue);
		}
		
		public JsonValue(int pValue)
		{
			SetValue(pValue);
		}
		
		public JsonValue(long pValue)
		{
			SetValue(pValue);
		}
		
		public override bool IsInt() { return mType == JsonDataType.IntValue; }
		public override bool IsLong() { return mType == JsonDataType.LongValue; }
		public override bool IsFloat() { return mType == JsonDataType.FloatValue; }
		public override bool IsDouble() { return mType == JsonDataType.DoubleValue; }
		public override bool IsBoolean() { return mType == JsonDataType.BoolValue; }
		public override bool IsString() { return mType == JsonDataType.StringValue; }
 
		public override string ToString()
		{
			switch(mType) {
			case JsonDataType.StringValue:
				if (mValue == null) {
					return JsonData.VALUE_NULL;
				}
				return "\"" + Escape(mValue.ToString()) + "\"";
			case JsonDataType.BoolValue:
				if (GetValue<bool>()) {
					return JsonData.VALUE_TRUE;
				}
				return JsonData.VALUE_FALSE;
			case JsonDataType.IntValue:
				return GetValue<int>().ToString();
			case JsonDataType.LongValue:
				return GetValue<long>().ToString();
			case JsonDataType.FloatValue:
				return GetValue<float>().ToString();
			case JsonDataType.DoubleValue:
				return GetValue<double>().ToString();
			default:
				return "";
			}
		}
	} // End of JsonValue
 }
