using System;

namespace FluentInterop.Coding {
  public class MyShortBuilder {
    private short[] data=new short[4];
    private int length;

    public int Count { get { return length; } }

    public void Add(short value) {
      if(length==data.Length) {
        var newData=new short[data.Length*2];
        Array.Copy(data, newData, data.Length);
        data=newData;
      }
      data[length++]=value;
    }

    public short[] ToShortArray() {
      var result=new short[length];
      Array.Copy(data, result, length);
      return result;
    }
  }
}
