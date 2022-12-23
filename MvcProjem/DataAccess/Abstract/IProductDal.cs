using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        //productla ilgili tüm interface operasyonlarım hazır artık.
        //Özel operasyonlarım olursa eğer bu kısma yazacagım 
    }
}
