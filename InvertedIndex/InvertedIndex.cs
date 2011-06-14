using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace InvertedIndexExample
{
    public class InvertedIndex : IInvertedIndex
    {
        Dictionary<string, List<int>> index;

        public int IndexSize { get {return index.Count ;} }

        #region Public

        public InvertedIndex()
        {
            index = new Dictionary<string, List<int>>();
        }

        public InvertedIndex(string path)
        {
            Load(path);
        }

        public void Add(string key, int record_id)
        {
            if(index.ContainsKey(key))
            {
                index[key].Add(record_id);
            }
            else
            {
                index.Add(key, new List<int>() { record_id });
            }
        }

        public void Load(string path)
        {
            Dictionary<string, List<int>> result = null;
            var serializedIndex = path;

            if (File.Exists(serializedIndex))
            {
                Stream stream = File.Open(serializedIndex, FileMode.Open);
                var formatter = new BinaryFormatter();

                result = (Dictionary<string, List<int>>)formatter.Deserialize(stream);

                stream.Close();
            }

            if (result != null)
                index = result;

        }

        public void Remove(string key)
        {
            index.Remove(key);
        }

        public void Remove(int id)
        {
            foreach (string key in index.Keys)
            {
                if (index[key].Contains(id))
                {
                    index[key].Remove(id);
                }
            }
        }

        public void Save(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, index);

            stream.Close(); 
        }

        public List<int> Search(string query)
        {
            var terms = prepare(query);
            var result = executeSearch(terms);
            
            return result;
        }

        #endregion

        #region Private

        private List<string> prepare(string query)
        {
            var keywords = query.Split(' ').ToList();

            if (keywords.Count > 1)
                keywords.Add(query);

            return keywords;
        }

        private List<int> executeSearch(List<string> keywords)
        {
            var result = new List<int>();
            var subResult = new List<int>();

            foreach (string term in keywords)
            {
                var termResult = searchTerm(term);
                subResult.AddRange(termResult);
            }

            result = subResult.Distinct().ToList();

            return result;
        }

        private List<int> searchTerm(string term)
        {
            var result = new List<int>();

            if (index.ContainsKey(term))
            {
                result = index[term];
            }

            return result;
        }

        #endregion
    }
}
