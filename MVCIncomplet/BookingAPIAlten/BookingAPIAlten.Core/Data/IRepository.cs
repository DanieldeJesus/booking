using BookingAPIAlten.Core.DomainObjects;
using System;

namespace BookingAPIAlten.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
