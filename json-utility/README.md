json-utility
============

C#.Net Written Json Parser/Wrapper For Unity Game Development

/*
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
 *     // it's a Map type
 *  }
 *  if (tData.IsArray()) {
 *     // it's an Array type
 *  }
 *  if (tData.IsInt()) {
 *     // it's an Integer(int32) type
 *  }
 *  
 *  # checking whether particular key is exist or not
 *  string tJsonText = @"{"name":"jewel","age":30,"male":true}";
 *  JsonData tData = JsonUtility.Parse(tJsonText);
 *  if (tData.Keys.Contains("age")) {
 *     // Key found
 *  }
 *
 **/

