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
    public class Item : INotifyPropertyChanged
    {
        public string ListName
        {
            get { return $"{Data["id"]}: {Data["name"]}"; }
            set { return; }
        }

        public string Name
        {
            get { return Data["name"]; }
            set { return;  }
        }

        public Dictionary<string, string> Data { get; set; }

        public Item(Dictionary<string, string> data)
        {
            Data = data;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
    }

    public class Items : ObservableCollection<Item>
    {
        public Items()
        {
            var items = CSVReader.ParseToDic(@"ItemData/item.csv");

            foreach (var i in items)
            {
                Add(new Item(i));
            }
        }

        internal void Save()
        {
            if (Count == 0) return;

            using (var stream = new FileStream(@"ItemData/item_out.csv", FileMode.Create))
            using (StreamWriter sr = new StreamWriter(stream))
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
