using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using MyCreditCalculator.Interfaces;
using MyCreditCalculator.Models;
using System.Threading.Tasks;

namespace MyCreditCalculator.Services
{
    public class CreditRepository
    {

        readonly SQLiteAsyncConnection database;

        public CreditRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<MyCredit>().Wait();
        }

        public Task<List<MyCredit>> GetItemsAsync()
        {
            return database.Table<MyCredit>().ToListAsync();
        }

        public Task<List<MyCredit>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<MyCredit>("SELECT * FROM [MyCredit] WHERE [Done] = 0");
        }

        public Task<MyCredit> GetItemAsync(int id)
        {
            return database.Table<MyCredit>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(MyCredit item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(MyCredit item)
        {
            return database.DeleteAsync(item);
        }
    }
}
