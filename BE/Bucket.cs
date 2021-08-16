using System;
using System.Collections.Generic;
using System.Text;

namespace BELayer
{
    public class Bucket
    {

        private List<Good> boughtList;
        private DateTime dateTime;
        public DateTime GetDateTime()
        {
            return dateTime;
        }
        public List<Good> getBoughtList()
        {
            return boughtList;
        }
    }
}
