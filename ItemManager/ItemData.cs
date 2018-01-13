﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLF3;
using System.ComponentModel;

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

        public UserData Data { get; set; }

        public Item(UserData data)
        {
            Data = data;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Items : ObservableCollection<Item>
    {
        public Items()
        {
            var items = CSVReader.Parse(@"ItemData/item.csv");

            foreach (var i in items)
            {
                Add(new Item(i));
            }
        }
    }
}
