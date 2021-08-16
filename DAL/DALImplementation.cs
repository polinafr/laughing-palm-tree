using BELayer;
using System;
using System.Collections.Generic;
using System.Text;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

/* Credit for this guy for brilliant explanation of Firebase Database usage:
 * https://www.youtube.com/watch?v=QE5UV8NyYqg
 */
namespace DAL
{
    public class DALImplementation : IDAL
    {
        private List<Good> catalog = new List<Good>();
        private List<Bucket> shoppingLists = new List<Bucket>(); //add reading from database
        private string goodDir = "Goods/";
        private string bucketsDir = "Buckets/";
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "ABAdEUajJeEWzBaQcr80vQnOQ5cbE95VGC6qFsi8",
            BasePath = "https://shoppingsystem-6521c-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;


        public bool AddNewBucket(Bucket bucket)
        {
            try
            {
                EstablishDatabaseConnection();
                for (int y = 0; y < bucket.getBoughtList().Count; y++)
                {
                    var setter = client.Set(bucketsDir + bucket.GetDateTime().ToString() + /*y.ToString() + '/' + */bucket.getBoughtList()[y].getID(), bucket.getBoughtList()[y]);
                }
                return true; // assuming that adding the same item with throw exception
            }
            catch
            {
                return false;
            }
        }

        public bool AddNewGood(Good good)
        {
            try
            {
                EstablishDatabaseConnection();
                var setter = client.Set(goodDir + good.getID(), good);
                return true; // assuming that adding the same item with throw exception
            }
            catch
            {
                return false;
            }

        }

        /* Credit for this guys https://stackoverflow.com/questions/58444872/how-to-get-all-data-from-firebase-with-firesharp
         * for explanation how to get all the objects from firebase
         */
        private async void updateCatalog()
        {
            EstablishDatabaseConnection();
            FirebaseResponse response = await client.GetAsync(goodDir);
            catalog = (List<Good>)Newtonsoft.Json.JsonConvert.DeserializeObject(response.ToString(), (typeof(List<Good>)));
        }

        public List<Good> GetCatalog()
        {
            updateCatalog();
            return catalog;
        }

        private async void updateShoppingLists()
        {
            EstablishDatabaseConnection();
            updateCatalog();
            FirebaseResponse response = await client.GetAsync(bucketsDir);
            //shoppingLists = (List<Bucket>)Newtonsoft.Json.

            shoppingLists = (List<Bucket>)Newtonsoft.Json.JsonConvert.DeserializeObject(response.ToString(), (typeof(List<Bucket>)));
            //may be very problematic

        }

        public List<Bucket> GetPreviousBuckets()
        {
            updateShoppingLists();
            return shoppingLists;
        }

        public bool UpdateGood(Good good)
        {
            try
            {
                EstablishDatabaseConnection();
                var setter = client.Update(goodDir + good.getID(), good);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EstablishDatabaseConnection()
        {
            try
            {
                client = new FireSharp.FirebaseClient(fcon);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Bucket> GetShoppingListsFromTill(DateTime start, DateTime finish)
        {
            List<Bucket> requestedBuckets = new List<Bucket>();
            //   Queue<Bucket> copy = shoppingLists;
            for (int i = 0; i < shoppingLists.Count; i++)
            {
                Bucket bucket = shoppingLists[i];
                if (bucket.GetDateTime() > start && bucket.GetDateTime() < finish)
                    requestedBuckets.Add(bucket);
                //shoppingLists.Enqueue(bucket);
            }
            return requestedBuckets;
        }

        List<Good> IDAL.UnupdatedGoods()
        {
            updateCatalog();
            List<Good> unupdated = new List<Good>();
            foreach (Good good in catalog)
            {
                if (good.DescriptionIsNull() || good.PictureIsNull())
                {
                    unupdated.Add(good);
                }
            }
            return unupdated;
        }

        Good IDAL.GetGoodByID(string _id)
        {
            EstablishDatabaseConnection();
            return client.Get("Goods/" + _id).ResultAs<Good>();
        }

        Bucket IDAL.GetBucketByDateTime(string _dateTime)//assuming the user can't make more than one shopping per day
        {
            updateShoppingLists();
            foreach (Bucket bucket in shoppingLists)
            {
                if (bucket.GetDateTime().ToString() == _dateTime)
                    return bucket;
            }
            return null;
        }

        public Dictionary<float, Good> GetGoodsRatio()
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, Good> GetBoughtGoodsQuantity()
        {
            throw new NotImplementedException();
        }
    }
}
