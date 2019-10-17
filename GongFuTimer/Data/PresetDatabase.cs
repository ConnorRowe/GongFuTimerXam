using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using GongFuTimer.ViewModel;

namespace GongFuTimer.Data
{
    public class PresetDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public PresetDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Preset>().Wait();
        }

        public Task<List<Preset>> GetPresetsAsync()
        {
            return _database.Table<Preset>().ToListAsync();
        }

        public Task<Preset> GetPresetAsync(int id)
        {
            return _database.Table<Preset>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SavePresetAsync(Preset preset)
        {
            if(preset.ID != 0)
            {
                return _database.UpdateAsync(preset);
            }
            else
            {
                return _database.InsertAsync(preset);
            }
        }

        public Task<int> DeletePresetAsync(Preset preset)
        {
            return _database.DeleteAsync(preset);
        }
    }
}
