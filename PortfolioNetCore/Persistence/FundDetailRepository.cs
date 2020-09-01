using PortfolioNetCore.Core;
using PortfolioNetCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioNetCore.Persistence
{
    public class FundDetailRepository : IFundDetailRepository
    {
        private readonly PortfolioContext context;

        public FundDetailRepository(PortfolioContext context)
        {
            this.context = context;
        }

        public bool DeleteFundDetail(string isin)
        {
            FundDetail fundDetail = (FundDetail)context.FundDetails.Where(p => p.ISINNumber == isin).FirstOrDefault();
            if (fundDetail != null)
            {
                context.FundDetails.Remove(fundDetail);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<FundDetail> GetFundDetailList()
        {
            return context.FundDetails.ToList();
        }
    }
}
