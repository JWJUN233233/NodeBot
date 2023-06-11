﻿using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Message;
using NodeBot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeBot.Command
{
    public class AtAll : ICommand
    {
        public bool Execute(ICommandSender sender, string commandLine)
        {
            throw new NotImplementedException();
        }

        public bool Execute(IQQSender QQSender, CqMessage msgs)
        {
            List<CqAtMsg> tmp = new List<CqAtMsg>();
            foreach(var user in ((GroupQQSender)QQSender).Session.GetGroupMemberList(QQSender.GetGroupNumber()!.Value)!.Members)
            {
                tmp.Add(new(user.UserId));
                if(tmp.Count >= 100)
                {
                    ((GroupQQSender)QQSender).Session.SendGroupMessage(QQSender.GetGroupNumber()!.Value, new(tmp));
                    tmp = new();
                }
            }
            ((GroupQQSender)QQSender).Session.SendGroupMessage(QQSender.GetGroupNumber()!.Value, new(tmp));
            return true;
        }

        public int GetDefaultPermission()
        {
            return 10;
        }

        public string GetName()
        {
            return "atall";
        }

        public bool IsConsoleCommand()
        {
            return false;
        }

        public bool IsGroupCommand()
        {
            return true;
        }

        public bool IsUserCommand()
        {
            return false;
        }
    }
}