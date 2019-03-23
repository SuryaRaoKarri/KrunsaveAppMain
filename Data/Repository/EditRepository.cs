using System.Threading.Tasks;
using Krunsave.Data.IRepository;
using Krunsave.Model;
using Microsoft.EntityFrameworkCore;

namespace Krunsave.Data.Repository
{
    public class EditRepository : IEditRepository
    {
        /*Add validation so that only the store owner can edit the store details */
        private readonly DataContext _context;
        public EditRepository(DataContext context){
            _context = context;
        }

        public async Task<Availablefood> Editavailablefood(Availablefood availablefood)
        {
            var availablefooddetails = await _context.Availablefoods.FirstOrDefaultAsync(a => a.availableFoodID == availablefood.availableFoodID);
            if(availablefood.engName != null) availablefooddetails.engName = availablefood.engName;
            if(availablefood.thaiName != null) availablefooddetails.thaiName = availablefood.thaiName;
            if(availablefood.totalUnits != null) availablefooddetails.totalUnits = availablefood.totalUnits;
            if(availablefood.unitType != null) availablefooddetails.unitType = availablefood.unitType;
            if(availablefood.availableUnits != null) availablefooddetails.availableUnits = availablefood.availableUnits;
            if(availablefood.discountPerUnit != null) availablefooddetails.discountPerUnit = availablefood.discountPerUnit;
            if(availablefood.pricePerUnit != null) availablefooddetails.pricePerUnit = availablefood.pricePerUnit;
            if(availablefood.cookedDate != null) availablefooddetails.cookedDate = availablefood.cookedDate;
            if(availablefood.expiryDate != null) availablefooddetails.expiryDate = availablefood.expiryDate;
            if(availablefood.description != null) availablefooddetails.description = availablefood.description;
            if(!availablefood.status.Equals(null)) availablefooddetails.status = availablefood.status;
            
           /* foodTypeID storeID*/

           _context.SaveChanges();
            return availablefooddetails;
        }

        public async Task<Store> Editstore(Store store)
        {
            var storedetails = await _context.Stores.FirstOrDefaultAsync(s => s.storeID == store.storeID);
            if(storedetails.storeName != null) storedetails.storeName = store.storeName;
            if(storedetails.managerName != null) storedetails.managerName = store.managerName;
            if(storedetails.phoneNumber != null) storedetails.phoneNumber = store.phoneNumber;
            if(storedetails.email != null) storedetails.email = store.email;
            if(storedetails.address != null) storedetails.address = store.address;
            if(!storedetails.lat.Equals(null)) storedetails.lat = store.lat;
            if(storedetails.lng.Equals(null)) storedetails.lng = store.lng;
            if(storedetails.openTime != null) storedetails.openTime = store.openTime;
            if(storedetails.closeTime != null) storedetails.closeTime = store.closeTime;
            /* Store Type and User ID think!!*/
            _context.SaveChanges();
            return storedetails;
        }
    }
}