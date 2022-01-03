using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;

using Qapo.DeFi.Bot.Core.Interfaces.Web3Services;
using Qapo.DeFi.Bot.Core.Models.Web3.LockedStratModels;
using Qapo.DeFi.Bot.Core.Web3Services.SushiSwapLpLockedStrat.DeploymentDefinition;

namespace Qapo.DeFi.Bot.Core.Web3Services.SushiSwapLpLockedStrat
{
    public partial class SushiSwapLpLockedStratService : ILockedStratService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SushiSwapLpLockedStratDeployment sushiSwapLpLockedStratDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SushiSwapLpLockedStratDeployment>().SendRequestAndWaitForReceiptAsync(sushiSwapLpLockedStratDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SushiSwapLpLockedStratDeployment sushiSwapLpLockedStratDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SushiSwapLpLockedStratDeployment>().SendRequestAsync(sushiSwapLpLockedStratDeployment);
        }

        public static async Task<SushiSwapLpLockedStratService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SushiSwapLpLockedStratDeployment sushiSwapLpLockedStratDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, sushiSwapLpLockedStratDeployment, cancellationTokenSource);
            return new SushiSwapLpLockedStratService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SushiSwapLpLockedStratService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> DepositRequestAsync(DepositFunction depositFunction)
        {
             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(DepositFunction depositFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public Task<string> DepositRequestAsync(BigInteger amount)
        {
            var depositFunction = new DepositFunction();
                depositFunction.Amount = amount;

             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var depositFunction = new DepositFunction();
                depositFunction.Amount = amount;

             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public Task<string> DepositAllRequestAsync(DepositAllFunction depositAllFunction)
        {
             return ContractHandler.SendRequestAsync(depositAllFunction);
        }

        public Task<string> DepositAllRequestAsync()
        {
             return ContractHandler.SendRequestAsync<DepositAllFunction>();
        }

        public Task<TransactionReceipt> DepositAllRequestAndWaitForReceiptAsync(DepositAllFunction depositAllFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositAllFunction, cancellationToken);
        }

        public Task<TransactionReceipt> DepositAllRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<DepositAllFunction>(null, cancellationToken);
        }

        public Task<string> ExecuteRequestAsync(ExecuteFunction executeFunction)
        {
             return ContractHandler.SendRequestAsync(executeFunction);
        }

        public Task<string> ExecuteRequestAsync()
        {
             return ContractHandler.SendRequestAsync<ExecuteFunction>();
        }

        public Task<TransactionReceipt> ExecuteRequestAndWaitForReceiptAsync(ExecuteFunction executeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(executeFunction, cancellationToken);
        }

        public Task<TransactionReceipt> ExecuteRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<ExecuteFunction>(null, cancellationToken);
        }

        public Task<BigInteger> GetDeployedBalanceQueryAsync(GetDeployedBalanceFunction getDeployedBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDeployedBalanceFunction, BigInteger>(getDeployedBalanceFunction, blockParameter);
        }


        public Task<BigInteger> GetDeployedBalanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDeployedBalanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetPendingRewardAmountQueryAsync(GetPendingRewardAmountFunction getPendingRewardAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPendingRewardAmountFunction, BigInteger>(getPendingRewardAmountFunction, blockParameter);
        }


        public Task<BigInteger> GetPendingRewardAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPendingRewardAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetTvlQueryAsync(GetTvlFunction getTvlFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTvlFunction, BigInteger>(getTvlFunction, blockParameter);
        }


        public Task<BigInteger> GetTvlQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTvlFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetUndeployedBalanceQueryAsync(GetUndeployedBalanceFunction getUndeployedBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUndeployedBalanceFunction, BigInteger>(getUndeployedBalanceFunction, blockParameter);
        }


        public Task<BigInteger> GetUndeployedBalanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUndeployedBalanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }


        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<string> PanicRequestAsync(PanicFunction panicFunction)
        {
             return ContractHandler.SendRequestAsync(panicFunction);
        }

        public Task<string> PanicRequestAsync()
        {
             return ContractHandler.SendRequestAsync<PanicFunction>();
        }

        public Task<TransactionReceipt> PanicRequestAndWaitForReceiptAsync(PanicFunction panicFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(panicFunction, cancellationToken);
        }

        public Task<TransactionReceipt> PanicRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<PanicFunction>(null, cancellationToken);
        }

        public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
        }

        public Task<string> RenounceOwnershipRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
        }

        public Task<string> RetireRequestAsync(RetireFunction retireFunction)
        {
             return ContractHandler.SendRequestAsync(retireFunction);
        }

        public Task<string> RetireRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RetireFunction>();
        }

        public Task<TransactionReceipt> RetireRequestAndWaitForReceiptAsync(RetireFunction retireFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(retireFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RetireRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RetireFunction>(null, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;

             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;

             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> UnpanicRequestAsync(UnpanicFunction unpanicFunction)
        {
             return ContractHandler.SendRequestAsync(unpanicFunction);
        }

        public Task<string> UnpanicRequestAsync()
        {
             return ContractHandler.SendRequestAsync<UnpanicFunction>();
        }

        public Task<TransactionReceipt> UnpanicRequestAndWaitForReceiptAsync(UnpanicFunction unpanicFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(unpanicFunction, cancellationToken);
        }

        public Task<TransactionReceipt> UnpanicRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<UnpanicFunction>(null, cancellationToken);
        }

        public Task<string> UntuckTokensRequestAsync(UntuckTokensFunction untuckTokensFunction)
        {
             return ContractHandler.SendRequestAsync(untuckTokensFunction);
        }

        public Task<TransactionReceipt> UntuckTokensRequestAndWaitForReceiptAsync(UntuckTokensFunction untuckTokensFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(untuckTokensFunction, cancellationToken);
        }

        public Task<string> UntuckTokensRequestAsync(string token)
        {
            var untuckTokensFunction = new UntuckTokensFunction();
                untuckTokensFunction.Token = token;

             return ContractHandler.SendRequestAsync(untuckTokensFunction);
        }

        public Task<TransactionReceipt> UntuckTokensRequestAndWaitForReceiptAsync(string token, CancellationTokenSource cancellationToken = null)
        {
            var untuckTokensFunction = new UntuckTokensFunction();
                untuckTokensFunction.Token = token;

             return ContractHandler.SendRequestAndWaitForReceiptAsync(untuckTokensFunction, cancellationToken);
        }

        public Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public Task<string> WithdrawRequestAsync(BigInteger amount)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Amount = amount;

             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Amount = amount;

             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public Task<string> WithdrawAllRequestAsync(WithdrawAllFunction withdrawAllFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawAllFunction);
        }

        public Task<string> WithdrawAllRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawAllFunction>();
        }

        public Task<TransactionReceipt> WithdrawAllRequestAndWaitForReceiptAsync(WithdrawAllFunction withdrawAllFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawAllFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawAllRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawAllFunction>(null, cancellationToken);
        }

        public Task<string> WithdrawAllUndeployedRequestAsync(WithdrawAllUndeployedFunction withdrawAllUndeployedFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawAllUndeployedFunction);
        }

        public Task<string> WithdrawAllUndeployedRequestAsync()
        {
             return ContractHandler.SendRequestAsync<WithdrawAllUndeployedFunction>();
        }

        public Task<TransactionReceipt> WithdrawAllUndeployedRequestAndWaitForReceiptAsync(WithdrawAllUndeployedFunction withdrawAllUndeployedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawAllUndeployedFunction, cancellationToken);
        }

        public Task<TransactionReceipt> WithdrawAllUndeployedRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<WithdrawAllUndeployedFunction>(null, cancellationToken);
        }
    }
}
