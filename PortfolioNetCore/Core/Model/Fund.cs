using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioNetCore.Core.Model
{
    public class Fund
    {

        [Key]
        public string ISINNumber { get; set; } = "";
        public string Currency { get; set; } = "";
        public string Name { get; set; } = "";
        public string Management { get; set; } = "";
        public string Focus { get; set; } = "";
        public string Type { get; set; } = "";
        public string PerformanceYTD { get; set; } = "";
        public string Performance1Year { get; set; } = "";
        public string Performance3Year { get; set; } = "";
        public string Performance5Year { get; set; } = "";
        public string PerformanceFromBeggining { get; set; } = "";
        public string PerformanceActualMinus9 { get; set; } = "";
        public string PerformanceActualMinus8 { get; set; } = "";
        public string PerformanceActualMinus7 { get; set; } = "";
        public string PerformanceActualMinus6 { get; set; } = "";
        public string PerformanceActualMinus5 { get; set; } = "";
        public string PerformanceActualMinus4 { get; set; } = "";
        public string PerformanceActualMinus3 { get; set; } = "";
        public string PerformanceActualMinus2 { get; set; } = "";
        public string PerformanceActualMinus1 { get; set; } = "";
        public string PerformanceAverage { get; set; } = "";

        public string VolatilityArrayString { get; set; }
        public string SharpRateArrayString { get; set; }
        public string BestMonthArrayString { get; set; }
        public string WorstMonthArrayString { get; set; }
        public string MaxLossArrayString { get; set; }
        public string OverFulFilmentArrayString { get; set; }

        public string Url { get; set; }

        [NotMapped]
        public List<string> VolatilityArray
        {
            get;// { return this.VolatilityArrayString != null ? this.VolatilityArrayString.Split(';').ToList() : this.VolatilityArray; }
            set;// { }
        }
        [NotMapped]
        public List<string> SharpRateArray
        {
            get;// { return this.SharpRateArrayString != null ? this.SharpRateArrayString.Split(';').ToList() : this.SharpRateArray; }
            set;// { }
        }
        [NotMapped]
        public List<string> BestMonthArray
        {
            get;// { return this.BestMonthArrayString != null ? this.BestMonthArrayString.Split(';').ToList() : this.BestMonthArray; }
            set;// { }
        }
        [NotMapped]
        public List<string> WorstMonthArray
        {
            get;// { return this.WorstMonthArrayString != null ? this.WorstMonthArrayString.Split(';').ToList() : this.WorstMonthArray; }
            set;// { }
        }
        [NotMapped]
        public List<string> MaxLossArray
        {
            get;// { return this.MaxLossArrayString != null ? this.MaxLossArrayString.Split(';').ToList() : this.MaxLossArray; }
            set;// { }
        }
        [NotMapped]
        public List<string> OverFulFilmentArray
        {
            get;//return this.OverFulFilmentArrayString != null ? this.OverFulFilmentArrayString.Split(';').ToList() : this.OverFulFilmentArray; }
            set;
        }


        //public string[] volatilityArray { get; set; } = new string[10];
        //public string[] sharpRateArray { get; set; } = new string[10];
        //public string[] bestMonthArray { get; set; } = new string[10];
        //public string[] worstMonthArray { get; set; } = new string[10];
        //public string[] maxLossArray { get; set; } = new string[10];
        //public string[] overFulFilmentArray { get; set; } = new string[10];
    }

  

   
}
