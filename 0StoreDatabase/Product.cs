using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0StoreDatabase
{
    [Serializable]
    public class Product
    {
        public string ModelNumber
        {
            get;set;
        }     
        public string ModelName
        {
            get;set;
        }
       
        public decimal UnitCost
        {
            get;set;
        }

        public string Description
        {
            get;set;
        }
      
        public string CategoryName
        {
            get;set;
        }

        public int CategoryID
        {
            get;set;
        }

        public string ProductImagePath
        {
            get;set;
        }
    }
}
