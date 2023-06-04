using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableStore.Models
{
    public class Order
    {
        int id;
        int clientId;
        int? employeeId;
        DateTime dateOfOperation;
        string status;

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

		public int ClientId
		{
			get
			{
				return clientId;
			}
			set
			{
				clientId = value;
			}
		}

		public int? EmployeeId
		{
			get
			{
				return employeeId;
			}
			set
			{
				employeeId = value;
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

		public string Status
        {
            get
            {
                return status;
            }
            set
            {
				status = value;
            }
        }

		public Client Client { get; set; }

		public Employee Employee { get; set; }

		public IEnumerable<Position> Positions { get; set; }
    }
}