using MicroNutritionist.Common.Database.Models;
using MicroNutritionist.Common.Environment;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.Common.Database
{
    public class NutritionDatabase
    {
        private SQLiteAsyncConnection Database;

        public NutritionDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            Database.CreateTableAsync<Product>().Wait();
            Database.CreateTableAsync<Nutrition>().Wait();
            Database.CreateTableAsync<ProductNutritionAmount>().Wait();
            Database.CreateTableAsync<ConsumptionEvent>().Wait();
            Database.CreateTableAsync<Profile>().Wait();
            Database.CreateTableAsync<ProfileNutritionAmount>().Wait();
        }

        public async Task ClearDatabase()
        { 
            await Database.CloseAsync();
            File.Delete(Constants.DatabasePath);
        }

        #region GetAll

        public Task<List<Product>> GetAllProducts()
        {
            return Database.Table<Product>().ToListAsync();
        }

        public Task<List<Profile>> GetAllProfiles()
        {
            return Database.Table<Profile>().ToListAsync();
        }

        public Task<List<Nutrition>> GetAllNutrition()
        {
            return Database.Table<Nutrition>().ToListAsync();
        }

        public Task<List<ProductNutritionAmount>> GetAllProductNutritionAmount()
        {
            return Database.Table<ProductNutritionAmount>().ToListAsync();
        }

        public Task<List<ProductNutritionAmount>> GetAllProductNutritionAmountByProduct(int productId)
        {
            return Database.Table<ProductNutritionAmount>().Where(e => e.ProductId == productId).ToListAsync();
        }

        public Task<List<ProductNutritionAmount>> GetAllProductNutritionAmountByNutrition(int nutritionId)
        {
            return Database.Table<ProductNutritionAmount>().Where(e => e.NutritionId == nutritionId).ToListAsync();
        }

        public Task<List<ProfileNutritionAmount>> GetAllProfileNutritionAmountByProfile(int profileId)
        {
            return Database.Table<ProfileNutritionAmount>().Where(e => e.ProfileId == profileId).ToListAsync();
        }

        public Task<List<ProfileNutritionAmount>> GetAllProfileNutritionAmountByNutrition(int nutritionId)
        {
            return Database.Table<ProfileNutritionAmount>().Where(e => e.NutritionId == nutritionId).ToListAsync();
        }

        public Task<List<ConsumptionEvent>> GetAllConsumptionEvent()
        {
            return Database.Table<ConsumptionEvent>().ToListAsync();
        }

        public Task<List<ConsumptionEvent>> GetAllConsumptionEventByTimeWindow(DateTime start, DateTime end)
        {
            return Database.Table<ConsumptionEvent>().Where(e => e.Time >= start && e.Time <= end).ToListAsync();
        }
        public Task<List<ConsumptionEvent>> GetAllConsumptionEventByProductId(int productId)
        {
            return Database.Table<ConsumptionEvent>().Where(e => e.ProductId == productId).ToListAsync();
        }

        #endregion

        #region ProductCrud

        public Task<Product> GetProduct(int id)
        {
            return Database.Table<Product>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public Task<Product> GetProductByName(string name)
        {
            return Database.Table<Product>().FirstOrDefaultAsync(e => e.Name == name);
        }

        public Task InsertProduct(Product input)
        {
            return Database.InsertAsync(input);
        }

        public Task UpdateProduct(Product input)
        {
            return Database.UpdateAsync(input);
        }

        public Task DeleteProduct(Product input)
        {
            return Database.DeleteAsync(input);
        }

        public async Task DeleteProductCascade(Product input)
        {
            var prodNutAmt = await GetAllProductNutritionAmountByProduct(input.Id);

            foreach (var item in prodNutAmt)
                await Database.DeleteAsync(item);

            var consumeEvents = await GetAllConsumptionEventByProductId(input.Id);

            foreach (var item in consumeEvents)
                await Database.DeleteAsync(item);

            await Database.DeleteAsync(input);
        }

        #endregion

        #region ProfileCrud

        public Task<Profile> GetProfile(int id)
        {
            return Database.Table<Profile>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public Task<Profile> GetProfileByName(string name)
        {
            return Database.Table<Profile>().FirstOrDefaultAsync(e => e.Name == name);
        }

        public Task InsertProfile(Profile input)
        {
            return Database.InsertAsync(input);
        }

        public Task UpdateProfile(Profile input)
        {
            return Database.UpdateAsync(input);
        }

        public Task DeleteProfile(Profile input)
        {
            return Database.DeleteAsync(input);
        }

        public async Task DeleteProfileCascade(Profile input)
        {
            var prodNutAmt = await GetAllProfileNutritionAmountByProfile(input.Id);

            foreach (var item in prodNutAmt)
                await Database.DeleteAsync(item);

            await Database.DeleteAsync(input);
        }

        #endregion

        #region NutritionCrud

        public Task<Nutrition> GetNutrition(int id)
        {
            return Database.Table<Nutrition>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public Task<Nutrition> GetNutrition(string name)
        {
            return Database.Table<Nutrition>().FirstOrDefaultAsync(e => e.Name == name);
        }

        public Task InsertNutrition(Nutrition input)
        {
            return Database.InsertAsync(input);
        }

        public Task UpdateNutrition(Nutrition input)
        {
            return Database.UpdateAsync(input);
        }

        public Task DeleteNutrition(Nutrition input)
        {
            return Database.DeleteAsync(input);
        }

        public async Task DeleteNutritionCascade(Nutrition input)
        {
            var prodNutAmt = await GetAllProductNutritionAmountByNutrition(input.Id);

            foreach (var item in prodNutAmt)
                await Database.DeleteAsync(item);

            var profNutAmt = await GetAllProfileNutritionAmountByNutrition(input.Id);

            foreach (var item in profNutAmt)
                await Database.DeleteAsync(item);

            await Database.DeleteAsync(input);
        }

        #endregion

        #region ProductNutritionAmountCrud

        public Task<ProductNutritionAmount> GetProductNutritionAmount(int id)
        {
            return Database.Table<ProductNutritionAmount>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public Task<List<ProductNutritionAmount>> GetProductNutritionAmountByNutrition(int nutritionId)
        {
            return Database.Table<ProductNutritionAmount>().Where(e => e.NutritionId == nutritionId).ToListAsync();
        }

        public Task<List<ProductNutritionAmount>> GetProductNutritionAmountByProduct(int productId)
        {
            return Database.Table<ProductNutritionAmount>().Where(e => e.ProductId == productId).ToListAsync();
        }

        public Task InsertProductNutritionAmount(ProductNutritionAmount input)
        {
            return Database.InsertAsync(input);
        }

        public Task UpdateProductNutritionAmount(ProductNutritionAmount input)
        {
            return Database.UpdateAsync(input);
        }

        public Task DeleteProductNutritionAmount(ProductNutritionAmount input)
        {
            return Database.DeleteAsync(input);
        }

        #endregion

        #region ProfileNutritionAmountCrud

        public Task<ProfileNutritionAmount> GetProfileNutritionAmount(int id)
        {
            return Database.Table<ProfileNutritionAmount>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public Task<List<ProfileNutritionAmount>> GetProfileNutritionAmountByNutrition(int nutritionId)
        {
            return Database.Table<ProfileNutritionAmount>().Where(e => e.NutritionId == nutritionId).ToListAsync();
        }

        public Task<List<ProfileNutritionAmount>> GetProfileNutritionAmountByProfile(int profileId)
        {
            return Database.Table<ProfileNutritionAmount>().Where(e => e.ProfileId == profileId).ToListAsync();
        }

        public Task InsertProfileNutritionAmount(ProfileNutritionAmount input)
        {
            return Database.InsertAsync(input);
        }

        public Task UpdateProfileNutritionAmount(ProfileNutritionAmount input)
        {
            return Database.UpdateAsync(input);
        }

        public Task DeleteProfileNutritionAmount(ProfileNutritionAmount input)
        {
            return Database.DeleteAsync(input);
        }

        #endregion

        #region ConsumptionEventCrud

        public Task<ConsumptionEvent> GetConsumptionEvent(int id)
        {
            return Database.Table<ConsumptionEvent>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task InsertConsumptionEvent(ConsumptionEvent input)
        {
            return Database.InsertAsync(input);
        }

        public Task UpdateConsumptionEvent(ConsumptionEvent input)
        {
            return Database.UpdateAsync(input);
        }

        public Task DeleteConsumptionEvent(ConsumptionEvent input)
        {
            return Database.DeleteAsync(input);
        }

        #endregion
    }
}
