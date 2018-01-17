using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItemManager
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _vm;
        private Item _clipboard;

        public MainWindow()
        {
            InitializeComponent();

            _vm = new ViewModel();
            this.DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.Save();
        }

        private void itemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                Item selected = ((sender as ListBox).SelectedItem as Item);
                _vm.ItemList.Remove(selected);
                return;
            }

            if(e.Key == Key.Insert)
            {
                var index = (sender as ListBox).SelectedIndex;
                
                _vm.ItemList.InsertEmpty(index);
                return;
            }

            if(e.Key == Key.C)
            {
                _clipboard = ((sender as ListBox).SelectedItem as Item);
                return;
            }

            if(e.Key == Key.V)
            {
                if (_clipboard == null) return;

                var index = (sender as ListBox).SelectedIndex;
                _vm.ItemList[index] = _clipboard.Clone();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var collectionView = CollectionViewSource.GetDefaultView(_vm.ItemList);
            collectionView?.Refresh();
        }
    }
}
