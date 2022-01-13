using System.Numerics;

using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Qapo.DeFi.Bot.Core.Models.Web3.LockedStratModels
{
    public partial class DeployFunction : DeployFunctionBase { }

    [Function("deploy")]
    public class DeployFunctionBase : FunctionMessage
    {
    }

    public partial class DepositFunction : DepositFunctionBase { }

    [Function("deposit")]
    public class DepositFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_amount", 1)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class DepositAllFunction : DepositAllFunctionBase { }

    [Function("depositAll")]
    public class DepositAllFunctionBase : FunctionMessage
    {
    }

    public partial class ExecuteFunction : ExecuteFunctionBase { }

    [Function("execute")]
    public class ExecuteFunctionBase : FunctionMessage
    {
    }

    public partial class GetDeployedBalanceFunction : GetDeployedBalanceFunctionBase { }

    [Function("getDeployedBalance", "uint256")]
    public class GetDeployedBalanceFunctionBase : FunctionMessage
    {
    }

    public partial class GetPendingRewardAmountFunction : GetPendingRewardAmountFunctionBase { }

    [Function("getPendingRewardAmount", "uint256")]
    public class GetPendingRewardAmountFunctionBase : FunctionMessage
    {
    }

    public partial class GetTvlFunction : GetTvlFunctionBase { }

    [Function("getTvl", "uint256")]
    public class GetTvlFunctionBase : FunctionMessage
    {
    }

    public partial class GetUndeployedBalanceFunction : GetUndeployedBalanceFunctionBase { }

    [Function("getUndeployedBalance", "uint256")]
    public class GetUndeployedBalanceFunctionBase : FunctionMessage
    {
    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {
    }

    public partial class PanicFunction : PanicFunctionBase { }

    [Function("panic")]
    public class PanicFunctionBase : FunctionMessage
    {
    }

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase { }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {
    }

    public partial class RetireFunction : RetireFunctionBase { }

    [Function("retire")]
    public class RetireFunctionBase : FunctionMessage
    {
    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class UnpanicFunction : UnpanicFunctionBase { }

    [Function("unpanic")]
    public class UnpanicFunctionBase : FunctionMessage
    {
    }

    public partial class UntuckTokensFunction : UntuckTokensFunctionBase { }

    [Function("untuckTokens")]
    public class UntuckTokensFunctionBase : FunctionMessage
    {
        [Parameter("address", "_token", 1)]
        public virtual string Token { get; set; }
    }

    public partial class WithdrawFunction : WithdrawFunctionBase { }

    [Function("withdraw")]
    public class WithdrawFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_amount", 1)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class WithdrawAllFunction : WithdrawAllFunctionBase { }

    [Function("withdrawAll")]
    public class WithdrawAllFunctionBase : FunctionMessage
    {

    }

    public partial class WithdrawAllUndeployedFunction : WithdrawAllUndeployedFunctionBase { }

    [Function("withdrawAllUndeployed")]
    public class WithdrawAllUndeployedFunctionBase : FunctionMessage
    {

    }

    public partial class GetDeployedBalanceOutputDTO : GetDeployedBalanceOutputDTOBase { }

    [FunctionOutput]
    public class GetDeployedBalanceOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetPendingRewardAmountOutputDTO : GetPendingRewardAmountOutputDTOBase { }

    [FunctionOutput]
    public class GetPendingRewardAmountOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetTvlOutputDTO : GetTvlOutputDTOBase { }

    [FunctionOutput]
    public class GetTvlOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetUndeployedBalanceOutputDTO : GetUndeployedBalanceOutputDTOBase { }

    [FunctionOutput]
    public class GetUndeployedBalanceOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
