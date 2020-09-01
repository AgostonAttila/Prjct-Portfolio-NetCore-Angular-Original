using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioNetCore.Core.Model
{
    public class FundDetail
    {
        [Key]
        public string ISINNumber { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Management { get; set; }
        public string Focus { get; set; }
        public string Type { get; set; }

        public string Url { get; set; }
    }
}
