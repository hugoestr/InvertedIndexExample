using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvertedIndexExample
{
    public interface IInvertedIndex
    {
        void Add(string key, int record_id);
        int IndexSize { get; }
        void Load(string path);
        
        void Remove(string key);
        void Remove(int record_id);
        void Save(string path);
        List<int> Search(string search_term);
    }

}
