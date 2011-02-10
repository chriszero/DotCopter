using System;
using System.Collections;

namespace FluentInterop.Coding {
  public static class MiscExtensionMethods {
    public static string ToBinary(this int value, int length) {
      var result=new char[length];
      for(var i=length-1; i>=0; --i) {
        result[i]=(char)((value&1)+'0');
        value=value>>1;
      }
      return new string(result);
    }

    public static string ToHex(this int i) {
      return ToHexHelper(i, 8);
    }
    public static string ToHex(this short s) {
      return ToHexHelper(s, 4);
    }
    public static string ToHex(this byte b) {
      return ToHexHelper(b, 2);
    }

    private static string ToHexHelper(int value, int numNibbles) {
      var buffer=new char[numNibbles];
      for(var i=numNibbles-1; i>=0; --i) {
        var nibble=value&0xf;
        value=value>>4;
        buffer[i]="0123456789ABCDEF"[nibble];
      }
      return new string(buffer);
    }

    public static string PaddedTo(this string s, int width) {
      if(s.Length<width) {
        s+=new string(' ', width-s.Length);
      }
      return s.Substring(0, width);
    }

    public static void CheckedAdd(this IDictionary ht, object key, object value) {
      if(ht.Contains(key)) {
        throw new Exception("key already in dictionary: "+key);
      }
      ht.Add(key, value);
    }

    public static ArrayList LookupOrCreateArrayList(this IDictionary ht, object key) {
      var al=(ArrayList)ht[key];
      if(al==null) {
        al=new ArrayList();
        ht.Add(key, al);
      }
      return al;
    }

    public static string MyConcat(this string prefix, object obj0, object obj1, object obj2,
      object obj3=null, object obj4=null, object obj5=null, object obj6=null) {
      return string.Concat(prefix, obj0, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public static string ReverseConcat(this IList items) {
      var count=items.Count;
      var reversed=new object[count];
      for(var i=0; i<count; ++i) {
        reversed[i]=items[count-i-1];
      }
      return string.Concat(reversed);
    }

    public static string ToCSharpCode(this short[] code) {
      var items=new ArrayList { "var compiledCode=unchecked(new []{\r\n  " };
      for(var i=0; i<code.Length; ++i) {
        var item=code[i];
        if(i>0 && ((i%8)==0)) {
          items.Add("\r\n  ");
        }
        var comma=i==code.Length-1 ? "" : ",";
        items.Add("(short)0x"+item.ToHex()+comma);
      }
      items.Add("});");
      return string.Concat((string[])items.ToArray(typeof(string)));
    }
  }
}
