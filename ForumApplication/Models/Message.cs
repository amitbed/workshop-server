﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApplication.Models
{
    public class Message
    {
        //Variables
        public string ID { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        [Column("Reply_ID")]
        public List<Message> Replies { get; set; }
        private object messageHandler;

        public Message() { }

        //Overload Contructor
        public Message(string title, string content, string userName)
        {
            this.messageHandler = new object();
            if ((String.IsNullOrEmpty(content)) || (String.IsNullOrEmpty(userName)))
            {
                if (String.IsNullOrEmpty(content))
                {
                    Logger.logError("Failed to create a new message. Reason: content is empty");
                }

                if (String.IsNullOrEmpty(userName))
                {
                    Logger.logError("Failed to create a new message. Reason: ID is empty");
                }
            }
            else
            {
                this.ID = IdGen.generateMessageId();
                this.Title = title;
                this.Content = content;
                this.Date = DateTime.Now;
                this.UserName = userName;
                this.Replies = new List<Message>();
            }
        }

        //Methods

        //This method displays the message
        public string displayMessage()
        {
            lock (messageHandler)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Message Id: " + this.ID);
                    sb.Append("Message Date: " + Date);
                    sb.Append("Message Content: " + Content);
                    sb.AppendLine();
                    return sb.ToString();
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return null;
                }
            }
        }

        public bool postReply(Message reply, string replierID)
        {
            lock (messageHandler)
            {
                try
                {
                    if (reply != null)
                    {
                        Replies.Add(reply);
                        Logger.logDebug(string.Format("The new reply: {0} has been created successfully with id {1}", reply.Title, reply.ID));
                        return true;
                    }
                    else
                    {
                        Logger.logError("Failed to add reply. Reason: reply is null");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return false;
                }
            }
        }

        public void delete()
        {
            this.ID = null;
            this.Title = null;
            this.Content = null;
            this.UserName = null;
            this.Replies = null;

        }
    }
}