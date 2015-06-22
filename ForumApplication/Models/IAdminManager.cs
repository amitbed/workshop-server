using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApplication.Models
{
    interface IAdminManager
    {
        bool setProperties(int moderatorNumber);
        bool addSubForum(SubForum subForum, MemberSubForum memberSubForum, ModeratorSubForum moderatorSubForum);
        bool removeSubForum(string subForumName);
        bool upgradeMember(string memberID);
        bool downgradeMember(string memberID);


    }
}
