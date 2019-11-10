using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfiniteScrollDemo.Models;

namespace InfiniteScrollDemo.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Seventh item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Eigth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Nineth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Tenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Eleventh item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twelveth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Thirteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Seventeenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Eighteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Nineteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentieth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentyfirst item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentysecond item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentythird item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentyfourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentyfifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentysixth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentyseventh item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentyeight item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Twentynineth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Thirtieth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false, int lastIndex = 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            int numberOfItemsPerPage = 15;
            return await Task.FromResult(items.Skip(lastIndex).Take(numberOfItemsPerPage));
        }
    }
}