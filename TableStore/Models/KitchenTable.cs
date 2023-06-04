using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableStore.Models
{
    public class KitchenTable
    {
        int id;
        int numberOfSeats;
        bool isExtendable;

        [ForeignKey("Table")]
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
        public int NumberOfSeats
        {
            get
            {
                return numberOfSeats;
            }
            set
            {
                numberOfSeats = value;
            }
        }
        public bool IsExtendable
        {
            get
            {
                return isExtendable;
            }
            set
            {
                isExtendable = value;
            }
        }
        public Table Table { get; set; }
    }
}
