using System.Collections.Generic;

namespace DutyFier.Core.Interfaces
{
    public interface IRepository<T>
    {
        ICollection<T> GetAll();
        void Create(T value);
        void Update(T value);
        void Delete(T value);
        T GetOne(int id);
        void AddRange(ICollection<T> values);
    }
}
