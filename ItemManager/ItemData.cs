using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLF3;
using System.ComponentModel;
using System.IO;

namespace ItemManager
{
    public class Element
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Element(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class ElementList : ObservableCollection<Element>
    {
        public string this[string key]
        {
            set
            {
                var e = this.First(x => x.Key == key);
                e.Value = value;

                OnPropertyChanged(new PropertyChangedEventArgs(e.Key));
            }

            get
            {
                return this.First(x => x.Key == key).Value;
            }
        }
    }


    public class Item
    {
        public string ListName
        {
            get { return $"{Data["id"]}: {Data["name"]}"; }
            set { return; }
        }

        public ElementList Data { get; set; }

        public Item(Dictionary<string, string> data)
        {
            Data = new ElementList();
            foreach (var d in data)
            {
                Data.Add(new Element(d.Key, d.Value));
            }
        }

        private Item(ElementList data)
        {
            Data = data;
        }

        public string GetKeys()
        {
            var sb = new StringBuilder();
            foreach (var i in Data)
            {
                sb.Append(i.Key);
                sb.Append(",");
            }
            sb.Length -= 1;
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var i in Data)
            {
                sb.Append(i.Value);
                sb.Append(",");
            }
            sb.Length -= 1;
            return sb.ToString();
        }

        public Item Clone()
        {
            return new Item(Data);
        }
    }

    public class Items : ObservableCollection<Item>
    {
        private static Dictionary<string, string> empty;

        public Items()
        {
            var items = CSVReader.ParseToDic(@"item.csv");
            empty = new Dictionary<string, string>();

            foreach (var i in items)
            {
                var item = new Item(i);
                Add(item);
            }

            foreach (var k in items[0].Keys)
            {
                empty.Add(k, "");
            }
        }

        public void InsertEmpty(int index)
        {
            var dic = new Dictionary<string, string>(empty);
            this.Insert(index, new Item(dic));
        }

        internal void Save()
        {
            if (Count == 0) return;

            using (var stream = new FileStream(@"item.csv", FileMode.Create))
            using (StreamWriter sr = new StreamWriter(stream, Encoding.GetEncoding("shift-jis")))
            {
                sr.WriteLine(this[0].GetKeys());
                foreach (var i in this)
                {
                    sr.WriteLine(i.ToString());
                }
                sr.Flush();
            }
        }


    }
}
