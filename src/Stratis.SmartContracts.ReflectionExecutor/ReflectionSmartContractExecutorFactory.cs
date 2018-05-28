﻿using Microsoft.Extensions.Logging;
using NBitcoin;
using Stratis.SmartContracts.Core;
using Stratis.SmartContracts.Core.Receipts;
using Stratis.SmartContracts.Core.State;
using Stratis.SmartContracts.ReflectionExecutor.ContractValidation;

namespace Stratis.SmartContracts.ReflectionExecutor
{
    /// <summary>
    /// Spawns SmartContractExecutor instances
    /// </summary>
    public class ReflectionSmartContractExecutorFactory : ISmartContractExecutorFactory
    {
        private readonly IKeyEncodingStrategy keyEncodingStrategy;
        private readonly ILoggerFactory loggerFactory;
        private readonly Network network;
        private readonly ISmartContractReceiptStorage receiptStorage;
        private readonly SmartContractValidator validator;

        public ReflectionSmartContractExecutorFactory(
            IKeyEncodingStrategy keyEncodingStrategy,
            ILoggerFactory loggerFactory,
            Network network,
            ISmartContractReceiptStorage receiptStorage,
            SmartContractValidator validator)
        {
            this.keyEncodingStrategy = keyEncodingStrategy;
            this.loggerFactory = loggerFactory;
            this.network = network;
            this.receiptStorage = receiptStorage;
            this.validator = validator;
        }

        /// <summary>
        /// Initialize a smart contract executor for the block assembler or consensus validator. 
        /// <para>
        /// After the contract has been executed, it will process any fees and/or refunds.
        /// </para>
        /// </summary>
        public ISmartContractExecutor CreateExecutor(
            ISmartContractCarrier carrier,
            Money mempoolFee,
            IContractStateRepository stateRepository)
        {
            return SmartContractExecutor.Initialize(carrier, this.network, this.receiptStorage, stateRepository, this.validator, this.keyEncodingStrategy, this.loggerFactory, mempoolFee);
        }
    }
}