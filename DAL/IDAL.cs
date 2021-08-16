using System;
using BELayer;
using System.Collections.Generic;


namespace DAL
{
    public interface IDAL
    {
        List<Good> GetCatalog();
        List<Bucket> GetPreviousBuckets(); 

        bool AddNewGood(Good good);
        bool AddNewBucket(Bucket bucket);
        bool UpdateGood(Good good);
        List<Bucket> GetShoppingListsFromTill(DateTime start, DateTime finish);
        List<Good> UnupdatedGoods();
        Good GetGoodByID(string _id);
        Bucket GetBucketByDateTime(string _dateTime);
        Dictionary<float, Good> GetGoodsRatio();
        Dictionary<int, Good> GetBoughtGoodsQuantity();
    }
}
