using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApplication.Models
{
    interface IMemberSubForumManager
    {
        bool createThread(Message msg, string threadToAddName);
        bool addMessage(Message msg, string relatedThreadName);
        bool addReply(Message msgReply, string ParentMsgTopic, string threadName);
        bool fileComplaint(string moderatorUsername, string memberUsername);
        bool removeMessage(string memberUsername, string threadName, string messageTopic);
        bool editMessage(string memberUsername, string msgTopic, string msgContent, string threadName);
    }
}
