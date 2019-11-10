using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using InfiniteScrollDemo.Models;
using InfiniteScrollDemo.Views;
using System.Linq;

namespace InfiniteScrollDemo.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command ItemTresholdReachedCommand { get; set; }
        public Command RefreshItemsCommand { get; set; }
        public const string ScrollToPreviousLastItem = "Scroll_ToPrevious";
        private int _itemTreshold;
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        public ItemsViewModel()
        {
            ItemTreshold = 1;
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            RefreshItemsCommand = new Command(async () =>
            {
                await ExecuteLoadItemsCommand();
                IsRefreshing = false;
            });
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ItemsTresholdReached()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DataStore.GetItemsAsync(true, Items.Count);

                var previousLastItem = Items.Last();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                Debug.WriteLine($"{items.Count()} {Items.Count} ");
                if (items.Count() == 0)
                {
                    ItemTreshold = -1;
                    return;
                }
                //MessagingCenter.Send<object, Item>(this, ScrollToPreviousLastItem, previousLastItem);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;


            try
            {
                ItemTreshold = 1;
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}