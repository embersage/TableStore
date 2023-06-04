using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableStore.Models
{
    public class ComputerDesk
    {
        int id;
        bool heightAdjustable;
        string additionalOptions;

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
        public bool HeightAdjustable
        {
            get
            {
                return heightAdjustable;
            }
            set
            {
                heightAdjustable = value;
            }
        }
        public string AdditionalOptions
        {
            get
            {
                return additionalOptions;
            }
            set
            {
                additionalOptions = value;
            }
        }
        public Table Table { get; set; }
    }
}
