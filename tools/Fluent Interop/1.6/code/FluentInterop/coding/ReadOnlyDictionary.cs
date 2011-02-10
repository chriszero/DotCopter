using System;
using System.Collections;

namespace FluentInterop.Coding {
  public class ReadOnlyDictionary : IEnumerable {
    private readonly IDictionary dict;

    public ReadOnlyDictionary(IDictionary dict) {
      this.dict=dict;
    }

    public object this[object index] {
      get { return dict[index]; }
    }

    public bool ContainsKey(object key) {
      return dict.Contains(key);
    }

    public IEnumerable Keys {
      get { return dict.Keys; }
    }

    public IEnumerable Values {
      get { return dict.Values; }
    }

    public Hashtable ToHashtable() {
      var ht=new Hashtable();
      foreach(DictionaryEntry kvp in dict) {
        ht.Add(kvp.Key, kvp.Value);
      }
      return ht;
    }

    public IEnumerator GetEnumerator() {
      return dict.GetEnumerator();
    }
  }

  public static class ReadOnlyDictionary_Extensions {
    public static ReadOnlyDictionary AsReadOnlyDictionary(this IDictionary dict) {
      return new ReadOnlyDictionary(dict);
    }
  }
}
