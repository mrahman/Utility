using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace Game.Utility
{
	public class JsonMap : JsonData, IEnumerable
	{
		private Dictionary<string, JsonData> mDict = new Dictionary<string, JsonData>();
		
		public override JsonData this[string pKey]
		{
			get
			{
				if (mDict.ContainsKey(pKey)) {
					return mDict[pKey];
				} else {
					return new JsonManipulator(this, pKey);
				}
			}
			
			set
			{
				JsonData tData = (value == null)? new JsonValue(null) : value;
				if (mDict.ContainsKey(pKey)) {
					mDict[pKey] = tData;
				} else {
					mDict.Add(pKey, tData);
				}
			}
		}
		
		public override JsonData this[int pIndex]
		{
			get
			{
				if (pIndex < 0 || pIndex >= mDict.Count) {
					return null;
				} else {
					return mDict.ElementAt(pIndex).Value;
				}
			}
			set
			{
				if (pIndex < 0 || pIndex >= mDict.Count) {
					return;
				}
				string key = mDict.ElementAt(pIndex).Key;
				mDict[key] = value;
			}
		}
		
		public override JsonDataType GetValueType()
		{
			return JsonDataType.MapValue;
		}
		
		public override int Count
		{
			get 
			{ 
				return mDict.Count; 
			}
		}
 
		public override void Add(string pKey, JsonData pItem)
		{
			if (!string.IsNullOrEmpty(pKey)) {
				if (mDict.ContainsKey(pKey)) {
					mDict[pKey] = pItem;
				} else {
					mDict.Add(pKey, pItem);
				}
			} else {
				mDict.Add(Guid.NewGuid().ToString(), pItem);
			}
		}
 
		public override JsonData Remove(string pKey)
		{
			if (!mDict.ContainsKey(pKey)) {
				return null;
			}
			JsonData tTmp = mDict[pKey];
			mDict.Remove(pKey);
			return tTmp;		
		}
		
		public override JsonData Remove(int pIndex)
		{
			if (pIndex < 0 || pIndex >= mDict.Count) {
				return null;
			}
			KeyValuePair<string, JsonData> tItem = mDict.ElementAt(pIndex);
			mDict.Remove(tItem.Key);
			return tItem.Value;
		}
		
		public override JsonData Remove(JsonData pData)
		{
			try
			{
				KeyValuePair<string, JsonData> tItem = mDict.Where(k => k.Value == pData).FirstOrDefault();
				mDict.Remove(tItem.Key);
				return pData;
			}
			catch
			{
				return null;
			}
		}
 
		public override IEnumerable<JsonData> Childs
		{
			get
			{
				foreach(KeyValuePair<string, JsonData> tChild in mDict) {
					yield return tChild.Value;
				}
			}
		}
		
		public override IEnumerable<string> Keys 
		{ 
			get 
			{ 
				foreach(KeyValuePair<string, JsonData> tChild in mDict) {
					yield return tChild.Key;
				} 
			} 
		}
 
		public IEnumerator GetEnumerator()
		{
			foreach(KeyValuePair<string, JsonData> tItem in mDict) {
				yield return tItem;
			}
		}
		
		public override bool IsMap() { return true; }
		
		public override string ToString()
		{
			if (mDict.Count == 0) {
				return "{}";
			}
			StringBuilder tRet = new StringBuilder("{");
			foreach (KeyValuePair<string, JsonData> tItem in mDict) {
				if (tRet.Length > 1) {
					tRet.Append(",");
				}
				tRet.Append("\"").Append(Escape(tItem.Key)).Append("\":").Append(tItem.Value.ToString());
			}
			tRet.Append("}");
			return tRet.ToString();
		}
	} // End of JsonMap
}
