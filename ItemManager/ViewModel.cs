using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemManager
{
    public class ViewModel
    {
        public Items ItemList { get; set; }

        public ViewModel()
        {
            ItemList = new Items();
        }

        internal void Save()
        {
            ItemList.Save();
        }
    }
}
