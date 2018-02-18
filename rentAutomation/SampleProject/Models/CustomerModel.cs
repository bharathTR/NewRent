using System.Collections.Generic;
using System.Web;

namespace SampleProject.Models
{

    public class CustomerModel
    {
        public int id { get; set; }
        public string firstName { get; set; }

        public string middleName { get; set; }

        public string lastName { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string  state { get; set; }
        public string phoneNO { get; set; }

        public int h_id { get; set; }

        public string mobileNo { get; set; }
        public string address { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public string lastLoginDate { get; set; }

        public string houseNo { get; set; }

        public string floorNo { get; set; }

        public string blockNo { get; set; }

        public List<HttpPostedFileBase> Files { get; set; }

        public string Aadhar { get; set; }
        public string VoterID { get; set; }

        public string btnValue { get; set; }


    }
}
 
  