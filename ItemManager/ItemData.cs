using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager
{
    public class Item
    {
        public string Name => "hoge";
    }

    public class Items : ObservableCollection<Item>
    {
        public Items()
        {
            Add(new Item());
            Add(new Item());
            Add(new Item());
            Add(new Item());
            Add(new Item());
            Add(new Item());
        }
    }
}
