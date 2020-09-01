using PortfolioNetCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortfolioNetCore.Core
{
	public interface IFundRepository 
	{
        IEnumerable<Fund> GetFundList();      
    }
}