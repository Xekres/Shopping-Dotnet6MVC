using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        //ABSTRACT VE INTERFACE DISINDA Kİ CLASSLARI YAZABİLMEK İÇİN new(){yenilenebilir yaptım}
        //ilgili terimin veri tabanı nesnesi olması için de IEntity 
        T Get(Expression<Func<T,bool>> filter=null);
        //Bir expression girilebilir diye LINQ kütüphanesinden Expression çektim.
        //kullanıcı bir değer girmeyebilir o yüzden default parametremi null aldım.
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
