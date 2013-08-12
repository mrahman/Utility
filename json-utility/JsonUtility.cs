/*
 *  @author: Md. Mizanur Rahman
 *  @date: 2013-08-08
 *  
 *  This utility have facility to parse JsonText(string) to JsonData Object which can be
 *  easy to use with any in application data manipulation
 * 
 *  @example:
 *  # Parsing JsonText to JsonData
 *  string tJsonText = @"{"name":"jewel","age":30,"male":true}";
 *  JsonData tData = JsonUtility.Parse(tJsonText);
 *  
 *  # checking JsonData Type
 *  JsonData tData = JsonUtility.CreateMap(); // sonData tData = JsonData.CreateMap();
 * 	if (tData.IsMap()) {
 *	 // it's a Map type
 *  }
 *  if (tData.IsArray()) {
 *	 // it's an Array type
 *  }
 *  if (tData.IsInt()) {
 *	 // it's an Integer(int32) type
 *  }
 *  
 *  # checking whether particular key is exist or not
 *  string tJsonText = @"{"name":"jewel","age":30,"male":true}";
 *  JsonData tData = JsonUtility.Parse(tJsonText);
 *  if (tData.Keys.Contains("age")) {
 *	 // Key found
 *  }
 *
 **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
 
namespace Game.Utility
{
	public enum JsonDataType
	{
		Unknown			= 0,
		ArrayValue	   	= 1,
		MapValue	  	= 2,
		StringValue	  	= 3,
		IntValue		= 4, // int32
		LongValue		= 5, // int64
		DoubleValue		= 6,
		BoolValue		= 7,
		FloatValue		= 8,
	}

	internal class JsonManipulator : JsonData
	{
		private JsonData mData = null;
		private string mKey = null;
 
		public JsonManipulator(JsonData pData)
		{
			mData = pData;
			mKey  = null;
		}
		
		public JsonManipulator(JsonData pData, string pKey)
		{
			mData = pData;
			mKey = pKey;
		}
 
		private void set(JsonData pData)
		{
			if (mKey == null) {
				mData.Add(pData);
			} else {
				mData.Add(mKey, pData);
			}
			mData = null;
		}
 
		public override JsonData this[int pIndex]
		{
			get {
				return new JsonManipulator(this);
			}
			
			set {
				JsonArray ttTmp = new JsonArray();
				ttTmp.Add(value);
				set(ttTmp);
			}
		}
 
		public override JsonData this[string pKey]
		{
			get {
				return new JsonManipulator(this, pKey);
			}
			
			set {
				JsonMap ttTmp = new JsonMap();
				ttTmp.Add(pKey, value);
				set(ttTmp);
			}
		}
		
		public override void Add (JsonData pItem)
		{
			JsonArray ttTmp = new JsonArray();
			ttTmp.Add(pItem);
			set(ttTmp);
		}
		
		public override void Add (string pKey, JsonData pItem)
		{
			JsonMap ttTmp = new JsonMap();
			ttTmp.Add(pKey, pItem);
			set(ttTmp);
		}
		
		public static bool operator ==(JsonManipulator a, object b)
		{
			if (b == null) {
				return true;
			}
			
			return System.Object.ReferenceEquals(a,b);
		}
 
		public static bool operator !=(JsonManipulator a, object b)
		{
			return !(a == b);
		}
		
		public override bool Equals (object pObj)
		{
			if (pObj == null) {
				return true;
			}
			
			return System.Object.ReferenceEquals(this, pObj);
		}
		
		public override int GetHashCode ()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "";
		}
 
		public override int GetInt()
		{
			JsonValue tTmp = new JsonValue(0);
			set(tTmp);
			return 0;
		}
		
		public override void SetInt(int pValue)
		{
			JsonValue tTmp = new JsonValue(pValue);
			set(tTmp);
		}
		
		public override float GetFloat()
		{
			JsonValue tTmp = new JsonValue(0.0f);
			set(tTmp);
			return 0.0f;
		}
		
		public override void SetFloat(float pValue)
		{
			JsonValue tTmp = new JsonValue(pValue);
			set(tTmp);
		}
		public override double GetDouble()
		{
			JsonValue tTmp = new JsonValue(0.0);
			set(tTmp);
			return 0.0;
		}
		
		public override void SetDouble(Double pValue)
		{
			JsonValue tTmp = new JsonValue(pValue);
			set(tTmp);
		}
		
		public override bool GetBool()
		{
			JsonValue tTmp = new JsonValue(false);
			set(tTmp);
			return false;
		}
		
		public override void SetBool(bool pValue)
		{
			JsonValue tTmp = new JsonValue(pValue);
			set(tTmp);
		}
		
		public override JsonArray GetArray()
		{
			JsonArray tTmp = new JsonArray();
			set(tTmp);
			return tTmp;
		}
		
		public override JsonMap GetMap()
		{
			JsonMap tTmp = new JsonMap();
			set(tTmp);
			return tTmp;
		}
	} // End of JsonManipulator
 
	public static class JsonUtility
	{
		public static JsonData Parse(string pJsonText)
		{
			return JsonData.Parse(pJsonText);
		}
		
		public static JsonData CreateMap()
		{
			return JsonData.CreateMap();
		}
		
		public static JsonData CreateArray()
		{
			return JsonData.CreateArray();
		}
	}
}
