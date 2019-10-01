using PN.Models;

namespace PN.DataServices
{
    public class MembersServices
    {
        public void Register(string email, string pwd, SainteirPNEntities DBContext = null)
        {
            if(DBContext == null)
            {
                DBContext = new SainteirPNEntities();
            }
            DBContext.members.Add(new members() { email = email });
            DBContext.SaveChanges();
        }
    }
}