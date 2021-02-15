using System;
using System.Collections.Generic;
using System.Linq;
using SputnikAsm.LGenerics;
using SputnikAsm.LString;

namespace SputnikAsm.LCollections
{
    public class ARefStringArray : AArrayManager<ARefString>
    {
        #region Constructor
        public ARefStringArray()
            : base()
        {
        }
        public ARefStringArray(params ARefString[] values)
            : base(values)
        {
        }
        #endregion
        #region NextElement
        public override ARefString NextElement(int index)
        {
            return new ARefString();
        }
        #endregion
        #region Assign
        public void Assign(params String[] values)
        {
            Assign(values.Select(c => new ARefString(c, -1)).ToArray());
        }
        #endregion
        #region Add
        public void Add(String value)
        {
            Add(new ARefString(value, -1));
        }
        public void Add(String value, int index)
        {
            Add(new ARefString(value, index));
        }
        #endregion
        #region AddRange
        public void AddRange(IEnumerable<String> array)
        {
            foreach (var item in array)
                Add(item);
        }
        #endregion
        #region Insert
        public void Insert(int index, String data)
        {
            Insert(index, new ARefString(data));
        }
        public void Insert(int index, String[] data)
        {
            Insert(index, data.Select(c => new ARefString(c)).ToArray());
        }
        public void Insert(int index, String[] data, int length)
        {
            Insert(index, data.Select(c => new ARefString(c)).ToArray(), length);
        }
        #endregion
        #region ToString
        public override String ToString()
        {
            return String.Join("\n", TakeAll().Select(c => c.Value));
        }
        public String ToString(String separator)
        {
            return String.Join(separator, TakeAll().Select(c => c.Value));
        }
        #endregion
    }
}
