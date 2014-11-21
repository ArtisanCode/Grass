using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GrassTemplate.Internals
{
    /// <summary>
    /// A .NET 2.0 version of a HashSet
    /// </summary>
    public class HashSet<T>: ICollection<T>, ISerializable, IDeserializationCallback
    {
        Dictionary<T, bool> Data { get; set; }

        public HashSet()
        {
            Data = new Dictionary<T, bool>();
        }
        
        public object this[T i]
        {
            get { return Data[i]; }
            set { Data[i] = true; }
        }

        public void Add(T item)
        {
            Data[item] = true;
        }

        public T[] All()
        {
            List<T> result = new List<T>();

            foreach (var k in Data.Keys)
            {
                result.Add(k);
            }

            return result.ToArray();
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(T item)
        {
            return Data.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return Data.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return Data.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Data.Keys.GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Data.GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            Data.OnDeserialization(sender);
        }
    }
}
