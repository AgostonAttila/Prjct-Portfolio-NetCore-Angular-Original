using Microsoft.AspNetCore.Mvc;
using PortfolioNetCore.Core;
using PortfolioNetCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioNetCore.Controllers
{
    [Route("api/[controller]")]
    public class FundDetailController : Controller
    {
        //private readonly IUnitOfWork unitOfWork;
        //private readonly IMapper mapper;

        private readonly IFundDetailRepository repository;

        public FundDetailController(IFundDetailRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public IEnumerable<FundDetail> GetFundDetailList()
        {
            return repository.GetFundDetailList();
        }


        [Route("/api/[controller]/DeleteFundDetail/{isin}")]
        [HttpPost("[action]")]
        public IEnumerable<FundDetail> DeleteFundDetail(string isin)
        {
            repository.DeleteFundDetail(isin);
            return repository.GetFundDetailList();
        }

        //[HttpGet("[action]")]
        //public IEnumerable<FundDetail> SaveFundDetailList(string filePath, string managementName)
        //{

        //    List<FundDetail> fundDetailList = Helper.GetTeletraderFundDetailList(filePath);

        //    using (var db = new PortfolioContext())
        //    {
        //        IEnumerable<FundDetail> dbFundDetailList = GetFundDetailList();
        //        List<Management> dbManagementList = GetManagementList().ToList();

        //        if (dbManagementList.Where(p => p.Name == managementName).FirstOrDefault() == null)
        //            dbManagementList.Add(new Management { Name = managementName, FundISINNumberHashSet = new HashSet<string>(), FundISINNumberString = "" });

        //        Management actManagement = dbManagementList.Where(p => p.Name == managementName).FirstOrDefault();

        //        for (int i = 0; i < fundDetailList.Count; i++)
        //        {
        //            if (db.FundDetails.Where(p => p.ISINNumber == fundDetailList[i].ISINNumber).FirstOrDefault() == null)
        //                db.FundDetails.Add(fundDetailList[i]);
        //            else
        //            {
        //                FundDetail actFundDetail = db.FundDetails.Where(p => p.ISINNumber == fundDetailList[i].ISINNumber).FirstOrDefault();
        //                actFundDetail.Currency = fundDetailList[i].Currency;
        //                actFundDetail.Focus = fundDetailList[i].Focus;
        //                actFundDetail.Management = fundDetailList[i].Management;
        //                actFundDetail.Name = fundDetailList[i].Name;
        //                actFundDetail.Type = fundDetailList[i].Type;
        //                actFundDetail.Url = fundDetailList[i].Url;
        //            }

        //            actManagement.FundISINNumberHashSet.Add(fundDetailList[i].ISINNumber);
        //        }

        //        if (db.Managements.Where(p => p.Name == managementName).FirstOrDefault() == null)
        //            db.Managements.Add(new Management { Name = managementName, FundISINNumberString = String.Join(";", actManagement.FundISINNumberHashSet) });
        //        else
        //        {
        //            Management actDBManagement = db.Managements.Where(p => p.Name == managementName).FirstOrDefault();
        //            if (actDBManagement != null)
        //                actDBManagement.FundISINNumberString = String.Join(";", actManagement.FundISINNumberHashSet);
        //        }

        //        var count = db.SaveChanges();
        //    }

        //    return fundDetailList;
        //}
    }
}


 
