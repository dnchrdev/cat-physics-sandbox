using System;

namespace Feature.Storage
{
    public interface IStorageDataService
    {
        void Load(Action callback);
        void Save();
    }
}
