using DO;

namespace DalApi
{
    internal interface ICrud<T> where T : struct
    {
        int Add(T a);
        void Update(T a);    
        void Delete(T id);
        T GetByID(int id);

        IEnumerable<T>? GetAll();    
    }
   
}
