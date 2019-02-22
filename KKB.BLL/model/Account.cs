using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKB.BLL.model
{
    public enum Currency  { KZT, USD,RUR  }
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Currency Currency { get; set; }
        public string AccountNumber { get; set; }
        private double Balance_;
        public double Balance {
            get
            {
                return Balance_;
            }
            set {
                if(value<0)
                    {
                    throw new Exception("Баланс не может быть отрицательным");
                    }
                else
                {
                    Balance_ = value;
                }
            } }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
