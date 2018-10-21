using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{
    public interface IManagementService<T> where T : class
    {
        T AddItem(T item);
        T GetItem(int id);
        IEnumerable<T> GetItems();
        void RemoveItem(int id);
        void Update(T item);
    }
}