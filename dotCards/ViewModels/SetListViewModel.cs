using dotCards.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System;

namespace dotCards.ViewModels
{
    public class SetListViewModel : ViewModelBase
    {
        public SetListViewModel(IEnumerable<SetItem> items)
        {
            Items = new ObservableCollection<SetItem>(items);
        }

        public ObservableCollection<SetItem> Items { get; }
    }
}
