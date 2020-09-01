using PortfolioNetCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioNetCore.Core
{
    public interface IFundDetailRepository
    {
        IEnumerable<FundDetail> GetFundDetailList();

        bool DeleteFundDetail(string isin);
    }
}
