using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace Game.Utility
{ 
	public class JsonArray : JsonData, IEnumerable
	{
		private List<JsonData> mList = new List<JsonData>();
		public override JsonData this[int pIndex]
		{
			get
			{
				if (pIndex<0 || pIndex >= mList.Count) {
					return new JsonManipulator(this);
				}
				return mList[pIndex];
			}
			set
			{
				JsonData tData = (value == null)? new JsonValue(null) : value;
				if (pIndex<0 || pIndex >= mList.Count) {
					mList.Add(tData);
				} else {
					mList[pIndex] = tData;
				}
			}
		}
		
		public override JsonData this[string pKey]
		{
			get
			{ 
				return new JsonManipulator(this);
			}
			set
			{ 
				JsonData tData = (value == null)? new JsonValue(null) : value;
				mList.Add(tData); 
			}
		}
		
		public override JsonDataType GetValueType()
		{
			return JsonDataType.ArrayValue;
		}
		
		public override int Count
		{
			get 
			{ 
				return mList.Count; 
			}
		}
		
		public override void Add(string pKey, JsonData pItem)
		{
			mList.Add(pItem);
		}
		
		public override JsonData Remove(int pIndex)
		{
			if (pIndex < 0 || pIndex >= mList.Count) {
				return null;
			}
			JsonData tTmp = mList[pIndex];
			mList.RemoveAt(pIndex);
			return tTmp;
		}
		
		public override JsonData Remove(JsonData pData)
		{
			mList.Remove(pData);
			return pData;
		}
		
		public override IEnumerable<JsonData> Childs
		{
			get
			{
				foreach (JsonData tItem in mList) {
					yield return tItem;
				}
			}
		}
		
		public IEnumerator GetEnumerator()
		{
			foreach (JsonData tItem in mList) {
				yield return tItem;
			}
		}
		
		public override bool IsArray() { return true; }
		
		public override string ToString()
		{
			int tCount = mList.Count;
			if (tCount == 0) {
				return "[]";
			}
			
			StringBuilder tRet = new StringBuilder("[");
			tRet.Append(mList[0].ToString());
			for (int i = 1; i < tCount; i++) {
				tRet.Append(",");
				tRet.Append(mList[i].ToString());
			}
			tRet.Append("]");
			return tRet.ToString();
		}
	} // End of JsonArray
}
