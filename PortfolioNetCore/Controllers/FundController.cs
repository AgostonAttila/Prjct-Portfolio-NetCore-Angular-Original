using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioNetCore.Core;
using PortfolioNetCore.Core.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortfolioNetCore.Controllers
{
    [Route("api/[controller]")]
    public class FundController : Controller
    {
        //private readonly IUnitOfWork unitOfWork;
        //private readonly IMapper mapper;
        private readonly IFundRepository repository;



        public FundController(IFundRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("[action]")]
        public IEnumerable<Fund> GetFundList()
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);         

            //if (fundList == null)
            //    return NotFound();

            return repository.GetFundList();
        }

        //[HttpGet("[action]")]
        //public IEnumerable<Fund> SavePortfolioFund(string filePath, List<Fund> fundList = null)
        //{
        //    fundList = Helper.GetTeletraderFundList(filePath, fundList);

        //    using (var db = new PortfolioContext())
        //    {
        //        for (int i = 0; i < fundList.Count; i++)
        //        {
        //            //fundList[i].VolatilityArrayString = String.Join(";", fundList[i].VolatilityArray);
        //            //fundList[i].SharpRateArrayString = String.Join(";", fundList[i].SharpRateArrayString);
        //            //fundList[i].BestMonthArrayString = String.Join(";", fundList[i].BestMonthArrayString);
        //            //fundList[i].WorstMonthArrayString = String.Join(";", fundList[i].WorstMonthArrayString);
        //            //fundList[i].MaxLossArrayString = String.Join(";", fundList[i].MaxLossArrayString);
        //            //fundList[i].OverFulFilmentArrayString = String.Join(";", fundList[i].OverFulFilmentArrayString);

        //            db.Funds.Add(fundList[i]);
        //        }
        //        var count = db.SaveChanges();
        //    }

        //    return fundList;
        //}

        //[HttpGet("[action]")]
        //public IEnumerable<Fund> ReFreshFundList()
        //{

        //    List<Fund> fundList = GetFundList().ToList();
        //    using (var db = new PortfolioContext())
        //    {

        //        List<FundDetail> dbFundDetailList = db.FundDetails.ToList();
        //        for (int i = 0; i < dbFundDetailList.Count; i++)
        //        {
        //            if (fundList.Where(p => p.Url == dbFundDetailList[i].Url).FirstOrDefault() == null)
        //                fundList.Add(new Fund { Url = dbFundDetailList[i].Url });
        //        }

        //        IEnumerable<Fund> fundList2 = SavePortfolioFund("", fundList);
        //        return fundList2;
        //    }
        //}
    }
}
