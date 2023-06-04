using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableStore.Models
{
    public class Position
    {
        int id;
        int orderId;
        int tableId;
        int count;
        int sellingPrice;

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
        public int OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                orderId = value;
            }
        }
        public int TableId
        {
            get
            {
                return tableId;
            }
            set
            {
                tableId = value;
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
        public int SellingPrice
        {
            get
            {
                return sellingPrice;
            }
            set
            {
                sellingPrice = value;
            }
        }

        public Table Table { get; set; }
        public Order Order { get; set; }
    }
}