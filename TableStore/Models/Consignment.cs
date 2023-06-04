using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableStore.Models
{
    public class Consignment
    {
        int id;
        int providerId;
        int count;
        int purchasePrice;
        DateTime dateOfOperation;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int ProviderId
        {
            get
            {
                return providerId;
            }
            set
            {
                providerId = value;
            }
        }
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }
        public int PurchasePrice
        {
            get
            {
                return purchasePrice;
            }
            set
            {
                purchasePrice = value;
            }
        }
        public DateTime DateOfOperation
        {
            get
            {
                return dateOfOperation;
            }
            set
            {
                dateOfOperation = value;
            }
        }
        public Provider Provider { get;set; }
    }
}
