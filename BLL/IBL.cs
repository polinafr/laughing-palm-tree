using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BELayer;
using DAL;

namespace BLL
{
    enum GraphType { Columns, Pie};
    interface IBL
    {
        
        bool saveToPDF(Dictionary<Good, float> statistics, GraphType type);
        List<List<Good>> getRecommendations();
        Dictionary<float, Good> GetGoodsRatio();
        Dictionary<int, Good> GetBoughtGoodsQuantity();
    }
}
