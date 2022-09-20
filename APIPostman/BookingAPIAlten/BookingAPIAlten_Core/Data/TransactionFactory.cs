using System;
using System.Transactions;

namespace BookingAPIAlten_Core.Data
{
    public abstract class TransactionFactory
    {
        public static TransactionScope CreateReadUncommited()
        {
            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }

        public static TransactionScope Create(bool overrideDefaultIsolationLevel = true)
        {
            return new TransactionScope(TransactionScopeOption.Required, GetTransactionOptions(overrideDefaultIsolationLevel));
        }

        public static TransactionScope Create(IsolationLevel isolationLevel)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = isolationLevel
            };

            return new TransactionScope(TransactionScopeOption.Required, GetTransactionOptions(transactionOptions, true));
        }

        public static TransactionScope Create(TimeSpan scopeTimeout, bool overrideDefaultIsolationLevel = true)
        {
            return new TransactionScope(TransactionScopeOption.Required, GetTransactionOptions(overrideDefaultIsolationLevel, scopeTimeout));
        }

        public static TransactionScope Create(Transaction transactionToUse)
        {
            return new TransactionScope(transactionToUse);
        }

        public static TransactionScope Create(TransactionScopeOption transactScopeOption, bool overrideDefaultIsolationLevel = true)
        {
            return new TransactionScope(transactScopeOption, GetTransactionOptions(overrideDefaultIsolationLevel));
        }

        public static TransactionScope Create(Transaction transactionToUse, TimeSpan scopeTimeout)
        {
            return new TransactionScope(transactionToUse, scopeTimeout);
        }

        public static TransactionScope Create(TransactionScopeOption transactScopeOption, TimeSpan scopeTimeout, bool overrideDefaultIsolationLevel = true)
        {
            var transactionOptions = GetTransactionOptions(overrideDefaultIsolationLevel);
            transactionOptions.Timeout = scopeTimeout;

            return Create(transactScopeOption, transactionOptions);
        }

        public static TransactionScope Create(TransactionScopeOption transactScopeOption, TransactionOptions transactionOptions, bool overrideDefaultIsolationLevel = true)
        {
            return new TransactionScope(transactScopeOption, GetTransactionOptions(transactionOptions, overrideDefaultIsolationLevel));
        }

        public static TransactionScope Create(Transaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
        {
            return new TransactionScope(transactionToUse, scopeTimeout, interopOption);
        }

        public static TransactionScope Create(TransactionScopeOption transactScopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption, bool overrideDefaultIsolationLevel = true)
        {
            return new TransactionScope(transactScopeOption, GetTransactionOptions(transactionOptions, overrideDefaultIsolationLevel), interopOption);
        }

        protected static TransactionOptions GetTransactionOptions(bool overrideDefaultIsolationLevel)
        {
            var transactionOptions = new TransactionOptions();

            return GetTransactionOptions(transactionOptions, overrideDefaultIsolationLevel);
        }

        protected static TransactionOptions GetTransactionOptions(bool overrideDefaultIsolationLevel, TimeSpan timeout)
        {
            var transactionOptions = GetTransactionOptions(overrideDefaultIsolationLevel);

            if (timeout != default)
            {
                transactionOptions.Timeout = timeout;
            }

            return transactionOptions;
        }

        protected static TransactionOptions GetTransactionOptions(TransactionOptions transactionOptions, bool overrideDefaultIsolationLevel)
        {
            if (overrideDefaultIsolationLevel)
            {
                transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            }

            return transactionOptions;
        }
    }
}
