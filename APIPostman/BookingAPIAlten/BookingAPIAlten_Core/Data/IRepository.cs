using BookingAPIAlten_Core.Domain;
using System;

namespace BookingAPIAlten_Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
