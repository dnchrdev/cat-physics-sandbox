using System;

namespace Features.Storage.Infrastructure.Interfaces
{
    public interface IStorageDataService
    {
        void Load(Action callback);
        void Save();
    }
}
