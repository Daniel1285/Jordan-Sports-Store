using DO;

namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        int Add(T a);
        void Update(T? a);
        void Delete(int id);
        IEnumerable<T?> GetAll(Func<T?,bool>? filter = null);
        
        T GetOneObject(Func<T?, bool>? filter);
        IEnumerable<T?> GetAll(Func<T?,bool>? filter = null);    
    }
   
}
