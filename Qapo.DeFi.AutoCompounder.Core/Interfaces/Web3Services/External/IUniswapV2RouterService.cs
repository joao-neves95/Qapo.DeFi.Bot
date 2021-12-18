using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

using Nethereum.RPC.Eth.DTOs;

using Qapo.DeFi.AutoCompounder.Core.Models.Web3.External;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services.External
{
    public interface IUniswapV2RouterService : IWeb3Service
    {
        Task<string> WETHQueryAsync(WETHFunction wETHFunction, BlockParameter blockParameter = null);

        Task<string> WETHQueryAsync(BlockParameter blockParameter = null);

        Task<string> AddLiquidityRequestAsync(AddLiquidityFunction addLiquidityFunction);

        Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(AddLiquidityFunction addLiquidityFunction, CancellationTokenSource cancellationToken = null);

        Task<string> AddLiquidityRequestAsync(string tokenA, string tokenB, BigInteger amountADesired, BigInteger amountBDesired, BigInteger amountAMin, BigInteger amountBMin, string to, BigInteger deadline);

        Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(string tokenA, string tokenB, BigInteger amountADesired, BigInteger amountBDesired, BigInteger amountAMin, BigInteger amountBMin, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> AddLiquidityETHRequestAsync(AddLiquidityETHFunction addLiquidityETHFunction);

        Task<TransactionReceipt> AddLiquidityETHRequestAndWaitForReceiptAsync(AddLiquidityETHFunction addLiquidityETHFunction, CancellationTokenSource cancellationToken = null);

        Task<string> AddLiquidityETHRequestAsync(string token, BigInteger amountTokenDesired, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline);

        Task<TransactionReceipt> AddLiquidityETHRequestAndWaitForReceiptAsync(string token, BigInteger amountTokenDesired, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> FactoryQueryAsync(FactoryFunction factoryFunction, BlockParameter blockParameter = null);

        Task<string> FactoryQueryAsync(BlockParameter blockParameter = null);

        Task<BigInteger> GetAmountInQueryAsync(GetAmountInFunction getAmountInFunction, BlockParameter blockParameter = null);

        Task<BigInteger> GetAmountInQueryAsync(BigInteger amountOut, BigInteger reserveIn, BigInteger reserveOut, BlockParameter blockParameter = null);

        Task<BigInteger> GetAmountOutQueryAsync(GetAmountOutFunction getAmountOutFunction, BlockParameter blockParameter = null);

        Task<BigInteger> GetAmountOutQueryAsync(BigInteger amountIn, BigInteger reserveIn, BigInteger reserveOut, BlockParameter blockParameter = null);

        Task<List<BigInteger>> GetAmountsInQueryAsync(GetAmountsInFunction getAmountsInFunction, BlockParameter blockParameter = null);

        Task<List<BigInteger>> GetAmountsInQueryAsync(BigInteger amountOut, List<string> path, BlockParameter blockParameter = null);

        Task<List<BigInteger>> GetAmountsOutQueryAsync(GetAmountsOutFunction getAmountsOutFunction, BlockParameter blockParameter = null);

        Task<List<BigInteger>> GetAmountsOutQueryAsync(BigInteger amountIn, List<string> path, BlockParameter blockParameter = null);

        Task<BigInteger> QuoteQueryAsync(QuoteFunction quoteFunction, BlockParameter blockParameter = null);

        Task<BigInteger> QuoteQueryAsync(BigInteger amountA, BigInteger reserveA, BigInteger reserveB, BlockParameter blockParameter = null);

        Task<string> RemoveLiquidityRequestAsync(RemoveLiquidityFunction removeLiquidityFunction);

        Task<TransactionReceipt> RemoveLiquidityRequestAndWaitForReceiptAsync(RemoveLiquidityFunction removeLiquidityFunction, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityRequestAsync(string tokenA, string tokenB, BigInteger liquidity, BigInteger amountAMin, BigInteger amountBMin, string to, BigInteger deadline);

        Task<TransactionReceipt> RemoveLiquidityRequestAndWaitForReceiptAsync(string tokenA, string tokenB, BigInteger liquidity, BigInteger amountAMin, BigInteger amountBMin, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHRequestAsync(RemoveLiquidityETHFunction removeLiquidityETHFunction);

        Task<TransactionReceipt> RemoveLiquidityETHRequestAndWaitForReceiptAsync(RemoveLiquidityETHFunction removeLiquidityETHFunction, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHRequestAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline);

        Task<TransactionReceipt> RemoveLiquidityETHRequestAndWaitForReceiptAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHSupportingFeeOnTransferTokensRequestAsync(RemoveLiquidityETHSupportingFeeOnTransferTokensFunction removeLiquidityETHSupportingFeeOnTransferTokensFunction);

        Task<TransactionReceipt> RemoveLiquidityETHSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(RemoveLiquidityETHSupportingFeeOnTransferTokensFunction removeLiquidityETHSupportingFeeOnTransferTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHSupportingFeeOnTransferTokensRequestAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline);

        Task<TransactionReceipt> RemoveLiquidityETHSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHWithPermitRequestAsync(RemoveLiquidityETHWithPermitFunction removeLiquidityETHWithPermitFunction);

        Task<TransactionReceipt> RemoveLiquidityETHWithPermitRequestAndWaitForReceiptAsync(RemoveLiquidityETHWithPermitFunction removeLiquidityETHWithPermitFunction, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHWithPermitRequestAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline, bool approveMax, byte v, byte[] r, byte[] s);

        Task<TransactionReceipt> RemoveLiquidityETHWithPermitRequestAndWaitForReceiptAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline, bool approveMax, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHWithPermitSupportingFeeOnTransferTokensRequestAsync(RemoveLiquidityETHWithPermitSupportingFeeOnTransferTokensFunction removeLiquidityETHWithPermitSupportingFeeOnTransferTokensFunction);

        Task<TransactionReceipt> RemoveLiquidityETHWithPermitSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(RemoveLiquidityETHWithPermitSupportingFeeOnTransferTokensFunction removeLiquidityETHWithPermitSupportingFeeOnTransferTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityETHWithPermitSupportingFeeOnTransferTokensRequestAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline, bool approveMax, byte v, byte[] r, byte[] s);

        Task<TransactionReceipt> RemoveLiquidityETHWithPermitSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(string token, BigInteger liquidity, BigInteger amountTokenMin, BigInteger amountETHMin, string to, BigInteger deadline, bool approveMax, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityWithPermitRequestAsync(RemoveLiquidityWithPermitFunction removeLiquidityWithPermitFunction);

        Task<TransactionReceipt> RemoveLiquidityWithPermitRequestAndWaitForReceiptAsync(RemoveLiquidityWithPermitFunction removeLiquidityWithPermitFunction, CancellationTokenSource cancellationToken = null);

        Task<string> RemoveLiquidityWithPermitRequestAsync(string tokenA, string tokenB, BigInteger liquidity, BigInteger amountAMin, BigInteger amountBMin, string to, BigInteger deadline, bool approveMax, byte v, byte[] r, byte[] s);

        Task<TransactionReceipt> RemoveLiquidityWithPermitRequestAndWaitForReceiptAsync(string tokenA, string tokenB, BigInteger liquidity, BigInteger amountAMin, BigInteger amountBMin, string to, BigInteger deadline, bool approveMax, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);

        Task<string> SwapETHForExactTokensRequestAsync(SwapETHForExactTokensFunction swapETHForExactTokensFunction);

        Task<TransactionReceipt> SwapETHForExactTokensRequestAndWaitForReceiptAsync(SwapETHForExactTokensFunction swapETHForExactTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapETHForExactTokensRequestAsync(BigInteger amountOut, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapETHForExactTokensRequestAndWaitForReceiptAsync(BigInteger amountOut, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactETHForTokensRequestAsync(SwapExactETHForTokensFunction swapExactETHForTokensFunction);

        Task<TransactionReceipt> SwapExactETHForTokensRequestAndWaitForReceiptAsync(SwapExactETHForTokensFunction swapExactETHForTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactETHForTokensRequestAsync(BigInteger amountOutMin, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapExactETHForTokensRequestAndWaitForReceiptAsync(BigInteger amountOutMin, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactETHForTokensSupportingFeeOnTransferTokensRequestAsync(SwapExactETHForTokensSupportingFeeOnTransferTokensFunction swapExactETHForTokensSupportingFeeOnTransferTokensFunction);

        Task<TransactionReceipt> SwapExactETHForTokensSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(SwapExactETHForTokensSupportingFeeOnTransferTokensFunction swapExactETHForTokensSupportingFeeOnTransferTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactETHForTokensSupportingFeeOnTransferTokensRequestAsync(BigInteger amountOutMin, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapExactETHForTokensSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(BigInteger amountOutMin, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForETHRequestAsync(SwapExactTokensForETHFunction swapExactTokensForETHFunction);

        Task<TransactionReceipt> SwapExactTokensForETHRequestAndWaitForReceiptAsync(SwapExactTokensForETHFunction swapExactTokensForETHFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForETHRequestAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapExactTokensForETHRequestAndWaitForReceiptAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForETHSupportingFeeOnTransferTokensRequestAsync(SwapExactTokensForETHSupportingFeeOnTransferTokensFunction swapExactTokensForETHSupportingFeeOnTransferTokensFunction);

        Task<TransactionReceipt> SwapExactTokensForETHSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(SwapExactTokensForETHSupportingFeeOnTransferTokensFunction swapExactTokensForETHSupportingFeeOnTransferTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForETHSupportingFeeOnTransferTokensRequestAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapExactTokensForETHSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForTokensRequestAsync(SwapExactTokensForTokensFunction swapExactTokensForTokensFunction);

        Task<TransactionReceipt> SwapExactTokensForTokensRequestAndWaitForReceiptAsync(SwapExactTokensForTokensFunction swapExactTokensForTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForTokensRequestAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapExactTokensForTokensRequestAndWaitForReceiptAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForTokensSupportingFeeOnTransferTokensRequestAsync(SwapExactTokensForTokensSupportingFeeOnTransferTokensFunction swapExactTokensForTokensSupportingFeeOnTransferTokensFunction);

        Task<TransactionReceipt> SwapExactTokensForTokensSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(SwapExactTokensForTokensSupportingFeeOnTransferTokensFunction swapExactTokensForTokensSupportingFeeOnTransferTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapExactTokensForTokensSupportingFeeOnTransferTokensRequestAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapExactTokensForTokensSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(BigInteger amountIn, BigInteger amountOutMin, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapTokensForExactETHRequestAsync(SwapTokensForExactETHFunction swapTokensForExactETHFunction);

        Task<TransactionReceipt> SwapTokensForExactETHRequestAndWaitForReceiptAsync(SwapTokensForExactETHFunction swapTokensForExactETHFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapTokensForExactETHRequestAsync(BigInteger amountOut, BigInteger amountInMax, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapTokensForExactETHRequestAndWaitForReceiptAsync(BigInteger amountOut, BigInteger amountInMax, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);

        Task<string> SwapTokensForExactTokensRequestAsync(SwapTokensForExactTokensFunction swapTokensForExactTokensFunction);

        Task<TransactionReceipt> SwapTokensForExactTokensRequestAndWaitForReceiptAsync(SwapTokensForExactTokensFunction swapTokensForExactTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> SwapTokensForExactTokensRequestAsync(BigInteger amountOut, BigInteger amountInMax, List<string> path, string to, BigInteger deadline);

        Task<TransactionReceipt> SwapTokensForExactTokensRequestAndWaitForReceiptAsync(BigInteger amountOut, BigInteger amountInMax, List<string> path, string to, BigInteger deadline, CancellationTokenSource cancellationToken = null);
    }
}
