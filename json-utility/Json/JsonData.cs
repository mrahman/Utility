using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
 
namespace Game.Utility
{
	public class JsonData
	{
		// defining special values
		public const string VALUE_NULL = "null";
		public const string VALUE_FALSE = "false";
		public const string VALUE_TRUE = "true";
		
		#region common interface
		// Interfaces for accessing/manipulating data item
		public virtual JsonData this[int pIndex]   { get { return null; } set { } }
		public virtual JsonData this[string pKey]  { get { return null; } set { } }
		public virtual T GetValue<T>()			 { return default(T);}
		public virtual JsonDataType GetValueType()	  { return JsonDataType.Unknown;}
		public virtual void SetValue(int pValue)   {}
		public virtual void SetValue(long pValue)   {}
		public virtual void SetValue(float pValue) {}
		public virtual void SetValue(double pValue){}
		public virtual void SetValue(bool pValue)  {}
		public virtual void SetValue(string pValue){}
		
		// Interface for get the count of list of object/array
		public virtual int Count				   { get { return 0;	} }
 
		// Interfaces for adding data item to the array and object
		public virtual void Add(string pKey, JsonData pItem){ }
		public virtual void Add(JsonData pItem)
		{
			Add("", pItem);
		}
		
		// method to handle null/false/true
		private void addValue(string pKey, string pItem, bool pEnclosed)
		{
			JsonData tData = null;
			if (!pEnclosed) {
				if (pItem != null) {
					string tLower = pItem.ToLower();
					switch(tLower) {
					case VALUE_NULL:
						tData = new JsonValue(null);
						break;
					case VALUE_FALSE:
						tData = new JsonValue(false);
						break;
					case VALUE_TRUE:
						tData = new JsonValue(true);
						break;
					default:
						// So its must be a int/float/double
						int tIntVal;
						if (int.TryParse(pItem, out tIntVal)) {
							tData = new JsonValue(tIntVal);
							break;
						}
						
						float tFloatVal;
						if (float.TryParse(pItem, out tFloatVal)) {
							tData = new JsonValue(tFloatVal);
							break;
						}
						
						int tLongVal;
						if (int.TryParse(pItem, out tLongVal)) {
							tData = new JsonValue(tLongVal);
							break;
						}
						
						double tDoubleVal;
						if (double.TryParse(pItem, out tDoubleVal)) {
							tData = new JsonValue(tDoubleVal);
							break;
						}
						tData = new JsonValue(pItem);
						break;
					}
				} else {
					tData = new JsonValue(pItem);
				}
			} else {
				tData = new JsonValue(pItem);
			}
			Add(pKey, tData);
		}
		
		private void addValue(string pItem, bool pEnclosed)
		{
			addValue("", pItem, pEnclosed);
		}
		
		// Make it protected so that from outside it cannot be accessed
		protected JsonData()
		{
		}
		
		// Create Instances
		public static JsonData CreateMap()
		{
			return new JsonMap();
		}
		
		public static JsonData CreateArray()
		{
			return new JsonArray();
		}
		
		// Interfaces for removing the data
		public virtual JsonData Remove(string pKey) { return null; }
		public virtual JsonData Remove(int pIndex) { return null; }
		public virtual JsonData Remove(JsonData pData) { return pData; }
 
		// Interface for Getting the Enumerator of values for manipulation
		public virtual IEnumerable<JsonData> Childs { get { yield break;} }
		public virtual IEnumerable<string> Keys { get { yield break; } }
		
		// Type checking Interfaces
		public virtual bool IsArray() { return false; }
		public virtual bool IsMap() { return false; }
		public virtual bool IsInt() { return false; }
		public virtual bool IsLong() { return false; }
		public virtual bool IsFloat() { return false; }
		public virtual bool IsDouble() { return false; }
		public virtual bool IsBoolean() { return false; }
		public virtual bool IsString() { return false; }
 
		public override string ToString()
		{
			return "JsonData";
		}
		#endregion common interface
 
		#region typecasting method
		public virtual int GetInt()
		{
			return GetValue<int>();
		}
		
		public virtual void SetInt(int pValue)
		{
			SetValue(pValue);
		}
		
		public virtual long GetLong()
		{
			return GetValue<long>();
		}
		
		public virtual void SetLong(long pValue)
		{
			SetValue(pValue);
		}
		
		public virtual float GetFloat()
		{
			return GetValue<float>();
		}
		
		public virtual void SetFloat(float pValue)
		{
			SetValue(pValue);
		}
		
		public virtual double GetDouble()
		{
			return GetValue<double>();
		}
		
		public virtual void SetDouble(double pValue)
		{
			SetValue(pValue);
		}
		
		public virtual bool GetBool()
		{
			return GetValue<bool>();
		}
		
		public virtual void SetBool(bool pValue)
		{
			SetValue(pValue);
		}
		
		public virtual string GetString()
		{
			return GetValue<string>();
		}
		
		public virtual void SetString(string pValue)
		{
			SetValue(pValue);
		}
		
		public virtual JsonArray GetArray()
		{
			return this as JsonArray;
		}
		
		public virtual JsonMap GetMap()
		{
			return this as JsonMap;
		}
		#endregion typecasting method
 
		#region operators
		public static implicit operator JsonData(string s)
		{
			return new JsonValue(s);
		}
		
		public static implicit operator JsonData(int i)
		{
			return new JsonValue(i);
		}
		
		public static implicit operator JsonData(float f)
		{
			return new JsonValue(f);
		}
		
		public static implicit operator JsonData(double d)
		{
			return new JsonValue(d);
		}
		
		public static implicit operator JsonData(long l)
		{
			return new JsonValue(l);
		}
		
		public static explicit operator string(JsonData pData)
		{
			if (pData == null) {
				return null;
			}
			if (!(pData is JsonValue)) {
				throw new InvalidCastException("Cannot cast to string");
			}
			return pData.GetValue<string>();
		}
		
		public static explicit operator int(JsonData pData)
		{
			if (pData == null) {
				throw new InvalidCastException("Primitive value cannot be null");
			}
			if (!(pData is JsonValue)) {
				throw new InvalidCastException("Cannot cast to integer");
			}
			return pData.GetValue<int>();
		}
		
		public static explicit operator long(JsonData pData)
		{
			if (pData == null) {
				throw new InvalidCastException("Primitive value cannot be null");
			}
			if (!(pData is JsonValue)) {
				throw new InvalidCastException("Cannot cast to long");
			}
			return pData.GetValue<long>();
		}
		
		public static explicit operator float(JsonData pData)
		{
			if (pData == null) {
				throw new InvalidCastException("Primitive value cannot be null");
			}
			if (!(pData is JsonValue)) {
				throw new InvalidCastException("Cannot cast to float");
			}
			return pData.GetValue<float>();
		}
		
		public static explicit operator double(JsonData pData)
		{
			if (pData == null) {
				throw new InvalidCastException("Primitive value cannot be null");
			}
			if (!(pData is JsonValue)) {
				throw new InvalidCastException("Cannot cast to double");
			}
			return pData.GetValue<double>();
		}
		
		public static explicit operator bool(JsonData pData)
		{
			if (pData == null) {
				throw new InvalidCastException("Primitive value cannot be null");
			}
			if (!(pData is JsonValue)) {
				throw new InvalidCastException("Cannot cast to boolean");
			}
			return pData.GetValue<bool>();
		}
		
		public static bool operator ==(JsonData a, object b)
		{
			if (b == null && a is JsonManipulator) {
				return true;
			}
			return System.Object.ReferenceEquals(a,b);
		}
 
		public static bool operator !=(JsonData a, object b)
		{
			return !(a == b);
		}
		
		public override bool Equals (object obj)
		{
			return System.Object.ReferenceEquals(this, obj);
		}
		
		public override int GetHashCode ()
		{
			return base.GetHashCode();
		}
		#endregion operators
 
		internal static string Escape(string pText)
		{
			string tRet = "";
			foreach(char tChar in pText)
			{
				switch(tChar)
				{
					case '\\' : 
						tRet += "\\\\"; 
						break;
					case '\"' : 
						tRet += "\\\""; 
						break;
					case '\n' : 
						tRet += "\\n" ; 
						break;
					case '\r' : 
						tRet += "\\r" ; 
						break;
					case '\t' : 
						tRet += "\\t" ; 
						break;
					case '\b' : 
						tRet += "\\b" ; 
						break;
					case '\f' : 
						tRet += "\\f" ; 
						break;
					default   : 
						tRet += tChar; 
						break;
				}
			}
			return tRet;
		}
 
		public static JsonData Parse(string pJsonText)
		{
			Stack<JsonData> tStack = new Stack<JsonData>();
			JsonData tData = null;
			int i = 0;
			string tToken = "";
			string tTokenName = "";
			bool tQuoteMode = false;
			bool tValueEnclosed = false; // Enclosed with Quote or Not
			while (i < pJsonText.Length)
			{
				switch (pJsonText[i])
				{
					case '{':
						if (tQuoteMode) {
							tToken += pJsonText[i];
							break;
						}
						tStack.Push(new JsonMap());
						if (tData != null) {
							tTokenName = tTokenName.Trim();
							if (tData is JsonArray) {
								tData.Add(tStack.Peek());
							} else if (tTokenName != "") {
								tData.Add(tTokenName, tStack.Peek());
							}
						}
						tTokenName = "";
						tToken = "";
						tValueEnclosed = false;
						tData = tStack.Peek();
					break;
 
					case '[':
						if (tQuoteMode) {
							tToken += pJsonText[i];
							break;
						}
 
						tStack.Push(new JsonArray());
						if (tData != null) {
							tTokenName = tTokenName.Trim();
							if (tData is JsonArray) {
								tData.Add(tStack.Peek());
							} else if (tTokenName != "") {
								tData.Add(tTokenName, tStack.Peek());
							}
						}
						tTokenName = "";
						tToken = "";
						tValueEnclosed = false;
						tData = tStack.Peek();
					break;
 
					case '}':
					case ']':
						if (tQuoteMode) {
							tToken += pJsonText[i];
							break;
						}
						if (tStack.Count == 0) {
							throw new Exception("JSON Parse: Too many closing brackets");
						}
 
						tStack.Pop();
						if (tToken != "") {
							tTokenName = tTokenName.Trim();
							if (tData is JsonArray) {
								tData.addValue(tToken, tValueEnclosed);
							} else if (tTokenName != "") {
								tData.addValue(tTokenName, tToken, tValueEnclosed);
							}
						}
						tTokenName = "";
						tToken = "";
						tValueEnclosed = false;
						if (tStack.Count > 0) {
							tData = tStack.Peek();
						}
					break;
 
					case ':':
						if (tQuoteMode) {
							tToken += pJsonText[i];
							break;
						}
						tTokenName = tToken;
						tToken = "";
						tValueEnclosed = false;
					break;
 
					case '"':
						tQuoteMode ^= true;
						if (tQuoteMode) {
							tValueEnclosed = true;
						}
					break;
 
					case ',':
						if (tQuoteMode) {
							tToken += pJsonText[i];
							break;
						}
						if (tToken != "") {
							if (tData is JsonArray) {
								tData.addValue(tToken, tValueEnclosed);
							} else if (tTokenName != "") {
								tData.addValue(tTokenName, tToken, tValueEnclosed);
							}
						}
						tTokenName = "";
						tToken = "";
						tValueEnclosed = false;
					break;
 
					case '\r':
					case '\n':
					break;
 
					case ' ':
					case '\t':
						if (tQuoteMode) {
							tToken += pJsonText[i];
						}
					break;
 
					case '\\':
						i++;
						if (tQuoteMode) {
							char tChar = pJsonText[i];
							switch (tChar)
							{
								case 't' : 
									tToken += '\t'; 
									break;
								case 'r' : 
									tToken += '\r'; 
									break;
								case 'n' : 
									tToken += '\n'; 
									break;
								case 'b' : 
									tToken += '\b'; 
									break;
								case 'f' : 
									tToken += '\f'; 
									break;
								case 'u':
								{
									string tStr = pJsonText.Substring(i+1, 4);
									tToken += (char)int.Parse(tStr, System.Globalization.NumberStyles.AllowHexSpecifier);
									i += 4;
									break;
								}
								default  : tToken += tChar; break;
							}
						}
					break;
 
					default:
						tToken += pJsonText[i];
					break;
				}
				i++;
			}
			if (tQuoteMode)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			return tData;
		}
	} // End of JsonData
}
