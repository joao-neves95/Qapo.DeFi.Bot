using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

using Nethereum.RPC.Eth.DTOs;

using Qapo.DeFi.AutoCompounder.Core.Models.Web3.LockedStratModels;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services
{
    public interface ILockedStratService
    {
        Task<string> DepositRequestAsync(DepositFunction depositFunction);

        Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(DepositFunction depositFunction, CancellationTokenSource cancellationToken = null);

        Task<string> DepositRequestAsync(BigInteger amount);

        Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null);

        Task<string> DepositAllRequestAsync(DepositAllFunction depositAllFunction);

        Task<string> DepositAllRequestAsync();

        Task<TransactionReceipt> DepositAllRequestAndWaitForReceiptAsync(DepositAllFunction depositAllFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> DepositAllRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);

        Task<string> ExecuteRequestAsync(ExecuteFunction executeFunction);

        Task<string> ExecuteRequestAsync();

        Task<TransactionReceipt> ExecuteRequestAndWaitForReceiptAsync(ExecuteFunction executeFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> ExecuteRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);

        Task<BigInteger> GetDeployedBalanceQueryAsync(GetDeployedBalanceFunction getDeployedBalanceFunction, BlockParameter blockParameter = null);

        Task<BigInteger> GetDeployedBalanceQueryAsync(BlockParameter blockParameter = null);

        Task<BigInteger> GetPendingRewardAmountQueryAsync(GetPendingRewardAmountFunction getPendingRewardAmountFunction, BlockParameter blockParameter = null);

        Task<BigInteger> GetPendingRewardAmountQueryAsync(BlockParameter blockParameter = null);

        Task<BigInteger> GetTvlQueryAsync(GetTvlFunction getTvlFunction, BlockParameter blockParameter = null);

        Task<BigInteger> GetTvlQueryAsync(BlockParameter blockParameter = null);

        Task<BigInteger> GetUndeployedBalanceQueryAsync(GetUndeployedBalanceFunction getUndeployedBalanceFunction, BlockParameter blockParameter = null);

        Task<BigInteger> GetUndeployedBalanceQueryAsync(BlockParameter blockParameter = null);

        Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null);

        Task<string> OwnerQueryAsync(BlockParameter blockParameter = null);

        Task<string> PanicRequestAsync(PanicFunction panicFunction);

        Task<string> PanicRequestAsync();

        Task<TransactionReceipt> PanicRequestAndWaitForReceiptAsync(PanicFunction panicFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> PanicRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);

        Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction);

        Task<string> RenounceOwnershipRequestAsync();

        Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);

        Task<string> RetireRequestAsync(RetireFunction retireFunction);

        Task<string> RetireRequestAsync();

        Task<TransactionReceipt> RetireRequestAndWaitForReceiptAsync(RetireFunction retireFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> RetireRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);

        Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction);

        Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null);

        Task<string> TransferOwnershipRequestAsync(string newOwner);

        Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null);

        Task<string> UnpanicRequestAsync(UnpanicFunction unpanicFunction);

        Task<string> UnpanicRequestAsync();

        Task<TransactionReceipt> UnpanicRequestAndWaitForReceiptAsync(UnpanicFunction unpanicFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> UnpanicRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);

        Task<string> UntuckTokensRequestAsync(UntuckTokensFunction untuckTokensFunction);

        Task<TransactionReceipt> UntuckTokensRequestAndWaitForReceiptAsync(UntuckTokensFunction untuckTokensFunction, CancellationTokenSource cancellationToken = null);

        Task<string> UntuckTokensRequestAsync(string token);

        Task<TransactionReceipt> UntuckTokensRequestAndWaitForReceiptAsync(string token, CancellationTokenSource cancellationToken = null);

        Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction);

        Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null);

        Task<string> WithdrawRequestAsync(BigInteger amount);

        Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null);

        Task<string> WithdrawAllRequestAsync(WithdrawAllFunction withdrawAllFunction);

        Task<string> WithdrawAllRequestAsync();

        Task<TransactionReceipt> WithdrawAllRequestAndWaitForReceiptAsync(WithdrawAllFunction withdrawAllFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> WithdrawAllRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);

        Task<string> WithdrawAllUndeployedRequestAsync(WithdrawAllUndeployedFunction withdrawAllUndeployedFunction);

        Task<string> WithdrawAllUndeployedRequestAsync();

        Task<TransactionReceipt> WithdrawAllUndeployedRequestAndWaitForReceiptAsync(WithdrawAllUndeployedFunction withdrawAllUndeployedFunction, CancellationTokenSource cancellationToken = null);

        Task<TransactionReceipt> WithdrawAllUndeployedRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null);
    }
}
