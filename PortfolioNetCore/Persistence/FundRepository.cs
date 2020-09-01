using PortfolioNetCore.Core;
using PortfolioNetCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioNetCore.Persistence
{

    public class FundRepository : IFundRepository
    {
        private readonly PortfolioContext context;

        public FundRepository(PortfolioContext context)
        {
            this.context = context;
        }

        public IEnumerable<Fund> GetFundList()
        {
            List<Fund> fundList = new List<Fund>();
            List<Fund> dbFundList = context.Funds.ToList();

            for (int i = 0; i < dbFundList.Count; i++)
            {
                fundList.Add(new Fund
                {
                    ISINNumber = dbFundList[i].ISINNumber,
                    Name = dbFundList[i].Name,
                    Currency = dbFundList[i].Currency,
                    Focus = dbFundList[i].Focus,
                    Management = dbFundList[i].Management,
                    Type = dbFundList[i].Type,
                    Performance1Year = dbFundList[i].Performance1Year,
                    Performance3Year = dbFundList[i].Performance3Year,
                    Performance5Year = dbFundList[i].Performance5Year,
                    PerformanceFromBeggining = dbFundList[i].PerformanceFromBeggining,
                    PerformanceActualMinus1 = dbFundList[i].PerformanceActualMinus1,
                    PerformanceActualMinus2 = dbFundList[i].PerformanceActualMinus2,
                    PerformanceActualMinus3 = dbFundList[i].PerformanceActualMinus3,
                    PerformanceActualMinus4 = dbFundList[i].PerformanceActualMinus4,
                    PerformanceActualMinus5 = dbFundList[i].PerformanceActualMinus5,
                    PerformanceActualMinus6 = dbFundList[i].PerformanceActualMinus6,
                    PerformanceActualMinus7 = dbFundList[i].PerformanceActualMinus7,
                    PerformanceActualMinus8 = dbFundList[i].PerformanceActualMinus8,
                    PerformanceActualMinus9 = dbFundList[i].PerformanceActualMinus9,
                    PerformanceAverage = dbFundList[i].PerformanceAverage,

                    VolatilityArray = dbFundList[i].VolatilityArrayString != null ? dbFundList[i].VolatilityArrayString.Split(';').ToList() : new List<string>(),
                    SharpRateArray = dbFundList[i].SharpRateArray != null ? dbFundList[i].SharpRateArrayString.Split(';').ToList() : new List<string>(),
                    BestMonthArray = dbFundList[i].BestMonthArray != null ? dbFundList[i].BestMonthArrayString.Split(';').ToList() : new List<string>(),
                    WorstMonthArray = dbFundList[i].WorstMonthArray != null ? dbFundList[i].WorstMonthArrayString.Split(';').ToList() : new List<string>(),
                    MaxLossArray = dbFundList[i].MaxLossArray != null ? dbFundList[i].MaxLossArrayString.Split(';').ToList() : new List<string>(),
                    OverFulFilmentArray = dbFundList[i].OverFulFilmentArray != null ? dbFundList[i].OverFulFilmentArrayString.Split(';').ToList() : new List<string>()
                });
            }


            return fundList;
        }
    }
}
