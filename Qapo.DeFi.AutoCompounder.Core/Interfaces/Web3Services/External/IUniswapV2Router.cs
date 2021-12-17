using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services.External
{
    public interface IUniswapV2Router
    {
        int[] GetAmountsOut(int amountIn, string[] addressPath);
    }
}
